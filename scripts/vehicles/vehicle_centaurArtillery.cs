datablock AudioProfile(CentaurArtilleryFireSound) {
   filename    = "fx/weapons/sniper_miss.wav";
   description = AudioClose3d;
   preload = true;
};

datablock ShockwaveData(MiniColliderWave) {
	className = "ShockwaveData";
	scale = "1 1 1";
	delayMS = "0";
	delayVariance = "0";
	lifetimeMS = "2700";
	lifetimeVariance = "0";
	width = "1";
	numSegments = "60";
	numVertSegments = "30";
	velocity = "10";
	height = "10";
	verticalCurve = "5";
	acceleration = "5";
	times[0] = "0";
	times[1] = "0.25";
	times[2] = "0.9";
	times[3] = "1";
	colors[0] = "1.000000 0.600000 0.200000 1.000000"; //1.0 0.9 0.9
	colors[1] = "1.000000 0.600000 0.200000 1.000000"; //0.6 0.6 0.6
	colors[2] = "1.000000 0.600000 0.200000 1.000000"; //0.6 0.6 0.6
	colors[3] = "1.000000 0.600000 0.200000 0.000000";
	texture[0] = "terraintiles/lavarockhot3";
	texture[1] = "special/shockwave5"; //gradient";
    texWrap = "1";
	is2D = "0";
	mapToTerrain = "0";
	orientToNormal = "1";
	renderBottom = "1";
	renderSquare = "0";
};

datablock ParticleData(MiniColliderExpandersPaticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;

   lifetimeMS           = 7000;
   lifetimeVarianceMS   = 0;
   constantAcceleration = 0.1;

   spinRandomMin = -30.0;
   spinRandomMax = 30.0;
   windcoefficient = 0;
   textureName          = "flarebase";

   colors[0] = "1 0 0";
   colors[1] = "0.8 1 0.2 0";
   colors[2] = "0.8 1 0.2 0";
   colors[3] = "0.8 1 0.2 0";

   sizes[0]      = 1;
   sizes[1]      = 2;
   sizes[2]      = 4;
   sizes[3]      = 5;

   times[0] = "0";
   times[1] = "0.25";
   times[2] = "0.9";
   times[3] = "1";

};

datablock ParticleEmitterData(MiniColliderExpandersEmitter)
{
   lifetimeMS        = 1000;
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;

   ejectionVelocity = 10;
   velocityVariance = 0;
   ejectionoffset   = 5;
   thetaMin         = 0.0;
   thetaMax         = 180.0;

   phiReferenceVel = "0";
   phiVariance = "360";
   orientParticles  = false;
   orientOnVelocity = false;

   particles = "MiniColliderExpandersPaticle";
};

datablock ExplosionData(MiniProtonColliderExplosion) {
   shakeCamera = true;
   camShakeFreq = "20.0 18.0 17.0";
   camShakeAmp = "35 35 35";
   camShakeDuration = 1.3;
   camShakeRadius = 30;

   emitter[0] = "MiniColliderExpandersEmitter";
   Shockwave = "MiniColliderWave";
};

datablock GrenadeProjectileData(MiniColliderShell) {
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.6;
   damageRadius        = 25.0;
   radiusDamageType    = $DamageType::Artillery;
   kickBackStrength    = 500;

   ImageSource         = "MiniColliderCannonImage";

   explosion           = "MiniProtonColliderExplosion";
   velInheritFactor    = 0.0;
   splash              = GrenadeSplash;

   baseEmitter         = GrenadeBubbleEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;

   grenadeElasticity = 0.0;
   grenadeFriction   = 0.3;
   armingDelayMS     = -1;
   gravityMod        = 1.0;
   muzzleVelocity    = 225.0;
   drag              = 0.1;

   sound			 = MortarTurretProjectileSound;

   hasLight    = true;
   lightRadius = 3;
   lightColor  = "0.05 0.2 0.05";
};

