datablock SeekerProjectileData(HornetStrikeMissile) {
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   hasDamageRadius     = true;
   indirectDamage      = 1.4;
   damageRadius        = 20.0;
   radiusDamageType    = $DamageType::RPG;
   kickBackStrength    = 3500;

   explosion           = "SatchelMainExplosion";
   splash              = MissileSplash;
   velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
                                 // is cranked up to full so the missile doesn't start
                                 // out behind the player when the player is moving
                                 // very quickly - bramage

   baseEmitter         = MissileSmokeEmitter;
   delayEmitter        = MissileFireEmitter;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = 5000; // z0dd - ZOD, 4/14/02. Was 6000
   muzzleVelocity      = 70.0;
   maxVelocity         = 350.0; // z0dd - ZOD, 4/14/02. Was 80.0
   turningSpeed        = 54.0;
   acceleration        = 75.0;

   explodeOnDeath = true;

   proximityRadius     = 3;

   sound = MissileProjectileSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0.2 0.05 0";

   useFlechette = true;
   flechetteDelayMs = 10;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = true;
};

datablock ParticleData(ACCGSmoke)
{
   dragCoeffiecient     = 0.05;
   gravityCoefficient   = 0.1;
   inheritedVelFactor   = 0.025;

   lifetimeMS           = 1250;
   lifetimeVarianceMS   = 150;

   textureName          = "particleTest";

   useInvAlpha =  true;
   spinRandomMin = -25.0;
   spinRandomMax =  25.0;

   textureName = "special/Smoke/bigSmoke";

   colors[0]     = "0.3 0.3 0.3 1.0";
   colors[1]     = "0.4 0.4 0.4 0.5";
   colors[2]     = "0.5 0.5 0.5 0.0";
   sizes[0]      = 1.0;
   sizes[1]      = 1.5;
   sizes[2]      = 2.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ACCGSmokeEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;

   ejectionOffset = 0.1;

   ejectionVelocity = 4.0;
   velocityVariance = 3.5;

   thetaMin         = 85.0;
   thetaMax         = 90.0;

   lifetimeMS       = 100;

   particles = "ACCGSmoke";

};

datablock ParticleEmitterData(ACCGSmokeEmitter2)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;

   ejectionOffset = 0.1;

   ejectionVelocity = 5.5;
   velocityVariance = 5.5;

   thetaMin         = 0.0;
   thetaMax         = 15.0;

   lifetimeMS       = 150;

   particles = "ACCGSmoke";

};

datablock ExplosionData(ACCGSubExplosion)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer = true;
   delayMS = 0;
   offset = 0.0;
   playSpeed = 2.0;
   sizes[0] = "0.1 0.1 0.1";
   sizes[1] = "0.1 0.1 0.1";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(ACCGExplosion)
{
   soundProfile   = plasmaExpSound;
   subExplosion[0] = ACCGSubExplosion;
   emitter[0] = ACCGSmokeEmitter;
   emitter[1] = ACCGSmokeEmitter2;
   shakeCamera = false;
};

datablock TracerProjectileData(GunshipCGBullet) {
   doDynamicClientHits = true;

   directDamage        = 0.0;
   directDamageType    = $DamageType::ACCG;
   explosion           = ChaingunExplosion;
   splash              = ChaingunSplash;

   hasDamageRadius     = true;
   indirectDamage      = 0.03;
   damageRadius        = 3.0;
   radiusDamageType    = $DamageType::ACCG;

   kickBackStrength  = 5;
   sound             = ChaingunProjectile;

   dryVelocity       = 1500.0;
   wetVelocity       = 100.0;
   velInheritFactor  = 0.0;
   fizzleTimeMS      = 3000;
   lifetimeMS        = 6000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 40.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.25;
   crossSize       = 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;

   decalData[0] = ChaingunDecal1;
   decalData[1] = ChaingunDecal2;
   decalData[2] = ChaingunDecal3;
   decalData[3] = ChaingunDecal4;
   decalData[4] = ChaingunDecal5;
   decalData[5] = ChaingunDecal6;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.5 0.5 0.175";
};

datablock TracerProjectileData(ApacheCGBullet) {
   doDynamicClientHits = true;

   directDamage        = 0.0;
   directDamageType    = $DamageType::ACCG;
   explosion           = ChaingunExplosion;
   splash              = ChaingunSplash;

   hasDamageRadius     = true;
   indirectDamage      = 0.1;
   damageRadius        = 5.5;
   radiusDamageType    = $DamageType::ACCG;

   kickBackStrength  = 5;
   sound             = ChaingunProjectile;

   dryVelocity       = 1500.0;
   wetVelocity       = 100.0;
   velInheritFactor  = 0.0;
   fizzleTimeMS      = 3000;
   lifetimeMS        = 6000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 65.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 1.25;
   crossSize       = 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;

   decalData[0] = ChaingunDecal1;
   decalData[1] = ChaingunDecal2;
   decalData[2] = ChaingunDecal3;
   decalData[3] = ChaingunDecal4;
   decalData[4] = ChaingunDecal5;
   decalData[5] = ChaingunDecal6;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.5 0.5 0.175";
};

