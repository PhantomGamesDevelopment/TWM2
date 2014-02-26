//**************************************************************
// BEOWULF ASSAULT VEHICLE
//**************************************************************
//**************************************************************
// SOUNDS
//**************************************************************
datablock AudioProfile(AssaultVehicleSkid)
{
   filename    = "fx/vehicles/tank_skid.wav";
   description = ClosestLooping3d;
   preload = true;
};

datablock AudioProfile(AssaultVehicleEngineSound)
{
   filename    = "fx/vehicles/tank_engine.wav";
   description = AudioDefaultLooping3d;
   preload = true;
};

datablock AudioProfile(AssaultVehicleThrustSound)
{
   filename    = "fx/vehicles/tank_boost.wav";
   description = AudioDefaultLooping3d;
   preload = true;
};

datablock AudioProfile(AssaultChaingunFireSound)
{
   filename    = "fx/vehicles/tank_chaingun.wav";
   description = AudioDefaultLooping3d;
   preload = true;
};

datablock AudioProfile(AssaultChaingunReloadSound)
{
   filename    = "fx/weapons/chaingun_dryfire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(TankChaingunProjectile)
{
   filename    = "fx/weapons/chaingun_projectile.wav";
   description = ProjectileLooping3d;
   preload = true;
};

datablock AudioProfile(AssaultTurretActivateSound)
{
    filename    = "fx/vehicles/tank_activate.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(AssaultChaingunDryFireSound)
{
   filename    = "fx/weapons/chaingun_dryfire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(AssaultChaingunIdleSound)
{
   filename    = "fx/misc/diagnostic_on.wav";
   description = ClosestLooping3d;
   preload = true;
};

datablock AudioProfile(AssaultMortarDryFireSound)
{
   filename    = "fx/weapons/mortar_dryfire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(AssaultMortarFireSound)
{
   filename    = "fx/vehicles/tank_mortar_fire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(AssaultMortarReloadSound)
{
   filename    = "fx/weapons/mortar_reload.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(AssaultMortarIdleSound)
{
   filename    = "fx/misc/diagnostic_on.wav";
   description = ClosestLooping3d;
   preload = true;
};

//**************************************************************
// LIGHTS
//**************************************************************
datablock RunningLightData(TankLight1)
{
   radius = 1.5;
   color = "1.0 1.0 1.0 0.2";
   nodeName = "Headlight_node01";
   direction = "0.0 1.0 0.0";
   texture = "special/headlight4";
};

datablock RunningLightData(TankLight2)
{
   radius = 1.5;
   color = "1.0 1.0 1.0 0.2";
   nodeName = "Headlight_node02";
   direction = "0.0 1.0 0.0";
   texture = "special/headlight4";
};

datablock RunningLightData(TankLight3)
{
   radius = 1.5;
   color = "1.0 1.0 1.0 0.2";
   nodeName = "Headlight_node03";
   direction = "0.0 1.0 0.0";
   texture = "special/headlight4";
};

datablock RunningLightData(TankLight4)
{
   radius = 1.5;
   color = "1.0 1.0 1.0 0.2";
   nodeName = "Headlight_node04";
   direction = "0.0 1.0 0.0";
   texture = "special/headlight4";
};

//**************************************************************
// VEHICLE CHARACTERISTICS
//**************************************************************

datablock HoverVehicleData(AssaultVehicle) : TankDamageProfile
{
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
   mountPose[1] = sitting;
   numMountPoints = 2;
   isProtectedMountPoint[0] = true;
   isProtectedMountPoint[1] = true;

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
   targetNameTag = 'Beowulf';
   targetTypeTag = 'Assault Tank';
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

//**************************************************************
// WEAPONS
//**************************************************************

//-------------------------------------
// ASSAULT CHAINGUN (projectile)
//-------------------------------------

datablock TracerProjectileData(AssaultChaingunBullet)
{
   doDynamicClientHits = true;

   projectileShapeName = "";
   directDamage        = 0.16;
   directDamageType    = $DamageType::TankChaingun;
   hasDamageRadius     = false;
   splash			   = ChaingunSplash;

   kickbackstrength    = 0.0;
   sound          	   = TankChaingunProjectile;

   dryVelocity       = 425.0;
   wetVelocity       = 100.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 3000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 15.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.10;
   crossSize       = 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;

   decalData[0] = ChaingunDecal1;
   decalData[1] = ChaingunDecal2;
   decalData[2] = ChaingunDecal3;
   decalData[3] = ChaingunDecal4;
   decalData[4] = ChaingunDecal5;
   decalData[5] = ChaingunDecal6;

   activateDelayMS   = 100;

   explosion = ChaingunExplosion;
};

//-------------------------------------
// ASSAULT CHAINGUN CHARACTERISTICS
//-------------------------------------

datablock TurretData(AssaultPlasmaTurret) : TurretDamageProfile
{
   className      = VehicleTurret;
   catagory       = "Turrets";
   shapeFile      = "Turret_tank_base.dts";
   preload        = true;
   canControl = true;
   cmdCategory = "Tactical";
   cmdIcon = CMDGroundTankIcon;
   cmdMiniIconName = "commander/MiniIcons/com_tank_grey";
   targetNameTag = 'Beowulf';
   targetTypeTag = 'Assault Tank turret';
   mass           = 1.0;  // Not really relevant

   maxEnergy               = 1;
   maxDamage               = AssaultVehicle.maxDamage;
   destroyedLevel          = AssaultVehicle.destroyedLevel;
   repairRate              = 0;

   // capacitor
   maxCapacitorEnergy      = 250;
   capacitorRechargeRate   = 1.0;

   thetaMin = 0;
   thetaMax = 100;

   inheritEnergyFromMount = true;
   firstPersonOnly = true;
   useEyePoint = true;
   numWeapons = 2;

   cameraDefaultFov = 90.0;
   cameraMinFov = 5.0;
   cameraMaxFov = 120.0;

   targetNameTag = 'Beowulf Chaingun';
   targetTypeTag = 'Turret';
};

datablock TurretImageData(AssaultPlasmaTurretBarrel)
{
   shapeFile = "turret_tank_barrelchain.dts";
   mountPoint = 1;

   projectile = AssaultChaingunBullet;
   projectileType = TracerProjectile;

   casing              = ShellDebris;
   shellExitDir        = "1.0 0.3 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 15.0;
   shellVelocity       = 3.0;

   projectileSpread = 12.0 / 1000.0;

   useCapacitor = true;
   usesEnergy = true;
   useMountEnergy = true;
   fireEnergy = 7.5;
   minEnergy = 15.0;

   // Turret parameters
   activationMS      = 4000;
   deactivateDelayMS = 500;
   thinkTimeMS       = 200;
   degPerSecTheta    = 360;
   degPerSecPhi      = 360;
   attackRadius      = 75;

   // State transitions
   stateName[0]                        = "Activate";
   stateTransitionOnNotLoaded[0]       = "Dead";
   stateTransitionOnLoaded[0]          = "ActivateReady";
   stateSound[0]                       = AssaultTurretActivateSound;

   stateName[1]                        = "ActivateReady";
   stateSequence[1]                    = "Activate";
   stateSound[1]                       = AssaultTurretActivateSound;
   stateTimeoutValue[1]                = 1;
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
   stateAllowImageChange[3]            = false;
   stateSound[3]                       = AssaultChaingunFireSound;
   stateScript[3]                      = "onFire";
   stateTimeoutValue[3]                = 0.1;
   stateTransitionOnTimeout[3]         = "Fire";
   stateTransitionOnTriggerUp[3]       = "Reload";
   stateTransitionOnNoAmmo[3]          = "noAmmo";

   stateName[4]                        = "Reload";
   stateSequence[4]                    = "Reload";
   stateTimeoutValue[4]                = 0.1;
   stateAllowImageChange[4]            = false;
   stateTransitionOnTimeout[4]         = "Ready";
   stateTransitionOnNoAmmo[4]          = "NoAmmo";
   stateWaitForTimeout[4]              = true;

   stateName[5]                        = "Deactivate";
   stateSequence[5]                    = "Activate";
   stateDirection[5]                   = false;
   stateTimeoutValue[5]                = 30;
   stateTransitionOnTimeout[5]         = "ActivateReady";

   stateName[6]                        = "Dead";
   stateTransitionOnLoaded[6]          = "ActivateReady";
   stateTransitionOnTriggerDown[6]     = "DryFire";

   stateName[7]                        = "DryFire";
   stateSound[7]                       = AssaultChaingunDryFireSound;
   stateTimeoutValue[7]                = 0.5;
   stateTransitionOnTimeout[7]         = "NoAmmo";

   stateName[8]                        = "NoAmmo";
   stateTransitionOnAmmo[8]            = "Reload";
   stateSequence[8]                    = "NoAmmo";
   stateTransitionOnTriggerDown[8]     = "DryFire";

};

//-------------------------------------
// ASSAULT MORTAR (projectile)
//-------------------------------------

datablock ItemData(AssaultMortarAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "repair_kit.dts";
   mass = 1;
   elasticity = 0.5;
   friction = 0.6;
   pickupRadius = 1;

   computeCRC = true;
};

datablock TracerProjectileData(TankShell) {
   doDynamicClientHits = true;

   directDamage        = 0.0; // z0dd - ZOD, 9-27-02. Was 0.0825
   directDamageType    = $DamageType::TankMortar;
   hasDamageRadius     = true;
   indirectDamage      = 0.8;
   damageRadius        = 7.0;
   radiusDamageType    = $DamageType::TankMortar;
   explosion           = "MissileExplosion";
   splash              = ChaingunSplash;

   kickBackStrength  = 1500.0;
   sound 				= ChaingunProjectile;

   //dryVelocity       = 425.0;
   dryVelocity       = 500.0; // z0dd - ZOD, 8-12-02. Was 425.0
   wetVelocity       = 400.0;
   velInheritFactor  = 0.0;
   fizzleTimeMS      = 300;
   lifetimeMS        = 600;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 300;

   tracerLength    = 25.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 2.25;
   crossSize       = 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;

   decalData[0] = ChaingunDecal1;
   decalData[1] = ChaingunDecal2;
   decalData[2] = ChaingunDecal3;
   decalData[3] = ChaingunDecal4;
   decalData[4] = ChaingunDecal5;
   decalData[5] = ChaingunDecal6;
};

//-------------------------------------
// ASSAULT MORTAR CHARACTERISTICS
//-------------------------------------

datablock TurretImageData(AssaultMortarTurretBarrel)
{
   shapeFile = "turret_tank_barrelmortar.dts";
   mountPoint = 0;

//   ammo = AssaultMortarAmmo;
   projectile = TankShell;
   projectileType = TracerProjectile;

   usesEnergy = true;
   useMountEnergy = true;
   fireEnergy = 77.00;
   minEnergy = 77.00;
   useCapacitor = true;

   // Turret parameters
   activationMS                        = 4000;
   deactivateDelayMS                   = 1500;
   thinkTimeMS                         = 200;
   degPerSecTheta                      = 360;
   degPerSecPhi                        = 360;
   attackRadius                        = 75;

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
   stateTransitionOnTimeout[3]         = "Reload";
   stateTimeoutValue[3]                = 1.0;
   stateFire[3]                        = true;
   stateRecoil[3]                      = LightRecoil;
   stateAllowImageChange[3]            = false;
   stateSound[3]                       = AssaultMortarFireSound;
   stateScript[3]                      = "onFire";

   stateName[4]                        = "Reload";
   stateSequence[4]                    = "Reload";
   stateTimeoutValue[4]                = 1.0;
   stateAllowImageChange[4]            = false;
   stateTransitionOnTimeout[4]         = "Ready";
   //stateTransitionOnNoAmmo[4]          = "NoAmmo";
   stateWaitForTimeout[4]              = true;

   stateName[5]                        = "Deactivate";
   stateDirection[5]                   = false;
   stateSequence[5]                    = "Activate";
   stateTimeoutValue[5]                = 1.0;
   stateTransitionOnLoaded[5]          = "ActivateReady";
   stateTransitionOnTimeout[5]         = "Dead";

   stateName[6]                        = "Dead";
   stateTransitionOnLoaded[6]          = "ActivateReady";
   stateTransitionOnTriggerDown[6]     = "DryFire";

   stateName[7]                        = "DryFire";
   stateSound[7]                       = AssaultMortarDryFireSound;
   stateTimeoutValue[7]                = 1.0;
   stateTransitionOnTimeout[7]         = "NoAmmo";

   stateName[8]                        = "NoAmmo";
   stateSequence[8]                    = "NoAmmo";
   stateTransitionOnAmmo[8]            = "Reload";
   stateTransitionOnTriggerDown[8]     = "DryFire";
};

datablock TurretImageData(AssaultTurretParam)
{
   mountPoint = 2;
   shapeFile = "turret_muzzlepoint.dts";

   projectile = AssaultChaingunBullet;
   projectileType = TracerProjectile;

   useCapacitor = true;
   usesEnergy = true;

   // Turret parameters
   activationMS      = 1000;
   deactivateDelayMS = 1500;
   thinkTimeMS       = 200;
   degPerSecTheta    = 500;
   degPerSecPhi      = 500;

   attackRadius      = 75;
};



























//
// Sandstorm AA Tank
//

datablock HoverVehicleData(SandstormTank) : AssaultVehicle {
   maxDamage = 3.15;
   destroyedLevel = 3.15;

   isShielded = false;
   rechargeRate = 1.0;
   energyPerDamagePoint = 135;
   maxEnergy = 400;
   minJetEnergy = 15;
   jetEnergyDrain = 2.0;
   
   cmdCategory = "Tactical";
   cmdIcon = CMDGroundTankIcon;
   cmdMiniIconName = "commander/MiniIcons/com_tank_grey";
   targetNameTag = 'Sandstorm';
   targetTypeTag = 'AA Tank';
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

datablock TurretData(SandstormAAPod) : TurretDamageProfile {
   className      = VehicleTurret;
   catagory       = "Turrets";
   shapeFile      = "Turret_tank_base.dts";
   preload        = true;
   canControl = false;
   cmdCategory = "Tactical";
   cmdIcon = CMDGroundTankIcon;
   cmdMiniIconName = "commander/MiniIcons/com_tank_grey";
   mass           = 1.0;

   isSeeker     = true;
   seekRadius   = 750;
   maxSeekAngle = 30;
   seekTime     = 1.0;
   minSeekHeat  = 0.00001;
   emap = true;
   minTargetingDistance = 40;

   maxEnergy               = 1;
   maxDamage               = AssaultVehicle.maxDamage;
   destroyedLevel          = AssaultVehicle.destroyedLevel;
   repairRate              = 0;

   // capacitor
   maxCapacitorEnergy      = 200;
   capacitorRechargeRate   = 0.45;

   thetaMin = 0;
   thetaMax = 100;

   inheritEnergyFromMount = true;
   firstPersonOnly = true;
   useEyePoint = true;
   numWeapons = 1;

   cameraDefaultFov = 90.0;
   cameraMinFov = 5.0;
   cameraMaxFov = 120.0;

   targetNameTag = 'Sandstorm';
   targetTypeTag = 'Turret';
};

datablock SeekerProjectileData(SandstormTankRocket) {
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   hasDamageRadius     = true;
   indirectDamage      = 0.54;
   damageRadius        = 12.0;
   radiusDamageType    = $DamageType::MissileTurret;
   kickBackStrength    = 500;

   flareDistance = 200;
   flareAngle    = 30;
   minSeekHeat   = 0.6;

   explosion           = "MissileExplosion";
   velInheritFactor    = 1.0;

   splash              = MissileSplash;
   baseEmitter         = MissileSmokeEmitter;
   delayEmitter        = MissileFireEmitter;
   puffEmitter         = MissilePuffEmitter;

   lifetimeMS          = 20000; // z0dd - ZOD, 4/14/02. Was 6000
   muzzleVelocity      = 40.0;
   maxVelocity         = 300.0; // z0dd - ZOD, 4/14/02. Was 80.0
   turningSpeed        = 60.0;
   acceleration        = 100.0;

   proximityRadius     = 7;

   terrainAvoidanceSpeed = 40;
   terrainScanAhead      = 50;
   terrainHeightFail     = 5;
   terrainAvoidanceRadius = 10;

   useFlechette = true;
   flechetteDelayMs = 225;
   casingDeb = FlechetteDebris;
};

//-------------------------------------
// ASSAULT MORTAR CHARACTERISTICS
//-------------------------------------

datablock TurretImageData(SandstormTurretBarrel) {
   shapeFile = "stackable2m.dts";
   rotation = "-1 0 0 90";
   offset = "0 0.7 0";
   mountPoint = 0;

   projectile = SandstormTankRocket;
   projectileType = SeekerProjectile;

   usesEnergy = true;
   useMountEnergy = true;
   fireEnergy = 25.00;
   minEnergy = 25.00;
   useCapacitor = true;
   
   isSeeker = true;
   seekRadius = 750;
   maxSeekAngle = 360;
   seekTime = $Bomber::SeekTime;
   minSeekHeat = 0.0001;
   minTargetingDistance = $Bomber::minTargetingDistance;
   useTargetAudio = $Bomber::useTargetAudio;

   // Turret parameters
   activationMS                        = 4000;
   deactivateDelayMS                   = 1500;
   thinkTimeMS                         = 200;
   degPerSecTheta                      = 360;
   degPerSecPhi                        = 360;
   attackRadius                        = 75;

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
   stateTransitionOnTimeout[3]         = "NextFire";
   stateTimeoutValue[3]                = 0.5;
   stateFire[3]                        = true;
   stateRecoil[3]                      = LightRecoil;
   stateAllowImageChange[3]            = false;
   stateSound[3]                       = MissileFireSound;
   stateScript[3]                      = "onFire";

   stateName[4]                        = "NextFire";
   stateTransitionOnNotLoaded[4]       = "Deactivate";
   stateTransitionOnNoAmmo[4]          = "NoAmmo";
   stateTransitionOnTriggerDown[4]     = "Fire";

   stateName[5]                        = "Fire2";
   stateSequence[5]                    = "Fire";
   stateTransitionOnTimeout[5]         = "NextFire2";
   stateTimeoutValue[5]                = 0.5;
   stateFire[5]                        = true;
   stateRecoil[5]                      = LightRecoil;
   stateAllowImageChange[5]            = false;
   stateSound[5]                       = MissileFireSound;
   stateScript[5]                      = "onFire";

   stateName[6]                        = "NextFire2";
   stateTransitionOnNotLoaded[6]       = "Deactivate";
   stateTransitionOnNoAmmo[6]          = "NoAmmo";
   stateTransitionOnTriggerDown[6]     = "Fire";

   stateName[7]                        = "Fire3";
   stateSequence[7]                    = "Fire";
   stateTransitionOnTimeout[7]         = "NextFire3";
   stateTimeoutValue[7]                = 0.5;
   stateFire[7]                        = true;
   stateRecoil[7]                      = LightRecoil;
   stateAllowImageChange[7]            = false;
   stateSound[7]                       = MissileFireSound;
   stateScript[7]                      = "onFire";

   stateName[8]                        = "NextFire3";
   stateTransitionOnNotLoaded[8]       = "Deactivate";
   stateTransitionOnNoAmmo[8]          = "NoAmmo";
   stateTransitionOnTriggerDown[8]     = "Fire";

   stateName[9]                        = "Fire4";
   stateSequence[9]                    = "Fire";
   stateTransitionOnTimeout[9]         = "NextFire4";
   stateTimeoutValue[9]                = 0.5;
   stateFire[9]                        = true;
   stateRecoil[9]                      = LightRecoil;
   stateAllowImageChange[9]            = false;
   stateSound[9]                       = MissileFireSound;
   stateScript[9]                      = "onFire";
   
   stateName[10]                        = "NextFire4";
   stateTransitionOnNotLoaded[10]       = "Deactivate";
   stateTransitionOnNoAmmo[10]          = "NoAmmo";
   stateTransitionOnTriggerDown[10]     = "Fire";

   stateName[11]                        = "Fire5";
   stateSequence[11]                    = "Fire";
   stateTransitionOnTimeout[11]         = "NextFire5";
   stateTimeoutValue[11]                = 0.5;
   stateFire[11]                        = true;
   stateRecoil[11]                      = LightRecoil;
   stateAllowImageChange[11]            = false;
   stateSound[11]                       = MissileFireSound;
   stateScript[11]                      = "onFire";

   stateName[12]                        = "NextFire5";
   stateTransitionOnNotLoaded[12]       = "Deactivate";
   stateTransitionOnNoAmmo[12]          = "NoAmmo";
   stateTransitionOnTriggerDown[12]     = "Fire";
   
   stateName[13]                        = "Fire6";
   stateSequence[13]                    = "Fire";
   stateTransitionOnTimeout[13]         = "NextFire6";
   stateTimeoutValue[13]                = 0.5;
   stateFire[13]                        = true;
   stateRecoil[13]                      = LightRecoil;
   stateAllowImageChange[13]            = false;
   stateSound[13]                       = MissileFireSound;
   stateScript[13]                      = "onFire";

   stateName[14]                        = "NextFire6";
   stateTransitionOnNotLoaded[14]       = "Deactivate";
   stateTransitionOnNoAmmo[14]          = "NoAmmo";
   stateTransitionOnTriggerDown[14]     = "Fire";
   
   stateName[15]                        = "Fire7";
   stateSequence[15]                    = "Fire";
   stateTransitionOnTimeout[15]         = "NextFire7";
   stateTimeoutValue[15]                = 0.5;
   stateFire[15]                        = true;
   stateRecoil[15]                      = LightRecoil;
   stateAllowImageChange[15]            = false;
   stateSound[15]                       = MissileFireSound;
   stateScript[15]                      = "onFire";

   stateName[16]                        = "NextFire7";
   stateTransitionOnNotLoaded[16]       = "Deactivate";
   stateTransitionOnNoAmmo[16]          = "NoAmmo";
   stateTransitionOnTriggerDown[16]     = "Fire";
   
   stateName[17]                        = "Fire8";
   stateSequence[17]                    = "Fire";
   stateTransitionOnTimeout[17]         = "Reload";
   stateTimeoutValue[17]                = 0.5;
   stateFire[17]                        = true;
   stateRecoil[17]                      = LightRecoil;
   stateAllowImageChange[17]            = false;
   stateSound[17]                       = MissileFireSound;
   stateScript[17]                      = "onFire";

   stateName[18]                        = "Reload";
   stateSequence[18]                    = "Reload";
   stateTimeoutValue[18]                = 8;
   stateAllowImageChange[18]            = false;
   stateTransitionOnTimeout[18]         = "ReloadSound";
   stateWaitForTimeout[18]              = true;

   stateName[19]                        = "ReloadSound";
   stateTimeoutValue[19]                = 2;
   stateAllowImageChange[19]            = false;
   stateTransitionOnTimeout[19]         = "Ready";
   stateSound[19]                       = MobileBaseStationDeploySound;
   stateWaitForTimeout[19]              = true;

   stateName[20]                        = "Deactivate";
   stateDirection[20]                   = false;
   stateSequence[20]                    = "Activate";
   stateTimeoutValue[20]                = 1.0;
   stateTransitionOnLoaded[20]          = "ActivateReady";
   stateTransitionOnTimeout[20]         = "Dead";

   stateName[21]                        = "Dead";
   stateTransitionOnLoaded[21]          = "ActivateReady";
   stateTransitionOnTriggerDown[21]     = "DryFire";

   stateName[22]                        = "DryFire";
   stateSound[22]                       = AssaultMortarDryFireSound;
   stateTimeoutValue[22]                = 1.0;
   stateTransitionOnTimeout[22]         = "NoAmmo";

   stateName[23]                        = "NoAmmo";
   stateSequence[23]                    = "NoAmmo";
   stateTransitionOnAmmo[23]            = "Reload";
   stateTransitionOnTriggerDown[23]     = "DryFire";
};

function SandstormTurretBarrel::onFire(%data,%obj,%slot) {
   %p = Parent::onFire(%data, %obj, %slot);
   MissileSet.add(%p);

   if (%obj.getControllingClient())
      %target = %obj.getLockedTarget();
   else
      %target = %obj.getTargetObject();

   %homein = missileCheckAirTarget(%target);
   if(%target && %homein)
      %p.setObjectTarget(%target);
   else if(%obj.isLocked())
      %p.setPositionTarget(%obj.getLockedPosition());
   else
      %p.setNoTarget();
}

datablock ShapeBaseImageData(SandstormParam) {
   mountPoint = 2;
   shapeFile = "turret_muzzlepoint.dts";

   projectile = SandstormTankRocket;
   projectileType = SeekerProjectile;
   isSeeker = true;
   seekRadius = 750;
   maxSeekAngle = 360;
   seekTime = $Bomber::SeekTime;
   minSeekHeat = 0.0001;
   minTargetingDistance = $Bomber::minTargetingDistance;
   useTargetAudio = $Bomber::useTargetAudio;
};

function SandstormTank::onAdd(%this, %obj) {
   Parent::onAdd(%this, %obj);

   %turret = TurretData::create(SandstormAAPod);
   %turret.selectedWeapon = 1;
   MissionCleanup.add(%turret);
   %turret.team = %obj.teamBought;
   %turret.setSelfPowered();
   %obj.mountObject(%turret, 10);
   %turret.mountImage(SandstormTurretBarrel, 0);
   %turret.setCapacitorRechargeRate( %turret.getDataBlock().capacitorRechargeRate );
   %obj.turretObject = %turret;

   //vehicle turrets should not auto fire at targets
   %turret.setAutoFire(false);

   %obj.schedule(6000, "playThread", $ActivateThread, "activate");
   %turret.mountImage(SandstormParam, 2);

   // set the turret's target info
   setTargetSensorGroup(%turret.getTarget(), %turret.team);
   setTargetNeverVisMask(%turret.getTarget(), 0xffffffff);
}

function SandstormTank::deleteAllMounted(%data, %obj) {
   %turret = %obj.getMountNodeObject(10);
   if (!%turret)
      return;

   if (%client = %turret.getControllingClient()) {
      %client.player.setControlObject(%client.player);
      %client.player.mountImage(%client.player.lastWeapon, $WeaponSlot);
      %client.player.mountVehicle = false;
   }
   %turret.schedule(2000, delete);
}

function SandstormTank::playerMounted(%data, %obj, %player, %node) {
//[[CHANGE]]
   if (%obj.clientControl)
       serverCmdResetControlObject(%obj.clientControl);

   if (%node == 0) {
      // driver position
      // is there someone manning the turret?
      //%turreteer = %obj.getMountedNodeObject(1);
	   commandToClient(%player.client, 'setHudMode', 'Pilot', "Assault", %node);
   }
   else if (%node == 1) {
      // turreteer position
      %turret = %obj.getMountNodeObject(10);
      %player.vehicleTurret = %turret;
      %player.setTransform("0 0 0 0 0 1 0");
      %player.lastWeapon = %player.getMountedImage($WeaponSlot);
      %player.unmountImage($WeaponSlot);
      if (!%player.client.isAIControlled())
      {
         %player.setControlObject(%turret);
         %player.client.setObjectActiveImage(%turret, 2);
      }
      %turret.turreteer = %player;
      // if the player is the turreteer, show vehicle's weapon icons
      //commandToClient(%player.client, 'showVehicleWeapons', %data.getName());
      //%player.client.setVWeaponsHudActive(1); // plasma turret icon (default)

      $aWeaponActive = 0;
      commandToClient(%player.client,'SetWeaponryVehicleKeys', true);
      %obj.getMountNodeObject(10).selectedWeapon = 1;
	   commandToClient(%player.client, 'setHudMode', 'Pilot', "Assault", %node);
   }

   // update observers who are following this guy...
   if ( %player.client.observeCount > 0 )
      resetObserveFollow( %player.client, false );

   // build a space-separated string representing passengers
   // 0 = no passenger; 1 = passenger (e.g. "1 0 ")
   %passString = buildPassengerString(%obj);
	// send the string of passengers to all mounted players
	for(%i = 0; %i < %data.numMountPoints; %i++)
		if (%obj.getMountNodeObject(%i) > 0)
		   commandToClient(%obj.getMountNodeObject(%i).client, 'checkPassengers', %passString);
}

function SandstormAAPod::onTrigger(%data, %obj, %trigger, %state) {
   switch (%trigger) {
      case 0:
         %obj.fireTrigger = %state;
            if(%state)
               %obj.setImageTrigger(4, true);
            else
               %obj.setImageTrigger(4, false);
      case 2:
         if(%state) {
            %obj.getDataBlock().playerDismount(%obj);
         }
   }
}

function SandstormAAPod::playerDismount(%data, %obj) {
   //Passenger Exiting
   %obj.fireTrigger = 0;
   %obj.setImageTrigger(2, false);
   %obj.setImageTrigger(4, false);
   %client = %obj.getControllingClient();
// %client.setControlObject(%client.player);
   %client.player.mountImage(%client.player.lastWeapon, $WeaponSlot);
   %client.player.mountVehicle = false;
   setTargetSensorGroup(%obj.getTarget(), 0);
   setTargetNeverVisMask(%obj.getTarget(), 0xffffffff);
//   %client.player.getDataBlock().doDismount(%client.player);
}