//the vehicle
datablock HoverVehicleData(CentaurVehicle) : TankDamageProfile {
   spawnOffset = "0 0 4";
   canControl = true;
   floatingGravMag = 4.5;

   catagory = "Vehicles";
   shapeFile = "vehicle_grav_tank.dts";
   multipassenger = true;
   computeCRC = true;
   renderWhenDestroyed = false;

   weaponNode = 1;

   debrisShapeName = "vehicle_land_assault_debris.dts";
   debris = ShapeDebris;

   drag = 0.0;
   density = 0.9;

   mountPose[0] = sitting;
   numMountPoints = 1;
   isProtectedMountPoint[0] = true;

   cameraMaxDist = 20;
   cameraOffset = 3;
   cameraLag = 1.5;
   explosion = LargeGroundVehicleExplosion;
   explosionDamage = 0.5;
   explosionRadius = 5.0;

   maxSteeringAngle = 0.5;  // 20 deg.

   maxDamage = 3.15;
   destroyedLevel = 3.15;

   isShielded = false;
   rechargeRate = 1.0;
   energyPerDamagePoint = 135;
   maxEnergy = 400;
   minJetEnergy = 15;
   jetEnergyDrain = 2.0;

   // Rigid Body
   mass = 1500;
   bodyFriction = 0.8;
   bodyRestitution = 0.5;
   minRollSpeed = 3;
   gyroForce = 400;
   gyroDamping = 0.3;
   stabilizerForce = 20;
   minDrag = 10;
   softImpactSpeed = 15;       // Play SoftImpact Sound
   hardImpactSpeed = 18;      // Play HardImpact Sound

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 17;
   speedDamageScale = 0.060;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 18;
   collDamageMultiplier   = 0.045;

   dragForce            = 40 / 20;
   vertFactor           = 0.0;
   floatingThrustFactor = 0.15;

   mainThrustForce    = 50;
   reverseThrustForce = 40;
   strafeThrustForce  = 40;
   turboFactor        = 1.7;

   brakingForce = 25;
   brakingActivationSpeed = 4;

   stabLenMin = 3.25;
   stabLenMax = 4;
   stabSpringConstant  = 50;
   stabDampingConstant = 20;

   gyroDrag = 20;
   normalForce = 20;
   restorativeForce = 10;
   steeringForce = 15;
   rollForce  = 5;
   pitchForce = 3;

   dustEmitter = TankDustEmitter;
   triggerDustHeight = 3.5;
   dustHeight = 1.0;
   dustTrailEmitter = TireEmitter;
   dustTrailOffset = "0.0 -1.0 0.5";
   triggerTrailHeight = 3.6;
   dustTrailFreqMod = 15.0;

   jetSound         = AssaultVehicleThrustSound;
   engineSound      = AssaultVehicleEngineSound;
   floatSound       = AssaultVehicleSkid;
   softImpactSound  = GravSoftImpactSound;
   hardImpactSound  = HardImpactSound;
   wheelImpactSound = WheelImpactSound;

   forwardJetEmitter = TankJetEmitter;

   //
   softSplashSoundVelocity = 5.0;
   mediumSplashSoundVelocity = 10.0;
   hardSplashSoundVelocity = 15.0;
   exitSplashSoundVelocity = 10.0;

   exitingWater      = VehicleExitWaterMediumSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterMediumSound;
   impactWaterHard   = VehicleImpactWaterMediumSound;
   waterWakeSound    = VehicleWakeMediumSplashSound;

   minMountDist = 4;

   damageEmitter[0] = SmallLightDamageSmoke;
   damageEmitter[1] = SmallHeavyDamageSmoke;
   damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "0.0 -1.5 3.5 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 1;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDGroundTankIcon;
   cmdMiniIconName = "commander/MiniIcons/com_tank_grey";
   targetNameTag = 'Centaur';
   targetTypeTag = 'Advanced Artillery';
   sensorData = VehiclePulseSensor;

   checkRadius = 5.5535;
   observeParameters = "1 10 10";
   runningLight[0] = TankLight1;
   runningLight[1] = TankLight2;
   runningLight[2] = TankLight3;
   runningLight[3] = TankLight4;
   shieldEffectScale = "0.9 1.0 0.6";
   showPilotInfo = 1;
};

