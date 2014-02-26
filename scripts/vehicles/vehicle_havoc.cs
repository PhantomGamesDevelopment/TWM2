//**************************************************************
// HAVOC HEAVY TRANSPORT FLIER
//**************************************************************
//**************************************************************
// SOUNDS
//**************************************************************
datablock AudioProfile(HAPCFlyerEngineSound)
{
   filename    = "fx/vehicles/htransport_thrust.wav";
   description = AudioDefaultLooping3d;
};

datablock AudioProfile(HAPCFlyerThrustSound)
{
   filename    = "fx/vehicles/htransport_boost.wav";
   description = AudioDefaultLooping3d;
};

//**************************************************************
// VEHICLE CHARACTERISTICS
//**************************************************************

datablock FlyingVehicleData(HAPCFlyer) : HavocDamageProfile
{
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
//   mountPose[1] = sitting;
   numMountPoints = 6;
   isProtectedMountPoint[0] = true;
   isProtectedMountPoint[1] = true;
   isProtectedMountPoint[2] = true;
   isProtectedMountPoint[3] = true;
   isProtectedMountPoint[4] = true;
   isProtectedMountPoint[5] = true;
   canControl = true;
   cameraMaxDist = 17;
   cameraOffset = 2;
   cameraLag = 8.5;
   explosion = LargeAirVehicleExplosion;
	explosionDamage = 0.5;
	explosionRadius = 5.0;

   maxDamage = 3.50;
   destroyedLevel = 3.50;

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
   maxSteeringAngle = 8;
   horizontalSurfaceForce = 10;  // Horizontal center "wing"
   verticalSurfaceForce = 10;    // Vertical center "wing"
   maneuveringForce = 6000;      // Horizontal jets
   steeringForce = 1000;          // Steering jets
   steeringRollForce = 400;      // Steering jets
   rollForce = 12;               // Auto-roll
   hoverHeight = 5.6;         // Height off the ground at rest
   createHoverHeight = 6;   // Height off the ground when created
   maxForwardSpeed = 71;  // speed in which forward thrust force is no longer applied (meters/second)

   // Turbo Jet
   jetForce = 5000;
   minJetEnergy = 55;
   jetEnergyDrain = 3.6;
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
   mass = 550;
   bodyFriction = 0;
   bodyRestitution = 0.3;
   minRollSpeed = 0;
   softImpactSpeed = 12;       // Sound hooks. This is the soft hit.
   hardImpactSpeed = 15;    // Sound hooks. This is the hard hit.

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
   targetNameTag = 'Havoc';
   targetTypeTag = 'Heavy Transport';
   sensorData = VehiclePulseSensor;

   checkRadius = 7.8115;
   observeParameters = "1 15 15";

   stuckTimerTicks = 32;   // If the hapc spends more than 1 sec in contact with something
   stuckTimerAngle = 80;   //  with a > 80 deg. pitch, BOOM!

   shieldEffectScale = "1.0 0.9375 0.45";
};

function HAPCFlyer::hasDismountOverrides(%data, %obj)
{
   return true;
}

