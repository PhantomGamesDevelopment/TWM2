$TWM2::ArmorHasCollisionFunction[ShifterZombieArmor] = true;

datablock PlayerData(ShifterZombieArmor) : LightMaleHumanArmor {
	runForce = 60.20 * 90;
	runEnergyDrain = 0.0;
	minRunEnergy = 10;
	maxForwardSpeed = 9;
	maxBackwardSpeed = 7;
	maxSideSpeed = 7;

	jumpForce = 14.0 * 90;

	maxDamage = 2.8;
	minImpactSpeed = 35;
	shapeFile = "bioderm_light.dts";
	jetEmitter = BiodermArmorJetEmitter;
	jetEffect =  BiodermArmorJetEffect;

	debrisShapeName = "bio_player_debris.dts";

	//Foot Prints
	decalData   = LightBiodermFootprint;
	decalOffset = 0.3;

	waterBreathSound = WaterBreathBiodermSound;

	damageScale[$DamageType::M1700] = 4.5;
	damageScale[$DamageType::Wp400] = 4.0;
	damageScale[$DamageType::SCD343] = 4.0;
	damageScale[$DamageType::SA2400] = 5.0;
	damageScale[$DamageType::Model1887] = 4.0;
	damageScale[$DamageType::CrimsonHawk] = 1.9;

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

function ShifterZombieArmor::armorCollisionFunction(%datablock, %zombie, %colPlayer) {
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

function ShifterZombieArmor::AI(%datablock, %zombie) {
	//Shifter zombies do not employ any "AI" other than target and move, fork off to main move function
	%zombie.moveloop = %datablock.Move(%zombie);
}

function ShifterZombieArmor::Move(%datablock, %zombie) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}
	%pos = %zombie.getWorldBoxCenter();
	%closestClient = TWM2Lib_Zombie_Core("lookForTarget", %zombie);
	%closestDistance = getWord(%closestClient, 1);
	%closestClient = getWord(%closestClient, 0).Player;
	if(%closestDistance <= $zombie::detectDist) {
		if(%zombie.hastarget != 1) {
			%zombie.hastarget = 1;
		}
		TWM2Lib_Zombie_Core("playZAudio", %zombie, 100, 40);
		%vector = TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %closestClient.getPosition());
	
		if(%closestDistance <= $Zombie::LungeDistance && %zombie.canjump == 1) {
			%vector = vectorScale(%vector, 4);
		}
		%vector = vectorScale(%vector, %zombie.speed);
		%upvec = "150";
		
		//tis shift timez :)
		if(%closestDistance > $Zombie::Shifter_Teleport_MaximumRange || (%zombie.getVelocity() == 0 && !%zombie.RecentShift)) {
			%zombie.setMoveState(true);
			%zombie.startFade($Zombie::Shifter_Teleport_PrepTime, 0, true);
			%zombie.schedule($Zombie::Shifter_Teleport_PrepTime + 100, "SetPosition", VectorAdd(%closestClient.getPosition(), vectorAdd("0 0 3", TWM2Lib_MainControl("getRandomPosition", "5\t1"))));
			%zombie.startFade($Zombie::Shifter_Teleport_PrepTime + 250, 0, false);
			%zombie.schedule($Zombie::Shifter_Teleport_PrepTime + 350, setMoveState, false);
			TWM2Lib_Zombie_Core("setZFlag", %zombie, "recentShift", 1);
			schedule($Zombie::Shifter_Teleport_Cooldown, 0, TWM2Lib_Zombie_Core, "setZFlag", %zombie, "recentShift", 0);
		}		
		
		if(%closestDistance <= $Zombie::LungeDistance && %zombie.canjump == 1) {
			%upvec *= 2;
			TWM2Lib_Zombie_Core("setZFlag", %zombie, "canJump", 0);
			schedule($Zombie::BaseJumpCooldown, 0, TWM2Lib_Zombie_Core, "setZFlag", %zombie, "canJump", 1);
		}
		%x = Getword(%vector, 0);
		%y = Getword(%vector, 1);
		%z = Getword(%vector, 2);
		if(%z >= 600) {
			%upvec = (%upvec * 5);
		}
		%vector = %x@" "@%y@" "@%upvec;
		%zombie.applyImpulse(%pos, %vector);
	}
	else if(%zombie.hastarget == 1) {
		%zombie.hastarget = 0;
		%zombie.zombieRmove = schedule(%zombie.updateTimeFrequency, %zombie, "TWM2Lib_Zombie_Core", "zRandomMoveLoop", %zombie);
	}
	%zombie.moveloop = %datablock.schedule(%zombie.updateTimeFrequency, %datablock, "Move", %zombie);	
}