//--------------------------------------
// Chaingun
//--------------------------------------

//--------------------------------------------------------------------------
// Sounds
//--------------------------------------
datablock AudioProfile(ChaingunSwitchSound)
{
   filename    = "fx/weapons/chaingun_activate.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ChaingunFireSound)
{
   filename    = "fx/weapons/chaingun_fire.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ChaingunProjectile)
{
   filename    = "fx/weapons/chaingun_projectile.wav";
   description = ProjectileLooping3d;
   preload = true;
};

datablock AudioProfile(ChaingunImpact)
{
   filename    = "fx/weapons/chaingun_impact.WAV";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ChaingunSpinDownSound)
{
   filename    = "fx/weapons/chaingun_spindown.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ChaingunSpinUpSound)
{
   filename    = "fx/weapons/chaingun_spinup.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ChaingunDryFireSound)
{
   filename    = "fx/weapons/chaingun_dryfire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ShrikeBlasterProjectileSound)
{
   filename    = "fx/vehicles/shrike_blaster_projectile.wav";
   description = ProjectileLooping3d;
   preload = true;
};


//--------------------------------------------------------------------------
// Splash
//--------------------------------------------------------------------------

datablock ParticleData( ChaingunSplashParticle )
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -1.4;
   lifetimeMS           = 300;
   lifetimeVarianceMS   = 0;
   textureName          = "special/droplet";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.05;
   sizes[1]      = 0.2;
   sizes[2]      = 0.2;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( ChaingunSplashEmitter )
{
   ejectionPeriodMS = 4;
   periodVarianceMS = 0;
   ejectionVelocity = 3;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 50;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 100;
   particles = "ChaingunSplashParticle";
};


datablock SplashData(ChaingunSplash)
{
   numSegments = 10;
   ejectionFreq = 10;
   ejectionAngle = 20;
   ringLifetime = 0.4;
   lifetimeMS = 400;
   velocity = 3.0;
   startRadius = 0.0;
   acceleration = -3.0;
   texWrap = 5.0;

   texture = "special/water2";

   emitter[0] = ChaingunSplashEmitter;

   colors[0] = "0.7 0.8 1.0 0.0";
   colors[1] = "0.7 0.8 1.0 1.0";
   colors[2] = "0.7 0.8 1.0 0.0";
   colors[3] = "0.7 0.8 1.0 0.0";
   times[0] = 0.0;
   times[1] = 0.4;
   times[2] = 0.8;
   times[3] = 1.0;
};

//--------------------------------------------------------------------------
// Particle Effects
//--------------------------------------
datablock ParticleData(ChaingunFireParticle)
{
   dragCoefficient      = 2.75;
   gravityCoefficient   = 0.1;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 550;
   lifetimeVarianceMS   = 0;
   textureName          = "particleTest";
   colors[0]     = "0.46 0.36 0.26 1.0";
   colors[1]     = "0.46 0.36 0.26 0.0";
   sizes[0]      = 0.25;
   sizes[1]      = 0.20;
};

datablock ParticleEmitterData(ChaingunFireEmitter)
{
   ejectionPeriodMS = 6;
   periodVarianceMS = 0;
   ejectionVelocity = 10;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 12;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance  = true;
   particles = "ChaingunFireParticle";
};

//--------------------------------------------------------------------------
// Explosions
//--------------------------------------
datablock ParticleData(ChaingunExplosionParticle1)
{
   dragCoefficient      = 0.65;
   gravityCoefficient   = 0.3;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 150;
   textureName          = "particleTest";
   colors[0]     = "0.56 0.36 0.26 1.0";
   colors[1]     = "0.56 0.36 0.26 0.0";
   sizes[0]      = 0.0625;
   sizes[1]      = 0.2;
};

datablock ParticleEmitterData(ChaingunExplosionEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 0.75;
   velocityVariance = 0.25;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "ChaingunExplosionParticle1";
};




datablock ParticleData(ChaingunImpactSmokeParticle)
{
   dragCoefficient      = 0.0;
   gravityCoefficient   = -0.2;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 200;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 90.0;
   textureName          = "particleTest";
   colors[0]     = "0.7 0.7 0.7 0.0";
   colors[1]     = "0.7 0.7 0.7 0.4";
   colors[2]     = "0.7 0.7 0.7 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.5;
   sizes[2]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ChaingunImpactSmoke)
{
   ejectionPeriodMS = 8;
   periodVarianceMS = 1;
   ejectionVelocity = 1.0;
   velocityVariance = 0.5;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 35;
   overrideAdvances = false;
   particles = "ChaingunImpactSmokeParticle";
   lifetimeMS       = 50;
};


datablock ParticleData(ChaingunSparks)
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 300;
   lifetimeVarianceMS   = 0;
   textureName          = "special/spark00";
   colors[0]     = "0.56 0.36 0.26 1.0";
   colors[1]     = "0.56 0.36 0.26 1.0";
   colors[2]     = "1.0 0.36 0.26 0.0";
   sizes[0]      = 0.6;
   sizes[1]      = 0.2;
   sizes[2]      = 0.05;
   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(ChaingunSparkEmitter)
{
   ejectionPeriodMS = 4;
   periodVarianceMS = 0;
   ejectionVelocity = 4;
   velocityVariance = 2.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 50;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 100;
   particles = "ChaingunSparks";
};


datablock ExplosionData(ChaingunExplosion)
{
   soundProfile   = ChaingunImpact;

   emitter[0] = ChaingunImpactSmoke;
   emitter[1] = ChaingunSparkEmitter;

   faceViewer           = false;
};

//--------------------------------------------------------------------------
// Particle effects
//--------------------------------------


datablock DebrisData( ShellDebris )
{
   shapeName = "weapon_chaingun_ammocasing.dts";

   lifetime = 3.0;

   minSpinSpeed = 300.0;
   maxSpinSpeed = 400.0;

   elasticity = 0.5;
   friction = 0.2;

   numBounces = 3;

   fade = true;
   staticOnMaxBounce = true;
   snapOnMaxBounce = true;
};             


//--------------------------------------------------------------------------
// Projectile
//--------------------------------------
datablock DecalData(ChaingunDecal1)
{
   sizeX       = 0.05;
   sizeY       = 0.05;
   textureName = "special/bullethole1";
};
datablock DecalData(ChaingunDecal2) : ChaingunDecal1
{
   textureName = "special/bullethole2";
};

datablock DecalData(ChaingunDecal3) : ChaingunDecal1
{
   textureName = "special/bullethole3";
};
datablock DecalData(ChaingunDecal4) : ChaingunDecal1
{
   textureName = "special/bullethole4";
};
datablock DecalData(ChaingunDecal5) : ChaingunDecal1
{
   textureName = "special/bullethole5";
};
datablock DecalData(ChaingunDecal6) : ChaingunDecal1
{
   textureName = "special/bullethole6";
};


datablock TracerProjectileData(ChaingunBullet)
{
   doDynamicClientHits = true;

   directDamage        = 0.06;
   directDamageType    = $DamageType::Bullet;
   explosion           = "ChaingunExplosion";
   splash              = ChaingunSplash;

   kickBackStrength  = 0.0;
   sound 				= ChaingunProjectile;

   dryVelocity       = 425.0;
   wetVelocity       = 100.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 3000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
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
	tracerWidth     = 0.10;
   crossSize       = 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;
   
   ImageSource         = "MiniChaingunImage";

   decalData[0] = ChaingunDecal1;
   decalData[1] = ChaingunDecal2;
   decalData[2] = ChaingunDecal3;
   decalData[3] = ChaingunDecal4;
   decalData[4] = ChaingunDecal5;
   decalData[5] = ChaingunDecal6;
};

datablock LinearFlareProjectileData(MCGPhaserBolt) {
   directDamage        = 0.07;
   directDamageType    = $DamageType::Phaser;
   explosion           = "BlasterExplosion";
   kickBackStrength  = 0.0;

   dryVelocity       = 120.0;
   wetVelocity       = 40.0;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 2000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;
   
   ImageSource         = "MiniChaingunImage";

   numFlares         = 5;
   size              = 0.04;
   flareColor        = "1 1 1";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound = BlasterProjectileSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 1 1";
};

//--------------------------------------------------------------------------
// Ammo
//--------------------------------------

datablock ItemData(MiniChaingunAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_chaingun.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some chaingun ammo";

   computeCRC = true;

};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(MiniChaingunImage)
{
   className = WeaponImage;
   shapeFile = "weapon_chaingun.dts";
   item      = MiniChaingun;
   ammo 	 = MiniChaingunAmmo;
   projectile = ChaingunBullet;
   projectileType = TracerProjectile;
   emap = true;
   
   //ClipStuff
   ClipName = "MiniChaingunClip";
   ClipPickupName["MiniChaingunClip"] = "some mini-chaingun belts";
   ShowsClipInHud = 1;
   ClipReloadTime = 6;
   ClipReturn = 100;
   InitialClips = 4;
   //
   HasChallenges = 1;
   ChallengeCt = 9;
   Challenge[1] = "MCG Killer\t50\t100\tNone";
   Challenge[2] = "MCG Hunter\t100\t250\tGrip";
   Challenge[3] = "MCG Expert\t250\t500\tLaser";
   Challenge[4] = "MCG Master\t500\t1000\tSilencer";
   Challenge[5] = "MCG God\t1000\t2000\tFusion Core";
   Challenge[6] = "MCG Bronze Commendation\t2500\t10000\tNone";
   Challenge[7] = "MCG Silver Commendation\t5000\t25000\tNone";
   Challenge[8] = "MCG Gold Commendation\t10000\t50000\tNone";
   Challenge[9] = "MCG Titan Commendation\t25000\t75000\tNone";
   Upgrade[1] = "Grip";
   Upgrade[2] = "Laser";
   Upgrade[3] = "Silencer";
   Upgrade[4] = "Fusion Core";
   GunName = "Mini-Chaingun";
   
   
   RankRequire = $TWM2::RankRequire["MiniChaingun"];

   casing              = ShellDebris;
   shellExitDir        = "1.0 0.3 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 15.0;
   shellVelocity       = 3.0;

   projectileSpread = 8.0 / 1000.0;

   //--------------------------------------
   stateName[0]             = "Activate";
   stateSequence[0]         = "Activate";
   stateSound[0]            = ChaingunSwitchSound;
   stateAllowImageChange[0] = false;
   //
   stateTimeoutValue[0]        = 0.5;
   stateTransitionOnTimeout[0] = "Ready";
   stateTransitionOnNoAmmo[0]  = "NoAmmo";

   //--------------------------------------
   stateName[1]       = "Ready";
   stateSpinThread[1] = Stop;
   //
   stateTransitionOnTriggerDown[1] = "Spinup";
   stateTransitionOnNoAmmo[1]      = "NoAmmo";

   //--------------------------------------
   stateName[2]               = "NoAmmo";
   stateTransitionOnAmmo[2]   = "Ready";
   stateSpinThread[2]         = Stop;
   stateTransitionOnTriggerDown[2] = "DryFire";

   //--------------------------------------
   stateName[3]         = "Spinup";
   stateSpinThread[3]   = SpinUp;
   stateSound[3]        = ChaingunSpinupSound;
   //
   stateTimeoutValue[3]          = 0.5;
   stateWaitForTimeout[3]        = false;
   stateTransitionOnTimeout[3]   = "Fire";
   stateTransitionOnTriggerUp[3] = "Spindown";

   //--------------------------------------
   stateName[4]             = "Fire";
   stateSequence[4]            = "Fire";
   stateSequenceRandomFlash[4] = true;
   stateSpinThread[4]       = FullSpeed;
   //stateSound[4]            = ChaingunFireSound;
   //stateRecoil[4]           = LightRecoil;
   stateAllowImageChange[4] = false;
   stateScript[4]           = "onFire";
   stateFire[4]             = true;
   stateEjectShell[4]       = true;
   //
   stateTimeoutValue[4]          = 0.02;
   stateTransitionOnTimeout[4]   = "Fire";
   stateTransitionOnTriggerUp[4] = "Spindown";
   stateTransitionOnNoAmmo[4]    = "EmptySpindown";

   //--------------------------------------
   stateName[5]       = "Spindown";
   stateSound[5]      = ChaingunSpinDownSound;
   stateSpinThread[5] = SpinDown;
   //
   stateTimeoutValue[5]            = 1.0;
   stateWaitForTimeout[5]          = true;
   stateTransitionOnTimeout[5]     = "Ready";
   stateTransitionOnTriggerDown[5] = "Spinup";

   //--------------------------------------
   stateName[6]       = "EmptySpindown";
   stateSound[6]      = ChaingunSpinDownSound;
   stateSpinThread[6] = SpinDown;
   //
   stateTimeoutValue[6]        = 0.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
   
   //--------------------------------------
   stateName[7]       = "DryFire";
   stateSound[7]      = ChaingunDryFireSound;
   stateTimeoutValue[7]        = 0.5;
   stateTransitionOnTimeout[7] = "NoAmmo";
};

datablock ItemData(MiniChaingun)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_chaingun.dts";
   image        = MiniChaingunImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
   pickUpName   = "a mini-chaingun";

   computeCRC = true;
   emap = true;
};

function MiniChaingunImage::onFire(%data, %obj, %slot) {
   if(%obj.client.UpgradeOn("Silencer", %data.getName())) {
      //zero sound :p
      %p = Parent::onFire(%data, %obj, %slot);
   }
   else if(%obj.client.UpgradeOn("Fusion Core", %data.getName())) {
      ServerPlay3d(ChaingunFireSound, %obj.getPosition());
      %p = spawnprojectile(MCGPhaserBolt, LinearFlareProjectile,
         %obj.getMuzzlePoint(%slot), %obj.getMuzzleVector(%slot), %obj);
      %p.WeaponImageSource = "MiniChaingunImage";
      %obj.decInventory(%data.ammo, 1);
      if(%obj.inv[%data.ammo] == 0) { //Added Phantom139, TWM2
         AttemptReload(%data, %obj, %slot);
      }
   }
   else {
      ServerPlay3d(ChaingunFireSound, %obj.getPosition());
      %p = Parent::onFire(%data, %obj, %slot);
   }
}
