//--------------------------------------------------------------------------
// Projectile
//--------------------------------------------------------------------------

datablock TracerProjectileData(m93Bullet) {
   doDynamicClientHits = true;

   directDamage        = 0.22; // reduced .26
   directDamageType    = $DamageType::m93;
   explosion           = "ChaingunExplosion";
   splash              = ChaingunSplash;
   HeadMultiplier      = 1.5;
   LegsMultiplier      = 0.5;

   kickBackStrength  = 5.0;
   sound 				= ChaingunProjectile;
   
   ImageSource         = "m93Image";

   //dryVelocity       = 425.0;
   dryVelocity       = 2000.0; // z0dd - ZOD, 8-12-02. Was 425.0
   wetVelocity       = 1750.0;
   velInheritFactor  = 0.0;
   fizzleTimeMS      = 3000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 25.0;
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

datablock ItemData(m93Ammo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_chaingun.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some .77mm bullets";

   computeCRC = true;

};

datablock ItemData(m93) {
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_energy_vehicle.dts";
   image = m93Image;
   mass = 2;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   pickUpName = "a m93 Pistol";
};

datablock ShapeBaseImageData(m93Image)
{
   className = WeaponImage;
   shapeFile = "weapon_energy_vehicle.dts";
   item = m93;
   offset = "0 0 0";
   rotation = "0 1 0 180";
   mass = 4;

   ammo = m93Ammo;

   projectile = m93bullet;
   projectileType = tracerProjectile;
   projectileSpread = 6.0 / 1000.0;
   
   //ClipStuff
   ClipName = "m93Clip";
   ClipPickupName["m93Clip"] = "Some .77MM Clips";
   ShowsClipInHud = 0;
   ClipReloadTime = 4;
   ClipReturn = 15;
   InitialClips = 950;
   HellClipCount = 10;
   //
   
   RankRequire = $TWM2::RankRequire["ALSWP"];
   PrestigeRequire = 2;
   
   //Challenges
   HasChallenges = 1;
   ChallengeCt = 9;
   Challenge[1] = "m93 Novice\t25\t100\tNone";
   Challenge[2] = "m93 Hunter\t50\t150\tGrip";
   Challenge[3] = "m93 Expert\t100\t250\tLaser";
   Challenge[4] = "m93 Master\t250\t500\tSilencer";
   Challenge[5] = "m93 God\t500\t1000\tHSBullets";
   Challenge[6] = "m93 Bronze Commendation\t1000\t10000\tNone";
   Challenge[7] = "m93 Silver Commendation\t2500\t25000\tNone";
   Challenge[8] = "m93 Gold Commendation\t5000\t50000\tNone";
   Challenge[9] = "m93 Titan Commendation\t15000\t75000\tNone";
   Upgrade[1] = "Grip";
   Upgrade[2] = "Laser";
   Upgrade[3] = "Silencer";
   Upgrade[4] = "HSBullets";
   GunName = "M93 Pistol";
   //

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = PlasmaSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Fire2";
   stateTimeoutValue[3] = 0.1;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateScript[3] = "onFire";
   stateEmitterTime[3] = 0.1;
   
   stateName[4] = "Fire2";
   stateTransitionOnTimeout[4] = "Fire3";
   stateTimeoutValue[4] = 0.1;
   stateFire[4] = true;
   stateRecoil[4] = LightRecoil;
   stateAllowImageChange[4] = false;
   stateScript[4] = "onFire";
   stateEmitterTime[4] = 0.1;
   
   stateName[5] = "Fire3";
   stateTransitionOnTimeout[5] = "Reload";
   stateTimeoutValue[5] = 0.1;
   stateFire[5] = true;
   stateRecoil[5] = LightRecoil;
   stateAllowImageChange[5] = false;
   stateScript[5] = "onFire";
   stateEmitterTime[5] = 0.1;
   //stateSound[3] = PistolFireSound;

   stateName[6] = "Reload";
   stateTransitionOnNoAmmo[6] = "NoAmmo";
   stateTransitionOnTimeout[6] = "Ready";
   stateTimeoutValue[6] = 0.1;
   stateAllowImageChange[6] = false;
   stateSequence[6] = "Reload";

   stateName[7] = "NoAmmo";
   stateTransitionOnAmmo[7] = "Reload";
   stateSequence[7] = "NoAmmo";
   stateTransitionOnTriggerDown[7] = "DryFire";

   stateName[8]       = "DryFire";
   stateSound[8]      = ChaingunDryFireSound;
   stateTimeoutValue[8]        = 1.5;
   stateTransitionOnTimeout[8] = "NoAmmo";
};

function m93Image::onMount(%this,%obj,%slot) {
   Parent::onMount(%this, %obj, %slot);
}

function m93Image::onFire(%data, %obj, %slot) {
   %p = Parent::onFire(%data, %obj, %slot);
   if(%obj.client.UpgradeOn("Silencer", %data.getName())) {
      //zero sound :p
   }
   else {
      ServerPlay3d(PistolFireSound, %obj.getPosition());
   }
}
