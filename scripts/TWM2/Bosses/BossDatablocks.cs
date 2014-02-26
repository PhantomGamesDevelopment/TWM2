//BossDatablocks.cs
//Phantom139
//Contains all datablocks for bosses...

//
// BOSS ARMORS / VEHICLES / OBJECTS
//

// ============================================================================
// YVEX
// ============================================================================
datablock PlayerData(YvexZombieArmor) : LightMaleHumanArmor {
   boundingBox = "1.63 1.63 2.6";
   maxDamage = 500.0;
   minImpactSpeed = 35;
   shapeFile = "medium_male.dts";

   debrisShapeName = "bio_player_debris.dts";

   //Foot Prints
   decalData   = HeavyBiodermFootprint;
   decalOffset = 0.4;

   waterBreathSound = WaterBreathBiodermSound;

   damageScale[$DamageType::W1700] = 3.0;
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs

   max[RepairKit]			= 0;
   max[Mine]				= 0;
   max[Grenade]			= 0;
};

// ============================================================================
// WINDSHEAR
// ============================================================================

// * Windshear's Vehicle Object is handled & Loaded in vehicles/vehicle_WindshearPlatform.cs

// ============================================================================
// GHOST OF LIGHTNING
// ============================================================================
datablock PlayerData(LightningGhostArmor) : MediumMaleHumanArmor {
   boundingBox = "1.63 1.63 2.6";
   maxDamage = 550.0;
   minImpactSpeed = 35;
   shapeFile = "medium_male.dts";

   debrisShapeName = "bio_player_debris.dts";

   //Foot Prints
   decalData   = HeavyBiodermFootprint;
   decalOffset = 0.4;

   waterBreathSound = WaterBreathBiodermSound;

   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs

   max[RepairKit]			= 0;
   max[Mine]				= 0;
   max[Grenade]			= 0;
};

// ============================================================================
// VEGENOR
// ============================================================================
datablock PlayerData(VegenorZombieArmor) : HeavyMaleBiodermArmor {
   maxDamage = 600.0;
   minImpactSpeed = 50;
   speedDamageScale = 0.015;

   damageScale[$DamageType::M1700] = 2.0;
   damageScale[$DamageType::Fire] = 0.1;
   damageScale[$DamageType::Burn] = 0.1;
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs

   max[RepairKit]			= 0;
   max[Mine]			= 0;
   max[Grenade]			= 0;
};

// ============================================================================
// LORD ROG
// ============================================================================
datablock PlayerData(LordRogZombieArmor) : HeavyMaleBiodermArmor {
   boundingBox = "1.63 1.63 2.6";
   maxDamage = 650.0;
   minImpactSpeed = 35;
   shapeFile = "bioderm_heavy.dts";

   debrisShapeName = "bio_player_debris.dts";

   //Foot Prints
   decalData   = HeavyBiodermFootprint;
   decalOffset = 0.4;

   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs

   waterBreathSound = WaterBreathBiodermSound;

   max[RepairKit]			= 0;
   max[Mine]				= 0;
   max[Grenade]			= 0;
};

// ============================================================================
// INSIGNIA
// ============================================================================
datablock StaticShapeData(NoCollideBio) : StaticShapeDamageProfile {
	className = "player";
	shapeFile = "bioderm_light.dts"; // dmiscf.dts, alternate
    mass = 1;
	elasticity = 0.1;
	friction = 0.9;
	collideable = 0;
    isInvincible = true;
};

datablock StaticShapeData(NoCollideHum) : StaticShapeDamageProfile {
	className = "player";
	shapeFile = "light_male.dts"; // dmiscf.dts, alternate
    mass = 1;
	elasticity = 0.1;
	friction = 0.9;
	collideable = 0;
    isInvincible = true;
};

datablock PlayerData(InsigniaZombieArmor) : LightMaleBiodermArmor {
   maxDamage = 700.0;
   minImpactSpeed = 50;
   speedDamageScale = 0.015;

   damageScale[$DamageType::M1700] = 2.0;
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs

   max[RepairKit]			= 0;
   max[Mine]			= 0;
   max[Grenade]			= 0;
};

// ============================================================================
// TREVOR
// ============================================================================
datablock HoverVehicleData(TreborTank) : CentaurVehicle {
   spawnOffset = "0 0 4";
   canControl = true;
   floatingGravMag = 4.5;

   catagory = "Vehicles";
   shapeFile = "vehicle_grav_tank.dts";
   multipassenger = false;
   computeCRC = true;
   renderWhenDestroyed = false;

   mountPose[0] = sitting;
   numMountPoints = 0;  // <-- Ignore this
   isProtectedMountPoint[0] = true;

   maxDamage = 100.15;
   destroyedLevel = 100.15;

   isShielded = true;
   rechargeRate = 1.0;
   energyPerDamagePoint = 135;
   maxEnergy = 400;
   minJetEnergy = 15;
   jetEnergyDrain = 2.0;

   targetNameTag = 'Centaur';
   targetTypeTag = 'MK III';

   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs
   ShieldDamageScale[$DamageType::Bullet] = 0.01;  //I deny you shrike n0bs
};