datablock SensorData(SSTurretBaseSensorObj) {
   detects = true;
   detectsUsingLOS = true;
   detectsPassiveJammed = false;
   detectsActiveJammed = false;
   detectsCloaked = true;
   detectionPings = true;
   detectRadius = 700;
};

datablock FlyingVehicleData(CombatHelicopter) : ShrikeDamageProfile {
   spawnOffset = "0 0 2";
   canControl = false;
   catagory = "Vehicles";
   shapeFile = "vehicle_air_scout.dts";
   computeCRC = true;

   debrisShapeName = "vehicle_air_scout_debris.dts";
   debris = ShapeDebris;
   renderWhenDestroyed = false;

   mountPose[0] = sitting;
   mountPose[1] = sitting;
   numMountPoints = 2;
   isProtectedMountPoint[0] = true;
   isProtectedMountPoint[1] = true;

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

   isShielded = false;
   energyPerDamagePoint = 0;
   maxEnergy = 150;      // Afterburner and any energy weapon pool
   rechargeRate = 1.0;

   minDrag = 10;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
   rotationalDrag = 700;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

   maxAutoSpeed = 15;       // Autostabilizer kicks in when less than this speed. (meters/second)
   autoAngularForce = 400;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
   autoLinearForce = 300;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
   autoInputDamping = 0.95;      // Dampen control input so you don't` whack out at very slow speeds

   // Maneuvering
   maxSteeringAngle = 7;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
   horizontalSurfaceForce = 4;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
   verticalSurfaceForce = 2;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
   maneuveringForce = 10;      // Horizontal jets (W,S,D,A key thrust)
   steeringForce = 620;         // Steering jets (force applied when you move the mouse)
   steeringRollForce = 35;     // Steering jets (how much you heel over when you turn)
   rollForce = 20;                //  was 20 Auto-roll (self-correction to right you after you roll/invert)
   hoverHeight = 1;        // Height off the ground at rest
   createHoverHeight = 3;  // Height off the ground when created
   maxForwardSpeed = 50;  // speed in which forward thrust force is no longer applied (meters/second)

   // Turbo Jet
   jetForce = 1250;      // Afterburner thrust (this is in addition to normal thrust)
   minJetEnergy = 1;     // Afterburner can't be used if below this threshhold.
   jetEnergyDrain = 0.1;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
   vertThrustMultiple = 3.5;

   // Rigid body
   mass = 125;        // Mass of the vehicle
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
   max[MiniChaingunAmmo] = 1000;

   minMountDist = 4;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_scout_grey";
   targetNameTag = 'Combat';
   targetTypeTag = 'Helicopter';
   sensorData = AWACPulseSensor;
   sensorRadius = AWACPulseSensor.detectRadius;
   sensorColor = "255 194 9";

   checkRadius = 5.5;
   observeParameters = "1 10 10";

   runningLight[0] = ShrikeLight1;
//   runningLight[1] = ShrikeLight2;

   shieldEffectScale = "0.937 1.125 0.60";
};

//Gunship Heli
datablock FlyingVehicleData(GunshipHelicopter) : ShrikeDamageProfile {
   spawnOffset = "0 0 2";
   canControl = false;
   catagory = "Vehicles";
   shapeFile = "vehicle_air_scout.dts";
   computeCRC = true;

   debrisShapeName = "vehicle_air_scout_debris.dts";
   debris = ShapeDebris;
   renderWhenDestroyed = false;

   mountPose[0] = sitting;
   mountPose[1] = sitting;
   numMountPoints = 2;
   isProtectedMountPoint[0] = true;
   isProtectedMountPoint[1] = true;

   drag    = 0.15;
   density = 1.0;

   canWarp = 1;

   cameraMaxDist = 15;
   cameraOffset = 2.5;
   cameraLag = 0.9;
   explosion = VehicleExplosion;
	explosionDamage = 0.5;
	explosionRadius = 5.0;

   maxDamage = 9.40;
   destroyedLevel = 9.40;

   isShielded = false;
   energyPerDamagePoint = 0;
   maxEnergy = 150;      // Afterburner and any energy weapon pool
   rechargeRate = 1.0;

   isShielded = false;
   energyPerDamagePoint = 0;
   maxEnergy = 150;      // Afterburner and any energy weapon pool
   rechargeRate = 1.0;

   minDrag = 10;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
   rotationalDrag = 700;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

   maxAutoSpeed = 15;       // Autostabilizer kicks in when less than this speed. (meters/second)
   autoAngularForce = 400;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
   autoLinearForce = 300;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
   autoInputDamping = 0.95;      // Dampen control input so you don't` whack out at very slow speeds

   // Maneuvering
   maxSteeringAngle = 7;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
   horizontalSurfaceForce = 4;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
   verticalSurfaceForce = 2;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
   maneuveringForce = 10;      // Horizontal jets (W,S,D,A key thrust)
   steeringForce = 620;         // Steering jets (force applied when you move the mouse)
   steeringRollForce = 35;     // Steering jets (how much you heel over when you turn)
   rollForce = 20;                //  was 20 Auto-roll (self-correction to right you after you roll/invert)
   hoverHeight = 1;        // Height off the ground at rest
   createHoverHeight = 3;  // Height off the ground when created
   maxForwardSpeed = 50;  // speed in which forward thrust force is no longer applied (meters/second)

   // Turbo Jet
   jetForce = 1250;      // Afterburner thrust (this is in addition to normal thrust)
   minJetEnergy = 1;     // Afterburner can't be used if below this threshhold.
   jetEnergyDrain = 0.1;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
   vertThrustMultiple = 3.5;

   // Rigid body
   mass = 125;        // Mass of the vehicle
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
   max[MiniChaingunAmmo] = 1000;

   minMountDist = 4;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_scout_grey";
   targetNameTag = 'Gunship';
   targetTypeTag = 'Helicopter';
   sensorData = AWACPulseSensor;
   sensorRadius = AWACPulseSensor.detectRadius;
   sensorColor = "255 194 9";

   checkRadius = 5.5;
   observeParameters = "1 10 10";

   runningLight[0] = ShrikeLight1;
//   runningLight[1] = ShrikeLight2;

   shieldEffectScale = "0.937 1.125 0.60";
};

