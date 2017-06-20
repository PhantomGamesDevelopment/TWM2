//SHADE LORD
datablock ParticleData(ShadeSwordParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.2;
   inheritedVelFactor   = 0.0;

   lifetimeMS           = 800;
   lifetimeVarianceMS   = 500;

   useInvAlpha = false;
   spinRandomMin = -160.0;
   spinRandomMax = 160.0;

   animateTexture = true;
   framesPerSec = 15;

   textureName = "special/cloudflash";

   colors[0] = "46 46 46 1.0";
   colors[1] = "46 46 46 1.0";
   colors[2] = "46 46 46 1.0";

   sizes[0]      = 2.5;
   sizes[1]      = 2.7;
   sizes[2]      = 3.0;

   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ShadeSwordEmitter) {
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.5;
   velocityVariance = 0.3;

   thetaMin         = 0.0;
   thetaMax         = 30.0;

   particles = "ShadeSwordParticle";
};

datablock SeekerProjectileData(ShadeLordSword){
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   hasDamageRadius     = true;
   indirectDamage      = 0.8;
   damageRadius        = 8.0;
   radiusDamageType    = $DamageType::Missile;
   kickBackStrength    = 2000;

   explosion           = "MissileExplosion";
   splash              = MissileSplash;
   velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
                                 // is cranked up to full so the missile doesn't start
                                 // out behind the player when the player is moving
                                 // very quickly - bramage

   delayEmitter        = MissileFireEmitter;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = 30000;
   muzzleVelocity      = 10.0;
   maxVelocity         = 250.0;
   turningSpeed        = 110.0;
   acceleration        = 450.0;

   proximityRadius     = 3;

   terrainAvoidanceSpeed         = 180;
   terrainScanAhead              = 25;
   terrainHeightFail             = 12;
   terrainAvoidanceRadius        = 100;

   flareDistance = 200;
   flareAngle    = 30;

   sound = MissileProjectileSound;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.2 0.05 0";

   useFlechette = true;
   flechetteDelayMs = 550;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = false;

   baseEmitter         = ShadeSwordEmitter;
};

function ShadeLordSword::OnExplode(%data, %proj, %pos, %mod) {
   %source = %proj.SourceObject;
   InitContainerRadiusSearch(%pos, 6, $TypeMasks::PlayerObjectType);
   while ((%potentialTarget = ContainerSearchNext()) != 0) {
      if(%potentialTarget != %source) {
         serverPlay3D(BOVHitSound,%potentialTarget.getPosition());
         MessageAll('msgDeath', "\c0"@%potentialTarget.client.namebase@"'s destiny of death is being fulfilled...");
         //%potentialTarget.blowUp();
         //%potentialTarget.scriptKill();
         %potentialTarget.isGoingToDie = 1;
         %potentialTarget.setMoveState(true);
         %potentialTarget.setActionThread("death1", true);
         createBlood(%potentialTarget);
         //===========================
         schedule(750, 0, createBlood, %potentialTarget);
         schedule(1250, 0, createBlood, %potentialTarget);
         
         schedule(1250, 0, ShadeLordFunction, -1, "DoReturnMissile", %potentialTarget SPC %source);

         schedule(2000, 0, createBlood, %potentialTarget); 
         schedule(3500, 0, createBlood, %potentialTarget);

         %potentialTarget.schedule(4000, "ScriptKill");
      }
   }
   
   //if(isObject(%proj.targetedPlayer) && %proj.targetedPlayer.getState() !$= "dead") {
   //   %proj.targetedPlayer.setMoveState(false); //free to move.
   //}
}

datablock ParticleData(ShadeStormParticle) {
   dragCoefficient      = 1.0;
   gravityCoefficient   = 0.00;
   windcoefficient      = 0.0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 10.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 90.0;
   textureName          = "special/cloudFlash";

   colors[0]     = "46 46 46 0.5";
   colors[1]     = "46 46 46 0.5";
   colors[2]     = "46 46 46 0.0";
   sizes[0]      = 500;
   sizes[1]      = 500;
   sizes[2]      = 500;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ShadeStormEmitter) {
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 400.0;
   velocityVariance = 150.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "ShadeStormParticle";
};

