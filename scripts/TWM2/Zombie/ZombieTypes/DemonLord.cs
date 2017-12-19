$TWM2::ArmorHasCollisionFunction[DemonMotherZombieArmor] = true;

datablock PlayerData(DemonMotherZombieArmor) : LightMaleHumanArmor {
	boundingBox = "1.5 1.5 2.6";
	maxDamage = 9.0;
	minImpactSpeed = 35;
	shapeFile = "medium_female.dts";

	debrisShapeName = "bio_player_debris.dts";

	//Foot Prints
	decalData   = HeavyBiodermFootprint;
	decalOffset = 0.4;

	waterBreathSound = WaterBreathBiodermSound;

	damageScale[$DamageType::M1700] = 4.5;
	damageScale[$DamageType::Wp400] = 4.0;
	damageScale[$DamageType::SCD343] = 4.0;
	damageScale[$DamageType::SA2400] = 5.0;
	damageScale[$DamageType::Model1887] = 4.0;
	damageScale[$DamageType::CrimsonHawk] = 1.9;
	damageScale[$DamageType::plasma] = 0.001;
	damageScale[$DamageType::Napalm] = 0.001;
	damageScale[$DamageType::Burn] = 0.001;
	damageScale[$DamageType::Fire] = 0.001;		

	max[RepairKit]			= 0;
	max[Mine]				= 0;
	max[Grenade]			= 0;
};

function DemonMotherZombieArmor::armorCollisionFunction(%datablock, %zombie, %colPlayer) {
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
	
	//Phantom139 (11/20): Demon Lords light players on fire, need to check for .onfire instead of the .infected flag.
	%total = %colPlayer.onfire ? (%baseDamage * %multiplier) : %baseDamage;
	
	%pushVector = vectorscale(%colPlayer.getvelocity(), 1000);
	%colPlayer.applyimpulse(%colPlayer.getposition(), %pushVector);
	if(%causeInfect) {
		//Phantom139: Demon Zombies now cause burns instead of infects
		//%colPlayer.Infected = 1;
		//%colPlayer.InfectedLoop = schedule(10, %colPlayer, "TWM2Lib_Zombie_Core", "InfectLoop", %colPlayer);
		%colPlayer.maxfirecount += (75 * (%total / 0.5));
		if(%colPlayer.onfire == 0 || %colPlayer.onfire $= ""){
			%colPlayer.onfire = 1;
			schedule(10, %colPlayer, "burnloop", %colPlayer);
		}		
	}
	%colPlayer.damage(0, %colPlayer.getPosition(), %total, $DamageType::Zombie);	
}

function DemonMotherZombieArmor::AI(%datablock, %zombie) {
	//Fork off to both of the AI functions
	%zombie.defenseLoop = %datablock.defenseLoop(%zombie);
	%zombie.aiRoutine = %datablock.AIRoutine(%zombie);
}

function DemonMotherZombieArmor::defenseLoop(%datablock, %zombie) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}
	%pos = %zombie.getposition();
	InitContainerRadiusSearch(%pos, 250, $TypeMasks::ProjectileObjectType);
	while ((%searchObject = containerSearchNext()) != 0) {
		%projpos = %searchobject.getPosition();
		%dist = vectorDist(%pos, %projpos);
		if(%dist <= 100) {
			if(%searchobject.lastpos) {
				%zombie.playShieldEffect("1 1 1");
				%searchobject.delete();
			}
		}
		else {
			%searchobject.lastpos = %projpos;
		}
	}
	%zombie.defenseLoop = %datablock.schedule(50, "defenseLoop", %zombie);
}

