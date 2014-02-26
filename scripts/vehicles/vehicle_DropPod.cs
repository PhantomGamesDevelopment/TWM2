datablock WheeledVehicleData(DropPod) : MPBDamageProfile {
   spawnOffset = "0 0 1.0";
   renderWhenDestroyed = false;
   canControl = true;
   catagory = "Vehicles";
   shapeFile = "stackable2l.dts";
   multipassenger = false;
   computeCRC = true;

   debrisShapeName = "stackable2l.dts";
   debris = ShapeDebris;

   drag = 0.0;
   density = 20.0;

   mountPose[0] = sitting;
   numMountPoints = 1;
   isProtectedMountPoint[0] = true;

   cameraMaxDist = 20;
   cameraOffset = 6;
   cameraLag = 1.5;
   explosion = HGVehicleExplosion;
   explosionDamage = 0.25;
   explosionRadius = 5.0;

   maxSteeringAngle = 0.3;  // 20 deg.

   // Used to test if the station can deploy
   stationPoints[1] = "-2.3 -7.38703 -0.65";
   stationPoints[2] = "-2.3 -11.8 -0.65";
   stationPoints[3] = "0 -7.38703 -0.65";
   stationPoints[4] = "0 -11.8 -0.65";
   stationPoints[5] = "2.3 -7.38703 -0.65";
   stationPoints[6] = "2.3 -11.8 -0.65";

   // Rigid Body
   mass = 1000;
   bodyFriction = 0.8;
   bodyRestitution = 0.5;
   minRollSpeed = 3;
   gyroForce = 400;
   gyroDamping = 0.3;
   stabilizerForce = 10;
   minDrag = 10;
   softImpactSpeed = 15;       // Play SoftImpact Sound
   hardImpactSpeed = 25;      // Play HardImpact Sound

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 24;
   speedDamageScale = 0.025;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 15;
   collDamageMultiplier   = 0.03;

   // Engine
   engineTorque = 0 * 745;
   breakTorque = 0 * 745;
   maxWheelSpeed = 10;

   // Springs
   springForce = 8000;
   springDamping = 1300;
   antiSwayForce = 6000;
   staticLoadScale = 2;

   // Tires
   tireRadius = 1.6;
   tireFriction = 10.0;
   tireRestitution = 0.5;
   tireLateralForce = 3000;
   tireLateralDamping = 400;
   tireLateralRelaxation = 1;
   tireLongitudinalForce = 12000;
   tireLongitudinalDamping = 600;
   tireLongitudinalRelaxation = 1;
   tireEmitter = TireEmitter;

   //
   maxDamage = 2.0;
   destroyedLevel = 2.0;

   isShielded = false;
   energyPerDamagePoint = 125;
   maxEnergy = 600;
   jetForce = 0;
   minJetEnergy = 60;
   jetEnergyDrain = 0;
   rechargeRate = 1.0;

   jetSound = MPBThrustSound;
   engineSound = MPBEngineSound;
   squeelSound = AssaultVehicleSkid;
   softImpactSound = GravSoftImpactSound;
   hardImpactSound = HardImpactSound;
   //wheelImpactSound = WheelImpactSound;

   //
   softSplashSoundVelocity = 5.0;
   mediumSplashSoundVelocity = 8.0;
   hardSplashSoundVelocity = 12.0;
   exitSplashSoundVelocity = 8.0;

   exitingWater      = VehicleExitWaterSoftSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterMediumSound;
   impactWaterHard   = VehicleImpactWaterHardSound;
   waterWakeSound    = VehicleWakeMediumSplashSound;

   minMountDist = 3;

   damageEmitter[0] = SmallLightDamageSmoke;
   damageEmitter[1] = SmallHeavyDamageSmoke;
   damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "3.0 0.5 0.0 ";
   damageEmitterOffset[1] = "-3.0 0.5 0.0 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 2;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDGroundMPBIcon;
   cmdMiniIconName = "commander/MiniIcons/com_mpb_grey";
   targetNameTag = 'Drop';
   targetTypeTag = 'Pod';
   sensorData = VehiclePulseSensor;

   observeParameters = "1 12 12";

   shieldEffectScale = "0.85 1.2 0.7";
};