datablock TurretData(CentaurTurret) : TurretDamageProfile {
   className               = VehicleTurret;
   catagory                = "Turrets";
   shapeFile               = "Turret_tank_base.dts";
   preload                 = true;
   canControl = false;
   cmdCategory = "Tactical";
   cmdIcon = CMDGroundTankIcon;
   cmdMiniIconName = "commander/MiniIcons/com_tank_grey";
   targetNameTag = 'Centaur';
   targetTypeTag = 'Cannon';

   mass                    = 1.0;  // Not really relevant
   repairRate              = 0;
   maxDamage               = CentaurVehicle.maxDamage;
   destroyedLevel          = CentaurVehicle.destroyedLevel;

   thetaMin                = 0;
   thetaMax                = 100;
   
   // capacitor
   maxCapacitorEnergy      = 250;
   capacitorRechargeRate   = 1.0;

   inheritEnergyFromMount  = true;
   firstPersonOnly         = true;
   useEyePoint             = true;
   numWeapons              = 4; //2 barrels each set

   max[MiniChaingunAmmo] = 9999;
   max[MortarAmmo] = 9999;
};

//Barrels:
datablock TurretImageData(Cent50CalBarrel) {
   shapeFile = "turret_tank_barrelchain.dts";
   mountPoint = 0;

   projectile = GunshipCGBullet;
   projectileType = TracerProjectile;

   casing              = ShellDebris;
   shellExitDir        = "1.0 0.3 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 15.0;
   shellVelocity       = 3.0;

   projectileSpread = 1.5 / 1000.0;

   usesEnergy = true;

   // Turret parameters
   activationMS                        = 2000;
   deactivateDelayMS                   = 1500;
   thinkTimeMS                         = 200;
   degPerSecTheta                      = 360;
   degPerSecPhi                        = 360;
   attackRadius                        = 200;

   // State transitions
   stateName[0]                        = "Activate";
   stateTransitionOnNotLoaded[0]       = "Dead";
   stateTransitionOnLoaded[0]          = "ActivateReady";
   stateSound[0]                       = AssaultTurretActivateSound;

   stateName[1]                        = "ActivateReady";
   stateSequence[1]                    = "Activate";
   stateSound[1]                       = AssaultTurretActivateSound;
   stateTimeoutValue[1]                = 0.1;
   stateTransitionOnTimeout[1]         = "Ready";
   stateTransitionOnNotLoaded[1]       = "Deactivate";

   stateName[2]                        = "Ready";
   stateTransitionOnNotLoaded[2]       = "Deactivate";
   stateTransitionOnTriggerDown[2]     = "Fire";
   stateTransitionOnNoAmmo[2]          = "NoAmmo";

   stateName[3]                        = "Fire";
   stateSequence[3]                    = "Fire";
   stateSequenceRandomFlash[3]         = true;
   stateFire[3]                        = true;
   stateAllowImageChange[3]            = true;
   stateSound[3]                       = AssaultChaingunFireSound;
   stateScript[3]                      = "onFire";
   stateTimeoutValue[3]                = 0.03;    //0.01
   stateTransitionOnTimeout[3]         = "Fire";
   stateTransitionOnTriggerUp[3]       = "Reload";
   stateTransitionOnNoAmmo[3]          = "noAmmo";

   stateName[4]                        = "Reload";
   stateSequence[4]                    = "Reload";
   stateTimeoutValue[4]                = 0.2;
   stateAllowImageChange[4]            = true;
   stateTransitionOnTimeout[4]         = "Ready";
   stateTransitionOnNoAmmo[4]          = "NoAmmo";
   stateWaitForTimeout[4]              = true;

   stateName[5]                        = "Deactivate";
   stateSequence[5]                    = "Activate";
   stateDirection[5]                   = false;
   stateTimeoutValue[5]                = 0.1;
   stateTransitionOnTimeout[5]         = "ActivateReady";

   stateName[6]                        = "Dead";
   stateTransitionOnLoaded[6]          = "ActivateReady";
   stateTransitionOnTriggerDown[6]     = "DryFire";

   stateName[7]                        = "DryFire";
   stateSound[7]                       = AssaultChaingunFireSound;
   stateTimeoutValue[7]                = 1.0;
   stateTransitionOnTimeout[7]         = "NoAmmo";

   stateName[8]                        = "NoAmmo";
   stateTransitionOnAmmo[8]            = "Reload";
   stateSequence[8]                    = "NoAmmo";
   stateTransitionOnTriggerDown[8]     = "DryFire";
};


