$TWM2::ArmorHasCollisionFunction[RavagerZombieArmor] = true;

datablock PlayerData(RavagerZombieArmor) : LightMaleBiodermArmor {
	maxDamage = 1.0;
	minImpactSpeed = 50;
	speedDamageScale = 0.015;

	damageScale[$DamageType::M1700] = 4.5;
	damageScale[$DamageType::Wp400] = 4.0;
	damageScale[$DamageType::SCD343] = 4.0;
	damageScale[$DamageType::SA2400] = 5.0;
	damageScale[$DamageType::Model1887] = 4.0;
	damageScale[$DamageType::CrimsonHawk] = 1.9;
	damageScale[$DamageType::AcidCannon] = 3.0;

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

function RavagerZombieArmor::armorCollisionFunction(%datablock, %zombie, %colPlayer) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}
	if(!isObject(%colPlayer) || %colPlayer.getState() $= "dead") {
		return;
	}
	//Check to make sure we're not hitting another zombie / boss
	if(%colPlayer.isBoss || %colPlayer.isZombie || %colPlayer.rapierShield) {
		return;
	}
	//Damage.
	%causeInfect = %zombie.damage_infectOnTouch;
	%baseDamage = %zombie.damage_amountOnTouch;
	%multiplier = %zombie.damage_alreadyInfectedMultiplier;
	
	%total = %colPlayer.infected ? (%baseDamage * %multiplier) : %baseDamage;
	
	%pushVector = vectorscale(%colPlayer.getvelocity(), 100);
	%colPlayer.applyimpulse(%colPlayer.getposition(), %pushVector);
	if(%causeInfect) {
		%colPlayer.Infected = 1;
		%colPlayer.InfectedLoop = schedule(10, %colPlayer, "TWM2Lib_Zombie_Core", "InfectLoop", %colPlayer, "impact");
	}
	%colPlayer.damage(0, %colPlayer.getPosition(), %total, $DamageType::Zombie);	
}

//Ravager Zombies
// TWM2 3.9.2
//  - Old Behavior: Ground crawling zombie with fast speed that would ram into targets
//  ** While these were effective in causing a good number of rage inducing moments, these zombies were more or less cannon fodder.
//  - New Behavior: Ground crawling zombies with fast speed and ambush style attacking
//   - When targeting an enemy, there is a 10% chance every 100ms the ravager will try to initalize an ambush maneuver
//   - The ravager will move to a secondary position in the hopes that the target will become engaged with an alternate target
//   - After reaching the ambush position, the ravager will barrel down on the target
function RavagerZombieArmor::AI(%datablock, %zombie) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}
	if(isSet(%zombie.targetedPlayer)) {
		if(!isObject(%zombie.targetedPlayer) || %zombie.targetedPlayer.getState() $= "dead") {
			%zombie.ambushing = 0;
			%zombie.fullAttack = 0;
			%zombie.ambushPosition = 0;
			%zombie.targetedPlayer = 0;
			%zombie.hasTarget = 0;
		}
	}	
	if(%zombie.ambushing) {
		//We're currently in an ambush maneuver, continue moving to position
		if(vectorDist(%zombie.getPosition(), %zombie.ambushPosition) < 10) {
			//Position reached, resume attack
			%zombie.ambushing = 0;
			%zombie.fullAttack = 1;
			%zombie.ambushPosition = 0;
		}
		else {
			//If the target is near us, break off the ambush and go in for the kill...
			%distanceToTarget = vectorDist(%zombie.getPosition(), %zombie.targetedPlayer.getPosition());
			if(%distanceToTarget < 20) {
				%zombie.ambushing = 0;
				%zombie.fullAttack = 1;
				%zombie.ambushPosition = 0;
			}
			//Otherwise, keep moving...
			%datablock.move(%zombie);
		}
	}
	else {
		if(!%zombie.hasTarget) {
			%targetParams = TWM2Lib_Zombie_Core("lookForTarget", %zombie);
			%target = getWord(targetParams, 0);
			%distance = getWord(%targetParams, 1);
			if(isObject(%target.player)) {
				if(%distance <= $zombie::detectDist) {
					%zombie.hasTarget = 1;
					%zombie.targetedPlayer = %target.player;
				}
			}
			//Outside targeting range, ignore...
		}
		if(%zombie.hasTarget) {
			//Ambush logic, determine if the best plan of action is a ambush, or a direct approach
			%distanceToTarget = vectorDist(%zombie.getPosition(), %zombie.targetedPlayer.getPosition());
			if(%distanceToTarget > 50 && getRandom(1,10) == 1 && !%zombie.ambushing && !%zombie.fullAttack) {
				//Ambush: Move to a side position from the target, then strike.
				%zombie.ambushing = 1;
				%targetPosition = %target.player.getPosition();
				%random = TWM2Lib_MainControl("getrandomposition", "100\t0");
				%rPos = vectorAdd(%targetPosition, %random);
				%z = getTerrainHeight(%rPos);
				%zombie.ambushPosition = getWord(%rPos, 0) SPC getWord(%rPos, 1) SPC %z;
			}
			else {
				//Continue moving to attack.
				%datablock.move(%zombie);
			}
		}
		else {
			//No target, random movement.
			%zombie.zombieRmove = schedule(%zombie.updateTimeFrequency, %zombie, "TWM2Lib_Zombie_Core", "zRandomMoveLoop", %zombie);
			%zombie.setActionThread("ski", true);
		}
	}
	%datablock.schedule(%zombie.updateTimeFrequency, "AI", %zombie);
}

function RavagerZombieArmor::Move(%datablock, %zombie) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}	
	%zombie.setActionThread("scoutRoot", true);
	%pos = %zombie.getWorldBoxCenter();
	%upVec = "250";
	//moanStuff
	TWM2Lib_Zombie_Core("playZAudio", %zombie, 250, 40);
	//Determine target location
	if(%zombie.ambushing) {
		%vector = TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %zombie.ambushPosition);
	}
	else {
		%vector = TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %zombie.targetedPlayer.getPosition());
	}
	//Lunge behavior
	if(!%zombie.ambushing && vectorDist(%zombie.targetedPlayer.getPosition(), %zombie.getPosition()) <= $Zombie::LungeDistance && %zombie.canJump && getWord(%vector, 2) <= 0.8) {
		%zombie.setVelocity("0 0 0");
		%vector = vectorScale(%vector, 2);
		%upvec *= 3.5;
		TWM2Lib_Zombie_Core("setZFlag", %zombie, "canJump", 0);
		schedule($Zombie::BaseJumpCooldown, 0, TWM2Lib_Zombie_Core, "setZFlag", %zombie, "canJump", 1);
	}
	//Scale to speed
	%vector = vectorScale(%vector, %zombie.speed);
	%x = getWord(%vector, 0);
	%y = getWord(%vector, 1);	
	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);	
}