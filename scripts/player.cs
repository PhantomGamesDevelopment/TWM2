//----------------------------------------------------------------------------
//----------------------------------------------------------------------------

$InvincibleTime = 6;

// Load dts shapes and merge animations
exec("scripts/light_male.cs");
exec("scripts/medium_male.cs");
exec("scripts/heavy_male.cs");
exec("scripts/light_female.cs");
exec("scripts/medium_female.cs");
exec("scripts/bioderm_light.cs");
exec("scripts/bioderm_medium.cs");
exec("scripts/bioderm_heavy.cs");
exec("scripts/TR2medium_male.cs");
exec("scripts/TR2heavy_male.cs");

$CorpseTimeoutValue = 22 * 1000;

//Damage Rate for entering Liquid
$DamageLava       = 0.0325;
$DamageHotLava    = 0.0325;
$DamageCrustyLava = 0.0325;

$PlayerDeathAnim::TorsoFrontFallForward = 1;
$PlayerDeathAnim::TorsoFrontFallBack = 2;
$PlayerDeathAnim::TorsoBackFallForward = 3;
$PlayerDeathAnim::TorsoLeftSpinDeath = 4;
$PlayerDeathAnim::TorsoRightSpinDeath = 5;
$PlayerDeathAnim::LegsLeftGimp = 6;
$PlayerDeathAnim::LegsRightGimp = 7;
$PlayerDeathAnim::TorsoBackFallForward = 8;
$PlayerDeathAnim::HeadFrontDirect = 9;
$PlayerDeathAnim::HeadBackFallForward = 10;
$PlayerDeathAnim::ExplosionBlowBack = 11;

//$Zombie::TurningSpeed = 100;
$Zombie::ForwardSpeed = 750;
$Zombie::FForwardSpeed = 1500;
$Zombie::LForwardSpeed = 4000;
$Zombie::DForwardSpeed = 1200;
$Zombie::RForwardSpeed = 1500;

datablock SensorData(PlayerSensor)
{
   detects = true;
   detectsUsingLOS = true;
   detectsPassiveJammed = true;
   detectRadius = 2000;
   detectionPings = false;
   detectsFOVOnly = true;
   detectFOVPercent = 1.3;
   useObjectFOV = true;
};

//----------------------------------------------------------------------------

//----------------------------------------------------------------------------
//datablock AudioProfile( DeathCrySound )
//{
//   fileName = "";
//   description = AudioClose3d;
//   preload = true;
//};
//
//datablock AudioProfile( PainCrySound )
//{
//   fileName = "";
//   description = AudioClose3d;
//   preload = true;
//};

datablock AudioProfile(ArmorJetSound)
{
   filename    = "fx/armor/thrust.wav";
   description = CloseLooping3d;
   preload = true;
};

datablock AudioProfile(ArmorWetJetSound)
{
   filename    = "fx/armor/thrust_uw.wav";
   description = CloseLooping3d;
   preload = true;
};

