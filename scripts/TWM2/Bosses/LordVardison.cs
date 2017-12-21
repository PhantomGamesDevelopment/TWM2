//Lord Vardison
//Version 2.0
//Revised And Reimagined... The Pure Evil of TWM2 Has Been Reborn, Now With Less Crashing xD
// Now with difficulty levels :P
//  1, Easy: Standard Vardison Battle
//    - Limited Mobs
//    - Attacks On Normal
//    - Has Timed Grab Kills
//  2, Normal: Tougher Vardison Battle
//    - Enhanced Mob Spawns
//    - Slightly Faster Attacks
//    - Phase 3 Insta-Kills
//    - Shadow Orb Mechanic: Deaths to phase 3 initiate Shadow Orb, which is a 20 second to kill all mechanic.
//  3, Hard: The True Experience
//    - Minions A Plenty
//    - Quick Attacks
//    - Phases 2 & 3 Insta-Kill
//    - Shadow Orb at Phase 2 & 3
//  4, WTF!?!?! Why God... Why....
//    - The Army Comith...
//    - Attacks Are So Rapid....
//    - All Phases Insta-Kill, Phase 3 does the spawn camp thingy :D
//    - Shadow Orb at all Phases
//    - Shadow Orb regenerates Vardison's HP

$Boss::Proficiency["Vardison3", 0] = "Team Bronze\t1000\tDefeat Lord Vardison with your team dying no more than 25 times";
$Boss::ProficiencyCode["Vardison3", 0] = "$TWM2::BossManager.bossKills < 25";
$Boss::Proficiency["Vardison3", 1] = "Team Silver\t5000\tDefeat Lord Vardison with your team dying no more than 15 times";
$Boss::ProficiencyCode["Vardison3", 1] = "$TWM2::BossManager.bossKills < 15";
$Boss::Proficiency["Vardison3", 2] = "Team Gold\t10000\tDefeat Lord Vardison with your team dying no more than 10 times";
$Boss::ProficiencyCode["Vardison3", 2] = "$TWM2::BossManager.bossKills < 10";
$Boss::Proficiency["Vardison3", 3] = "Demon Slayer\t25000\tDefeat Lord Vardison's Third Phase without dying, and dealing more than 15% damage to him";
$Boss::ProficiencyCode["Vardison3", 3] = "[bProf].bossDeaths == 0 && [dPerc] > 15";
$Boss::Proficiency["Vardison3", 4] = "Shadow Disconnect\t10000\tDeny Lord Vardison all Shadow Orb detonations";
$Boss::ProficiencyCode["Vardison3", 4] = "$TWM2::VardisonDifficulty >= 2 && [bProf].orbDetonates == 0";
$Boss::Proficiency["Vardison3", 5] = "The Unholy One\t75000\tDefeat Lord Vardison's Third Phase without dying, and dealing more than 15% damage to him on WTF difficulty";
$Boss::ProficiencyCode["Vardison3", 5] = "$TWM2::VardisonDifficulty == 4 && [bProf].bossDeaths == 0 && [dPerc] > 15";

$TWM2::VardisonDifficulty = 1;
$TWM2::Vardison_DMsg[1] = "Lord Vardison Fight [EASY]: The Standard Battle... Work Togther and Be Victorious.";
$TWM2::Vardison_DMsg[2] = "Lord Vardison Fight [NORMAL]: Vardison Has Enhanced His Skills, Will you prove stronger?";
$TWM2::Vardison_DMsg[3] = "Lord Vardison Fight [HARD]: The True Vardison Experience... Only The True Will Be Victorious...";
$TWM2::Vardison_DMsg[4] = "Lord Vardison Fight [WTF]: All Prayers Will Be Accepted Before You Painfully Die... Over And Over And Over....";
//Difficulty Variables, Don't Touch
$TWM2::Vardison_OrbKillTime = 12500;
$TWM2::Vardison1_AttSpeed[1] = 20000;
$TWM2::Vardison1_AttSpeed[2] = 17500;
$TWM2::Vardison1_AttSpeed[3] = 13500;
$TWM2::Vardison1_AttSpeed[4] = 7500;
$TWM2::Vardison2_AttSpeed[1] = 20000;
$TWM2::Vardison2_AttSpeed[2] = 17500;
$TWM2::Vardison2_AttSpeed[3] = 12500;
$TWM2::Vardison2_AttSpeed[4] = 7000;
$TWM2::Vardison3_AttSpeed[1] = 17500;
$TWM2::Vardison3_AttSpeed[2] = 15000;
$TWM2::Vardison3_AttSpeed[3] = 11500;
$TWM2::Vardison3_AttSpeed[4] = 6500;
$TWM2::Vardison1_CanOrb[1] = false;
$TWM2::Vardison1_CanOrb[2] = false;
$TWM2::Vardison1_CanOrb[3] = false;
$TWM2::Vardison1_CanOrb[4] = true;
$TWM2::Vardison2_CanOrb[1] = false;
$TWM2::Vardison2_CanOrb[2] = false;
$TWM2::Vardison2_CanOrb[3] = true;
$TWM2::Vardison2_CanOrb[4] = true;
$TWM2::Vardison3_CanOrb[1] = false;
$TWM2::Vardison3_CanOrb[2] = true;
$TWM2::Vardison3_CanOrb[3] = true;
$TWM2::Vardison3_CanOrb[4] = true;
$TWM2::Vardison_OrbRegenHP[1] = false;
$TWM2::Vardison_OrbRegenHP[2] = false;
$TWM2::Vardison_OrbRegenHP[3] = false;
$TWM2::Vardison_OrbRegenHP[4] = true;
$TWM2::Vardison1_MaxMinions[1] = 5;
$TWM2::Vardison1_MaxMinions[2] = 10;
$TWM2::Vardison1_MaxMinions[3] = 12;
$TWM2::Vardison1_MaxMinions[4] = 15;
$TWM2::Vardison2_MaxMinions[1] = 7;
$TWM2::Vardison2_MaxMinions[2] = 9;
$TWM2::Vardison2_MaxMinions[3] = 12;
$TWM2::Vardison2_MaxMinions[4] = 15;
$TWM2::Vardison3_MaxMinions[1] = 10;
$TWM2::Vardison3_MaxMinions[2] = 13;
$TWM2::Vardison3_MaxMinions[3] = 17;
$TWM2::Vardison3_MaxMinions[4] = 25;
$TWM2::Vardison_MinionCooldown[1] = 20;
$TWM2::Vardison_MinionCooldown[2] = 17;
$TWM2::Vardison_MinionCooldown[3] = 15;
$TWM2::Vardison_MinionCooldown[4] = 10;

