datablock FlyingVehicleData(Harrier) : ShrikeDamageProfile {
   spawnOffset = "0 0 2";
   canControl = false;
   catagory = "Vehicles";
   shapeFile = "vehicle_air_scout.dts";
   computeCRC = true;

   debrisShapeName = "vehicle_air_scout_debris.dts";
   debris = ShapeDebris;
   renderWhenDestroyed = false;

   mountPose[0] = sitting;
   numMountPoints = 1;
   isProtectedMountPoint[0] = true;

   drag    = 0.15;
   density = 1.0;

   canWarp = 1;

   cameraMaxDist = 15;
   cameraOffset = 2.5;
   cameraLag = 0.9;
   explosion = VehicleExplosion;
	explosionDamage = 0.5;
	explosionRadius = 5.0;

   maxDamage = 5.40;
   destroyedLevel = 5.40;

   minDrag = 30;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
   rotationalDrag = 900;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

   maxAutoSpeed = 5;       // Autostabilizer kicks in when less than this speed. (meters/second)
   autoAngularForce = 50;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
   autoLinearForce = 300;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
   autoInputDamping = 0.95;      // Dampen control input so you don't` whack out at very slow speeds

   // Maneuvering
   maxSteeringAngle = 5;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
   horizontalSurfaceForce = 6;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
   verticalSurfaceForce = 8;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
   maneuveringForce = 3000;      // Horizontal jets (W,S,D,A key thrust)
   steeringForce = 1200;         // Steering jets (force applied when you move the mouse)
   steeringRollForce = 1600;      // Steering jets (how much you heel over when you turn)
   rollForce = 18;                // Auto-roll (self-correction to right you after you roll/invert)
   hoverHeight = 2;        // Height off the ground at rest
   createHoverHeight = 3;  // Height off the ground when created
   maxForwardSpeed = 400;  // speed in which forward thrust force is no longer applied (meters/second)

   // Turbo Jet
   jetForce = 3000;      // Afterburner thrust (this is in addition to normal thrust)
   minJetEnergy = 1;     // Afterburner can't be used if below this threshhold.
   jetEnergyDrain = 0;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
   vertThrustMultiple = 3.0;

   // Rigid body
   mass = 150;        // Mass of the vehicle
   bodyFriction = 0;     // Don't mess with this.
   bodyRestitution = 0.5;   // When you hit the ground, how much you rebound. (between 0 and 1)
   minRollSpeed = 0;     // Don't mess with this.
   softImpactSpeed = 14;       // Sound hooks. This is the soft hit.
   hardImpactSpeed = 25;    // Sound hooks. This is the hard hit.

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 10;      // If hit ground at speed above this then it's an impact. Meters/second
   speedDamageScale = 0.06;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 23.0;
   collDamageMultiplier   = 0.02;

   //
   minTrailSpeed = 15;      // The speed your contrail shows up at.
   trailEmitter = ContrailEmitter;
   forwardJetEmitter = FlyerJetEmitter;
   downJetEmitter = FlyerJetEmitter;

   //
   jetSound = ScoutFlyerThrustSound;
   engineSound = ScoutFlyerEngineSound;
   softImpactSound = SoftImpactSound;
   hardImpactSound = HardImpactSound;
   //wheelImpactSound = WheelImpactSound;

   //
   softSplashSoundVelocity = 10.0;
   mediumSplashSoundVelocity = 15.0;
   hardSplashSoundVelocity = 20.0;
   exitSplashSoundVelocity = 10.0;

   exitingWater      = VehicleExitWaterMediumSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterMediumSound;
   impactWaterHard   = VehicleImpactWaterMediumSound;
   waterWakeSound    = VehicleWakeMediumSplashSound;

   dustEmitter = VehicleLiftoffDustEmitter;
   triggerDustHeight = 4.0;
   dustHeight = 1.0;

   damageEmitter[0] = LightDamageSmoke;
   damageEmitter[1] = HeavyDamageSmoke;
   damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "0.0 -3.0 0.0 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 1;

   //
   max[MiniChaingun] = 1;
   max[MiniChaingunAmmo] = 1000;

   minMountDist = 4;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_scout_grey";
   targetNameTag = 'Plasma';
   targetTypeTag = 'Harrier';
   sensorData = VehiclePulseSensor;
   sensorRadius = VehiclePulseSensor.detectRadius;
   sensorColor = "255 194 9";

   checkRadius = 5.5;
   observeParameters = "1 10 10";

   runningLight[0] = ShrikeLight1;
//   runningLight[1] = ShrikeLight2;

   shieldEffectScale = "0.937 1.125 0.60";

};

datablock TurretData(HarrierTurret) : TurretDamageProfile
{
   className               = VehicleTurret;
   catagory                = "Turrets";
   shapeFile               = "turret_belly_base.dts";
   preload                 = true;
   canControl              = false;
   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingBomberIcon;
   cmdMiniIconName = "commander/MiniIcons/com_bomber_grey";
   targetNameTag = 'Harrier Bottom';
   targetTypeTag = 'Turret';

   mass                    = 1.0;  // Not really relevant
   repairRate              = 0;
   maxDamage               = Harrier.maxDamage;
   destroyedLevel          = Harrier.destroyedLevel;
   
   maxEnergy = 5000;
   
   max[MiniChaingun] = 1;
   max[MiniChaingunAmmo] = 1000;

   thetaMin                = 90;
   thetaMax                = 180;

   inheritEnergyFromMount  = true;
   firstPersonOnly         = true;
   useEyePoint             = true;
   numWeapons              = 1;
};