//Apache
datablock FlyingVehicleData(ApacheHelicopter) : ShrikeDamageProfile {
   spawnOffset = "0 0 2";
   canControl = false;
   catagory = "Vehicles";
   shapeFile = "vehicle_air_scout.dts";
   computeCRC = true;

   debrisShapeName = "vehicle_air_scout_debris.dts";
   debris = ShapeDebris;
   renderWhenDestroyed = false;

   mountPose[0] = sitting;
   mountPose[1] = sitting;
   numMountPoints = 2;
   isProtectedMountPoint[0] = true;
   isProtectedMountPoint[1] = true;

   drag    = 0.15;
   density = 1.0;

   canWarp = 1;

   cameraMaxDist = 15;
   cameraOffset = 2.5;
   cameraLag = 0.9;
   explosion = VehicleExplosion;
	explosionDamage = 0.5;
	explosionRadius = 5.0;

   maxDamage = 7.70;
   destroyedLevel = 7.70;

   isShielded = false;
   energyPerDamagePoint = 0;
   maxEnergy = 150;      // Afterburner and any energy weapon pool
   rechargeRate = 1.0;

   minDrag = 10;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
   rotationalDrag = 700;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

   maxAutoSpeed = 15;       // Autostabilizer kicks in when less than this speed. (meters/second)
   autoAngularForce = 400;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
   autoLinearForce = 300;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
   autoInputDamping = 0.95;      // Dampen control input so you don't` whack out at very slow speeds

   // Maneuvering
   maxSteeringAngle = 7;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
   horizontalSurfaceForce = 4;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
   verticalSurfaceForce = 2;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
   maneuveringForce = 10;      // Horizontal jets (W,S,D,A key thrust)
   steeringForce = 620;         // Steering jets (force applied when you move the mouse)
   steeringRollForce = 35;     // Steering jets (how much you heel over when you turn)
   rollForce = 20;                //  was 20 Auto-roll (self-correction to right you after you roll/invert)
   hoverHeight = 1;        // Height off the ground at rest
   createHoverHeight = 3;  // Height off the ground when created
   maxForwardSpeed = 50;  // speed in which forward thrust force is no longer applied (meters/second)

   // Turbo Jet
   jetForce = 1250;      // Afterburner thrust (this is in addition to normal thrust)
   minJetEnergy = 1;     // Afterburner can't be used if below this threshhold.
   jetEnergyDrain = 0.1;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
   vertThrustMultiple = 3.5;

   // Rigid body
   mass = 125;        // Mass of the vehicle
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
   max[MiniChaingunAmmo] = 1000;

   minMountDist = 4;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_scout_grey";
   targetNameTag = 'Apache';
   targetTypeTag = 'Helicopter';
   sensorData = AWACPulseSensor;
   sensorRadius = AWACPulseSensor.detectRadius;
   sensorColor = "255 194 9";

   checkRadius = 5.5;
   observeParameters = "1 10 10";

   runningLight[0] = ShrikeLight1;
//   runningLight[1] = ShrikeLight2;

   shieldEffectScale = "0.937 1.125 0.60";
};
//

datablock TurretData(GunshipTurret) : TurretDamageProfile
{
   className               = VehicleTurret;
   catagory                = "Turrets";
   shapeFile               = "turret_belly_base.dts";
   preload                 = true;
   canControl              = false;
   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingBomberIcon;
   cmdMiniIconName = "commander/MiniIcons/com_bomber_grey";
   targetNameTag = 'Helicopter AT';
   targetTypeTag = 'Turret';

   mass                    = 1.0;  // Not really relevant
   repairRate              = 0;
   maxDamage               = CombatHelicopter.maxDamage;
   destroyedLevel          = CombatHelicopter.destroyedLevel;

   thetaMin                = 90;
   thetaMax                = 180;

   inheritEnergyFromMount  = true;
   firstPersonOnly         = true;
   useEyePoint             = true;
   numWeapons              = 1;
};