//Particles & Emitters
datablock ParticleData(SummoningPierParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.1;
   inheritedVelFactor   = 0.1;

   lifetimeMS           = 1500;
   lifetimeVarianceMS   = 50;

   textureName          = "special/cloudflash";

   spinRandomMin = -10.0;
   spinRandomMax = 10.0;

   colors[0]     = "1 0.18 0.03 0.4";
   colors[1]     = "0 134 139 0.3";
   colors[2]     = "1 0.18 0.03 0.0";
   sizes[0]      = 10.0;
   sizes[1]      = 7.5;
   sizes[2]      = 5.0;
   times[0]      = 0.0;
   times[1]      = 0.6;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(SummoningPierEmitter) {
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;

   ejectionOffset = 0.2;
   ejectionVelocity = 10.0;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 10.0;

   particles = "SummoningPierParticle";
};

datablock ParticleData(ShadowOrbParticle) {
   dragCoefficient      = 5;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = -1.3;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 150;
   textureName          = "special/cloudflash";
   useInvAlpha          =  false;
   colors[0]     = "0.5 0.1 0.9 1.0";
   colors[1]     = "0.5 0.1 0.9 1.0";
   colors[2]     = "0.5 0.1 0.9 1.0";
   sizes[0]      = 20.501;
   sizes[1]      = 25.001;
   sizes[2]      = 30.001;
   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ShadowOrbEmitter) {
   ejectionPeriodMS = 15;
   periodVarianceMS = 5;

   ejectionVelocity = 42.7;  // A little oomph at the back end
   velocityVariance = 0.0;
   ejectionoffset = 0;
   thetaMin         = 0.0;
   thetaMax         = 180.0;
   phiReferenceVel = 0;
   phiVariance = "360";
   particles = "ShadowOrbParticle";
   overrideAdvances = true;
   orientParticles  = true;
};

datablock ParticleData(ShadowOrbDetonationParticle) {
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

   colors[0]     = "0.5 0.1 0.9 1.0";
   colors[1]     = "156 0 0 0.5";
   colors[2]     = "0.5 0.1 0.9 1.0";
   sizes[0]      = 500;
   sizes[1]      = 500;
   sizes[2]      = 2;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ShadowOrbDetonationEmitter) {
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
   particles = "ShadowOrbDetonationParticle";
};

datablock ParticleData(ShadowBaseParticle) {
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

   colors[0] = "0.5 0.1 0.9 1.0";
   colors[1] = "0.5 0.1 0.9 1.0";
   colors[2] = "0.5 0.1 0.9 1.0";

   sizes[0]      = 2.5;
   sizes[1]      = 2.7;
   sizes[2]      = 3.0;

   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ShadowBaseEmitter) {
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.5;
   velocityVariance = 0.3;

   thetaMin         = 0.0;
   thetaMax         = 30.0;

   particles = "ShadowBaseParticle";
};

//Projectile Datablocks:
//Ultimate Laser Projectile
datablock LinearFlareProjectileData(SuperlaserProjectile) {
   scale               = "15.0 15.0 15.0";
   faceViewer          = false;
   directDamage        = 1.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.9;
   damageRadius        = 30.0;
   kickBackStrength    = 1000.0;
   radiusDamageType    = $DamageType::Explosion;

   explosion[0]           = "HyperDevCannonExplosion2";
   explosion[1]           = "SatchelMainExplosion";
   splash              = PlasmaSplash;
   baseEmitter         = HyperDevCannonBaseEmitter;


   dryVelocity       = 200.0;
   wetVelocity       = 200;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 10000;
   lifetimeMS        = 10000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 9;
   size[1]           = 10;
   size[2]           = 11;


   numFlares         = 400;
   flareColor        = "0.0 1.0 0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound        = MissileProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 0.75 0.25";

};

datablock LinearFlareProjectileData(ShadowBladeSlam) {
   scale               = "0.1 0.1 0.1";
   faceViewer          = false;
   directDamage        = 1.0;
   hasDamageRadius     = true;
   indirectDamage      = 5.0;
   damageRadius        = 15.0;
   kickBackStrength    = 1000.0;
   radiusDamageType    = $DamageType::BladeOfVengance;

   explosion[0]           = "SatchelMainExplosion";
   splash              = PlasmaSplash;


   dryVelocity       = 200.0;
   wetVelocity       = 200;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 10000;
   lifetimeMS        = 10000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = "0.1";
   size[1]           = "0.1";
   size[2]           = "0.1";


   numFlares         = 1;
   flareColor        = "0.0 1.0 0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound        = MissileProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 0.75 0.25";

};

datablock LinearFlareProjectileData(ShadowBlastBolt) {
   projectileShapeName = "turret_muzzlepoint.dts";
   scale               = "1.0 1.0 1.0";
   faceViewer          = true;
   directDamage        = 0.9;
   hasDamageRadius     = true;
   indirectDamage      = 0.9;
   damageRadius        = 15.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::Vardison;

   explosion           = "MortarExplosion";
   splash              = PlasmaSplash;

   baseEmitter        = ShadowBaseEmitter;

   dryVelocity       = 50.0; // z0dd - ZOD, 7/20/02. Faster plasma projectile. was 55
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 29500;
   lifetimeMS        = 30000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.5;
   size[2]           = 0.1;


   numFlares         = 35;
   flareColor        = "1 0.18 0.03";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	sound        = PlasmaProjectileSound;
   fireSound    = FlamethrowerFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 10.0;
   lightColor  = "0.94 0.03 0.12";
};

//Armor Datablocks
datablock StaticShapeData(InitialVaridionHoloArmor) : StaticShapeDamageProfile {
	className = "player";
	shapeFile = "TR2Heavy_male.dts"; // dmiscf.dts, alternate
    mass = 1;
	elasticity = 0.1;
	friction = 0.9;
	collideable = 0;
    isInvincible = true;
};
function InitialVaridionHoloArmor::shouldApplyImpulse(%targetObject) {
   return false;
}

datablock PlayerData(VardisonStageOneArmor) : LightMaleHumanArmor {
   boundingBox = "1.63 1.63 2.6";
   maxDamage = 300.0;
   minImpactSpeed = 35;
   shapeFile = "medium_male.dts";

   debrisShapeName = "bio_player_debris.dts";

   //Foot Prints
   decalData   = HeavyBiodermFootprint;
   decalOffset = 0.4;

   waterBreathSound = WaterBreathBiodermSound;

   damageScale[$DamageType::W1700] = 3.0;
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs
   damageScale[$DamageType::BladeOfVengance] = 0.001;
};

datablock PlayerData(VardisonStageTwoArmor) : LightMaleHumanArmor {
   shapefile = "TR2medium_male.dts";
   mass = 500;
   maxDamage = 350.0;
   minImpactSpeed = 50;
   speedDamageScale = 0.015;
   boundingBox = "2.9 2.9 4.8";

   underwaterJetForce = 10;

   LFootSoftSound       = ZLordFootSound;
   RFootSoftSound       = ZLordFootSound;
   LFootHardSound       = HZLordFootSound;
   RFootHardSound       = HZLordFootSound;
   LFootMetalSound      = ZLordFootSound;
   RFootMetalSound      = ZLordFootSound;
   LFootSnowSound       = ZLordFootSound;
   RFootSnowSound       = ZLordFootSound;

   damageScale[$DamageType::M1700] = 1.5;
   damageScale[$DamageType::BladeOfVengance] = 0.001;

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

datablock PlayerData(VardisonStageThreeArmor) : LightMaleHumanArmor {
   shapefile = "TR2Heavy_male.dts";
   mass = 500;
   maxDamage = 500.0;
   minImpactSpeed = 50;
   speedDamageScale = 0.015;
   boundingBox = "2.9 2.9 4.8";

   underwaterJetForce = 10;

   LFootSoftSound       = ZLordFootSound;
   RFootSoftSound       = ZLordFootSound;
   LFootHardSound       = HZLordFootSound;
   RFootHardSound       = HZLordFootSound;
   LFootMetalSound      = ZLordFootSound;
   RFootMetalSound      = ZLordFootSound;
   LFootSnowSound       = ZLordFootSound;
   RFootSnowSound       = ZLordFootSound;

   damageScale[$DamageType::M1700] = 1.5;
   damageScale[$DamageType::BladeOfVengance] = 0.001;

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

datablock StaticShapeData(ShadowOrb) : StaticShapeDamageProfile {
	className = "Generator";
	shapeFile = "station_generator_large.dts";

	maxDamage      = 2.0;
	destroyedLevel = 2.0;
	disabledLevel  = 0.3;

	isShielded = false;
	energyPerDamagePoint = 240;
	maxEnergy = 50;
	rechargeRate = 0.25;

	explosion      = HandGrenadeExplosion;
	expDmgRadius = 1.0;
	expDamage = 0.05;
	expImpulse = 200;
 
    boundingBox = "5.0 5.0 5.0";
	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;
	cmdCategory = "DSupport";
	cmdIcon = CMDSensorIcon;
	cmdMiniIconName = "commander/MiniIcons/com_deploymotionsensor";
	targetNameTag = 'Shadow Rift';
	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
	needsPower = true;
};

function ShadowOrb::onDestroyed(%this, %obj, %prevState) {
   if (%obj.isRemoved) {
      return;
   }
   if(isObject(%obj.waypoint)) {
      %obj.waypoint.schedule(500, "delete");
   }
   if(isObject($TWM2::VardisonManager.OrbSFX)) {
      $TWM2::VardisonManager.OrbSFX.schedule(500, "delete");
   }
   %obj.isRemoved = true;
   Parent::onDestroyed(%this, %obj, %prevState);
   %obj.schedule(500, "delete");
   $TWM2::VardisonManager.orbDestroyed();
}

//Boss Functions
function CheckVardisonManager() {
   if($TWM2::VardisonDifficulty < 1 || $TWM2::VardisonDifficulty > 4) {
      //Are the odds in your favor now, or will Vardison rek you endlessly?
      $TWM2::VardisonDifficulty = getRandom(1, 4);
   }
   if(!isObject($TWM2::VardisonManager)) {
      $TWM2::VardisonManager = new ScriptObject() {
         class = "VardisonManager";
      };
   }
   //Setup default params
   $TWM2::VardisonManager.lastAttackTime = getRealTime();
   $TWM2::VardisonManager.minionCount = 0;
   $TWM2::VardisonManager.orbObject = -1;
}

function StartVardison1(%position) {
   CheckVardisonManager();
   //Trip the boss manager so nothing else fires up in the initial 10 seconds.
   $TWM2::BossGoing = 1;
   //Erupt a firestorm in his coming....
   // They shall know fear xD
   MessageAll('msgAdminForce', "\c5"@$TWM2::Vardison_DMsg[$TWM2::VardisonDifficulty]);
   //Adjust Position
   %spawnPos = getWord(%position, 0) SPC getWord(%position, 1) SPC getTerrainHeight(%position);
   //First Spawn The Firestorm & Sound FX
   MessageAll('msgSound', "~wfx/environment/wind_sandstorm.wav");
   MessageAll('msgSound', "~wfx/environment/snowstorm1.wav");
   schedule(1500, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(2500, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(3500, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(5000, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(5500, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(7500, 0, MessageAll, 'msgSound', "~wfx/armor/breath_bio_uw.wav");
   schedule(10000, 0, MessageAll, 'msgSound', "~wfx/explosions/explosion.xpl03.wav");
   schedule(10000, 0, MessageAll, 'msgSound', "~wfx/explosions/explosion.xpl03.wav");
   schedule(10000, 0, MessageAll, 'msgSound', "~wfx/explosions/explosion.xpl03.wav");
   %fire = new ParticleEmissionDummy(){
      position = vectoradd(%spawnPos, "0 0 0.5");
      dataBlock = "defaultEmissionDummy";
      emitter = "SummoningPierEmitter";
   };
   MissionCleanup.add(%fire);
   %fire.schedule(10000, "delete");
   //Spawn the fake hologram vardison.
   %fakeHolo = new StaticShape(){
      Datablock = "InitialVaridionHoloArmor";
   };
   %fakeHolo.setTransform(vectorAdd(%spawnPos, "0 0 0.25"));
   %fakeHolo.startfade(0, 0, true);
   %fakeHolo.schedule(1000, startfade, 2500, 0, false);
   %fakeHolo.schedule(7500, startfade, 2500, 0, true);
   %fakeHolo.schedule(10000, "Delete");
   //And then start the real one...
   schedule(10000, 0, SpawnVardison, %spawnPos);
}

function SpawnVardison(%position) {
   %Boss = new player(){
      Datablock = "VardisonStageOneArmor";
   };
   %Cpos = vectorAdd(%position, "0 0 5");

   %Boss.isMultiPhaseBoss = true;
   %Boss.isFirstPhase = true;
   %Boss.isFinalPhase = false;

   InitiateBoss(%Boss, "Vardison1");

   %Boss.team = 30;
   %Boss.target = createTarget(%Boss, "\c7Lord Vardison", "", "Derm3", '', %Boss.team, PlayerSensor);
   setTargetSensorData(%Boss.target, PlayerSensor);
   setTargetSensorGroup(%Boss.target, 30);
   setTargetName(%Boss.target, addtaggedstring("\c7Lord Vardison"));
   setTargetSkin(%Boss.target, 'Horde');
   
   %Boss.setTransform(%Cpos);
   %Boss.phase = 1;
   MissionCleanup.add(%Boss);
   
   %Boss.lastKillCount = $TWM2::BossManager.bossKills;
   %Boss.canSummonMinions = true;
   $TWM2::VardisonManager.Vardison = %Boss;
   
   %Boss.thinkSched = schedule(2500, 0, VardisonThink, %Boss);
}

function SpawnVardison2(%position) {
   schedule(250, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(500, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(750, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(1000, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(1100, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");

   %Boss = new player(){
      Datablock = "VardisonStageTwoArmor";
   };
   %Cpos = vectorAdd(%position, "0 0 5");

   %Boss.isMultiPhaseBoss = true;
   %Boss.isFirstPhase = false;
   %Boss.isFinalPhase = false;

   InitiateBoss(%Boss, "Vardison2");

   %Boss.team = 30;
   %Boss.target = createTarget(%Boss, "\c7Lord Vardison", "", "Derm3", '', %Boss.team, PlayerSensor);
   setTargetSensorData(%Boss.target, PlayerSensor);
   setTargetSensorGroup(%Boss.target, 30);
   setTargetName(%Boss.target, addtaggedstring("\c7Lord Vardison"));
   setTargetSkin(%Boss.target, 'Horde');

   %Boss.setTransform(%Cpos);
   %Boss.phase = 2;
   MissionCleanup.add(%Boss);

   %Boss.lastKillCount = $TWM2::BossManager.bossKills;
   %Boss.canSummonMinions = true;
   $TWM2::VardisonManager.Vardison = %Boss;
   
   //If we're on difficulty level 4, summon an orb :P
   if($TWM2::VardisonDifficulty >= 4) {
      VardisonSummonOrb(%Boss);
   }

   %Boss.thinkSched = schedule(1000, 0, VardisonThink, %Boss);
}

function SpawnVardison3(%position) {
   schedule(250, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(500, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(750, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(1000, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(1100, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(1250, 0, MessageAll, 'msgSound', "~wfx/armor/breath_bio_uw.wav");
   schedule(1250, 0, MessageAll, 'msgSound', "~wfx/explosions/explosion.xpl03.wav");
   schedule(1250, 0, MessageAll, 'msgSound', "~wfx/explosions/explosion.xpl03.wav");
   schedule(1250, 0, MessageAll, 'msgSound', "~wfx/explosions/explosion.xpl03.wav");

   %Boss = new player(){
      Datablock = "VardisonStageThreeArmor";
   };
   %Cpos = vectorAdd(%position, "0 0 5");

   %Boss.isMultiPhaseBoss = true;
   %Boss.isFirstPhase = false;
   %Boss.isFinalPhase = true;

   InitiateBoss(%Boss, "Vardison3");

   %Boss.team = 30;
   %Boss.target = createTarget(%Boss, "\c7Lord Vardison", "", "Derm3", '', %Boss.team, PlayerSensor);
   setTargetSensorData(%Boss.target, PlayerSensor);
   setTargetSensorGroup(%Boss.target, 30);
   setTargetName(%Boss.target, addtaggedstring("\c7Lord Vardison"));
   setTargetSkin(%Boss.target, 'Horde');

   %Boss.setTransform(%Cpos);
   %Boss.phase = 3;
   MissionCleanup.add(%Boss);

   %Boss.lastKillCount = $TWM2::BossManager.bossKills;
   %Boss.canSummonMinions = true;
   $TWM2::VardisonManager.Vardison = %Boss;
   
   //If we're on level 3 or 4, instantly summon the Shadow Orb to kick things off
   if($TWM2::VardisonDifficulty >= 3) {
      VardisonSummonOrb(%Boss);
   }

   %Boss.thinkSched = schedule(1000, 0, VardisonThink, %Boss);
}

function VardisonThink(%Boss) {
   if(!isObject(%Boss) || %Boss.getState() $= "Dead") {
      //Kill this think instance, let the next one take over.
      return;
   }
   if(%Boss.busy) {
      //I'm currently doing something, check back soon.
      %Boss.thinkSched = schedule(200, 0, VardisonThink, %Boss);
      return;
   }
   %dLevel = $TWM2::VardisonDifficulty;
   %lastAtt = $TWM2::VardisonManager.lastAttackTime; //<--- Note: This value is from the time the last attack ENDED
   %cTime = getRealTime();
   %lastKC = %Boss.lastKillCount;
   %nowKC = $TWM2::BossManager.bossKills;
   %Boss.lastKillCount = $TWM2::BossManager.bossKills;
   //Possible Outcomes...
   %needAttack = false;
   %needMinions = false;
   %needOrbSummon = false;
   %needMove = false;
   //Check for anti-fall....
   if(getWord(%Boss.getPosition(), 2) < (getTerrainHeight(%Boss.getPosition()) - 250)) {
      if(getWord(%Boss.getVelocity(), 2) < -1) {
         //Block the fall... bring us back up...
         echo("VardisonThink(): Anti-Fall Countermeasures activated.");
         %Boss.setPosition(getWord(%Boss.getPosition(), 0) SPC getWord(%Boss.getPosition(), 1) SPC getTerrainHeight(%Boss.getPosition()) + 15);
         %Boss.setVelocity("0 0 1");
         %Boss.startfade(0, 0, true);
         %Boss.schedule(250, startfade, 1000, 0, false);
      }
   }
   //Determine the correct action.
   switch(%Boss.phase) {
      case 1:
         //Did I just get a kill?
         if(%nowKC > %lastKC) {
            //Do I need to summon a shadow orb?
            if($TWM2::Vardison1_CanOrb[%dLevel]) {
               //Is there already an active orb?
               if(!isObject($TWM2::VardisonManager.orbObject)) {
                  //Yep, I need to summon it.
                  %needOrbSummon = true;
               }
            }
         }
         if((%cTime - %lastAtt) >= $TWM2::Vardison1_AttSpeed[%dLevel]) {
            //Time for an attack
            %needAttack = true;
         }
         else {
            //If we're not ready to dish out an attack, let's check what else we need to do...
            // Are we low on minions?
            if($TWM2::VardisonManager.minionCount < $TWM2::Vardison1_MaxMinions[%dLevel]) {
               if(%Boss.canSummonMinions) {
                  //How many players am I up against?
                  %pCount = $HostGamePlayerCount;
                  //How low is my health?
                  %percentage = mCeil((mFloor(%boss.getDamageLeft()*100) / mFloor(%boss.getMaxDamage()*100)) * 100);
                  %BackwardsHP = 100 - %percentage;
                  %chance = %BackwardsHP * %pCount;
                  //Using our test factor, determine if I need minions.
                  if(%chance <= getRandom(1, 100)) {
                     %needMinions = true;
                  }
               }
            }
            //Do I need to be moving towards the enemy?
            %needMove = true; //Phase 1 is always doing this...
         }
      case 2:
         //Did I just get a kill?
         if(%nowKC > %lastKC) {
            //Do I need to summon a shadow orb?
            if($TWM2::Vardison2_CanOrb[%dLevel]) {
               //Is there already an active orb?
               if(!isObject($TWM2::VardisonManager.orbObject)) {
                  //Yep, I need to summon it.
                  %needOrbSummon = true;
               }
            }
         }
         if((%cTime - %lastAtt) >= $TWM2::Vardison2_AttSpeed[%dLevel]) {
            //Time for an attack
            %needAttack = true;
         }
         else {
            //If we're not ready to dish out an attack, let's check what else we need to do...
            // Are we low on minions?
            if($TWM2::VardisonManager.minionCount < $TWM2::Vardison2_MaxMinions[%dLevel]) {
               if(%Boss.canSummonMinions) {
                  //How many players am I up against?
                  %pCount = $HostGamePlayerCount;
                  //How low is my health?
                  %percentage = mCeil((mFloor(%boss.getDamageLeft()*100) / mFloor(%boss.getMaxDamage()*100)) * 100);
                  %BackwardsHP = 100 - %percentage;
                  %chance = %BackwardsHP * %pCount;
                  //Using our test factor, determine if I need minions.
                  if(%chance <= getRandom(1, 100)) {
                     %needMinions = true;
                  }
               }
            }
            %needMove = true;
         }
      case 3:
         //Did I just get a kill?
         if(%nowKC > %lastKC) {
            //Do I need to summon a shadow orb?
            if($TWM2::Vardison3_CanOrb[%dLevel]) {
               //Is there already an active orb?
               if(!isObject($TWM2::VardisonManager.orbObject)) {
                  //Yep, I need to summon it.
                  %needOrbSummon = true;
               }
            }
         }
         if((%cTime - %lastAtt) >= $TWM2::Vardison3_AttSpeed[%dLevel]) {
            //Time for an attack
            %needAttack = true;
         }
         else {
            //If we're not ready to dish out an attack, let's check what else we need to do...
            // Are we low on minions?
            if($TWM2::VardisonManager.minionCount < $TWM2::Vardison3_MaxMinions[%dLevel] && %boss.canSummonMinions) {
               if(%Boss.canSummonMinions) {
                  //How many players am I up against?
                  %pCount = $HostGamePlayerCount;
                  //How low is my health?
                  %percentage = mCeil((mFloor(%boss.getDamageLeft()*100) / mFloor(%boss.getMaxDamage()*100)) * 100);
                  %BackwardsHP = 100 - %percentage;
                  %chance = %BackwardsHP * %pCount;
                  //Using our test factor, determine if I need minions.
                  if(%chance <= getRandom(1, 100)) {
                     %needMinions = true;
                  }
               }
            }
            //Do I need to be moving towards the enemy?
            %needMove = true; //Phase 3 is always doing this...
         }
   }
   //Call the correct function...
   if(%needOrbSummon) {
      VardisonSummonOrb(%Boss);
   }
   else if(%needAttack) {
      VardisonPerformAttack(%Boss);
   }
   else {
      if(%needMinions) {
         VardisonSummonMinions(%Boss);
      }
      if(%needMove) {
         VardisonDoMove(%Boss);
      }
   }
   %Boss.thinkSched = schedule(500, 0, VardisonThink, %Boss);
}

function VardisonDoMove(%Boss) {
   if(!isObject(%Boss) || %Boss.getState() $= "Dead") {
      return;
   }
   %pos = %Boss.getworldboxcenter();
   switch(%Boss.phase) {
      case 1:
         %closest = VardisonGetClosest(%Boss);
         %clDst = getWord(%closest, 1);
         %clPlayer = getWord(%closest, 0).Player;
         if(%clDst < 12) {
            //Perform a sword slam
            VardisonDoSlam(%Boss, %clPlayer);
            return;
         }
         if(%Boss.hastarget != 1){
            %Boss.hastarget = 1;
         }
         %vector = ZgetFacingDirection(%Boss, %clPlayer, %pos);
         %vector = vectorscale(%vector, $Zombie::DForwardSpeed);
         %upvec = "150";
         %x = Getword(%vector,0);
         %y = Getword(%vector,1);
         %z = Getword(%vector,2);
         if(%z >= ($Zombie::DForwardSpeed)) {
            %upvec = (%upvec * 5);
         }
         %vector = %x@" "@%y@" "@%upvec;
         %Boss.applyImpulse(%pos, %vector);
         
      case 2:
         %closest = VardisonGetClosest(%Boss);
         %clDst = getWord(%closest, 1);
         %clPlayer = getWord(%closest, 0).Player;
         if(%clDst < 15) {
            //Perform a sword slam
            VardisonDoSlam(%Boss, %clPlayer);
            return;
         }
         if(%Boss.hastarget != 1){
            %Boss.hastarget = 1;
         }
         %vector = ZgetFacingDirection(%Boss, %clPlayer, %pos);
         %vector = vectorscale(%vector, $Zombie::DForwardSpeed*2);
         %upvec = "150";
         %x = Getword(%vector,0);
         %y = Getword(%vector,1);
         %z = Getword(%vector,2);
         if(%z >= ($Zombie::DForwardSpeed)) {
            %upvec = (%upvec * 5);
         }
         %vector = %x@" "@%y@" "@%upvec;
         %Boss.applyImpulse(%pos, %vector);
         
      case 3:
         %closest = VardisonGetClosest(%Boss);
         %clDst = getWord(%closest, 1);
         %clPlayer = getWord(%closest, 0).Player;
         if(%clDst < 6) {
            //Insta-Kill xD
            %clPlayer.setInvincible(false);
            MessageAll('msgDie', "\c4"@$TWM2::BossNameInternal["Vardison"]@": DIE "@getWord(%closest, 0).namebase@"!!!");
            %clPlayer.damage(%boss, %clPlayer.getPosition(), 10000, $DamageType::Idiocy);
            return;
         }
         if(%Boss.hastarget != 1){
            %Boss.hastarget = 1;
         }
         %vector = ZgetFacingDirection(%Boss, %clPlayer, %pos);
         %vector = vectorscale(%vector, $Zombie::DForwardSpeed*5);
         %upvec = "150";
         %x = Getword(%vector,0);
         %y = Getword(%vector,1);
         %z = Getword(%vector,2);
         if(%z >= ($Zombie::DForwardSpeed)) {
            %upvec = (%upvec * 5);
         }
         %vector = %x@" "@%y@" "@%upvec;
         %Boss.applyImpulse(%pos, %vector);
   }
}

function VardisonDoSlam(%Boss, %ToDie) {
   %Boss.busy = true;
   %vector = ZgetFacingDirection(%Boss, %ToDie, %Boss.getWorldBoxCenter());
   %vector = vectorscale(%vector, $Zombie::DForwardSpeed * 7.5);
   %upvec = "150";
   %x = Getword(%vector,0);
   %y = Getword(%vector,1);
   %z = Getword(%vector,2);
   if(%z >= ($Zombie::DForwardSpeed)) {
      %upvec = (%upvec * 5);
   }
   %vector = %x@" "@%y@" "@%upvec;
   %Boss.applyImpulse(%pos, %vector);
   schedule(750, 0, "VardisonFinishSlam", %Boss, %ToDie);
}

function VardisonFinishSlam(%Boss, %ToDie) {
   //Summon the invis-projectile
   %Boss.setMoveState(true);
   %p = new LinearFlareProjectile() {
      dataBlock        = ShadowBladeSlam;
      initialDirection = "0 0 -10";
      initialPosition  = vectorAdd(%ToDie.getPosition(), "0 0 1");
      sourceSlot       = 0;
   };
   %p.sourceObject = %Boss;
   MissionCleanup.add(%p);
   %Boss.schedule(1000, "setMoveState", false);
   ServerPlay3d(BOVHitSound, %ToDie.getPosition());
   ServerPlay3d(BOVHitSound, %ToDie.getPosition());
   ServerPlay3d(BOVHitSound, %ToDie.getPosition());
   $TWM2::VardisonManager.schedule(1000, cooldownOff, %Boss, "busy");
}

function VardisonSummonOrb(%Boss) {
   if(isObject($TWM2::VardisonManager.orbObject)) {
      //Stop, we already have an orb.
      return;
   }
   //Shadow Orb Time...
   MessageAll('msgAttack', "\c5Alert: Lord Vardison Opens The Shadow Rift...");
   schedule(250, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(700, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(1250, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(1350, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(1500, 0, MessageAll, 'msgSound', "~wfx/environment/ctmelody4.wav");
   schedule(2000, 0, MessageAll, 'msgSound', "~wfx/explosions/explosion.xpl03.wav");
   schedule(2000, 0, MessageAll, 'msgSound', "~wfx/explosions/explosion.xpl03.wav");
   schedule(2000, 0, MessageAll, 'msgSound', "~wfx/explosions/explosion.xpl03.wav");
   %Boss.busy = true;
   %Boss.rapierShield = true;
   %Boss.setMoveState(true);
   //Fire SFX
   $TWM2::VardisonManager.orbFire = new ParticleEmissionDummy(){
      position = vectoradd(%Boss.getPosition(), "0 0 0.5");
      dataBlock = "defaultEmissionDummy";
      emitter = "SummoningPierEmitter";
   };
   MissionCleanup.add($TWM2::VardisonManager.orbFire);
   //Create the Orb Object
   $TWM2::VardisonManager.schedule(2000, summonOrb, %Boss);
}

function VardisonSummonMinions(%Boss) {
   if(%Boss.canSummonMinions) {
      %currentCount = $TWM2::VardisonManager.minionCount;
      %dLevel = $TWM2::VardisonDifficulty;
      %max = 0;
      switch(%Boss.phase) {
         case 1:
            %max = $TWM2::Vardison1_MaxMinions[%dLevel];
         case 2:
            %max = $TWM2::Vardison2_MaxMinions[%dLevel];
         case 3:
            %max = $TWM2::Vardison3_MaxMinions[%dLevel];
      }
      %factor = %dLevel / 4;
      %Low = 1;
      %High = mCeil((%max - %currentCount) * %factor);
      for(%i = 0; %i < getRandom(%Low, %High); %i++) {
         VardisonDoMinionSummon(%Boss);
      }
      %Boss.canSummonMinions = false;
      $TWM2::VardisonManager.schedule($TWM2::Vardison_MinionCooldown[%dLevel] * 1000, cooldownOff, %Boss, "minions");
   }
}

function VardisonDoMinionSummon(%Boss) {
   %posSpawn = vectorAdd(%Boss.getPosition(), TWM2Lib_MainControl("getRandomPosition", 50 TAB 1));
   %spawnFire = new ParticleEmissionDummy(){
      position = vectoradd(%posSpawn, "0 0 0.5");
      dataBlock = "defaultEmissionDummy";
      emitter = "SummoningPierEmitter";
   };
   MissionCleanup.add(%spawnFire);
   %spawnFire.schedule(500, "delete");
   schedule(650, 0, SpawnVMinion, %posSpawn);
}

function SpawnVMinion(%position) {
   %minion = TWM2Lib_Zombie_Core("SpawnZombie", "zSpawnCommand", 17, %position);
   if(isObject(%minion)) {
      //Apply minion settings & increase count
      $TWM2::VardisonManager.minionCount++;
      %minion.isBossMinion = true;
      %minion.isVardisonMinion = true;
   }
}

function VardisonGetClosest(%Boss) {
   %wbpos = %Boss.getworldboxcenter();
   %count = ClientGroup.getCount();
   %closestClient = -1;
   %closestDistance = 32767;
   for(%i = 0; %i < %count; %i++) {
      %cl = ClientGroup.getObject(%i);
      if(isObject(%cl.player)){
         %testPos = %cl.player.getWorldBoxCenter();
         %distance = vectorDist(%wbpos, %testPos);
         if (%distance > 0 && %distance < %closestDistance) {
            %closestClient = %cl;
            %closestDistance = %distance;
         }
      }
   }
   return %closestClient SPC %closestDistance;
}

function VardisonPerformAttack(%Boss) {
   //The do-all opening function for Vardison's attacks
   //Start by halting the think() method
   %Boss.busy = true;
   switch(%Boss.phase) {
      case 1:
         %attackSelect = getRandom(1, 5);
         switch(%attackSelect) {
            case 1:
               //Shadow Bomb Strike
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player;
                  VardisonNamedAttack(%Boss, "ShadowBombStrike", %tPl);
                  $TWM2::VardisonManager.schedule(4000, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }
               
            case 2:
               //Nightmare Missile
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player;
                  VardisonNamedAttack(%Boss, "NightmareMissile", %tPl);
                  $TWM2::VardisonManager.schedule(4500, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }
               
            case 3:
               //Shadow Fissure
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player;
                  %tPos = %tPl.getPosition();
                  %bPos = %Boss.getPosition();
                  %fVec = vectorNormalize(vectorSub(%tPos, %bPos));
                  //Phase Vardison Out To Safety
                  %fire = new ParticleEmissionDummy(){
                     position = vectoradd(%spawnPos, "0 0 0.5");
                     dataBlock = "defaultEmissionDummy";
                     emitter = "SummoningPierEmitter";
                  };
                  MissionCleanup.add(%fire);
                  %fire.schedule(6500, "delete");
                  //Fades & Teleports
                  %Boss.startfade(1500, 0, true);
                  %Boss.schedule(2000, setPosition, vectorAdd(%bPos, "9999 9999 10"));
                  schedule(3000, 0, "VardisonNamedAttack", %Boss, "ShadowFissure", %bPos TAB %fVec);
                  %Boss.schedule(6000, setPosition, %bPos);
                  %Boss.schedule(6000, startFade, 1000, 0, false);
                  //
                  $TWM2::VardisonManager.schedule(7000, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }
               
            case 4:
               //4x Seeker Photon
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player;
                  schedule(1000, 0, "VardisonNamedAttack", %Boss, "SeekerPhoton", %tPl);
                  schedule(2000, 0, "VardisonNamedAttack", %Boss, "SeekerPhoton", %tPl);
                  schedule(3000, 0, "VardisonNamedAttack", %Boss, "SeekerPhoton", %tPl);
                  schedule(4000, 0, "VardisonNamedAttack", %Boss, "SeekerPhoton", %tPl);
                  $TWM2::VardisonManager.schedule(5000, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }
               
            case 5:
               //Fire Missile
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player;
                  VardisonNamedAttack(%Boss, "FireSeeker", %tPl);
                  $TWM2::VardisonManager.schedule(4500, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }
         }
         
      case 2:
         %attackSelect = getRandom(1, 8);
         switch(%attackSelect) {
            case 1:
               //Shadow Bomb Strike
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player;
                  VardisonNamedAttack(%Boss, "ShadowBombStrike", %tPl);
                  $TWM2::VardisonManager.schedule(4000, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }

            case 2:
               //Nightmare Missile
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player;
                  VardisonNamedAttack(%Boss, "NightmareMissile", %tPl);
                  $TWM2::VardisonManager.schedule(4500, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }

            case 3:
               //Shadow Fissure
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player;
                  %tPos = %tPl.getPosition();
                  %bPos = %Boss.getPosition();
                  %fVec = vectorNormalize(vectorSub(%tPos, %bPos));
                  //Phase Vardison Out To Safety
                  %fire = new ParticleEmissionDummy(){
                     position = vectoradd(%spawnPos, "0 0 0.5");
                     dataBlock = "defaultEmissionDummy";
                     emitter = "SummoningPierEmitter";
                  };
                  MissionCleanup.add(%fire);
                  %fire.schedule(6500, "delete");
                  //Fades & Teleports
                  %Boss.startfade(1500, 0, true);
                  %Boss.schedule(2000, setPosition, vectorAdd(%bPos, "9999 9999 10"));
                  schedule(3000, 0, "VardisonNamedAttack", %Boss, "ShadowFissure", %bPos TAB %fVec);
                  %Boss.schedule(6000, setPosition, %bPos);
                  %Boss.schedule(6000, startFade, 1000, 0, false);
                  //
                  $TWM2::VardisonManager.schedule(7000, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }

            case 4:
               //4x Seeker Photon
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player;
                  schedule(1000, 0, "VardisonNamedAttack", %Boss, "SeekerPhoton", %tPl);
                  schedule(2000, 0, "VardisonNamedAttack", %Boss, "SeekerPhoton", %tPl);
                  schedule(3000, 0, "VardisonNamedAttack", %Boss, "SeekerPhoton", %tPl);
                  schedule(4000, 0, "VardisonNamedAttack", %Boss, "SeekerPhoton", %tPl);
                  $TWM2::VardisonManager.schedule(5000, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }

            case 5:
               //Minion Army
               VardisonNamedAttack(%Boss, "MinionFlood", "");

            case 6:
               //Stasis Gate
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player.getPosition();
                  VardisonNamedAttack(%Boss, "StasisGate", %tPl);
                  $TWM2::VardisonManager.schedule(10000, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }

            case 7:
               //Rift Gate
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player.getPosition();
                  VardisonNamedAttack(%Boss, "RiftGate", %tPl);
                  $TWM2::VardisonManager.schedule(10000, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }

            case 8:
               //SGF
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player;
                  VardisonNamedAttack(%Boss, "SolomentaryGravityFlux", %tPl TAB 0);
                  $TWM2::VardisonManager.schedule(5000, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }
         }

      case 3:
         %attackSelect = getRandom(1, 7);
         switch(%attackSelect) {
            case 1:
               //Shadow Bomb Strike
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player;
                  VardisonNamedAttack(%Boss, "ShadowBombStrike", %tPl);
                  $TWM2::VardisonManager.schedule(4000, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }

            case 2:
               //Nightmare Missile
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player;
                  VardisonNamedAttack(%Boss, "NightmareMissile", %tPl);
                  $TWM2::VardisonManager.schedule(4500, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }

            case 3:
               //Shadow Fissure
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player;
                  %tPos = %tPl.getPosition();
                  %bPos = %Boss.getPosition();
                  %fVec = vectorNormalize(vectorSub(%tPos, %bPos));
                  //Phase Vardison Out To Safety
                  %fire = new ParticleEmissionDummy(){
                     position = vectoradd(%spawnPos, "0 0 0.5");
                     dataBlock = "defaultEmissionDummy";
                     emitter = "SummoningPierEmitter";
                  };
                  MissionCleanup.add(%fire);
                  %fire.schedule(6500, "delete");
                  //Fades & Teleports
                  %Boss.startfade(1500, 0, true);
                  %Boss.schedule(2000, setPosition, vectorAdd(%bPos, "9999 9999 10"));
                  schedule(3000, 0, "VardisonNamedAttack", %Boss, "ShadowFissure", %bPos TAB %fVec);
                  %Boss.schedule(6000, setPosition, %bPos);
                  %Boss.schedule(6000, startFade, 1000, 0, false);
                  //
                  $TWM2::VardisonManager.schedule(7000, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }

            case 4:
               //4x Seeker Photon
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player;
                  schedule(1000, 0, "VardisonNamedAttack", %Boss, "SeekerPhoton", %tPl);
                  schedule(2000, 0, "VardisonNamedAttack", %Boss, "SeekerPhoton", %tPl);
                  schedule(3000, 0, "VardisonNamedAttack", %Boss, "SeekerPhoton", %tPl);
                  schedule(4000, 0, "VardisonNamedAttack", %Boss, "SeekerPhoton", %tPl);
                  $TWM2::VardisonManager.schedule(5000, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }

            case 5:
               //Laser Drop
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player;
                  VardisonNamedAttack(%Boss, "LaserDrop", %tPl);
                  $TWM2::VardisonManager.schedule(5000, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }
               
            case 6:
               //Laser Wall
               %Boss.setMoveState(true);
               %target = FindValidTarget(%Boss);
               if(isObject(%target)) {
                  %tPl = %target.player;
                  for(%i = 0; %i < 25; %i++) {
                     schedule(50+(%i*150), 0, VardisonNamedAttack, %Boss, "LaserWall", %tPl);
                  }
                  $TWM2::VardisonManager.schedule(10000, cooldownOff, %Boss, "attackFinished");
               }
               else {
                  %Boss.setMoveState(false);
                  %Boss.busy = false;
               }
               
            case 7:
               //Gravity Well
               %Boss.setMoveState(true);
               VardisonNamedAttack(%Boss, "GravityWell", 0);
               $TWM2::VardisonManager.schedule(5000, cooldownOff, %Boss, "attackFinished");
         }
      
   }
}

function VardisonNamedAttack(%Boss, %attack, %args) {
   if(!isObject(%Boss) || %Boss.getState() $= "Dead") {
      return;
   }
   %bPos = %Boss.getPosition();
   
   switch$(%attack) {
      case "ShadowBombStrike":
         //Spawn 4 Pulses Above Vardison Fired at a guy
         %target = getField(%args, 0);
         %sPos = vectorAdd(%bPos, "0 0 25");
         %vec = vectorNormalize(vectorSub(%target.getPosition(), %sPos));
         for(%i = 0; %i < 4; %i++) {
            %p = new LinearFlareProjectile() {
               dataBlock        = ShadowBlastBolt;
               initialDirection = vectorScale(%vec, 10);
               initialPosition  = %sPos;
               sourceSlot       = 4;
            };
            %p.sourceObject = %Boss;
            MissionCleanup.add(%p);
         }
         
      case "NightmareMissile":
         %target = getField(%args, 0);
         %sPos = vectorAdd(%bPos, "0 0 25");
         %vec = vectorNormalize(vectorSub(%target.getPosition(), %sPos));
   	     createMissileSeekingProjectile("YvexNightmareMissile", %target, %Boss, %Boss.getMuzzlePoint(4), %vec, 4, 100);
         
      case "ShadowFissure":
         %sPos = getField(%args, 0);
         %fVec = getField(%args, 1);
         for(%i = 1; %i < 20; %i++) {
            %sPos = vectorAdd(%sPos, "0 0 0.5");
            %sPos = vectorAdd(%sPos, vectorScale(%fVec, %i*3));
            %p = new LinearFlareProjectile() {
               dataBlock        = ShadowBlastBolt;
               initialDirection = "0 0 -15";
               initialPosition  = %sPos;
               sourceSlot       = 4;
            };
            %p.sourceObject = %Boss;
            MissionCleanup.add(%p);
         }
         
      case "SeekerPhoton":
         %target = getField(%args, 0);
         FireSeekerPhotons(%Boss, %target);
         
      case "FireSeeker":
         %target = getField(%args, 0);
         %sPos = vectorAdd(%bPos, "0 0 25");
         %vec = vectorNormalize(vectorSub(%target.getPosition(), %sPos));
         createMissileSeekingProjectile("VegenorFireMissile", %target, %Boss, %Boss.getMuzzlePoint(4), %vec, 4, 100);
         
      case "MinionFlood":
         //Summon Maximum Minions & then Phase out until the team drops them
         %max = $TWM2::Vardison2_MaxMinions[$TWM2::VardisonDifficulty];
         %lowEnd = mFloor(%max * 0.25);
         %retPos = %bPos;
         %current = $TWM2::VardisonManager.minionCount;
         %need = %max - %current;
         for(%i = 0; %i < %need; %i++) {
            VardisonDoMinionSummon(%Boss);
         }
         //Meanwhile, I take my leave :)
         %Boss.startfade(1500, 0, true);
         %Boss.rapierShield = true;
         %Boss.schedule(2000, setPosition, vectorAdd(%bPos, "9999 9999 10"));
         %Boss.vmcountLoop = schedule(1000, 0, VardisonMinionCountCheckup, %Boss, %retPos, %lowEnd);
         
      case "StasisGate":
         %pos = getField(%args, 0);
         %TargetSearchMask = $TypeMasks::PlayerObjectType;
         %c = createEmitter(%pos, FlashLEmitter, "1 0 0");      //Rotate it
         %c.schedule(1000, delete);
         InitContainerRadiusSearch(%pos, 25, %TargetSearchMask);
         while ((%potentialTarget = ContainerSearchNext()) != 0){
            if (!%potentialTarget.isZombie && !%potentialTarget.isBoss && !%potentialTarget.isVardisonMinion) {
               VardisonNamedAttack(%Boss, "StasisLoop", %potentialTarget TAB 0);
            }
         }
         
      case "StasisLoop":
         %pl = getField(%args, 0);
         if(!isObject(%pl) || %pl.getState() $= "dead") {
            return;
         }
         %counter = getField(%args, 1);
         %counter++;
         if(%counter > 10) {
            %pl.setMoveState(false);
            return;
         }
         %c = createEmitter(%pl.getPosition(), PBCExpEmitter, "1 0 0");      //Rotate it
         %c.schedule(1000, delete);
         %pl.setMoveState(true);
         schedule(1000, 0, VardisonNamedAttack, %Boss, %attack, %pl TAB %counter);
      
      case "RiftGate":
         %pos = getField(%args, 0);
         %goPos = TWM2Lib_MainControl("RMPG");
         %TargetSearchMask = $TypeMasks::PlayerObjectType;
         %c = createEmitter(%pos, FlashLEmitter, "1 0 0");      //Rotate it
         %c.schedule(1000, delete);
         %c2 = createEmitter(%goPos, FlashLEmitter, "1 0 0");      //Rotate it
         %c2.schedule(1000, delete);
         InitContainerRadiusSearch(%pos, 25, %TargetSearchMask);
         while ((%potentialTarget = ContainerSearchNext()) != 0){
            if (!%potentialTarget.isZombie && !%potentialTarget.isBoss && !%potentialTarget.isVardisonMinion) {
               %PosAdd = vectorNormalize(vectorSub(%pos, %potentialTarget.getPosition()));
               %PosDst = vectorDist(%pos, %potentialTarget.getPosition());
               %newPos = vectorAdd(%goPos, vectorScale(%PosAdd, %PosDst));
               %potentialTarget.setPosition(%newPos);
            }
         }
      
      case "LaserDrop":
         %toDie = getField(%args, 0);
         if(!isObject(%toDie) || %toDie.getState() $= "dead") {
            return;
         }
         %p = new LinearFlareProjectile() {
            dataBlock        = HyperDevestatorBeam;
            initialDirection = "0 0 -10";
            initialPosition  = vectoradd(%toDie.getPosition(), "0 0 500");
            sourceSlot       = 4;
         };
         %p.sourceObject     = %boss;
         MissionCleanup.add(%p);
      
      case "LaserWall":
         %toDie = getField(%args, 0);
         if(!isObject(%toDie) || %toDie.getState() $= "dead") {
            return;
         }
         %vec = vectorNormalize(vectorSub(%toDie.getPosition(), %bPos));
         %p = new LinearFlareProjectile() {
            dataBlock        = SuperlaserProjectile;
            initialDirection = %vec;
            initialPosition  = %bPos;
            sourceObject     = %boss;
            sourceSlot       = 4;
         };
         MissionCleanup.add(%p);
      
      case "SolomentaryGravityFlux":
         %target = getField(%args, 0);
         if(!isObject(%target) || %target.getState() $= "dead") {
            return;
         }
         %counter = getField(%args, 1);
         %counter++;
         if(%counter < 15) {
            %target.setVelocity("0 0 300");
         }
         else if(%counter > 25) {
            //Stop...
            return;
         }
         else {
            %target.setVelocity("0 0 -300");
         }
         schedule(150, 0, VardisonNamedAttack, %Boss, %attack, %target TAB %counter);
      
      case "GravityWell":
         %counter = getField(%args, 0);
         %counter++;
         if(%counter < 15) {
            for(%i = 0; %i < ClientGroup.getCount(); %i++) {
               %cl = ClientGroup.getObject(%i);
               if(isObject(%cl.player) || %cl.player.getState() !$= "dead") {
                  %cl.player.setVelocity("0 0 300");
               }
            }
         }
         else if(%counter > 25) {
            //Stop...
            return;
         }
         else {
            for(%i = 0; %i < ClientGroup.getCount(); %i++) {
               %cl = ClientGroup.getObject(%i);
               if(isObject(%cl.player) || %cl.player.getState() !$= "dead") {
                  %cl.player.setVelocity("0 0 -300");
               }
            }
         }
         schedule(150, 0, VardisonNamedAttack, %Boss, %attack, %counter);

      default:
         error("Invalid vardison attack...");
   }
}

function VardisonMinionCountCheckup(%boss, %retPos, %lowEnd) {
   if($TWM2::VardisonManager.minionCount < %lowEnd) {
      //Return to the arena
      %Boss.schedule(1500, setPosition, %retPos);
      %Boss.schedule(1500, startFade, 1000, 0, false);
      %Boss.rapierShield = false;
      $TWM2::VardisonManager.schedule(7000, cooldownOff, %Boss, "attackFinished");
      return;
   }
   %boss.vmcountLoop = schedule(1000, 0, VardisonMinionCountCheckup, %boss, %retPos, %lowEnd);
}

//Vardison Manager Functions
//SummonOrb()
function VardisonManager::summonOrb(%this, %boss) {
   %summonPos = vectorAdd(%boss.getPosition(), "0 0 35");
   %orb = new (StaticShape)() {
      dataBlock = ShadowOrb;
   };
   %orb.setTransform(%summonPos SPC "0 0 0 1");
   %orb.team = %boss.Team;
   %orb.setOwner(%boss);

   %orb.team = 30;
   %orb.target = createTarget(%orb, "\c7Shadow Rift", "", "Derm3", '', %orb.team, PlayerSensor);
   setTargetSensorData(%orb.target, PlayerSensor);
   setTargetSensorGroup(%orb.target, 30);
   setTargetName(%orb.target, addtaggedstring("\c7Shadow Rift"));
   
   %orb.waypoint = new WayPoint() {
      position = %orb.getPosition();
      dataBlock = "WayPointMarker";
      team = %boss.Team;
      name = "Shadow Rift";
   };
   
   %orb.startfade(1, 0, true);
   
   //SFX
   $TWM2::VardisonManager.OrbSFX = new ParticleEmissionDummy(){
      position = %orb.getPosition();
      dataBlock = "defaultEmissionDummy";
      emitter = "ShadowOrbEmitter";
   };
   MissionCleanup.add($TWM2::VardisonManager.OrbSFX);
   //
   %this.orbKillSched = %this.schedule($TWM2::Vardison_OrbKillTime, orbKill, %boss, %orb);
}

function VardisonManager::orbKill(%this, %boss, %orb) {
   %restoreCount = 0;
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
	  %cl.bossProficiency.orbDetonates++;
      if(isObject(%cl.player) && %cl.player.getState() !$= "dead") {
         //Bye Bye :)
         %cl.player.rapierShield = false;
         %cl.player.setInvincible(false);
         %cl.player.damage(%boss, %cl.player.getPosition(), 10000, $DamageType::ShadowOrb);
         %cl.player.blowup();
         MessageAll('msgDeath', "\c2"@%cl.namebase@" has been annihilated by the Shadow Rift.");
         //If Vardison Restores HP from rift kills, do that now :P
         if($TWM2::Vardison_OrbRegenHP[$TWM2::VardisonDifficulty]) {
            %boss.setDamageLevel(%boss.getDamageLevel() - 0.35);
            %restoreCount++;
         }
      }
   }
   if($TWM2::Vardison_OrbRegenHP[$TWM2::VardisonDifficulty]) {
      MessageAll('msgRestore', "\c5Lord Vardison has absorbed the life energy of "@%restoreCount@" combatants.");
   }
   %wipeEmit = new ParticleEmissionDummy(){
      position = %orb.getPosition();
      dataBlock = "defaultEmissionDummy";
      emitter = "ShadowOrbDetonationEmitter";
   };
   %wipeEmit.schedule(500, "delete");
   //Delete the orb & it's effects
   if(isObject(%obj.waypoint)) {
      %obj.waypoint.schedule(500, "delete");
   }
   if(isObject($TWM2::VardisonManager.OrbSFX)) {
      $TWM2::VardisonManager.OrbSFX.schedule(500, "delete");
   }
   %orb.schedule(500, "delete");
   if(isObject(%this.orbFire)) {
      %this.orbFire.schedule(500, "delete");
   }
   %this.orbObject = -1;
   %boss.rapierShield = false;
   %boss.busy = false; //<-- let think() resume...
}

function VardisonManager::orbDestroyed(%this) {
   if(isObject(%this.orbFire)) {
      %this.orbFire.schedule(500, "delete");
   }
   cancel(%this.orbKillSched);
   %this.orbObject = -1;
   %boss = %this.Vardison;
   %boss.rapierShield = false;
   %boss.busy = false; //<-- let think() resume...
   MessageAll('msgDeath', "\c2The Shadow Rift has been Shattered...");
}

function VardisonManager::cooldownOff(%this, %Boss, %type) {
   if(!isObject(%this)) {
      error("VardisonManager::cooldownOff(): Missing instance of VM.");
      return;
   }
   if(!isObject(%Boss) || %Boss.getState() $= "dead") {
      error("VardisonManager::cooldownOff(): Vardison Dead.");
      return;
   }
   switch$(%type) {
      case "minions":
         %Boss.canSummonMinions = true;

      case "busy":
         %Boss.busy = false;
         
      case "attackFinished":
         //Unlock the boss & update LAT
         %Boss.setMoveState(false);
         %this.lastAttackTime = getRealTime();
         %Boss.busy = false;
   }
}

function ShadowOrb::damageObject(%data, %targetObject, %sourceObject, %position, %amount, %damageType) {
   if(%sourceObject && %targetObject.isEnabled()) {
      if(%sourceObject.client) {
         %targetObject.lastDamagedBy = %sourceObject.client;
         %targetObject.lastDamagedByTeam = %sourceObject.client.team;
         %targetObject.damageTimeMS = GetSimTime();
      }
      else {
         %targetObject.lastDamagedBy = %sourceObject;
         %targetObject.lastDamagedByTeam = %sourceObject.team;
         %targetObject.damageTimeMS = GetSimTime();
      }
   }
   if (%data.isShielded) {
      %amount = %data.checkShields(%targetObject, %position, %amount, %damageType);
   }
   %damageScale = %data.damageScale[%damageType];
   if(%damageScale !$= "") {
      %amount *= %damageScale;
   }
   if (%amount > 0) {
      %targetObject.applyDamage(%amount);
   }
}
