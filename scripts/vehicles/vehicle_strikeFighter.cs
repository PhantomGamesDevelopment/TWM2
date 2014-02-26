datablock FlyingVehicleData(StrikeFlyer) : ShrikeDamageProfile
{
   spawnOffset = "0 0 2";
   canControl = false;
   catagory = "Vehicles";
   shapeFile = "vehicle_air_scout.dts";
   multipassenger = false;
   computeCRC = true;

   debrisShapeName = "vehicle_air_scout.dts";
   debris = MeShapeDebris;
   renderWhenDestroyed = false;

   drag    = 0.15;
   density = 1.0;

   mountPose[0] = sitting;
   numMountPoints = 1;
   isProtectedMountPoint[0] = false;
   cameraMaxDist = 15;
   cameraOffset = 2.5;
   cameraLag = 0.9;
   explosion = MeVehicleExplosion;
	explosionDamage = 1.0;
	explosionRadius = 10.0;

   maxDamage = 3.5;
   destroyedLevel = 3.5;

   HDAddMassLevel = 2.625;
   HDMassImage = LflyerHDMassImage;

   isShielded = false;
   energyPerDamagePoint = 0;
   maxEnergy = 500;      // Afterburner and any energy weapon pool
   rechargeRate = 4;

   minDrag = 25;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
   rotationalDrag = 900;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

   maxAutoSpeed = 57;       // Autostabilizer kicks in when less than this speed. (meters/second)
   autoAngularForce = 400;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
   autoLinearForce = 1;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
   autoInputDamping = 0.8;      // Dampen control input so you don't` whack out at very slow speeds


   // Maneuvering
   maxSteeringAngle = 7.0;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
   horizontalSurfaceForce = 6;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
   verticalSurfaceForce = 4;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
   maneuveringForce = 4750;      // Horizontal jets (W,S,D,A key thrust)
   steeringForce = 587;         // Steering jets (force applied when you move the mouse)
   steeringRollForce = 3000;      // Steering jets (how much you heel over when you turn)
   rollForce = 1;                // Auto-roll (self-correction to right you after you roll/invert)
   hoverHeight = 2.5;        // Height off the ground at rest
   createHoverHeight = 1;  // Height off the ground when created
   maxForwardSpeed = 190;  // speed in which forward thrust force is no longer applied (meters/second)

   // Turbo Jet
   jetForce = 2000;      // Afterburner thrust (this is in addition to normal thrust)
   minJetEnergy = 40;     // Afterburner can't be used if below this threshhold.
   jetEnergyDrain = 12;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
   vertThrustMultiple = 0.0;

   // Rigid body
   mass = 180;        // Mass of the vehicle
   bodyFriction = 0;     // Don't mess with this.
   bodyRestitution = 0.5;   // When you hit the ground, how much you rebound. (between 0 and 1)
   minRollSpeed = 0;     // Don't mess with this.
   softImpactSpeed = 14;       // Sound hooks. This is the soft hit.
   hardImpactSpeed = 25;    // Sound hooks. This is the hard hit.

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 20;      // If hit ground at speed above this then it's an impact. Meters/second
   speedDamageScale = 0.06;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 23.0;
   collDamageMultiplier   = 0.02;

   //
   minTrailSpeed = 150;      // The speed your contrail shows up at.
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

   damageEmitter[0] = MeLightDamageSmoke;
   damageEmitter[1] = MeHeavyDamageSmoke;
   damageEmitter[2] = MeDamageBubbles;
   damageEmitterOffset[0] = "0.0 -3.0 0.0 ";
   damageLevelTolerance[0] = 0.4;
   damageLevelTolerance[1] = 0.75;
   numDmgEmitterAreas = 1;

   //
   max[chaingunAmmo] = 1500;
   max[MissileLauncherAmmo] = 2;
   max[MortarAmmo] = 3;

   minMountDist = 7;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_scout_grey";
   targetNameTag = 'F41 Awring Strike';
   targetTypeTag = 'Fighter';
   sensorData = combatSensor;
   sensorRadius = combatSensor.detectRadius;
   sensorColor = "9 9 255";

   checkRadius = 5.5;
   observeParameters = "1 10 10";

   runningLight[0] = ShrikeLight1;
//   runningLight[1] = ShrikeLight2;

   shieldEffectScale = "0.937 1.125 0.60";

   numWeapons = 3;

   replaceTime = 110;
};