function HAPCFlyer::getDismountOverride(%data, %obj, %mounted)
{
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

function HAPCFlyer::onTrigger(%data, %obj, %trigger, %state) {
   %up = %obj.getUpVector();
   if(%state == 1 && %trigger == 5 && vectorDist(%up,"0 0 1") <= 0.3){
	  %frd = %obj.getForwardVector();
	  %right = vectorNormalize(vectorSub(%obj.getEdge("1 0 0"),%obj.getEdge("-1 0 0")));
	  for(%i = 2; %i < 6; %i++){
	     if(%obj.getMountNodeObject(%i)){
		    %plyr = %obj.getMountNodeObject(%i);
            %plyr.setMoveState(true); //ha! no running now
		    %pod = schedule((2000 * %i), 0, "MakeDropPod", %frd, %right, %plyr, %obj, %i);
            BottomPrint(%plyr.client, "Helljumpers!!! Prepare for drop!", 3, 1);
	     }
      }
   }
}

$PodPos[2] = "-3 7";
$PodPos[3] = "-3 3";
$PodPos[4] = "3 3";
$PodPos[5] = "3 7";







//Hunter Dropship
//1 Pilot
//4 Riders
//1 Rapid Pulse Cannon
datablock FlyingVehicleData(HunterDropship) : HAPCFlyer {
   maxDamage = 10.0;
   destroyedLevel = 10.0;

   isShielded = true;
   rechargeRate = 0.8;
   energyPerDamagePoint = 50;
   maxEnergy = 1200;
   minDrag = 100;                // Linear Drag
   rotationalDrag = 2700;        // Anguler Drag

   // Turbo Jet
   jetForce = 10000;
   minJetEnergy = 55;
   jetEnergyDrain = 3.6;
   vertThrustMultiple = 3.0;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingHAPCIcon;
   cmdMiniIconName = "commander/MiniIcons/com_hapc_grey";
   targetNameTag = 'Hunter';
   targetTypeTag = 'Dropship';
   sensorData = VehiclePulseSensor;

   shieldEffectScale = "1.0 0.9375 0.45";
};

datablock TurretData(HunterDefenseCannon) : TurretDamageProfile {
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

   targetNameTag = 'Hunter Defense';
   targetTypeTag = 'Cannon';
};

datablock TurretImageData(HunterPBL)
{
   shapeFile = "turret_fusion_large.dts";
   // ---------------------------------------------
   // z0dd - ZOD, 5/8/02. Incorrect parameter value
   //item      = PlasmaBarrelLargePack;
   item = PlasmaBarrelPack;

   projectile = PlasmaBarrelBolt;
   projectileType = LinearFlareProjectile;
   usesEnergy = true;
   fireEnergy = 3;
   minEnergy = 3;
   emap = true;

   // Turret parameters
   activationMS      = 1000;
   deactivateDelayMS = 1500;
   thinkTimeMS       = 200;
   degPerSecTheta    = 300;
   degPerSecPhi      = 500;
   attackRadius      = 500;

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
   stateTransitionOnTriggerDown[2] = "Fire";
   stateTransitionOnNoAmmo[2]      = "NoAmmo";

   stateName[3]                = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3]        = 0.3;
   stateFire[3]                = true;
   stateRecoil[3]              = LightRecoil;
   stateAllowImageChange[3]    = false;
   stateSequence[3]            = "Fire";
   stateSound[3]               = PBLFireSound;
   stateScript[3]              = "onFire";

   stateName[4]                  = "Reload";
   stateTimeoutValue[4]          = 0.1;
   stateAllowImageChange[4]      = false;
   stateSequence[4]              = "Reload";
   stateTransitionOnTimeout[4]   = "Ready";
   stateTransitionOnNotLoaded[4] = "Deactivate";
   stateTransitionOnNoAmmo[4]    = "NoAmmo";

   stateName[5]                = "Deactivate";
   stateSequence[5]            = "Activate";
   stateDirection[5]           = false;
   stateTimeoutValue[5]        = 1;
   stateTransitionOnLoaded[5]  = "ActivateReady";
   stateTransitionOnTimeout[5] = "Dead";

   stateName[6]               = "Dead";
   stateTransitionOnLoaded[6] = "ActivateReady";

   stateName[7]             = "NoAmmo";
   stateTransitionOnAmmo[7] = "Reload";
   stateSequence[7]         = "NoAmmo";
};

function HunterDropship::onAdd(%this, %obj) {
   Parent::onAdd(%this, %obj);
   if (%obj.clientControl)
       serverCmdResetControlObject(%obj.clientControl);

   setTargetSensorGroup(%obj.getTarget(),%obj.team);

   %turret5 = TurretData::create(HunterDefenseCannon);
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
   %turret5.mountImage("HunterPBL", 0);
   %turret5.setAutoFire(true); //YES

   %obj.schedule(5500, "playThread", $ActivateThread, "activate");
}