datablock TurretImageData(HarrierPlasmaBarrelImage) {
   shapeFile = "turret_belly_barrell.dts";
   offset = "-0.3 0 0";
   mountPoint = 0;

   item      = MiniChaingun;
   ammo      = MiniChaingunAmmo;

   projectile = BomberFusionBolt;
   projectileType = LinearFlareProjectile;
   emap = true;

   casing              = ShellDebris;
   shellExitDir        = "1.0 0.3 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 5.0;
   shellVelocity       = 0.0;

   usesEnergy = false;
   useMountEnergy = false;

   // Turret parameters
   activationMS         = 175; // z0dd - ZOD, 3/27/02. Was 250. Amount of time it takes turret to unfold
   deactivateDelayMS    = 500;
   thinkTimeMS          = 140; // z0dd - ZOD, 3/27/02. Was 200. Amount of time before turret starts to unfold (activate)
   degPerSecTheta       = 600;
   degPerSecPhi         = 1080;
   attackRadius         = 200;

   stateName[0]                     = "Activate";
   stateTransitionOnTimeout[0]      = "WaitFire1";
   stateTimeoutValue[0]             = 0.5;
   stateSequence[0]                 = "Activate";
   stateSound[0]                    = BomberTurretActivateSound;

   stateName[1]                     = "WaitFire1";
   stateTransitionOnTriggerDown[1]  = "Fire1";
   stateTransitionOnNoAmmo[1]       = "NoAmmo1";

   stateName[2]                     = "Fire1";
   stateTransitionOnTimeout[2]      = "Reload1";
   stateTimeoutValue[2]             = 0.2;
   stateFire[2]                     = true;
   stateRecoil[2]                   = LightRecoil;
   stateAllowImageChange[2]         = false;
   stateSequence[2]                 = "Fire";
   stateScript[2]                   = "onFire";
   stateSound[2]                    = BomberTurretFireSound;

   stateName[3]                     = "Reload1";
   stateSequence[3]                 = "Reload";
   stateTimeoutValue[3]             = 0.1;
   stateAllowImageChange[3]         = false;
   stateTransitionOnTimeout[3]      = "WaitFire1";
   stateTransitionOnNoAmmo[3]       = "NoAmmo1";

   stateName[4]                     = "NoAmmo1";
   stateTransitionOnAmmo[4]         = "Reload1";
   // ---------------------------------------------
   // z0dd - ZOD, 5/8/02. Incorrect parameter value
   //stateSequence[4]                 = "NoAmmo";
   stateSequence[4] = "NoAmmo1";

   stateTransitionOnTriggerDown[4]  = "DryFire1";

   stateName[5]                     = "DryFire1";
   stateSound[5]                    = BomberTurretDryFireSound;
   stateTimeoutValue[5]             = 0.5;
   stateTransitionOnTimeout[5]      = "NoAmmo1";
};


function Harrier::deleteAllMounted(%data, %obj) {
   %turret = %obj.turretObject;
   if (!%turret)
      return;

   %turret.schedule(1000, delete);
}

function Harrier::onAdd(%this, %obj) {
   Parent::onAdd(%this, %obj);

   if (%obj.clientControl)
       serverCmdResetControlObject(%obj.clientControl);

   %obj.mountImage(ScoutChaingunParam, 0);
   %obj.mountImage(ScoutChaingunImage, 2);
   %obj.mountImage(ScoutChaingunPairImage, 3);
   %obj.mountImage(ShrikeMissileImage, 4);
   %obj.mountImage(ShrikebombImage, 5);
   %obj.selectedWeapon = 1;
   %obj.nextWeaponFire = 1;
   %obj.setInventory(MissileLauncherAmmo, 4);
   %obj.setInventory(MortarAmmo, 2);
   %obj.setInventory(MiniChaingunAmmo, 1500);

   %obj.playThread($activatethread, "activate");

   setTargetSensorGroup(%obj.getTarget(),%obj.team);
   %turret = TurretData::create(HarrierTurret);
   %turret.selectedWeapon = 1;
   MissionCleanup.add(%turret);
   %turret.setSelfPowered();
   %obj.mountObject(%turret, 10);
   %turret.setCapacitorRechargeRate(999);
   %turret.mountobj = %obj;
   %obj.turretObject = %turret;
   %turret.team = %obj.team;
   %turret.base = %obj;
   %turret.mountImage(HarrierPlasmaBarrelImage,0);
   setTargetSensorGroup(%turret.getTarget(),%obj.team);
   %turret.setAutoFire(true); //YES
   %turret.setInventory(MiniChaingunAmmo, 9999, true);
}

function HarrierTurret::onDamage(%data, %obj) {
   %newDamageVal = %obj.getDamageLevel();
   if(%obj.lastDamageVal !$= "")
      if(isObject(%obj.getObjectMount()) && %obj.lastDamageVal > %newDamageVal)
         %obj.getObjectMount().setDamageLevel(%newDamageVal);
   %obj.lastDamageVal = %newDamageVal;
}
