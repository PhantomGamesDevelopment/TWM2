//RC
datablock FlyingVehicleData(CruiseMissileVehicle) : ShrikeDamageProfile {
   spawnOffset = "0 0 2";
   canControl = false;
   catagory = "Vehicles";
   shapeFile = "vehicle_grav_scout.dts";
   multipassenger = false;
   computeCRC = true;

   debrisShapeName = "vehicle_grav_scout.dts";
   debris = ShapeDebris;
   renderWhenDestroyed = false;

   drag    = 0.15;
   density = 1.0;

   mountPose[0] = sitting;
   numMountPoints = 1;
   isProtectedMountPoint[0] = false;
   cameraMaxDist = 15;
   cameraOffset = 2.5;
   cameraLag = 0.9;

   explosion = VehicleBombExplosion;
	explosionDamage = 5.0;
	explosionRadius = 35.0;

   maxDamage = 0.5;
   destroyedLevel = 0.5;


   isShielded = false;
   energyPerDamagePoint = 0;
   maxEnergy = 400;      // Afterburner and any energy weapon pool
   rechargeRate = 0.0;

   minDrag = 30;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
   rotationalDrag = 100;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

   maxAutoSpeed = 100000;       // Autostabilizer kicks in when less than this speed. (meters/second)
   autoAngularForce = 100;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
   autoLinearForce = 0;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
   autoInputDamping = 1.0;      // Dampen control input so you don't` whack out at very slow speeds


   // Maneuvering
   maxSteeringAngle = 4.5;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
   horizontalSurfaceForce = 6;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
   verticalSurfaceForce = 4;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
   maneuveringForce = 0;      // Horizontal jets (W,S,D,A key thrust)
   steeringForce = 150;         // Steering jets (force applied when you move the mouse)
   steeringRollForce = 50;      // Steering jets (how much you heel over when you turn)
   rollForce = 10;                // Auto-roll (self-correction to right you after you roll/invert)
   hoverHeight = 5;        // Height off the ground at rest
   createHoverHeight = 3;  // Height off the ground when created
   maxForwardSpeed = 180;  // speed in which forward thrust force is no longer applied (meters/second)

   // Turbo Jet
   jetForce = 6000;      // Afterburner thrust (this is in addition to normal thrust)
   minJetEnergy = 1;     // Afterburner can't be used if below this threshhold.
   jetEnergyDrain = 0.5;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
   vertThrustMultiple = 0.0;

   // Rigid body
   mass = 150;        // Mass of the vehicle
   bodyFriction = 0;     // Don't mess with this.
   bodyRestitution = 0.5;   // When you hit the ground, how much you rebound. (between 0 and 1)
   minRollSpeed = 0;     // Don't mess with this.
   softImpactSpeed = 14;       // Sound hooks. This is the soft hit.
   hardImpactSpeed = 25;    // Sound hooks. This is the hard hit.

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 1;      // If hit ground at speed above this then it's an impact. Meters/second
   speedDamageScale = 0.1;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 1.0;
   collDamageMultiplier   = 0.02;

   //
   minTrailSpeed = 150;      // The speed your contrail shows up at.
   trailEmitter = ContrailEmitter;
   forwardJetEmitter = MissileSmokeEmitter;
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

   minMountDist = 4;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_scout_grey";
   targetNameTag = 'Tactical';
   targetTypeTag = 'Cruise Missile';
   sensorData = PlayerSensor;

   checkRadius = 5.5;
   observeParameters = "1 10 10";

   runningLight[0] = ShrikeLight1;

   shieldEffectScale = "0.937 1.125 0.60";

   replaceTime = 1;
};

//UAV/UAMS
datablock SensorData(UAVSensor) {
   detects = true;
   detectsUsingLOS = true;
   detectsPassiveJammed = true;
   detectsActiveJammed = true;
   detectsCloaked = true;
   detectionPings = true;
   detectRadius = 4000;
};