function HunterDropship::deleteAllMounted(%data, %obj) {
   %turret5 = %obj.turretObject5;
   if (!%turret5)
      return;
   %turret5.schedule(1000, delete);
   
   //
   %obj.getMountNodeObject(0).scriptKill(); //remove the wraith now.
   for(%i = 2; %i < 6; %i++){
      if(%obj.getMountNodeObject(%i)){
         %plyr = %obj.getMountNodeObject(%i);
         %plyr.scriptKill();
      }
   }
}

function HunterDefenseCannon::selectTarget(%this, %turret) {
    %turretTarg = %turret.getTarget();
	if(%turretTarg == -1)
		return;

	// if the turret isn't on a team, don't fire at anyone
	if(getTargetSensorGroup(%turretTarg) == 0) {
		%turret.clearTarget();
		return;
	}

	// stop firing if turret is disabled or if it needs power and isn't powered
	if((!%turret.isPowered()) && (!%turret.needsNoPower)) {
		%turret.clearTarget();
		return;
	}

	%TargetSearchMask = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::StationObjectType | $TypeMasks::GeneratorObjectType |
	$TypeMasks::SensorObjectType | $TypeMasks::TurretObjectType; //$TypeMasks::StaticObjectType;

	InitContainerRadiusSearch(%turret.getMuzzlePoint(0),
				%turret.getMountedImage(0).attackRadius,
				%TargetSearchMask);

	// TODO - clean up this mess + GameBaseData::hasLOS()

	while ((%potentialTarget = ContainerSearchNext()) != 0) {
		if (%potentialtarget) {
			%potTargTarg = %potentialTarget.getTarget();
			if (%turret.isValidTarget(%potentialTarget)
			&& (getTargetSensorGroup(%turretTarg) != getTargetSensorGroup(%potTargTarg))
			&& (getTargetSensorGroup(%potTargTarg) != 0)
			&& ((%potentialTarget.getType() & $TypeMasks::PlayerObjectType) || !$TurretOnlyTargetPlayers)
			&& %this.hasLOS(%turret,0,%potentialTarget)) {
				if (%potentialTarget.homingCount > 0 && !%secondTarg) {
					if (!%firstTarg)
						%firstTarg = %potentialTarget;
					else
						%secondTarg = %potentialTarget;
				}
				else {
					%turret.setTargetObject(%potentialTarget);
					%turret.aquireTime = getSimTime();
					return;
				}
			}
		}
	}
	if (%secondTarg) {
		%turret.setTargetObject(%firstTarg);
		%turret.aquireTime = getSimTime();
		return;
	}
	if (%firstTarg) {
		%turret.setTargetObject(%firstTarg);
		%turret.aquireTime = getSimTime();
		return;
	}
}


//Creation functions, and AI of the dropship
//%position - where to spawn the dropship
//%dropPosition - where to drop, or "AA" to automatically determine
//%dropType - type of passengers, by mount seat
// * so: "1 1 1 1" would be four normal zombies, ect.
function spawnHunterDropship(%position, %dropPosition, %dropType) {
   //spawn dropship
   %drop = new FlyingVehicle() {
       dataBlock  = "HunterDropship";
       position = %position;
       respawn    = "0";
       teamBought = 30;
       team = 30;
   };
   MissionCleanup.add(%drop);
   //attach waypoint, spawn pilot/passengers
   %wraith = StartAZombie(vectorAdd(%position, "0 0 100"), 15);
   %drop.mountObject(%wraith, 0);
   for(%i = 0; %i < getWordCount(%dropType); %i++) {
      %z = StartAZombie(vectorAdd(%position, "0 0 100"), getWord(%dropType, %i));
      if(isObject(%z)) {
         %drop.mountObject(%z, %i+2);
         //%z.mountObject(%drop, %i+2);
      }
   }
   dropshipMarkerLoop(%drop);
   //engage dropship AI
   BeginDropshipAI(%drop, %dropPosition);
}

function dropshipMarkerLoop(%obj) {
   if(!isObject(%obj)) {
      return;
   }
   %obj.dm = new WayPoint() {
      position = %obj.getPosition();
      dataBlock = "WayPointMarker";
      team = 30;
      name = "Hunter Dropship";
   };
   MissionCleanup.add(%obj.dm);
   %obj.dm.schedule(1000, "Delete");
   schedule(1000, 0, "dropshipMarkerLoop", %obj);
}