//----------------------------------------------
//Projectiles
//----------------------------------------------

datablock BombProjectileData(NapalmBomb)
{
   projectileShapeName  = "bomb.dts";
   emitterDelay         = -1;
   directDamage         = 0.0;
   hasDamageRadius      = true;
   indirectDamage       = 1.0;
   damageRadius         = 30;
   radiusDamageType     = $DamageType::Plasma;
   kickBackStrength     = 2000;

   explosion            = "VehicleBombExplosion";
   velInheritFactor     = 1.0;

   grenadeElasticity    = 0.25;
   grenadeFriction      = 0.4;
   armingDelayMS        = 2000;
   muzzleVelocity       = 0.1;
   drag                 = 0.3;

   minRotSpeed          = "60.0 0.0 0.0";
   maxRotSpeed          = "80.0 0.0 0.0";
   scale                = "1.0 1.0 1.0";

   sound                = BomberBombProjectileSound;
};

datablock SeekerProjectileData(sidewinder_MarkIII)
{
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   hasDamageRadius     = true;
   indirectDamage      = 1.0;
   damageRadius        = 12.0;
   radiusDamageType    = $DamageType::Missile;
   kickBackStrength    = 750;

   explosion           = "MissileExplosion";
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

   lifetimeMS          = 20000; // z0dd - ZOD, 4/14/02. Was 6000
   muzzleVelocity      = 12.0;
   maxVelocity         = 215.0; // z0dd - ZOD, 4/14/02. Was 80.0
   turningSpeed        = 60.0;
   acceleration        = 100.0;

   proximityRadius     = 5;

   terrainAvoidanceSpeed         = 100;
   terrainScanAhead              = 25;
   terrainHeightFail             = 12;
   terrainAvoidanceRadius        = 30;

   flareDistance = 100;
   flareAngle    = 25;
   minSeekHeat   = 0.5;

   sound = MissileProjectileSound;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.2 0.05 0";

   useFlechette = true;
   flechetteDelayMs = 100;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = false;
};

//--------------------------------------------------
//weaponImages
//--------------------------------------------------

datablock ShapeBaseImageData(StrikeChaingunImage)
{
   className = WeaponImage;
   shapeFile = "turret_missile_large.dts"; //was turret_tank_barrelchain.dts
   item      = MiniChaingun;
   ammo   = MiniChaingunAmmo;
   projectile = ApacheCGBullet;
   projectileType = TracerProjectile;
   mountPoint = 10;
   offset = "0 1.5 -0.2"; // L/R - F/B - T/B was "0 3.25 0.75"
   rotation = "0 1 0 180";

   projectileSpread = 1.0 / 1000.0;

   usesEnergy = false;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.1;
   stateSequence[0] = "Activate";

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateFire[3] = true;
   stateScript[3] = "onFire";
   stateSound[3] = ShrikeBlasterFire;
   stateTimeoutValue[3] = 0.01;
   stateTransitionOnTimeout[3] = "Reload";
   stateAllowImageChange[3] = false;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.01;
   stateAllowImageChange[4] = false;
   stateSound[4] = MissileReloadSound;

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6] = "DryFire";
   stateSound[6] = ShrikeBlasterDryFireSound;
   stateTimeoutValue[6] = 1.0;
   stateTransitionOnTimeout[6] = "NoAmmo";
};

datablock ShapeBaseImageData(StrikeMissileImage)
{
   className = WeaponImage;
   shapeFile = "weapon_energy_vehicle.dts";
   item = MissileLauncher;
   ammo = MissileLauncherAmmo;
   projectile = sidewinder_MarkIII;
   projectileType = SeekerProjectile;

   mountPoint = 10;
   offset = "0 -0 -0.15"; // L/R - F/B - T/B

   usesEnergy = false;
   useMountEnergy = true;
   minEnergy = 100;
   fireEnergy = 100;
   fireTimeout = 125;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.1;
   stateFire[3] = true;
   stateAllowImageChange[3] = false;
   stateScript[3] = "onFire";
   stateEmitterTime[3] = 0.2;
   stateSound[3] = MissileFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.1;
   stateAllowImageChange[4] = false;
   stateSound[4] = MissileReloadSound;

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6] = "DryFire";
   stateSound[6] = ShrikeBlasterDryFireSound;
   stateTimeoutValue[6] = 1.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
};

