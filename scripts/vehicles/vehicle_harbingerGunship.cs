datablock SensorData(AerialGunshipSensor) {
   detects = true;
   detectsUsingLOS = true;
   detectsPassiveJammed = false;
   detectsActiveJammed = false;
   detectsCloaked = false;
   detectionPings = true;
   detectRadius = 1750;
};

datablock ShockwaveData(ColliderWave) {
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
	height = "20";
	verticalCurve = "5";
	acceleration = "9";
	times[0] = "0";
	times[1] = "0.25";
	times[2] = "0.9";
	times[3] = "1";
	colors[0] = "1.000000 0.600000 0.200000 1.000000"; //1.0 0.9 0.9
	colors[1] = "1.000000 0.600000 0.200000 1.000000"; //0.6 0.6 0.6
	colors[2] = "1.000000 0.600000 0.200000 1.000000"; //0.6 0.6 0.6
	colors[3] = "1.000000 0.600000 0.200000 0.000000";
	texture[0] = "terraintiles/molten1";
	texture[1] = "special/shockwave4"; //gradient";
    texWrap = "1";
	is2D = "0";
	mapToTerrain = "0";
	orientToNormal = "1";
	renderBottom = "1";
	renderSquare = "0";
};

datablock ParticleData(ColliderExpandersPaticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;

   lifetimeMS           = 10000;
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

datablock ParticleEmitterData(ColliderExpandersEmitter)
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

   particles = "ColliderExpandersPaticle";
};

datablock ExplosionData(ProtonColliderExplosion) {
   shakeCamera = true;
   camShakeFreq = "20.0 18.0 17.0";
   camShakeAmp = "70.0 70.0 70.0";
   camShakeDuration = 1.3;
   camShakeRadius = 35;

   emitter[0] = "ColliderExpandersEmitter";
   Shockwave = "ColliderWave";
};