function BeginDropshipAI(%drop, %dropPosition) {
   if(%dropPosition $= "AA") {
      DropshipAquireMove(%drop);
   }
   else {
      DropshipAssaultMove(%drop, %dropPosition);
   }
}

function DropshipAquireMove(%drop) {
   if(!isObject(%drop)) {
      return;
   }
   //
   %closestD = 99999;
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %client = ClientGroup.getObject(%i);
      if(%client.player.isAlive()) {
         %d = vectorDist(%client.player.getPosition(), %drop.getPosition());
         if(%d < %closestD) {
            %closestD = %d;
            %closestPos = %client.player.getPosition();
         }
      }
   }
   //
   if(isSet(%closestPos)) {
      DropshipAssaultMove(%drop, %closestPos);
   }
   else {
      schedule(500, 0, "DropshipAquireMove", %drop);
   }
}

//Movment functions
function AssaultMove(%type) {
   %typef = %type SPC %type SPC %type SPC %type;
   spawnHunterDropship("2000 0 400", "100 0 110", %typef);
   schedule(2500, 0, "spawnHunterDropship", "-2000 0 400", "-100 0 110", %typef);
   schedule(5000, 0, "spawnHunterDropship", "0 2000 400", "0 100 110", %typef);
   schedule(7500, 0, "spawnHunterDropship", "0 -2000 400", "0 -100 110", %typef);
}

function DropshipAssaultMove(%object, %target) {
   if(!isObject(%object)) {
      return;
   }
   %landZ = getTerrainHeight(%target) + 10; //10M off ground of target
   %landPosition = getWords(%target, 0, 1) SPC %landZ;
   //
   if(vectorDist(%object.getPosition(), getWords(%landPosition, 0, 1) SPC getWord(%object.getPosition(), 2)) > 300) {
      if(vectorLen(%object.getVelocity()) < 500) {
         %object.applyImpulse(%object.getPosition(),vectorScale(%object.getForwardVector(), 1000 * 1.3));
         DropshipAssaultVector(%object, %landPosition);
      }
   }
   else {
      //slow and forward drop momentum...
      if(vectorLen(%object.getVelocity()) < 100) {
         %object.applyImpulse(%object.getPosition(),vectorScale(getWords(%object.getForwardVector(), 0, 1) SPC -5, 100));
      }
      DropShipLand(%object);
      return;
   }
   schedule(200, 0, "DropshipAssaultMove", %object, %target);
}

function DropShipLand(%drop) {
   if(!isObject(%drop)) {
      return;
   }
   %dropP = %drop.getPosition();
   %tH = getTerrainHeight(%dropP) + 10;
   %land = getWords(%dropP, 0, 1) SPC %tH;
   if(getWord(%dropP, 2) > %th) {
      if(vectorLen(%drop.getVelocity()) < 100) {
         %drop.applyImpulse(%drop.getPosition(),vectorScale("0 0 -3", 300));
      }
   }
   else {
      //landing time, schedule the land/take off
      DropShipFinal(%drop);
      return;
   }
   //
   schedule(200, 0, "DropShipLand", %drop);
}

function DropShipFinal(%drop, %go) {
   if(!isSet(%go)) {
      %go = 0;
   }
   //
   if(!isObject(%drop)) {
      return;
   }
   //
   if(!%go) {
      schedule(15000, 0, "DropShipFinal", %drop, 1);
      for(%i = 2; %i < 6; %i++){
         if(%drop.getMountNodeObject(%i)){
            %plyr = %drop.getMountNodeObject(%i);
            %plyr.getDatablock().doDismount(%plyr, 1, 5);
         }
      }
      %drop.applyImpulse(%drop.getPosition(),vectorScale("0 0 5", 500));
   }
   else {
      DropshipLeave(%drop);
      %drop.schedule(20000, "delete");
   }
}