function MakeDropPod(%frd, %right, %plyr, %src, %i) {
   if(!isObject(%src)) {
      return;
   }
   if(!%plyr.isAlive()) {
      return;
   }
   if(!%plyr.client.jumpDownYet) {
      %plyr.client.jumpDownYet = 1;
   }
   %vec = vectorAdd(vectorScale(%right,getWord($PodPos[%i],0)),vectorScale(%frd,getWord($PodPos[%i],1)));
   %posa = vectorAdd(%src.getPosition(),%vec);
   %pos = vectorAdd(%posa, "0 0 -16");

   %pod = new WheeledVehicle() {
      dataBlock    = DropPod;
      position     = %pos;
      rotation     = "0 0 1 0";
      team         = %src.team;
   };
   MissionCleanUp.add(%pod);
   
   %plyr.unmount();
   %pod.mountObject(%plyr,0);
   
   %pod.flying = 1;
   %plyr.setMoveState(true); //ha! no running now
   PodScanLoop(%pod, %plyr);
   
   %plyr.schedule(1, "setInvinc", true);
   
   return %pod;
}

function DropPod::playerMounted(%data, %obj, %player, %node) {
   bottomPrint(%player.client, "Standby for pod launch... HOLD ON TROOPER \n It's About to get real bumpy!!!", 5, 2 );
   if( %player.client.observeCount > 0 )
      resetObserveFollow( %player.client, false );
}

function PodScanLoop(%pod, %player) {
   if(!isObject(%pod)) {
      if(%pod.flying) {
         //mid air = die
         if(%player.isAlive()) {
            %plyr.schedule(1, "setInvinc", false);
            %player.schedule(5, "scriptKill", 0);
            return;
         }
         return;
      }
      else {
         return;
      }
   }
   %podPos = %pod.getPosition();
   %ZGrd = GetTerrainHeight(%podPos);
   %landPos = ""@getWord(%podPos, 0)@" "@getWord(%podPos, 1)@" "@%ZGrd@"";
   //time to land?
   if(vectorDist(%podPos, %landPos) < 50) {
      // Lets blow up anything blocking the ground
      %c4 = new Item() {
         datablock = C4Deployed;
         position = %landPos;
         scale = ".1 .1 .1";
      };
      MissionCleanup.add(%c4);
      schedule(100, 0, "C4GoBoom", %c4);
      //
      %pod.setVelocity("0 0 3");
      %player.schedule(1250, "setInvinc", false);
      %player.unmount();
      %pod.flying = 0;
      //pod explodes...
      %pod.damage(0, %pod.GetPosition(), 99999, $DamageType::Explosion);
      %player.setMoveState(false); //ha! no running now
      %player.applyRepair(999);
      messageClient(%player.client, 'CloseHud', "", "vehicleHud");
   }
   schedule(250, 0, "PodScanLoop", %pod, %player);
}

function Player::setInvinc(%player, %yeh) {
   %player.IsinvincibleC = %yeh;
}


//AMMO PODS
datablock WheeledVehicleData(AmmoPod) : MPBDamageProfile {
   spawnOffset = "0 0 1.0";
   renderWhenDestroyed = false;
   canControl = true;
   catagory = "Vehicles";
   shapeFile = "stackable2l.dts";
   multipassenger = false;
   computeCRC = true;

   debrisShapeName = "stackable2l.dts";
   debris = ShapeDebris;

   drag = 0.0;
   density = 20.0;

   mountPose[0] = sitting;
   numMountPoints = 1;
   isProtectedMountPoint[0] = true;

   cameraMaxDist = 20;
   cameraOffset = 6;
   cameraLag = 1.5;
   explosion = HGVehicleExplosion;
   explosionDamage = 0.25;
   explosionRadius = 5.0;

   maxSteeringAngle = 0.3;  // 20 deg.

   // Used to test if the station can deploy
   stationPoints[1] = "-2.3 -7.38703 -0.65";
   stationPoints[2] = "-2.3 -11.8 -0.65";
   stationPoints[3] = "0 -7.38703 -0.65";
   stationPoints[4] = "0 -11.8 -0.65";
   stationPoints[5] = "2.3 -7.38703 -0.65";
   stationPoints[6] = "2.3 -11.8 -0.65";

   // Rigid Body
   mass = 1000;
   bodyFriction = 0.8;
   bodyRestitution = 0.5;
   minRollSpeed = 3;
   gyroForce = 400;
   gyroDamping = 0.3;
   stabilizerForce = 10;
   minDrag = 10;
   softImpactSpeed = 15;       // Play SoftImpact Sound
   hardImpactSpeed = 25;      // Play HardImpact Sound

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 24;
   speedDamageScale = 0.025;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 15;
   collDamageMultiplier   = 0.03;

   // Engine
   engineTorque = 0 * 745;
   breakTorque = 0 * 745;
   maxWheelSpeed = 10;

   // Springs
   springForce = 8000;
   springDamping = 1300;
   antiSwayForce = 6000;
   staticLoadScale = 2;

   // Tires
   tireRadius = 1.6;
   tireFriction = 10.0;
   tireRestitution = 0.5;
   tireLateralForce = 3000;
   tireLateralDamping = 400;
   tireLateralRelaxation = 1;
   tireLongitudinalForce = 12000;
   tireLongitudinalDamping = 600;
   tireLongitudinalRelaxation = 1;
   tireEmitter = TireEmitter;

   //
   maxDamage = 2.0;
   destroyedLevel = 2.0;

   isShielded = false;
   energyPerDamagePoint = 125;
   maxEnergy = 600;
   jetForce = 0;
   minJetEnergy = 60;
   jetEnergyDrain = 0;
   rechargeRate = 1.0;

   jetSound = MPBThrustSound;
   engineSound = MPBEngineSound;
   squeelSound = AssaultVehicleSkid;
   softImpactSound = GravSoftImpactSound;
   hardImpactSound = HardImpactSound;
   //wheelImpactSound = WheelImpactSound;

   //
   softSplashSoundVelocity = 5.0;
   mediumSplashSoundVelocity = 8.0;
   hardSplashSoundVelocity = 12.0;
   exitSplashSoundVelocity = 8.0;

   exitingWater      = VehicleExitWaterSoftSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterMediumSound;
   impactWaterHard   = VehicleImpactWaterHardSound;
   waterWakeSound    = VehicleWakeMediumSplashSound;

   minMountDist = 3;

   damageEmitter[0] = SmallLightDamageSmoke;
   damageEmitter[1] = SmallHeavyDamageSmoke;
   damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "3.0 0.5 0.0 ";
   damageEmitterOffset[1] = "-3.0 0.5 0.0 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 2;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDGroundMPBIcon;
   cmdMiniIconName = "commander/MiniIcons/com_mpb_grey";
   targetNameTag = 'Ammunition';
   targetTypeTag = 'Pod';
   sensorData = VehiclePulseSensor;

   observeParameters = "1 12 12";

   shieldEffectScale = "0.85 1.2 0.7";
};

