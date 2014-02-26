//**************************************************************
// HAVOC HEAVY TRANSPORT FLIER
//**************************************************************

datablock FlyingVehicleData(WindshearPlatform) : HavocDamageProfile {

   spawnOffset = "0 0 6";
   renderWhenDestroyed = false;

   catagory = "Vehicles";
   shapeFile = "vehicle_air_hapc.dts";
   multipassenger = true;
   computeCRC = true;

   debrisShapeName = "vehicle_air_hapc_debris.dts";
   debris = ShapeDebris;

   drag = 0.2;
   density = 1.0;

   mountPose[0] = sitting;

   numMountPoints = 6;
   isProtectedMountPoint[0] = true;
   
   canControl = false;
   cameraMaxDist = 17;
   cameraOffset = 2;
   cameraLag = 8.5;
   explosion = LargeAirVehicleExplosion;
	explosionDamage = 0.5;
	explosionRadius = 5.0;

   maxDamage = 75.50;
   destroyedLevel = 75.50;

   isShielded = true;
   rechargeRate = 0.8;
   energyPerDamagePoint = 200;
   maxEnergy = 550;
   minDrag = 100;                // Linear Drag
   rotationalDrag = 2700;        // Anguler Drag

   // Auto stabilize speed
   maxAutoSpeed = 10;
   autoAngularForce = 3000;      // Angular stabilizer force
   autoLinearForce = 450;        // Linear stabilzer force
   autoInputDamping = 0.95;      //

   // Maneuvering
   maxSteeringAngle = 4.5;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
   horizontalSurfaceForce = 6;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
   verticalSurfaceForce = 4;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
   maneuveringForce = 5250;      // Horizontal jets (W,S,D,A key thrust)
   steeringForce = 675;         // Steering jets (force applied when you move the mouse)
   steeringRollForce = 3000;      // Steering jets (how much you heel over when you turn)
   rollForce = 1;                // Auto-roll (self-correction to right you after you roll/invert)
   hoverHeight = 2.5;        // Height off the ground at rest
   createHoverHeight = 1;  // Height off the ground when created
   maxForwardSpeed = 165;  // speed in which forward thrust force is no longer applied (meters/second)

   // Turbo Jet
   jetForce = 2500;      // Afterburner thrust (this is in addition to normal thrust)
   minJetEnergy = 40;     // Afterburner can't be used if below this threshhold.
   jetEnergyDrain = 10;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
   vertThrustMultiple = 1.25;

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
   mass = 150;        // Mass of the vehicle
   bodyFriction = 0;     // Don't mess with this.
   bodyRestitution = 0.5;   // When you hit the ground, how much you rebound. (between 0 and 1)
   minRollSpeed = 0;     // Don't mess with this.
   softImpactSpeed = 14;       // Sound hooks. This is the soft hit.
   hardImpactSpeed = 25;    // Sound hooks. This is the hard hit.

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 25;      // If hit ground at speed above this then it's an impact. Meters/second
   speedDamageScale = 0.060;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 28;
   collDamageMultiplier   = 0.020;

   //
   minTrailSpeed = 15;
   trailEmitter = ContrailEmitter;
   forwardJetEmitter = FlyerJetEmitter;
   downJetEmitter = FlyerJetEmitter;

   //
   jetSound = HAPCFlyerThrustSound;
   engineSound = HAPCFlyerEngineSound;
   softImpactSound = SoftImpactSound;
   hardImpactSound = HardImpactSound;
   //wheelImpactSound = WheelImpactSound;

   //
   softSplashSoundVelocity = 5.0;
   mediumSplashSoundVelocity = 8.0;
   hardSplashSoundVelocity = 12.0;
   exitSplashSoundVelocity = 8.0;

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
   cmdIcon = CMDFlyingHAPCIcon;
   cmdMiniIconName = "commander/MiniIcons/com_hapc_grey";
   targetNameTag = 'Harbinger';
   targetTypeTag = 'Assault Platform';
   sensorData = VehiclePulseSensor;

   checkRadius = 7.8115;
   observeParameters = "1 15 15";


   shieldEffectScale = "1.0 0.9375 0.45";
};

function WindshearPlatform::hasDismountOverrides(%data, %obj) {
   return true;
}

function WindshearPlatform::getDismountOverride(%data, %obj, %mounted) {
   %node = -1;
   for (%i = 0; %i < %data.numMountPoints; %i++)
   {
      if (%obj.getMountNodeObject(%i) == %mounted)
      {
         %node = %i;
         break;
      }
   }
   if (%node == -1)
   {
      warning("Couldn't find object mount point");
      return "0 0 1";
   }

   if (%node == 3 || %node == 2)
   {
      return "-1 0 1";
   }
   else if (%node == 5 || %node == 4)
   {
      return "1 0 1";
   }
   else
   {
      return "0 0 1";
   }
}

