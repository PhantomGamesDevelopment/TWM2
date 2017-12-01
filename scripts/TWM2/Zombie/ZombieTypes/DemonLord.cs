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

	max[RepairKit]			= 0;
	max[Mine]				= 0;
	max[Grenade]			= 0;
};

datablock StaticShapeData(SubBeacon) {
	shapeFile = "turret_muzzlepoint.dts";
	targetNameTag = 'beacon';
	isInvincible = true;

	dynamicType = $TypeMasks::SensorObjectType;
};

datablock SeekerProjectileData(DMMissile) {
	casingShapeName     = "weapon_missile_casement.dts";
	projectileShapeName = "weapon_missile_projectile.dts";
	hasDamageRadius     = true;
	indirectDamage      = 0.5;
	damageRadius        = 5.0;
	radiusDamageType    = $DamageType::Zombie;
	kickBackStrength    = 2000;

	explosion           = "MissileExplosion";
	splash              = MissileSplash;
	velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
								 // is cranked up to full so the missile doesn't start
								 // out behind the player when the player is moving
								 // very quickly - bramage

	baseEmitter         = MortarSmokeEmitter;
	delayEmitter        = MissileFireEmitter;
	puffEmitter         = MissilePuffEmitter;
	bubbleEmitter       = GrenadeBubbleEmitter;
	bubbleEmitTime      = 1.0;

	exhaustEmitter      = MissileLauncherExhaustEmitter;
	exhaustTimeMs       = 300;
	exhaustNodeName     = "muzzlePoint1";

	lifetimeMS          = 10000; // z0dd - ZOD, 4/14/02. Was 6000
	muzzleVelocity      = 10.0;
	maxVelocity         = 35.0; // z0dd - ZOD, 4/14/02. Was 80.0
	turningSpeed        = 23.0;
	acceleration        = 15.0;

	proximityRadius     = 2.5;

	terrainAvoidanceSpeed = 10;
	terrainScanAhead      = 7;
	terrainHeightFail     = 1;
	terrainAvoidanceRadius = 3;

	flareDistance = 40;
	flareAngle    = 20;
	minSeekHeat   = 0.0;

	sound = MissileProjectileSound;

	hasLight    = true;
	lightRadius = 5.0;
	lightColor  = "0.2 0.05 0";

	useFlechette = true;
	flechetteDelayMs = 250;
	casingDeb = FlechetteDebris;

	explodeOnWaterImpact = false;
};

datablock LinearFlareProjectileData(DMPlasma) {
	doDynamicClientHits = true;

	directDamage        = 0;
	directDamageType    = $DamageType::Zombie;
	hasDamageRadius     = true;
	indirectDamage      = 0.8;  // z0dd - ZOD, 4/25/02. Was 0.5
	damageRadius        = 15.0;
	kickBackStrength    = 1500;
	radiusDamageType    = $DamageType::Zombie;
	explosion           = MortarExplosion;
	splash              = PlasmaSplash;

	dryVelocity       = 85.0; // z0dd - ZOD, 4/25/02. Was 50. Velocity of projectile out of water
	wetVelocity       = -1;
	velInheritFactor  = 1.0;
	fizzleTimeMS      = 4000;
	lifetimeMS        = 2500; // z0dd - ZOD, 4/25/02. Was 6000
	explodeOnDeath    = true;
	reflectOnWaterImpactAngle = 0.0;
	explodeOnWaterImpact      = true;
	deflectionOnWaterImpact   = 0.0;
	fizzleUnderwaterMS        = -1;

	activateDelayMS = 100;

	scale             = "3.0 3.0 3.0";
	numFlares         = 30;
	flareColor        = "0.1 0.3 1.0";
	flareModTexture   = "flaremod";
	flareBaseTexture  = "flarebase";
};

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
	if(%closestClient != -1){
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
							DemonMotherFireRainAttack(%zombie, %searchobject);
						}
					}
					//damn, to close, ok lung at him
					else {			
						DemonMotherLungAttack(%zombie, %searchobject);
					}
				}
				else {
					//ok so theres 3 good possible attacks here, so lets get a random variable and decide what to do.
					%rand = getRandom(1, 5); 
					if(%rand == 1) {
						DemonMotherPlasmaAttack(%zombie, %searchobject);
					}
					else if(%rand <= 3) {
						DemonMotherStrafeAttack(%zombie, %searchobject);
					}
					else {
						DemonMotherFlyAttack(%zombie, %searchobject);
					}
				}
			}
			//ok, were to far away, maybe we should shoot at them.
			else {		
				//humm we just attacked, ok, let charge him, get in close
				if(%zombie.justshot == 1) {	
					DemonMotherChargeIn(%zombie, %searchobject);
				}
				//were good to fire, FIRE AWAY!
				else {	
					//ok so theres 3 good possible attacks here, so lets get a random variable and decide what to do.
					%rand = getRandom(1, 5);	
					if(%rand == 1) {
						DemonMotherFireRainAttack(%zombie, %searchobject);
					}
					else if(%rand <= 3) {
						DemonMotherMissileAttack(%zombie, %searchobject);
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
				DemonMotherDemonSpawn(%zombie);
			}
			else {
				DemonMotherMoveToTarget(%zombie,%searchobject);
			}
		}
		else {
			DemonMotherMoveToTarget(%zombie,%searchobject);
		}
		%zombie.justshot = 0;
		%zombie.justmelee = 0;
	}
	else {
		%zombie.aiRoutine = %datablock.schedule(500, 0, "AIRoutine", %zombie);
	}	
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
		
		case "StrafeMove":
		
		case "FlyAttack":
		
		case "Firestorm":
		
		case "MissileStrike":
		
		case "PlasmaStrike":
		
		case "ChargeAttack":
	}
}