datablock TurretImageData(CentaurColliderBarrel) {
   shapeFile = "turret_tank_barrelmortar.dts";
   mountPoint = 0;

//   ammo = AssaultMortarAmmo;
   projectile = MiniColliderShell;
   projectileType = GrenadeProjectile;

   usesEnergy = true;
   useMountEnergy = true;
   fireEnergy = 1.00;
   minEnergy = 1.00;
   useCapacitor = true;

   // Turret parameters
   activationMS                        = 4000;
   deactivateDelayMS                   = 1500;
   thinkTimeMS                         = 200;
   degPerSecTheta                      = 360;
   degPerSecPhi                        = 360;
   attackRadius                        = 750;

   // State transitions
   stateName[0]                        = "Activate";
   stateTransitionOnNotLoaded[0]       = "Dead";
   stateTransitionOnLoaded[0]          = "ActivateReady";

   stateName[1]                        = "ActivateReady";
   stateSequence[1]                    = "Activate";
   stateSound[1]                       = AssaultTurretActivateSound;
   stateTimeoutValue[1]                = 1.0;
   stateTransitionOnTimeout[1]         = "Ready";
   stateTransitionOnNotLoaded[1]       = "Deactivate";

   stateName[2]                        = "Ready";
   stateTransitionOnNotLoaded[2]       = "Deactivate";
   stateTransitionOnNoAmmo[2]          = "NoAmmo";
   stateTransitionOnTriggerDown[2]     = "Fire";

   stateName[3]                        = "Fire";
   stateSequence[3]                    = "Fire";
   stateTransitionOnTimeout[3]         = "Fire2";
   stateTimeoutValue[3]                = 1.0;
   stateFire[3]                        = true;
   stateRecoil[3]                      = LightRecoil;
   stateAllowImageChange[3]            = true;
   stateSound[3]                       = CentaurArtilleryFireSound;
   stateScript[3]                      = "onFire";
   
   stateName[4]                        = "Fire2";
   stateSequence[4]                    = "Fire";
   stateTransitionOnTimeout[4]         = "Fire3";
   stateTimeoutValue[4]                = 1.0;
   stateFire[4]                        = true;
   stateRecoil[4]                      = LightRecoil;
   stateAllowImageChange[4]            = true;
   stateSound[4]                       = CentaurArtilleryFireSound;
   stateScript[4]                      = "onFire";
   
   stateName[5]                        = "Fire3";
   stateSequence[5]                    = "Fire";
   stateTransitionOnTimeout[5]         = "Reload";
   stateTimeoutValue[5]                = 1.0;
   stateFire[5]                        = true;
   stateRecoil[5]                      = LightRecoil;
   stateAllowImageChange[5]            = true;
   stateSound[5]                       = CentaurArtilleryFireSound;
   stateScript[5]                      = "onFire";

   stateName[6]                        = "Reload";
   stateSequence[6]                    = "Reload";
   stateTimeoutValue[6]                = 7.0;
   stateAllowImageChange[6]            = false;
   stateTransitionOnTimeout[6]         = "Ready";
   //stateTransitionOnNoAmmo[4]          = "NoAmmo";
   stateWaitForTimeout[6]              = true;

   stateName[7]                        = "Deactivate";
   stateDirection[7]                   = false;
   stateSequence[7]                    = "Activate";
   stateTimeoutValue[7]                = 1.0;
   stateTransitionOnLoaded[7]          = "ActivateReady";
   stateTransitionOnTimeout[7]         = "Dead";

   stateName[8]                        = "Dead";
   stateTransitionOnLoaded[8]          = "ActivateReady";
   stateTransitionOnTriggerDown[8]     = "DryFire";

   stateName[9]                        = "DryFire";
   stateSound[9]                       = AssaultMortarDryFireSound;
   stateTimeoutValue[9]                = 1.0;
   stateTransitionOnTimeout[9]         = "NoAmmo";

   stateName[10]                        = "NoAmmo";
   stateSequence[10]                    = "NoAmmo";
   stateTransitionOnAmmo[10]            = "Reload";
   stateTransitionOnTriggerDown[10]     = "DryFire";
};