//**************************************************************
// WEAPONS
//**************************************************************

datablock TurretData(PlatFormPlasma) : TurretDamageProfile {
   className      = VehicleTurret;
   catagory       = "Turrets";
   shapeFile      = "turret_base_large.dts";
   preload        = true;

   mass           = 1.0;  // Not really relevant

   maxEnergy               = 200;
   maxDamage               = 3.0;
   destroyedLevel          = 3.0;
   repairRate              = 0;

   // capacitor
   maxCapacitorEnergy      = 250;
   capacitorRechargeRate   = 1.0;

   thetaMin = 0;
   thetaMax = 100;

   inheritEnergyFromMount = true;
   firstPersonOnly = true;
   useEyePoint = true;
   numWeapons = 1;

   cameraDefaultFov = 90.0;
   cameraMinFov = 5.0;
   cameraMaxFov = 120.0;

   targetNameTag = 'Platform';
   targetTypeTag = 'Plasma Cannon';
};

datablock TurretData(PlatFormMissile) : PlatFormPlasma {
   targetNameTag = 'Platform';
   targetTypeTag = 'Missile Turret';
};

datablock TurretData(PlatFormMortar) : PlatFormPlasma {
   targetNameTag = 'Platform';
   targetTypeTag = 'Mortar Cannon';
};

//
//PLASMA CANNON
//BARREL
//
datablock TargetProjectileData(PlasmaCannonTLProj)
{
   directDamage        	= 0.0;
   hasDamageRadius     	= false;
   indirectDamage      	= 0.0;
   damageRadius        	= 0.0;
   velInheritFactor    	= 1.0;

   maxRifleRange       	= 1000;
   beamColor           	= "0.1 1.0 0.1";

   startBeamWidth			= 0.3; //0.02
   pulseBeamWidth 	   = 0.7; //0.025
   beamFlareAngle 	   = 3.0;
   minFlareSize        	= 0.0;
   maxFlareSize        	= 400.0;
   pulseSpeed          	= 6.0;
   pulseLength         	= 0.150;

   textureName[0]      	= "special/nonlingradient";
   textureName[1]      	= "special/flare";
   textureName[2]      	= "special/pulse";
   textureName[3]      	= "special/expFlare";
   beacon               = true;
};

datablock TracerProjectileData(PlasmaCannonMainProj)
{
   doDynamicClientHits = true;

   directDamage        = 25.5;
   explosion           = "PlasmaBarrelBoltExplosion";
   splash              = ChaingunSplash;

   directDamageType    = $DamageType::PlasmaCannon;
   kickBackStrength  = 0.0;

   sound = ShrikeBlasterProjectileSound;

   dryVelocity       = 5000.0;
   wetVelocity       = 0.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 200;
   lifetimeMS        = 200;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 45.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = "0.1 1.0 0.1 1.0";
	tracerTex[0]  	 = "special/shrikeBolt";
	tracerTex[1]  	 = "special/shrikeBoltCross";
	tracerWidth     = 1.4;
   crossSize       = 0.99;
   crossViewAng    = 0.990;
   renderCross     = true;
};

datablock TurretImageData(PlasmaCannonTLBarrelLarge) {
   shapeFile = "turret_aa_large.dts";

   activationMS         = 175; // z0dd - ZOD, 3/27/02. Was 250. Amount of time it takes turret to unfold
   deactivateDelayMS    = 500;
   thinkTimeMS          = 140; // z0dd - ZOD, 3/27/02. Was 200. Amount of time before turret starts to unfold (activate)
   degPerSecTheta       = 600;
   degPerSecPhi         = 1080;
   attackRadius         = 700;

   projectile = PlasmaCannonTLProj;
   projectileType = TargetProjectile;
   deleteLastProjectile = false;

   offset = "0.1 0.75 0.0"; // L/R - F/B - T/B

   usesEnergy = true;
   minEnergy = 0;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.15;
   stateTransitionOnTimeout[0]      = "Fire";

   stateName[3]                     = "Fire";
   stateEnergyDrain[3]              = 0;
   stateFire[3]                     = true;
   stateScript[3]                   = "onFire";
};