datablock TurretImageData(GunshipCGImage)
{
   shapeFile = "turret_tank_barrelchain.dts";
   offset = "-0.3 0 0";
   mountPoint = 0;

   item      = MiniChaingun;
   ammo      = MiniChaingunAmmo;
   projectile = GunshipCGBullet;
   projectileType = TracerProjectile;
   projectileSpread = 0.4 / 1000.0;
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
   attackRadius         = 400;

   stateName[0] = "Activate";
   stateSequence[0] = "Activate";
//   stateSound[0] = GravChaingunIdleSound;
   stateAllowImageChange[0] = false;
   stateTimeoutValue[0] = 0.1;
   stateTransitionOnTimeout[0] = "Ready";
   stateTransitionOnNoAmmo[0] = "NoAmmo";

   stateName[1] = "Ready";
   stateSpinThread[1] = Stop;
   stateTransitionOnTriggerDown[1] = "Spinup";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "NoAmmo";
   stateTransitionOnAmmo[2] = "Ready";
   stateSpinThread[2] = Stop;
   stateTransitionOnTriggerDown[2] = "DryFire";

   stateName[3] = "Spinup";
   stateSpinThread[3] = SpinUp;
   //stateSound[3] = ChaingunSpinupSound;
   stateTimeoutValue[3] = 0.2;
   stateWaitForTimeout[3] = false;
   stateTransitionOnTimeout[3] = "Fire";
   stateTransitionOnTriggerUp[3] = "Spindown";

   stateName[4] = "Fire";
   stateSequence[4] = "Fire";
   stateSequenceRandomFlash[4] = true;
   stateSpinThread[4] = FullSpeed;
   stateSound[4] = ChaingunFireSound;
   //stateRecoil[4] = LightRecoil;
   stateAllowImageChange[4] = false;
   stateScript[4] = "onFire";
   stateFire[4] = true;
   stateEjectShell[4] = true;
   stateTimeoutValue[4] = 0.07;
   stateTransitionOnTimeout[4] = "Fire";
   stateTransitionOnTriggerUp[4] = "Spindown";
   stateTransitionOnNoAmmo[4] = "EmptySpindown";

   stateName[5] = "Spindown";
   //stateSound[5] = ChaingunSpinDownSound;
   stateSpinThread[5] = SpinDown;
   stateTimeoutValue[5] = 0.05;
   stateWaitForTimeout[5] = true;
   stateTransitionOnTimeout[5] = "Ready";
   stateTransitionOnTriggerDown[5] = "Spinup";

   stateName[6] = "EmptySpindown";
   //stateSound[6] = ChaingunSpinDownSound;
   stateSpinThread[6] = SpinDown;
   stateTimeoutValue[6] = 0.5;
   stateTransitionOnTimeout[6] = "NoAmmo";

   stateName[7] = "DryFire";
   stateSound[7] = ShrikeBlasterDryFireSound;
   stateTimeoutValue[7] = 0.5;
   stateTransitionOnTimeout[7] = "NoAmmo";
};

//APACHE
datablock TurretImageData(ApacheCGImage) {
   shapeFile = "weapon_chaingun.dts";
   offset = "-0.3 0 0";
   mountPoint = 0;

   item      = MiniChaingun;
   ammo      = MiniChaingunAmmo;
   projectile = ApacheCGBullet;
   projectileType = TracerProjectile;
   projectileSpread = 0.4 / 1000.0;
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
   attackRadius         = 400;

   stateName[0]             = "Activate";
   stateSequence[0]         = "Activate";
   stateSound[0]            = ChaingunSwitchSound;
   stateAllowImageChange[0] = false;
   //
   stateTimeoutValue[0]        = 0.5;
   stateTransitionOnTimeout[0] = "Ready";
   stateTransitionOnNoAmmo[0]  = "NoAmmo";

   //--------------------------------------
   stateName[1]       = "Ready";
   stateSpinThread[1] = Stop;
   //
   stateTransitionOnTriggerDown[1] = "Spinup";
   stateTransitionOnNoAmmo[1]      = "NoAmmo";

   //--------------------------------------
   stateName[2]               = "NoAmmo";
   stateTransitionOnAmmo[2]   = "Ready";
   stateSpinThread[2]         = Stop;
   stateTransitionOnTriggerDown[2] = "DryFire";

   //--------------------------------------
   stateName[3]         = "Spinup";
   stateSpinThread[3]   = SpinUp;
   stateSound[3]        = ChaingunSpinupSound;
   //
   stateTimeoutValue[3]          = 1.5;
   stateWaitForTimeout[3]        = false;
   stateTransitionOnTimeout[3]   = "Fire";
   stateTransitionOnTriggerUp[3] = "Spindown";

   //--------------------------------------
   stateName[4]             = "Fire";
   stateSequence[4]            = "Fire";
   stateSequenceRandomFlash[4] = true;
   stateSpinThread[4]       = FullSpeed;
   stateSound[4]            = ChaingunFireSound;
   //stateRecoil[4]           = LightRecoil;
   stateAllowImageChange[4] = false;
   stateScript[4]           = "onFire";
   stateFire[4]             = true;
   stateEjectShell[4]       = true;
   //
   stateTimeoutValue[4]          = 0.01;
   stateTransitionOnTimeout[4]   = "Fire";
   stateTransitionOnTriggerUp[4] = "Spindown";
   stateTransitionOnNoAmmo[4]    = "EmptySpindown";

   //--------------------------------------
   stateName[5]       = "Spindown";
   stateSound[5]      = ChaingunSpinDownSound;
   stateSpinThread[5] = SpinDown;
   //
   stateTimeoutValue[5]            = 1.0;
   stateWaitForTimeout[5]          = true;
   stateTransitionOnTimeout[5]     = "Ready";
   stateTransitionOnTriggerDown[5] = "Spinup";

   //--------------------------------------
   stateName[6]       = "EmptySpindown";
   stateSound[6]      = ChaingunSpinDownSound;
   stateSpinThread[6] = SpinDown;
   //
   stateTimeoutValue[6]        = 0.5;
   stateTransitionOnTimeout[6] = "NoAmmo";

   //--------------------------------------
   stateName[7]       = "DryFire";
   stateSound[7]      = ChaingunDryFireSound;
   stateTimeoutValue[7]        = 0.5;
   stateTransitionOnTimeout[7] = "NoAmmo";
};



