function DemonMotherLungAttack(%obj,%target){
   FaceTarget(%obj,%target);
   %vector = vectorNormalize(vectorSub(%target.getPosition(), %obj.getPosition()));
   %vector = vectorscale(%vector, 4000);
   %x = Getword(%vector,0);
   %y = Getword(%vector,1);
   %z = Getword(%vector,2);
   %vector = %x@" "@%y@" 400";
   %obj.applyImpulse(%obj.getPosition(), %vector);

   %obj.justmelee = 1;
   schedule(750, 0, "DemonMotherThink", %obj);
}

function DemonMotherStrafeAttack(%obj,%target){
   if(!isObject(%obj))
	return;
   if(%obj.getState() $= "dead")
	return;
   if(%obj.chargecount $= "")
	%obj.chargecount = 0;
   if(%obj.chargecount <= 8){
	%obj.setVelocity("0 0 0");
      FaceTarget(%obj,%target);
      %vector = vectorNormalize(vectorSub(%target.getPosition(), %obj.getPosition()));
      %vector = vectorscale(%vector, 3250);
      %x = Getword(%vector,0);
      %y = Getword(%vector,1);
      %nv1 = %y;
      %nv2 = (%x * -1);
      %vector = %nv1@" "@%nv2@" 0";
      %obj.applyImpulse(%obj.getPosition(), %vector);
   }
   else if(%obj.chargecount <= 11){
	%obj.setvelocity("0 0 0");
      FaceTarget(%obj,%target);
      %vector = vectorNormalize(vectorSub(%target.getPosition(), %obj.getPosition()));
      %vector = vectorscale(%vector, 4500);
      %x = Getword(%vector,0);
      %y = Getword(%vector,1);
      %z = Getword(%vector,2);
      %vector = %x@" "@%y@" 150";
      %obj.applyImpulse(%obj.getPosition(), %vector);
   }
   else{
	%obj.chargecount = 0;
	%obj.justmelee = 1;
	schedule(250, 0, "DemonMotherThink", %obj);
	return;
   }
   schedule(250, 0, "DemonMotherStrafeAttack", %obj, %target);
   %obj.chargecount++;
}