function DemonMotherZombieArmor::AIRoutine(%datablock, %zombie) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		%zombie.throwWeapon(1);
		return;
	}
	%pos = %zombie.getWorldBoxCenter();
	%closestClient = TWM2Lib_Zombie_Core("lookForTarget", %zombie);
	%closestDistance = getWord(%closestClient, 1);
	%closestClient = getWord(%closestClient, 0).Player;
	if(%closestClient != -1) {
		%searchobject = %closestclient;
		%dist = vectorDist(%pos, %searchobject.getPosition());
		if(%dist <= 100) {
			//ok were now in combat mode, lets decide on what we should do, move attack, or shoot.
			if(%dist <= 50) {		
				//if we just used a melee attack, maybe we should follow it up with a shot attack.
				if(%zombie.justmelee == 1) {	 
					//good were far enough away, lets shoot em.
					if(%dist >= 10) {	
						%rand = getrandom(1,3);
						if(%rand <= 2) {
							%zombie.attackFunction = %datablock.AttackFunction(%zombie, "AcidStrike", %searchObject); 
						}
						else {
							%zombie.attackFunction = %datablock.AttackFunction(%zombie, "Firestorm", %searchObject); 
						}
					}
					//damn, to close, ok lung at him
					else {			
						%zombie.attackFunction = %datablock.AttackFunction(%zombie, "FireLunge", %searchObject); 
					}
				}
				else {
					//ok so theres 3 good possible attacks here, so lets get a random variable and decide what to do.
					%rand = getRandom(1, 5); 
					if(%rand == 1) {
						%zombie.attackFunction = %datablock.AttackFunction(%zombie, "PlasmaStrike", %searchObject); 
					}
					else if(%rand <= 3) {
						%zombie.attackFunction = %datablock.AttackFunction(%zombie, "StrafeMove", %searchObject); 
					}
					else {
						%zombie.attackFunction = %datablock.AttackFunction(%zombie, "FlyAttack", %searchObject); 
					}
				}
			}
			//ok, were to far away, maybe we should shoot at them.
			else {		
				//humm we just attacked, ok, let charge him, get in close
				if(%zombie.justshot == 1) {	
					%zombie.attackFunction = %datablock.AttackFunction(%zombie, "ChargeAttack", %searchObject); 
				}
				//were good to fire, FIRE AWAY!
				else {	
					//ok so theres 3 good possible attacks here, so lets get a random variable and decide what to do.
					%rand = getRandom(1, 5);	
					if(%rand == 1) {
						%zombie.attackFunction = %datablock.AttackFunction(%zombie, "Firestorm", %searchObject); 
					}
					else if(%rand <= 3) {
						%zombie.attackFunction = %datablock.AttackFunction(%zombie, "MissileStrike", %searchObject); 
					}
					else {
						%zombie.attackFunction = %datablock.AttackFunction(%zombie, "AcidStrike", %searchObject); 
					}
				}
			}
		}
		else if(%dist > 100) {
			%rand = getrandom(1,120);
			//please, dont ask why i choose this number, it just popped in my head
			if(%rand == 94)	{
				%datablock.AttackFunction(%zombie, "SpawnZombies"); 
			}
			else {
				%zombie.moveTarget = %searchObject;
				%datablock.Move(%zombie);
			}
		}
		else {
			%zombie.moveTarget = %searchObject;
			%datablock.Move(%zombie);
		}
		%zombie.justshot = 0;
		%zombie.justmelee = 0;
	}
	else {
		%zombie.aiRoutine = %datablock.schedule(%zombie.updateTimeFrequency, 0, "AIRoutine", %zombie);
	}	
}

function DemonMotherZombieArmor::Move(%datablock, %zombie) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}
	TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %zombie.moveTarget.getPosition());
	%vector = vectorNormalize(vectorSub(%zombie.moveTarget.getPosition(), %zombie.getPosition()));
	%vector = vectorscale(%vector, %zombie.speed);
	%x = Getword(%vector, 0);
	%y = Getword(%vector, 1);
	%z = Getword(%vector, 2);
	%vector = %x@" "@%y@" 150";
	%zombie.applyImpulse(%zombie.getPosition(), %vector);

	%zombie.aiRoutine = %datablock.schedule(%zombie.updateTimeFrequency, 0, "AIRoutine", %zombie);
}

