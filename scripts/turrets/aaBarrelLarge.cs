//--------------------------------------
// Default blaster
//--------------------------------------

//--------------------------------------------------------------------------
// Sounds and feedback effects
//--------------------------------------
datablock AudioProfile(AASwitchSound)
{
   filename    = "fx/powered/turret_aa_activate.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(AAFireSound)
{
   filename    = "fx/vehicles/tank_chaingun.wav";
   description = AudioDefault3d;
   preload = true;
};

//--------------------------------------------------------------------------
// Particle effects
//--------------------------------------
//--------------------------------------------------------------------------
// Explosion
//--------------------------------------
datablock ParticleData(AABulletExplosionParticle1)
{
   dragCoefficient      = 2;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 000;
   textureName          = "special/crescent4";
   colors[0] = "0.57 0.41 1.0 1.0";
   colors[1] = "0.57 0.41 1.0 1.0";
   colors[2] = "0.57 0.41 0.0 0.0";
   sizes[0]      = 0.25;
   sizes[1]      = 0.5;
   sizes[2]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(AABulletExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 2;
   velocityVariance = 1.5;
   ejectionOffset   = 0.0;
   thetaMin         = 70;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 200;
   particles = "AABulletExplosionParticle1";
};

datablock ParticleData(AABulletExplosionParticle2)
{
   dragCoefficient      = 2;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 000;
   textureName          = "special/blasterHit";
   colors[0] = "0.57 0.41 1.0 0.6";
   colors[1] = "0.57 0.41 1.0 0.6";
   colors[2] = "0.57 0.41 0.0 0.0";
   sizes[0]      = 0.3;
   sizes[1]      = 0.90;
   sizes[2]      = 1.50;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(AABulletExplosionEmitter2)
{
   ejectionPeriodMS = 30;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = false;
   lifetimeMS       = 200;
   particles = "AABulletExplosionParticle2";
};

datablock ExplosionData(AABulletExplosion)
{
   soundProfile   = blasterExpSound;
   emitter[0]     = AABulletExplosionEmitter;
   emitter[1]     = AABulletExplosionEmitter2;
};

//--------------------------------------------------------------------------
// Projectile
//--------------------------------------

datablock TracerProjectileData(Flak_Bullet)
{
   doDynamicClientHits = true;

   directDamage        =0.2;
   directDamageType    = $DamageType::bullet;
   explosion           = GrenadeExplosion;
   splash              = ChaingunSplash;

   hasDamageRadius     = true;
   indirectDamage      = 0.6;
   damageRadius        = 20.0;
   radiusDamageType    = $DamageType::Flak;

   kickBackStrength  = 100;
   sound             = ChaingunProjectile;

   dryVelocity               = 425.0;
   wetVelocity               = 200.0;
   velInheritFactor          = 1.0;
   fizzleTimeMS              = 3000;
   lifetimeMS                = 500;
   lifetimeVarianceMS 	     = 150;
   explodeOnDeath            = true;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 15.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.40;
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
   lightRadius = 5.0;
   lightColor  = "0.5 0.5 0.175";
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock TurretImageData(AABarrelLarge)
{
   shapeFile = "turret_aa_large.dts";
   item      = AABarrelPack; // z0dd - ZOD, 4/25/02. Was wrong: AABarrelLargePack

   projectileType = TracerProjectile;
   projectile = Flak_Bullet;
   projectileSpread = 30.0 / 1000.0;
   usesEnergy = true;
   fireEnergy = 4.0;
   minEnergy = 4.0;
   emap = true;
   isSeeker = true;
   seekRadius   = 300;
   maxSeekAngle = 6;
   seekTime     = 1.0;
   minSeekHeat  = 0.6; 
   useTargetAudio = false;

   // Turret parameters
   activationMS         = 175; // z0dd - ZOD, 3/27/02. Was 250. Amount of time it takes turret to unfold
   deactivateDelayMS    = 500;
   thinkTimeMS          = 140; // z0dd - ZOD, 3/27/02. Was 200. Amount of time before turret starts to unfold (activate)
   degPerSecTheta       = 600;
   degPerSecPhi         = 1080;
   attackRadius         = 550;

   // State transitions
   stateName[0]                     = "Activate";
   stateTransitionOnNotLoaded[0]    = "Dead";
   stateTransitionOnLoaded[0]       = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateSequence[1]                 = "Activate";
   stateSound[1]                    = AASwitchSound;
   stateTimeoutValue[1]             = 1.0;
   stateTransitionOnTimeout[1]      = "Ready";
   stateTransitionOnNotLoaded[1]    = "Deactivate";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";
   stateScript[1]                   = "AAActivateReady";

   stateName[2]                     = "Ready";
   stateTransitionOnNotLoaded[2]    = "Deactivate";
   stateTransitionOnTriggerDown[2]  = "Fire1";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateScript[2]                   = "AAReady";

   stateName[3]                     = "Fire1";
   stateTransitionOnTimeout[3]      = "Reload1";
   stateTimeoutValue[3]             = 0.25;
   stateFire[3]                     = true;
   stateRecoil[3]                   = LightRecoil;
   stateAllowImageChange[3]         = false;
   stateSequence[3]                 = "Fire1";
   stateSound[3]                    = AAFireSound;
   stateScript[3]                   = "onFire";

   stateName[4]                     = "Reload1";
   stateTimeoutValue[4]             = 0.25;
   stateAllowImageChange[4]         = false;
   stateSequence[4]                 = "Reload";
   stateTransitionOnTimeout[4]      = "Fire2";
   stateTransitionOnNotLoaded[4]    = "Deactivate";
   stateTransitionOnNoAmmo[4]       = "NoAmmo";

   stateName[5]                     = "Fire2";
   stateTransitionOnTimeout[5]      = "Reload2";
   stateTimeoutValue[5]             = 0.25;
   stateFire[5]                     = true;
   stateRecoil[5]                   = LightRecoil;
   stateAllowImageChange[5]         = false;
   stateSequence[5]                 = "Fire2";
   stateSound[5]                    = AAFireSound;
   stateScript[5]                   = "onFire";

   stateName[6]                     = "Reload2";
   stateTimeoutValue[6]             = 0.25;
   stateAllowImageChange[6]         = false;
   stateSequence[6]                 = "Reload";
   stateTransitionOnTimeout[6]      = "Ready";
   stateTransitionOnNotLoaded[6]    = "Deactivate";
   stateTransitionOnNoAmmo[6]       = "NoAmmo";

   stateName[7]                     = "Deactivate";
   stateSequence[7]                 = "Activate";
   stateDirection[7]                = false;
   stateTimeoutValue[7]             = 1.0;
   stateTransitionOnLoaded[7]       = "ActivateReady";
   stateTransitionOnTimeout[7]      = "Dead";

   stateName[8]                    = "Dead";
   stateTransitionOnLoaded[8]      = "ActivateReady";

   stateName[9]                    = "NoAmmo";
   stateTransitionOnAmmo[9]        = "Reload2";
   stateSequence[9]                = "NoAmmo";
};