function DemonMotherFlyAttack(%obj,%target){
   if(!isObject(%obj))
	return;
   if(%obj.getState() $= "dead")
	return;
   if(%obj.chargecount $= "")
	%obj.chargecount = 0;
   if(%obj.chargecount <= 9){
	FaceTarget(%obj,%target);
	%obj.setvelocity("0 0 10");
	%obj.chargecount++;
	schedule(100, 0, "DemonMotherFlyAttack",%obj,%target);
   }
   else if(%obj.chargecount == 10){
	FaceTarget(%obj,%target);
	%obj.setvelocity("0 0 5");
	%vector = vectorSub(%target.getPosition(),%obj.getPosition());
	%nVec = vectorNormalize(%vector);
	%vector = vectorAdd(%vector,vectorscale(%nvec,-4));
	%obj.attackpos = vectorAdd(%obj.getPosition(),%vector);
	%obj.attackdir = %nVec;
//	echo(%obj.getPosition() SPC %target.getPosition() SPC %obj.attackpos SPC %obj.attackdir);
	%obj.startFade(400, 0, true);
	%obj.chargecount++;
	schedule(400, 0, "DemonMotherFlyAttack",%obj,%target);
   }
   else if(%obj.chargecount >= 11){
	%obj.startFade(500, 0, false);
	%obj.setPosition(%obj.attackpos);
	%obj.setvelocity(vectorscale(%obj.attackdir,25));
	%obj.justmelee = 1;
	%obj.chargecount = 0;
//	echo(%obj.getPosition() SPC %target.getPosition() SPC %obj.attackpos SPC %obj.attackdir);
	%obj.attackpos = "";
	%obj.attackdir = "";
	schedule(1000, 0, "DemonMotherThink",%obj);
   }
}

function DemonMotherFireRainAttack(%obj,%target){
   if(!isObject(%obj))
	return;
   if(%obj.getState() $= "dead")
	return;
   if(%obj.chargecount $= "")
	%obj.chargecount = 0;
   if(%obj.chargecount == 0){
	FaceTarget(%obj, %target);
	for(%i = 0; %i < 10; %i++){
	   %pos = %obj.getPosition();
         %x = getRandom(0,10) - 5;
         %y = getRandom(0,10) - 5;
         %vec = vectorAdd(%pos,%x SPC %y SPC "5");
	   %searchResult = containerRayCast(%vec, vectorAdd(%vec,"0 0 -10"), $TypeMasks::TerrainObjectType, %obj);

         %charge = new ParticleEmissionDummy()
         {
   	      position = posFromRaycast(%searchresult);
   	      dataBlock = "defaultEmissionDummy";
   	      emitter = "BurnEmitter";
         };
         MissionCleanup.add(%charge);
         %charge.schedule(1500, "delete");
	}
	%obj.chargecount++;
	schedule(1000, 0, "DemonMotherFireRainAttack",%obj,%target);
   }
   else{
	%x = (getRandom() * 2) - 1;
	%y = (getRandom() * 2) - 1;
	%z = getRandom();
	%vec = vectorNormalize(%x SPC %y SPC %z);
	%pos = vectorAdd(%target.getPosition(),vectorScale(%vec, 20));
	for(%i = 0;%i < 10;%i++){
	   %x = getRandom(0,14) - 7;
         %y = getRandom(0,14) - 7;
         %spwpos = vectorAdd(%pos,%x SPC %y SPC "2");
   	   %p = new GrenadeProjectile()
   	   {
		dataBlock        = DemonFireball;
		initialDirection = vectorScale(%vec,-1);
		initialPosition  = %spwpos;
		sourceObject     = %obj;
		sourceSlot       = 4;
   	   };
	}
	%obj.justshot = 1;
	%obj.chargecount = 0;
	schedule(1000, 0, "DemonMotherThink",%obj);
   }
}

function DemonMotherMissileAttack(%obj,%target){
   if(!isObject(%obj))
	return;
   if(%obj.getState() $= "dead")
	return;
   if(%obj.chargecount $= "")
	%obj.chargecount = 0;
   if(%obj.chargecount == 0){
	%obj.chargecount++;
	schedule(1000, 0, "DemonMotherMissileAttack", %obj, %target);
   }
   else{
	%vec = vectorNormalize(vectorSub(%target.getPosition(),%obj.getPosition()));
   	%p = new SeekerProjectile()
   	{
	   dataBlock        = DMMissile;
	   initialDirection = %vec;
	   initialPosition  = %obj.getMuzzlePoint(4);
	   sourceObject     = %obj;
	   sourceSlot       = 4;
   	};
   	%beacon = new BeaconObject() {
         dataBlock = "SubBeacon";
         beaconType = "vehicle";
         position = %target.getWorldBoxCenter();
   	};
   	%beacon.team = 0;
   	%beacon.setTarget(0);
   	MissionCleanup.add(%beacon);
	%p.setObjectTarget(%beacon);
	DemonMotherMissileFollow(%target,%beacon,%p);

	%obj.justshot = 1;
	%obj.chargecount = 0;
	schedule(1000, 0, "DemonMotherThink", %obj);
   }
}