datablock FlyingVehicleData(UAVVehicle) : ShrikeDamageProfile {
   spawnOffset = "0 0 2";
   canControl = false;
   catagory = "Vehicles";
   shapeFile = "vehicle_grav_scout.dts";
   multipassenger = false;
   computeCRC = true;

   weaponNode = 1;

   debrisShapeName = "vehicle_grav_scout_debris.dts";
   debris = ShapeDebris;
   renderWhenDestroyed = false;

   drag    = 0.2;
   density = 1.0;

   cameraDefaultFov = 90.0;
   cameraMinFov = 5.0;
   cameraMaxFov = 120.0;

   cameraMaxDist = 22;
   cameraOffset = 5;
   cameraLag = 1.0;
   explosion = LargeAirVehicleExplosion;
	explosionDamage = 0.5;
	explosionRadius = 5.0;

   maxDamage = 6.00;     // Total health
   destroyedLevel = 6.00;   // Damage textures show up at this health level

   isShielded = false;
   energyPerDamagePoint = 150;
   maxEnergy = 400;      // Afterburner and any energy weapon pool
   minDrag = 60;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
   rotationalDrag = 1800;        // Angular Drag (dampens the drift after you stop moving the mouse...also tumble drag)
   rechargeRate = 0.8;

   // Auto stabilize speed
   maxAutoSpeed = 15;       // Autostabilizer kicks in when less than this speed. (meters/second)
   autoAngularForce = 1500;      // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
   autoLinearForce = 300;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
   autoInputDamping = 0.95;      // Dampen control input so you don't whack out at very slow speeds

   // Maneuvering
   maxSteeringAngle = 5;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
   horizontalSurfaceForce = 5;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
   verticalSurfaceForce = 8;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
   maneuveringForce = 4700;      // Horizontal jets (W,S,D,A key thrust)
   steeringForce = 1100;         // Steering jets (force applied when you move the mouse)
   steeringRollForce = 300;      // Steering jets (how much you heel over when you turn)
   rollForce = 8;                // Auto-roll (self-correction to right you after you roll/invert)
   hoverHeight = 5;        // Height off the ground at rest
   createHoverHeight = 3;  // Height off the ground when created
   maxForwardSpeed = 85;  // speed in which forward thrust force is no longer applied (meters/second)

   // Turbo Jet
   jetForce = 3000;      // Afterburner thrust (this is in addition to normal thrust)
   minJetEnergy = 40.0;     // Afterburner can't be used if below this threshhold.
   jetEnergyDrain = 3.0;       // Energy use of the afterburners (low number is less drain...can be fractional)
   vertThrustMultiple = 3.0;

   dustEmitter = LargeVehicleLiftoffDustEmitter;
   triggerDustHeight = 4.0;
   dustHeight = 2.0;

   damageEmitter[0] = LightDamageSmoke;
   damageEmitter[1] = HeavyDamageSmoke;
   damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "3.0 -3.0 0.0 ";
   damageEmitterOffset[1] = "-3.0 -3.0 0.0 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 2;

   // Rigid body
   mass = 350;        // Mass of the vehicle
   bodyFriction = 0;     // Don't mess with this.
   bodyRestitution = 0.5;   // When you hit the ground, how much you rebound. (between 0 and 1)
   minRollSpeed = 0;     // Don't mess with this.
   softImpactSpeed = 20;       // Sound hooks. This is the soft hit.
   hardImpactSpeed = 25;    // Sound hooks. This is the hard hit.

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 40;      // If hit ground at speed above this then it's an impact. Meters/second
   speedDamageScale = 0.060;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 25;
   collDamageMultiplier   = 0.020;

   //
   minTrailSpeed = 15;      // The speed your contrail shows up at.
   trailEmitter = ContrailEmitter;
   forwardJetEmitter = FlyerJetEmitter;
   downJetEmitter = FlyerJetEmitter;

   //
   jetSound = BomberFlyerThrustSound;
   engineSound = BomberFlyerEngineSound;
   softImpactSound = SoftImpactSound;
   hardImpactSound = HardImpactSound;
   //wheelImpactSound = WheelImpactSound;

   //
   softSplashSoundVelocity = 15.0;
   mediumSplashSoundVelocity = 20.0;
   hardSplashSoundVelocity = 30.0;
   exitSplashSoundVelocity = 10.0;

   exitingWater      = VehicleExitWaterHardSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterMediumSound;
   impactWaterHard   = VehicleImpactWaterHardSound;
   waterWakeSound    = VehicleWakeHardSplashSound;

   minMountDist = 4;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingBomberIcon;
   cmdMiniIconName = "commander/MiniIcons/com_bomber_grey";
   targetNameTag = 'UAV';
   targetTypeTag = '';
   sensorData = UAVSensor;

   max[MiniChaingunAmmo] = 9999;
   max[MissileLauncherAmmo] = 9999;
   max[MortarAmmo] = 9999;

   checkRadius = 7.1895;
   observeParameters = "1 10 10";
   shieldEffectScale = "0.75 0.975 0.375";
   showPilotInfo = 1;
};

datablock TurretData(MissileSatellite) : TurretDamageProfile {
   className               = VehicleTurret;
   catagory                = "Turrets";
   shapeFile               = "turret_belly_base.dts";
   preload                 = true;
   canControl = true;
   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingBomberIcon;
   cmdMiniIconName = "commander/MiniIcons/com_bomber_grey";
   targetNameTag = 'Un-Manned Aerial';
   targetTypeTag = 'Missile Satellite';

   mass                    = 1.0;  // Not really relevant
   repairRate              = 0;
   maxDamage               = UAVVehicle.maxDamage;
   destroyedLevel          = UAVVehicle.destroyedLevel;

   thetaMin                = 90;
   thetaMax                = 180;

   inheritEnergyFromMount  = true;
   firstPersonOnly         = true;
   useEyePoint             = true;
   numWeapons              = 1;

   max[MissileLauncherAmmo] = 9999;
};

