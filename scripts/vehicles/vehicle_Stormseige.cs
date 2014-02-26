datablock SensorData(SSTurretBaseSensorObj) {
   detects = true;
   detectsUsingLOS = true;
   detectsPassiveJammed = false;
   detectsActiveJammed = false;
   detectsCloaked = true;
   detectionPings = true;
   detectRadius = 700;
};

datablock FlyingVehicleData(StormSeigeDrone) : ShrikeDamageProfile
{
   spawnOffset = "0 0 2";
   canControl = false;
   catagory = "Vehicles";
   shapeFile = "vehicle_air_bomber.dts";
   multipassenger = false;
   computeCRC = true;

   debrisShapeName = "vehicle_air_bomber.dts";
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

   maxDamage = 50.0;
   destroyedLevel = 50.0;

   HDAddMassLevel = 49.9;
   HDMassImage = LflyerHDMassImage;

   isShielded = false;
   energyPerDamagePoint = 0;
   maxEnergy = 5000;      // Afterburner and any energy weapon pool
   rechargeRate = 4;

   minDrag = 22;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
   rotationalDrag = 900;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

   maxAutoSpeed = 50;       // Autostabilizer kicks in when less than this speed. (meters/second)
   autoAngularForce = 400;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
   autoLinearForce = 1;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
   autoInputDamping = 0.8;      // Dampen control input so you don't` whack out at very slow speeds


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

   // Rigid body
   mass = 150;        // Mass of the vehicle
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
   max[chaingunAmmo] = 2000;
   max[MissileLauncherAmmo] = 200;
   max[MortarAmmo] = 200;

   minMountDist = 7;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_scout_grey";
   targetNameTag = 'Stormseige';
   targetTypeTag = ' Battle Drone';
   sensorData = SSTurretBaseSensorObj;
   sensorRadius = SSTurretBaseSensorObj.detectRadius;
   sensorColor = "9 9 255";

   checkRadius = 5.5;
   observeParameters = "1 10 10";

   runningLight[0] = ShrikeLight1;
//   runningLight[1] = ShrikeLight2;

   shieldEffectScale = "0.937 1.125 0.60";

   numWeapons = 3;

   replaceTime = 90;
};

function StormSeigeDrone::onDestroyed(%data, %obj, %prevState) {
   if(%obj.isDrone) {
      %obj.DronePilot.delete();
   }

   Parent::onDestroyed(%data, %obj, %prevState);
}

function StormSeigeDrone::onEnterLiquid(%data, %obj, %coverage, %type)
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

datablock TurretData(ssTurret) : TurretDamageProfile
{
   className = VehicleTurret;
   catagory       = "Turrets";
   shapeFile = "turret_base_large.dts";
   preload        = true;

   mass           = 1.0;  // Not really relevant

   maxEnergy               = 1;
   maxDamage               = StormSeigeDrone.maxDamage;
   destroyedLevel          = StormSeigeDrone.destroyedLevel;
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

   targetNameTag = 'Stormsiege';
   targetTypeTag = 'Turret';
};

datablock ShapeBaseImageData(UltrBossImage)
{
    shapeFile = "vehicle_air_scout.dts";
    offset = "0 .5 -.5";
};

function StormSeigeDrone::deleteAllMounted(%data, %obj)
{
   %turret = %obj.turretObject;
   if (!%turret)
      return;

   %turret.schedule(1000, delete);
   
   %turret2 = %obj.turretObject2;
   if (!%turret2)
      return;

   %turret2.schedule(1000, delete);
}

function MountStormSBarrel(%turret) {
%turret.mountImage("AABarrelLarge", 0);
}

function StormSeigeDrone::playerMounted(%data, %obj, %player, %node)
{
   %ammoAmt = %player.inv[MissileLauncherAmmo];
   if(%ammoAmt)
     %obj.incInventory(MissileLauncherAmmo, %ammoAmt);

   %ammoAmt = %player.inv[MortarAmmo];
   if(%ammoAmt)
     %obj.incInventory(MortarAmmo, %ammoAmt);

   %ammoAmt = %player.inv[chaingunAmmo];
   if(%ammoAmt)
     %obj.incInventory(chaingunAmmo, %ammoAmt);

   bottomPrint(%player.client, "StormSeige - CG, Missiles, Automated Turret", 5, 2 );


   commandToClient(%player.client, 'setHudMode', 'Pilot', "Shrike2", %node);
   %obj.selectedWeapon = 1;
   $numVWeapons = 3;
   commandToClient(%player.client, 'SetWeaponryVehicleKeys', true);
   if( %player.client.observeCount > 0 )
      resetObserveFollow( %player.client, false );
}