function DropshipLeave(%drop) {
   if(!isObject(%drop)) {
      return;
   }
   if(vectorLen(%drop.getVelocity()) < 500) {
      %drop.applyImpulse(%drop.getPosition(),vectorScale(vectorAdd(%drop.getForwardVector(), "0 0 1"), 1000));
   }
   //
   schedule(200, 0, "DropshipLeave", %drop);
}

function DropshipAssaultVector(%trans, %targPos) {
   if(!isObject(%trans)) {
      return;
   }
   %pos = %trans.getworldboxcenter();
   %vector = vectorNormalize(vectorSub(%targPos, %pos));
   %v1 = getword(%vector, 0);
   %v2 = getword(%vector, 1);
   %v3 = getword(%vector, 2);
   %nv1 = %v2;
   %nv2 = (%v1 * -1);
   %vector2 = %nv1@" "@%nv2@" "@%v3;
   %trans.setRotation(fullrot("0 0 0",%vector2));

   return %vector;
}






























//Assault Lander
//Used in helljump, deploys an ammo station...
datablock FlyingVehicleData(AmmoStationDropship) : HAPCFlyer {
   maxDamage = 10.0;
   destroyedLevel = 10.0;

   isShielded = true;
   rechargeRate = 0.8;
   energyPerDamagePoint = 50;
   maxEnergy = 1200;
   minDrag = 100;                // Linear Drag
   rotationalDrag = 2700;        // Anguler Drag

   // Turbo Jet
   jetForce = 10000;
   minJetEnergy = 55;
   jetEnergyDrain = 3.6;
   vertThrustMultiple = 3.0;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingHAPCIcon;
   cmdMiniIconName = "commander/MiniIcons/com_hapc_grey";
   targetNameTag = 'CX995';
   targetTypeTag = 'Assault Lander';
   sensorData = VehiclePulseSensor;

   shieldEffectScale = "1.0 0.9375 0.45";
};

datablock TurretData(AssaultLanderCannon) : TurretDamageProfile {
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

   targetNameTag = 'Assault Lander';
   targetTypeTag = 'Cannon';
};

datablock TurretImageData(AssaultLanderBarrel)
{
   shapeFile = "turret_fusion_large.dts";
   // ---------------------------------------------
   // z0dd - ZOD, 5/8/02. Incorrect parameter value
   //item      = PlasmaBarrelLargePack;
   item = PlasmaBarrelPack;

   projectile = PlasmaBarrelBolt;
   projectileType = LinearFlareProjectile;
   usesEnergy = true;
   fireEnergy = 3;
   minEnergy = 3;
   emap = true;

   // Turret parameters
   activationMS      = 1000;
   deactivateDelayMS = 1500;
   thinkTimeMS       = 200;
   degPerSecTheta    = 300;
   degPerSecPhi      = 500;
   attackRadius      = 500;

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
   stateTransitionOnTriggerDown[2] = "Fire";
   stateTransitionOnNoAmmo[2]      = "NoAmmo";

   stateName[3]                = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3]        = 0.3;
   stateFire[3]                = true;
   stateRecoil[3]              = LightRecoil;
   stateAllowImageChange[3]    = false;
   stateSequence[3]            = "Fire";
   stateSound[3]               = PBLFireSound;
   stateScript[3]              = "onFire";

   stateName[4]                  = "Reload";
   stateTimeoutValue[4]          = 0.1;
   stateAllowImageChange[4]      = false;
   stateSequence[4]              = "Reload";
   stateTransitionOnTimeout[4]   = "Ready";
   stateTransitionOnNotLoaded[4] = "Deactivate";
   stateTransitionOnNoAmmo[4]    = "NoAmmo";

   stateName[5]                = "Deactivate";
   stateSequence[5]            = "Activate";
   stateDirection[5]           = false;
   stateTimeoutValue[5]        = 1;
   stateTransitionOnLoaded[5]  = "ActivateReady";
   stateTransitionOnTimeout[5] = "Dead";

   stateName[6]               = "Dead";
   stateTransitionOnLoaded[6] = "ActivateReady";

   stateName[7]             = "NoAmmo";
   stateTransitionOnAmmo[7] = "Reload";
   stateSequence[7]         = "NoAmmo";
};