function MakeAmmoPod(%frd, %right, %src, %i) {
   if(!isObject(%src)) {
      return;
   }
   %vec = vectorAdd(vectorScale(%right,getWord($PodPos[%i],0)),vectorScale(%frd,getWord($PodPos[%i],1)));
   %posa = vectorAdd(%src.getPosition(),%vec);
   %pos = vectorAdd(%posa, "0 0 -16");

   %pod = new WheeledVehicle() {
      dataBlock    = AmmoPod;
      position     = %pos;
      rotation     = "0 0 1 0";
      team         = %src.team;
   };
   MissionCleanUp.add(%pod);

   %pod.flying = 1;
   AmmoPodScanLoop(%pod);

   return %pod;
}

function AmmoPodScanLoop(%pod) {
   if(!isObject(%pod)) {
      if(%pod.flying) {
         //mid air = die
         return;
      }
      else {
         return;
      }
   }
   %podPos = %pod.getPosition();
   %ZGrd = GetTerrainHeight(%podPos);
   %landPos = ""@getWord(%podPos, 0)@" "@getWord(%podPos, 1)@" "@%ZGrd@"";
   //time to land?
   if(vectorDist(%podPos, %landPos) < 70) {
      echo("ammo pod land");
      %pod.setVelocity("0 0 0");
      %pod.flying = 0;
      //pod explodes...
      %pod.damage(0, %pod.GetPosition(), 99999, $DamageType::Explosion);
      //Drop Contents
      %gunNum = getRandom(1,10);
      switch(%gunNum) {
         //colt
         case 1:
            WeaponDrop(%pod.GetPosition(), "pistol");
         //S3
         case 2:
            WeaponDrop(%pod.GetPosition(), "S3Rifle");
         //Mp26
         case 3:
            WeaponDrop(%pod.GetPosition(), "Mp26");
         //Pg700
         case 4:
            WeaponDrop(%pod.GetPosition(), "Pg700");
         //RPG
         case 5:
            WeaponDrop(%pod.GetPosition(), "RPG");
         //Flamer
         case 6:
            WeaponDrop(%pod.GetPosition(), "flamer");
         //wp400
         case 7:
            WeaponDrop(%pod.GetPosition(), "WP400");
         //m1700
         case 8:
            WeaponDrop(%pod.GetPosition(), "M1700");
         //miniCG
         case 9:
            WeaponDrop(%pod.GetPosition(), "MiniChaingun");
         //Rep Kits
         case 10:
            for(%i = 0; %i < 4; %i++) {
               %blockNum = getRandom(1,2);
               if(%blockNum == 1) {
                  %block = RepairKit;
               }
               else {
                  %block = RepairPatch;
               }
               %reps = new Item() {
                  datablock = %block;
                  static = false;
                  rotate = false;
                  position = %pod.getPosition();
               };
               missionCleanup.add(%reps);
            }
      }
   }
   schedule(250, 0, "AmmoPodScanLoop", %pod);
}