function StormSeigeDrone::onAdd(%this, %obj)
{
   Parent::onAdd(%this, %obj);
   if (%obj.clientControl)
       serverCmdResetControlObject(%obj.clientControl);

   setTargetSensorGroup(%obj.getTarget(),%obj.team);
   %turret = TurretData::create(ssTurret);
   %turret.selectedWeapon = 1;
   MissionCleanup.add(%turret);
   %turret.team = %obj.team;
   %turret.setSelfPowered();
   %obj.mountObject(%turret, 10);
   %turret.setCapacitorRechargeRate(999);
   %turret.mountobj = %obj;
   %obj.turretObject = %turret;
   %turret.team = %obj.team;
   %turret.base = %obj;
   setTargetSensorGroup(%turret.getTarget(),%turret.team);
   MountStormSBarrel(%turret); //mount the barrel
   %turret.setAutoFire(true); //YES

   // 2nd turret
   %turret2 = TurretData::create(ssTurret);
   %turret2.selectedWeapon = 1;
   MissionCleanup.add(%turret2);
   %turret2.team = %obj.team;
   %turret2.setSelfPowered();
   %obj.mountObject(%turret2, 7);
   %turret2.setCapacitorRechargeRate(999);
   %turret2.mountobj = %obj;
   %obj.turretObject2 = %turret2;
   %turret2.team = %obj.team;
   %turret2.base = %obj;
   setTargetSensorGroup(%turret2.getTarget(),%turret2.team);
   MountStormSBarrel(%turret2); //mount the barrel
   %turret2.setAutoFire(true); //YES

   %obj.mountImage(ScoutChaingunParam, 0);
   %obj.mountImage(ScoutChaingunImage, 2);
   %obj.mountImage(ScoutChaingunPairImage, 3);
   %obj.mountImage(ShrikeMissileImage, 4);
   %obj.mountImage(ShrikebombImage, 5);
   %obj.shrike = %obj.mountImage(UltrBossImage, 6);  //<testing
   %obj.selectedWeapon = 1;
   %obj.nextWeaponFire = 2;
   %obj.setInventory(MissileLauncherAmmo, 100);
   %obj.setInventory(MortarAmmo, 100);
   %obj.setInventory(chaingunammo, 1500);
   %obj.schedule(5500, "playThread", $ActivateThread, "activate");
}

function StormSeigeDrone::onTrigger(%data, %obj, %trigger, %state)
{
   %player = %obj.getMountNodeObject(0);
   if(%trigger == 0)
   {
      switch (%state)
	{
         case 0:
            %obj.fireWeapon = false;
            %obj.setImageTrigger(2, false);
            %obj.setImageTrigger(3, false);
            %obj.setImageTrigger(4, false);
            %obj.setImageTrigger(5, false);
         case 1:
            %obj.fireWeapon = true;
            if(%obj.selectedWeapon == 1)
		{
               if(%obj.nextWeaponFire == 2)
		   {
                  %obj.setImageTrigger(2, true);
                  %obj.setImageTrigger(3, false);
               }
               else
		   {
                  %obj.setImageTrigger(2, false);
                  %obj.setImageTrigger(3, true);
               }
      	}
            else if(%obj.selectedWeapon == 2)
            {
            %obj.setImageTrigger(2, false);
            %obj.setImageTrigger(3, false);
            %obj.setImageTrigger(4, true);
            %obj.setImageTrigger(5, false);
            }
            else
            {
            %obj.setImageTrigger(2, false);
            %obj.setImageTrigger(3, false);
            %obj.setImageTrigger(4, false);
            %obj.setImageTrigger(5, true);
            }
      }
   }
}

function StormSeigeDrone::playerDismounted(%data, %obj, %player)
{
   %obj.fireWeapon = false;
   %obj.setImageTrigger(2, false);
   %obj.setImageTrigger(3, false);
   %obj.setImageTrigger(4, false);
   %obj.setImageTrigger(5, false);
   setTargetSensorGroup(%obj.getTarget(), %obj.team);
   if( %player.client.observeCount > 0 )
      resetObserveFollow( %player.client, true );
}

function ssTurret::onAdd(%this, %obj)
{
   Parent::onAdd(%this, %obj);
   setTargetSensorGroup(%obj.target, %obj.team);
   //setTargetNeverVisMask(%obj.target, 0xffffffff);
}

function ssTurret::onDamage(%data, %obj)
{
   %newDamageVal = %obj.getDamageLevel();
   if(%obj.lastDamageVal !$= "")
      if(isObject(%obj.getObjectMount()) && %obj.lastDamageVal > %newDamageVal)
         %obj.getObjectMount().setDamageLevel(%newDamageVal);
   %obj.lastDamageVal = %newDamageVal;
}

function ssTurret::damageObject(%this, %targetObject, %sourceObject, %position, %amount, %damageType ,%vec, %client, %projectile)
{
   //If vehicle turret is hit then apply damage to the vehicle
   %vehicle = %targetObject.getObjectMount();
   if(%vehicle)
      %vehicle.getDataBlock().damageObject(%vehicle, %sourceObject, %position, %amount, %damageType, %vec, %client, %projectile);
}