datablock ShapeBaseImageData(StrikeBombImage)
{
   className = WeaponImage;
   shapeFile = "weapon_energy_vehicle.dts";
   item = Mortar;
   ammo = MortarAmmo;
   projectile = NapalmBomb;
   projectileType = BombProjectile;

   mountPoint = 10;
   offset = "0 -0 -0.15"; // L/R - F/B - T/B

   usesEnergy = false;
   useMountEnergy = true;
   minEnergy = 100;
   fireEnergy = 100;
   fireTimeout = 125;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.1;
   stateFire[3] = true;
   stateAllowImageChange[3] = false;
   stateScript[3] = "onFire";
   stateEmitterTime[3] = 0.2;
   stateSound[3] = BomberBombFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.5;
   stateAllowImageChange[4] = false;
   stateSound[4] = MissileReloadSound;

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6] = "DryFire";
   stateSound[6] = BomberBombDryFireSound;
   stateTimeoutValue[6] = 1.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
};

//------------------------------------------------------
//Vehicle Functions
//------------------------------------------------------

function StrikeFlyer::onAdd(%this, %obj)
{
   Parent::onAdd(%this, %obj);
   if (%obj.clientControl)
       serverCmdResetControlObject(%obj.clientControl);

   %obj.mountImage(ScoutChaingunParam, 0);
   %obj.mountImage(StrikeMissileImage, 2);
   %obj.mountImage(StrikeBombImage, 3);
   %obj.mountImage(StrikeChaingunImage, 4);
   %obj.selectedWeapon = 1;
   %obj.nextWeaponFire = 2;
   %obj.setInventory(MiniChaingunAmmo, 1500);
   %obj.setInventory(MissileLauncherAmmo, 2);
   %obj.setInventory(MortarAmmo, 3);
   %obj.schedule(5500, "playThread", $ActivateThread, "activate");
}

function StrikeFlyer::playerMounted(%data, %obj, %player, %node)
{
   %ammoAmt = %obj.inv[MissileLauncherAmmo];
   if(%ammoAmt)
      %obj.setInventory(MissileLauncherAmmo, 2);

   %ammoAmt = %obj.inv[MortarAmmo];
   if(%ammoAmt)
      %obj.setInventory(MortarAmmo, 3);

   %ammoAmt = %obj.inv[MiniChaingunAmmo];
   if(%ammoAmt)
     %obj.incInventory(MiniChaingunAmmo, %ammoAmt);

   bottomPrint(%player.client, "Strike Fighter: wep1 CG, wep2 missile, wep3 Napalm bombs", 5, 2 );


   commandToClient(%player.client, 'setHudMode', 'Pilot', "Shrike2", %node);
   %obj.selectedWeapon = 1;
   $numVWeapons = 3;
   commandToClient(%player.client, 'SetWeaponryVehicleKeys', true);
   if( %player.client.observeCount > 0 )
      resetObserveFollow( %player.client, false );
}

function StrikeFlyer::playerDismounted(%data, %obj, %player)
{
   %obj.fireWeapon = false;
   %obj.setImageTrigger(2, false);
   %obj.setImageTrigger(3, false);
   %obj.setImageTrigger(4, false);
   %obj.setImageTrigger(5, false);
   %obj.setImageTrigger(6, false);
   setTargetSensorGroup(%obj.getTarget(), %obj.team);
   if( %player.client.observeCount > 0 )
      resetObserveFollow( %player.client, true );
}