function AmmoStationDropship::onAdd(%this, %obj) {
   Parent::onAdd(%this, %obj);
   if (%obj.clientControl)
       serverCmdResetControlObject(%obj.clientControl);

   setTargetSensorGroup(%obj.getTarget(),%obj.team);

   %turret5 = TurretData::create(AssaultLanderCannon);
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
   %turret5.mountImage("AssaultLanderBarrel", 0);
   %turret5.setAutoFire(true); //YES

   %obj.schedule(5500, "playThread", $ActivateThread, "activate");
}

function AmmoStationDropship::deleteAllMounted(%data, %obj) {
   %turret5 = %obj.turretObject5;
   if (!%turret5)
      return;
   %turret5.schedule(1000, delete);

   //
   %obj.getMountNodeObject(0).scriptKill(); //remove the wraith now.
   for(%i = 2; %i < 6; %i++){
      if(%obj.getMountNodeObject(%i)){
         %plyr = %obj.getMountNodeObject(%i);
         %plyr.scriptKill();
      }
   }
}

function AmmoStationDropship::selectTarget(%this, %turret) {
    %turretTarg = %turret.getTarget();
	if(%turretTarg == -1)
		return;

	// if the turret isn't on a team, don't fire at anyone
	if(getTargetSensorGroup(%turretTarg) == 0) {
		%turret.clearTarget();
		return;
	}

	// stop firing if turret is disabled or if it needs power and isn't powered
	if((!%turret.isPowered()) && (!%turret.needsNoPower)) {
		%turret.clearTarget();
		return;
	}

	%TargetSearchMask = $TypeMasks::PlayerObjectType; //$TypeMasks::StaticObjectType;

	InitContainerRadiusSearch(%turret.getMuzzlePoint(0),
				%turret.getMountedImage(0).attackRadius,
				%TargetSearchMask);

	// TODO - clean up this mess + GameBaseData::hasLOS()

	while ((%potentialTarget = ContainerSearchNext()) != 0) {
		if (%potentialtarget && %potentialtarget.isZombie) {
			%potTargTarg = %potentialTarget.getTarget();
			if (%turret.isValidTarget(%potentialTarget)
			&& (getTargetSensorGroup(%turretTarg) != getTargetSensorGroup(%potTargTarg))
			&& (getTargetSensorGroup(%potTargTarg) != 0)
			&& ((%potentialTarget.getType() & $TypeMasks::PlayerObjectType) || !$TurretOnlyTargetPlayers)
			&& %this.hasLOS(%turret,0,%potentialTarget)) {
				if (%potentialTarget.homingCount > 0 && !%secondTarg) {
					if (!%firstTarg)
						%firstTarg = %potentialTarget;
					else
						%secondTarg = %potentialTarget;
				}
				else {
					%turret.setTargetObject(%potentialTarget);
					%turret.aquireTime = getSimTime();
					return;
				}
			}
		}
	}
	if (%secondTarg) {
		%turret.setTargetObject(%firstTarg);
		%turret.aquireTime = getSimTime();
		return;
	}
	if (%firstTarg) {
		%turret.setTargetObject(%firstTarg);
		%turret.aquireTime = getSimTime();
		return;
	}
}


//Creation functions, and AI of the dropship
//%position - where to spawn the dropship
//%dropPosition - where to drop, or "AA" to automatically determine
function spawnAmmoStationDropship(%position, %dropPosition) {
   //spawn dropship
   %drop = new FlyingVehicle() {
       dataBlock  = "AmmoStationDropship";
       position = %position;
       respawn    = "0";
       teamBought = 1;
       team = 1;
   };
   MissionCleanup.add(%drop);
   //engage dropship AI
   BeginAmmoDropshipAI(%drop, %dropPosition);
}


function BeginAmmoDropshipAI(%drop, %dropPosition) {
   if(%dropPosition $= "AA") {
      AmmoDropshipAquireMove(%drop);
   }
   else {
      AmmoDropshipAssaultMove(%drop, %dropPosition);
   }
}

