$TWM2::ArmorHasCollisionFunction[SummonerZombieArmor] = true;

datablock PlayerData(SummonerZombieArmor) : LightMaleHumanArmor {
	runForce = 60.20 * 90;
	runEnergyDrain = 0.0;
	minRunEnergy = 10;
	maxForwardSpeed = 9;
	maxBackwardSpeed = 7;
	maxSideSpeed = 7;

	jumpForce = 14.0 * 90;

	maxDamage = 2.8;
	minImpactSpeed = 35;
	shapeFile = "light_male.dts";
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
	damageScale[$DamageType::AcidCannon] = 3.0;

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

function SummonerZombieArmor::armorCollisionFunction(%datablock, %zombie, %colPlayer) {
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

function SummonerZombieArmor::AI(%datablock, %zombie) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}	
	%pos = %zombie.getWorldBoxCenter();
	%closestClient = TWM2Lib_Zombie_Core("lookForTarget", %zombie);
	%closestDistance = getWord(%closestClient, 1);
	%closestClient = getWord(%closestClient, 0).Player;	
		
	if(isObject(%closestClient) && %closestClient.getState() !$= "dead") {
		if(%closestDistance <= $zombie::detectDist) {
			%zombie.targetPlayer = %closestClient;
			%zombie.moveloop = %datablock.Move(%zombie);
		}
	}
	
	if(isObject(%zombie.targetPlayer)) {
		//Summoner Logic
		if(%zombie.canSummon) {			
			%Ct = GetRandom(1, 3);
			%type = GetRandom(1, 8);
			if(%type > 5) {
				%type += 3;
				if(%type == 10) { //summoners don;t summon more summoners
					%type = 12;
				}
			}
			%SumPos = vectorAdd(VectorAdd(TWM2Lib_MainControl("getRandomPosition", 20 TAB 1), "0 0 7"), %zombie.getPosition());
			%c = CreateEmitter(%SumPos, NightmareGlobeEmitter, "0 0 1");
			%c.schedule(((%Ct * 1000) + 500), "delete");
			for(%i = 1; %i <= %ct; %i++) {
				%time = %i * 1000;
				schedule(%time, 0, "TWM2Lib_Zombie_Core", "SpawnZombie", "zSpawnCommand", %type, %SumPos);
			}
			TWM2Lib_Zombie_Core("setZFlag", %zombie, "canSummon", 0);
			schedule($Zombie::Summoner_Cooldown, 0, TWM2Lib_Zombie_Core, "setZFlag", %zombie, "canSummon", 1);
		}	
	}
	%datablock.schedule(%zombie.updateTimeFrequency, "AI", %zombie);
}

function SummonerZombieArmor::Move(%datablock, %zombie) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}
	%pos = %zombie.getWorldBoxCenter();
	TWM2Lib_Zombie_Core("playZAudio", %zombie, 100, 40);		
	//
	%vector = TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %zombie.targetPlayer.getPosition());	
	if(%closestDistance <= $Zombie::LungeDistance && %zombie.canjump == 1) {
		%vector = vectorScale(%vector, 4);
	}
	%vector = vectorScale(%vector, %zombie.speed);
	%upvec = "150";
	if(vectorDist(%pos, %zombie.targetPlayer.getPosition()) <= $Zombie::LungeDistance && %zombie.canjump == 1) {
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