// ============================================================================
// LORD VARDISON / D.A. VARDISON
// ============================================================================
datablock PlayerData(VardisonStage1Armor) : LightMaleHumanArmor {
   runForce = 60.20 * 90;
   runEnergyDrain = 0.0;
   minRunEnergy = 10;
   maxForwardSpeed = 9;
   maxBackwardSpeed = 7;
   maxSideSpeed = 7;

   jumpForce = 14.0 * 90;

   maxDamage = 300.0;
   minImpactSpeed = 35;
   shapeFile = "light_male.dts";
   jetEmitter = BiodermArmorJetEmitter;
   jetEffect =  BiodermArmorJetEffect;

   debrisShapeName = "bio_player_debris.dts";

   //Foot Prints
   decalData   = LightBiodermFootprint;
   decalOffset = 0.3;

   waterBreathSound = WaterBreathBiodermSound;

   damageScale[$DamageType::M1700] = 3.0;
   damageScale[$DamageType::Missile] = 0.0000000000000001;
   damageScale[$DamageType::Nuclear] = 0.0000000000000001;
   damageScale[$DamageType::EMP] = 0.0000000000000001;
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

datablock PlayerData(DarkArchmageVardisonArmor) : VardisonStage1Armor {
   maxDamage = 1000.0;
   minImpactSpeed = 35;
   shapeFile = "light_male.dts";
   jetEmitter = BiodermArmorJetEmitter;
   jetEffect =  BiodermArmorJetEffect;

   damageScale[$DamageType::M1700] = 3.0;
   damageScale[$DamageType::Missile] = 0.0000000000000001;
   damageScale[$DamageType::Nuclear] = 0.0000000000000001;
   damageScale[$DamageType::EMP] = 0.0000000000000001;
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

datablock FlyingVehicleData(VardisonStage2Flyer) : ShrikeDamageProfile {
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
   minTrailSpeed = 70;      // The speed your contrail shows up at.
   trailEmitter = JetShadowEmitter;
   forwardJetEmitter = JetShadowEmitter;
   downJetEmitter = JetShadowEmitter;

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

   damageScale[$DamageType::Nuclear] = 0.0000000000000001;
   damageScale[$DamageType::EMP] = 0.0000000000000001;
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs
   ShieldDamageScale[$DamageType::Bullet] = 0.01;  //I deny you shrike n0bs

   minMountDist = 7;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_scout_grey";
   targetNameTag = 'Lord Vardison';
   targetTypeTag = '';
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

datablock PlayerData(VardisonStage3Armor) : LightMaleHumanArmor {
   runForce = 60.20 * 90;
   runEnergyDrain = 0.0;
   minRunEnergy = 10;
   maxForwardSpeed = 9;
   maxBackwardSpeed = 7;
   maxSideSpeed = 7;

   jumpForce = 14.0 * 90;

   maxDamage = 500.0;
   minImpactSpeed = 35;
   shapeFile = "TR2Heavy_Male.dts";
   jetEmitter = BiodermArmorJetEmitter;
   jetEffect =  BiodermArmorJetEffect;

   debrisShapeName = "bio_player_debris.dts";

   //Foot Prints
   decalData   = LightBiodermFootprint;
   decalOffset = 0.3;

   boundingBox = "5 5 10";

   waterBreathSound = WaterBreathBiodermSound;

   damageScale[$DamageType::M1700] = 3.0;
   damageScale[$DamageType::PlasmaCannon] = 0.001;
   damageScale[$DamageType::Missile] = 0.0000000000000001;
   damageScale[$DamageType::Nuclear] = 0.0000000000000001;
   damageScale[$DamageType::EMP] = 0.0000000000000001;
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

datablock PlayerData(MiniDemonArmor) : LightMaleHumanArmor {
   runForce = 60.20 * 90;
   runEnergyDrain = 0.0;
   minRunEnergy = 10;
   maxForwardSpeed = 9;
   maxBackwardSpeed = 7;
   maxSideSpeed = 7;

   jumpForce = 14.0 * 90;

   maxDamage = 2.8;
   minImpactSpeed = 1000;
   shapeFile = "bioderm_medium.dts";
   jetEmitter = BiodermArmorJetEmitter;
   jetEffect =  BiodermArmorJetEffect;

   debrisShapeName = "bio_player_debris.dts";

   //Foot Prints
   decalData   = LightBiodermFootprint;
   decalOffset = 0.3;

   waterBreathSound = WaterBreathBiodermSound;

   damageScale[$DamageType::M1700] = 3.0;
   damageScale[$DamageType::PlasmaCannon] = 0.001;
   damageScale[$DamageType::Missile] = 0.0000000000000001;
   damageScale[$DamageType::Nuclear] = 0.0000000000000001;
   damageScale[$DamageType::EMP] = 0.0000000000000001;

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

// ============================================================================
// STORMRIDER
// ============================================================================

// * See notes on Windshear...

// ============================================================================
// GHOST OF FIRE
// ============================================================================
datablock PlayerData(GhostFireArmor) : MediumPlayerDamageProfile {
   emap = true;

   className = Armor;
   shapeFile = "medium_male.dts";
   cameraMaxDist = 3;
   computeCRC = true;

   debrisShapeName = "debris_player.dts";
   debris = HumanRedPlayerDebris;

   canObserve = true;
   cmdCategory = "Clients";
   cmdIcon = CMDPlayerIcon;
   cmdMiniIconName = "commander/MiniIcons/com_player_grey";

   hudImageNameFriendly[0] = "gui/hud_playertriangle";
   hudImageNameEnemy[0] = "gui/hud_playertriangle_enemy";
   hudRenderModulated[0] = true;

   hudImageNameFriendly[1] = "commander/MiniIcons/com_flag_grey";
   hudImageNameEnemy[1] = "commander/MiniIcons/com_flag_grey";
   hudRenderModulated[1] = true;
   hudRenderAlways[1] = true;
   hudRenderCenter[1] = true;
   hudRenderDistance[1] = true;

   hudImageNameFriendly[2] = "commander/MiniIcons/com_flag_grey";
   hudImageNameEnemy[2] = "commander/MiniIcons/com_flag_grey";
   hudRenderModulated[2] = true;
   hudRenderAlways[2] = true;
   hudRenderCenter[2] = true;
   hudRenderDistance[2] = true;

   aiAvoidThis = true;

   minLookAngle = -1.5;
   maxLookAngle = 1.5;
   maxFreelookAngle = 3.0;

   mass = 70;
   drag = 0.3;
   maxdrag = 0.5;
   density = 10;
   maxDamage = 500.0;
   maxEnergy =  400;
   repairRate = 0.0053;
   energyPerDamagePoint = 75.0; // shield energy required to block one point of damage

   rechargeRate = 0.456;
   jetForce = 21.22 * 230;
   underwaterJetForce = 25.22 * 130 * 1.5;
   underwaterVertJetFactor = 1.5;
   jetEnergyDrain = 4.0;
   underwaterJetEnergyDrain =  1.0;
   minJetEnergy = 10;
   maxJetHorizontalPercentage = 0.8;

   runForce = 60 * 150;
   runEnergyDrain = 0;
   minRunEnergy = 0;
   maxForwardSpeed = 18;
   maxBackwardSpeed = 18;
   maxSideSpeed = 18;

   maxUnderwaterForwardSpeed = 10.5;
   maxUnderwaterBackwardSpeed = 9.5;
   maxUnderwaterSideSpeed = 9.5;

   recoverDelay = 4;
   recoverRunForceScale = 0.7;

   // heat inc'ers and dec'ers
   heatDecayPerSec      = 1.0 / 5.0; // takes 4 seconds to clear heat sig.
   heatIncreasePerSec   = 1.0 / 2.0; // takes 3.0 seconds of constant jet to get full heat sig.

   jumpForce = 8.3 * 130;
   jumpEnergyDrain = 0;
   minJumpEnergy = 0;
   jumpSurfaceAngle = 75;
   jumpDelay = 0;

   // Controls over slope of runnable/jumpable surfaces
   runSurfaceAngle  = 85;
   jumpSurfaceAngle = 85;

   minJumpSpeed = 25;
   maxJumpSpeed = 35;

   horizMaxSpeed = 70;
   horizResistSpeed = 28;
   horizResistFactor = 0.32;
   maxJetForwardSpeed = 18;

   upMaxSpeed = 80;
   upResistSpeed = 30;
   upResistFactor = 0.23;

   minImpactSpeed = 45;
   speedDamageScale = 0.006;

   jetSound = ArmorJetSound;
   wetJetSound = ArmorWetJetSound;

   jetEmitter = FlammerArmorJetEmitter; //Pyro jets
   jetEffect = HumanMediumArmorJetEffect;

   boundingBox = "1.45 1.45 2.4";
   pickupRadius = 0.75;

   // damage location details
   boxNormalHeadPercentage       = 0.83;
   boxNormalTorsoPercentage      = 0.49;
   boxHeadLeftPercentage         = 0;
   boxHeadRightPercentage        = 1;
   boxHeadBackPercentage         = 0;
   boxHeadFrontPercentage        = 1;

   //Foot Prints
   decalData   = MediumMaleFootprint;
   decalOffset = 0.35;

   footPuffEmitter = LightPuffEmitter;
   footPuffNumParts = 15;
   footPuffRadius = 0.25;

   dustEmitter = LiftoffDustEmitter;

   splash = PlayerSplash;
   splashVelocity = 4.0;
   splashAngle = 67.0;
   splashFreqMod = 300.0;
   splashVelEpsilon = 0.60;
   bubbleEmitTime = 0.4;
   splashEmitter[0] = PlayerFoamDropletsEmitter;
   splashEmitter[1] = PlayerFoamEmitter;
   splashEmitter[2] = PlayerBubbleEmitter;
   mediumSplashSoundVelocity = 10.0;
   hardSplashSoundVelocity = 20.0;
   exitSplashSoundVelocity = 5.0;

   footstepSplashHeight = 0.35;
   //Footstep Sounds
   LFootSoftSound       = LFootMediumSoftSound;
   RFootSoftSound       = RFootMediumSoftSound;
   LFootHardSound       = LFootMediumHardSound;
   RFootHardSound       = RFootMediumHardSound;
   LFootMetalSound      = LFootMediumMetalSound;
   RFootMetalSound      = RFootMediumMetalSound;
   LFootSnowSound       = LFootMediumSnowSound;
   RFootSnowSound       = RFootMediumSnowSound;
   LFootShallowSound    = LFootMediumShallowSplashSound;
   RFootShallowSound    = RFootMediumShallowSplashSound;
   LFootWadingSound     = LFootMediumWadingSound;
   RFootWadingSound     = RFootMediumWadingSound;
   LFootUnderwaterSound = LFootMediumUnderwaterSound;
   RFootUnderwaterSound = RFootMediumUnderwaterSound;
   LFootBubblesSound    = LFootMediumBubblesSound;
   RFootBubblesSound    = RFootMediumBubblesSound;
   movingBubblesSound   = ArmorMoveBubblesSound;
   waterBreathSound     = WaterBreathMaleSound;

   impactSoftSound      = ImpactMediumSoftSound;
   impactHardSound      = ImpactMediumHardSound;
   impactMetalSound     = ImpactMediumMetalSound;
   impactSnowSound      = ImpactMediumSnowSound;

   skiSoftSound         = SkiAllSoftSound;
   skiHardSound         = SkiAllHardSound;
   skiMetalSound        = SkiAllMetalSound;
   skiSnowSound         = SkiAllSnowSound;

   impactWaterEasy      = ImpactMediumWaterEasySound;
   impactWaterMedium    = ImpactMediumWaterMediumSound;
   impactWaterHard      = ImpactMediumWaterHardSound;

   groundImpactMinSpeed    = 10.0;
   groundImpactShakeFreq   = "4.0 4.0 4.0";
   groundImpactShakeAmp    = "1.0 1.0 1.0";
   groundImpactShakeDuration = 0.8;
   groundImpactShakeFalloff = 10.0;

   exitingWater         = ExitingWaterMediumSound;

   maxWeapons = 2;            // Max number of different weapons the player can have
   maxGrenades = 1;           // Max number of different grenades the player can have
   maxMines = 1;              // Max number of different mines the player can have

   damageScale[$DamageType::plasma] = 0.05;
   damageScale[$DamageType::Burn] = 0.05;
   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs

   // Inventory restrictions
    max[RepairKit]          = 4;
	max[Mine]			    = 0;
	max[ZapMine]			= 0;
	max[CrispMine]			= 5;
	max[flamerAmmoPack]		= 1;
	max[Deagle]				= 1;
	max[SPistol]			= 1;
	max[Pistol]				= 1;
	max[PistolAmmo]			= 10;
	max[Pistolclip]			= 8;
 	max[PulsePhaser]	    = 1;
	max[flamer]				= 1;
	max[flamerAmmo]			= 0;
	max[Napalm]				= 1;
	max[NapalmAmmo]			= 20;
	max[melee]				= 1;
	max[BOV]				= 1;
	max[SOmelee]			= 1;
	max[IncindinaryGrenade]	= 7;
    max[Beacon]             = 3;
   // -END

   observeParameters = "0.5 4.5 4.5";

   shieldEffectScale = "0.7 0.7 1.0";
};

// ============================================================================
// SHADE LORD
// ============================================================================
datablock PlayerData(ShadeLordArmor) : LightMaleHumanArmor {
   boundingBox = "1.63 1.63 2.6";
   maxDamage = 900.0;
   minImpactSpeed = 35;
   shapeFile = "bioderm_heavy.dts";

   debrisShapeName = "bio_player_debris.dts";

   //Foot Prints
   decalData   = HeavyBiodermFootprint;
   decalOffset = 0.4;

   waterBreathSound = WaterBreathBiodermSound;

   damageScale[$DamageType::Bullet] = 0.10;  //I deny you shrike n0bs
   damageScale[$DamageType::Fire] = 3.0;
   damageScale[$DamageType::Plasma] = 3.0;
   damageScale[$DamageType::Burn] = 2.0;

   max[RepairKit]			= 0;
   max[Mine]				= 0;
   max[Grenade]			= 0;
};

//
// PARTICLES
//

// ============================================================================
// YVEX
// ============================================================================
datablock ParticleData(InflictionNightmareGlobeSmoke) {
   dragCoefficient = 50;/////////-----------------------
   gravityCoefficient = 0.0;
   inheritedVelFactor = 1.0;
   constantAcceleration = 0.0;
   lifetimeMS = 5050;
   lifetimeVarianceMS = 0;
   useInvAlpha = true;
   spinRandomMin = -360.0;
   spinRandomMax = 360.0;
   textureName = "particleTest";
   colors[0] = "0.5 0.1 0.9 1.0";
   colors[1] = "0.5 0.1 0.9 1.0";
   colors[2] = "0.5 0.1 0.9 1.0";
   colors[3] = "0.5 0.1 0.9";
   sizes[0] = 1.0;
   sizes[1] = 1.0;
   sizes[2] = 1.0;
   sizes[3] = 1.0;
   times[0] = 0.0;
   times[1] = 0.33;
   times[2] = 0.66;
   times[3] = 1.0;
   mass = 0.7;
   elasticity = 0.2;
   friction = 1;
   computeCRC = true;
   haslight = true;
   lightType = "PulsingLight";
   lightColor = "0.2 0.0 0.5 1.0";
   lightTime = "200";
   lightRadius = "2.0";
};

datablock ParticleEmitterData(InfNightmareGlobeEmitter) {
   ejectionPeriodMS = 0.1;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
   ejectionOffset = 5;
   thetaMin = 0;
   thetaMax = 180;
   overrideAdvances = false;
   particles = "InflictionNightmareGlobeSmoke";
};


datablock ParticleData(NightmareGlobeSmoke) {
   dragCoefficient = 50;/////////-----------------------
   gravityCoefficient = 0.0;
   inheritedVelFactor = 1.0;
   constantAcceleration = 0.0;
   lifetimeMS = 5050;
   lifetimeVarianceMS = 0;
   useInvAlpha = true;
   spinRandomMin = -360.0;
   spinRandomMax = 360.0;
   textureName = "particleTest";
   colors[0] = "0.1 0.1 0.1 1.0";// ////////////////////
   colors[1] = "0.1 0.1 0.1 1.0";// ////////////////////
   colors[2] = "0.1 0.1 0.1 1.0";// \\\\\\\\\\\\\\\\\\\\
   colors[3] = "0.1 0.1 0.1 1.0";// \\\\\\\\\\\\\\\\\\\\
   sizes[0] = 1.0;
   sizes[1] = 1.0;
   sizes[2] = 1.0;
   sizes[3] = 1.0;
   times[0] = 0.0;
   times[1] = 0.33;
   times[2] = 0.66;
   times[3] = 1.0;
   mass = 0.7;
   elasticity = 0.2;
   friction = 1;
   computeCRC = true;
   haslight = true;
   lightType = "PulsingLight";
   lightColor = "0.2 0.0 0.5 1.0";
   lightTime = "200";
   lightRadius = "2.0";
};

datablock ParticleEmitterData(NightmareGlobeEmitter) {
   ejectionPeriodMS = 0.1;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
   ejectionOffset = 5;
   thetaMin = 0;
   thetaMax = 180;
   overrideAdvances = false;
   particles = "NightmareGlobeSmoke";
};

datablock ParticleData(GreenEmitParticle) {
   dragCoeffiecient     = 1;
   gravityCoefficient   = -0.3;   // rises slowly
   inheritedVelFactor   = 0;

   lifetimeMS           =  300;
   lifetimeVarianceMS   =  0;
   useInvAlpha          =  false;
   spinRandomMin        = 0.0;
   spinRandomMax        = 0.0;

   animateTexture = false;

   textureName = "flareBase"; // "special/Smoke/bigSmoke"

   colors[0]     = "0 1 0";
   colors[1]     = "0 1 0";
   colors[2]     = "0 1 0";

   sizes[0]      = 0.8;
   sizes[1]      = 0.8;
   sizes[2]      = 0.8;

   times[0]      = 0.0;
   times[1]      = 1.0;
   times[2]      = 5.0;

};

datablock ParticleEmitterData(PulseGreenEmitter) {
   ejectionPeriodMS = 2;
   periodVarianceMS = 1;

   ejectionVelocity = 10;
   velocityVariance = 0;

   thetaMin         = 89.0;
   thetaMax         = 90.0;

   orientParticles = false;

   particles = "GreenEmitParticle";
};

datablock ParticleData(PurpleNightmareEmitParticle) {
   dragCoeffiecient     = 1;
   gravityCoefficient   = -0.3;   // rises slowly
   inheritedVelFactor   = 0;

   lifetimeMS           =  300;
   lifetimeVarianceMS   =  0;
   useInvAlpha          =  false;
   spinRandomMin        = 0.0;
   spinRandomMax        = 0.0;

   animateTexture = false;

   textureName = "flareBase"; // "special/Smoke/bigSmoke"

   colors[0] = "0.5 0.1 0.9 1.0";
   colors[1] = "0.5 0.1 0.9 1.0";
   colors[2] = "0.5 0.1 0.9";

   sizes[0]      = 0.4;
   sizes[1]      = 0.4;
   sizes[2]      = 0.4;

   times[0]      = 0.0;
   times[1]      = 1.0;
   times[2]      = 5.0;

};

datablock ParticleEmitterData(YvexSniperEmitter) {
   ejectionPeriodMS = 2;
   periodVarianceMS = 1;

   ejectionVelocity = 10;
   velocityVariance = 0;

   thetaMin         = 89.0;
   thetaMax         = 90.0;

   orientParticles = false;

   particles = "PurpleNightmareEmitParticle";
};

// ============================================================================
// WINDSHEAR
// ============================================================================

// ============================================================================
// GHOST OF LIGHTNING
// ============================================================================
datablock ParticleData(ShockParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.0;
   inheritedVelFactor   = 0.0;

   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 0;

   textureName          = "particleTest";

   useInvAlpha = false;
   spinRandomMin = -100.0;
   spinRandomMax = 100.0;

   numParts = 50;

   animateTexture = true;
   framesPerSec = 26;

   animTexName[00]       = "special/Explosion/exp_0002";
   animTexName[01]       = "special/Explosion/exp_0004";
   animTexName[02]       = "special/Explosion/exp_0006";
   animTexName[03]       = "special/Explosion/exp_0008";
   animTexName[04]       = "special/Explosion/exp_0010";
   animTexName[05]       = "special/Explosion/exp_0012";
   animTexName[06]       = "special/Explosion/exp_0014";
   animTexName[07]       = "special/Explosion/exp_0016";
   animTexName[08]       = "special/Explosion/exp_0018";
   animTexName[09]       = "special/Explosion/exp_0020";
   animTexName[10]       = "special/Explosion/exp_0022";
   animTexName[11]       = "special/Explosion/exp_0024";
   animTexName[12]       = "special/Explosion/exp_0026";
   animTexName[13]       = "special/Explosion/exp_0028";
   animTexName[14]       = "special/Explosion/exp_0030";
   animTexName[15]       = "special/Explosion/exp_0032";
   animTexName[16]       = "special/Explosion/exp_0034";
   animTexName[17]       = "special/Explosion/exp_0036";
   animTexName[18]       = "special/Explosion/exp_0038";
   animTexName[19]       = "special/Explosion/exp_0040";
   animTexName[20]       = "special/Explosion/exp_0042";
   animTexName[21]       = "special/Explosion/exp_0044";
   animTexName[22]       = "special/Explosion/exp_0046";
   animTexName[23]       = "special/Explosion/exp_0048";
   animTexName[24]       = "special/Explosion/exp_0050";
   animTexName[25]       = "special/Explosion/exp_0052";


   colors[0]     = "0.5   0.5  1.0 1.0";
   colors[1]     = "0.5   0.5  1.0 0.5";
   colors[2]     = "0.25  0.25 1.0 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.5;
   sizes[2]      = 0.5;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ShockParticleEmitter) {
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;

   ejectionVelocity = 0.25;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 30.0;

   particles = "ShockParticle";
};

datablock ShockwaveData( ShocklanceHit ) {
   width = 0.5;
   numSegments = 20;
   numVertSegments = 1;
   velocity = 0.25;
   acceleration = 1.0;
   lifetimeMS = 600;
   height = 0.1;
   verticalCurve = 0.5;

   mapToTerrain = false;
   renderBottom = false;
   orientToNormal = true;

   texture[0] = "special/shocklanceHit";
   texture[1] = "special/gradient";
   texWrap = 3.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0] = "1.0 1.0 1.0 1.0";
   colors[1] = "1.0 1.0 1.0 0.5";
   colors[2] = "1.0 1.0 1.0 0.0";
};

// ============================================================================
// VEGENOR
// ============================================================================

// ============================================================================
// LORD ROG
// ============================================================================
datablock ParticleData(PBCParticle) {
	dragCoefficient = 0.5;
	WindCoefficient = 0;
	gravityCoefficient  = 0.0;
	inheritedVelFactor  = 1;
	constantAcceleration = 0;
	lifetimeMS      = 3000;
	lifetimeVarianceMS  = 0;
	textureName     = "special/lightning1frame2";
	useInvAlpha = 0;

	spinRandomMin = -800;
	spinRandomMax = 800;

	spinspeed = 50;
	colors[0]   = "0.1 1.0 0.1 1.0";
	colors[1]   = "0.6 0.9 0.6 1.0";
	colors[2]   = "0.8 0.8 1.0 1.0";
	colors[3]   = "0.8 0.8 1.0 0.0";
	sizes[0]   = 0.5;
	sizes[1]   = 1;
	sizes[2]   = 1.5;
	sizes[3]   = 1.5;
	times[0]   = 0.0;
	times[1]   = 0.1;
	times[2]   = 0.3;
	times[3]   = 1.0;
};

datablock ParticleEmitterData(PBCExpEmitter) {
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;

   ejectionVelocity = 2.5;
   velocityVariance = 2.5;

   thetaMin         = 0.0;
   thetaMax         = 180.0;

   lifetimeMS       = 200;

   particles = "PBCParticle";
};

// ============================================================================
// INSIGNIA
// ============================================================================

// ============================================================================
// TREVOR
// ============================================================================

// ============================================================================
// LORD VARDISON / D.A. VARDISON
// ============================================================================
datablock ParticleData(ShadowBaseParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.2;
   inheritedVelFactor   = 0.0;

   lifetimeMS           = 800;
   lifetimeVarianceMS   = 500;

   useInvAlpha = true;
   spinRandomMin = -160.0;
   spinRandomMax = 160.0;

   animateTexture = true;
   framesPerSec = 15;

   textureName = "special/cloudflash";

   colors[0] = "0.1 0.1 0.1 1.0";// ////////////////////
   colors[1] = "0.1 0.1 0.1 1.0";// ////////////////////
   colors[2] = "0.1 0.1 0.1 1.0";// \\\\\\\\\\\\\\\\\\\\

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

datablock ParticleData(SmallShadowBaseParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.2;
   inheritedVelFactor   = 0.0;

   lifetimeMS           = 9999999999999;
   lifetimeVarianceMS   = 9999999999999;

   useInvAlpha = true;
   spinRandomMin = -160.0;
   spinRandomMax = 160.0;

   animateTexture = true;
   framesPerSec = 15;

   textureName = "special/cloudflash";

   colors[0] = "0.1 0.1 0.1 1.0";// ////////////////////
   colors[1] = "0.1 0.1 0.1 1.0";// ////////////////////
   colors[2] = "0.1 0.1 0.1 1.0";// \\\\\\\\\\\\\\\\\\\\

   sizes[0]      = 0.5;
   sizes[1]      = 0.7;
   sizes[2]      = 1.0;

   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(SmallShadowBaseEmitter) {
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.5;
   velocityVariance = 0.3;

   thetaMin         = 0.0;
   thetaMax         = 30.0;

   particles = "SmallShadowBaseParticle";
};

datablock ParticleData(JetShadowParticle) {
	dragCoeffiecient     = 0.0;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0.0;

	lifetimeMS           = 2500;
	lifetimeVarianceMS   = 0;

	textureName          = "particleTest";

	useInvAlpha = true;
	spinRandomMin = -160.0;
	spinRandomMax = 160.0;

	animateTexture = true;
	framesPerSec = 15;

	animTexName[0]       = "special/Explosion/exp_0016";
	animTexName[1]       = "special/Explosion/exp_0018";
	animTexName[2]       = "special/Explosion/exp_0020";
	animTexName[3]       = "special/Explosion/exp_0022";
	animTexName[4]       = "special/Explosion/exp_0024";
	animTexName[5]       = "special/Explosion/exp_0026";
	animTexName[6]       = "special/Explosion/exp_0028";
	animTexName[7]       = "special/Explosion/exp_0030";
	animTexName[8]       = "special/Explosion/exp_0032";

    colors[0] = "0.1 0.1 0.1 1.0";// ////////////////////
    colors[1] = "0.1 0.1 0.1 1.0";// ////////////////////
    colors[2] = "0.1 0.1 0.1 1.0";// \\\\\\\\\\\\\\\\\\\\
	sizes[0]      = 2.5;
	sizes[1]      = 1.25;
	sizes[2]      = 0.625;
	times[0]      = 0.0;
	times[1]      = 0.7;
	times[2]      = 1.0;
};

datablock ParticleEmitterData(JetShadowEmitter) {
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;

   ejectionVelocity = 0;
   velocityVariance = 0;
   ejectionOffset   = 5;
   thetaMin         = 22.5;
   thetaMax         = 45;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   spinRandomMin   = "200";
   spinRandomMax   = "-200";
   overrideAdvances = false;
   particles = "JetShadowParticle";
};

datablock ParticleData(LaserBallGlobeSmoke) {
   dragCoefficient = 50;/////////-----------------------
   gravityCoefficient = 0.0;
   inheritedVelFactor = 1.0;
   constantAcceleration = 0.0;
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
   sizes[0] = 1.0;
   sizes[1] = 1.0;
   sizes[2] = 1.0;
   sizes[3] = 1.0;
   times[0] = 0.0;
   times[1] = 0.33;
   times[2] = 0.66;
   times[3] = 1.0;
   mass = 0.7;
   elasticity = 0.2;
   friction = 1;
   computeCRC = true;
   haslight = true;
   lightType = "PulsingLight";
   lightColor = "0.2 0.0 0.5 1.0";
   lightTime = "200";
   lightRadius = "2.0";
};

datablock ParticleEmitterData(MiniShadowBallEmitter) {
   ejectionPeriodMS = 0.3;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
   ejectionOffset = 2;
   thetaMin = 0;
   thetaMax = 180;
   overrideAdvances = false;
   particles = "LaserBallGlobeSmoke";
};

// ============================================================================
// STORMRIDER
// ============================================================================
//Seeker Plasma Stuff
//Credit To Abrikcham
//-abirikcham@yahoo.com
datablock ShockwaveData(FakePhotonMissileShockwave) {
   width = 0.5;
   numSegments = 30;
   numVertSegments = 2;
   velocity = 8;
   verticalcurve = 0;
   acceleration = -17.0;
   lifetimeMS = 600;
   height = 0.00001;
   is2D = false;
   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 10.0;
   mapToTerrain = false;
   orientToNormal = true;
   renderBottom = true;
   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;
   colors[0] = "0 1 0 1";
   colors[1] = "0.0 1.1 0.0 0.60";//0.4 0.1 1.0
   colors[2] = "0.0 1.1 0.0 0.0";
};

datablock AudioProfile(FakePhotonMissileShockwaveSound) {
	filename = "fx/misc/gridjump.wav";
	description = AudioExplosion3d;
	preload = true;
};

datablock ExplosionData(FakePhotonShockwaveExp) {
	soundProfile   = FakePhotonMissileShockwaveSound;
	faceViewer           = false;
	shockwave = FakePhotonMissileShockwave;

	shakeCamera = true;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 3.0;
};

datablock ParticleData(PhotonMissileExpPart) {
   dragCoefficient      = 2;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 750;
   lifetimeVarianceMS   = 150;
   textureName          = "particleTest";
   colors[0]     = "0 1 0 1.0";
   colors[1]     = "0 1 0 0.0";
   sizes[0]      = 3;
   sizes[1]      = 2;
};

datablock ParticleEmitterData(PhotonMissileExpEmit) {
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 3;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "PhotonMissileExpPart";
};

datablock ExplosionData(PhotonMissileExplosion) {
   explosionShape = "disc_explosion.dts";
   soundProfile   = UnderwaterGrenadeExplosionSound;//plasmaexpsound orig

   shakeCamera = true;
   camShakeFreq = "8.0 9.0 7.0";
   camShakeAmp = "25.0 25.0 25.0";
   camShakeDuration = 1.3;
   camShakeRadius = 25.0;

   particleEmitter = PhotonMissileExpEmit;
   particleDensity = 150;
   particleRadius = 3.5;
   faceViewer = true;
};

// ============================================================================
// GHOST OF FIRE
// ============================================================================
datablock ParticleData(GhostflameParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.1;
   inheritedVelFactor   = 0.1;

   lifetimeMS           = 500;
   lifetimeVarianceMS   = 50;

   textureName          = "particleTest";

   spinRandomMin = -10.0;
   spinRandomMax = 10.0;

   colors[0]     = "0 1 0 0.4";
   colors[1]     = "0 1 0 0.3";
   colors[2]     = "0 1 0 0.0";
   sizes[0]      = 2.0;
   sizes[1]      = 1.0;
   sizes[2]      = 0.8;
   times[0]      = 0.0;
   times[1]      = 0.6;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(GhostflameEmitter) {
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;

   ejectionOffset = 0.2;
   ejectionVelocity = 10.0;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 10.0;

   particles = "GhostflameParticle";
};

datablock ParticleData(NapalmExplosionParticle) {
   dragCoefficient      = 2;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 450;
   lifetimeVarianceMS   = 150;
   textureName          = "particleTest";
   colors[0]     = "1 0 0";
   colors[1]     = "1 0 0";
   sizes[0]      = 0.5;
   sizes[1]      = 2;
};

datablock ParticleEmitterData(NapalmExplosionEmitter) {
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 5;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "NapalmExplosionParticle";
};

datablock ExplosionData(NapalmExplosion) {
   explosionShape = "effect_plasma_explosion.dts";
   soundProfile   = plasmaExpSound;
   particleEmitter = NapalmExplosionEmitter;
   particleDensity = 150;
   particleRadius = 1.25;
   faceViewer = true;

   sizes[0] = "3.0 3.0 3.0";
   sizes[1] = "3.0 3.0 3.0";
   times[0] = 0.0;
   times[1] = 1.5;
};

// ============================================================================
// SHADE LORD
// ============================================================================
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
   ejectionVelocity = 400.0;
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
// PROJECTILES
//

// ============================================================================
// YVEX
// ============================================================================
datablock SeekerProjectileData(YvexNightmareMissile){
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
   maxVelocity         = 150.0;
   turningSpeed        = 110.0;
   acceleration        = 350.0;

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

   baseEmitter         = NMMissileBaseEmitter;
};

datablock LinearFlareProjectileData(KillerPulse) {
   scale               = "1.0 1.0 1.0";
   faceViewer          = false;
   directDamage        = 0.00001;
   hasDamageRadius     = false;
   indirectDamage      = 0.6;
   damageRadius        = 10.0;
   kickBackStrength    = 100.0;
   directDamageType    = $DamageType::Admin;
   indirectDamageType  = $DamageType::Admin;

   explosion           = "BlasterExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 200.0;
   wetVelocity       = 10;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 30000;
   lifetimeMS        = 30000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   baseEmitter         = PulseGreenEmitter;
   delayEmitter        = PulseGreenEmitter;
   bubbleEmitter       = PulseGreenEmitter;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.2;
   size[2]           = 0.2;


   numFlares         = 15;
   flareColor        = "0 1 0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound        = MissileProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 1 0";

};

datablock LinearFlareProjectileData(YvexSniperShot) {
   projectileShapeName = "weapon_missile_projectile.dts";
   scale               = "3.0 5.0 3.0";
   faceViewer          = true;
   directDamage        = 0.01;
   kickBackStrength    = 4000.0;
   DirectDamageType    = $DamageType::Zombie;

   explosion           = "BlasterExplosion";

   dryVelocity       = 150.0;
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 10000;
   lifetimeMS        = 10000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 100;
   activateDelayMS = -1;

   baseEmitter = YvexSniperEmitter;

   size[0]           = 0.0;
   size[1]           = 0.0;
   size[2]           = 0.0;


   numFlares         = 0;
   flareColor        = "0.0 0.0 0.0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	sound        = PlasmaProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

datablock SeekerProjectileData(YvexZombieMakerMissile) {
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   hasDamageRadius     = true;
   indirectDamage      = 0.5;
   damageRadius        = 5.0;
   radiusDamageType    = $DamageType::Zombie;
   kickBackStrength    = 2000;

   explosion           = "MissileExplosion";
   splash              = MissileSplash;
   velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
                                 // is cranked up to full so the missile doesn't start
                                 // out behind the player when the player is moving
                                 // very quickly - bramage

   baseEmitter         = MortarSmokeEmitter;
   delayEmitter        = MissileFireEmitter;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = 30000; // z0dd - ZOD, 4/14/02. Was 6000
   muzzleVelocity      = 30.0;
   maxVelocity         = 35.0; // z0dd - ZOD, 4/14/02. Was 80.0
   turningSpeed        = 23.0;
   acceleration        = 15.0;

   proximityRadius     = 2.5;

   terrainAvoidanceSpeed = 10;
   terrainScanAhead      = 7;
   terrainHeightFail     = 1;
   terrainAvoidanceRadius = 3;

   flareDistance = 40;
   flareAngle    = 20;
   minSeekHeat   = 0.0;

   sound = MissileProjectileSound;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.2 0.05 0";

   useFlechette = true;
   flechetteDelayMs = 250;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = false;
};

// ============================================================================
// WINDSHEAR
// ============================================================================
datablock SeekerProjectileData(BossMissiles) {
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   hasDamageRadius     = true;
   indirectDamage      = 0.1;
   damageRadius        = 6.0;
   radiusDamageType    = $DamageType::MissileTurret;
   kickBackStrength    = 500;

   flareDistance = 200;
   flareAngle    = 30;
   minSeekHeat   = 0.0;

   explosion           = "MissileExplosion";
   velInheritFactor    = 1.0;

   splash              = MissileSplash;
   baseEmitter         = MortarSmokeEmitter;
   delayEmitter        = MissileFireEmitter;
   puffEmitter         = MissilePuffEmitter;

   lifetimeMS          = 15000; // z0dd - ZOD, 4/14/02. Was 6000
   muzzleVelocity      = 12.0;
   maxVelocity         = 225.0; // z0dd - ZOD, 4/14/02. Was 80.0
   turningSpeed        = 50.0;
   acceleration        = 100.0;

   proximityRadius     = 4;

   terrainAvoidanceSpeed = 100;
   terrainScanAhead      = 50;
   terrainHeightFail     = 50;
   terrainAvoidanceRadius = 150;

   useFlechette = true;
   flechetteDelayMs = 225;
   casingDeb = FlechetteDebris;
};

// ============================================================================
// GHOST OF LIGHTNING
// ============================================================================
datablock ShockLanceProjectileData(GoLShocker) {
   directDamage        = 0.45;
   radiusDamageType    = $DamageType::ShockLance;
   kickBackStrength    = 2500;
   velInheritFactor    = 0;
   sound               = "";

   zapDuration = 1.0;
   impulse = 1800;
   boltLength = 50.0;
   extension = 50.0;            // script variable indicating distance you can shock people from
   lightningFreq = 25.0;
   lightningDensity = 3.0;
   lightningAmp = 0.25;
   lightningWidth = 0.05;

   shockwave = ShocklanceHit;

   boltSpeed[0] = 2.0;
   boltSpeed[1] = -0.5;

   texWrap[0] = 1.5;
   texWrap[1] = 1.5;

   startWidth[0] = 0.3;
   endWidth[0] = 0.6;
   startWidth[1] = 0.3;
   endWidth[1] = 0.6;

   texture[0] = "special/shockLightning01";
   texture[1] = "special/shockLightning02";
   texture[2] = "special/shockLightning03";
   texture[3] = "special/ELFBeam";

   emitter[0] = ShockParticleEmitter;
};

// ============================================================================
// VEGENOR
// ============================================================================
datablock SeekerProjectileData(VegenorFireMissile) : YvexZombieMakerMissile {
   indirectDamage      = 0.5;
   damageRadius        = 5.0;
   radiusDamageType    = $DamageType::Fire;
};

datablock GrenadeProjectileData(VegenorFireMeteor) : JTLMeteorStormFireball {
	projectileShapeName  = "plasmabolt.dts";
	scale                = "40.0 40.0 40.0";
	emitterDelay         = -1;
	directDamage         = 0;
	directDamageType     = $DamageType::Fire;
	hasDamageRadius      = true; // true;
	indirectDamage       = 0.5; // 0.5;
	damageRadius         = 150.0;
	radiusDamageType     = $DamageType::Fire;
};

// ============================================================================
// LORD ROG
// ============================================================================
datablock SeekerProjectileData(LordRogStiloutte) {
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   hasDamageRadius     = true;
   indirectDamage      = 0.5;
   damageRadius        = 5.0;
   radiusDamageType    = $DamageType::Zombie;
   kickBackStrength    = 2000;

   explosion           = "MissileExplosion";
   splash              = MissileSplash;
   velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
                                 // is cranked up to full so the missile doesn't start
                                 // out behind the player when the player is moving
                                 // very quickly - bramage

   baseEmitter         = MortarSmokeEmitter;
   delayEmitter        = MissileFireEmitter;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = 20000; // z0dd - ZOD, 4/14/02. Was 6000
   muzzleVelocity      = 10.0;
   maxVelocity         = 80.0; // z0dd - ZOD, 4/14/02. Was 80.0
   turningSpeed        = 23.0;
   acceleration        = 15.0;

   proximityRadius     = 2.5;

   terrainAvoidanceSpeed = 10;
   terrainScanAhead      = 7;
   terrainHeightFail     = 1;
   terrainAvoidanceRadius = 3;

   flareDistance = 40;
   flareAngle    = 20;
   minSeekHeat   = 0.0;

   sound = MissileProjectileSound;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.2 0.05 0";

   useFlechette = true;
   flechetteDelayMs = 250;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = false;
};

// ============================================================================
// INSIGNIA
// ============================================================================

// ============================================================================
// TREVOR
// ============================================================================

// ============================================================================
// LORD VARDISON / D.A. VARDISON
// ============================================================================
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

datablock LinearFlareProjectileData(ShadowBomb) {
   projectileShapeName = "turret_muzzlepoint.dts";
   scale               = "1.0 1.0 1.0";
   faceViewer          = true;
   directDamage        = 0.02;
   hasDamageRadius     = true;
   indirectDamage      = 0.02;
   damageRadius        = 4.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::Fire;

   explosion           = "ThrowerExplosion";
   splash              = PlasmaSplash;

   baseEmitter        = ShadowBaseEmitter;

   dryVelocity       = 50.0; // z0dd - ZOD, 7/20/02. Faster plasma projectile. was 55
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 14000;
   lifetimeMS        = 10000; // z0dd - ZOD, 4/25/02. Was 6000
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

datablock SeekerProjectileData(VardisonLaserBallMissile) : YvexNightmareMissile {
   baseEmitter         = ShadowBaseEmitter;
};

datablock LinearFlareProjectileData(VardisonSubShadowBomb) : DMPlasma {
   explosion = MortarExplosion;
   dryVelocity       = 500.0; // z0dd - ZOD, 4/25/02. Was 50. Velocity of projectile out of water
   wetVelocity       = -1;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 14000;
   lifetimeMS        = 10000; // z0dd - ZOD, 4/25/02. Was 6000
};

datablock SeekerProjectileData(VardisonMiniDemonSpawner) : VardisonNightmareMissile {
   baseEmitter         = ShadowBaseEmitter;
};

datablock GrenadeProjectileData(MiniDemonBlaster) {
   projectileShapeName = "plasmabolt.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.45;
   damageRadius        = 5.0; // z0dd - ZOD, 8/13/02. Was 20.0
   radiusDamageType    = $DamageType::Demon;
   kickBackStrength    = 1500;

   explosion           = "MortarExplosion";
   underwaterExplosion = "MortarExplosion";
   velInheritFactor    = 0;
   splash              = PlasmaSplash;
   depthTolerance      = 100.0;

   baseEmitter         = DemonFBSmokeEmitter;
   bubbleEmitter       = DemonFBSmokeEmitter;

   grenadeElasticity = 0;
   grenadeFriction   = 0.4;
   armingDelayMS     = -1; // z0dd - ZOD, 4/14/02. Was 2000

   gravityMod        = 0.4;  // z0dd - ZOD, 5/18/02. Make mortar projectile heavier, less floaty
   muzzleVelocity    = 125.0; // z0dd - ZOD, 8/13/02. More velocity to compensate for higher gravity. Was 63.7
   drag              = 0;
   sound	     = PlasmaProjectileSound;

   hasLight    = true;
   lightRadius = 10;
   lightColor  = "1 0.75 0.25";

   hasLightUnderwaterColor = true;
   underWaterLightColor = "1 0.75 0.25";
};

// ============================================================================
// STORMRIDER
// ============================================================================
datablock LinearProjectileData(FakePhotonShockwaveProj) {
	projectileShapeName = "turret_muzzlepoint.dts";
	scale               = "0.1 0.1 0.1";
	faceViewer          = true;
	directDamage        = 0;
	hasDamageRadius     = false;
	indirectDamage      = 0.1;
	damageRadius        = 10;
	kickBackStrength    = 1;
	radiusDamageType    = $DamageType::Photon;
	explosion           = "FakePhotonShockwaveExp";
	dryVelocity       = 0.0001;
	wetVelocity       = 0.00001;
	velInheritFactor  = 0.0;
	lifetimeMS        = 0.00000001;
	explodeOnDeath    = true;
	reflectOnWaterImpactAngle = 0.0;
	explodeOnWaterImpact      = true;
	deflectionOnWaterImpact   = 0.0;
};

datablock LinearFlareProjectileData(PhotonMissileProj) {
   scale               = "4 4 4";//6
   sound      = PlasmaProjectileSound;

   faceViewer          = true;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 1.0;
   damageRadius        = 10.0;
   kickBackStrength    = 4000;
   radiusDamageType    = $DamageType::Photon; //obviously change this

   explosion           = "PhotonMissileExplosion";
   underwaterExplosion = "PhotonMissileExplosion";
   splash              = BlasterSplash;

   dryVelocity       = 200.0;
   wetVelocity       = 200.0;
   velInheritFactor  = 0.6;
   fizzleTimeMS      = 8000;
   lifetimeMS        = 8000;
   explodeOnDeath    = true;

   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 15000;

   activateDelayMS = 0;
   numFlares         = 35;
   flareColor        = "0.0 1.1 0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   size[0]           = 1;
   size[1]           = 10;
   size[2]           = 2;


   hasLight    = true;
   lightRadius = 1.0;
   lightColor  = "0.6 1.1 0";
};

// ============================================================================
// GHOST OF FIRE
// ============================================================================
datablock LinearFlareProjectileData(GhostFlameboltMain) {
   projectileShapeName = "turret_muzzlepoint.dts";
   scale               = "1.0 1.0 1.0";
   faceViewer          = true;
   directDamage        = 0.05;
   hasDamageRadius     = true;
   indirectDamage      = 0.1;
   damageRadius        = 4.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::Plasma;

   explosion           = "ThrowerExplosion";
   splash              = PlasmaSplash;

   baseEmitter        = GhostflameEmitter;

   dryVelocity       = 50.0; // z0dd - ZOD, 7/20/02. Faster plasma projectile. was 55
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 250;
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

datablock LinearProjectileData(NapalmShot) {
   projectileShapeName = "mortar_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.5;
   damageRadius        = 20.0;
   radiusDamageType    = $DamageType::Plasma;
   kickBackStrength    = 3000;

   explosion           = "NapalmExplosion";
//   underwaterExplosion = "UnderwaterNapalmExplosion";
   velInheritFactor    = 0.5;
//   splash              = NapalmSplash;
   depthTolerance      = 10.0; // depth at which it uses underwater explosion

   baseEmitter         = MissileFireEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;

   grenadeElasticity = 0.15;
   grenadeFriction   = 0.4;
   armingDelayMS     = 2000;
   muzzleVelocity    = 63.7;
   drag              = 0.1;

   sound          = MortarProjectileSound;

   hasLight    = true;
   lightRadius = 4;
   lightColor  = "1.00 0.9 1.00";

   hasLightUnderwaterColor = true;
   underWaterLightColor = "0.05 0.075 0.2";

   dryVelocity       = 90;
   wetVelocity       = 50;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 5000;
   lifetimeMS        = 2700;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 5000;

};

// ============================================================================
// SHADE LORD
// ============================================================================
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