datablock TracerProjectileData(ColliderBolt) {
   doDynamicClientHits = true;

   directDamage        = 0.2; // Phantom139, From 1.45 (2.3)
   directDamageType    = $DamageType::GunTurret;
   hasDamageRadius     = true;
   indirectDamage      = 2.2;
   damageRadius        = 20.0;
   radiusDamageType    = $DamageType::GunTurret;
   explosion           = "ProtonColliderExplosion";
   splash              = ChaingunSplash;

   kickBackStrength  = 1500.0;
   sound 				= ChaingunProjectile;

   //dryVelocity       = 425.0;
   dryVelocity       = 1250.0; // z0dd - ZOD, 8-12-02. Was 425.0
   wetVelocity       = 1000.0;
   velInheritFactor  = 0.0;
   fizzleTimeMS      = 30000;
   lifetimeMS        = 30000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

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


//PROJECTLE FUNCTIONS
function ColliderBolt::onExplode(%data, %proj, %pos, %mod) {
   Parent::OnExplode(%data, %proj, %pos, %mod);
}



//Vehicle
datablock FlyingVehicleData(HarbingerGunship) : BomberDamageProfile
{
   spawnOffset = "0 0 2";
   canControl = true;
   catagory = "Vehicles";
   shapeFile = "vehicle_air_bomber.dts";
   multipassenger = true;
   computeCRC = true;

   weaponNode = 1;

   debrisShapeName = "vehicle_air_bomber_debris.dts";
   debris = ShapeDebris;
   renderWhenDestroyed = false;

   drag    = 0.2;
   density = 1.0;

   mountPose[0] = sitting;
   mountPose[1] = sitting;
   numMountPoints = 2;
   isProtectedMountPoint[0] = true;
   isProtectedMountPoint[1] = true;

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

   isShielded = true;
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
   targetNameTag = 'Harbinger';
   targetTypeTag = 'Gunship';
   sensorData = AerialGunshipSensor;

   max[MiniChaingunAmmo] = 9999;
   max[MortarAmmo] = 9999;

   checkRadius = 7.1895;
   observeParameters = "1 10 10";
   shieldEffectScale = "0.75 0.975 0.375";
   showPilotInfo = 1;
};

//TURRET
datablock TurretData(HarbingerGunshipTurret) : TurretDamageProfile
{
   className               = VehicleTurret;
   catagory                = "Turrets";
   shapeFile               = "turret_belly_base.dts";
   preload                 = true;
   canControl = true;
   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingBomberIcon;
   cmdMiniIconName = "commander/MiniIcons/com_bomber_grey";
   targetNameTag = 'Harbinger Collider';
   targetTypeTag = 'Cannon';

   mass                    = 1.0;  // Not really relevant
   repairRate              = 0;
   maxDamage               = HarbingerGunship.maxDamage;
   destroyedLevel          = HarbingerGunship.destroyedLevel;

   thetaMin                = 90;
   thetaMax                = 180;

   inheritEnergyFromMount  = true;
   firstPersonOnly         = true;
   useEyePoint             = true;
   numWeapons              = 2;
   
   max[MiniChaingunAmmo] = 9999;
   max[MortarAmmo] = 9999;
};

datablock TurretImageData(HarbingerGunshipCGImage) {
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

   usesEnergy = true;
   useMountEnergy = false;

   // Turret parameters
   activationMS         = 175; // z0dd - ZOD, 3/27/02. Was 250. Amount of time it takes turret to unfold
   deactivateDelayMS    = 500;
   thinkTimeMS          = 140; // z0dd - ZOD, 3/27/02. Was 200. Amount of time before turret starts to unfold (activate)
   degPerSecTheta       = 600;
   degPerSecPhi         = 1080;
   attackRadius         = 2000;

   stateName[0] = "Activate";
   stateSequence[0] = "Activate";
//   stateSound[0] = GravChaingunIdleSound;
   stateAllowImageChange[0] = true;
   stateTimeoutValue[0] = 0.1;
   stateTransitionOnTimeout[0] = "Ready";

   stateName[1] = "Ready";
   stateSpinThread[1] = Stop;
   stateTransitionOnTriggerDown[1] = "Spinup";

   stateName[2] = "Spinup";
   stateSpinThread[2] = SpinUp;
   //stateSound[3] = ChaingunSpinupSound;
   stateTimeoutValue[2] = 0.2;
   stateWaitForTimeout[2] = false;
   stateTransitionOnTimeout[2] = "Fire";
   stateTransitionOnTriggerUp[2] = "Spindown";

   stateName[3] = "Fire";
   stateSequence[3] = "Fire";
   stateSequenceRandomFlash[3] = true;
   stateSpinThread[3] = FullSpeed;
   stateSound[3] = ChaingunFireSound;
   //stateRecoil[4] = LightRecoil;
   stateAllowImageChange[3] = true;
   stateScript[3] = "onFire";
   stateFire[3] = true;
   stateEjectShell[3] = true;
   stateTimeoutValue[3] = 0.01;
   stateTransitionOnTimeout[3] = "Fire";
   stateTransitionOnTriggerUp[3] = "Spindown";

   stateName[4] = "Spindown";
   //stateSound[5] = ChaingunSpinDownSound;
   stateSpinThread[4] = SpinDown;
   stateTimeoutValue[4] = 0.05;
   stateWaitForTimeout[4] = true;
   stateTransitionOnTimeout[4] = "Ready";
   stateTransitionOnTriggerDown[4] = "Spinup";

   stateName[5] = "DryFire";
   stateSound[5] = ShrikeBlasterDryFireSound;
   stateTimeoutValue[5] = 0.5;
};

datablock TurretImageData(HarbingerGunshipColliderImage) {
   shapeFile = "turret_mortar_large.dts";
   offset = "-0.3 0 0";
   mountPoint = 0;

   item      = Mortar;
   
   projectile = ColliderBolt;
   projectileType = TracerProjectile;
   projectileSpread = 0.4 / 1000.0;
   emap = true;

   casing              = ShellDebris;
   shellExitDir        = "1.0 0.3 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 5.0;
   shellVelocity       = 0.0;

   usesEnergy = true;
   useMountEnergy = false;

   // Turret parameters
   activationMS         = 175; // z0dd - ZOD, 3/27/02. Was 250. Amount of time it takes turret to unfold
   deactivateDelayMS    = 500;
   thinkTimeMS          = 140; // z0dd - ZOD, 3/27/02. Was 200. Amount of time before turret starts to unfold (activate)
   degPerSecTheta       = 600;
   degPerSecPhi         = 1080;
   attackRadius         = 2000;

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
   stateTransitionOnTimeout[3] = "Fire2";
   stateTimeoutValue[3]        = 0.3;
   stateFire[3]                = true;
   stateRecoil[3]              = LightRecoil;
   stateAllowImageChange[3]    = true;
   stateSequence[3]            = "Fire";
   stateSound[3]               = MBLFireSound;
   stateScript[3]              = "onFire";
   
   stateName[4]                = "Fire2";
   stateTransitionOnTimeout[4] = "Reload";
   stateTimeoutValue[4]        = 0.3;
   stateFire[4]                = true;
   stateRecoil[4]              = LightRecoil;
   stateAllowImageChange[4]    = true;
   stateSequence[4]            = "Fire";
   stateSound[4]               = MBLFireSound;
   stateScript[4]              = "onFire";

   stateName[5]                  = "Reload";
   stateTimeoutValue[5]          = 4.5;
   stateAllowImageChange[5]      = true;
   stateSequence[5]              = "Reload";
   stateTransitionOnTimeout[5]   = "Ready";
   stateTransitionOnNotLoaded[5] = "Deactivate";

   stateName[6]                = "Deactivate";
   stateSequence[6]            = "Activate";
   stateDirection[6]           = false;
   stateTimeoutValue[6]        = 1;
   stateTransitionOnLoaded[6]  = "ActivateReady";
   stateTransitionOnTimeout[6] = "Dead";

   stateName[7]               = "Dead";
   stateTransitionOnLoaded[7] = "ActivateReady";
};


//Functions
function HarbingerGunship::playerMounted(%data, %obj, %player, %node) {
   switch(%node) {
      case 0:
         bottomPrint(%player.client, "PILOT: Fly around \n [MINE] Toggle Weapons (No Gunner)", 2, 2);
      case 1:
         bottomPrint(%player.client, "GUNNER: Fire... \n [MINE] Toggle Weapons", 2, 2);
         %player.isACGunner = 1;
         %player.client.setControlObject(%obj.turretObject);
   }
}

function HarbingerGunship::deleteAllMounted(%data, %obj) {
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

function HarbingerGunship::onAdd(%this, %obj) {
   Parent::onAdd(%this, %obj);

   setTargetSensorGroup(%obj.getTarget(),%obj.team);
   %turret = TurretData::create(HarbingerGunshipTurret);
   %turret.selectedWeapon = 1;
   MissionCleanup.add(%turret);
   %turret.setSelfPowered();
   %obj.mountObject(%turret, 10);
   %turret.setCapacitorRechargeRate(999);
   %turret.mountobj = %obj;
   %obj.turretObject = %turret;
   %turret.team = %obj.team;
   %turret.base = %obj;
   %turret.mountImage(HarbingerGunshipCGImage,0);
   setTargetSensorGroup(%turret.getTarget(),%obj.team);
   %turret.setAutoFire(true); //YES

   %turret.setInventory(MiniChaingunAmmo, 9999, true);
   %turret.setInventory(MortarAmmo, 9999, true);
   
   %obj.barrel = "Chain";
}

function HarbingerGunship::onTrigger(%data, %obj, %trigger, %state) {
   %plyr = %obj.getMountNodeObject(0);
   if(%obj.turretObject.getControllingClient() != 0) {
      BottomPrint(%plyr.client, "Harbinger: There is a gunner, cannot change weapons", 3, 1);
      return;
   }
   if(%state == 1 && %trigger == 5) {
       if(%obj.barrel $= "Chain") {
          %obj.barrel = "Collider";
          %obj.turretObject.mountImage(HarbingerGunshipColliderImage, 0);
          BottomPrint(%plyr.client, "Harbinger: Switching to collider cannon", 3, 1);
       }
       else {
          %obj.barrel = "Chain";
          %obj.turretObject.mountImage(HarbingerGunshipCGImage, 0);
          BottomPrint(%plyr.client, "Harbinger: Switching to 50Cal Chaingun", 3, 1);
       }
   }
}

function HarbingerGunshipTurret::playerDismount(%data, %obj) {
   %client = %obj.getControllingClient();

   commandToClient( %client, 'VehicleDismount' );
   %client.player.mountVehicle = false;
   %client.player.isACGunner = 0;
   if(%client.player.getState() !$= "Dead") {
      %client.player.mountImage(%client.player.lastWeapon, $WeaponSlot);
   }
}

function HarbingerGunshipTurret::onTrigger(%data, %obj, %trigger, %state) {
   %cl = %obj.getControllingClient();
   if(%state == 1) {
      if(%trigger == 2 && %cl.player.isACGunner) {
         %cl.setControlObject(%cl.player);
         %obj.getDataBlock().playerDismount(%obj);
      }
      if(%trigger == 5) {
         if(%obj.mountobj.barrel $= "Chain") {
            %obj.mountobj.barrel = "Collider";
            %obj.mountImage(HarbingerGunshipColliderImage, 0);
            BottomPrint(%cl, "Harbinger: Switching to collider cannon", 3, 1);
            commandToClient(%cl, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
         }
         else {
            %obj.mountobj.barrel = "Chain";
            %obj.mountImage(HarbingerGunshipCGImage, 0);
            BottomPrint(%cl, "Harbinger: Switching to 50Cal Chaingun", 3, 1);
            commandToClient(%cl, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
         }
      }
   }
}

function HarbingerGunshipTurret::onDamage(%data, %obj) {
   %newDamageVal = %obj.getDamageLevel();
   if(%obj.lastDamageVal !$= "")
      if(isObject(%obj.getObjectMount()) && %obj.lastDamageVal > %newDamageVal)
         %obj.getObjectMount().setDamageLevel(%newDamageVal);
   %obj.lastDamageVal = %newDamageVal;
}

function HarbingerGunshipColliderImage::onFire(%data, %obj, %slot) {
   %p = Parent::onFire(%data, %obj, %slot);
   if(isObject(%obj.GetControllingClient().player) && %obj.GetControllingClient().player.getState() !$= "dead") {
      %p.sourceObject = %obj.GetControllingClient().player;
      //Now I get ze killz... muhaha
      //messageAllExcept(%obj.GetControllingClient(), -1, 'msgFia', "~wfx/powered/turret_mortar_fire.wav");
   }
}

function HarbingerGunshipCGImage::onFire(%data, %obj, %slot) {
   %p = Parent::onFire(%data, %obj, %slot);
   if(isObject(%obj.GetControllingClient().player) && %obj.GetControllingClient().player.getState() !$= "dead") {
      %p.sourceObject = %obj.GetControllingClient().player;
      //Now I get ze killz... muhaha
      //messageAllExcept(%obj.GetControllingClient(), -1, 'msgFia', "~wfx/weapons/chaingun_fire.wav");
   }
}