datablock TurretImageData(PlasmaCannonBarrelLarge) {
   shapeFile = "turret_tank_barrelmortar.dts";

   projectile = PlasmaCannonMainProj;
   projectileType = TracerProjectile;
   usesEnergy = true;
   fireEnergy = 1;
   minEnergy = 1;
   emap = true;

   // Turret parameters
   activationMS      = 700; // z0dd - ZOD, 3/27/02. Was 1000. Amount of time it takes turret to unfold
   deactivateDelayMS = 1500;
   thinkTimeMS       = 140; // z0dd - ZOD, 3/27/02. Was 200. Amount of time before turret starts to unfold (activate)
   degPerSecTheta    = 300;
   degPerSecPhi      = 500;
   attackRadius      = 700; // z0dd - ZOD, 3/27/02. Was 300

   // State transitions
   stateName[0]                  = "Activate";
   stateTransitionOnNotLoaded[0] = "Dead";
   stateTransitionOnLoaded[0]    = "ActivateReady";

   stateName[1]                  = "ActivateReady";
   stateSequence[1]              = "Activate";
   stateSound[1]                 = PBLSwitchSound;
   stateTimeoutValue[1]          = 1;
   stateTransitionOnTimeout[1]   = "Ready";
   stateTransitionOnNotLoaded[1] = "Deactivate";
   stateTransitionOnNoAmmo[1]    = "NoAmmo";

   stateName[2]                    = "Ready";
   stateTransitionOnNotLoaded[2]   = "Deactivate";
   stateTransitionOnTriggerDown[2] = "Spinup";
   stateTransitionOnNoAmmo[2]      = "NoAmmo";
   
   stateName[3]         = "Spinup";
   stateSpinThread[3]   = SpinUp;
   stateSound[3]        = ChaingunSpinupSound;
   stateScript[3]           = "onSpinup";
   stateTimeoutValue[3]          = 2;
   stateWaitForTimeout[3]        = false;
   stateTransitionOnTimeout[3]   = "Fire";
   stateTransitionOnTriggerUp[3] = "Spindown";

   stateName[4]             = "Fire";
   stateSequence[4]            = "Fire";
   stateSpinThread[4]       = FullSpeed;

   stateScript[4]           = "onFire";
   stateFire[4]             = true;
   stateEjectShell[4]       = true;
   stateTimeoutValue[4]          = 1.0;
   stateTransitionOnTimeout[4]   = "Spindown";
   stateTransitionOnTriggerUp[4] = "Spindown";
   stateTransitionOnNoAmmo[4]    = "EmptySpindown";

   //--------------------------------------
   stateName[5]       = "Spindown";
   stateSpinThread[5] = SpinDown;
   stateScript[5]           = "onSpindown";
   stateTimeoutValue[5]            = 1.5;
   stateWaitForTimeout[5]          = true;
   stateTransitionOnTimeout[5]     = "Ready";
   stateTransitionOnTriggerDown[5] = "Spinup";

   //--------------------------------------
   stateName[6]       = "EmptySpindown";
   stateSpinThread[6] = SpinDown;
   stateScript[6]           = "onSpindown";
   stateTimeoutValue[5]            = 1.5;
   stateWaitForTimeout[5]          = true;
   stateTransitionOnTimeout[6] = "NoAmmo";

   //--------------------------------------
   stateName[7]       = "DryFire";
   stateSound[7]      = ChaingunDryFireSound;
   stateTimeoutValue[7]        = 0.5;
   stateTransitionOnTimeout[7] = "NoAmmo";

   stateName[8]               = "Dead";
   stateTransitionOnLoaded[8] = "ActivateReady";
   
   stateName[9]                     = "Deactivate";
   stateSequence[9]                 = "Activate";
   stateDirection[9]                = false;
   stateTimeoutValue[9]             = 1.0;
   stateTransitionOnLoaded[9]       = "ActivateReady";
   stateTransitionOnTimeout[9]      = "Dead";
   
   stateName[10]                    = "NoAmmo";
   stateTransitionOnAmmo[10]        = "Ready";
   stateSequence[10]                = "NoAmmo";
};

function PlasmaCannonBarrelLarge::onSpinup(%data,%obj,%slot){
   %obj.mountImage(PlasmaCannonTLBarrelLarge, 7);
}

function PlasmaCannonBarrelLarge::onSpindown(%data,%obj,%slot){
   %obj.unmountImage(7);
}

function PlasmaCannonTLBarrelLarge::onFire(%data,%obj,%slot) {
   %p = Parent::onFire(%data, %obj, %slot);
   %p.setTarget(%obj.team);
   %obj.PBCLP = %p;
   %p.schedule(10000, "delete");
}

function PlasmaCannonTLBarrelLarge::onUnmount(%this,%obj,%slot) {
   if(isObject(%obj.PBCLP)) {
      %obj.PBCLP.delete();
   }
   Parent::onUnmount(%this, %obj, %slot);
}
//END





