datablock TracerProjectileData(PulseRifleBullet) {
   doDynamicClientHits = true;

   directDamage        = 0.2;
   explosion           = "BlasterExplosion";
   splash              = ChaingunSplash;

   directDamageType    = $DamageType::ShrikeBlaster;
   kickBackStrength  = 0.0;

   sound = ShrikeBlasterProjectileSound;
   
   ImageSource         = "PulseRifleImage";

   dryVelocity       = 425.0;
   wetVelocity       = 100.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 1000;
   lifetimeMS        = 1000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 45.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = "1.0 0.0 0.0 0.0";
	tracerTex[0]  	 = "special/shrikeBolt";
	tracerTex[1]  	 = "special/shrikeBoltCross";
	tracerWidth     = 0.55;
   crossSize       = 0.99;
   crossViewAng    = 0.990;
   renderCross     = true;

};

datablock ItemData(PulseRifleAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_chaingun.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "Some Pulse Rifle Bullets";

   computeCRC = true;

};

datablock ShapeBaseImageData(PulseRifleImage)
{
   className = WeaponImage;
   shapeFile = "weapon_sniper.dts";
   mass = 10;
   item = PulseRifle;
   ammo = PulseRifleAmmo;
   projectile = PulseRifleBullet;
   projectileType = TracerProjectile;
   emap = true;

   //ClipStuff
   ClipName = "PulseRifleClip";
   ClipPickupName["PulseRifleClip"] = "some Pulse Clip Cartons";
   ShowsClipInHud = 1;
   ClipReloadTime = 5;
   ClipReturn = 20;
   InitialClips = 7;
   //
   //Challenges
   HasChallenges = 1;
   ChallengeCt = 8;
   Challenge[1] = "Pulse Hunter\t75\t150\tNone";
   Challenge[2] = "Pulse Expert\t150\t250\tGrip";
   Challenge[3] = "Pulse Master\t350\t500\tLaser";
   Challenge[4] = "Pulse God\t600\t1000\tSilencer";
   Challenge[5] = "Pulse Bronze Commendation\t2500\t10000\tNone";
   Challenge[6] = "Pulse Silver Commendation\t5000\t25000\tNone";
   Challenge[7] = "Pulse Gold Commendation\t10000\t50000\tNone";
   Challenge[8] = "Pulse Titan Commendation\t25000\t75000\tNone";
   Upgrade[1] = "Grip";
   Upgrade[2] = "Laser";
   Upgrade[3] = "Silencer";
   GunName = "Pulse Semi-Automatic Rifle";
   //

   RankRequire = $TWM2::RankRequire["G41"];
   PrestigeRequire = 2;

   casing              = ShellDebris;
   shellExitDir        = "1.0 0.3 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 15.0;
   shellVelocity       = 3.0;

   projectileSpread = 2.0 / 1000.0;

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
   stateTimeoutValue[3] = 0.01;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateScript[3] = "onFire";
   stateEmitterTime[3] = 0.2;
   //stateSound[3] = S3FireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.3;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = ChaingunDryFireSound;
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

datablock ItemData(PulseRifle)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_sniper.dts";
   image        = PulseRifleImage;
   mass         = 1.0;
   elasticity   = 0.0;
   friction     = 0.6;
   pickupRadius = 2;
   pickUpName   = "an Pulse Semi-Automatic Rifle";

   computeCRC = true;
   emap = true;
};

function PulseRifleImage::onFire(%data, %obj, %slot) {
   %p = Parent::onFire(%data, %obj, %slot);
   if(%obj.client.UpgradeOn("Silencer", %data.getName())) {
      //zero sound :p
   }
   else {
      ServerPlay3d(S3FireSound, %obj.getPosition());
   }
}
