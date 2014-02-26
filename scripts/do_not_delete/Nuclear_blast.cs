//--------------------------------------
// Arrow IV Nuclear Launcher
//--------------------------------------

//-----------------------------------------
//-----------------------------------------
//Copyright (C) Dave "Uranium - 235" Schutz
//-----------------------------------------
//-----------------------------------------




//--------------------------------------------------------------------------
// Sounds
//--------------------------------------

datablock AudioProfile(NuclearSwitchSound)
{
   filename    = "fx/weapons/missile_launcher_activate.wav";
   description = AudioClosest3d;
   preload = true;
};


datablock AudioProfile(NuclearProjectileSound)
{
   filename    = "fx/weapons/missile_projectile.wav";
   description = ProjectileLooping3d;
   preload = true;
};

datablock AudioProfile(NuclearReloadSound)
{
   filename    = "fx/weapons/weapon.missilereload.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(NuclearLockSound)
{
   filename    = "fx/weapons/missile_launcher_searching.WAV";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(NuclearDryFireSound)
{
   filename    = "fx/weapons/missile_launcher_dryfire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(NukeDeathSound)
{
   filename    = "voice/derm1/gbl.brag.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(NuclearFireSound)
{
   filename    = "fx/weapons/grenade_flash_explode.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(NuclearExplosionSound)
{
   filename    = "fx/vehicles/bomber_bomb_impact.wav";
   description = AudioBIGExplosion3d;
   preload = true;
};

//----------------------------------------------------------------------------
// Explosion smoke particles
//----------------------------------------------------------------------------

datablock ParticleData(NuclearSmokeParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.02;
   inheritedVelFactor   = 0.1;

   lifetimeMS           = 8200;
   lifetimeVarianceMS   = 100;

   textureName          = "special/cloudFlash";

   useInvAlpha = false;
   spinRandomMin = -90.0;
   spinRandomMax = 90.0;

   colors[0]     = "1.0 0.75 0.0 0.0";
   colors[1]     = "0.7 0.7 0.7 1.0";
   colors[2]     = "0.5 0.5 0.5 0.0";
   sizes[0]      = 4;
   sizes[1]      = 6;
   sizes[2]      = 8;
   times[0]      = 0.0;
   times[1]      = 0.1;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(NuclearSmokeEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.5;
   velocityVariance = 0.3;

   thetaMin         = 0.0;
   thetaMax         = 25.0;

   particles = "NuclearSmokeParticle";
};

datablock ParticleData(NuclearFireParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 1.0;

   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 000;

   textureName          = "special/cloudFlash";

   spinRandomMin = -135;
   spinRandomMax =  135;

   colors[0]     = "1.0 0.75 0.2 1.0";
   colors[1]     = "1.0 0.5 0.0 1.0";
   colors[2]     = "1.0 0.40 0.0 0.0";
   sizes[0]      = 2;
   sizes[1]      = 4;
   sizes[2]      = 5.5;
   times[0]      = 0.0;
   times[1]      = 0.3;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(NuclearFireEmitter)
{
   ejectionPeriodMS = 15;
   periodVarianceMS = 0;

   ejectionVelocity = 15.0;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 0.0;

   particles = "NuclearFireParticle";
};

//---------------------------------------------------------------------------
// Shockwaves
//---------------------------------------------------------------------------

datablock ShockwaveData(NuclearShockwave)
{
   width = 5;
   numSegments = 20;
   numVertSegments = 4;
   velocity = 100;
   acceleration = -10.0; // was 40
   lifetimeMS = 8200;
   height = 2.0;  // was 1.0
   verticalCurve = 0.5;

   mapToTerrain = true;
   renderBottom = true;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 6.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0]     = "1.0 0.7 0.5 1.0";
   colors[1]     = "1.0 0.5 0.2 1.0";
   colors[2]     = "1.0 0.25 0.1 0.0";

};

datablock ShockwaveData(NuclearShockwave2)
{
   width = 20.0;
   numSegments = 50;
   numVertSegments = 4;
   velocity = 350;
   acceleration = 0.0; // was 40
   lifetimeMS = 3000;
   height = 300.0;  // was 1.0
   verticalCurve = 0.5;

   mapToTerrain = false;
   orientToNormal = false;
   renderBottom = true;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 6.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0]     = "0.7 0.7 1.0 1.0";
   colors[1]     = "0.7 0.7 1.0 0.5";
   colors[2]     = "0.7 0.7 1.0 1.0";

};

datablock ShockwaveData(NuclearShockwave3)
{
   width = 6.0;
   numSegments = 32;
   numVertSegments = 60;
   velocity = 150;
   acceleration = -5.0;
   lifetimeMS = 8000;
   height = 20.0;
   verticalCurve = 0.5;
   is2D = false;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 6.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0] = "0.8 0.8 0.0 0.50";
   colors[1] = "0.4 0.4 0.0 0.25";
   colors[2] = "0.4 0.4 0.0 0.0";

   mapToTerrain = true;
   orientToNormal = false;
   renderBottom = true;
};

//--------------------------------------------------------------------------
// Particle effects
//--------------------------------------

//----------------------------------------------------
//Condensation
//----------------------------------------------------
datablock ParticleData(NuclearCapSmokeParticle)
{
    dragCoefficient = 0.25;
    gravityCoefficient = -0.01;
    windCoefficient = 0;
    inheritedVelFactor = 0.025;
    constantAcceleration = 0;
    lifetimeMS = 30000;
    lifetimeVarianceMS = 100;
    useInvAlpha = 0;
    spinRandomMin = -90;
    spinRandomMax = 90;
   textureName          = "special/cloudFlash";
   colors[0]     = "0.3 0.3 0.3 0.5";
   colors[1]     = "0.3 0.3 0.3 0.5";
   colors[2]     = "0.3 0.3 0.3 0.0";
   
   
   sizes[0]      = 50;
   sizes[1]      = 50;
   sizes[2]      = 50;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(NuclearCapSmokeEmitter)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 40.0;
    velocityVariance = 5;
    ejectionOffset =   0;
    thetaMin = 90;
    thetaMax = 180;
    phiReferenceVel = 0;
    phiVariance = 360;

	   lifeTimeMS = 25000;
    orientParticles = 0;
    orientOnVelocity = 1;
    particles = "NuclearCapSmokeParticle";
};

datablock ParticleData(NuclearStemSmokeParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   windcoefficient      = 0.0;

   lifetimeMS           = 30000;
   lifetimeVarianceMS   = 100;

   textureName          = "special/cloudFlash";

   useInvAlpha = false;
   spinRandomMin = -90.0;
   spinRandomMax = 90.0;

   colors[0]     = "0.3 0.3 0.3 0.5";
   colors[1]     = "0.3 0.3 0.3 0.5";
   colors[2]     = "0.3 0.3 0.3 0.0";
   sizes[0]      = 50;
   sizes[1]      = 50;
   sizes[2]      = 50;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(NuclearStemSmokeEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 2.0;
   velocityVariance = 1.0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   particles = "NuclearStemSmokeParticle";
};

datablock ParticleData(NuclearDustParticle)
{
   dragCoefficient      = 1.0;
   gravityCoefficient   = 0.00;
   windcoefficient      = 0.0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 30000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 90.0;
   textureName          = "special/cloudFlash";

   colors[0]     = "0.3 0.3 0.3 0.5";
   colors[1]     = "0.3 0.3 0.3 0.5";
   colors[2]     = "0.3 0.3 0.3 0.0";
   sizes[0]      = 50;
   sizes[1]      = 50;
   sizes[2]      = 50;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(NuclearDustEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 150.0;
   velocityVariance = 150.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 23000;
   particles = "NuclearDustParticle";
};

datablock ParticleData(LargeCondensationParticle)
{
    dragCoefficient = 1;
    gravityCoefficient = 0;
    windCoefficient = 0;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 12000;
    lifetimeVarianceMS = 1000;
    useInvAlpha = 0;
    spinRandomMin = -90;
    spinRandomMax = 500;
    textureName = "special/cloudFlash";
    times[0] = 0;
    times[1] = 0.33;
    times[2] = 1;
    colors[0] = "0.7 0.8 1.0 1.0";
    colors[1] = "0.7 0.8 1.0 0.5";
    colors[2] = "0.7 0.8 1.0 0.0";
    sizes[0] = 30;
    sizes[1] = 30;
    sizes[2] = 30;
};

datablock ParticleEmitterData(LargeCondensationEmitter)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 2.66935;
    velocityVariance = 2.59677;
    ejectionOffset =   325;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvances = 0;
	   lifeTimeMS = 8750;
    orientParticles= 0;
    orientOnVelocity = 0;
    particles = "LargeCondensationParticle";
};

datablock ParticleData(SmallCondensationParticle)
{
    dragCoefficient = 1;
    gravityCoefficient = -0.01;
    windCoefficient = 0;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 12000;
    lifetimeVarianceMS = 1000;
    useInvAlpha = 0;
    spinRandomMin = -90;
    spinRandomMax = 500;
    textureName = "special/cloudFlash";
    times[0] = 0;
    times[1] = 0.5;
    times[2] = 1;
    colors[0] = "0.7 0.8 1.0 1.0";
    colors[1] = "0.7 0.8 1.0 0.5";
    colors[2] = "0.7 0.8 1.0 0.0";
    sizes[0] = 30;
    sizes[1] = 30;
    sizes[2] = 30;
};

datablock ParticleEmitterData(SmallCondensationEmitter)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 2.66935;
    velocityVariance = 2.59677;
    ejectionOffset =   275;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvances = 0;
	   lifeTimeMS = 8750;
    orientParticles= 0;
    orientOnVelocity = 0;
    particles = "SmallCondensationParticle";
};

datablock ParticleData(UpperRingParticle)
{
    dragCoefficient = 1.0;
    gravityCoefficient = 0;
    windCoefficient = 0;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 12000;
    lifetimeVarianceMS = 1000;
    useInvAlpha = 0;
    spinRandomMin = -90;
    spinRandomMax = 500;
    textureName = "special/cloudFlash";
    times[0] = 0;
    times[1] = 0.5;
    times[2] = 1;
    colors[0] = "0.7 0.8 1.0 1.0";
    colors[1] = "0.7 0.8 1.0 0.5";
    colors[2] = "0.7 0.8 1.0 0.0";
    sizes[0] = 30;
    sizes[1] = 30;
    sizes[2] = 30;
};

datablock ParticleEmitterData(UpperRingEmitter)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 50;
    velocityVariance = 50;
    ejectionOffset =   150;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvances = 0;
    lifeTimeMS = 28000;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "UpperRingParticle";
};

datablock ParticleData(NuclearSplashParticle)
{
    dragCoefficient = 0;
    gravityCoefficient = 0.1;
    windCoefficient = 0;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 10000;
    lifetimeVarianceMS = 1000;
    useInvAlpha = 0;
    spinRandomMin = 0;
    spinRandomMax = 0;
    textureName = "special/droplet";
    times[0] = 0;
    times[1] = 0.5;
    times[2] = 1;
    colors[0] = "0.7 0.8 1.0 1.0";
    colors[1] = "0.7 0.8 1.0 0.5";
    colors[2] = "0.7 0.8 1.0 0.0";
    sizes[0] = 4.74194;
    sizes[1] = 4.74194;
    sizes[2] = 4.74194;
};

datablock ParticleEmitterData(NuclearSplashEmitter)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 10;
    velocityVariance = 10;
    ejectionOffset =   10;
    thetaMin = 0;
    thetaMax = 31.2097;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvances = 0;
    lifeTimeMS = 30000;
    orientParticles= 1;
    orientOnVelocity = 1;
    particles = "NuclearSplashParticle";
};

datablock ParticleData(DomeCondensationParticle)
{
    dragCoefficient = 0.1;
    gravityCoefficient = 0;
    windCoefficient = 0;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 15000;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = -90;
    spinRandomMax = 500;
    textureName = "special/cloudFlash";
    times[0] = 0;
    times[1] = 0.5;
    times[2] = 1;
    colors[0] = "0.7 0.8 1.0 1.0";
    colors[1] = "0.7 0.8 1.0 0.5";
    colors[2] = "0.7 0.8 1.0 0.0";
    sizes[0] = 7;
    sizes[1] = 7;
    sizes[2] = 7;
};

datablock ParticleEmitterData(DomeCondensationEmitter)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 10;
    velocityVariance = 1;
    ejectionOffset =   10;
    thetaMin = 90;
    thetaMax = 0;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvances = 0;
	lifeTimeMS = 23000;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "DomeCondensationParticle";
};

// More explosions

datablock ExplosionData(RingExplosion1)
{
   emitter[0] = SmallCondensationEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   sizes[0] = "0.01 0.01 0.01";
   sizes[1] = "0.01 0.01 0.01";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock ExplosionData(RingExplosion2)
{
   emitter[0] = LargeCondensationEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   sizes[0] = "0.01 0.01 0.01";
   sizes[1] = "0.01 0.01 0.01";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock ExplosionData(UpperRingExplosion)
{
   emitter[0] = UpperRingEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   sizes[0] = "0.01 0.01 0.01";
   sizes[1] = "0.01 0.01 0.01";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock ExplosionData(DustExplosion)
{
   emitter[0] = NuclearDustEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   sizes[0] = "0.01 0.01 0.01";
   sizes[1] = "0.01 0.01 0.01";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock ExplosionData(CapExplosion)
{
   emitter[0] = NuclearCapSmokeEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   sizes[0] = "0.01 0.01 0.01";
   sizes[1] = "0.01 0.01 0.01";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock ExplosionData(NuclearSplash)
{
   emitter[0] = NuclearSplashEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   sizes[0] = "0.01 0.01 0.01";
   sizes[1] = "0.01 0.01 0.01";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock ExplosionData(DomeCondensationExplosion)
{
   emitter[0] = DomeCondensationEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   sizes[0] = "0.01 0.01 0.01";
   sizes[1] = "0.01 0.01 0.01";
   times[0] = 0.0;
   times[1] = 1.0;
};


//---------------------------------------------------------------------------
// Explosions
//---------------------------------------------------------------------------
datablock ExplosionData(NuclearExplosion1)
{
   shockwave = NuclearShockwave2;
   shockwaveOnTerrain = false;

   explosionShape = "effect_plasma_explosion.dts";

   playSpeed = 0.05;
   soundProfile   = NuclearExplosionSound;
   faceViewer = true;

   sizes[0] = "100.0 100.0 100.0";

   shakeCamera = true;
   camShakeFreq = "6.0 7.0 7.0";
   camShakeAmp = "100.0 100.0 100.0";
   camShakeDuration = 1.0;
   camShakeRadius = 7.0;
};

datablock ExplosionData(NuclearExplosion2)
{
   shockwave = NuclearShockwave;
   shockwaveOnTerrain = false;

   explosionShape = "effect_plasma_explosion.dts";

   playSpeed = 0.05;
   soundProfile   = NuclearExplosionSound;
   faceViewer = true;

   sizes[0] = "100.0 100.0 100.0";

   shakeCamera = true;
   camShakeFreq = "6.0 7.0 7.0";
   camShakeAmp = "70.0 70.0 70.0";
   camShakeDuration = 1.0;
   camShakeRadius = 7.0;
};

datablock ExplosionData(NuclearExplosion3)
{
   explosionShape = "effect_plasma_explosion.dts";

   playSpeed = 0.05;
   soundProfile   = NuclearExplosionSound;
   faceViewer = true;
   
   shockwave = NuclearShockwave3;
   shockwaveOnTerrain = false;

   sizes[0] = "150.0 150.0 150.0";

   shakeCamera = true;
   camShakeFreq = "6.0 7.0 7.0";
   camShakeAmp = "70.0 70.0 70.0";
   camShakeDuration = 1.0;
   camShakeRadius = 7.0;
};

datablock ExplosionData(NuclearExplosion4)
{
   explosionShape = "effect_plasma_explosion.dts";

   playSpeed = 0.05;
   soundProfile   = NuclearExplosionSound;
   faceViewer = true;

   sizes[0] = "250.0 250.0 250.0";

   shakeCamera = true;
   camShakeFreq = "6.0 7.0 7.0";
   camShakeAmp = "70.0 70.0 70.0";
   camShakeDuration = 1.0;
   camShakeRadius = 7.0;
};

datablock ExplosionData(NuclearMainExplosion)
{
   soundProfile = NuclearExplosionSound;

   subExplosion[0] = NuclearExplosion1;
   subExplosion[1] = NuclearExplosion2;
   subExplosion[2] = NuclearExplosion3;
   subExplosion[3] = NuclearExplosion4;
   subExplosion[4] = DustExplosion;
};

//--------------------------------------------------------------------------
// Projectile
//--------------------------------------
datablock SeekerProjectileData(ShoulderNuclear)
{
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   hasDamageRadius     = true;
   indirectDamage      = 1000.0;
   damageRadius        = 250.0;
   radiusDamageType    = $DamageType::Missile;
   kickBackStrength    = 100000;

   Underwaterexplosion = "NuclearSplash";
   splash              = BlasterSplash;
   velInheritFactor    = 0.5;
   depthTolerance      = 10.0; // depth at which it uses underwater explosion
   
   baseEmitter         = NuclearSmokeEmitter;
   delayEmitter        = NuclearFireEmitter;
   puffEmitter         = NuclearPuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;


   exhaustEmitter      = NuclearExhaustEmitter;
   exhaustTimeMs       = 2000;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = 1800000;        //60000
   muzzleVelocity      = 1.0;          //1
   maxVelocity         = 100.0; //80   100
   turningSpeed        = 120.0; //110  120
   acceleration        = 10.0; //200   10

   proximityRadius     = 5;

   terrainAvoidanceSpeed         = 480; //180
   terrainScanAhead              = 25;  //25
   terrainHeightFail             = 3;  //12
   terrainAvoidanceRadius        = 100; //100

   flareDistance = 50; //200
   flareAngle    = 5; //30

   sound = NuclearProjectileSound;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.2 0.05 0";

   useFlechette = true;
   flechetteDelayMs = 550;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = false;
};

datablock LinearProjectileData(RingProjectile1)
{
   projectileShapeName = "weapon_missile_Projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 1.0;
   damageRadius        = 500.0;
   radiusDamageType    = $DamageType::EMP;
   kickBackStrength    = 0;

   sound 				= discProjectileSound;
   explosion           = "RingExplosion1";
   underwaterExplosion = "RingExplosion1";
   splash              = DiscSplash;
   
   dryVelocity       = 180;
   wetVelocity       = 180;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 1500;
   lifetimeMS        = 1500;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 5000;

   activateDelayMS = -1;
};

datablock LinearProjectileData(RingProjectile2)
{
   projectileShapeName = "weapon_missile_Projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 1000.0;
   damageRadius        = 250.0;
   radiusDamageType    = $DamageType::Nuclear;
   kickBackStrength    = 0;

   sound 				= discProjectileSound;
   explosion           = "RingExplosion2";
   underwaterExplosion = "RingExplosion2";
   splash              = DiscSplash;
   depthTolerance      = 10.0; // depth at which it uses underwater explosion

   dryVelocity       = 180;
   wetVelocity       = 180;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 1000;
   lifetimeMS        = 1000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 5000;

   activateDelayMS = 200;
};

datablock LinearProjectileData(StemProjectile1)
{
   projectileShapeName = "weapon_missile_Projectile.dts";
   emitterDelay        = 1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 1000.0;
   damageRadius        = 250.0;
   radiusDamageType    = $DamageType::Nuclear;
   kickBackStrength    = 0;

   baseEmitter         = NuclearStemSmokeEmitter;

   dryVelocity       = 50;
   wetVelocity       = 50;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 5000;
   lifetimeMS        = 5000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 5000;

   activateDelayMS = 200;
};

datablock LinearProjectileData(StemProjectile2)
{
   projectileShapeName = "weapon_missile_Projectile.dts";
   emitterDelay        = 1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 1000.0;
   damageRadius        = 250.0;
   radiusDamageType    = $DamageType::Nuclear;
   kickBackStrength    = 0;

   explosion           = "CapExplosion";
   underwaterExplosion = "CapExplosion";

   dryVelocity       = 50;
   wetVelocity       = 50;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 4000;
   lifetimeMS        = 4000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 4000;

   activateDelayMS = 200;
};

datablock LinearProjectileData(UpperRingProjectile1)
{
   projectileShapeName = "weapon_missile_Projectile.dts";
   emitterDelay        = 1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 1000.0;
   damageRadius        = 250.0;
   radiusDamageType    = $DamageType::Nuclear;
   kickBackStrength    = 0;

   explosion           = "UpperRingExplosion";
   underwaterExplosion = "UpperRingExplosion";

   dryVelocity       = 180;
   wetVelocity       = 180;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 2000;
   lifetimeMS        = 2000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 2000;

   activateDelayMS = -1;
};

datablock LinearProjectileData(DustProjectile1)
{
   projectileShapeName = "weapon_missile_Projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 1000.0;
   damageRadius        = 250.0;
   radiusDamageType    = $DamageType::Nuclear;
   kickBackStrength    = 35000;
   
   maxWhiteout = 2.0;

   sound 				= discProjectileSound;
   explosion           = "NuclearMainExplosion";
   underwaterExplosion = "NuclearMainExplosion";
   splash              = DiscSplash;

   dryVelocity       = 180;
   wetVelocity       = 180;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 100;
   lifetimeMS        = 100;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 5000;

   activateDelayMS = 200;
};

function ShoulderNuclear::onExplode(%data, %proj, %pos, %mod)
{
   if (%data.hasDamageRadius)
      RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
   for(%i=0;%i<1;%i++)
   {
      %p = new LinearProjectile() {
         dataBlock        = RingProjectile2;
         initialDirection = "0 0 1";
         initialPosition  = %pos;
         //sourceObject     = %projectile.sourceObject;
         sourceSlot       = %projectile.sourceSlot;
         vehicleObject    = %projectile.vehicleObject;
      };
      MissionCleanup.add(%p);
   }
      %p = new LinearProjectile() {
         dataBlock        = RingProjectile1;
         initialDirection = "0 0 1";
         initialPosition  = %pos;
         //sourceObject     = %projectile.sourceObject;
         sourceSlot       = %projectile.sourceSlot;
         vehicleObject    = %projectile.vehicleObject;
      };
      MissionCleanup.add(%p);

      %p = new LinearProjectile() {
         dataBlock        = DustProjectile1;
         initialDirection = "0 0 1";
         initialPosition  = %pos;
         //sourceObject     = %projectile.sourceObject;
         sourceSlot       = %projectile.sourceSlot;
         vehicleObject    = %projectile.vehicleObject;
      };
      MissionCleanup.add(%p);

      %p = new LinearProjectile() {
         dataBlock        = StemProjectile1;
         initialDirection = "0 0 1";
         initialPosition  = %pos;
         //sourceObject     = %projectile.sourceObject;
         sourceSlot       = %projectile.sourceSlot;
         vehicleObject    = %projectile.vehicleObject;
      };
      MissionCleanup.add(%p);
      
      %p = new LinearProjectile() {
         dataBlock        = UpperRingProjectile1;
         initialDirection = "0 0 1";
         initialPosition  = %pos;
         //sourceObject     = %projectile.sourceObject;
         sourceSlot       = %projectile.sourceSlot;
         vehicleObject    = %projectile.vehicleObject;
      };
      MissionCleanup.add(%p);
      
      %p = new LinearProjectile() {
         dataBlock        = StemProjectile2;
         initialDirection = "0 0 1";
         initialPosition  = %pos;
         //sourceObject     = %projectile.sourceObject;
         sourceSlot       = %projectile.sourceSlot;
         vehicleObject    = %projectile.vehicleObject;
      };
      MissionCleanup.add(%p);

}