//CREATION
function CombatHelicopter::deleteAllMounted(%data, %obj) {
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

function CombatHelicopter::onAdd(%this, %obj) {
   Parent::onAdd(%this, %obj);

   setTargetSensorGroup(%obj.getTarget(),%obj.team);
   %turret = TurretData::create(GunshipTurret);
   %turret.selectedWeapon = 1;
   MissionCleanup.add(%turret);
   %turret.setSelfPowered();
   %obj.mountObject(%turret, 10);
   %turret.setCapacitorRechargeRate(999);
   %turret.mountobj = %obj;
   %obj.turretObject = %turret;
   %turret.team = %obj.team;
   %turret.base = %obj;
   %turret.mountImage(GunshipCGImage,0);
   setTargetSensorGroup(%turret.getTarget(),%obj.team);
   %turret.setAutoFire(true); //YES
   %turret.setInventory(MiniChaingunAmmo, 9999, true);
}
//
function ApacheHelicopter::deleteAllMounted(%data, %obj) {
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

function ApacheHelicopter::onAdd(%this, %obj) {
   Parent::onAdd(%this, %obj);

   setTargetSensorGroup(%obj.getTarget(),%obj.team);
   %turret = TurretData::create(GunshipTurret);
   %turret.selectedWeapon = 1;
   MissionCleanup.add(%turret);
   %turret.setSelfPowered();
   %obj.mountObject(%turret, 10);
   %turret.setCapacitorRechargeRate(999);
   %turret.mountobj = %obj;
   %obj.canFireMissiles = 1;
   %obj.turretObject = %turret;
   %turret.team = %obj.team;
   %turret.base = %obj;
   %turret.mountImage(ApacheCGImage,0);
   setTargetSensorGroup(%turret.getTarget(),%obj.team);
   %turret.setAutoFire(true); //YES
   %turret.setInventory(MiniChaingunAmmo, 9999, true);
}

//
function GunshipHelicopter::deleteAllMounted(%data, %obj) {
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
   //
}

function GunshipHelicopter::onAdd(%this, %obj) {
   Parent::onAdd(%this, %obj);

   setTargetSensorGroup(%obj.getTarget(),%obj.team);
   %turret = TurretData::create(GunshipTurret);
   %turret.selectedWeapon = 1;
   MissionCleanup.add(%turret);
   %turret.setSelfPowered();
   %obj.mountObject(%turret, 10);
   %obj.canFireMissiles = 1;
   %turret.setCapacitorRechargeRate(999);
   %turret.mountobj = %obj;
   %obj.turretObject = %turret;
   %turret.team = %obj.team;
   %turret.base = %obj;
   %turret.mountImage(ApacheCGImage,0);
   setTargetSensorGroup(%turret.getTarget(),%obj.team);
   %turret.setAutoFire(true); //YES
   %turret.setInventory(MiniChaingunAmmo, 9999, true);
}


function GunshipTurret::onDamage(%data, %obj) {
   %newDamageVal = %obj.getDamageLevel();
   if(%obj.lastDamageVal !$= "")
      if(isObject(%obj.getObjectMount()) && %obj.lastDamageVal > %newDamageVal)
         %obj.getObjectMount().setDamageLevel(%newDamageVal);
   %obj.lastDamageVal = %newDamageVal;
}

function ApacheCGImage::onFire(%data, %obj, %slot) {
   %p = Parent::onFire(%data, %obj, %slot);
   if(isObject(%obj.GetControllingClient().player) && %obj.GetControllingClient().player.getState() !$= "dead") {
      %p.sourceObject = %obj.GetControllingClient().player;
      //Now I get ze killz... muhaha
   }
}

//new code
function CombatHelicopter::playerMounted(%data, %obj, %player, %node) {
   switch(%node) {
      case 0:
         bottomPrint(%player.client, "PILOT: Fly around!", 2, 2);
      case 1:
         bottomPrint(%player.client, "GUNNER: 15mm Chainned Cannon", 2, 2);
         %player.client.setControlObject(%obj.turretObject);
   }
}

function GunshipHelicopter::playerMounted(%data, %obj, %player, %node) {
   switch(%node) {
      case 0:
         bottomPrint(%player.client, "PILOT: Fly around \n [MINE] Launch Missile Pod", 2, 2);
      case 1:
         bottomPrint(%player.client, "GUNNER: 45 Caliber Cannon", 2, 2);
         %player.client.setControlObject(%obj.turretObject);
   }
}

function ApacheHelicopter::playerMounted(%data, %obj, %player, %node) {
   switch(%node) {
      case 0:
         bottomPrint(%player.client, "PILOT: Fly around \n [MINE] Pop Flares", 2, 2);
      case 1:
         bottomPrint(%player.client, "GUNNER: 50 Caliber Chaingun", 2, 2);
         %player.client.setControlObject(%obj.turretObject);
   }
}

//
function GunshipMissilePod(%heli) {
   if(!isobject(%heli)) {
      return;
   }
   %p = new SeekerProjectile() { //TWM2 Sniper zombies use M1 Snipers :P
      dataBlock        = HornetStrikeMissile;
	  initialDirection = %heli.getForwardVector();
      initialPosition  = %heli.turretObject.getMuzzlePoint(0);
	  sourceObject     = %heli;
   };
   ServerPlay3d(EscapePodLaunchSound, %heli.getPosition());
   ServerPlay3d(EscapePodLaunchSound2, %heli.getPosition());
}

function GunshipHelicopter::onTrigger(%data, %obj, %trigger, %state) {
   %plyr = %obj.getMountNodeObject(0);
   if(%state == 1 && %trigger == 5) {
      if(%obj.canFireMissiles) {
         GunshipMissilePod(%obj);
         schedule(750, 0, "GunshipMissilePod", %obj);
         schedule(1500, 0, "GunshipMissilePod", %obj);
         %obj.canFireMissiles = 0;
         schedule(15000, 0, "ResetHeliMissiles", %obj);
      }
      else {
         bottomPrint(%plyr.client, "Missile Pod is Reloading", 2, 2);
      }
   }
}

function ApacheHelicopter::onTrigger(%data, %obj, %trigger, %state) {
   %plyr = %obj.getMountNodeObject(0);
   if(%state == 1 && %trigger == 5) {
      if(%obj.canFireMissiles) {
         schedule(700,0,"FireFlares",%obj);
         schedule(1400,0,"FireFlares",%obj);
         schedule(2100,0,"FireFlares",%obj);
         schedule(2800,0,"FireFlares",%obj);
         %obj.canFireMissiles = 0;
         schedule(10000, 0, "ResetHeliMissiles", %obj);
      }
      else {
         bottomPrint(%plyr.client, "Flare Pod is Reloading", 2, 2);
      }
   }
}





























//-------------------------------------------------------
//HIND: TWM2 3.7
datablock FlyingVehicleData(Hind) : ShrikeDamageProfile {
   spawnOffset = "0 0 2";
   canControl = false;
   catagory = "Vehicles";
   shapeFile = "vehicle_air_scout.dts";
   multipassenger = false;
   computeCRC = true;

   debrisShapeName = "vehicle_air_scout.dts";
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
   explosion = VehicleExplosion;
	explosionDamage = 1.0;
	explosionRadius = 10.0;

   maxDamage = 2.0;
   destroyedLevel = 2.0;

   isShielded = false;
   energyPerDamagePoint = 0;
   maxEnergy = 150;      // Afterburner and any energy weapon pool
   rechargeRate = 1.0;

   minDrag = 10;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
   rotationalDrag = 700;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

   maxAutoSpeed = 15;       // Autostabilizer kicks in when less than this speed. (meters/second)
   autoAngularForce = 400;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
   autoLinearForce = 300;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
   autoInputDamping = 0.95;      // Dampen control input so you don't` whack out at very slow speeds

   // Maneuvering
   maxSteeringAngle = 7;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
   horizontalSurfaceForce = 4;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
   verticalSurfaceForce = 2;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
   maneuveringForce = 10;      // Horizontal jets (W,S,D,A key thrust)
   steeringForce = 620;         // Steering jets (force applied when you move the mouse)
   steeringRollForce = 35;     // Steering jets (how much you heel over when you turn)
   rollForce = 20;                //  was 20 Auto-roll (self-correction to right you after you roll/invert)
   hoverHeight = 1;        // Height off the ground at rest
   createHoverHeight = 3;  // Height off the ground when created
   maxForwardSpeed = 50;  // speed in which forward thrust force is no longer applied (meters/second)

   // Turbo Jet
   jetForce = 1250;      // Afterburner thrust (this is in addition to normal thrust)
   minJetEnergy = 1;     // Afterburner can't be used if below this threshhold.
   jetEnergyDrain = 0.1;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
   vertThrustMultiple = 3.5;

   // Rigid body
   mass = 125;        // Mass of the vehicle
   bodyFriction = 0;     // Don't mess with this.
   bodyRestitution = 0.5;   // When you hit the ground, how much you rebound. (between 0 and 1)
   minRollSpeed = 0;     // Don't mess with this.
   softImpactSpeed = 14;       // Sound hooks. This is the soft hit.
   hardImpactSpeed = 25;    // Sound hooks. This is the hard hit.

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 10;      // If hit ground at speed above this then it's an impact. Meters/second
   speedDamageScale = 0.07;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 23.0;
   collDamageMultiplier   = 0.02;

   //
   minTrailSpeed = 45;      // The speed your contrail shows up at.
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
   damageLevelTolerance[0] = 0.4;
   damageLevelTolerance[1] = 0.8;
   numDmgEmitterAreas = 1;

   minMountDist = 4;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_scout_grey";
   targetNameTag = 'Hind';
   targetTypeTag = 'Gunship';
   sensorData = AWACPulseSensor;
   sensorRadius = AWACPulseSensor.detectRadius;
   sensorColor = "9 9 255";

   checkRadius = 5.5;
   observeParameters = "1 10 10";

   runningLight[0] = ShrikeLight1;
//   runningLight[1] = ShrikeLight2;

   shieldEffectScale = "0.937 1.125 0.60";

   max[MiniChaingunAmmo] = 1250;
   max[MissileLauncherAmmo] = 24;

   numWeapons = 2;
};

datablock TurretImageData(HindCGImage) {
   shapeFile = "weapon_chaingun.dts";
   offset = "0 0.2 0";
   mountPoint = 0;

   item      = MiniChaingun;
   ammo      = MiniChaingunAmmo;
   projectile = ApacheCGBullet;
   projectileType = TracerProjectile;
   projectileSpread = 0.4 / 1000.0;
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
   attackRadius         = 400;

   stateName[0]             = "Activate";
   stateSequence[0]         = "Activate";
   stateSound[0]            = ChaingunSwitchSound;
   stateAllowImageChange[0] = false;
   //
   stateTimeoutValue[0]        = 0.5;
   stateTransitionOnTimeout[0] = "Ready";
   stateTransitionOnNoAmmo[0]  = "NoAmmo";

   //--------------------------------------
   stateName[1]       = "Ready";
   stateSpinThread[1] = Stop;
   //
   stateTransitionOnTriggerDown[1] = "Spinup";
   stateTransitionOnNoAmmo[1]      = "NoAmmo";

   //--------------------------------------
   stateName[2]               = "NoAmmo";
   stateTransitionOnAmmo[2]   = "Ready";
   stateSpinThread[2]         = Stop;
   stateTransitionOnTriggerDown[2] = "DryFire";

   //--------------------------------------
   stateName[3]         = "Spinup";
   stateSpinThread[3]   = SpinUp;
   stateSound[3]        = ChaingunSpinupSound;
   //
   stateTimeoutValue[3]          = 1.5;
   stateWaitForTimeout[3]        = false;
   stateTransitionOnTimeout[3]   = "Fire";
   stateTransitionOnTriggerUp[3] = "Spindown";

   //--------------------------------------
   stateName[4]             = "Fire";
   stateSequence[4]            = "Fire";
   stateSequenceRandomFlash[4] = true;
   stateSpinThread[4]       = FullSpeed;
   stateSound[4]            = ChaingunFireSound;
   //stateRecoil[4]           = LightRecoil;
   stateAllowImageChange[4] = false;
   stateScript[4]           = "onFire";
   stateFire[4]             = true;
   stateEjectShell[4]       = true;
   //
   stateTimeoutValue[4]          = 0.01;
   stateTransitionOnTimeout[4]   = "Fire";
   stateTransitionOnTriggerUp[4] = "Spindown";
   stateTransitionOnNoAmmo[4]    = "EmptySpindown";

   //--------------------------------------
   stateName[5]       = "Spindown";
   stateSound[5]      = ChaingunSpinDownSound;
   stateSpinThread[5] = SpinDown;
   //
   stateTimeoutValue[5]            = 1.0;
   stateWaitForTimeout[5]          = true;
   stateTransitionOnTimeout[5]     = "Ready";
   stateTransitionOnTriggerDown[5] = "Spinup";

   //--------------------------------------
   stateName[6]       = "EmptySpindown";
   stateSound[6]      = ChaingunSpinDownSound;
   stateSpinThread[6] = SpinDown;
   //
   stateTimeoutValue[6]        = 0.5;
   stateTransitionOnTimeout[6] = "NoAmmo";

   //--------------------------------------
   stateName[7]       = "DryFire";
   stateSound[7]      = ChaingunDryFireSound;
   stateTimeoutValue[7]        = 0.5;
   stateTransitionOnTimeout[7] = "NoAmmo";
};

datablock ShapeBaseImageData(HindMissilePairImage) {
   className = WeaponImage;
   shapeFile = "turret_missile_large.dts";
   item      = MissileLauncher;
   ammo   = MissileLauncherAmmo;
   projectile = HornetStrikeMissile;
   projectileType = SeekerProjectile;
   mountPoint = 10;
   offset = "1.0 1 0.5";

   projectileSpread = 1.0 / 1000.0;

   usesEnergy = false;
   useMountEnergy = true;
   minEnergy = 1;
   fireEnergy = 1;
   fireTimeout = 125;
   emap = true;


   //--------------------------------------
   stateName[0]             = "Activate";
   stateSequence[0]         = "Activate";
   stateAllowImageChange[0] = false;
   stateTimeoutValue[0]        = 0.5;
   stateTransitionOnTimeout[0] = "Ready";
   stateTransitionOnNoAmmo[0]  = "NoAmmo";
   //--------------------------------------
   stateName[1]       = "Ready";
   stateSpinThread[1] = Stop;
   stateTransitionOnTriggerDown[1] = "Spinup";
   stateTransitionOnNoAmmo[1]      = "NoAmmo";
   //--------------------------------------
   stateName[2]               = "NoAmmo";
   stateTransitionOnAmmo[2]   = "AmmoLoading";
   stateSpinThread[2]         = Stop;
   stateTransitionOnTriggerDown[2] = "DryFire";
   //--------------------------------------
   stateName[3]         = "Spinup";
   stateSpinThread[3]   = SpinUp;
   stateTimeoutValue[3]          = 0.01;
   stateWaitForTimeout[3]        = false;
   stateTransitionOnTimeout[3]   = "Fire";
   stateTransitionOnTriggerUp[3] = "Spindown";
   //--------------------------------------
   stateName[4]             = "Fire";
   stateSpinThread[4]       = FullSpeed;
   stateRecoil[4]           = LightRecoil;
   stateAllowImageChange[4] = false;
   stateScript[4]           = "onFire";
   stateFire[4]             = true;
   stateSound[4]            = ShrikeBlasterFire;
   stateTimeoutValue[4]          = 0.15;
   stateTransitionOnTimeout[4]   = "checkState";
   //--------------------------------------
   stateName[5]       = "Spindown";
   stateSpinThread[5] = SpinDown;
   stateTimeoutValue[5]            = 0.01;
   stateWaitForTimeout[5]          = false;
   stateTransitionOnTimeout[5]     = "Ready";
   stateTransitionOnTriggerDown[5] = "Spinup";
   //--------------------------------------
   stateName[6]       = "EmptySpindown";
//   stateSound[6]      = ChaingunSpindownSound;
   stateSpinThread[6] = SpinDown;
   stateTransitionOnAmmo[6]   = "Ready";
   stateTimeoutValue[6]        = 0.01;
   stateTransitionOnTimeout[6] = "NoAmmo";
   //--------------------------------------
   stateName[7]       = "DryFire";
   stateSound[7]      = ShrikeBlasterDryFireSound;
   stateTransitionOnTriggerUp[7] = "NoAmmo";
   stateTimeoutValue[7]        = 1.0;
   stateTransitionOnTimeout[7] = "NoAmmo";

   stateName[8] = "checkState";
   stateTransitionOnTriggerUp[8] = "Spindown";
   stateTransitionOnNoAmmo[8]    = "EmptySpindown";
   stateTimeoutValue[8]          = 0.01;
   stateTransitionOnTimeout[8]   = "ready";

   stateName[9]                     = "AmmoLoading";
   stateSound[9] 			 	= MissileReloadSound;
   stateTimeoutValue[9]             = 0.1;
   stateAllowImageChange[9]         = false;
   stateTransitionOnTimeout[9]      = "Ready";
};

datablock ShapeBaseImageData(HindMissileImage) : HindMissilePairImage {
//**original**   offset = "-.73 0 0";
   offset = "-1.0 1 0.5";
   stateScript[3]           = "onTriggerDown";
   stateTimeoutValue[3]          = 0.08;
   stateScript[5]           = "onTriggerUp";
   stateScript[6]           = "onTriggerUp";
};

datablock ShapeBaseImageData(HATDummy) {
   shapeFile = "stackable2m.dts";
   mountPoint = 10;
   offset = "1.1 -5 2.5";
   rotation = "1 0 0 90";
   mass = 20;
   emap = true;
};

datablock ShapeBaseImageData(HATDummy2) {
   shapeFile = "stackable2m.dts";
   mountPoint = 10;
   offset = "-1.1 -5 2.5";
   rotation = "1 0 0 90";
   mass = 20;
   emap = true;
};

function Hind::onAdd(%this, %obj) {
   Parent::onAdd(%this, %obj);
   if (%obj.clientControl)
       serverCmdResetControlObject(%obj.clientControl);

   %obj.mountImage(HindMissileImage, 2);
   %obj.mountImage(HindMissilePairImage, 3);
   %obj.mountImage(HindCGImage, 4);
   %obj.mountImage(HATDummy, 5);
   %obj.mountImage(HATDummy2, 6);
   %obj.selectedWeapon = 1;
   %obj.nextWeaponFire = 2;
   %obj.setInventory(minichaingunammo, 1250);
   %obj.setInventory(MissileLauncherAmmo, 24);
   %obj.schedule(5500, "playThread", $ActivateThread, "activate");
}

function Hind::playerMounted(%data, %obj, %player, %node) {
   $numVWeapons = 2;

   if (%obj.clientControl)
       serverCmdResetControlObject(%obj.clientControl);

   %player.setPilot(true);
   commandToClient(%player.client, 'setHudMode', 'Pilot', "hind", %node);
   commandToClient(%player.client, 'SetWeaponryVehicleKeys', true);
   %obj.selectedWeapon = 1;

   // update observers who are following this guy...
   if ( %player.client.observeCount > 0 )
      resetObserveFollow( %player.client, false );
}

function Hind::onTrigger(%data, %obj, %trigger, %state) {
   if(%trigger == 0) {
      switch (%state) {
         case 0:
            %obj.fireWeapon = false;
            %obj.setImageTrigger(2, false);
            %obj.setImageTrigger(3, false);
            %obj.setImageTrigger(4, false);
         case 1:
            %obj.fireWeapon = true;
	        if(%obj.selectedWeapon == 1) {
			   if(%obj.nextWeaponFire == 2){
                  %obj.setImageTrigger(2, true);
               	  %obj.setImageTrigger(3, true);
     	       }
		    }
            else {
               %obj.setImageTrigger(2, false);
               %obj.setImageTrigger(3, false);
               %obj.setImageTrigger(4, true);
            }
      }
   }
}