function DemonMotherMissileFollow(%target, %beacon, %missile){
   if(!isObject(%target)){
	%beacon.delete();
	return;
   }
   if(!isObject(%missile)){
	%beacon.delete();
	return;
   }
   %beacon.setPosition(%target.getWorldBoxCenter());
   schedule(100, 0, "DemonMotherMissileFollow", %target, %beacon, %missile);
}

function DemonMotherPlasmaAttack(%obj,%target){
   if(!isObject(%obj))
	return;
   if(%obj.getState() $= "dead")
	return;
   if(%obj.chargecount $= "")
	%obj.chargecount = 0;
   if(%obj.chargecount <= 9){
	%obj.setVelocity("0 0 10");
	%obj.chargecount++;
	schedule(100, 0, "DemonMotherPlasmaAttack", %obj, %target);
   }
   else{
	%obj.setVelocity("0 0 3");
	%vec = vectorNormalize(vectorSub(%target.getPosition(),%obj.getPosition()));
   	%p = new LinearFlareProjectile()
   	{
	   dataBlock        = DMPlasma;
	   initialDirection = %vec;
	   initialPosition  = %obj.getMuzzlePoint(4);
	   sourceObject     = %obj;
	   sourceSlot       = 4;
   	};
	%obj.chargecount = 0;
	%obj.justshot = 1;
	schedule(1000, 0, "DemonMotherThink", %obj);
   }
}

function DemonMotherChargeIn(%obj,%target){
   if(!isObject(%obj))
	return;
   if(%obj.getState() $= "dead")
	return;
   if(%obj.chargecount $= "")
	%obj.chargecount = 0;
   if(%obj.chargecount <= 4){
	FaceTarget(%obj, %target);
	%vec = vectorNormalize(vectorSub(%target.getPosition(),%obj.getPosition()));
	%obj.setvelocity(vectorScale(%vec,50));
	%obj.chargecount++;
	schedule(500, 0, "DemonMotherChargeIn", %obj, %target);
   }
   else{
	%obj.justmelee = 1;
	%obj.chargecount = 0;
	DemonMotherThink(%obj);
   }
}

function DemonMotherMoveToTarget(%obj,%target){
   FaceTarget(%obj,%target);
   %vector = vectorNormalize(vectorSub(%target.getPosition(), %obj.getPosition()));
   %vector = vectorscale(%vector, 1200);
   %x = Getword(%vector,0);
   %y = Getword(%vector,1);
   %z = Getword(%vector,2);
   %vector = %x@" "@%y@" 150";
   %obj.applyImpulse(%obj.getPosition(), %vector);

   schedule(500, 0, "DemonMotherThink", %obj);
}

function DemonMotherDemonSpawn(%obj){
   if($TWM::PlayingHellJump || $TWM::PlayingHorde) {
      return;
   }
   for(%i = 0; %i < 5; %i++){
      %pos = %obj.getPosition();
      %x = getRandom(0,200) - 100;
      %y = getRandom(0,200) - 100;
      %vec = vectorAdd(%pos,%x SPC %y SPC "40");
	%searchResult = containerRayCast(%vec, vectorAdd(%vec,"0 0 -80"), $TypeMasks::TerrainObjectType, %obj);

      %charge = new ParticleEmissionDummy()
      {
   	   position = posFromRaycast(%searchresult);
   	   dataBlock = "defaultEmissionDummy";
   	   emitter = "BurnEmitter";
      };
      MissionCleanup.add(%charge);
      %charge.schedule(1100, "delete");
	  schedule(1000, 0, "TWM2Lib_Zombie_Core", "SpawnZombie", "zSpawnCommand", 4, posFromRaycast(%searchResult));
   }
   schedule(1500, 0, "DemonMotherThink", %obj);
}