//NEEDED FUNCTONS

function WindshearPlatform::onAdd(%this, %obj) {
   Parent::onAdd(%this, %obj);
   if (%obj.clientControl)
       serverCmdResetControlObject(%obj.clientControl);

   setTargetSensorGroup(%obj.getTarget(),%obj.team);
   //Turrets
   //PlatFormPlasma(2,3) PlatFormMissile(4,5) PlatFormMortar(1)

   %turret = TurretData::create(PlatFormPlasma);
   %turret.selectedWeapon = 1;
   MissionCleanup.add(%turret);
   %turret.team = %obj.team;
   %turret.setSelfPowered();
   %obj.mountObject(%turret, 2);
   %turret.setCapacitorRechargeRate(999);
   %turret.mountobj = %obj;
   %obj.turretObject = %turret;
   %turret.team = %obj.team;
   %turret.base = %obj;
   setTargetSensorGroup(%turret.getTarget(),%obj.team);
   %turret.mountImage("PlasmaCannonBarrelLarge", 0);
   %turret.setAutoFire(true); //YES

   %turret2 = TurretData::create(PlatFormPlasma);
   %turret2.selectedWeapon = 1;
   MissionCleanup.add(%turret2);
   %turret2.team = %obj.team;
   %turret2.setSelfPowered();
   %obj.mountObject(%turret2, 4);
   %turret2.setCapacitorRechargeRate(999);
   %turret2.mountobj = %obj;
   %obj.turretObject2 = %turret2;
   %turret2.team = %obj.team;
   %turret2.base = %obj;
   setTargetSensorGroup(%turret2.getTarget(),%obj.team);
   %turret2.mountImage("PlasmaCannonBarrelLarge", 0);
   %turret2.setAutoFire(true); //YES

   %turret3 = TurretData::create(PlatFormMissile);
   %turret3.selectedWeapon = 1;
   MissionCleanup.add(%turret3);
   %turret3.team = %obj.team;
   %turret3.setSelfPowered();
   %obj.mountObject(%turret3, 3);
   %turret3.setCapacitorRechargeRate(999);
   %turret3.mountobj = %obj;
   %obj.turretObject3 = %turret3;
   %turret3.team = %obj.team;
   %turret3.base = %obj;
   setTargetSensorGroup(%turret3.getTarget(),%obj.team);
   %turret3.mountImage("MissileBarrelLarge", 0);
   %turret3.setAutoFire(true); //YES

   %turret4 = TurretData::create(PlatFormMissile);
   %turret4.selectedWeapon = 1;
   MissionCleanup.add(%turret4);
   %turret4.team = %obj.team;
   %turret4.setSelfPowered();
   %obj.mountObject(%turret4, 5);
   %turret4.setCapacitorRechargeRate(999);
   %turret4.mountobj = %obj;
   %obj.turretObject4 = %turret4;
   %turret4.team = %obj.team;
   %turret4.base = %obj;
   setTargetSensorGroup(%turret4.getTarget(),%obj.team);
   %turret4.mountImage("MissileBarrelLarge", 0);
   %turret4.setAutoFire(true); //YES
   
   %turret5 = TurretData::create(PlatFormMortar);
   %turret5.selectedWeapon = 1;
   MissionCleanup.add(%turret5);
   %turret5.team = %obj.team;
   %turret5.setSelfPowered();
   %obj.mountObject(%turret5, 1);
   %turret5.setCapacitorRechargeRate(999);
   %turret5.mountobj = %obj;
   %obj.turretObject5 = %turret5;
   %turret5.team = %obj.team;
   %turret5.base = %obj;
   setTargetSensorGroup(%turret5.getTarget(),%obj.team);
   %turret5.mountImage("MortarBarrelLarge", 0);
   %turret5.setAutoFire(true); //YES

   %obj.schedule(5500, "playThread", $ActivateThread, "activate");
}

function WindshearPlatform::deleteAllMounted(%data, %obj) {
   %turret = %obj.turretObject;
   if (!%turret)
      return;
   %turret.schedule(1000, delete);

   %turret2 = %obj.turretObject2;
   if (!%turret2)
      return;
   %turret2.schedule(1000, delete);

   %turret3 = %obj.turretObject3;
   if (!%turret3)
      return;
   %turret3.schedule(1000, delete);

   %turret4 = %obj.turretObject4;
   if (!%turret4)
      return;
   %turret4.schedule(1000, delete);

   %turret5 = %obj.turretObject5;
   if (!%turret5)
      return;
   %turret5.schedule(1000, delete);
}


