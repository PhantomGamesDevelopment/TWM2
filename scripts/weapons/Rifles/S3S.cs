datablock TracerProjectileData(S3SBullet) {
   doDynamicClientHits = true;

   directDamage        = 0.45;
   directDamageType    = $DamageType::S3;
   explosion           = "ChaingunExplosion";
   splash              = ChaingunSplash;
   HeadMultiplier      = 1.5;
   LegsMultiplier      = 0.35;

   HeadShotKill        = 1;

   kickBackStrength  = 15.0;
   sound 		   = ChaingunProjectile;
   
   ImageSource         = "S3SRifleImage";

   dryVelocity       = 3000.0;
   wetVelocity       = 2000.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 1000;
   lifetimeMS        = 1000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 20.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.09;
   crossSize       = 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;

   decalData[0] = ChaingunDecal1;
   decalData[1] = ChaingunDecal2;
   decalData[2] = ChaingunDecal3;
   decalData[3] = ChaingunDecal4;
   decalData[4] = ChaingunDecal5;
   decalData[5] = ChaingunDecal6;
};

datablock ItemData(S3SRifleAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_chaingun.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "Some S3S Rifle Bullets";

   computeCRC = true;

};

datablock ShapeBaseImageData(S3SRifleImage)
{
   className = WeaponImage;
   shapeFile = "weapon_sniper.dts";
   mass = 10;
   item = S3SRifle;
   ammo = S3SRifleAmmo;
   projectile = S3SBullet;
   projectileType = TracerProjectile;
   emap = true;
   
   //ClipStuff
   ClipName = "S3SRifleClip";
   ClipPickupName["S3SRifleClip"] = "some S3S Clip Cartridges";
   ShowsClipInHud = 1;
   ClipReloadTime = 4;
   ClipReturn = 30;
   InitialClips = 6;
   //

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
   stateTransitionOnTimeout[3] = "Fire2";
   stateTimeoutValue[3] = 0.04;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateScript[3] = "onFire";
   stateEmitterTime[3] = 0.2;
   stateSound[3] = S3FireSound;
   
   stateName[4] = "Fire2";
   stateTransitionOnTimeout[4] = "Fire3";
   stateTimeoutValue[4] = 0.04;
   stateFire[4] = true;
   stateRecoil[4] = LightRecoil;
   stateAllowImageChange[4] = false;
   stateScript[4] = "onFire";
   stateEmitterTime[4] = 0.2;
   stateSound[4] = S3FireSound;

   stateName[5] = "Fire3";
   stateTransitionOnTimeout[5] = "Reload";
   stateTimeoutValue[5] = 0.04;
   stateFire[5] = true;
   stateRecoil[5] = LightRecoil;
   stateAllowImageChange[5] = false;
   stateScript[5] = "onFire";
   stateEmitterTime[5] = 0.2;
   stateSound[5] = S3FireSound;

   stateName[6] = "Reload";
   stateTransitionOnNoAmmo[6] = "NoAmmo";
   stateTransitionOnTimeout[6] = "Ready";
   stateTimeoutValue[6] = 0.9;
   stateAllowImageChange[6] = false;
   stateSequence[6] = "Reload";

   stateName[7] = "NoAmmo";
   stateTransitionOnAmmo[7] = "Reload";
   stateSequence[7] = "NoAmmo";
   stateTransitionOnTriggerDown[7] = "DryFire";

   stateName[8]       = "DryFire";
   stateSound[8]      = ChaingunDryFireSound;
   stateTimeoutValue[8]        = 1.0;
   stateTransitionOnTimeout[8] = "NoAmmo";

   stateName[9]       = "WetFire";
   stateSound[9]      = PlasmaFireWetSound;
   stateTimeoutValue[9]        = 1.0;
   stateTransitionOnTimeout[9] = "Ready";

   stateName[10]               = "CheckWet";
   stateTransitionOnWet[10]    = "WetFire";
   stateTransitionOnNotWet[10] = "Fire";
};

datablock ItemData(S3SRifle)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_sniper.dts";
   image        = S3SRifleImage;
   mass         = 1.0;
   elasticity   = 0.0;
   friction     = 0.6;
   pickupRadius = 2;
   pickUpName   = "an S3S Combat Rifle";

   computeCRC = true;
   emap = true;
};