function MissileSatellite::onDamage(%data, %obj) {
   %newDamageVal = %obj.getDamageLevel();
   if(%obj.lastDamageVal !$= "")
      if(isObject(%obj.getObjectMount()) && %obj.lastDamageVal > %newDamageVal)
         %obj.getObjectMount().setDamageLevel(%newDamageVal);
   %obj.lastDamageVal = %newDamageVal;
}

datablock TurretImageData(MissileSatelliteBarrel) {
   shapeFile = "turret_muzzlepoint.dts";
   //item      = PlasmaTurretBarrel;
   
   item      = MissileLauncher;

   // Turret parameters
   activationMS      = 150;
   deactivateDelayMS = 300;
   thinkTimeMS       = 150;
   degPerSecTheta    = 300;
   degPerSecPhi      = 500;
   attackRadius      = 11;
   
   usesEnergy = true;
   useMountEnergy = false;

   // State transitions
   stateName[0]                  = "Activate";
   stateTransitionOnNotLoaded[0] = "Dead";
   stateTransitionOnLoaded[0]    = "ActivateReady";

   stateName[1]                  = "ActivateReady";
   stateSequence[1]              = "Activate";
   stateSound[1]                 = MBLSwitchSound;
   stateTimeoutValue[1]          = 1;
   stateTransitionOnTimeout[1]   = "Ready";
   stateTransitionOnNotLoaded[1] = "Deactivate";

   stateName[2]                    = "Ready";
   stateTransitionOnNotLoaded[2]   = "Deactivate";
   stateTransitionOnTriggerDown[2] = "Fire1";

   stateName[3]                = "Fire1";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3]        = 0.3;
   stateFire[3]                = true;
   stateRecoil[3]              = LightRecoil;
   stateAllowImageChange[3]    = true;
   stateSequence[3]            = "Fire";
   stateSound[3]               = MBLFireSound;
   stateScript[3]              = "onFire";

   stateName[4]                  = "Reload";
   stateTimeoutValue[4]          = 0.8;
   stateAllowImageChange[4]      = true;
   stateSequence[4]              = "Reload";
   stateTransitionOnTimeout[4]   = "Ready";
   stateTransitionOnNotLoaded[4] = "Deactivate";

   stateName[5]                = "Deactivate";
   stateSequence[5]            = "Activate";
   stateDirection[5]           = false;
   stateTimeoutValue[5]        = 1;
   stateTransitionOnLoaded[5]  = "ActivateReady";
   stateTransitionOnTimeout[5] = "Dead";

   stateName[6]               = "Dead";
   stateTransitionOnLoaded[6] = "ActivateReady";
};

function MissileSatellite::onTrigger(%data, %obj, %trigger, %state) {
   if(%state == 1 && %trigger == 5) {
      if(%obj.mountobj.isUnlimitedSat) {
         if(%obj.mountobj.canLaucnhStrike) {
         
            %obj.getcontrollingclient().player.lastTransformStuff = %obj.getcontrollingclient().player.getTransform();
         
            %obj.mountobj.canLaucnhStrike = 0;
            schedule(30000, 0, "ResetSat", %obj.mountobj);
            schedule(100, 0, "MakeCruiseMissile", %obj.getcontrollingclient(), %obj);
            schedule(100, 0, "ServerPlay3d", EscapePodLaunchSound2, %obj.getPosition());
         }
      }
   }
}

function CruiseMissileVehicle::onTrigger(%data, %obj, %trigger, %state) {
   // data = ScoutFlyer datablock
   // obj = ScoutFlyer object number
   // trigger = 0 for "fire", 1 for "jump", 3 for "thrust"
   // state = 1 for firing, 0 for not firing
   if(%state == 1 && %trigger == 5) {
      //detonate.
      %obj.damage(0, %obj.getposition(), 100.0, $DamageType::Admin);
   }
}

function UAVVehicle::onAdd(%this, %obj) {
   Parent::onAdd(%this, %obj);

   setTargetSensorGroup(%obj.getTarget(),%obj.team);
   %turret = TurretData::create(MissileSatellite);
   %turret.selectedWeapon = 1;
   MissionCleanup.add(%turret);
   %turret.setSelfPowered();
   %obj.mountObject(%turret, 10);
   %turret.setCapacitorRechargeRate(999);
   %turret.mountobj = %obj;
   %turret.powerCount = 1;
   %obj.turretObject = %turret;
   %turret.team = %obj.team;
   %turret.base = %obj;
   %turret.mountImage(MissileSatelliteBarrel,0);
   setTargetSensorGroup(%turret.getTarget(),%obj.team);
   
   %turret.setInventory(MissileLauncherAmmo, 9999, true);
   
   %turret.setCloaked(true);
   %obj.setCloaked(true);
}

function UAVVehicle::deleteAllMounted(%data, %obj) {
   %turret = %obj.turretObject;
   if (!%turret) {

   }
   else {
      if(%turret.getControllingClient() != 0) {
         %cl = %turret.getControllingClient();
         if(isObject(%cl.player) && %cl.player.getState() !$= "dead") {
            %cl.setControlObject(%cl.player);
         }
      }
      %turret.schedule(1000, delete);
   }
}