function AmmoDropshipAquireMove(%drop) {
   if(!isObject(%drop)) {
      return;
   }
   //
   %closestD = 99999;
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %client = ClientGroup.getObject(%i);
      if(%client.player.isAlive()) {
         %d = vectorDist(%client.player.getPosition(), %drop.getPosition());
         if(%d < %closestD) {
            %closestD = %d;
            %closestPos = %client.player.getPosition();
         }
      }
   }
   //
   if(isSet(%closestPos)) {
      AmmoDropshipAssaultMove(%drop, %closestPos);
   }
   else {
      schedule(500, 0, "AmmoDropshipAquireMove", %drop);
   }
}

function AmmoDropshipAssaultMove(%object, %target) {
   if(!isObject(%object)) {
      return;
   }
   %landZ = getTerrainHeight(%target) + 10; //10M off ground of target
   %landPosition = getWords(%target, 0, 1) SPC %landZ;
   //
   if(vectorDist(%object.getPosition(), getWords(%landPosition, 0, 1) SPC getWord(%object.getPosition(), 2)) > 300) {
      if(vectorLen(%object.getVelocity()) < 500) {
         %object.applyImpulse(%object.getPosition(),vectorScale(%object.getForwardVector(), 1000 * 1.3));
         AmmoDropshipAssaultVector(%object, %landPosition);
      }
   }
   else {
      //slow and forward drop momentum...
      if(vectorLen(%object.getVelocity()) < 100) {
         %object.applyImpulse(%object.getPosition(),vectorScale(getWords(%object.getForwardVector(), 0, 1) SPC -5, 100));
      }
      AmmoDropShipLand(%object);
      return;
   }
   schedule(200, 0, "AmmoDropshipAssaultMove", %object, %target);
}

function AmmoDropShipLand(%drop) {
   if(!isObject(%drop)) {
      return;
   }
   %dropP = %drop.getPosition();
   %tH = getTerrainHeight(%dropP) + 10;
   %land = getWords(%dropP, 0, 1) SPC %tH;
   if(getWord(%dropP, 2) > %th) {
      if(vectorLen(%drop.getVelocity()) < 100) {
         %drop.applyImpulse(%drop.getPosition(),vectorScale("0 0 -3", 300));
      }
   }
   else {
      //landing time, schedule the land/take off
      AmmoDropShipFinal(%drop);
      return;
   }
   //
   schedule(200, 0, "AmmoDropShipLand", %drop);
}

function AmmoDropShipFinal(%drop, %go) {
   if(!isSet(%go)) {
      %go = 0;
   }
   //
   if(!isObject(%drop)) {
      return;
   }
   //
   if(!%go) {
      schedule(15000, 0, "DropShipFinal", %drop, 1);
      //deploy the station, lock the dropship in place...
      %drop.applyImpulse(%drop.getPosition(),vectorScale("0 0 5", 500));
   }
   else {
      AmmoDropshipLeave(%drop);
      %drop.schedule(20000, "delete");
   }
}

function AmmoDropshipLeave(%drop) {
   if(!isObject(%drop)) {
      return;
   }
   if(vectorLen(%drop.getVelocity()) < 500) {
      %drop.applyImpulse(%drop.getPosition(),vectorScale(vectorAdd(%drop.getForwardVector(), "0 0 1"), 1000));
   }
   //
   schedule(200, 0, "DropshipLeave", %drop);
}

function AmmoDropshipAssaultVector(%trans, %targPos) {
   if(!isObject(%trans)) {
      return;
   }
   %pos = %trans.getworldboxcenter();
   %vector = vectorNormalize(vectorSub(%targPos, %pos));
   %v1 = getword(%vector, 0);
   %v2 = getword(%vector, 1);
   %v3 = getword(%vector, 2);
   %nv1 = %v2;
   %nv2 = (%v1 * -1);
   %vector2 = %nv1@" "@%nv2@" "@%v3;
   %trans.setRotation(fullrot("0 0 0",%vector2));

   return %vector;
}
