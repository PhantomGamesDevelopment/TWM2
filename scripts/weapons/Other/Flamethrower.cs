datablock AudioProfile(FlamethrowerFireSound) {
   filename    = "fx/Bonuses/Nouns/snake.wav";
   description = AudioClose3d;
   preload = true;
};

//Muzzle Emmiter
datablock ParticleData(ThrowerMuzzleParticle) {
   dragCoeffiecient     = 0.4;
   gravityCoefficient   = -0.0;   // rises slowly
   inheritedVelFactor   = 0.125;

   lifetimeMS           =  800;
   lifetimeVarianceMS   =  500;
   useInvAlpha          =  false;
   spinRandomMin        = 500.0;
   spinRandomMax        =  700.0;

   textureName = "special/cloudFlash";


   colors[0]     = "1.0 0.2 0.0 0.5";
   colors[1]     = "1.0 0.2 0.0 0.8";
   colors[2]     = "1.0 0.2 0.0 0.0";
   colors[3]     = "1.0 0.2 0.0 0.0";
   
   sizes[0]      = 0.2;
   sizes[1]      = 0.3;
   sizes[2]      = 0.5;
   sizes[3]      = 0.6;
   
   times[0]      = 0.2; //.2
   times[1]      = 0.5; //.5
   times[2]      = 0.7; //.7
   times[3]      = 1.0; //1.0
};

datablock ParticleEmitterData(ThrowerMuzzleEmitter) {
   ejectionPeriodMS = 3;
   periodVarianceMS = 1;

   ejectionVelocity = 0.0;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 45.0;

   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;

   particles = "ThrowerMuzzleParticle";
};
//

datablock ParticleData(ThrowerExplosionParticle) {
   dragCoefficient      = 2;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 0;
   textureName          = "special/cloudflash";
   colors[0]     = "1 0.18 0.03 0.6";
   colors[1]     = "1 0.18 0.03 0.0";
   sizes[0]      = 2;
   sizes[1]      = 2.5;
};

datablock ParticleEmitterData(ThrowerExplosionEmitter) {
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionOffset = 2.0;
   ejectionVelocity = 4.0;
   velocityVariance = 0.0;
   thetaMin         = 60.0;
   thetaMax         = 90.0;
   lifetimeMS       = 250;

   particles = "ThrowerExplosionParticle";
};

datablock ExplosionData(ThrowerExplosion) {
   particleEmitter = ThrowerExplosionEmitter;
   particleDensity = 75;
   particleRadius = 1.25;
   faceViewer = true;
};

datablock LinearFlareProjectileData(FlamethrowerBolt) {
   projectileShapeName = "turret_muzzlepoint.dts";
   scale               = "1.0 1.0 1.0";
   faceViewer          = true;
   directDamage        = 0.02;
   hasDamageRadius     = true;
   indirectDamage      = 0.02;
   damageRadius        = 4.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::Fire;
   
   ImageSource         = "flamerImage";

   explosion           = "ThrowerExplosion";
   splash              = PlasmaSplash;

   baseEmitter        = ThrowerBaseEmitter;

   dryVelocity       = 50.0; // z0dd - ZOD, 7/20/02. Faster plasma projectile. was 55
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 250;
   lifetimeMS        = 745;
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

//THE GUN
datablock ItemData(flamerAmmo) {
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_plasma.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some flame thrower fuel";
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ItemData(flamer) {
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_plasma.dts";
   image = flamerImage;
   mass = 1.0;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a flame thrower";
};

datablock ShapeBaseImageData(flamerImage) {
   className = WeaponImage;
   shapeFile = "weapon_grenade_launcher.dts";
   mass = 10;
   item = flamer;
   ammo = flamerAmmo;
   offset = "0 0 0"; // L/R - F/B - T/B
   rotation = "0 1 0 180"; // L/R - F/B - T/B

   projectileSpread = 5.0 / 1000.0;
   
   //ClipStuff
   ClipName = "FlamerClip";
   ClipPickupName["FlamerClip"] = "some flamethrower fuel canisters";
   ShowsClipInHud = 1;
   ClipReloadTime = 5;
   ClipReturn = 100;
   InitialClips = 6;
   
   GunName = "A|V|X Flamethrower";
   //
   MedalRequire = 10;

   projectile = FlamethrowerBolt;
   projectileType = LinearFlareProjectile;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = PlasmaSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateEmitter[2] = "ThrowerMuzzleEmitter";
   stateEmitterNode[2] = "muzzlepoint1";
   stateEmitterTime[2] = 10000;
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "CheckWet";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.02;
   stateFire[3] = true;
   stateAllowImageChange[3] = false;
   stateScript[3] = "onFire";
   stateSound[3] = FlamethrowerFireSound;
   stateEmitterTime[3] = 0.02;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.02;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";
   stateSound[4] = PlasmaReloadSound;

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = PlasmaDryFireSound;
   stateTimeoutValue[6]        = 1.0;
   stateTransitionOnTimeout[6] = "NoAmmo";

   stateName[7]       = "WetFire";
   stateSound[7]      = PlasmaFireWetSound;
   stateTimeoutValue[7]        = 1.0;
   stateTransitionOnTimeout[7] = "Ready";

   stateName[8]               = "CheckWet";
   stateTransitionOnWet[8]    = "WetFire";
   stateTransitionOnNotWet[8] = "Fire";
};