function DemonMotherZombieArmor::AttackFunction(%datablock, %zombie, %attackFunction, %target) {
	if(!isObject(%zombie) || %zombie.getState() $= "dead") {
		return;
	}
	switch$(%attackFunction) {
		case "AcidStrike":
			if(!isObject(%target) || %target.getState() $= "dead") {
				%zombie.aiRoutine = %datablock.AIRoutine(%zombie);
				return;
			}
			%vector = TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %target.getPosition());
			if(%zombie.chargeCount $= "") {
				%zombie.chargeCount = 0;
			}
			%chargeEmitter = new ParticleEmissionDummy() {
				datablock = "defaultEmissionDummy";
				emitter = "BurnEmitter";
				position = %zombie.getMuzzlePoint(4);
			};
			MissionCleanup.add(%chargeEmitter);
			%chargeEmitter.schedule(100, "delete");
			//
			if(%zombie.chargeCount == 7 || %zombie.chargeCount == 9) {
				%vec = vectorSub(%target.getWorldBoxCenter(), %zombie.getMuzzlePoint(4));
				%vec = vectorAdd(%vec, vectorScale(%target.getVelocity(), vectorLen(%vec) / 100));
				%p = new TracerProjectile() {
					dataBlock        = LZombieAcidBall;
					initialDirection = %vec;
					initialPosition  = %zombie.getMuzzlePoint(4);
					sourceObject     = %zombie;
					sourceSlot       = 6;
				};			
			}
			if(%obj.chargecount <= 9) {
				%zombie.attackFunction = %datablock.schedule(100, 0, "AttackFunction", %zombie, %attackFunction, %target);
				%zombie.chargeCount++;
			}
			else {
				%zombie.chargecount = 0;
				%zombie.justshot = 1;
				%zombie.aiRoutine = %datablock.AIRoutine(%zombie);
			}			
		
		case "FireLunge":
			if(!isObject(%target) || %target.getState() $= "dead") {
				%zombie.aiRoutine = %datablock.AIRoutine(%zombie);
				return;
			}	
			if(%zombie.chargeCount $= "") {
				%zombie.chargeCount = 0;
			}
			if(%zombie.chargeCount == 0) {
				TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %target.getPosition());
				%vector = vectorNormalize(vectorSub(%target.getPosition(), %zombie.getPosition()));
				%vector = vectorscale(%vector, 4000);
				%x = Getword(%vector, 0);
				%y = Getword(%vector, 1);
				%z = Getword(%vector, 2);
				%vector = %x@" "@%y@" 400";
				%zombie.applyImpulse(%zombie.getPosition(), %vector);
				%zombie.justmelee = 1;
				
				%chargeEmitter = new ParticleEmissionDummy() {
					datablock = "defaultEmissionDummy";
					emitter = "NapalmExplosionEmitter";
					position = %zombie.getMuzzlePoint(4);
				};
				MissionCleanup.add(%chargeEmitter);
				%chargeEmitter.schedule(100, "delete");				
				
				%zombie.attackFunction = %datablock.schedule(300, 0, "AttackFunction", %zombie, %attackFunction, %target);
			}
			else {
				if(vectorDist(%zombie.getPosition(), %target.getPosition()) < 10) {
					%p = new TracerProjectile() {
						dataBlock        = napalmSubExplosion;
						initialDirection = "0 0 -10";
						initialPosition  = %target.getPosition();
						sourceObject     = %zombie;
						sourceSlot       = 4;
					};
					%p.vector = "0 0 -10";
					%p.count = 1;				
				}
				%zombie.chargecount = 0;
				%zombie.aiRoutine = %datablock.AIRoutine(%zombie);			
			}	
		
		case "StrafeMove":
			if(%zombie.chargecount $= "") {
				%zombie.chargecount = 0;
			}
			if(%zombie.chargecount <= 8) {
				%zombie.setVelocity("0 0 0");
				//FaceTarget(%zombie, %target);
				TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %target.getPosition());
				%vector = vectorNormalize(vectorSub(%target.getPosition(), %zombie.getPosition()));
				%vector = vectorscale(%vector, 3250);
				%x = Getword(%vector, 0);
				%y = Getword(%vector, 1);
				%nv1 = %y;
				%nv2 = (%x * -1);
				%vector = %nv1@" "@%nv2@" 0";
				%zombie.applyImpulse(%zombie.getPosition(), %vector);
			}
			else if(%zombie.chargecount <= 11){
				%zombie.setvelocity("0 0 0");
				TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %target.getPosition());
				%vector = vectorNormalize(vectorSub(%target.getPosition(), %zombie.getPosition()));
				%vector = vectorscale(%vector, 4500);
				%x = Getword(%vector, 0);
				%y = Getword(%vector, 1);
				%z = Getword(%vector, 2);
				%vector = %x@" "@%y@" 150";
				%zombie.applyImpulse(%zombie.getPosition(), %vector);
			}
			else{
				%zombie.chargecount = 0;
				%zombie.justmelee = 1;
				%zombie.aiRoutine = %datablock.AIRoutine(%zombie);
				return;
			}
			%zombie.attackFunction = %datablock.schedule(250, 0, "AttackFunction", %zombie, %attackFunction, %target);
			%zombie.chargecount++;		
		
		case "FlyAttack":
			if(%zombie.chargecount $= "") {
				%zombie.chargecount = 0;
			}
			if(%zombie.chargecount <= 9){
				TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %target.getPosition());
				%zombie.setvelocity("0 0 10");
				%zombie.chargecount++;
				%zombie.attackFunction = %datablock.schedule(100, 0, "AttackFunction", %zombie, %attackFunction, %target);
			}
			else if(%zombie.chargecount == 10) {
				TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %target.getPosition());
				%zombie.setvelocity("0 0 5");
				%vector = vectorSub(%target.getPosition(), %zombie.getPosition());
				%nVec = vectorNormalize(%vector);
				%vector = vectorAdd(%vector, vectorscale(%nvec,-4));
				%zombie.attackpos = vectorAdd(%zombie.getPosition(), %vector);
				%zombie.attackdir = %nVec;
				%zombie.startFade(400, 0, true);
				%zombie.chargecount++;
				%zombie.attackFunction = %datablock.schedule(400, 0, "AttackFunction", %zombie, %attackFunction, %target);
			}
			else if(%zombie.chargecount >= 11){
				%zombie.startFade(500, 0, false);
				%zombie.setPosition(%zombie.attackpos);
				%zombie.setvelocity(vectorscale(%zombie.attackdir, 25));
				%zombie.justmelee = 1;
				%zombie.chargecount = 0;
				%zombie.attackpos = "";
				%zombie.attackdir = "";
				%zombie.aiRoutine = %datablock.AIRoutine(%zombie);
			}		
		
		case "Firestorm":
			if(!isObject(%target) || %target.getState() $= "dead") {
				%zombie.aiRoutine = %datablock.AIRoutine(%zombie);
				return;
			}		
			if(%zombie.chargecount $= "") {
				%zombie.chargecount = 0;
			}
			if(%zombie.chargecount == 0) {
				%vector = TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %target.getPosition());
				for(%i = 0; %i < 10; %i++) {
					%pos = %zombie.getPosition();
					%x = getRandom(0, 10) - 5;
					%y = getRandom(0, 10) - 5;
					%vec = vectorAdd(%pos, %x SPC %y SPC "5");
					%searchResult = containerRayCast(%vec, vectorAdd(%vec,"0 0 -10"), $TypeMasks::TerrainObjectType, %zombie);

					%charge = new ParticleEmissionDummy() {
						position = posFromRaycast(%searchresult);
						dataBlock = "defaultEmissionDummy";
						emitter = "BurnEmitter";
					};
					MissionCleanup.add(%charge);
					%charge.schedule(1500, "delete");
				}
				%zombie.chargecount++;
				%zombie.attackFunction = %datablock.schedule($Zombie::DemonLord_FirestormTrigger, 0, "AttackFunction", %zombie, %attackFunction, %target);
			}
			else {
				%x = (getRandom() * 2) - 1;
				%y = (getRandom() * 2) - 1;
				%z = getRandom();
				%vec = vectorNormalize(%x SPC %y SPC %z);
				%pos = vectorAdd(%target.getPosition(), vectorScale(%vec, 20));
				for(%i = 0; %i < 10; %i++) {
					%x = getRandom(0, 14) - 7;
					%y = getRandom(0, 14) - 7;
					%spwpos = vectorAdd(%pos, %x SPC %y SPC "2");
					%p = new GrenadeProjectile() {
						dataBlock        = DemonFireball;
						initialDirection = vectorScale(%vec, -1);
						initialPosition  = %spwpos;
						sourceObject     = %zombie;
						sourceSlot       = 4;
					};
				}
				%zombie.justshot = 1;
				%zombie.chargecount = 0;
				%zombie.aiRoutine = %datablock.AIRoutine(%zombie);
			}		
		
		case "MissileStrike":
			if(%zombie.chargecount $= "") {
				%zombie.chargecount = 0;
			}
			if(%zombie.chargecount == 0) {
				%zombie.chargecount++;
				%zombie.attackFunction = %datablock.schedule(1000, 0, "AttackFunction", %zombie, %attackFunction, %target);
			}
			else {
				%vec = vectorNormalize(vectorSub(%target.getPosition(), %zombie.getPosition()));
				createMissileSeekingProjectile("DMMissile", %target, %zombie, %zombie.getMuzzlePoint(4), %vec, 4, 100);
				%zombie.justshot = 1;
				%zombie.chargecount = 0;
				%zombie.aiRoutine = %datablock.AIRoutine(%zombie);
			}		
		
		case "PlasmaStrike":
			if(%zombie.chargecount $= "") {
				%zombie.chargecount = 0;
			}
			if(%zombie.chargecount <= 9) {
				%zombie.setVelocity("0 0 10");
				%zombie.chargecount++;
				%zombie.attackFunction = %datablock.schedule(100, 0, "AttackFunction", %zombie, %attackFunction, %target);
			}
			else{
				%zombie.setVelocity("0 0 3");
				%vec = vectorNormalize(vectorSub(%target.getPosition(), %zombie.getPosition()));
				%p = new LinearFlareProjectile() {
					dataBlock        = DMPlasma;
					initialDirection = %vec;
					initialPosition  = %zombie.getMuzzlePoint(4);
					sourceObject     = %zombie;
					sourceSlot       = 4;
				};
				%zombie.chargecount = 0;
				%zombie.justshot = 1;
				%zombie.aiRoutine = %datablock.AIRoutine(%zombie);
			}		
		
		case "ChargeAttack":
			if(%zombie.chargecount $= "") {
				%zombie.chargecount = 0;
			}
			if(%zombie.chargecount <= 4) {
				TWM2Lib_Zombie_Core("zombieGetFacingDirection", %zombie, %target.getPosition());
				%vec = vectorNormalize(vectorSub(%target.getPosition(), %zombie.getPosition()));
				%zombie.setvelocity(vectorScale(%vec, 50));
				%zombie.chargecount++;
				%zombie.attackFunction = %datablock.schedule(500, 0, "AttackFunction", %zombie, %attackFunction, %target);
			}
			else{
				%zombie.justmelee = 1;
				%zombie.chargecount = 0;
				%zombie.aiRoutine = %datablock.AIRoutine(%zombie);
			}	
			
		case "SpawnZombies":
			if($TWM::PlayingHellJump || $TWM::PlayingHorde) {
				return;
			}
			for(%i = 0; %i < 5; %i++) {
				%pos = %zombie.getPosition();
				%x = getRandom(0, 200) - 100;
				%y = getRandom(0, 200) - 100;
				%vec = vectorAdd(%pos, %x SPC %y SPC "40");
				%searchResult = containerRayCast(%vec, vectorAdd(%vec,"0 0 -80"), $TypeMasks::TerrainObjectType, %zombie);

				%charge = new ParticleEmissionDummy() {
					position = posFromRaycast(%searchresult);
					dataBlock = "defaultEmissionDummy";
					emitter = "BurnEmitter";
				};
				MissionCleanup.add(%charge);
				%charge.schedule(1100, "delete");
				schedule(1000, 0, "TWM2Lib_Zombie_Core", "SpawnZombie", "zSpawnCommand", 4, posFromRaycast(%searchResult));
			}
			%zombie.aiRoutine = %datablock.AIRoutine(%zombie);	
	}
}