datablock ParticleData(dayCloakSmokeParticles) {
   dragCoefficient = 50;/////////-----------------------
   gravityCoefficient = 0.0;
   inheritedVelFactor = 1.0;
   constantAcceleration = 1.0;
   lifetimeMS = 1000;
   lifetimeVarianceMS = 0;
   useInvAlpha = true;
   spinRandomMin = -360.0;
   spinRandomMax = 360.0;
   textureName = "particleTest";
   colors[0] = "0.1 0.1 0.1 1.0";// ////////////////////
   colors[1] = "0.1 0.1 0.1 1.0";// ////////////////////
   colors[2] = "0.1 0.1 0.1 1.0";// \\\\\\\\\\\\\\\\\\\\
   colors[3] = "0.1 0.1 0.1 1.0";// \\\\\\\\\\\\\\\\\\\\
   sizes[0] = 350.0;
   sizes[1] = 350.0;
   sizes[2] = 350.0;
   sizes[3] = 350.0;
   times[0] = 0.0;
   times[1] = 0.33;
   times[2] = 0.66;
   times[3] = 1.0;
   mass = 0.4;
   elasticity = 0.2;
   friction = 1;
   computeCRC = true;
   haslight = true;
   lightType = "PulsingLight";
   lightColor = "0.2 0.0 0.5 1.0";
   lightTime = "200";
   lightRadius = "2.0";
};

datablock ParticleEmitterData(dayCloakEmitter) {
   ejectionPeriodMS = 50;
   periodVarianceMS = 0;
   ejectionVelocity = 10.0;
   velocityVariance = 0.0;
   ejectionOffset = 2;
   thetaMin = 0;
   thetaMax = 180;
   overrideAdvances = false;
   particles = "dayCloakSmokeParticles";
};

//
datablock ParticleData(ShadeLordScreamParticle) {
   dragCoefficient      = 1.0;
   gravityCoefficient   = 0.00;
   windcoefficient      = 0.0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 10.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 90.0;
   textureName          = "special/cloudFlash";

   colors[0]     = "156 0 0 0.5";
   colors[1]     = "156 0 0 0.5";
   colors[2]     = "156 0 0 0.0";
   sizes[0]      = 500;
   sizes[1]      = 500;
   sizes[2]      = 2;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ShadeLordScreamEmitter) {
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 50.0;
   velocityVariance = 150.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "ShadeLordScreamParticle";
};
//