function StrikeFlyer::onTrigger(%data, %obj, %trigger, %state)
{
   // data = ScoutFlyer datablock
   // obj = ScoutFlyer object number
   // trigger = 0 for "fire", 1 for "jump", 3 for "thrust"
   // state = 1 for firing, 0 for not firing
   if(%trigger == 0)
   {
      switch (%state) {
         case 0:
            %obj.fireWeapon = false;
		%obj.setImageTrigger(2, false);
		%obj.setImageTrigger(3, false);
		%obj.setImageTrigger(4, false);
         case 1:
            %obj.fireWeapon = true;
		if(%obj.selectedWeapon == 1)
            {
		   %obj.setImageTrigger(2, false);
		   %obj.setImageTrigger(3, false);
		   %obj.setImageTrigger(4, true);
		}
            else if(%obj.selectedWeapon == 2)
            {
		   %obj.setImageTrigger(2, true);
		   %obj.setImageTrigger(3, false);
		   %obj.setImageTrigger(4, false);
            }
		else
		{
		   %obj.setImageTrigger(2, false);
		   %obj.setImageTrigger(3, true);
		   %obj.setImageTrigger(4, false);
		}
      }
   }
}

function strikeflyer::onDestroyed(%data, %obj, %prevState)
{
   if(%obj.lastPilot.lastVehicle == %obj)
      if(%obj.getMountNodeObject(0) == %obj.lastPilot)
         schedule(200, %obj.lastPilot, "scKillPilot", %obj.lastPilot, %obj.lastDamagedBy);

   Parent::onDestroyed(%data, %obj, %prevState);
}

//------------------------------------------------------
//Weapon Functions
//------------------------------------------------------

function StrikeMissileImage::onFire(%data,%obj,%slot)
{
   %p = Parent::onFire(%data, %obj, %slot);
   if(isobject(%obj) || %obj) {
   %obj.getMountNodeObject(0).decInventory(%data.ammo, 1);
   }
   MissileSet.add(%p);

   if (%obj.getControllingClient())
      if(isobject(%obj) || %obj)
      %target = %obj.getLockedTarget();
   else
      if(isobject(%obj) || %obj)
      %target = %obj.getTargetObject();

   %homein = missileCheckAirTarget(%target);
   if(%target && %homein)
      %p.setObjectTarget(%target);
   else if(%obj.isLocked())
      %p.setPositionTarget(%obj.getLockedPosition());
   else
      %p.setNoTarget();
}

function StrikeBombImage::onFire(%data,%obj,%slot){
   if(isobject(%obj) || %obj) {
   %p = Parent::onFire(%data, %obj, %slot);
   %obj.getMountNodeObject(0).decInventory(%data.ammo, 1);
   %p.spdvec = %obj.getVelocity();
   }
}

function Strikeflyer::onEnterLiquid(%data, %obj, %coverage, %type)
{
   switch(%type)
   {
      case 0:
         //Water
         %obj.setHeat(0.0);
         %obj.liquidDamage(%data, 0.1, $DamageType::Crash);
      case 1:
         //Ocean Water
         %obj.setHeat(0.0);
         %obj.liquidDamage(%data, 0.1, $DamageType::Crash);
      case 2:
         //River Water
         %obj.setHeat(0.0);
         %obj.liquidDamage(%data, 0.1, $DamageType::Crash);
      case 3:
         //Stagnant Water
         %obj.setHeat(0.0);
         %obj.liquidDamage(%data, 0.1, $DamageType::Crash);
      case 4:
         //Lava
         %obj.liquidDamage(%data, $VehicleDamageLava, $DamageType::Lava);
      case 5:
         //Hot Lava
         %obj.liquidDamage(%data, $VehicleDamageHotLava, $DamageType::Lava);
      case 6:
         //Crusty Lava
         %obj.liquidDamage(%data, $VehicleDamageCrustyLava, $DamageType::Lava);
      case 7:
         //Quick Sand
   }
}

datablock ParticleData(NapalmInitExpFlameParticle)
{
   dragCoefficient      = 0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -1.1;
   lifetimeMS           = 2000;
   lifetimeVarianceMS   = 0;
   textureName          = "special/cloudflash";
   colors[0]     = "1 0.18 0.03 0.6";
   colors[1]     = "1 0.18 0.03 0.0";
   sizes[0]      = 7;
   sizes[1]      = 8;
};