//


function CentaurVehicle::onAdd(%this, %obj) {
   Parent::onAdd(%this, %obj);
   
   setTargetSensorGroup(%obj.getTarget(),%obj.team);

   %turret = TurretData::create(CentaurTurret);
   %turret.selectedWeapon = 1;
   MissionCleanup.add(%turret);
   %turret.team = %obj.teamBought;
   %turret.setSelfPowered();
   %obj.mountObject(%turret, 10);

   %turret.mountImage(Cent50CalBarrel, 0);
   %obj.barrel = "Chain";

   %obj.turretObject = %turret;
   %turret.source = %obj;

   %turret.setCapacitorRechargeRate(999);
   %turret.setAutoFire(true);

   %obj.schedule(6000, "playThread", $ActivateThread, "activate");

   // set the turret's target info
   setTargetSensorGroup(%turret.getTarget(), %obj.team);
   setTargetNeverVisMask(%turret.getTarget(), 0xffffffff);
}

function CentaurVehicle::deleteAllMounted(%data, %obj) {
   %turret = %obj.getMountNodeObject(10);
   if (!%turret)
      return;

   if(%turret.getControllingClient()) {
      %turret.getControllingClient().setControlObject(%turret.getControllingClient().player);
   }

   %turret.schedule(2000, delete);
}

function CentaurVehicle::onTrigger(%data, %obj, %trigger, %state) {
   %plyr = %obj.getMountNodeObject(0);
   if(%state == 1 && %trigger == 5) {
       if(%obj.barrel $= "Chain") {
          %obj.barrel = "Collider";
          %obj.turretObject.schedule(3500, "mountImage", CentaurColliderBarrel, 0);
          BottomPrint(%plyr.client, "Centaur: Switching to collider artillery", 3, 1);
          %obj.setfrozenstate(true);      //lock the artillery into place
          %obj.turretObject.source = %obj;
       }
       else {
          %obj.barrel = "Chain";
          %obj.turretObject.schedule(3500, "mountImage", Cent50CalBarrel, 0);
          BottomPrint(%plyr.client, "Centaur: Switching to 50Cal Chainguns", 3, 1);
          %obj.setfrozenstate(false);
       }
   }
   if(%state == 1 && %trigger == 4) {
      %plyr.client.setControlObject(%obj.turretObject);
   }
}

function CentaurTurret::onTrigger(%data, %obj, %trigger, %state) {
   %cli = %obj.getControllingClient();
   if(%state == 1 && %trigger == 5) {
       if(%obj.source.barrel $= "Chain") {
          %obj.source.barrel = "Collider";
          %obj.schedule(3500, "mountImage", CentaurColliderBarrel, 0);
          BottomPrint(%cli, "Centaur: Switching to collider artillery", 3, 1);
          %obj.source.setfrozenstate(true);      //lock the artillery into place
       }
       else {
          %obj.source.barrel = "Chain";
          %obj.schedule(3500, "mountImage", Cent50CalBarrel, 0);
          BottomPrint(%cli, "Centaur: Switching to 50Cal Chainguns", 3, 1);
          %obj.source.setfrozenstate(false);
       }
   }
   if(%state == 1 && %trigger == 4) {
      %cli.setControlObject(%obj.source);
   }
}

function CentaurTurret::onDamage(%data, %obj) {
   %newDamageVal = %obj.getDamageLevel();
   if(%obj.lastDamageVal !$= "")
      if(isObject(%obj.getObjectMount()) && %obj.lastDamageVal > %newDamageVal)
         %obj.getObjectMount().setDamageLevel(%newDamageVal);
   %obj.lastDamageVal = %newDamageVal;
}