datablock PlayerData(ShadeLordArmor) : LightMaleHumanArmor {
   boundingBox = "1.63 1.63 2.6";
   maxDamage = 700.0;
   minImpactSpeed = 35;
   shapeFile = "bioderm_heavy.dts";

   debrisShapeName = "bio_player_debris.dts";

   //Foot Prints
   decalData   = HeavyBiodermFootprint;
   decalOffset = 0.4;

   waterBreathSound = WaterBreathBiodermSound;
   
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs
   damageScale[$DamageType::Fire] = 5.0;
   damageScale[$DamageType::Plasma] = 4.0;
   damageScale[$DamageType::Burn] = 4.0;

	max[RepairKit]			= 0;
	max[Mine]				= 0;
	max[Grenade]			= 0;
	max[SmokeGrenade]			= 0;
	max[BeaconSmokeGrenade]		= 0;
	max[Blaster]			= 0;
	max[Plasma]				= 0;
	max[PlasmaAmmo]			= 0;
	max[Disc]				= 0;
	max[DiscAmmo]			= 0;
	max[SniperRifle]			= 0;
	max[GrenadeLauncher]		= 0;
	max[GrenadeLauncherAmmo]	= 0;
	max[Mortar]				= 0;
	max[MortarAmmo]			= 0;
	max[MissileLauncher]		= 0;
	max[MissileLauncherAmmo]	= 0;
	max[Chaingun]			= 0;
	max[ChaingunAmmo]			= 0;
	max[RepairGun]			= 0;
	max[CloakingPack]			= 0;
	max[SensorJammerPack]		= 0;
	max[EnergyPack]			= 0;
	max[RepairPack]			= 0;
	max[ShieldPack]			= 0;
	max[AmmoPack]			= 0;
	max[SatchelCharge]		= 0;
	max[MortarBarrelPack]		= 0;
	max[MissileBarrelPack]		= 0;
	max[AABarrelPack]			= 0;
	max[PlasmaBarrelPack]		= 0;
	max[ELFBarrelPack]		= 0;
	max[artillerybarrelpack]	= 0;
	max[MedPack]			= 0;
	max[InventoryDeployable]	= 0;
	max[MotionSensorDeployable]	= 0;
	max[PulseSensorDeployable]	= 0;
	max[TurretOutdoorDeployable]	= 0;
	max[TurretIndoorDeployable]	= 0;
	max[FlashGrenade]			= 0;
	max[ConcussionGrenade]		= 0;
	max[FlareGrenade]			= 0;
	max[TargetingLaser]		= 0;
	max[ELFGun]				= 0;
	max[ShockLance]			= 0;
	max[CameraGrenade]		= 0;
	max[Beacon]				= 0;
	max[flamerAmmoPack]		= 0;
	max[ParachutePack]		= 0;
	max[ConstructionTool]		= 0;
	max[MergeTool]			= 0;
	max[NerfGun]			= 0;
	max[NerfBallLauncher]		= 0;
	max[NerfBallLauncherAmmo]	= 0;
	max[SuperChaingun]		= 0;
	max[SuperChaingunAmmo]		= 0;
	max[RPChaingun]			= 0;
	max[RPChaingunAmmo]		= 0;
	max[MGClip]				= 0;
	max[LSMG]				= 0;
	max[LSMGAmmo]			= 0;
	max[LSMGClip]			= 0;
	max[snipergun]			= 0;
	max[snipergunAmmo]		= 0;
	max[Bazooka]			= 0;
	max[BazookaAmmo]			= 0;
	max[BunkerBuster]				= 0;
	max[MG42]				= 0;
	max[MG42Ammo]			= 0;
	max[SPistol]			= 0;
	max[Pistol]				= 0;
	max[PistolAmmo]			= 0;
	max[Pistolclip]			= 0;
	max[flamer]				= 0;
	max[flamerAmmo]			= 0;
	max[AALauncher]			= 0;
	max[AALauncherAmmo]		= 0;
	max[melee]				= 0;
	max[SOmelee]			= 0;
	max[KriegRifle]			= 0;
	max[KriegAmmo]			= 0;
	max[Rifleclip]			= 0;
	max[Shotgun]			= 0;
	max[ShotgunAmmo]			= 0;
	max[ShotgunClip]			= 0;
	max[RShotgun]			= 0;
	max[RShotgunAmmo]			= 0;
	max[RShotgunClip]			= 0;
	max[LMissileLauncher]		= 0;
	max[LMissileLauncherAmmo]	= 0;
	max[HRPChaingun]			= 0;
	max[RPGAmmo]			= 0;
	max[RPGItem]			= 0;
	max[spineDeployable]		= 0;
	max[mspineDeployable]		= 0;
	max[wWallDeployable]		= 0;
	max[floorDeployable]		= 0;
	max[WallDeployable]		= 0;
      max[DoorDeployable]           = 0;
	max[TurretLaserDeployable]	= 0;
	max[TurretMissileRackDeployable]= 0;
	max[DiscTurretDeployable]	= 0;
	max[EnergizerDeployable]	= 0;
	max[TreeDeployable]		= 0;
	max[CrateDeployable]		= 0;
	max[DecorationDeployable]	= 0;
	max[LogoProjectorDeployable]	= 0;
	max[LightDeployable]		= 0;
	max[TripwireDeployable]		= 0;
	max[TelePadPack]			= 0;
	max[TurretBasePack]		= 0;
	max[LargeInventoryDeployable]	= 0;
	max[GeneratorDeployable]	= 0;
	max[SolarPanelDeployable]	= 0;
	max[SwitchDeployable]		= 0;
	max[MediumSensorDeployable]	= 0;
	max[LargeSensorDeployable]	= 0;
	max[SpySatelliteDeployable]	= 0;
	max[JumpadDeployable]		= 0;
	max[EscapePodDeployable]	= 0;
	max[ForceFieldDeployable]	= 0;
	max[GravityFieldDeployable]	= 0;
      max[VehiclePadPack]		= 0;
};

