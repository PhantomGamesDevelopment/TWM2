datablock AudioProfile(PistolFireSound)
{
   filename    = "fx/vehicles/tank_chaingun.wav";
   description = AudioDefault3d;
   preload = true;
};

//--------------------------------------------------------------------------
// Projectile
//--------------------------------------------------------------------------

datablock TracerProjectileData(coltBullet) {
   doDynamicClientHits = true;

   directDamage        = 0.15; // z0dd - ZOD, 9-27-02. Was 0.0825
   directDamageType    = $DamageType::pistol;
   explosion           = "ChaingunExplosion";
   splash              = ChaingunSplash;
   HeadMultiplier      = 1.5;
   LegsMultiplier      = 0.5;
   
   ImageSource         = "PistolImage";

   kickBackStrength  = 5.0;
   sound 				= ChaingunProjectile;

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

datablock ItemData(pistolAmmo)
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

datablock ItemData(pistol) {
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_energy_vehicle.dts";
   image = pistolImage;
   mass = 2;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   pickUpName = "a Colt Pistol";
};

datablock ShapeBaseImageData(PistolImage)
{
   className = WeaponImage;
   shapeFile = "weapon_energy_vehicle.dts";
   item = pistol;
   offset = "0 0 0";
   rotation = "0 1 0 180";
   mass = 4;

   ammo = pistolAmmo;

   projectile = coltbullet;
   projectileType = tracerProjectile;
   projectileSpread = 10.0 / 1000.0;
   
   //ClipStuff
   ClipName = "ColtClip";
   ClipPickupName["ColtClip"] = "Some .77MM Clips";
   ShowsClipInHud = 0;
   ClipReloadTime = 2;
   ClipReturn = 10;
   InitialClips = 950;
   HellClipCount = 10;
   //
   //Challenges
   HasChallenges = 1;
   ChallengeCt = 9;
   Challenge[1] = "Coltee\t25\t100\tGrip";
   Challenge[2] = "Coltist\t50\t150\tLaser";
   Challenge[3] = "Colt Expert\t100\t250\tSilencer";
   Challenge[4] = "Colt Master\t250\t500\tExcessive Duality";
   Challenge[5] = "Colt God\t500\t1000\tHSBullets";
   Challenge[6] = "Colt Bronze Commendation\t1000\t10000\tNone";
   Challenge[7] = "Colt Silver Commendation\t2500\t25000\tNone";
   Challenge[8] = "Colt Gold Commendation\t5000\t50000\tNone";
   Challenge[9] = "Colt Titan Commendation\t15000\t75000\tNone";
   Upgrade[1] = "Grip";
   Upgrade[2] = "Laser";
   Upgrade[3] = "Silencer";
   Upgrade[4] = "Excessive Duality";
   Upgrade[5] = "HSBullets";
   GunName = "Colt Pistol";
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
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.1;
   stateFire[3] = true;
   stateEmitter[3] = "GunFireEffectEmitter";
   stateEmitterNode[3] = "muzzlepoint1";
   stateEmitterTime[3] = 1;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateScript[3] = "onFire";
   stateEmitterTime[3] = 0.2;
   //stateSound[3] = PistolFireSound;

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

datablock ShapeBaseImageData(PistolImage2) : PistolImage {
   offset = "-0.5 0 0";
};

function PistolImage::onMount(%this,%obj,%slot) {
   Parent::onMount(%this, %obj, %slot);
   if(%obj.client.UpgradeOn("Excessive Duality", %this.getName())) {
      %obj.mountImage(PistolImage2, 5);
   }
}

function PistolImage::onUnmount(%this,%obj,%slot) {
   %obj.unmountImage(5);
   Parent::onUnmount(%this, %obj, %slot);
}

function PistolImage::onFire(%data, %obj, %slot) {
   %p = Parent::onFire(%data, %obj, %slot);
   if(%obj.client.UpgradeOn("Silencer", %data.getName())) {
      //zero sound :p
   }
   else if(%obj.client.UpgradeOn("Excessive Duality", %data.getName())) {
      schedule(200, 0, "FirePistolShot", %data, %obj, %slot);
      ServerPlay3d(PistolFireSound, %obj.getPosition());
   }
   else {
      ServerPlay3d(PistolFireSound, %obj.getPosition());
   }
}

function FirePistolShot(%data, %obj, %slot) {
   serverPlay3d("PistolFireSound", %obj.getPosition());

   %vector = %obj.getMuzzleVector(%slot);
   %mp = %obj.getMuzzlePoint(%slot);

   %x = (getRandom() - 0.5) * 2 * 3.1415926 * %data.projectileSpread;
   %y = (getRandom() - 0.5) * 2 * 3.1415926 * %data.projectileSpread;
   %z = (getRandom() - 0.5) * 2 * 3.1415926 * %data.projectileSpread;
   %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
   %newvector = MatrixMulVector(%mat, %vector);

   %p = new (%data.projectileType)() {
      dataBlock        = %data.projectile;
      initialDirection = %newvector;
      initialPosition  = %mp;
      sourceObject     = %obj;
      damageFactor	 = 1;
      sourceSlot       = %slot;
   };
   %p.ImageSource = "PistolImage";
   %p.WeaponImageSource = "PistolImage";
}