datablock ParticleEmitterData(NapalmInitExpFlameEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionOffset = 2.0;
   ejectionVelocity = 30.0;
   velocityVariance = 10.0;
   thetaMin         = 0.0;
   thetaMax         = 90.0;
   lifetimeMS       = 250;

   particles = "NapalmInitExpFlameParticle";
};

datablock ParticleData(NapalmExpGroundBurnParticle)
{
   dragCoefficient      = 2;
   gravityCoefficient   = -0.4;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 3000;
   lifetimeVarianceMS   = 0;
   textureName          = "special/cloudflash";
   colors[0]     = "1 0.18 0.03 0.6";
   colors[1]     = "1 0.18 0.03 0.0";
   sizes[0]      = 6;
   sizes[1]      = 6.75;
};

datablock ParticleEmitterData(NapalmExpGroundBurnEmitter)
{
   ejectionPeriodMS = 8;
   periodVarianceMS = 0;
   ejectionOffset = 0.0;
   ejectionVelocity = 10.0;
   velocityVariance = 10.0;
   thetaMin         = 87.0;
   thetaMax         = 88.0;
   lifetimeMS       = 10000;

   particles = "NapalmExpGroundBurnParticle";
};

datablock ExplosionData(NapalmExplosion)
{
//   soundProfile   = BombExplosionSound;
   soundProfile   = MortarExplosionSound;
   emitter[0] = NapalmInitExpFlameEmitter;
   emitter[1] = NapalmExpGroundBurnEmitter;

   explosionShape = "effect_plasma_explosion.dts";
   faceViewer = true;
   lifetimeMS = 10000;
   playSpeed = 0.7;

   sizes[0] = "7.0 7.0 7.0";
   sizes[1] = "7.0 7.0 7.0";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock TracerProjectileData(napalmSubExplosion)
{
   doDynamicClientHits = true;

   directDamage        = 0.0;
   directDamageType    = $DamageType::Plasma;
   explosion           = NapalmExplosion;
   splash              = ChaingunSplash;

   hasDamageRadius     = true;
   indirectDamage      = 0.3;
   damageRadius        = 20;
   radiusDamageType    = $DamageType::Plasma;

   kickBackStrength  = 5;
   sound             = ChaingunProjectile;

   dryVelocity       = 30.0;
   wetVelocity       = 30.0;
   velInheritFactor  = 0;
   fizzleTimeMS      = 3000;
   lifetimeMS        = 6000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 1.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.20;
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
   lightRadius = 1.0;
   lightColor  = "0.5 0.5 0.175";
};

function NapalmBomb::onExplode(%data, %proj, %pos, %mod)
{
   %vec = %proj.spdvec;
   %vec = getword(%vec, 0)@" "@getword(%vec, 1)@" 0";
   %vec = vectorNormalize(%vec);
   %vec = vectorscale(%vec, 30);
   %result = containerRayCast(vectoradd(%pos,"0 0 10"), vectoradd(%pos,%vec), $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::TerrainObjectType, %proj);
   for(%i = 0; %i < 3; %i++){
	if(%result)
	   schedule((5 * %i), 0, "NapalmFindNewDir", %pos, %vec, %proj.sourceobject, 0, 0);
	else {
	   %rndvec = (getRandom(1, 15) - 7.5)@" "@(getRandom(1, 15) - 7.5)@" "@((getRandom() * 5) + 5);
	   %newvec = vectoradd(%vec,%rndvec);
	   %newvec = vectoradd(%pos,%newvec);
	   %p = new TracerProjectile()
	   {
	   	dataBlock        = napalmSubExplosion;
	   	initialDirection = "0 0 -1";
	   	initialPosition  = %newvec;
   	   	sourceSlot       = 5;
	   };
	   %p.sourceobject = %proj.sourceobject;
	   %p.vector = %vec;
	   %p.count = 1;
	}
   }

   if (%data.hasDamageRadius)
      RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
}

function napalmSubExplosion::onExplode(%data, %proj, %pos, %mod)
{
   if(!isSet(%proj.maxCount)) {
      %proj.maxCount = 5;
   }
   if(%proj.count < %proj.maxCount){
      %vec = vectorscale(vectornormalize(%proj.vector), 24);
      %result = containerRayCast(vectoradd(%pos,"0 0 10"), vectoradd(%pos,%vec), $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::TerrainObjectType, %proj);
	if(%result)
         schedule(5, 0, "NapalmFindNewDir", %pos, %vec, %proj.sourceobject, %proj.count, 0);
	else{
	   %rndvec = (getRandom(1, 10) - 5)@" "@(getRandom(1, 10) - 5)@" "@((getRandom() * 5) + 5);
	   %newvec = vectoradd(%vec,%rndvec);
	   %newvec = vectoradd(%pos,%newvec);
	   %p = new TracerProjectile()
	   {
		dataBlock        = napalmSubExplosion;
		initialDirection = "0 0 -1";
		initialPosition  = %newvec;
		sourceObject     = %proj.sourceobject;
   		sourceSlot       = 5;
	   };
	   %p.sourceobject = %proj.sourceobject;
	   %p.vector = %vec;
	   %p.count = %proj.count + 1;
       %p.maxCount = %proj.maxCount;
	}
   }

   if (%data.hasDamageRadius)
      RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
}

function NapalmFindNewDir(%pos, %vec, %source, %count, %count2){
   if(%count2 == 2){
	%rndvec = getRandom(1, 10)@" "@getRandom(1, 10)@" "@((getRandom() * 5) + 4);
	%newvec = vectoradd(%pos,%rndvec);
	%p = new TracerProjectile()
	{
	   dataBlock        = napalmSubExplosion;
	   initialDirection = "0 0 -1";
	   initialPosition  = %newvec;
	   sourceObject     = %source;
   	   sourceSlot       = 5;
	};
	%p.sourceobject = %source;
	%p.vector = %vec;
	%p.count = %count+1;
	return;
   }
   if(%count2 == 1){
	%vec = vectorscale(%vec,-1);
      %result = containerRayCast(vectoradd(%pos,"0 0 10"), vectoradd(%pos,%vec), $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::TerrainObjectType, 0);
	if(!(%result)){
	   %rndvec = getRandom(1, 10)@" "@getRandom(1, 10)@" "@((getRandom() * 5) + 4);
	   %newvec = vectoradd(%vec,%rndvec);
	   %newvec = vectoradd(%pos,%newvec);
	   %p = new TracerProjectile()
	   {
		dataBlock        = napalmSubExplosion;
		initialDirection = "0 0 -1";
		initialPosition  = %newvec;
		sourceObject     = %source;
   		sourceSlot       = 5;
	   };
	   %p.sourceobject = %source;
	   %p.vector = %vec;
	   %p.count = %count+1;
	   return;
	}
   }
   else {
	%chance = getrandom(1,4);
	if(%chance <= 2){
	   %nv2 = (getword(%vec, 0) * -1);
	   %nv1 = getword(%vec, 1);
	   %vec = %nv1@" "@%nv2@" 0";
	}else{
	   %nv2 = getword(%vec, 0);
	   %nv1 = (getword(%vec, 1) * -1);
	   %vec = %nv1@" "@%nv2@" 0";
	}
      %result = containerRayCast(vectoradd(%pos,"0 0 10"), vectoradd(%pos,%vec), $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::TerrainObjectType, 0);
	if(!(%result)){
	   %rndvec = getRandom(1, 10)@" "@getRandom(1, 10)@" "@((getRandom() * 5) + 4);
	   %newvec = vectoradd(%vec,%rndvec);
	   %newvec = vectoradd(%pos,%newvec);
	   %p = new TracerProjectile()
	   {
		dataBlock        = napalmSubExplosion;
		initialDirection = "0 0 -1";
		initialPosition  = %newvec;
		sourceObject     = %source;
   		sourceSlot       = 5;
	   };
	   %p.sourceobject = %source;
	   %p.vector = %vec;
	   %p.count = %count+1;
	   return;
	}
   }
   %count2++;
   schedule(2, 0, "NapalmFindNewDir", %pos, %vec, %source, %count, %count2);
}