function SpawnShadeLord(%position) {
   %Boss = new player(){
      Datablock = "ShadeLordArmor";
   };
   %Cpos = vectorAdd(%position, "0 0 5");
   MessageAll('MsgBossSpawn', "\c4"@$TWM2::BossName["ShadeLord"]@": Take your stand, and prepare to face your destined fate of death!");
   schedule(3000, 0, MessageAll, 'MsgBossSpawn', "\c4"@$TWM2::BossName["ShadeLord"]@": And so it begins... Let's see how you face your fears...");
   schedule(3500, 0, "ShadeLordFunction", %boss, "ShadeLordToggleCondition", 1);

   %Boss.ticks = 0;
   InitiateBoss(%Boss, "ShadeLord");

   %Boss.team = 30;
   %zname = $TWM2::BossName["ShadeLord"]; // <- To Hosts, Enjoy, You can
                                      //Change the Zombie Names now!!!
                                      
   $ShadeLordBoss::AllowedNighttime = 1;
   %Boss.target = createTarget(%Boss, %zname, "", "Derm3", '', %Boss.team, PlayerSensor);
   setTargetSensorData(%Boss.target, PlayerSensor);
   setTargetSensorGroup(%Boss.target, 30);
   setTargetName(%Boss.target, addtaggedstring(%zname));
   setTargetSkin(%Boss.target, 'Horde');
   //
   %Boss.setTransform(%cpos);
   %Boss.canjump = 1;
   %Boss.hastarget = 1;
   MissionCleanup.add(%Boss);
   %boss.moveloop = schedule(7500, %boss, "ShadeLordFunction", %boss, "ShadeLordDoMove", "");
}