datablock AudioProfile(MountVehicleSound)
{
   filename    = "fx/vehicles/mount_dis.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(UnmountVehicleSound)
{
   filename    = "fx/vehicles/mount.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(CorpseLootingSound)
{
   fileName = "fx/weapons/generic_switch.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ArmorMoveBubblesSound)
{
   filename    = "fx/armor/bubbletrail2.wav";
   description = CloseLooping3d;
   preload = true;
};

datablock AudioProfile(WaterBreathMaleSound)
{
   filename    = "fx/armor/breath_uw.wav";
   description = ClosestLooping3d;
   preload = true;
};

datablock AudioProfile(WaterBreathFemaleSound)
{
   filename    = "fx/armor/breath_fem_uw.wav";
   description = ClosestLooping3d;
   preload = true;
};

datablock AudioProfile(waterBreathBiodermSound)
{
   filename    = "fx/armor/breath_bio_uw.wav";
   description = ClosestLooping3d;
   preload = true;
};

datablock AudioProfile(SkiAllSoftSound)
{
   filename    = "fx/armor/ski_soft.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(SkiAllHardSound)
{
   filename    = "fx/armor/ski_soft.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(SkiAllMetalSound)
{
   filename    = "fx/armor/ski_soft.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(SkiAllSnowSound)
{
   filename    = "fx/armor/ski_soft.wav";
   description = AudioClosest3d;
   preload = true;
};

//SOUNDS ----- LIGHT ARMOR--------
datablock AudioProfile(LFootLightSoftSound)
{
   filename    = "fx/armor/light_LF_soft.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(RFootLightSoftSound)
{
   filename    = "fx/armor/light_RF_soft.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(LFootLightHardSound)
{
   filename    = "fx/armor/light_LF_hard.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootLightHardSound)
{
   filename    = "fx/armor/light_RF_hard.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(LFootLightMetalSound)
{
   filename    = "fx/armor/light_LF_metal.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootLightMetalSound)
{
   filename    = "fx/armor/light_RF_metal.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(LFootLightSnowSound)
{
   filename    = "fx/armor/light_LF_snow.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(RFootLightSnowSound)
{
   filename    = "fx/armor/light_RF_snow.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(LFootLightShallowSplashSound)
{
   filename    = "fx/armor/light_LF_water.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootLightShallowSplashSound)
{
   filename    = "fx/armor/light_RF_water.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(LFootLightWadingSound)
{
   filename    = "fx/armor/light_LF_wade.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootLightWadingSound)
{
   filename    = "fx/armor/light_RF_wade.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(LFootLightUnderwaterSound)
{
   filename    = "fx/armor/light_LF_uw.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(RFootLightUnderwaterSound)
{
   filename    = "fx/armor/light_RF_uw.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(LFootLightBubblesSound)
{
   filename    = "fx/armor/light_LF_bubbles.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootLightBubblesSound)
{
   filename    = "fx/armor/light_RF_bubbles.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactLightSoftSound)
{
   filename    = "fx/armor/light_land_soft.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactLightHardSound)
{
   filename    = "fx/armor/light_land_hard.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactLightMetalSound)
{
   filename    = "fx/armor/light_land_metal.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactLightSnowSound)
{
   filename    = "fx/armor/light_land_snow.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ImpactLightWaterEasySound)
{
   filename    = "fx/armor/general_water_smallsplash2.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactLightWaterMediumSound)
{
   filename    = "fx/armor/general_water_medsplash.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactLightWaterHardSound)
{
   filename    = "fx/armor/general_water_bigsplash.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(ExitingWaterLightSound)
{
   filename    = "fx/armor/general_water_exit2.wav";
   description = AudioClose3d;
   preload = true;
};

//SOUNDS ----- Medium ARMOR--------
datablock AudioProfile(LFootMediumSoftSound)
{
   filename    = "fx/armor/med_LF_soft.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(RFootMediumSoftSound)
{
   filename    = "fx/armor/med_RF_soft.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(LFootMediumHardSound)
{
   filename    = "fx/armor/med_LF_hard.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootMediumHardSound)
{
   filename    = "fx/armor/med_RF_hard.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(LFootMediumMetalSound)
{
   filename    = "fx/armor/med_LF_metal.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootMediumMetalSound)
{
   filename    = "fx/armor/med_RF_metal.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(LFootMediumSnowSound)
{
   filename    = "fx/armor/med_LF_snow.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(RFootMediumSnowSound)
{
   filename    = "fx/armor/med_RF_snow.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(LFootMediumShallowSplashSound)
{
   filename    = "fx/armor/med_LF_water.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootMediumShallowSplashSound)
{
   filename    = "fx/armor/med_RF_water.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(LFootMediumWadingSound)
{
   filename    = "fx/armor/med_LF_uw.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootMediumWadingSound)
{
   filename    = "fx/armor/med_RF_uw.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(LFootMediumUnderwaterSound)
{
   filename    = "fx/armor/med_LF_uw.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(RFootMediumUnderwaterSound)
{
   filename    = "fx/armor/med_RF_uw.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(LFootMediumBubblesSound)
{
   filename    = "fx/armor/light_LF_bubbles.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootMediumBubblesSound)
{
   filename    = "fx/armor/light_RF_bubbles.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(ImpactMediumSoftSound)
{
   filename    = "fx/armor/med_land_soft.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ImpactMediumHardSound)
{
   filename    = "fx/armor/med_land_hard.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactMediumMetalSound)
{
   filename    = "fx/armor/med_land_metal.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactMediumSnowSound)
{
   filename    = "fx/armor/med_land_snow.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ImpactMediumWaterEasySound)
{
   filename    = "fx/armor/general_water_smallsplash.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactMediumWaterMediumSound)
{
   filename    = "fx/armor/general_water_medsplash.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactMediumWaterHardSound)
{
   filename    = "fx/armor/general_water_bigsplash.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(ExitingWaterMediumSound)
{
   filename    = "fx/armor/general_water_exit.wav";
   description = AudioClose3d;
   preload = true;
};

//SOUNDS ----- HEAVY ARMOR--------
datablock AudioProfile(LFootHeavySoftSound)
{
   filename    = "fx/armor/heavy_LF_soft.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(RFootHeavySoftSound)
{
   filename    = "fx/armor/heavy_RF_soft.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(LFootHeavyHardSound)
{
   filename    = "fx/armor/heavy_LF_hard.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootHeavyHardSound)
{
   filename    = "fx/armor/heavy_RF_hard.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(LFootHeavyMetalSound)
{
   filename    = "fx/armor/heavy_LF_metal.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootHeavyMetalSound)
{
   filename    = "fx/armor/heavy_RF_metal.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(LFootHeavySnowSound)
{
   filename    = "fx/armor/heavy_LF_snow.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(RFootHeavySnowSound)
{
   filename    = "fx/armor/heavy_RF_snow.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(LFootHeavyShallowSplashSound)
{
   filename    = "fx/armor/heavy_LF_water.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootHeavyShallowSplashSound)
{
   filename    = "fx/armor/heavy_RF_water.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(LFootHeavyWadingSound)
{
   filename    = "fx/armor/heavy_LF_uw.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootHeavyWadingSound)
{
   filename    = "fx/armor/heavy_RF_uw.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(LFootHeavyUnderwaterSound)
{
   filename    = "fx/armor/heavy_LF_uw.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(RFootHeavyUnderwaterSound)
{
   filename    = "fx/armor/heavy_RF_uw.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(LFootHeavyBubblesSound)
{
   filename    = "fx/armor/light_LF_bubbles.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootHeavyBubblesSound)
{
   filename    = "fx/armor/light_RF_bubbles.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(ImpactHeavySoftSound)
{
   filename    = "fx/armor/heavy_land_soft.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactHeavyHardSound)
{
   filename    = "fx/armor/heavy_land_hard.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactHeavyMetalSound)
{
   filename    = "fx/armor/heavy_land_metal.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactHeavySnowSound)
{
   filename    = "fx/armor/heavy_land_snow.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ImpactHeavyWaterEasySound)
{
   filename    = "fx/armor/general_water_smallsplash.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactHeavyWaterMediumSound)
{
   filename    = "fx/armor/general_water_medsplash.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactHeavyWaterHardSound)
{
   filename    = "fx/armor/general_water_bigsplash.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(ExitingWaterHeavySound)
{
   filename    = "fx/armor/general_water_exit2.wav";
   description = AudioClose3d;
   preload = true;
};

//----------------------------------------------------------------------------
// Splash
//----------------------------------------------------------------------------

datablock ParticleData(PlayerSplashMist)
{
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.05;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.5;
   sizes[2]      = 0.8;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(PlayerSplashMistEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 3.0;
   velocityVariance = 2.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 250;
   particles = "PlayerSplashMist";
};


datablock ParticleData(PlayerBubbleParticle)
{
   dragCoefficient      = 0.0;
   gravityCoefficient   = -0.50;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   textureName          = "special/bubbles";
   colors[0]     = "0.7 0.8 1.0 0.4";
   colors[1]     = "0.7 0.8 1.0 0.4";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.1;
   sizes[1]      = 0.3;
   sizes[2]      = 0.3;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(PlayerBubbleEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 2.0;
   ejectionOffset   = 0.5;
   velocityVariance = 0.5;
   thetaMin         = 0;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "PlayerBubbleParticle";
};

datablock ParticleData(PlayerFoamParticle)
{
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.05;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "0.7 0.8 1.0 0.20";
   colors[1]     = "0.7 0.8 1.0 0.20";
   colors[2]     = "0.7 0.8 1.0 0.00";
   sizes[0]      = 0.2;
   sizes[1]      = 0.4;
   sizes[2]      = 1.6;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(PlayerFoamEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 3.0;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "PlayerFoamParticle";
};


datablock ParticleData( PlayerFoamDropletsParticle )
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 0;
   textureName          = "special/droplet";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.8;
   sizes[1]      = 0.3;
   sizes[2]      = 0.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( PlayerFoamDropletsEmitter )
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 2;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 60;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   particles = "PlayerFoamDropletsParticle";
};



datablock ParticleData( PlayerSplashParticle )
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 0;
   textureName          = "special/droplet";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.5;
   sizes[2]      = 0.5;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( PlayerSplashEmitter )
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 3;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 60;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 100;
   particles = "PlayerSplashParticle";
};

datablock SplashData(PlayerSplash)
{
   numSegments = 15;
   ejectionFreq = 15;
   ejectionAngle = 40;
   ringLifetime = 0.5;
   lifetimeMS = 300;
   velocity = 4.0;
   startRadius = 0.0;
   acceleration = -3.0;
   texWrap = 5.0;

   texture = "special/water2";

   emitter[0] = PlayerSplashEmitter;
   emitter[1] = PlayerSplashMistEmitter;

   colors[0] = "0.7 0.8 1.0 0.0";
   colors[1] = "0.7 0.8 1.0 0.3";
   colors[2] = "0.7 0.8 1.0 0.7";
   colors[3] = "0.7 0.8 1.0 0.0";
   times[0] = 0.0;
   times[1] = 0.4;
   times[2] = 0.8;
   times[3] = 1.0;
};

//----------------------------------------------------------------------------
// Jet data
//----------------------------------------------------------------------------
datablock ParticleData(HumanArmorJetParticle)
{
   dragCoefficient      = 1.5;  //Was 0.0, original game shiped as 1.5
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 100;  //Was a watered down 100, Original game was 150
   lifetimeVarianceMS   = 0;
   textureName          = "particleTest";
   colors[0]     = "0.32 0.47 0.47 1.0";
   colors[1]     = "0.32 0.47 0.47 0";
   sizes[0]      = 0.40;
   sizes[1]      = 0.15;
};

datablock ParticleEmitterData(HumanArmorJetEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 3;
   velocityVariance = 2.9;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 5;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "HumanArmorJetParticle";
};

datablock JetEffectData(HumanArmorJetEffect)
{
   texture        = "special/jetExhaust02";
   coolColor      = "0.0 0.0 1.0 1.0";
   hotColor       = "0.2 0.4 0.7 1.0";
   activateTime   = 0.2;
   deactivateTime = 0.05;
   length         = 0.75;
   width          = 0.2;
   speed          = -15;
   stretch        = 2.0;
   yOffset        = 0.2;
};

datablock JetEffectData(HumanMediumArmorJetEffect)
{
   texture        = "special/jetExhaust02";
   coolColor      = "0.0 0.0 1.0 1.0";
   hotColor       = "0.2 0.4 0.7 1.0";
   activateTime   = 0.2;
   deactivateTime = 0.05;
   length         = 0.75;
   width          = 0.2;
   speed          = -15;
   stretch        = 2.0;
   yOffset        = 0.4;
};

datablock JetEffectData(HumanLightFemaleArmorJetEffect)
{
   texture        = "special/jetExhaust02";
   coolColor      = "0.0 0.0 1.0 1.0";
   hotColor       = "0.2 0.4 0.7 1.0";
   activateTime   = 0.2;
   deactivateTime = 0.05;
   length         = 0.75;
   width          = 0.2;
   speed          = -15;
   stretch        = 2.0;
   yOffset        = 0.2;
};

datablock JetEffectData(BiodermArmorJetEffect)
{
   texture        = "special/jetExhaust02";
   coolColor      = "0.0 0.0 1.0 1.0";
   hotColor       = "0.8 0.6 0.2 1.0";
   activateTime   = 0.2;
   deactivateTime = 0.05;
   length         = 0.75;
   width          = 0.2;
   speed          = -15;
   stretch        = 2.0;
   yOffset        = 0.0;
};

//----------------------------------------------------------------------------
// Foot puffs
//----------------------------------------------------------------------------
datablock ParticleData(LightPuff)
{
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -35.0;
   spinRandomMax        = 35.0;
   textureName          = "particleTest";
   colors[0]     = "0.46 0.36 0.26 0.4";
   colors[1]     = "0.46 0.46 0.36 0.0";
   sizes[0]      = 0.4;
   sizes[1]      = 1.0;
};

datablock ParticleEmitterData(LightPuffEmitter)
{
   ejectionPeriodMS = 35;
   periodVarianceMS = 10;
   ejectionVelocity = 0.1;
   velocityVariance = 0.05;
   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 20;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   useEmitterColors = true;
   particles = "LightPuff";
};

//----------------------------------------------------------------------------
// Liftoff dust
//----------------------------------------------------------------------------
datablock ParticleData(LiftoffDust)
{
   dragCoefficient      = 1.0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "0.46 0.36 0.26 0.0";
   colors[1]     = "0.46 0.46 0.36 0.4";
   colors[2]     = "0.46 0.46 0.36 0.0";
   sizes[0]      = 0.2;
   sizes[1]      = 0.6;
   sizes[2]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(LiftoffDustEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 2.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 90;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   useEmitterColors = true;
   particles = "LiftoffDust";
};

//----------------------------------------------------------------------------

datablock ParticleData(BiodermArmorJetParticle) : HumanArmorJetParticle
{
   colors[0]     = "0.50 0.48 0.36 1.0";
   colors[1]     = "0.50 0.48 0.36 0";
};

datablock ParticleEmitterData(BiodermArmorJetEmitter) : HumanArmorJetEmitter
{
   particles = "BiodermArmorJetParticle";
};


//----------------------------------------------------------------------------
datablock DecalData(LightMaleFootprint)
{
   sizeX       = 0.125;
   sizeY       = 0.25;
   textureName = "special/footprints/L_male";
};

datablock DebrisData( HumanRedPlayerDebris )
{
   explodeOnMaxBounce = false;

   elasticity = 0.35;
   friction = 0.5;

   lifetime = 4.0;
   lifetimeVariance = 0.0;

   emitters[0] =   HumanRedBloodEmitter;
   emitters[1] =   HumanRedPlayerSplashMistEmitter;
   emitters[2] =   HumanRedDropletsEmitter;

   minSpinSpeed = 60;
   maxSpinSpeed = 900;

   numBounces = 15;
   bounceVariance = 0;

   staticOnMaxBounce = true;
   gravModifier = 1.0;

   useRadiusMass = true;
   baseRadius = 1;

   velocity = 18.0;
   velocityVariance = 12.0;

};

datablock DebrisData( BiodermPlayerDebris )
{
   explodeOnMaxBounce = false;

   elasticity = 0.35;
   friction = 0.5;

   lifetime = 4.0;
   lifetimeVariance = 0.0;

   emitters[0] =   BiodermBloodEmitter;
   emitters[1] =   BiodermPlayerSplashMistEmitter;
   emitters[2] =   PurpleBiodermDropletsEmitter;

   minSpinSpeed = 60;
   maxSpinSpeed = 900;

   numBounces = 15;
   bounceVariance = 0;

   staticOnMaxBounce = true;
   gravModifier = 1.0;

   useRadiusMass = true;
   baseRadius = 1;

   velocity = 18.0;
   velocityVariance = 12.0;

};

datablock DebrisData( PlayerDebris )
{
   explodeOnMaxBounce = false;

   elasticity = 0.35;
   friction = 0.5;

   lifetime = 4.0;
   lifetimeVariance = 0.0;

   minSpinSpeed = 60;
   maxSpinSpeed = 600;

   numBounces = 5;
   bounceVariance = 0;

   staticOnMaxBounce = true;
   gravModifier = 1.0;

   useRadiusMass = true;
   baseRadius = 1;

   velocity = 18.0;
   velocityVariance = 12.0;
};

datablock PlayerData(LightMaleHumanArmor) : LightPlayerDamageProfile
{
   emap = true;

   className = Armor;
   shapeFile = "light_male.dts";
   cameraMaxDist = 3;
   computeCRC = true;

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

   cameraDefaultFov = 90.0;
   cameraMinFov = 5.0;
   cameraMaxFov = 120.0;

   debrisShapeName = "debris_player.dts";
   debris = HumanRedPlayerDebris;

   aiAvoidThis = true;

   minLookAngle = -1.5;
   maxLookAngle = 1.5;
   maxFreelookAngle = 3.0;

   mass = 90;
   drag = 0.275;
   maxdrag = 0.4;
   density = 10;
   maxDamage = 0.66;
   maxEnergy =  60;
   repairRate = 0.0033;
   energyPerDamagePoint = 75.0; // shield energy required to block one point of damage

   rechargeRate = 0.256;
   jetForce = 0;
   underwaterJetForce = 26.21 * 90 * 1.5;
   underwaterVertJetFactor = 1.5;
   jetEnergyDrain =  0.8;
   underwaterJetEnergyDrain = 0.6;
   minJetEnergy = 1;
   maxJetHorizontalPercentage = 0.8;

   runForce = 55.20 * 90;
   runEnergyDrain = 0;
   minRunEnergy = 0;
   maxForwardSpeed = 15;
   maxBackwardSpeed = 13;
   maxSideSpeed = 13;

   maxUnderwaterForwardSpeed = 11;
   maxUnderwaterBackwardSpeed = 10;
   maxUnderwaterSideSpeed = 10;


   jumpForce = 8.3 * 90;
   jumpEnergyDrain = 0;
   minJumpEnergy = 0;
   jumpDelay = 0;


   recoverDelay = 9;
   recoverRunForceScale = 1.2;

   minImpactSpeed = 40;
   speedDamageScale = 0.015;

   jetSound = ArmorJetSound;
   wetJetSound = ArmorJetSound;
   jetEmitter = HumanArmorJetEmitter;
   jetEffect = HumanArmorJetEffect;

   boundingBox = "1.2 1.2 2.3";
   pickupRadius = 0.75;

   // damage location details
   boxNormalHeadPercentage       = 0.83;
   boxNormalTorsoPercentage      = 0.49;
   boxHeadLeftPercentage         = 0;
   boxHeadRightPercentage        = 1;
   boxHeadBackPercentage         = 0;
   boxHeadFrontPercentage        = 1;

   //Foot Prints
   decalData   = LightMaleFootprint;
   decalOffset = 0.25;

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

   // Controls over slope of runnable/jumpable surfaces
   runSurfaceAngle  = 70;
   jumpSurfaceAngle = 80;

   minJumpSpeed = 20;
   maxJumpSpeed = 30;

   horizMaxSpeed = 68;
   horizResistSpeed = 33;
   horizResistFactor = 0.35;
   maxJetForwardSpeed = 30;

   upMaxSpeed = 80;
   upResistSpeed = 25;
   upResistFactor = 0.3;

   // heat inc'ers and dec'ers
   heatDecayPerSec      = 1.0 / 4.0; // takes 4 seconds to clear heat sig.
   heatIncreasePerSec   = 1.0 / 3.0; // takes 3.0 seconds of constant jet to get full heat sig.

   footstepSplashHeight = 0.35;
   //Footstep Sounds
   LFootSoftSound       = LFootLightSoftSound;
   RFootSoftSound       = RFootLightSoftSound;
   LFootHardSound       = LFootLightHardSound;
   RFootHardSound       = RFootLightHardSound;
   LFootMetalSound      = LFootLightMetalSound;
   RFootMetalSound      = RFootLightMetalSound;
   LFootSnowSound       = LFootLightSnowSound;
   RFootSnowSound       = RFootLightSnowSound;
   LFootShallowSound    = LFootLightShallowSplashSound;
   RFootShallowSound    = RFootLightShallowSplashSound;
   LFootWadingSound     = LFootLightWadingSound;
   RFootWadingSound     = RFootLightWadingSound;
   LFootUnderwaterSound = LFootLightUnderwaterSound;
   RFootUnderwaterSound = RFootLightUnderwaterSound;
   LFootBubblesSound    = LFootLightBubblesSound;
   RFootBubblesSound    = RFootLightBubblesSound;
   movingBubblesSound   = ArmorMoveBubblesSound;
   waterBreathSound     = WaterBreathMaleSound;

   impactSoftSound      = ImpactLightSoftSound;
   impactHardSound      = ImpactLightHardSound;
   impactMetalSound     = ImpactLightMetalSound;
   impactSnowSound      = ImpactLightSnowSound;

   skiSoftSound         = SkiAllSoftSound;
   skiHardSound         = SkiAllHardSound;
   skiMetalSound        = SkiAllMetalSound;
   skiSnowSound         = SkiAllSnowSound;

   impactWaterEasy      = ImpactLightWaterEasySound;
   impactWaterMedium    = ImpactLightWaterMediumSound;
   impactWaterHard      = ImpactLightWaterHardSound;

   groundImpactMinSpeed    = 10.0;
   groundImpactShakeFreq   = "4.0 4.0 4.0";
   groundImpactShakeAmp    = "1.0 1.0 1.0";
   groundImpactShakeDuration = 0.8;
   groundImpactShakeFalloff = 10.0;

   exitingWater         = ExitingWaterLightSound;

   maxWeapons = 1;            // Max number of different weapons the player can have
   maxGrenades = 1;           // Max number of different grenades the player can have
   maxMines = 1;              // Max number of different mines the player can have

	// Inventory restrictions
	max[RepairKit]			= 1;
	max[Mine]			= 3;
	max[Grenade]			= 5;
	max[RepairGun]			= 1;
	max[RepairPack]			= 1;
	max[AmmoPack]			= 1;
	max[SatchelCharge]		= 1;
	max[FlashGrenade]		= 5;
	max[ConcussionGrenade]		= 5;
	max[FlareGrenade]		= 5;
	max[TargetingLaser]		= 1;
	max[ShockLance]			= 1;
	max[CameraGrenade]		= 2;
	max[Beacon]			= 3;
	max[PulsePhaser]			= 1;
	max[LD06Savager]			= 1;
	max[LD06SavagerAmmo]			= 10;
	max[GrappleHook]			= 1;
	max[pistol]			= 1;
	max[pistolAmmo]			= 10;
	max[Dualpistol]			= 1;
	max[DualpistolAmmo]			= 20;
	max[DEagle]			= 1;
	max[DEagleAmmo]			= 7;
    max[melee]			= 1;
    max[BOV]			= 1;
    max[Plasmasaber]			= 1;
    max[GravityAxe]			= 1;
	max[S3Rifle]			= 1;
	max[S3RifleAmmo]			= 10;
    max[C4]			= 3;
	max[SCD343]			= 1;
	max[SCD343Ammo]			= 1;
	max[M1700]			= 1;
	max[M1700Ammo]			= 1;
    max[spiker]			= 1;
	max[Model1887]			= 1;
    max[Model1887Ammo]			= 7;
	max[SA2400]			= 1;
	max[SA2400Ammo]			= 21;
	max[G41Rifle]			= 1;
	max[G41RifleAmmo]			= 20;
	max[R700SniperRifle]			= 1;
	max[R700SniperRifleAmmo]			= 4;
	max[Javelin]			= 0;
	max[JavelinAmmo]			= 0;
	max[Stinger]			= 0;
	max[StingerAmmo]			= 0;
	max[MP26]			= 1;
	max[MP26Ammo]			= 30;
	max[Pg700]			= 1;
	max[Pg700Ammo]			= 45;
 	max[PulseRifle]			= 1;
	max[PulseRifleAmmo]			= 20;
	max[PulseSMG]			= 1;
	max[PulseSMGAmmo]			= 45;
	max[M4A1]			= 1;
	max[M4A1Ammo]			= 30;
	max[MiniChaingun]			= 1;
	max[MiniChaingunAmmo]			= 100;
	max[MiniColliderCannon]			= 0;
	max[ShadowRifle]			= 0;
	max[ConcussionGun]			= 0;
	max[ConcussionGunAmmo]			= 0;
	max[M1SniperRifle]			= 1;
	max[M1SniperRifleAmmo]			= 5;
	max[G17SniperRifle]			= 1;
	max[G17SniperRifleAmmo]			= 1;
	max[MissileLauncher]			= 0;
	max[MissileLauncherAmmo]			= 0;
	max[RP432]			= 0;
	max[RP432Ammo]			= 0;
	max[MRXX]			= 0;
	max[MRXXAmmo]			= 0;
	max[Wp400]			= 1;
	max[Wp400Ammo]			= 5;
    max[lasergun]       = 0;
    max[lasergunammo]       = 0;
    max[RPG]       = 1;
    max[RPGAmmo]       = 1;
	max[GravityCannon]			= 1;
    max[MedPack]            = 1;
    max[ClipBox] = 9999;
    max[ParachutePack]		    = 1;
    
    max[ALSWPSniperRifle]       = 1;
    max[ALSWPSniperRifleAmmo]       = 10;
    max[P90]       = 1;
    max[P90Ammo]       = 65;
    max[PlasmaTorpedo]       = 0;
    max[m93]       = 1;
    max[m93Ammo]       = 15;
    max[CrimsonHawk]       = 1;
    max[CrimsonHawkAmmo]       = 15;

	observeParameters = "0.5 4.5 4.5";
	shieldEffectScale = "0.7 0.7 1.0";
};


//----------------------------------------------------------------------------

datablock DecalData(MediumMaleFootprint)
{
   sizeX       = 0.125;
   sizeY       = 0.25;
   textureName = "special/footprints/M_male";
};

datablock PlayerData(MediumMaleHumanArmor) : MediumPlayerDamageProfile
{
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

   // z0dd - ZOD, 10/06/02. Was missing these parameters.
   cameraDefaultFov = 90.0;
   cameraMinFov = 5.0;
   cameraMaxFov = 120.0;

   aiAvoidThis = true;

   minLookAngle = -1.5;
   maxLookAngle = 1.5;
   maxFreelookAngle = 3.0;

   mass = 130;
   drag = 0.3;
   maxdrag = 0.5;
   density = 10;
   maxDamage = 1.1;
   maxEnergy =  80;
   repairRate = 0.0033;
   energyPerDamagePoint = 75.0; // shield energy required to block one point of damage

   rechargeRate = 0.256;
   jetForce = 0;
   underwaterJetForce = 25.22 * 130 * 1.5;
   underwaterVertJetFactor = 1.5;
   jetEnergyDrain =  1.0;
   underwaterJetEnergyDrain =  0.6;
   minJetEnergy = 1;
   maxJetHorizontalPercentage = 0.8;

   runForce = 46 * 130;
   runEnergyDrain = 0;
   minRunEnergy = 0;
   maxForwardSpeed = 12;
   maxBackwardSpeed = 10;
   maxSideSpeed = 10;

   maxUnderwaterForwardSpeed = 8.5;
   maxUnderwaterBackwardSpeed = 7.5;
   maxUnderwaterSideSpeed = 7.5;

   recoverDelay = 9;
   recoverRunForceScale = 1.2;

   // heat inc'ers and dec'ers
   heatDecayPerSec      = 1.0 / 4.0; // takes 4 seconds to clear heat sig.
   heatIncreasePerSec   = 1.0 / 3.0; // takes 3.0 seconds of constant jet to get full heat sig.

   jumpForce = 8.3 * 130;
   jumpEnergyDrain = 0;
   minJumpEnergy = 0;
   jumpSurfaceAngle = 75;
   jumpDelay = 0;

   // Controls over slope of runnable/jumpable surfaces
   runSurfaceAngle  = 70;
   jumpSurfaceAngle = 80;

   minJumpSpeed = 15;
   maxJumpSpeed = 25;

   horizMaxSpeed = 60;
   horizResistSpeed = 28;
   horizResistFactor = 0.32;
   maxJetForwardSpeed = 22;

   upMaxSpeed = 70;
   upResistSpeed = 30;
   upResistFactor = 0.23;

   minImpactSpeed = 40;
   speedDamageScale = 0.015;

   jetSound = ArmorJetSound;
   wetJetSound = ArmorWetJetSound;

   jetEmitter = HumanArmorJetEmitter;
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

	// Inventory restrictions
	max[RepairKit]			= 1;
	max[Mine]			    = 4;
	max[Grenade]			= 6;
	max[RepairGun]			= 1;
	max[RepairPack]			= 1;
	max[AmmoPack]			= 1;
	max[SatchelCharge]		= 1;
	max[FlashGrenade]		= 7;
	max[ConcussionGrenade]	= 6;
	max[FlareGrenade]		= 7;
	max[TargetingLaser]		= 1;
	max[ShockLance]			= 1;
	max[CameraGrenade]		= 2;
	max[Beacon]			= 3;
	max[PulsePhaser]			= 1;
	max[LD06Savager]			= 1;
	max[LD06SavagerAmmo]			= 10;
	max[ConcussionGun]			= 0;
	max[ConcussionGunAmmo]			= 0;
	max[GrappleHook]			= 1;
	max[pistol]			= 1;
	max[pistolAmmo]			= 10;
	max[Dualpistol]			= 1;
	max[DualpistolAmmo]			= 20;
	max[DEagle]			= 1;
	max[DEagleAmmo]			= 7;
    max[melee]			= 1;
    max[GravityAxe]			= 1;
    max[BOV]			= 1;
	max[S3Rifle]			= 1;
	max[S3RifleAmmo]			= 10;
	max[S3SRifle]			= 1;
	max[S3SRifleAmmo]			= 30;
    max[C4]			= 5;
	max[M1700]			= 1;
	max[M1700Ammo]			= 1;
	max[Model1887]			= 1;
 max[Model1887Ammo]			= 7;
	max[M4A1]			= 1;
	max[M4A1Ammo]			= 30;
	max[SA2400]			= 0;
	max[SA2400Ammo]			= 0;
	max[G41Rifle]			= 0;
	max[G41RifleAmmo]			= 0;
	max[R700SniperRifle]			= 0;
	max[R700SniperRifleAmmo]			= 0;
    max[spiker]			= 1;
	max[Javelin]			= 1;
	max[JavelinAmmo]			= 1;
	max[Stinger]			= 1;
	max[StingerAmmo]			= 1;
	max[MP26]			= 1;
	max[MP26Ammo]			= 30;
	max[Mp26CMDO]			= 1;
	max[Mp26CMDOAmmo]			= 50;
	max[Pg700]			= 1;
	max[Pg700Ammo]			= 45;
	max[flamer]			= 1;
	max[flamerAmmo]			= 100;
	max[MiniChaingun]			= 1;
	max[MiniChaingunAmmo]			= 100;
	max[MiniColliderCannon]			= 0;
	max[ShadowRifle]			= 0;
	max[M1SniperRifle]			= 0;
	max[M1SniperRifleAmmo]			= 0;
	max[MissileLauncher]			= 1;
	max[MissileLauncherAmmo]			= 4;
	max[RP432]			= 1;
	max[RP432Ammo]			= 75;
	max[MRXX]			= 1;
	max[MRXXAmmo]			= 150;
	max[Wp400]			= 1;
	max[Wp400Ammo]			= 5;
    max[lasergun]       = 1;
    max[lasergunammo]       = 10;
	max[SCD343]			= 1;
	max[SCD343Ammo]			= 1;
 	max[PulseRifle]			= 1;
	max[PulseRifleAmmo]			= 20;
	max[PulseSMG]			= 1;
	max[PulseSMGAmmo]			= 45;
    max[RPG]       = 1;
    max[RPGAmmo]       = 1;
	max[GravityCannon]			= 1;
	max[G17SniperRifle]			= 0;
	max[G17SniperRifleAmmo]			= 0;
    max[MedPack]            = 1;
    max[Plasmasaber]			= 1;
    max[ClipBox] = 9999;
    max[ParachutePack]		    = 1;
    
    max[ALSWPSniperRifle]       = 0;
    max[ALSWPSniperRifleAmmo]       = 0;
    max[P90]       = 1;
    max[P90Ammo]       = 65;
    max[PlasmaTorpedo]       = 1;
    max[m93]       = 1;
    max[m93Ammo]       = 15;
    max[CrimsonHawk]       = 1;
    max[CrimsonHawkAmmo]       = 15;

	observeParameters = "0.5 4.5 4.5";
	shieldEffectScale = "0.7 0.7 1.0";
};


//----------------------------------------------------------------------------
datablock DecalData(HeavyMaleFootprint)
{
   sizeX       = 0.25;
   sizeY       = 0.5;
   textureName = "special/footprints/H_male";
};

datablock PlayerData(HeavyMaleHumanArmor) : HeavyPlayerDamageProfile
{
   emap = true;

   className = Armor;
   shapeFile = "heavy_male.dts";
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

   // z0dd - ZOD, 10/06/02. Was missing these parameters.
   cameraDefaultFov = 90.0;
   cameraMinFov = 5.0;
   cameraMaxFov = 120.0;

   aiAvoidThis = true;

   minLookAngle = -1.5;
   maxLookAngle = 1.5;
   maxFreelookAngle = 3.0;

   mass = 180;
   drag = 0.33;
   maxdrag = 0.6;
   density = 10;
   maxDamage = 1.32;
   maxEnergy =  110;
   repairRate = 0.0033;
   energyPerDamagePoint = 75.0; // shield energy required to block one point of damage

   rechargeRate = 0.256;
   jetForce = 0;
   underwaterJetForce = 22.47 * 180 * 1.5;
   underwaterVertJetFactor = 1.5;
   jetEnergyDrain =  1.1;
   underwaterJetEnergyDrain =  0.65;
   minJetEnergy = 1;
   maxJetHorizontalPercentage = 0.8;

   runForce = 40.25 * 180;
   runEnergyDrain = 0;
   minRunEnergy = 0;
   maxForwardSpeed = 7;
   maxBackwardSpeed = 5;
   maxSideSpeed = 5;

   maxUnderwaterForwardSpeed = 4.5;
   maxUnderwaterBackwardSpeed = 3;
   maxUnderwaterSideSpeed = 3;

   recoverDelay = 9;
   recoverRunForceScale = 1.2;

   jumpForce = 8.3 * 180;
   jumpEnergyDrain = 0;
   minJumpEnergy = 0;
   jumpDelay = 0;

   // heat inc'ers and dec'ers
   heatDecayPerSec      = 1.0 / 4.0; // takes 4 seconds to clear heat sig.
   heatIncreasePerSec   = 1.0 / 3.0; // takes 3.0 seconds of constant jet to get full heat sig.

   // Controls over slope of runnable/jumpable surfaces
   runSurfaceAngle  = 70;
   jumpSurfaceAngle = 75;

   minJumpSpeed = 20;
   maxJumpSpeed = 30;

   horizMaxSpeed = 52;
   horizResistSpeed = 23;
   horizResistFactor = 0.29;
   maxJetForwardSpeed = 16;

   upMaxSpeed = 60;
   upResistSpeed = 35;
   upResistFactor = 0.18;

   minImpactSpeed = 40;
   speedDamageScale = 0.015;

   jetSound = ArmorJetSound;
   wetJetSound = ArmorJetSound;
   jetEmitter = HumanArmorJetEmitter;

   boundingBox = "1.63 1.63 2.6";
   pickupRadius = 0.75;

   // damage location details
   boxNormalHeadPercentage       = 0.83;
   boxNormalTorsoPercentage      = 0.49;
   boxHeadLeftPercentage         = 0;
   boxHeadRightPercentage        = 1;
   boxHeadBackPercentage         = 0;
   boxHeadFrontPercentage        = 1;

   //Foot Prints
   decalData   = HeavyMaleFootprint;
   decalOffset = 0.4;

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
   LFootSoftSound       = LFootHeavySoftSound;
   RFootSoftSound       = RFootHeavySoftSound;
   LFootHardSound       = LFootHeavyHardSound;
   RFootHardSound       = RFootHeavyHardSound;
   LFootMetalSound      = LFootHeavyMetalSound;
   RFootMetalSound      = RFootHeavyMetalSound;
   LFootSnowSound       = LFootHeavySnowSound;
   RFootSnowSound       = RFootHeavySnowSound;
   LFootShallowSound    = LFootHeavyShallowSplashSound;
   RFootShallowSound    = RFootHeavyShallowSplashSound;
   LFootWadingSound     = LFootHeavyWadingSound;
   RFootWadingSound     = RFootHeavyWadingSound;
   LFootUnderwaterSound = LFootHeavyUnderwaterSound;
   RFootUnderwaterSound = RFootHeavyUnderwaterSound;
   LFootBubblesSound    = LFootHeavyBubblesSound;
   RFootBubblesSound    = RFootHeavyBubblesSound;
   movingBubblesSound   = ArmorMoveBubblesSound;
   waterBreathSound     = WaterBreathMaleSound;

   impactSoftSound      = ImpactHeavySoftSound;
   impactHardSound      = ImpactHeavyHardSound;
   impactMetalSound     = ImpactHeavyMetalSound;
   impactSnowSound      = ImpactHeavySnowSound;

   skiSoftSound         = SkiAllSoftSound;
   skiHardSound         = SkiAllHardSound;
   skiMetalSound        = SkiAllMetalSound;
   skiSnowSound         = SkiAllSnowSound;

   impactWaterEasy      = ImpactHeavyWaterEasySound;
   impactWaterMedium    = ImpactHeavyWaterMediumSound;
   impactWaterHard      = ImpactHeavyWaterHardSound;

   groundImpactMinSpeed    = 10.0;
   groundImpactShakeFreq   = "4.0 4.0 4.0";
   groundImpactShakeAmp    = "1.0 1.0 1.0";
   groundImpactShakeDuration = 0.8;
   groundImpactShakeFalloff = 10.0;

   exitingWater         = ExitingWaterHeavySound;

   maxWeapons = 3;            // Max number of different weapons the player can have
   maxGrenades = 1;           // Max number of different grenades the player can have
   maxMines = 1;              // Max number of different mines the player can have

	// Inventory restrictions
	max[RepairKit]			= 1;
	max[Mine]			    = 5;
	max[Grenade]			= 7;
	max[RepairGun]			= 1;
	max[RepairPack]			= 1;
	max[AmmoPack]			= 1;
	max[SatchelCharge]		= 1;
	max[FlashGrenade]		= 9;
	max[ConcussionGrenade]	= 7;
	max[FlareGrenade]		= 9;
	max[TargetingLaser]		= 1;
	max[ShockLance]			= 1;
	max[CameraGrenade]		= 2;
	max[Beacon]			= 3;
	max[PulsePhaser]			= 1;
	max[LD06Savager]			= 1;
	max[LD06SavagerAmmo]			= 10;
	max[GrappleHook]			= 1;
	max[pistol]			= 1;
	max[pistolAmmo]			= 10;
	max[Dualpistol]			= 1;
	max[DualpistolAmmo]			= 20;
	max[DEagle]			= 1;
	max[DEagleAmmo]			= 7;
	max[M4A1]			= 0;
	max[M4A1Ammo]			= 0;
	max[G17SniperRifle]			= 0;
	max[G17SniperRifleAmmo]			= 0;
 	max[PulseRifle]			= 0;
	max[PulseRifleAmmo]			= 0;
	max[PulseSMG]			= 0;
	max[PulseSMGAmmo]			= 0;
    max[melee]			= 1;
    max[GravityAxe]			= 0;
    max[spiker]			= 0;
    max[BOV]			= 1;
	max[M1700]			= 1;
	max[M1700Ammo]			= 1;
	max[Model1887]			= 1;
    max[Model1887Ammo]			= 7;
	max[SA2400]			= 1;
	max[SA2400Ammo]			= 21;
	max[SCD343]			= 0;
	max[SCD343Ammo]			= 0;
	max[flamer]			= 1;
	max[flamerAmmo]			= 100;
	max[ConcussionGun]			= 0;
	max[ConcussionGunAmmo]			= 0;
	max[G41Rifle]			= 0;
	max[G41RifleAmmo]			= 0;
	max[R700SniperRifle]			= 1;
	max[R700SniperRifleAmmo]			= 4;
	max[MiniChaingun]			= 1;
	max[MiniChaingunAmmo]			= 100;
	max[MiniColliderCannon]			= 1;
	max[ShadowRifle]			= 0;
	max[MP26]			= 0;
	max[MP26Ammo]			= 0;
	max[Pg700]			= 0;
	max[Pg700Ammo]			= 0;
	max[M1SniperRifle]			= 0;
	max[M1SniperRifleAmmo]			= 0;
	max[MissileLauncher]			= 1;
	max[MissileLauncherAmmo]			= 7;
	max[RP432]			= 1;
	max[RP432Ammo]			= 75;
	max[MRXX]			= 1;
	max[MRXXAmmo]			= 150;
	max[MG42]			= 1;
	max[MG42Ammo]			= 200;
	max[Wp400]			= 1;
	max[Wp400Ammo]			= 5;
    max[lasergun]       = 0;
    max[lasergunammo]       = 0;
    max[RPG]       = 1;
    max[RPGAmmo]       = 1;
	max[Javelin]			= 1;
	max[JavelinAmmo]			= 1;
	max[Stinger]			= 1;
	max[StingerAmmo]			= 1;
	max[GravityCannon]			= 1;
    max[Plasmasaber]			= 1;
    max[ClipBox] = 9999;
    max[ParachutePack]		    = 1;
    
    max[ALSWPSniperRifle]       = 0;
    max[ALSWPSniperRifleAmmo]       = 0;
    max[P90]       = 0;
    max[P90Ammo]       = 0;
    max[PlasmaTorpedo]       = 1;
    max[m93]       = 1;
    max[m93Ammo]       = 15;
    max[CrimsonHawk]       = 1;
    max[CrimsonHawkAmmo]       = 15;

	observeParameters = "0.5 4.5 4.5";
	shieldEffectScale = "0.7 0.7 1.0";
};


//----------------------------------------------------------------------------
datablock PlayerData(LightFemaleHumanArmor) : LightMaleHumanArmor
{
   shapeFile = "light_female.dts";
   waterBreathSound = WaterBreathFemaleSound;
   jetEffect =  HumanMediumArmorJetEffect;
};

//----------------------------------------------------------------------------
datablock PlayerData(MediumFemaleHumanArmor) : MediumMaleHumanArmor
{
   shapeFile = "medium_female.dts";
   waterBreathSound = WaterBreathFemaleSound;
   jetEffect =  HumanArmorJetEffect;
};

//----------------------------------------------------------------------------
datablock PlayerData(HeavyFemaleHumanArmor) : HeavyMaleHumanArmor
{
   shapeFile = "heavy_male.dts";
   waterBreathSound = WaterBreathFemaleSound;
};

//----------------------------------------------------------------------------
datablock DecalData(LightBiodermFootprint)
{
   sizeX       = 0.25;
   sizeY       = 0.25;
   textureName = "special/footprints/L_bioderm";
};

datablock PlayerData(LightMaleBiodermArmor) : LightMaleHumanArmor
{
   shapeFile = "bioderm_light.dts";
   jetEmitter = BiodermArmorJetEmitter;
   jetEffect =  BiodermArmorJetEffect;


   debrisShapeName = "bio_player_debris.dts";
   debris = BiodermPlayerDebris;


   //Foot Prints
   decalData   = LightBiodermFootprint;
   decalOffset = 0.3;

   waterBreathSound = WaterBreathBiodermSound;
};

//----------------------------------------------------------------------------
datablock DecalData(MediumBiodermFootprint)
{
   sizeX       = 0.25;
   sizeY       = 0.25;
   textureName = "special/footprints/M_bioderm";
};

datablock PlayerData(MediumMaleBiodermArmor) : MediumMaleHumanArmor
{
   shapeFile = "bioderm_medium.dts";
   jetEmitter = BiodermArmorJetEmitter;
   jetEffect =  BiodermArmorJetEffect;

   debrisShapeName = "bio_player_debris.dts";
   debris = BiodermPlayerDebris;

   //Foot Prints
   decalData   = MediumBiodermFootprint;
   decalOffset = 0.35;

   waterBreathSound = WaterBreathBiodermSound;
};

//----------------------------------------------------------------------------
datablock DecalData(HeavyBiodermFootprint)
{
   sizeX       = 0.25;
   sizeY       = 0.5;
   textureName = "special/footprints/H_bioderm";
};

datablock PlayerData(HeavyMaleBiodermArmor) : HeavyMaleHumanArmor
{
   emap = false;

   shapeFile = "bioderm_heavy.dts";
   jetEmitter = BiodermArmorJetEmitter;

   debrisShapeName = "bio_player_debris.dts";
   debris = BiodermPlayerDebris;

   //Foot Prints
   decalData    = HeavyBiodermFootprint;
   decalOffset  = 0.4;

   waterBreathSound = WaterBreathBiodermSound;
};





// --------------------------------------------------------------------------
// True Build Armors
// --------------------------------------------------------------------------

datablock PlayerData(PureMaleHumanArmor) : LightMaleHumanArmor
{
   jetForce = 26.21 * 120; // 26.21 * 90
   underwaterJetForce = 26.21 * 120 * 1.5; // 26.21 * 90 * 1.5

   maxEnergy =  60;
   jetEnergyDrain =  0.1;
   underwaterJetEnergyDrain = 0.05;
   rechargeRate = 0.256;
   mass = 115;

   minImpactSpeed = 450;
   speedDamageScale = 0.0004;

   maxWeapons = 4;            // Max number of different weapons the player can have
   maxGrenades = 1;           // Max number of different grenades the player can have
   maxMines = 0;              // Max number of different mines the player can have

	// Inventory restrictions
	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
	max[Blaster]			= 0;
	max[Plasma]			= 0;
	max[PlasmaAmmo]			= 0;
	max[Disc]			= 0;
	max[DiscAmmo]			= 0;
	max[SniperRifle]		= 0;
	max[GrenadeLauncher]		= 0;
	max[GrenadeLauncherAmmo]	= 0;
	max[Mortar]			= 0;
	max[MortarAmmo]			= 0;
	max[MissileLauncher]		= 0;
	max[MissileLauncherAmmo]	= 0;
	max[MiniChaingun]			= 0;
	max[MiniChaingunAmmo]		= 0;
	max[ShadowRifle]			= 0;
 	max[PulseRifle]			= 0;
	max[PulseRifleAmmo]			= 0;
	max[PulseSMG]			= 0;
	max[PulseSMGAmmo]			= 0;
	max[RepairGun]			= 1;
	max[CloakingPack]		= 0;
	max[SensorJammerPack]		= 0;
	max[EnergyPack]			= 0;
	max[RepairPack]			= 1;
	max[ShieldPack]			= 0;
	max[AmmoPack]			= 0;
	max[SatchelCharge]		= 0;
	max[MortarBarrelPack]		= 1;
	max[MissileBarrelPack]		= 1;
	max[AABarrelPack]		= 1;
	max[PlasmaBarrelPack]		= 1;
	max[ELFBarrelPack]		= 1;
	max[InventoryDeployable]	= 1;
	max[MotionSensorDeployable]	= 1;
	max[PulseSensorDeployable]	= 1;
	max[TurretOutdoorDeployable]	= 1;
	max[TurretIndoorDeployable]	= 1;
	max[FlashGrenade]		= 0;
	max[ConcussionGrenade]		= 0;
	max[FlareGrenade]		= 5;
	max[TargetingLaser]		= 1;
	max[ELFGun]			= 0;
    max[GravityAxe]			= 0;
	max[ShockLance]			= 0;
	max[CameraGrenade]		= 2;
	max[Beacon]			= 5;
	//Guns
	max[ConstructionTool]		= 1;
	max[NerfGun] 			= 1;
	max[NerfBallLauncher] 	 	= 1;
	max[NerfBallLauncherAmmo]	= 25;
	max[SuperChaingun] 	 	= 0;
	max[SuperChaingunAmmo]		= 0;
    max[MergeTool] = 1;
	max[EditorGun]		= 1;
	max[EditTool]		= 1;
	max[ConcussionGun]			= 0;
	max[ConcussionGunAmmo]			= 0;
	max[Stinger]			= 0;
	max[StingerAmmo]			= 0;
 
    max[ALSWPSniperRifle]       = 0;
    max[ALSWPSniperRifleAmmo]       = 0;
    max[P90]       = 0;
    max[P90Ammo]       = 0;
    max[PlasmaTorpedo]       = 0;
    max[m93]       = 1;
    max[m93Ammo]       = 15;
    max[CrimsonHawk]       = 1;
    max[CrimsonHawkAmmo]       = 15;
	max[MRXX]			= 0;
	max[MRXXAmmo]			= 0;
    max[spiker]			= 0;
 
	//Building parts
	max[CardPackDeployable]		= 1;
	max[SpawnPointPack]		= 1;
	max[spineDeployable]		= 1;
	max[mspineDeployable]		= 1;
	max[wWallDeployable]		= 1;
	max[floorDeployable]		= 1;
	max[WallDeployable]		= 1;
	//Turrets
	max[TurretLaserDeployable]	= 1;
	max[TurretMissileRackDeployable]= 1;
	max[DiscTurretDeployable]	= 0;
	//Largepacks
	max[EnergizerDeployable]	= 0;
	max[TreeDeployable]		= 1;
	max[CrateDeployable]		= 1;
	max[DecorationDeployable]	= 1;
	max[LogoProjectorDeployable]	= 1;
	max[LightDeployable]		= 1;
	max[TripwireDeployable]		= 1;
	max[TelePadPack]		= 1;
	max[TurretBasePack]		= 1;
	max[LargeInventoryDeployable]	= 1;
	max[GeneratorDeployable]	= 1;
	max[SolarPanelDeployable]	= 1;
	max[SwitchDeployable]		= 1;
	max[MediumSensorDeployable]	= 1;
	max[LargeSensorDeployable]	= 1;
    max[DoorDeployable]         = 1;
	//Misc
	max[JumpadDeployable]		= 1;
	max[EscapePodDeployable]	= 1;
	max[ForceFieldDeployable]	= 1;
	max[GravityFieldDeployable]	= 1;

	max[S3Rifle]			= 0;
	max[S3RifleAmmo]			= 0;
	max[Javelin]			= 0;
	max[JavelinAmmo]			= 0;
    max[C4]			= 0;
	max[M1700]			= 0;
	max[M1700Ammo]			= 0;
	max[Model1887]			= 0;
	max[Model1887Ammo]			= 0;
	max[G41Rifle]			= 0;
	max[G41RifleAmmo]			= 0;
	max[R700SniperRifle]			= 0;
	max[R700SniperRifleAmmo]			= 0;
	max[MP26]			= 0;
	max[MP26Ammo]			= 0;
	max[Pg700]			= 0;
	max[Pg700Ammo]			= 0;
	max[M1SniperRifle]			= 0;
	max[M1SniperRifleAmmo]			= 0;
	max[MissileLauncher]			= 0;
	max[MissileLauncherAmmo]			= 0;
	max[M4A1]			= 0;
	max[M4A1Ammo]			= 0;
	max[RP432]			= 0;
	max[RP432Ammo]			= 0;
	max[Chaingun]			= 0;
	max[ChaingunAmmo]			= 0;
	max[Wp400]			= 0;
	max[Wp400Ammo]			= 0;
	max[SCD343]			= 0;
	max[SCD343Ammo]			= 0;
    max[lasergun]       = 0;
    max[lasergunamo]       = 0;
    max[RPG]       = 0;
    max[RPGAmmo]       = 0;
	max[GravityCannon]			= 0;
	max[PulsePhaser]			= 1;
	max[LD06Savager]			= 1;
	max[LD06SavagerAmmo]			= 10;
	max[GrappleHook]			= 1;
	max[pistol]			= 1;
	max[pistolAmmo]			= 10;
	max[Dualpistol]			= 1;
	max[DualpistolAmmo]			= 20;
	max[DEagle]			= 1;
	max[DEagleAmmo]			= 7;
	max[G17SniperRifle]			= 0;
	max[G17SniperRifleAmmo]			= 0;
	max[SA2400]			= 0;
	max[SA2400Ammo]			= 0;
    max[ParachutePack]		    = 1;
    
        //Some small additions
        //Note all that keeps us from full plugin compability is this code.
        max[TractorGun]			= 1;
        max[TransGun]			= 1;
        max[VehiclePadPack]		= 1;
        max[EmitterDepPack]		= 1;
        max[AudioDepPack]		= 1;
        max[DispenserDepPack]		= 1;
        max[DetonationDepPack]		= 1;
        max[TransDepPack]		= 1;
    max[ClipBox] = 9999;

	observeParameters = "0.5 4.5 4.5";
	shieldEffectScale = "0.7 0.7 1.0";
};


//----------------------------------------------------------------------------
datablock PlayerData(PureFemaleHumanArmor) : PureMaleHumanArmor
{
   shapeFile = "light_female.dts";
   waterBreathSound = WaterBreathFemaleSound;
   jetEffect =  HumanMediumArmorJetEffect;
};

datablock PlayerData(PureMaleBiodermArmor) : PureMaleHumanArmor
{
   shapeFile = "bioderm_light.dts";
   jetEmitter = BiodermArmorJetEmitter;
   jetEffect =  BiodermArmorJetEffect;

   debrisShapeName = "bio_player_debris.dts";
   debris = BiodermPlayerDebris;

   //Foot Prints
   decalData   = LightBiodermFootprint;
   decalOffset = 0.3;

   waterBreathSound = WaterBreathBiodermSound;
};

// --------------------------------------------------------------------------
// End True Build Armors
// --------------------------------------------------------------------------

//----------------------------------------------------------------------------

function Armor::onAdd(%data,%obj)
{
   Parent::onAdd(%data, %obj);
   // Vehicle timeout
   %obj.mountVehicle = true;

   // Default dynamic armor stats
   %obj.setRechargeRate(%data.rechargeRate);
   %obj.setRepairRate(0);

   %obj.setSelfPowered();
   //TWM2 : Recharge shields if they have them
   if(%obj.getMaxShieldHealth() > 0) {
      %obj.rechargeShields(%data.shieldHeathCharge);
      %obj.activeShieldEffect();
   }
}

function Armor::onRemove(%this, %obj)
{
   //Frohny asked me to remove this - all players are deleted now on mission cycle...
   //if(%obj.getState() !$= "Dead")
   //{
   //   error("ERROR PLAYER REMOVED WITHOUT DEATH - TRACE:");
   //   trace(1);
   //   schedule(0,0,trace,0);
   //}

   if (%obj.client.player == %obj)
      %obj.client.player = 0;
}

function Armor::onNewDataBlock(%this,%obj)
{
}

function Armor::onDisabled(%this,%obj,%state) {
   %obj.revived = 0;
   if(%obj.Infected == 1 && !%obj.iszombie){
	  %obj.createTheZ = schedule(14000, %obj, "TWM2Lib_Zombie_Core", "spawnZombie", "infectedplayer", %obj);
	  %obj.revcheck = schedule(($CorpseTimeoutValue) - %fadeTime, %obj, "checkIfRevived", %obj);
   }
   else {
      %fadeTime = 1000;
	  %obj.revcheck = schedule(($CorpseTimeoutValue) - %fadeTime, %obj, "checkIfRevived", %obj);
   }
   if(%obj.isZombie || %obj.isBoss || %obj.isGOF || %obj.isAllyBot){
      if(%obj.isZombie) {
         $Game::ZombieCount--;
         if($Game::ZombieCount < 0) {  //nice?
            $Game::ZombieCount = 0; //We can easily reset this
         }
      }

      freeclienttarget(%obj);
      %sound = getRandom(1,3);
      if(%sound == 1) {
         ServerPlay3d(ZombieDeathSound1, %obj.getPosition());
      }
      else if(%sound == 2) {
         ServerPlay3d(ZombieDeathSound2, %obj.getPosition());
      }
      else {
         ServerPlay3d(ZombieDeathSound3, %obj.getPosition());
      }
      %dodeath = (getrandom() * 3);
	  if(%dodeath <= 1)
         %obj.setActionThread("Death" @ $PlayerDeathAnim::HeadFrontDirect);
	  else if(%dodeath <= 2)
         %obj.setActionThread("Death" @ $PlayerDeathAnim::HeadBackFallForward);
      else
         %obj.setActionThread("Death" @ $PlayerDeathAnim::TorsoBackFallForward);
	  if(%obj.hasCP == 1) {
	     if(isObject(%obj.cp))
		    %obj.CP.numZ = (%obj.CP.numZ - 1);
	  }
      if(%obj.randspawnerstarted == 1)
         $numspawnedzombies--;
   }
}

function checkIfRevived(%obj){
   if(%obj.revived != 1) {
      %fadeTime = 1000;
      %obj.startFade( %fadeTime, 1, true );
   	  %obj.schedule(%fadeTime, "delete");
   }
   else {
      %obj.revived = 0;
   }
}

function Armor::shouldApplyImpulse(%data, %obj)
{
   return true;
}

$wasFirstPerson = true;

function Armor::onMount(%this,%obj,%vehicle,%node)
{
//[most]
if (%vehicle.base.leftpad == %vehicle || %vehicle.base.rightpad == %vehicle)
   {
   %node = 1;
   %obj.mvehicle = %vehicle;
   }
//[most]

   if (%node == 0)
   {
      // Node 0 is the pilot's pos.
      %obj.setTransform("0 0 0 0 0 1 0");
      %obj.setActionThread(%vehicle.getDatablock().mountPose[%node],true,true);

      if(!%obj.inStation)
         %obj.lastWeapon = (%obj.getMountedImage($WeaponSlot) == 0 ) ? "" : %obj.getMountedImage($WeaponSlot).getName().item;

       %obj.unmountImage($WeaponSlot);

      if(!%obj.client.isAIControlled())
      {
         %obj.setControlObject(%vehicle);
         %obj.client.setObjectActiveImage(%vehicle, 2);
      }

      //E3 respawn...

      if(%obj == %obj.lastVehicle.lastPilot && %obj.lastVehicle != %vehicle)
      {
         schedule(240000, %obj.lastVehicle,"vehicleAbandonTimeOut", %obj.lastVehicle);
          %obj.lastVehicle.lastPilot = "";
      }
      if(%vehicle.lastPilot !$= "" && %vehicle == %vehicle.lastPilot.lastVehicle)
            %vehicle.lastPilot.lastVehicle = "";

      %vehicle.abandon = false;
      %vehicle.lastPilot = %obj;
      %obj.lastVehicle = %vehicle;

      // update the vehicle's team
      if((%vehicle.getTarget() != -1) && %vehicle.getDatablock().cantTeamSwitch $= "")
      {
         setTargetSensorGroup(%vehicle.getTarget(), %obj.client.getSensorGroup());
         if( %vehicle.turretObject > 0 )
            setTargetSensorGroup(%vehicle.turretObject.getTarget(), %obj.client.getSensorGroup());
      }

      // Send a message to the client so they can decide if they want to change view or not:
      commandToClient( %obj.client, 'VehicleMount' );

   }
   else
   {
      // tailgunner/passenger positions
      if(%vehicle.getDataBlock().mountPose[%node] !$= "")
         %obj.setActionThread(%vehicle.getDatablock().mountPose[%node]);
      else
         %obj.setActionThread("root", true);
   }
   // -------------------------------------------------------------------------
   // z0dd - ZOD, 10/06/02. announce to any other passengers that you've boarded
   if(%vehicle.getDatablock().numMountPoints > 1)
   {
      %nodeName = findNodeName(%vehicle, %node); // function in vehicle.cs
      for(%i = 0; %i < %vehicle.getDatablock().numMountPoints; %i++)
      {
         if (%vehicle.getMountNodeObject(%i) > 0)
         {
            if(%vehicle.getMountNodeObject(%i).client != %obj.client)
            {
               %team = (%obj.team == %vehicle.getMountNodeObject(%i).client.team ? 'Teammate' : 'Enemy');
               messageClient( %vehicle.getMountNodeObject(%i).client, 'MsgShowPassenger', '\c2%3: \c3%1\c2 has boarded in the \c3%2\c2 position.', %obj.client.name, %nodeName, %team );
            }
            commandToClient( %vehicle.getMountNodeObject(%i).client, 'showPassenger', %node, true);
         }
      }
   }
   //make sure they don't have any packs active
//    if ( %obj.getImageState( $BackpackSlot ) $= "activate")
//       %obj.use("Backpack");
   if ( %obj.getImageTrigger( $BackpackSlot ) )
      %obj.setImageTrigger( $BackpackSlot, false );

   //AI hooks
   %obj.client.vehicleMounted = %vehicle;
   AIVehicleMounted(%vehicle);
   if(!%obj.isZombie)
      if(%obj.client.isAIControlled())
         %this.AIonMount(%obj, %vehicle, %node);
}


function Armor::onUnmount( %this, %obj, %vehicle, %node )
{
   if ( %node == 0 )
   {
      commandToClient( %obj.client, 'VehicleDismount' );
      commandToClient(%obj.client, 'removeReticle');

      if(%obj.inv[%obj.lastWeapon])
         %obj.use(%obj.lastWeapon);

      if(%obj.getMountedImage($WeaponSlot) == 0)
         %obj.selectWeaponSlot( 0 );

      //Inform gunner position when pilot leaves...
      //if(%vehicle.getDataBlock().showPilotInfo !$= "")
      //   if((%gunner = %vehicle.getMountNodeObject(1)) != 0)
      //      commandToClient(%gunner.client, 'PilotInfo', "PILOT EJECTED", 6, 1);
   }
   // ----------------------------------------------------------------------
   // z0dd - ZOD, 10/06/02. announce to any other passengers that you've left
   if(%vehicle.getDatablock().numMountPoints > 1)
   {
      %nodeName = findNodeName(%vehicle, %node); // function in vehicle.cs
      for(%i = 0; %i < %vehicle.getDatablock().numMountPoints; %i++)
      {
         if (%vehicle.getMountNodeObject(%i) > 0)
         {
            if(%vehicle.getMountNodeObject(%i).client != %obj.client)
            {
               %team = (%obj.team == %vehicle.getMountNodeObject(%i).client.team ? 'Teammate' : 'Enemy');
               messageClient( %vehicle.getMountNodeObject(%i).client, 'MsgShowPassenger', '\c2%3: \c3%1\c2 has ejected from the \c3%2\c2 position.', %obj.client.name, %nodeName, %team );
            }
            commandToClient( %vehicle.getMountNodeObject(%i).client, 'showPassenger', %node, false);
         }
      }
   }
   //AI hooks
   %obj.client.vehicleMounted = "";
   if(!%obj.isZombie)
      if(%obj.client.isAIControlled())
         %this.AIonUnMount(%obj, %vehicle, %node);
}

$ammoType[0] = "PlasmaAmmo";
$ammoType[1] = "DiscAmmo";
$ammoType[2] = "GrenadeLauncherAmmo";
$ammoType[3] = "MortarAmmo";
$ammoType[4] = "MissileLauncherAmmo";
$ammoType[5] = "ChaingunAmmo";
// z0dd - ZOD, 9/13/02. TR2 weapons
$ammoType[6] = "TR2DiscAmmo";
$ammoType[7] = "TR2GrenadeLauncherAmmo";
$ammoType[8] = "TR2ChaingunAmmo";
$ammoType[9] = "TR2MortarAmmo";
$ammoType[10] = "NerfBallLauncherAmmo";
$ammoType[11] = "SuperChaingunAmmo";
//TWM2
$ammoType[12] = "pistolAmmo";
$ammoType[13] = "S3RifleAmmo";
$ammoType[14] = "M1700Ammo";
$ammoType[15] = "G41RifleAmmo";
$ammoType[16] = "R700SniperRifleAmmo";
$ammoType[17] = "MP26Ammo";
$ammoType[18] = "Pg700Ammo";
$ammoType[19] = "M1SniperRifleAmmo";
$ammoType[20] = "MissileLauncherAmmo";
$ammoType[21] = "RP432Ammo";
$ammoType[22] = "MiniChaingunAmmo";
$ammoType[23] = "Wp400Ammo";
$ammoType[24] = "lasergunAmmo";
$ammoType[25] = "RPGAmmo";
$ammoType[26] = "IonLauncherAmmo";
$ammoType[27] = "MG42Ammo";
$ammoType[28] = "flamerAmmo";
$ammoType[29] = "G17SniperRifleAmmo";
$ammoType[30] = "ConcussionGunAmmo";
$ammoType[31] = "S3SRifleAmmo";
$ammoType[32] = "MP26CMDOAmmo";
$ammoType[33] = "SA2400Ammo";
$ammoType[34] = "DualpistolAmmo";
$ammoType[35] = "DeagleAmmo";
$ammoType[36] = "JavelinAmmo";
$ammoType[37] = "SCD343Ammo";
$ammoType[38] = "M4A1Ammo";
$ammoType[39] = "PulseRifleAmmo";
$ammoType[40] = "PulseSMGAmmo";
$ammoType[41] = "ALSWPSniperRifleAmmo";
$ammoType[42] = "P90Ammo";
$ammoType[43] = "m93Ammo";
$ammoType[44] = "CrimsonHawkAmmo";
$ammoType[45] = "StingerAmmo";
$ammoType[46] = "MRXXAmmo";
$ammoType[47] = "Model1887Ammo";
$ammoType[48] = "NapalmAmmo";


function Armor::onCollision(%this,%obj,%col,%forceVehicleNode)
{
   if (%obj.getState() $= "Dead")
      return;

   %dataBlock = %col.getDataBlock();
   %colarmortype = %Datablock.getname();
   %className = %dataBlock.className;
   %objarmortype = %obj.getdatablock().getname();
   %client = %obj.client;
   // player collided with a vehicle
   %node = -1;
   if (%forceVehicleNode !$= "" || (%className $= WheeledVehicleData || %className $= FlyingVehicleData || %className $= HoverVehicleData) &&
         %obj.mountVehicle && %obj.getState() $= "Move" && %col.mountable && !%obj.inStation && %col.getDamageState() !$= "Destroyed") {

	// Escape pod
	if (%dataBlock == EscapePodVehicle.getID()) {
		if (%dataBlock.isBlocked(%col,%obj)) {
			if (%col.blockedTime + 1000 < getSimTime()) {
				%col.blockedTime = getSimTime();
				%col.play3D(TelePadAccessDeniedSound);
				messageClient(%obj.client,'msgClient','\c2Unable to launch, escape pod is blocked!');
			}
			return;
		}
	}

      //if the player is an AI, he should snap to the mount points in node order,
      //to ensure they mount the turret before the passenger seat, regardless of where they collide...
      if (!%obj.isZombie && %obj.client.isAIControlled())
      {
         %transform = %col.getTransform();

         //either the AI is *required* to pilot, or they'll pick the first available passenger seat
         if (%client.pilotVehicle)
         {
            //make sure the bot is in light armor
            if (%client.player.getArmorSize() $= "Light")
            {
               //make sure the pilot seat is empty
               if (!%col.getMountNodeObject(0))
                  %node = 0;
            }
         }
         else
            %node = findAIEmptySeat(%col, %obj);
      }
      else
         %node = findEmptySeat(%col, %obj, %forceVehicleNode);

      //now mount the player in the vehicle
      if(%node >= 0)
      {
         // players can't be pilots, bombardiers or turreteers if they have
         // "large" packs -- stations, turrets, turret barrels
         if(hasLargePack(%obj)) {
            // check to see if attempting to enter a "sitting" node
            if(nodeIsSitting(%datablock, %node)) {
               // send the player a message -- can't sit here with large pack
               if(!%obj.noSitMessage)
               {
                  %obj.noSitMessage = true;
                  %obj.schedule(2000, "resetSitMessage");
                  messageClient(%obj.client, 'MsgCantSitHere', '\c2Pack too large, can\'t occupy this seat.~wfx/misc/misc.error.wav');
               }
               return;
            }
         }
         if(%col.noEnemyControl && %obj.team != %col.team)
            return;

         commandToClient(%obj.client,'SetDefaultVehicleKeys', true);
         //If pilot or passenger then bind a few extra keys
         if(%node == 0)
            commandToClient(%obj.client,'SetPilotVehicleKeys', true);
         else
            commandToClient(%obj.client,'SetPassengerVehicleKeys', true);

         if(!%obj.inStation)
            %col.lastWeapon = ( %col.getMountedImage($WeaponSlot) == 0 ) ? "" : %col.getMountedImage($WeaponSlot).getName().item;
         else
            %col.lastWeapon = %obj.lastWeapon;

	%obj.preVehicleMountPos = %obj.getPosition();

         %col.mountObject(%obj,%node);
         %col.playAudio(0, MountVehicleSound);
         %obj.mVehicle = %col;

			// if player is repairing something, stop it
			if(%obj.repairing)
				stopRepairing(%obj);

         //this will setup the huds as well...
         %dataBlock.playerMounted(%col,%obj, %node);
      }
   }
	// TODO - keep these up to date
	else if (%className $= "wall" || %className $= "wwall" || %className $= "spine" || %className $= "mspine" || %className $= "floor") {
		%obj.playAudio(0,(GetRandom()*2 >1) ? LFootMediumMetalSound : RFootMediumMetalSound);
		%obj.lastDepCol = %col;
		%obj.lastDepColPos = %obj.getPosition();
                if (%className $= "wall")
	            doorfunction(%obj,%col);
	}
   else if (%className $= "Armor") {
      // player has collided with another player
	  if(%obj.isZombie) {
	     %objiszomb = 1;
      }
	  else {
	     %objiszomb = 0;
      }
      if(%col.onfire == 1 && %obj.onfire != 1 && !%obj.rapierShield){
	     %obj.maxfirecount = (%col.maxfirecount - %col.firecount);
	     %obj.onfire = 1;
	     schedule(10, %obj, "burnloop", %obj);
      }
      if(%col.getState() $= "Dead" && !%objiszomb) {
         %obj.lasttouchedcorpse = %col;
         %gotSomething = false;
         // it's corpse-looting time!
         // weapons -- don't pick up more than you are allowed to carry!
         for(%i = 0; ( %obj.weaponCount < %obj.getDatablock().maxWeapons ) && $InvWeapon[%i] !$= ""; %i++)
         {
            %weap = $NameToInv[$InvWeapon[%i]];
            if ( %col.hasInventory( %weap ) )
            {
               if ( %obj.incInventory(%weap, 1) > 0 )
               {
                  %col.decInventory(%weap, 1);
                  %gotSomething = true;
                  messageClient(%obj.client, 'MsgItemPickup', '\c0You picked up %1.', %weap.pickUpName);
               }
               //New Clip Stuff
               %WImg = %weap @ "Image"; //I love how there are so many ways
                                        //To get The WeaponImage with only
                                        //Ammo or NameToInv :p -Phantom
               %ClipsLeft = %obj.ClipCount[%WImg.ClipName];
               %MaxClips = %WImg.InitialClips; //Initial == Max?
               %ColClipsLeft = %col.ClipCount[%WImg.ClipName];
               //AHA! Pickup time
               if(%ClipsLeft < %MaxClips) {
                  %clipsToAdd = %MaxClips - %ClipsLeft;
                  //this if checks the dead player's remaining clips
                  //If needed is more than what we have, we go to the
                  //else, otherwise, we add it.
                  if(%ColClipsLeft > 1 && (%ColClipsLeft - %clipsToAdd >= 0)) {
                     %ClipPickUpName = %WImg.ClipPickupName[%WImg.ClipName];
                     %col.ClipCount[%WImg.ClipName] -= %clipsToAdd;
                     %obj.ClipCount[%WImg.ClipName] += %clipsToAdd;
                     %GotSomething = true;
                     messageClient(%obj.client, 'MsgItemPickup', "\c0You picked up "@%ClipPickUpName@".");
                     //check if reload needs to be done?
                     
                  }
                  else {
                     %clipsToAdd = %ColClipsLeft;
                     %ClipPickUpName = %WImg.ClipPickupName[%WImg.ClipName];
                     %col.ClipCount[%WImg.ClipName] -= %clipsToAdd;
                     %obj.ClipCount[%WImg.ClipName] += %clipsToAdd;
                     %GotSomething = true;
                     messageClient(%obj.client, 'MsgItemPickup', "\c0You picked up "@%ClipPickUpName@".");
                  }
               }
            }
         }
         // targeting laser:
         if ( %col.hasInventory( "TargetingLaser" ) )
         {
            if ( %obj.incInventory( "TargetingLaser", 1 ) > 0 )
            {
               %col.decInventory( "TargetingLaser", 1 );
               %gotSomething = true;
               messageClient( %obj.client, 'MsgItemPickup', '\c0You picked up a targeting laser.' );
            }
         }
         // ammo
         for(%j = 0; $ammoType[%j] !$= ""; %j++)
         {
            %ammoAmt = %col.inv[$ammoType[%j]];
            if(%ammoAmt)
            {
               // incInventory returns the amount of stuff successfully grabbed
               %grabAmt = %obj.incInventory($ammoType[%j], %ammoAmt);
               if(%grabAmt > 0)
               {
                  %col.decInventory($ammoType[%j], %grabAmt);
                  %gotSomething = true;
                  messageClient(%obj.client, 'MsgItemPickup', '\c0You picked up %1.', $ammoType[%j].pickUpName);
                  %obj.client.setWeaponsHudAmmo($ammoType[%j], %obj.getInventory($ammoType[%j]));
               }
            }
         }
         // figure out what type, if any, grenades the (live) player has
         %playerGrenType = "None";
         for(%x = 0; $InvGrenade[%x] !$= ""; %x++) {
            %gren = $NameToInv[$InvGrenade[%x]];
            %playerGrenAmt = %obj.inv[%gren];
            if(%playerGrenAmt > 0)
            {
               %playerGrenType = %gren;
               break;
            }
         }
         // grenades
         for(%k = 0; $InvGrenade[%k] !$= ""; %k++)
         {
            %gren = $NameToInv[$InvGrenade[%k]];
            %corpseGrenAmt = %col.inv[%gren];
            // does the corpse hold any of this grenade type?
            if(%corpseGrenAmt)
            {
               // can the player pick up this grenade type?
               if((%playerGrenType $= "None") || (%playerGrenType $= %gren))
               {
                  %taken = %obj.incInventory(%gren, %corpseGrenAmt);
                  if(%taken > 0)
                  {
                     %col.decInventory(%gren, %taken);
                     %gotSomething = true;
                     messageClient(%obj.client, 'MsgItemPickup', '\c0You picked up %1.', %gren.pickUpName);
                     %obj.client.setInventoryHudAmount(%gren, %obj.getInventory(%gren));
                  }
               }
               break;
            }
         }
         // figure out what type, if any, mines the (live) player has
         %playerMineType = "None";
         for(%y = 0; $InvMine[%y] !$= ""; %y++)
         {
            %mType = $NameToInv[$InvMine[%y]];
            %playerMineAmt = %obj.inv[%mType];
            if(%playerMineAmt > 0)
            {
               %playerMineType = %mType;
               break;
            }
         }
         // mines
         for(%l = 0; $InvMine[%l] !$= ""; %l++)
         {
            %mine = $NameToInv[$InvMine[%l]];
            %mineAmt = %col.inv[%mine];
            if(%mineAmt) {
               if((%playerMineType $= "None") || (%playerMineType $= %mine))
               {
                  %grabbed = %obj.incInventory(%mine, %mineAmt);
                  if(%grabbed > 0)
                  {
                     %col.decInventory(%mine, %grabbed);
                     %gotSomething = true;
                     messageClient(%obj.client, 'MsgItemPickup', '\c0You picked up %1.', %mine.pickUpName);
                     %obj.client.setInventoryHudAmount(%mine, %obj.getInventory(%mine));
                  }
               }
               break;
            }
         }
         // beacons
         %beacAmt = %col.inv[Beacon];
         if(%beacAmt)
         {
            %bTaken = %obj.incInventory(Beacon, %beacAmt);
            if(%bTaken > 0)
            {
               %col.decInventory(Beacon, %bTaken);
               %gotSomething = true;
               messageClient(%obj.client, 'MsgItemPickup', '\c0You picked up %1.', Beacon.pickUpName);
               %obj.client.setInventoryHudAmount(Beacon, %obj.getInventory(Beacon));
            }
         }
         // repair kit
         %rkAmt = %col.inv[RepairKit];
         if(%rkAmt)
         {
            %rkTaken = %obj.incInventory(RepairKit, %rkAmt);
            if(%rkTaken > 0)
            {
               %col.decInventory(RepairKit, %rkTaken);
               %gotSomething = true;
               messageClient(%obj.client, 'MsgItemPickup', '\c0You picked up %1.', RepairKit.pickUpName);
               %obj.client.setInventoryHudAmount(RepairKit, %obj.getInventory(RepairKit));
            }
         }
      }
	else if((%colarmortype $= "ZombieArmor" || %col.canZkill == 1) && %obj.Infected != 1 && %objiszomb != 1 && !%obj.rapierShield){
	      %Vector = vectorscale(%col.getvelocity(), 100);
	      %obj.applyimpulse(%obj.getposition(), %Vector);
	      %obj.Infected = 1;
	      %obj.InfectedLoop = schedule(10, %obj, "TWM2Lib_Zombie_Core", "InfectLoop", %obj);
	      %obj.damage(0, %obj.position, 0.2, $DamageType::Zombie);
	}
	else if(%colarmortype $= "RavagerZombieArmor" && %obj.Infected != 1 && %objiszomb != 1 && !%obj.rapierShield){
	      if(%obj.hit $= ""){
		     %obj.hit = 1;
		     %obj.setWhiteOut("0.5");
		     playPain(%obj);
	      }
	      else if(%obj.hit == 1){
	   	     %obj.Infected = 1;
	   	     %obj.InfectedLoop = schedule(10, %obj, "TWM2Lib_Zombie_Core", "InfectLoop", %obj);
          }
          %Vector = vectorscale(%col.getvelocity(), 100);
	      %obj.applyimpulse(%obj.getposition(), %Vector);
	      %obj.damage(0, %obj.position, 0.2, $DamageType::Zombie);
       }
	else if(%colarmortype $= "DemonZombieArmor" && %obj.Infected != 1 && %objiszomb != 1 && !%obj.rapierShield){
	      %Vector = vectorscale(%col.getvelocity(), 100);
	      %obj.applyimpulse(%obj.getposition(), %Vector);
	      %obj.Infected = 1;
	      %obj.InfectedLoop = schedule(10, %obj, "TWM2Lib_Zombie_Core", "InfectLoop", %obj);
	      %obj.damage(0, %obj.position, 0.4, $DamageType::Zombie);
	}
	else if(%colarmortype $= "RapierZombieArmor" && %obj.grabbed != 1 && %objiszomb != 1 && !%obj.rapierShield){
	      %chance = getRandom(1,3);
	      if(%chance == 3 && %obj.Infected != 1){
		     %obj.damage(0, %obj.position, 0.4, $DamageType::Zombie);
		     %obj.Infected = 1;
		     %obj.InfectedLoop = schedule(10, %obj, "TWM2Lib_Zombie_Core", "InfectLoop", %obj);
	      }
          else {
		     %col.iscarrying = 1;
		     %obj.grabbed = 1;
		     %obj.damage(0, %obj.position, 0.2, $DamageType::Zombie);
		     %col.killingPlayer = %col.getDatablock().zCarryLoop(%col, %obj, 0); // schedule(10, 0, "RkillLoop", %col, %obj, 0);
	      }
	}
	else if(%colarmortype $= "ShifterZombieArmor" && %obj.Infected != 1 && %objiszomb != 1 && !%obj.rapierShield){
	      %Vector = vectorscale(%col.getvelocity(), 100);
	      %obj.applyimpulse(%obj.getposition(), %Vector);
	      %obj.Infected = 1;
	      %obj.InfectedLoop = schedule(10, %obj, "TWM2Lib_Zombie_Core", "InfectLoop", %obj);
	      %obj.damage(0, %obj.position, 0.3, $DamageType::Zombie);
	}
	else if(%colarmortype $= "SummonerZombieArmor" && %obj.Infected != 1 && %objiszomb != 1 && !%obj.rapierShield){
	      %Vector = vectorscale(%col.getvelocity(), 100);
	      %obj.applyimpulse(%obj.getposition(), %Vector);
	      %obj.Infected = 1;
	      %obj.InfectedLoop = schedule(10, %obj, "TWM2Lib_Zombie_Core", "InfectLoop", %obj);
	      %obj.damage(0, %obj.position, 0.3, $DamageType::Zombie);
	}
	else if(%colarmortype $= "DemonUltraZombieArmor" && %obj.Infected != 1 && %objiszomb != 1 && !%obj.rapierShield){
	      %Vector = vectorscale(%col.getvelocity(), 100);
	      %obj.applyimpulse(%obj.getposition(), %Vector);
	      %obj.Infected = 1;
	      %obj.InfectedLoop = schedule(10, %obj, "TWM2Lib_Zombie_Core", "InfectLoop", %obj);
	      %obj.damage(0, %obj.position, 0.5, $DamageType::Zombie);
	}
    else if((%colarmortype $= "LordRogZombieArmor" && %objiszomb != 1 && !%obj.rapierShield)) {
       if(%col.isAllyBot && !%col.isAttacking) {
          return;
       }
          %obj.scriptkill($DamageType::Admin);
          %col.setDamageLevel(%col.getDamageLevel() - 10.0);
          %col.setVelocity("0 0 0");
          if(!%obj.iszombie) {
             ServerPlay3d(BOVHitSound, %obj.getPosition());
             %randMessage = getrandom(3)+1;
             switch(%randMessage) {
                case 1:
                   MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[8]@": Slice And Dice "@%obj.client.namebase@"!");
                case 2:
                   MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[8]@": Die... "@%obj.client.namebase@" Painfully.");
                case 3:
                   MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[8]@": Your life "@%obj.client.namebase@", is now mine!");
                default:
                   MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[8]@": Slice And Dice "@%obj.client.namebase@"!");
             }
          }
    }
    else if((%colarmortype $= "ROGZombieArmor" && %objiszomb != 1 && !%obj.rapierShield && !%col.isPlayerRog)) {
       if(%col.isAllyBot && !%col.isAttacking) {
          return;
       }
          %obj.scriptkill($DamageType::Admin);
          %col.setDamageLevel(%col.getDamageLevel() - 10.0);
          %col.setVelocity("0 0 0");
          if(!%obj.iszombie) {
             ServerPlay3d(BOVHitSound, %obj.getPosition());
             %randMessage = getrandom(3)+1;
             switch(%randMessage) {
                case 1:
                   MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[16]@": Slice And Dice "@%obj.client.namebase@"!");
                case 2:
                   MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[16]@": Die... "@%obj.client.namebase@" Painfully.");
                case 3:
                   MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[16]@": Your life "@%obj.client.namebase@", is now mine!");
                default:
                   MessageAll('MessageAll', "\c4"@$TWM2::ZombieName[16]@": Slice And Dice "@%obj.client.namebase@"!");
             }
          }
    }
	else if((%colarmortype $= "ZombieArmor" || %col.canZkill == 1) && %objiszomb != 1 && !%obj.rapierShield) {
	      %obj.damage(0, %obj.position, 0.2, $DamageType::Zombie);
    }
	else if(%colarmortype $= "FZombieArmor" && %objiszomb != 1 && !%obj.rapierShield) {
	      %obj.damage(0, %obj.position, 0.4, $DamageType::Zombie);
    }
	else if((%colarmortype $= "DemonZombieArmor" || %colarmortype $= "WraithZombieArmor")  && %objiszomb != 1 && !%obj.rapierShield) {
	      %obj.damage(0, %obj.position, 0.4, $DamageType::Zombie);
    }
	else if(%colarmortype $= "DemonMotherZombieArmor" && %objiszomb != 1 && !%obj.rapierShield) {
	      if(%obj.Infected != 1) {
		     %obj.Infected = 1;
		     %obj.damage(0, %obj.position, 0.8, $DamageType::Zombie);
		     %obj.InfectedLoop = schedule(10, %obj, "TWM2Lib_Zombie_Core", "InfectLoop", %obj);
	      }
          else {
		     %obj.damage(0, %obj.position, 1.2, $DamageType::Zombie);
          }
	}
	else if ($PlayerSnapTo == true) {
		if (!isObject(%col.getMountedObject(0))) {
			if (!isObject(%obj.getMountedObject(0)) || %obj.getMountedObject(0) != %col) {
				%col.mountObject(%obj,$BackPackSlot);
				%gotSomething = true;
			}
		}
	}
	else if ($NaughtyMode == true) {
		playPain(%obj);
		playPain(%col);
	}
	else if ($AngstMode == true) {
		playDeathCry(%obj);
		playDeathCry(%col);
	}
	if ($JumpyMode == true) {
		%obj.applyImpulse(%obj.getPosition(),"0 0 500");
		%col.applyImpulse(%col.getPosition(),"0 0 500");
	}
	if (%obj.client.race $= "Human" && $SexChangeMode == true) {
		if (%obj.client.oldSex $= "")
			%obj.client.oldSex = %obj.client.sex;
		if (%obj.client.oldVoice $= "")
			%obj.client.oldVoice = %obj.client.voice;
		%voice = getTaggedString(getTargetVoice(%obj.client.target));
		%voiceNum = getSubStr(%voice,strLen(%voice)-1,1);
		if (%obj.client.sex $= "Male") {
			%obj.client.sex = "Female";
			%obj.client.voiceTag =  addTaggedString("Fem" @ %voiceNum);
			setTargetVoice(%obj.client.target,%obj.client.voiceTag);
		}
		else {
			%obj.client.sex = "Male";
			%obj.client.voiceTag =  addTaggedString("Male" @ %voiceNum);
			setTargetVoice(%obj.client.target,%obj.client.voiceTag);
		}
		%obj.setArmor(%client.armor);
	}
	if(%gotSomething)
		%col.playAudio(0, CorpseLootingSound);
   }
}

function Player::resetSitMessage(%obj)
{
   %obj.noSitMessage = false;
}

function Player::setInvincible(%this, %val)
{
   %this.invincible = %val;
}

function Player::causedRecentDamage(%this, %val)
{
   %this.causedRecentDamage = %val;
}

function hasLargePack(%player)
{
   %pack = %player.getMountedImage($BackpackSlot);
   if(%pack.isLarge)
      return true;
   else
      return false;
}

function nodeIsSitting(%vehDBlock, %node)
{
   // pilot == always a "sitting" node
   if(%node == 0)
      return true;
   else {
      switch$ (%vehDBlock.getName())
      {
         // note: for assault tank -- both nodes are sitting
         // for any single-user vehicle -- pilot node is sitting
         case "BomberFlyer":
            // bombardier == sitting; tailgunner == not sitting
            if(%node == 1)
               return true;
            else
               return false;
         case "HAPCFlyer":
            // only the pilot node is sitting
            return false;
         case "SuperHAPCFlyer":
            // only the pilot node is sitting
            return false;
         default:
            return true;
      }
   }
}

//----------------------------------------------------------------------------
function Player::setMountVehicle(%this, %val)
{
   %this.mountVehicle = %val;
}

function Armor::doDismount(%this, %obj, %forced,%eject) {
   // This function is called by player.cc when the jump trigger
   // is true while mounted
   if (!%obj.isMounted())
      return;
   //[most] emp hook
   if (%obj.isemped)
      return;
   //[most]

	%vehicle = %obj.getObjectMount();
	%vehicleData = %vehicle.getDataBlock();

	if (%vehicleData == EscapePodVehicle.getID()) {
		if (%vehicle.playerMountedTime + 4000 > getSimTime())
			return;
	}

   if(isObject(%obj.getObjectMount().shield))
      %obj.getObjectMount().shield.delete();

   commandToClient(%obj.client,'SetDefaultVehicleKeys', false);

 if (%Eject)
      %push = 30;
   else
      %push = 1;


   // Position above dismount point

   %pos    = getWords(%obj.getTransform(), 0, 2);
   %oldPos = %pos;
   %vec[0] = " 0  0  1";
   %vec[1] = " 0  0  1";
   %vec[2] = " 0  0 -1";
   %vec[3] = " 1  0  0";
   %vec[4] = "-1  0  0";
   %numAttempts = 5;
   %success     = -1;
   %impulseVec  = "0 0 0";
   if (%obj.getObjectMount().getDatablock().hasDismountOverrides() == true)
   {
      %vec[0] = %obj.getObjectMount().getDatablock().getDismountOverride(%obj.getObjectMount(), %obj);
      %vec[0] = MatrixMulVector(%obj.getObjectMount().getTransform(), %vec[0]);
   }
   else
   {
      %vec[0] = MatrixMulVector( %obj.getTransform(), %vec[0]);
   }

   %pos = "0 0 0";
   for (%i = 0; %i < %numAttempts; %i++)
   {
      %pos = VectorAdd(%oldPos, VectorScale(%vec[%i], 3));
      if (%obj.checkDismountPoint(%oldPos, %pos))
      {
         %success = %i;
         %impulseVec = %vec[%i];
         break;
      }
   }
   if (%forced && %success == -1)
   {
      %pos = %oldPos;
   }

   // hide the dashboard HUD and delete elements based on node
   commandToClient(%obj.client, 'setHudMode', 'Standard', "", 0);
   // Unmount and control body
   if(%obj.vehicleTurret)
      %obj.vehicleTurret.getDataBlock().playerDismount(%obj.vehicleTurret);
   %obj.unmount();
   if(%obj.mVehicle)
      %obj.mVehicle.getDataBlock().playerDismounted(%obj.mVehicle, %obj);

   // bots don't change their control objects when in vehicles
   if(!%obj.isZombie)
   if(!%obj.client.isAIControlled())
   {
      %vehicle = %obj.getControlObject();
      %obj.setControlObject(0);
   }

   %obj.mountVehicle = false;
   %obj.schedule(4000, "setMountVehicle", true);

   // Position above dismount point
   %obj.setTransform(%pos);
   %obj.playAudio(0, UnmountVehicleSound);
   %obj.applyImpulse(%pos, VectorScale(%impulseVec, %obj.getDataBlock().mass * 3 * %push));
   //%obj.applyImpulse(%pos, VectorScale(%impulseVec, %obj.getDataBlock().mass * 3));
   %obj.setPilot(false);
   %obj.vehicleTurret = "";
}

function resetObserveFollow( %client, %dismount )
{
   if( %dismount )
   {
      if( !isObject( %client.player ) )
         return;

      for( %i = 0; %i < %client.observeCount; %i++ )
      {
      if (isObject(%client.observers[%i].camera))
         %client.observers[%i].camera.setOrbitMode( %client.player, %client.player.getTransform(), 0.5, 4.5, 4.5);
      }
   }
   else
   {
      if( !%client.player.isMounted() )
         return;

      // grab the vehicle...
      %mount = %client.player.getObjectMount();
      if( %mount.getDataBlock().observeParameters $= "" )
         %params = %client.player.getTransform();
      else
         %params = %mount.getDataBlock().observeParameters;

      for( %i = 0; %i < %client.observeCount; %i++ )
      {
      if (isObject(%client.observers[%i].camera))
         %client.observers[%i].camera.setOrbitMode(%mount, %mount.getTransform(), getWord( %params, 0 ), getWord( %params, 1 ), getWord( %params, 2 ));
      }
   }
}


//----------------------------------------------------------------------------

function Player::scriptKill(%player, %damageType) {
   if(%player.rapierShield) {
      %player.rapierShield = 0;
   }
   %player.scriptKilled = 1;
   %player.setInvincible(false);
   %player.damage(0, %player.getPosition(), 10000, %damageType);
}

//For horde/helljump, prevents Killstealing
function Player::damageInvulnerabilityCountdown(%object, %source, %on) {
   if(!$TWM::PlayingHorde && !$TWM::PlayingHellJump) {
      return;
   }
   //
   if(isObject(%object) && isObject(%source)) {
      if(isPlayer(%object) && isPlayer(%source)) {
         if(!%object.isAlive() || !%source.isAlive()) {
            return;
         }
      }
   }
   //
   if(%on) {
      %object.onlyDamageFrom = %source;
      cancel(%object.prevDamageSched);
   }
   else {
      %object.onlyDamageFrom = 0;
      return;
   }
   %object.prevDamageSched = %object.schedule(3000, "damageInvulnerabilityCountdown", %source, 0);
}

function Armor::damageObject(%data, %targetObject, %sourceObject, %position, %amount, %damageType, %momVec, %mineSC) {
//	error("Armor::damageObject( "@%data@", "@%targetObject@", "@%sourceObject@", "@%position@", "@%amount@", "@%damageType@", "@%momVec@" )");
	if (%targetObject.invincible || %targetObject.getState() $= "Dead" || ((%targetObject.client.isJailed || %targetObject.client.permInvincible) && !%targetObject.scriptKilled))
	return;
 
    if(%targetObject.IsinvincibleC)
       return;
       
       
    if(%targetObject.onlyDamageFrom != %sourceObject && %targetObject.onlyDamageFrom != 0) {
       if($KSPROTECTIONENABLED) {
          if($TWM::PlayingHorde || $TWM::PlayingHellJump) {
             %targetObject.playShieldEffect("1 1 1");
             messageClient(%sourceObject.client, 'msgNoDmg', "\c3TWM2: Target is currently Killsteal protected");
             return;
          }
       }
    }
    //
    if($TWM::PlayingHorde || $TWM::PlayingHellJump) {
       %targetObject.damageInvulnerabilityCountdown(%sourceObject, 1);
    }

    if(%targetObject.getShieldPercent() > 0 && %damageType != $DamageType::Suicide) {
       //throw the damage points at the user's shields
       %amount = %targetObject.damageShields(%amount);
    }

   //----------------------------------------------------------------
   // z0dd - ZOD, 6/09/02. Check to see if this vehcile is destroyed,
   // if it is do no damage. Fixes vehicle ghosting bug. We do not
   // check for isObject here, destroyed objects fail it even though
   // they exist as objects, go figure.
   if(%targetObject.rapiershield && (%damageType != $DamageType::Suicide)) {
      if(%targetObject.Infected) {
		 TWM2Lib_Zombie_Core("cureInfection", %targetObject);
      }
      return;
   }
   if(%damageType == $DamageType::Impact) {
      if(%sourceObject.getDamageState() $= "Destroyed") {
         return;
      }
	  //Check for the mother of all humiliating deaths :)
	  if(%targetObject.getState() $= "dead") {
		  if(%sourceObject.getClassName() $= "FlyingVehicle" && %sourceObject.lastPilot.getState() $= "dead") {
			 if((%sourceObject.lastPilot.client.lastKilledBy == %targetObject.client) && (%sourceObject.lastPilot.client.lastKilledByPlayer == %targetObject)) {
		        //You just got rekt....
				CompleteNWChallenge(%sourceObject.lastPilot.client, "Uncomprehendable");
			 }
		  }
	  }
   }
   %armortype = %targetobject.getdatablock().getname();
   if (%damageType == $DamageType::ZAcid && %armortype !$= "ZombieArmor" && %armortype !$= "FZombieArmor" && %armortype !$= "LordZombieArmor" && %armortype !$= "DemonZombieArmor" && %armortype !$= "DemonMotherZombieArmor" && %armortype !$= "RapierZombieArmor" && %targetobject.infected != 1 && (%sourceObject.isZombie == 1 || %sourceObject.isBoss == 1)){
	   %targetObject.Infected = 1;
	   %targetObject.InfectedLoop = schedule(10, %targetObject, "TWM2Lib_Zombie_Core", "InfectLoop", %targetObject);
   }
   if (%damageType == $DamageType::RapierShield) {
      CreateBlood(%targetObject);
   }
   if (%damageType == $DamageType::Fire || %damageType == $DamageType::Plasma){
       if(%targetObject.team == %sourceObject.team && !$TeamDamage) {
          if(%targetObject != %sourceObject) {
             %noBurn = 1;
          }
          else {
             %noBurn = 0;
          }
       }
       else {
          %noBurn = 0;
       }
       if(!%noBurn) {
	      if(%targetObject.maxfirecount $= "")
		     %targetObject.maxfirecount = 0;
          %targetObject.maxfirecount += (75 * (%amount / 0.2));
	      if(%targetObject.onfire == 0 || %targetObject.onfire $= ""){
	         %targetObject.onfire = 1;
	         schedule(10, %targetObject, "burnloop", %targetObject);
	      }
       }
   }
   
   if(%targetObject.client !$= "") {
      if(!%targetObject.client.isHarb) {
         if(%targetObject.client.IsActivePerk("Storm Barrier")) {
            if(%damageType == $DamageType::Shocklance) {
               %targetObject.playShieldEffect("1 1 1");
               %amount *= 0.2;
            }
            else if(%damageType == $DamageType::Lightning) {
               %targetObject.playShieldEffect("1 1 1");
               return;
            }
         }
      }
   }

   if (%targetObject.isMounted() && %targetObject.scriptKilled $= "") {
      %mount = %targetObject.getObjectMount();
      if(%mount.team == %targetObject.team)
      {
         %found = -1;
         for (%i = 0; %i < %mount.getDataBlock().numMountPoints; %i++)
         {
            if (%mount.getMountNodeObject(%i) == %targetObject)
            {
               %found = %i;
               break;
            }
         }

         if (%found != -1)
         {
            if (%mount.getDataBlock().isProtectedMountPoint[%found])
            {
               %mount.getDataBlock().damageObject(%mount, %sourceObject, %position, %amount, %damageType);
               return;
            }
         }
      }
   }

   %targetClient = %targetObject.getOwnerClient();
   if(isObject(%mineSC))
      %sourceClient = %mineSC;
   else
      %sourceClient = isObject(%sourceObject) ? %sourceObject.getOwnerClient() : 0;

   %targetTeam = %targetClient.team;

   //if the source object is a player object, player's don't have sensor groups
   // if it's a turret, get the sensor group of the target
   // if its a vehicle (of any type) use the sensor group
   if (%sourceClient)
      %sourceTeam = %sourceClient.getSensorGroup();
   else if(%damageType == $DamageType::Suicide)
      %sourceTeam = 0;

   //--------------------------------------------------------------------------------------------------------------------
   // z0dd - ZOD, 5/8/02. Check to see if this turret has a valid owner, if not clear the owner varible.
   //else if(isObject(%sourceObject) && %sourceObject.getClassName() $= "Turret")
   //   %sourceTeam = getTargetSensorGroup(%sourceObject.getTarget());
   else if(isObject(%sourceObject) && %sourceObject.getClassName() $= "Turret")
   {
      %sourceTeam = getTargetSensorGroup(%sourceObject.getTarget());
      if(%sourceObject.getOwner() !$="" && (%sourceObject.getOwner().team != %sourceObject.team || !isObject(%sourceObject.getOwner())))
         %sourceObject.setOwner();
   }
   // End z0dd - ZOD
   //--------------------------------------------------------------------------------------------------------------------
   else if( isObject(%sourceObject) &&
   	( %sourceObject.getClassName() $= "FlyingVehicle" || %sourceObject.getClassName() $= "WheeledVehicle" || %sourceObject.getClassName() $= "HoverVehicle"))
      %sourceTeam = getTargetSensorGroup(%sourceObject.getTarget());
   else
   {
      if (isObject(%sourceObject) && %sourceObject.getTarget() >= 0 )
      {
         %sourceTeam = getTargetSensorGroup(%sourceObject.getTarget());
      }
      else
      {
         %sourceTeam = -1;
      }
   }

   // if teamdamage is off, and both parties are on the same team
   // (but are not the same person), apply no damage
   if($TWM::PlayingInfection) {
      if((%targetClient != %sourceClient) && (%targetTeam == %sourceTeam)) {
//         if(!%targetClient.isZombie) {
//            return;
//         }
      }
   }
   else {
      if(!$teamDamage && (%targetClient != %sourceClient) && (%targetTeam == %sourceTeam))
         return;
   }

   if(%targetObject.isShielded && %damageType != $DamageType::Blaster)
      %amount = %data.checkShields(%targetObject, %position, %amount, %damageType);

   if(%amount == 0)
      return;

   // Set the damage flash
   %damageScale = %data.damageScale[%damageType];
   if(%damageScale !$= "")
      %amount *= %damageScale;

   %flash = %targetObject.getDamageFlash() + (%amount * 2);
   if (%flash > 0.75)
      %flash = 0.75;

   %previousDamage = %targetObject.getDamagePercent();
   %targetObject.setDamageFlash(%flash);

	if ($Host::InvincibleArmors == 1 && !%targetObject.scriptKilled) {
		if ((%sourceObject.team != %targetObject.team) || Game.numTeams == 1) {
			%wp = new WayPoint() {
				position = %targetObject.getWorldBoxCenter();
				dataBlock = "WayPointMarker";
				team = (%sourceObject.team == %targetObject.team) ? 0 : %sourceObject.team;
				name = mFloatLength((100 / %data.maxDamage) * %amount,0) @ "%";
			};
			MissionCleanup.add(%wp);
			%wp.schedule(1500,delete);
		}
	}
	else
		%targetObject.applyDamage(%amount);

   Game.onClientDamaged(%targetClient, %sourceClient, %damageType, %sourceObject);

   %targetClient.lastDamagedBy = %damagingClient;
   %targetClient.lastDamaged = getSimTime();

   //now call the "onKilled" function if the client was... you know...
   if(%targetObject.getState() $= "Dead") {
      // where did this guy get it?
      %damLoc = %targetObject.getDamageLocation(%position);	   
	  postObjectDestroyed(%sourceObject, %targetObject, %damageType, %damLoc);

      // should this guy be blown apart?
      if( %damageType == $DamageType::Explosion ||
          %damageType == $DamageType::TankMortar ||
          %damageType == $DamageType::Mortar ||
          %damageType == $DamageType::MortarTurret ||
          %damageType == $DamageType::BomberBombs ||
          %damageType == $DamageType::SatchelCharge ||
          %damageType == $DamageType::Missile )
      {
         if( %previousDamage >= 0.35 ) // only if <= 35 percent damage remaining
         {
            %targetObject.setMomentumVector(%momVec);
            %targetObject.blowup();
            %targetObject.kibbled = 1;
         }
      }

      // this should be funny...
      if( %damageType == $DamageType::VehicleSpawn )
      {
         %targetObject.setMomentumVector("0 0 1");
         %targetObject.blowup();
         %targetObject.kibbled = 1;
      }

      // If we were killed, max out the flash
      %targetObject.setDamageFlash(0.75);
      Game.onClientKilled(%targetClient, %sourceClient, %damageType, %sourceObject, %damLoc);
   }
   else if ( %amount > 0.1 )
   {
      if( %targetObject.station $= "" && %targetObject.isCloaked() )
      {
         %targetObject.setCloaked( false );
         %targetObject.reCloak = %targetObject.schedule( 500, "setCloaked", true );
      }

      playPain( %targetObject );
   }
}

function Armor::onImpact(%data, %playerObject, %collidedObject, %vec, %vecLen) {
	%data.damageObject(%playerObject, 0, VectorAdd(%playerObject.getPosition(),%vec), %vecLen * %data.speedDamageScale , $DamageType::Ground);
//	if (%collidedObject & $TypeMasks::PlayerObjectType) {
//		if (%collidedObject.getState() !$= "Dead") {
//			%data.damageObject(%collidedObject, 0, VectorAdd(%playerObject.getPosition(),%vec),
//			%vecLen * %data.speedDamageScale , $DamageType::Ground);
//		}
//	}
}

function Armor::applyConcussion( %this, %dist, %radius, %sourceObject, %targetObject )
{
   %percentage = 1 - ( %dist / %radius );
   %random = getRandom();

   if( %sourceObject == %targetObject )
   {
      %flagChance = 1.0;
      %itemChance = 1.0;
   }
   else
   {
      %flagChance = 0.7;
      %itemChance = 0.7;
   }

   %probabilityFlag = %flagChance * %percentage;
   %probabilityItem = %itemChance * %percentage;

   if( %random <= %probabilityFlag )
   {
      Game.applyConcussion( %targetObject );
   }

   if( %random <= %probabilityItem )
   {
      %player = %targetObject;
      %numWeapons = 0;

      // blaster 0
      // plasma 1
      // chain 2
      // disc 3
      // grenade 4
      // snipe 5
      // elf 6
      // mortar 7
      // nerfgun 8
      // NerfBallLauncher 9
      // superchaingun 10

      //get our inventory
      if( %weaps[0] = %player.getInventory("Blaster") > 0 ) %numWeapons++;
      if( %weaps[1] = %player.getInventory("Plasma") > 0 ) %numWeapons++;
      if( %weaps[2] = %player.getInventory("Chaingun") > 0 ) %numWeapons++;
      if( %weaps[3] = %player.getInventory("Disc") > 0 ) %numWeapons++;
      if( %weaps[4] = %player.getInventory("GrenadeLauncher") > 0 ) %numWeapons++;
      if( %weaps[5] = %player.getInventory("SniperRifle") > 0 ) %numWeapons++;
      if( %weaps[6] = %player.getInventory("ELFGun") > 0 ) %numWeapons++;
      if( %weaps[7] = %player.getInventory("Mortar") > 0 ) %numWeapons++;
      if( %weaps[8] = %player.getInventory("NerfGun") > 0 ) %numWeapons++;
      if( %weaps[9] = %player.getInventory("NerfBallLauncher") > 0 ) %numWeapons++;
      if( %weaps[10] = %player.getInventory("SuperChaingun") > 0 ) %numWeapons++;

      %foundWeapon = false;
      %attempts = 0;

      if( %numWeapons > 0 )
      {
         while( !%foundWeapon )
         {
            %rand = mFloor( getRandom() * 11 );
            if( %weaps[ %rand ] )
            {
               %foundWeapon = true;

               switch ( %rand )
               {
                  case 0:
                     %player.use("Blaster");
                  case 1:
                     %player.use("Plasma");
                  case 2:
                     %player.use("Chaingun");
                  case 3:
                     %player.use("Disc");
                  case 4:
                     %player.use("GrenadeLauncher");
                  case 5:
                     %player.use("SniperRifle");
                  case 6:
                     %player.use("ElfGun");
                  case 7:
                     %player.use("Mortar");
                  case 8:
                     %player.use("NerfGun");
                  case 9:
                     %player.use("NerfBallLauncher");
                  case 10:
                     %player.use("SuperChaingun");
               }

               %image = %player.getMountedImage( $WeaponSlot );
               %player.throw( %image.item );
               %player.client.setWeaponsHudItem( %image.item, 0, 0 );
               %player.throwPack();
            }
            else
            {
               %attempts++;
               if( %attempts > 10 )
                  %foundWeapon = true;
            }
         }
      }
      else
      {
         %targetObject.throwPack();
         %targetObject.throwWeapon();
      }
   }
}

//----------------------------------------------------------------------------

$DeathCry[1] = 'avo.deathCry_01';
$DeathCry[2] = 'avo.deathCry_02';
$PainCry[1] = 'avo.grunt';
$PainCry[2] = 'avo.pain';

function playDeathCry( %obj )
{
   %client = %obj.client;
   %random = getRandom(1) + 1;
   %desc = AudioClosest3d;

   playTargetAudio( %client.target, $DeathCry[%random], %desc, false );
}

function playPain( %obj ) {
   if(%obj.isZombie || %obj.isBoss || %obj.isGoF || %obj.isAllyBot) {
      return;
   }
   %client = %obj.client;
   %random = getRandom(1) + 1;
   %desc = AudioClosest3d;

   playTargetAudio( %client.target, $PainCry[%random], %desc, false);
}

//----------------------------------------------------------------------------

//Just in case >:)
// -Phantom139
datablock ShapeBaseImageData(PhantomFlag) {
   shapeFile = "flag.dts";
   emap = false;
   mountPoint = 1;
   offset = "0 -0.1 -0.2";
   rotation = "-1 0 0 10";
};
//

//$DefaultPlayerArmor = LightMaleHumanArmor;
$DefaultPlayerArmor = Light;

function Player::setArmor(%this,%size) {
   if(%this.hasflag) {
     %this.unmountImage(1);
   }
   %this.hasflag = 0;
   // Takes size as "Light","Medium", "Heavy"
   %client = %this.client;
   if (%client.race $= "Bioderm")
      // Only have male bioderms.
      %armor = %size @ "Male" @ %client.race @ Armor;
   else
      %armor = %size @ %client.sex @ %client.race @ Armor;
   //echo("Player::armor: " @ %armor);
   %this.setDataBlock(%armor);
   %client.armor = %size;
   %this.Infected = 0;
   
   //
   if(%client.TWM2Core.officer >= 5 && %client.useFlag) {
      if(%client.flagType $= "Blue" && %client.hasStoreItem("Blue Flag")) {
         %this.mountImage(PhantomFlag, 1, true, 'swolf');
         %this.hasflag = 1;
      }
      else if(%client.flagType $= "Red" && %client.hasStoreItem("Red Flag")) {
         %this.mountImage(PhantomFlag, 1, true, 'beagle');
         %this.hasflag = 1;
      }
      else if(%client.flagType $= "Gold" && %client.hasStoreItem("Gold Flag")) {
         %this.mountImage(PhantomFlag, 1, true, 'dsword');
         %this.hasflag = 1;
      }
      else {
         if(%client.hasStoreItem("Silver Flag")) {
            %this.mountImage(PhantomFlag, 1);
            %this.hasflag = 1;
         }
      }
   }
   //
   
   //Infection Game
   if($TWM::PlayingInfection) {
      %trans = vectorAdd(%this.getPosition(), "0 0 5");
      if($InfectionGame::Infected[%client]) {
         if ($InfectionGame::ClientZombie[%client] $= "Norm") {
            schedule(5,0,"makePersonZombie", %trans, %client, 1);
         }
         else if ($InfectionGame::ClientZombie[%client] $= "Rav") {
            schedule(5,0,"makePersonZombie", %trans, %client, 2);
         }
         else if ($InfectionGame::ClientZombie[%client] $= "Lord") {
            schedule(5,0,"makePersonZombie", %trans, %client, 3);
         }
         else if ($InfectionGame::ClientZombie[%client] $= "Demon") {
            schedule(5,0,"makePersonZombie", %trans, %client, 4);
         }
         else if ($InfectionGame::ClientZombie[%client] $= "Rap") {
            schedule(5,0,"makePersonZombie", %trans, %client, 5);
         }
         else if ($InfectionGame::ClientZombie[%client] $= "VolRav") {
            schedule(5,0,"makePersonZombie", %trans, %client, 13);
         }
         else if ($InfectionGame::ClientZombie[%client] $= "UDem") {
            schedule(5,0,"makePersonZombie", %trans, %client, 12);
         }
         else if ($InfectionGame::ClientZombie[%client] $= "Wraith") {
            schedule(5,0,"makePersonZombie", %trans, %client, 15);
         }
         else if ($InfectionGame::ClientZombie[%client] $= "Rog") {
            schedule(5,0,"makePersonZombie", %trans, %client, 16);
         }
         else {
            if($InfectionGame::IsAlpha[%client]) {
               $InfectionGame::ClientZombie[%client] = "Demon";
               schedule(5,0,"makePersonZombie", %trans, %client, 4);
            }
            else {
               $InfectionGame::ClientZombie[%client] = "Norm";
               schedule(5,0,"makePersonZombie", %trans, %client, 1);
            }
         }
      }
      else {
         if(%size !$= "Light") {
            MessageClient(%this.client, '', "\c1INFECTON: Your armor is incorrect, Set to scout.");
            if (%client.race $= "Bioderm")
            // Only have male bioderms.
                %force = "Light" @ "Male" @ %client.race @ Armor;
            else
                %force = "Light" @ %client.sex @ %client.race @ Armor;
            %this.setDataBlock(%force);
            %client.armor = "Light";
            %client.favorites[0] = "Scout";
         }
      }
   }
   //DoTWM2ArmorSetChecks(%client, %size);
}

function getDamagePercent(%maxDmg, %dmgLvl)
{
   return (%dmgLvl / %maxDmg);
}

function Player::getArmorSize(%this)
{
   // return size as "Light","Medium", "Heavy"
   %dataBlock = %this.getDataBlock().getName();
   if (getSubStr(%dataBlock, 0, 5) $= "Light")
      return "Light";
   else if (getSubStr(%dataBlock, 0, 6) $= "Medium")
      return "Medium";
   else if (getSubStr(%dataBlock, 0, 5) $= "Heavy")
      return "Heavy";
   else if (getSubStr(%dataBlock, 0, 4) $= "Tech")
      return "Tech";
   else if (getSubStr(%dataBlock, 0, 10) $= "Microburst")
      return "Microburst";
   else if (getSubStr(%dataBlock, 0, 5) $= "Flame")
      return "Flame";
   else if (getSubStr(%dataBlock, 0, 14) $= "ShadowCommando")
      return "ShadowCommando";
   else if (getSubStr(%dataBlock, 0, 4) $= "Pure")
      return "Pure";
   else if (getSubStr(%dataBlock, 0, 8) $= "TR2Light")
      return "TR2Light";
   else if (getSubStr(%dataBlock, 0, 9) $= "TR2Medium")
      return "TR2Medium";
   else if (getSubStr(%dataBlock, 0, 8) $= "TR2Heavy")
      return "TR2Heavy";
   else
      return "Unknown";
}

function Player::pickup(%this,%obj,%amount)
{
	if (%this.client.isJailed)
		return 0;
   %data = %obj.getDataBlock();
   // Don't pick up a pack if we already have one mounted
   if (%data.className $= Pack &&
         %this.getMountedImage($BackpackSlot) != 0)
      return 0;
	// don't pick up a weapon (other than targeting laser) if player's at maxWeapons
   else if(%data.className $= Weapon
     && %data.getName() !$= "TargetingLaser"  // Special case
     && %this.weaponCount >= %this.getDatablock().maxWeapons
     && !%data.alwaysAllowPickup)             // Is it a special weapon that we can always grab?
      return 0;
	// don't allow players to throw large packs at pilots (thanks Wizard)
   else if(%data.className $= Pack && %data.image.isLarge && %this.isPilot())
		return 0;
   return ShapeBase::pickup(%this,%obj,%amount);
}

function Player::use( %this,%data )
{
   // If player is in a station then he can't use any items
   if(%this.station !$= "")
      return false;

   // Convert the word "Backpack" to whatever is in the backpack slot.
   if ( %data $= "Backpack" )
   {
      if ( %this.inStation )
         return false;

      if ( %this.isPilot() )
      {
	%vehicle = %this.getObjectMount();
	if (%vehicle.getDataBlock().getName() $= "MobileBaseVehicle" && $Host::Purebuild == 1) {
		%vehicle.applyImpulse(%vehicle.getPosition(),"0 0 8000");
		return( false );
	}
        //[most]
        else if (%vehicle.base.leftpad == %vehicle)
             {
             PressButton(%vehicle,%this,0,0);
             }
        else if (%vehicle.base.rightpad == %vehicle)
             {
             PressButton(%vehicle,%this,1,0);
             }
        //[most]
	else {
		messageClient( %this.client, 'MsgCantUsePack', '\c2You can\'t use your pack while piloting.~wfx/misc/misc.error.wav' );
		return( false );
	}
      }
      else if ( %this.isWeaponOperator() )
      {
         messageClient( %this.client, 'MsgCantUsePack', '\c2You can\'t use your pack while in a weaponry position.~wfx/misc/misc.error.wav' );
         return( false );
      }

      %image = %this.getMountedImage( $BackpackSlot );
      if ( %image )
         %data = %image.item;
   }

   // Can't use some items when piloting or your a weapon operator
   if ( %this.isPilot() || %this.isWeaponOperator() )
      if (isObject(%data) && %data.getName() !$= "RepairKit" )
         return false;
  // Convert the word "Backpack" to whatever is in the backpack slot.
  //[most]
   if (isObject(%data) && %data.getName() $= "RepairKit" )
   {     
      if ( %this.isPilot() )
      {       
	%vehicle = %this.getObjectMount();
        if (%vehicle.base.leftpad == %vehicle)
             {
             PressButton(%vehicle,%this,0,1);
             }
        else if (%vehicle.base.rightpad == %vehicle)
             {
             PressButton(%vehicle,%this,1,1);
             }
       
      }
   }
   //[most]

   return ShapeBase::use( %this, %data );
}

function Player::maxInventory(%this,%data)
{
   %max = ShapeBase::maxInventory(%this,%data);
   if (%this.getInventory(AmmoPack))
      %max += AmmoPack.max[%data.getName()];
   return %max;
}

function Player::isPilot(%this)
{
   %vehicle = %this.getObjectMount();
   // There are two "if" statements to avoid a script warning.
   if (%vehicle)
      if (%vehicle.getMountNodeObject(0) == %this)
         return true;
   return false;
}

function Player::isWeaponOperator(%this)
{
   %vehicle = %this.getObjectMount();
   if ( %vehicle )
   {
      %weaponNode = %vehicle.getDatablock().weaponNode;
      if ( %weaponNode > 0 && %vehicle.getMountNodeObject( %weaponNode ) == %this )
         return( true );
   }

   return( false );
}

function Player::liquidDamage(%obj, %data, %damageAmount, %damageType)
{
   if(%obj.getState() !$= "Dead")
   {
      %data.damageObject(%obj, 0, "0 0 0", %damageAmount, %damageType);
      %obj.lDamageSchedule = %obj.schedule(50, "liquidDamage", %data, %damageAmount, %damageType);
   }
   else
      %obj.lDamageSchedule = "";
}

function Armor::onEnterLiquid(%data, %obj, %coverage, %type)
{
   switch(%type)
   {
      case 0:
         //Water
   	     if (!( %obj.isPilot() || %obj.isWeaponOperator() )){
            //no drowning in purebuild
            if($host::Purebuild) {

            }
            else {
               %obj.DrownLoop = schedule(15000, 0, "DrowningLoop", %obj);
   	   	       %obj.CheckDLoop = schedule(500, 0, "checkforwater", %obj);
		       %obj.Drowning = 1;
	        }
         }
      case 1:
         //Ocean Water
   	     if (!( %obj.isPilot() || %obj.isWeaponOperator() )){
            //no drowning in purebuild
            if($host::Purebuild) {

            }
            else {
               %obj.DrownLoop = schedule(15000, 0, "DrowningLoop", %obj);
   	   	       %obj.CheckDLoop = schedule(500, 0, "checkforwater", %obj);
		       %obj.Drowning = 1;
	        }
         }
      case 2:
         //River Water
   	     if (!( %obj.isPilot() || %obj.isWeaponOperator() )){
            //no drowning in purebuild
            if($host::Purebuild) {

            }
            else {
               %obj.DrownLoop = schedule(15000, 0, "DrowningLoop", %obj);
   	   	       %obj.CheckDLoop = schedule(500, 0, "checkforwater", %obj);
		       %obj.Drowning = 1;
	        }
         }
      case 3:
         //Stagnant Water
   	     if (!( %obj.isPilot() || %obj.isWeaponOperator() )){
            //no drowning in purebuild
            if($host::Purebuild) {

            }
            else {
               %obj.DrownLoop = schedule(15000, 0, "DrowningLoop", %obj);
   	   	       %obj.CheckDLoop = schedule(500, 0, "checkforwater", %obj);
		       %obj.Drowning = 1;
	        }
         }
      case 4:
         //Lava
         %obj.liquidDamage(%data, $DamageLava, $DamageType::Lava);
      case 5:
         //Hot Lava
         %obj.liquidDamage(%data, $DamageHotLava, $DamageType::Lava);
      case 6:
         //Crusty Lava
         %obj.liquidDamage(%data, $DamageCrustyLava, $DamageType::Lava);
      case 7:
         //Quick Sand
   }
}

function Armor::onLeaveLiquid(%data, %obj, %type)
{
   switch(%type)
   {
      case 0:
         //Water
         Cancel(%obj.DrownLoop);
         Cancel(%obj.CheckDLoop);
	   %obj.Drowning = 0;
      case 1:
         //Ocean Water
         Cancel(%obj.DrownLoop);
         Cancel(%obj.CheckDLoop);
	   %obj.Drowning = 0;
      case 2:
         //River Water
         Cancel(%obj.DrownLoop);
         Cancel(%obj.CheckDLoop);
	   %obj.Drowning = 0;
      case 3:
         //Stagnant Water
         Cancel(%obj.DrownLoop);
         Cancel(%obj.CheckDLoop);
	   %obj.Drowning = 0;
      case 4:
         //Lava
      case 5:
         //Hot Lava
      case 6:
         //Crusty Lava
      case 7:
         //Quick Sand
   }

   if(%obj.lDamageSchedule !$= "")
   {
      cancel(%obj.lDamageSchedule);
      %obj.lDamageSchedule = "";
   }
}

function DrowningLoop(%obj) {
   //no drowning in purebuild
   if($host::Purebuild) {
      return;
   }
   if(isObject(%obj)) {
      %obj.damage(0, %obj.getPosition(), 0.05, $DamageType::Drown);
   	  %obj.DrownLoop = schedule(250, 0, "DrowningLoop", %obj);
   }
}

function checkforwater(%obj){
   //no drowning in purebuild
   if($host::Purebuild) {
      return;
   }
   if(isObject(%obj)){
      %eyeVec   = %obj.getEyeVector();
      %eyeTrans = %obj.getEyeTransform();
      %eyePos = posFromTransform(%eyeTrans);
      %vector = vectorAdd("0 0 -1", %eyepos);
      %searchresult = containerRayCast(%eyePos, %vector, $TypeMasks::WaterObjectType);
      if(%searchresult){
	     if(%obj.Drowning == 1){
            Cancel(%obj.DrownLoop);
	        %obj.Drowning = 0;
	     }
      }
      else if(%obj.Drowning == 0){
   	     if (!( %obj.isPilot() || %obj.isWeaponOperator() )){
	        %obj.DrownLoop = schedule(15000, 0, "DrowningLoop", %obj);
	        %obj.Drowning = 1;
	     }
      }
      %obj.CheckDLoop = schedule(500, 0, "checkforwater", %obj);
   }
}

function Armor::onTrigger(%data, %player, %triggerNum, %val)
{
   if (%triggerNum == 4)
   {
      // Throw grenade
      if (%val == 1)
      {
         %player.grenTimer = 1;
      }
      else
      {
         if (%player.grenTimer == 0)
         {
            // Bad throw for some reason
         }
         else
         {
            %player.use(Grenade);
            %player.grenTimer = 0;
         }
      }
   }
   else if (%triggerNum == 5)
   {
      // Throw mine
      if (%val == 1)
      {
         %player.mineTimer = 1;
      }
      else
      {
         if (%player.mineTimer == 0)
         {
            // Bad throw for some reason
         }
         else {
            if(%player.inv[C4] > 0) {
               %player.use(C4);
               %player.mineTimer = 0;
            }
            else {
               %player.use(Mine);
               %player.mineTimer = 0;
            }
         }
      }
   }
   else if (%triggerNum == 3) {
   
      if(%player.getMountedImage($weaponslot)){
         if(%player.getMountedImage($WeaponSlot).getName() $= "MedPackGunImage" && %val == 1){
	        checkrevive(%player);
	     }
         //M4A1 Upgrades
         if(%player.getMountedImage($WeaponSlot).getName() $= "M4A1Image" && %val == 1){
            if(!%player.canUseM4A1Attachment) {
               bottomPrint(%player.client, "Reloading... standby", 2, 2);
               return;
            }
            if(%player.client.UpgradeOn("Shotgun Attachment", M4A1Image)) {
               if(%player.M4A1ShotgunClip <= 0) {
                  bottomPrint(%player.client, "No Shotgun Attachment Ammo", 2, 2);
                  return;
               }
               else {
                  if(!%player.client.CheckChallengeCompletion(M4A1Image, 7)) {
                     %player.M4A1ShotgunClip--;
                  }
                  serverPlay3D(ShotgunFireSound, %player.getPosition());
                  
                  %player.canUseM4A1Attachment = 0;
                  schedule(2500, 0, "M4A1RefreshAttachment", %player);

                  %vector = %player.getMuzzleVector($WeaponSlot);
                  %mp = %player.getMuzzlePoint($WeaponSlot);

                  for (%i =0; %i < 8; %i++) {
                     %x = (getRandom() - 0.5) * 2 * 3.1415926 * M4A1Image.projectileSpread;
                     %y = (getRandom() - 0.5) * 2 * 3.1415926 * M4A1Image.projectileSpread;
                     %z = (getRandom() - 0.5) * 2 * 3.1415926 * M4A1Image.projectileSpread;
                     %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
                     %newvector = MatrixMulVector(%mat, %vector);

                     %p = new (TracerProjectile)() {
                         dataBlock        = M1700Pellet;
                         initialDirection = %newvector;
                         initialPosition  = %mp;
                         sourceObject     = %player;
                         damageFactor	  = 1;
                         sourceSlot       = $WeaponSlot;
                     };

                     %p.WeaponImageSource = M4A1Image;
                  }
               }
            }
            else if(%player.client.UpgradeOn("Grenade Launcher Attachment", M4A1Image)) {
               if(%player.M4A1GLClip <= 0) {
                  bottomPrint(%player.client, "No Grenade Launcher Attachment Ammo", 2, 2);
                  return;
               }
               else {
                  if(!%player.client.CheckChallengeCompletion(M4A1Image, 7)) {
                     %player.M4A1GLClip--;
                  }
                  serverPlay3D(GrenadeFireSound, %player.getPosition());

                  %player.canUseM4A1Attachment = 0;
                  schedule(4500, 0, "M4A1RefreshAttachment", %player);

                  %vector = %player.getMuzzleVector($WeaponSlot);
                  %mp = %player.getMuzzlePoint($WeaponSlot);

                  %p = new (GrenadeProjectile)() {
                     dataBlock        = BasicGrenade;
                     initialDirection = %vector;
                     initialPosition  = %mp;
                     sourceObject     = %player;
                     damageFactor	  = 1;
                     sourceSlot       = $WeaponSlot;
                  };
                  %p.WeaponImageSource = M4A1Image;
               }
            }
            else if(%player.client.UpgradeOn("Mini-Launcher Attachment", M4A1Image)) {
               if(%player.M4A1MLClip <= 0) {
                  bottomPrint(%player.client, "No Mini-Launcher Attachment Ammo", 2, 2);
                  return;
               }
               else {
                  if(!%player.client.CheckChallengeCompletion(M4A1Image, 7)) {
                     %player.M4A1MLClip--;
                  }
                  serverPlay3D(MissileFireSound, %player.getPosition());

                  %player.canUseM4A1Attachment = 0;
                  schedule(6000, 0, "M4A1RefreshAttachment", %player);

                  %vector = %player.getMuzzleVector($WeaponSlot);
                  %mp = %player.getMuzzlePoint($WeaponSlot);

                  %p = new (SeekerProjectile)() {
                     dataBlock        = ShoulderMissile;
                     initialDirection = %vector;
                     initialPosition  = %mp;
                     sourceObject     = %player;
                     damageFactor	  = 1;
                     sourceSlot       = $WeaponSlot;
                  };
                  %p.WeaponImageSource = M4A1Image;
               }
            }
         }
      }
      // val = 1 when jet key (LMB) first pressed down
      // val = 0 when jet key released
      // MES - do we need this at all any more?
      if(%val == 1)
         %player.isJetting = true;
      else
         %player.isJetting = false;
   }
}

function Player::setMoveState(%obj, %move)
{
   %obj.disableMove(%move);
}

function Armor::onLeaveMissionArea(%data, %obj)
{
   Game.leaveMissionArea(%data, %obj);
}

function Armor::onEnterMissionArea(%data, %obj)
{
   Game.enterMissionArea(%data, %obj);
}

function Armor::animationDone(%data, %obj)
{
   if(%obj.animResetWeapon !$= "")
   {
      if(%obj.getMountedImage($WeaponSlot) == 0)
         if(%obj.inv[%obj.lastWeapon])
            %obj.use(%obj.lastWeapon);
      %obj.animSetWeapon = "";
   }
}

function playDeathAnimation(%player, %damageLocation, %type)
{
   %vertPos = firstWord(%damageLocation);
   %quadrant = getWord(%damageLocation, 1);

   //echo("vert Pos: " @ %vertPos);
   //echo("quad: " @ %quadrant);

   if( %type == $DamageType::Explosion || %type == $DamageType::Mortar || %type == $DamageType::Grenade)
   {
      if(%quadrant $= "front_left" || %quadrant $= "front_right")
         %curDie = $PlayerDeathAnim::ExplosionBlowBack;
      else
         %curDie = $PlayerDeathAnim::TorsoBackFallForward;
   }
   else if(%vertPos $= "head")
   {
      if(%quadrant $= "front_left" ||  %quadrant $= "front_right" )
         %curDie = $PlayerDeathAnim::HeadFrontDirect;
      else
         %curDie = $PlayerDeathAnim::HeadBackFallForward;
   }
   else if(%vertPos $= "torso")
   {
      if(%quadrant $= "front_left" )
         %curDie = $PlayerDeathAnim::TorsoLeftSpinDeath;
      else if(%quadrant $= "front_right")
         %curDie = $PlayerDeathAnim::TorsoRightSpinDeath;
      else if(%quadrant $= "back_left" )
         %curDie = $PlayerDeathAnim::TorsoBackFallForward;
      else if(%quadrant $= "back_right")
         %curDie = $PlayerDeathAnim::TorsoBackFallForward;
   }
   else if (%vertPos $= "legs")
   {
      if(%quadrant $= "front_left" ||  %quadrant $= "back_left")
         %curDie = $PlayerDeathAnim::LegsLeftGimp;
      if(%quadrant $= "front_right" || %quadrant $= "back_right")
         %curDie = $PlayerDeathAnim::LegsRightGimp;
   }

   if(%curDie $= "" || %curDie < 1 || %curDie > 11)
      %curDie = 1;

   %player.setActionThread("Death" @ %curDie);
}

function Armor::onDamage(%data, %obj)
{
   if(%obj.station !$= "" && %obj.getDamageLevel() == 0)
      %obj.station.getDataBlock().endRepairing(%obj.station);
}

function CreateBlood(%obj) {
   DeathExplosionEffect(%obj);
}

function DeathExplosionEffect(%obj) {
   %pPos = %obj.getPosition();
   %pos = %pPos;

   %obj.setMomentumVector(%momVec);
//   %obj.blowup();

   if(%obj.isZombie || %obj.client.race $= "Bioderm") {
      schedule(200,0,"BiodermGreenSplatter",%obj);
      for (%i=0;%i<6;%i++) {
         %x = getRandom(-3,3);
         %y = getRandom(-3,3);
         %z = 2;
         %vec = %x SPC %y SPC %z;
         %vec = VectorScale(%vec, 200);
         %pos = %obj.getPosition();
         %p = new (GrenadeProjectile)() {
            dataBlock = PurpleBiodermBlood;
            initialDirection = %vec;
            initialPosition = %pos;
            sourceObject = %obj;
            sourceSlot = 1;
            vehicleObject = 0;
         };
         MissionCleanup.add(%p);
         return;
      }
   }
   else {
      for (%i=0;%i<6;%i++) {
         %x = getRandom(-3,3);
         %y = getRandom(-3,3);
         %z = 2;
         %vec = %x SPC %y SPC %z;
         %vec = VectorScale(%vec, 200);
         %pos = %obj.getPosition();
         %p = new (GrenadeProjectile)() {
             dataBlock = HumanBlood;
             initialDirection = %vec;
             initialPosition = %pos;
             sourceObject = %obj;
             sourceSlot = 1;
             vehicleObject = 0;
         };
         MissionCleanup.add(%p);
      }
   }
}

function BiodermGreenSplatter(%obj) {
   for (%i=0;%i<6;%i++) {
      %x = getRandom(-3,3);
      %y = getRandom(-3,3);
      %z = 2;
      %vec = %x SPC %y SPC %z;
      %vec = VectorScale(%vec, 200);
      %pos = %obj.getPosition();
      %p = new (GrenadeProjectile)() {
          dataBlock = BiodermBlood;
          initialDirection = %vec;
          initialPosition = %pos;
          sourceObject = %obj;
          sourceSlot = 1;
          vehicleObject = 0;
      };
      MissionCleanup.add(%p);
   }
}

