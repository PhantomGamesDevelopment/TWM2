datablock AudioProfile(DeagleFireSound) {
   filename    = "fx/weapons/cg_hard4.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock TracerProjectileData(DeagleBullet)
{
   doDynamicClientHits = true;

   directDamage        = 0.3; //reduced .6
   directDamageType    = $DamageType::deserteagle;
   explosion           = "ChaingunExplosion";
   splash              = ChaingunSplash;

   kickBackStrength  = 5.0;
   sound 				= ChaingunProjectile;

   ImageSource         = "DeagleImage";

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

datablock ShapeBaseImageData(DeagleImage2)
{
   className = WeaponImage;
   shapeFile = "weapon_sniper.dts";
   offset = "0 0 0"; 	// L/R - F/B - T/B
   rotation = "0 0 0 0"; 		// L/R - F/B - T/B
   emap = true;
};

datablock ItemData(DeagleAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_chaingun.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some 4mm bullets";

   computeCRC = true;

};

datablock ItemData(Deagle)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_energy_vehicle.dts";
   image = DeagleImage;
   mass = 2;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a Desert Eagle";
};

datablock ShapeBaseImageData(DeagleImage) {
   className = WeaponImage;
   shapeFile = "weapon_energy_vehicle.dts";
   item = Deagle;
   offset = "0 0 0";
   rotation = "0 1 0 180";
   mass = 6;

   ammo = DeagleAmmo;

   projectile = DeagleBullet;
   projectileType = tracerProjectile;
   
   ProjectileSpread = 6.0 / 1000.0;   //2.5 spread added
   
   //ClipStuff
   ClipName = "DesertEagleClip";
   ClipPickupName["DesertEagleClip"] = "Some 4MM Clips";
   ShowsClipInHud = 0;
   ClipReloadTime = 5;
   ClipReturn = 7;
   InitialClips = 950;
   //
   //Challenges
   HasChallenges = 1;
   ChallengeCt = 9;
   Challenge[1] = "Eagle Trainee\t25\t100\tNone";
   Challenge[2] = "Eagle Recruit\t50\t150\tNone";
   Challenge[3] = "Eagle Expert\t100\t250\tLaser";
   Challenge[4] = "Eagle Master\t250\t500\tSilencer";
   Challenge[5] = "Eagle Legend\t500\t1000\tScope";
   Challenge[6] = "Eagle Bronze Commendation\t1000\t10000\tNone";
   Challenge[7] = "Eagle Silver Commendation\t2500\t25000\tNone";
   Challenge[8] = "Eagle Gold Commendation\t5000\t50000\tNone";
   Challenge[9] = "Eagle Titan Commendation\t15000\t75000\tNone";
   Upgrade[1] = "Laser";
   Upgrade[2] = "Silencer";
   Upgrade[3] = "Scope";
   GunName = "Desert Eagle Pistol";
   //
   
   RankRequire = $TWM2::RankRequire["DesertEagle"];

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
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.1;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateScript[3] = "onFire";
   stateEmitterTime[3] = 0.2;
   //stateSound[3] = DeagleFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.1;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = ChaingunDryFireSound;
   stateTimeoutValue[6]        = 1.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
};

function DeagleImage::onMount(%this,%obj,%slot) {
   Parent::onMount(%this, %obj, %slot);
   %obj.mountImage(DeagleImage2, 5);
}

function DeagleImage::onUnmount(%this,%obj,%slot) {
   Parent::onUnmount(%this, %obj, %slot);
   %obj.unmountImage(5);
}

function DeagleImage::onFire(%data,%obj,%slot) {
   %p = Parent::onFire(%data,%obj,%slot);
   if(%obj.client.UpgradeOn("Silencer", %data.getName())) {
      //zero sound :p
   }
   else {
      ServerPlay3d(DeagleFireSound, %obj.getPosition());
   }
}