function ShadeLordFunction(%boss, %function, %args) {
   switch$(%function) {

      //-------------
      //Boss Attacks
      //-------------
      case "Att_ShadeStrike":
         %target = getWord(%args, 0);
         %boss.setMoveState(true);
         %boss.schedule(5000, setMoveState, false);
         %boss.setActionThread($Zombie::RogThread,true);
         //
         %bPos = %boss.getPosition();
         %start1 = vectorAdd(%bPos, "300 -300 50");
         %go = vectorAdd(%bPos, "-300 300 50");
         %interval = 15;
         for(%i = 0; %i < 3; %i++) {
            %neg = %i % 2 == 0 ? 1 : -1;
            %start = vectorAdd(%start1, %neg*%interval*%i@" 0 0");
            %vec = vectorNormalize(vectorSub(%go,%start));
            %p = new SeekerProjectile() {
               dataBlock        = ShadeLordSword;
               initialDirection = %vec;
               initialPosition  = %start;
            };
            %p.sourceObject = %boss;
            %p.targetedPlayer = %target;
            %beacon = new BeaconObject() {
               dataBlock = "SubBeacon";
               beaconType = "vehicle";
               position = %target.player.getWorldBoxCenter();
            };
            %beacon.team = 0;
            %beacon.setTarget(0);
            MissionCleanup.add(%beacon);
            %p.setObjectTarget(%beacon);
            DemonMotherMissileFollow(%target,%beacon,%p);
         }

      case "Att_HealSequence":
         %count = getWord(%args, 0);
         if(!isObject(%boss) || %boss.getState() $= "dead") {
            return;
         }  
         if(%count == 0) {
            %boss.setMoveState(true);
            %boss.setPosition(vectorAdd(%boss.getPosition(), TWM2Lib_MainControl("getRandomPosition", 300 TAB 1)));
            cancel(%boss.moveLoop);
         }
         if(%count < 25) {
            %boss.setDamageLevel(%boss.getDamageLevel() - 0.1);
            createLifeEmitter(%boss.getPosition(), PrebeamEmitter, 5000);
         }
         else {
            %boss.schedule(3000, setMoveState, false);
            %boss.moveloop = schedule(3000, %boss, "ShadeLordFunction", %boss, "ShadeLordDoMove", "");
            return;
         }
         schedule(200, %boss, "ShadeLordFunction", %boss, "Att_HealSequence", %count++);

      case "Att_ShadeLordDecend":
         %count = getWord(%args, 0);
         if(%count == 0) {
            cancel(%boss.moveLoop);
            //%boss.rapierShield = 1;
            %boss.setMoveState(true);
            if(isObject(%boss.shadeStorm)) {
               %boss.shadeStorm.delete();
            }
         }
         else if(%count > 0 && %count <= 25) {
            %pos = "0 0 "@ 250 - (10 * %count);
            if(isObject(%boss.shadeStorm)) {
               %boss.shadeStorm.delete();
            }
            %boss.shadeStorm = new ParticleEmissionDummy(){
               position = vectoradd(%boss.getPosition(), %pos);
               dataBlock = "defaultEmissionDummy";
   	       emitter = "ShadeStormEmitter";            //ShadeStormEmitter
            };
         }
         else if(%count == 26) {
            for(%i = 0; %i < ClientGroup.getCount(); %i++) {
               %cl = ClientGroup.getObject(%i);
               if(isObject(%cl.player) && %cl.player.getState() !$= "dead") {
                  ShadeLordFunction(%boss, "ShadeLordDropKill", %cl.player);
               }
            }
           if(isObject(%boss.shadeStorm)) {
              %boss.shadeStorm.delete();
            }
            %boss.shadeStorm = new ParticleEmissionDummy(){
               position = vectoradd(%boss.getPosition(), "0 0 0.5");
               dataBlock = "defaultEmissionDummy";
   	       emitter = "ShadeStormEmitter";            //ShadeStormEmitter
            };
         }
         else if(%count > 26 && %count < 40) {
            if(isObject(%boss.shadeStorm)) {
               %boss.shadeStorm.delete();
            }
            %boss.shadeStorm = new ParticleEmissionDummy(){
               position = vectoradd(%boss.getPosition(), "0 0 1.5");
               dataBlock = "defaultEmissionDummy";
   	       emitter = "ShadeStormEmitter";            //ShadeStormEmitter
            };
         }
         else if(%count == 40) {
            %boss.setMoveState(false);
            %boss.moveloop = schedule(3000, %boss, "ShadeLordFunction", %boss, "ShadeLordDoMove", "");
            //flash all
            for(%i = 0; %i < ClientGroup.getCount(); %i++) {
               %cl = ClientGroup.getObject(%i);
               if(isObject(%cl.player) && %cl.player.getState() !$= "dead") {
                  %cl.player.setWhiteout(1.0);
               }
            }
            return;
         }
         %count++;
         schedule(300, %boss, "ShadeLordFunction", %boss, "Att_ShadeLordDecend", %count);

      case "Att_ShadeLordScream":
         cancel(%boss.moveloop);
         %boss.setMoveState(true);
         %boss.schedule(5000, setMoveState, false);
         //create emitter
         %screamEmit = new ParticleEmissionDummy(){
            position = vectoradd(%boss.getPosition(),"0 0 0.5");
            dataBlock = "defaultEmissionDummy";
            emitter = "ShadeLordScreamEmitter";            //ShadeStormEmitter
         };
         %screamEmit.schedule(5000, "delete");
   
         //knock down and throw weapons in radius.
         %TargetSearchMask = $TypeMasks::PlayerObjectType;
         InitContainerRadiusSearch(%boss.getPosition(), 45, %TargetSearchMask);
         while ((%potentialTarget = ContainerSearchNext()) != 0) {
            if(isSet(%potentialTarget.client)) {
               //throw guns, knock down.
               %potentialTarget.setActionThread("death1", true);
               %potentialTarget.throwweapon(1);
               %potentialTarget.throwweapon(0);
               %potentialTarget.setMoveState(true);
               %potentialTarget.schedule(3000, setMoveState, false);
            }
         }
         //
         %boss.moveloop = schedule(5000, %boss, "ShadeLordFunction", %boss, "ShadeLordDoMove", "");

      //-------------
      //Boss Functions
      //-------------   
      case "ShadeLordDarkAttacks":
         if(!isObject(%boss) || %boss.getState() $= "Dead") {
            return;
         }
         if(isObject(%boss.dayCloak)) {
            %boss.dayCloak.delete();
         }
         if(%boss.randomFX $= "") {
            %boss.randomFX = ShadeLordFunction(%boss, "ShadeStormFX", "");
         }
         if(%boss.antiSky $= "") {
            %boss.antiSky = ShadeLordFunction(%boss, "ShadeStormAntiSky", "");
         }
         %attack = getRandom(1, 3);
         switch(%attack) {
            case 1:
               MessageAll('MsgBossSpawn', "\c4"@$TWM2::BossName["ShadeLord"]@": SHALDORVAAAAAAAAAAAAAAH!!!!!!!");
               ShadeLordFunction(%boss, "Att_ShadeLordScream", "");
            case 2:
               MessageAll('MsgBossSpawn', "\c4"@$TWM2::BossName["ShadeLord"]@": Descend Mighty Shade Storm, Destroy all who dare oppose us!");
               ShadeLordFunction(%boss, "Att_ShadeLordDecend", 0);
            case 3:
               %target = FindValidTarget(%boss);
               MessageAll('MessageAll', "\c4"@$TWM2::BossName["ShadeLord"]@": Come forth my shade, Destroy "@getTaggedString(%target.name)@"!");              
               if(isObject(%target.player)) {           
                  ShadeLordFunction(%boss, "Att_ShadeStrike", %target.player);
               }
               else {
                  MessageAll('MessageAll', "\c4"@$TWM2::BossName["ShadeLord"]@": Hiding in death does not save you "@getTaggedString(%target.name)@"");
               }
         }
         %boss.attacks = schedule(25000, %boss, "ShadeLordFunction", %boss, "ShadeLordDarkAttacks", "");

      case "ShadeLordLightAttacks":
         if(!isObject(%boss) || %boss.getState() $= "Dead") {
            return;
         }
         if(isObject(%boss.shadeStorm)) {
            %boss.shadeStorm.delete();
         }
         if(!isObject(%boss) || !%boss.getState() $= "dead") {
            if(isObject(%boss.dayCloak)) {
               %boss.dayCloak.delete();
            }
            if(isObject(%boss.shadeStorm)) {
               %boss.shadeStorm.delete();
            }
            return;
         }
         %attack = getRandom(1, 2);
         switch(%attack) {
            case 1:
               MessageAll('MsgBossSpawn', "\c4"@$TWM2::BossName["ShadeLord"]@": SHALDORVAAAAAAAAAAAAAAH!!!!!!!");
               ShadeLordFunction(%boss, "Att_ShadeLordScream", "");
            case 2:
               MessageAll('MsgBossEvilness', "\c4"@$TWM2::BossName["ShadeLord"]@": Come forth, and return to me the power of the shadows!");
               ShadeLordFunction(%boss, "Att_HealSequence", 0);
         }
         %boss.attacks = schedule(25000, %boss, "ShadeLordFunction", %boss, "ShadeLordLightAttacks", "");

      case "ShadeStormFX":
         if(!isObject(%boss) || %boss.getState() $= "Dead") {
            return;
         }
         %bPos = %boss.getPosition();
         %start1 = vectorAdd(%bPos, "300 -300 50");
         %go = vectorAdd(%bPos, "-300 300 50");
         %interval = 15;
         for(%i = 0; %i < 20; %i++) {
            %neg = %i % 2 == 0 ? 1 : -1;
            %start = vectorAdd(%start1, %neg*%interval*%i@" 0 0");
            %vec = vectorNormalize(vectorSub(%go,%start));
            %p = new SeekerProjectile() {
               dataBlock        = ShadeLordSword;
               initialDirection = %vec;
               initialPosition  = %start;
            };
            %p.sourceObject = %boss;
            MissionCleanup.add(%p);
         } 
         %boss.randomFX = schedule(getRandom(10000, 25000), %boss, "ShadeLordFunction", %boss, "ShadeStormFX", "");
   
      case "ShadeStormAntiSky":
         if(!isObject(%boss) || %boss.getState() $= "Dead") {
            return;
         }
         if(!$ShadeLordBoss::AllowedNighttime) {
            return;
         }
         %killHeight = getWord(%boss.getPosition(), 2) + 50;
         for(%i = 0; %i < ClientGroup.getCount(); %i++) {
            %cl = ClientGroup.getObject(%i);
            if(isObject(%cl.player) && %cl.player.getState() !$= "dead") {
               if(getWord(%cl.player.getPosition(), 2) >= %killHeight) {
                  ShadeLordFunction(%boss, "ShadeLordDropKill", %cl.player);
               }
            }
         }
         %boss.antiSky = schedule(2500, %boss, "ShadeLordFunction", %boss, "ShadeStormAntiSky", "");

      case "ShadeLordDoDeath":
         %boss.RapierShield = 1;
         %boss.inDeath = 1;
         if(isObject(%boss.dayCloak)) {
            %boss.dayCloak.delete();
         }
         if(isObject(%boss.shadeStorm)) {
            %boss.shadeStorm.delete();
         }
         //set on fire
         %fire = new ParticleEmissionDummy(){
            position = vectoradd(%boss.getPosition(),"0 0 0.5");
            dataBlock = "defaultEmissionDummy";
   	    emitter = "BurnEmitter";
         };
         MissionCleanup.add(%fire);
         %fire.schedule(5000, delete);
         //
         %Boss.setMoveState(true);
         %boss.setActionThread("death1", true);
         %boss.schedule(5000, "blowup");
         %boss.schedule(5000, "scriptkill");
         schedule(4999, 0, eval, ""@%boss@".rapierShield = 0;");
   
         Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
         Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
         Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
         Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
         Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());
         Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)],%boss.getPosition());

      case "ShadeLordToggleCondition":
         %flag = getWord(%args, 0);
         if(!isObject(%boss) || %boss.getState() $= "dead") {
            return;
         }
         cancel(%boss.attacks);
         if(%flag) {
            Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)], %boss.getPosition());
            Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)], %boss.getPosition());
            Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)], %boss.getPosition());
            Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)], %boss.getPosition());
            Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)], %boss.getPosition());
            Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)], %boss.getPosition());
            %Boss.setMoveState(true);
            %Boss.setActionThread("cel4",true);
            %Boss.schedule(3500, "SetMoveState", false);
            skyVeryDark();

            %boss.attacks = ShadeLordFunction(%boss, "ShadeLordDarkAttacks", "");
         }
         else {
            Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)], %boss.getPosition());
            Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)], %boss.getPosition());
            Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)], %boss.getPosition());
            Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)], %boss.getPosition());
            Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)], %boss.getPosition());
            Serverplay3D($Ion::ThunderSound[mFloor(getRandom() * $Ion::ThunderSoundCount)], %boss.getPosition());
            %Boss.setMoveState(true);
            %Boss.setActionThread("death1",true);
            %Boss.schedule(3000, "setActionThread", "cel4", true);
            %Boss.schedule(4500, "SetMoveState", false);
            skyDusk();
      
            cancel(%boss.antiSky);
            cancel(%boss.randomFX);
      
            %boss.antiSky = "";
            %boss.randomFX = "";
            %boss.attacks = schedule(4500, %boss, "ShadeLordFunction", %boss, "ShadeLordLightAttacks", "");
         }

      case "ShadeLordDoMove":
         if(!isobject(%boss) || %boss.getState() $= "Dead") {
            if(isObject(%boss.dayCloak)) {
               %boss.dayCloak.delete();
            }
            if(isObject(%boss.shadeStorm)) {
               %boss.shadeStorm.delete();
            }
            return;
         }
         if(%boss.getDamageLeftPct() < 0.025) {
            ShadeLordFunction(%boss, "ShadeLordDoDeath", "");
         }
         if(%boss.getDamageLeftPct() < 0.4) {
            if($ShadeLordBoss::AllowedNighttime == 1) {
               $ShadeLordBoss::AllowedNighttime = 0;
               ShadeLordFunction(%boss, "ShadeLordToggleCondition", 0);
               MessageAll('MsgBossSpawn', "\c4"@$TWM2::BossName["ShadeLord"]@": No, You will not break the barrier of dark!");
            }
         }
         else {
            if($ShadeLordBoss::AllowedNighttime == 0) {
               $ShadeLordBoss::AllowedNighttime = 1;
               ShadeLordFunction(%boss, "ShadeLordToggleCondition", 1);
               MessageAll('MsgBossSpawn', "\c4"@$TWM2::BossName["ShadeLord"]@": Awaken, mighty storm of shade, bring forth the doom of our foes!");
            }
         }
         if(isObject(%boss.dayCloak) && !%boss.inDeath) {
            %boss.dayCloak.delete();
            %boss.dayCloak = new ParticleEmissionDummy(){
               position = vectoradd(%boss.getPosition(),"0 0 0.5");
               dataBlock = "defaultEmissionDummy";
   	       emitter = "dayCloakEmitter";            //ShadeStormEmitter
            };
            MissionCleanup.add(%boss.dayCloak);
         }
         else {
            if($ShadeLordBoss::AllowedNighttime == 0) {
               %boss.dayCloak = new ParticleEmissionDummy(){
                  position = vectoradd(%boss.getPosition(),"0 0 0.5");
                  dataBlock = "defaultEmissionDummy";
   	          emitter = "dayCloakEmitter";            //ShadeStormEmitter
               };
            }
         }   
         if(isObject(%boss.shadeStorm)) {
            %boss.shadeStorm.delete();
            %boss.shadeStorm = new ParticleEmissionDummy(){
               position = vectoradd(%boss.getPosition(),"0 0 250");
               dataBlock = "defaultEmissionDummy";
   	       emitter = "ShadeStormEmitter";            //ShadeStormEmitter
            };  
         }
         else {
            if($ShadeLordBoss::AllowedNighttime == 1) {
               %boss.shadeStorm = new ParticleEmissionDummy(){
                  position = vectoradd(%boss.getPosition(),"0 0 250");
                  dataBlock = "defaultEmissionDummy";
   	          emitter = "ShadeStormEmitter";            //ShadeStormEmitter
               };
            }
         }
         %pos = %boss.getworldboxcenter();
         %closestClient = ZombieLookForTarget(%boss);
         %z = getWord(%pos, 2);
         if(%z < -300) {
            %boss.startFade(400, 0, true);
            %boss.startFade(1000, 0, false);
            %boss.setPosition(vectorAdd(vectoradd(%closestclient.getPosition(), "0 0 20"), TWM2Lib_MainControl("getRandomPosition", 25 TAB 1)));
            %boss.setVelocity("0 0 0");
            MessageAll('MsgVardison', "\c4"@$TWM2::BossName["ShadeLord"]@": I'm back....");
         }
         %closestDistance = getWord(%closestClient,1);
         %closestClient = getWord(%closestClient,0).Player;
         if(%closestDistance <= $Zombie::detectDist) {
            if(%closestDistance < 10) {
               ShadeLordFunction(%boss, "ShadeLordDropKill", %closestClient);
               MessageAll('MsgVardison', "\c4"@$TWM2::BossName["ShadeLord"]@": Feel The Vengeance of the Shadows "@getTaggedString(%closestClient.client.name)@".");
               %closestClient.setMoveState(true);
               ShadeLordFunction(%boss, "ShadeLordRandomTeleport", "");
            }
	    if(%boss.hastarget != 1){
	       %boss.hastarget = 1;
            }
            %vector = ZgetFacingDirection(%boss,%closestClient,%pos);
	    %vector = vectorscale(%vector, ($Zombie::DForwardSpeed) * 0.6);
	    %upvec = "150";
	    %x = Getword(%vector,0);
	    %y = Getword(%vector,1);
	    %z = Getword(%vector,2);
	    if(%z >= ($Zombie::DForwardSpeed))
	       %upvec = (%upvec * 5);
	    %vector = %x@" "@%y@" "@%upvec;
            %boss.applyImpulse(%pos, %vector);
         }
         else if(%boss.hastarget == 1){
            %boss.hastarget = 0;
	    %boss.DemonRmove = schedule(100, %boss, "ZSetRandomMove", %boss);
         }
         %boss.moveloop = schedule(230, %boss, "ShadeLordFunction", %boss, "ShadeLordDoMove", "");

      case "ShadeLordRandomTeleport":
         if(%boss.getState() $= "dead") {
            return;
         }
         %newPosition = vectorAdd(%boss.getPosition(), TWM2Lib_MainControl("getRandomPosition", 150 TAB 1));
         %boss.setPosition(%newPosition); 

      case "ShadeLordDropKill":
         %target = getWord(%args, 0); 
         %incoming = vectorAdd(%target.getPosition(), vectorAdd(TWM2Lib_MainControl("getRandomPosition", 70 TAB 1), "0 0 250"));
         %vec = vectorNormalize(vectorSub(%target.getPosition(),%incoming));
         %p = new SeekerProjectile() {
            dataBlock        = ShadeLordSword;
            initialDirection = %vec;
            initialPosition  = %incoming;
         };
         %p.sourceObject = %boss;
         %p.targetedPlayer = %target;
         %beacon = new BeaconObject() {
            dataBlock = "SubBeacon";
            beaconType = "vehicle";
            position = %target.getWorldBoxCenter();
         };
         %beacon.team = 0;
         %beacon.setTarget(0);
         MissionCleanup.add(%beacon);
         %p.setObjectTarget(%beacon);
         DemonMotherMissileFollow(%target,%beacon,%p); 

      //-------------
      //Misc Functions
      //-------------  
      case "DoReturnMissile":
         %ini = getWord(%args, 0);
         %src = getWord(%args, 1);
         %final = vectorAdd(%ini.getPosition(), vectorAdd(TWM2Lib_MainControl("getRandomPosition", 70 TAB 1), "0 0 250"));
         %vec = vectorNormalize(vectorSub(%final, %ini.getPosition()));
         %p = new SeekerProjectile() {
            dataBlock        = ShadeLordSword;
            initialDirection = %vec;
            initialPosition  = vectorAdd(%ini.getPosition(), "0 0 3");
         };
         %p.sourceObject = %src;   

      default:
         error("ShadeLordFunction(): Non-existent SL function call "@%function@".");

   }
}
