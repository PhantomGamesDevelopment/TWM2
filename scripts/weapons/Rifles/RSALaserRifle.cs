datablock AudioProfile(LaserRifleFireSound)
{
   filename    = "fx/weapons/sniper_fire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock ParticleData(RedFlareParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.15;
   inheritedVelFactor   = 0.5;

   lifetimeMS           = 1800;
   lifetimeVarianceMS   = 200;

   textureName          = "special/flareSpark";

   colors[0]     = "1.0 1.0 1.0 1.0";
   colors[1]     = "1.0 1.0 1.0 1.0";
   colors[2]     = "1.0 1.0 1.0 0.0";

   sizes[0]      = 0.6;
   sizes[1]      = 0.3;
   sizes[2]      = 0.1;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(RedFlareemmiter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.0;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   orientParticles  = true;
   orientOnVelocity = false;

   particles = "RedFlareParticle";
};

datablock LinearFlareProjectileData(LaserShot)
{
   scale               = "1.0 1.0 1.0";
   faceViewer          = false;
   directDamage        = 0.6;
   kickBackStrength    = 100.0;
   directDamageType    = $DamageType::LaserRifle;

   explosion           = "BlasterExplosion";
   splash              = PlasmaSplash;

   ImageSource         = "lasergunImage";

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

   baseEmitter         = RedFlareemmiter;
   delayEmitter        = RedFlareemmiter;
   bubbleEmitter       = RedFlareemmiter;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.2;
   size[2]           = 0.2;


   numFlares         = 15;
   flareColor        = "1 0 0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound        = MissileProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0 0";

};

//--------------------------------------------------------------------------
// Ammo
//--------------------------------------

datablock ItemData(lasergunAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_chaingun.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some laser rifle ammo";

   computeCRC = true;

};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(lasergunImage)
{
   className = WeaponImage;
   shapeFile = "weapon_sniper.dts";
   item      = lasergun;
   ammo 	 = lasergunAmmo;
   projectile = LaserShot;
   projectileType = LinearFlareProjectile;
   emap = true;
	armThread = looksn;
   mass = 10;

   casing              = ShellDebris;
   shellExitDir        = "1.0 0.3 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 15.0;
   shellVelocity       = 3.0;

   //ClipStuff
   ClipName = "lasergunClip";
   ClipPickupName["lasergunClip"] = "A Photonic Laser Battery";
   ShowsClipInHud = 1;
   ClipReloadTime = 6;
   ClipReturn = 10;
   InitialClips = 5;
   //
   //Challenges
   HasChallenges = 1;
   ChallengeCt = 8;
   Challenge[1] = "Laser Rifle Blaster\t100\t250\tNone";
   Challenge[2] = "Laser Rifle Expert\t250\t500\tGrip";
   Challenge[3] = "Laser Rifle Master\t750\t1000\tLaser";
   Challenge[4] = "Laser Rifle God\t1500\t2000\tSilencer";
   Challenge[5] = "Laser Rifle Bronze Commendation\t5000\t10000\tNone";
   Challenge[6] = "Laser Rifle Silver Commendation\t10000\t25000\tNone";
   Challenge[7] = "Laser Rifle Gold Commendation\t25000\t50000\tNone";
   Challenge[8] = "Laser Rifle Titan Commendation\t50000\t75000\tNone";
   Upgrade[1] = "Grip";
   Upgrade[2] = "Laser";
   Upgrade[3] = "Silencer";
   GunName = "RSA Laser Rifle";
   //

   RankRequire = $TWM2::RankRequire["RSALaserRifle"];

   maxSpread = 10.0 / 1000.0;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = ChaingunSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "CheckWet";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.0001;
   stateFire[3] = true;
   stateEmitter[3] = "GunFireEffectEmitter";
   stateEmitterNode[3] = "muzzlepoint1";
   stateEmitterTime[3] = 1;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateScript[3] = "onFire";
   stateEmitterTime[3] = 0.1;
   //stateSound[3] = LaserRifleFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.7;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = ChaingunDryFireSound;
   stateTimeoutValue[6]        = 0.2;
   stateTransitionOnTimeout[6] = "NoAmmo";

   stateName[7]       = "WetFire";
   stateSound[7]      = ChaingunDryFireSound;
   stateTimeoutValue[7]        = 0.2;
   stateTransitionOnTimeout[7] = "Ready";

   stateName[8]               = "CheckWet";
   stateTransitionOnWet[8]    = "WetFire";
   stateTransitionOnNotWet[8] = "Fire";
};

datablock ItemData(lasergun)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_chaingun.dts";
   image        = lasergunImage;
   mass         = 1.0;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
   pickUpName   = "a Laser Rifle";

   computeCRC = true;
   emap = true;
};

function lasergunImage::onFire(%data, %obj, %slot) {
   %p = Parent::onFire(%data, %obj, %slot);
   if(%obj.client.UpgradeOn("Silencer", %data.getName())) {
      //zero sound :p
   }
   else {
      ServerPlay3d(LaserRifleFireSound, %obj.getPosition());
   }
}
