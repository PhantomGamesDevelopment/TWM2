datablock TracerProjectileData(Pg700Bullet)
{
   doDynamicClientHits = true;
   
   isBullet = true;
   isPistol = false;
   isAntiAir = false;
   isAntiArmor = false;

   directDamage        = 0.09;
   directDamageType    = $DamageType::Pg700;
   explosion           = "ChaingunExplosion";
   splash              = ChaingunSplash;
   HeadMultiplier      = 1.25;
   LegsMultiplier      = 0.75;

   kickBackStrength  = 15.0;
   sound 				= ChaingunProjectile;
   
   ImageSource         = "Pg700Image";

   dryVelocity       = 1750.0;
   wetVelocity       = 1500.0;
   velInheritFactor  = 0.0;
   fizzleTimeMS      = 3000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 9.0;
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

//--------------------------------------------------------------------------
// Ammo
//--------------------------------------

datablock ItemData(Pg700Ammo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_chaingun.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some Pg700 ammo";

   computeCRC = true;
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(Pg700Image)
{
   className = WeaponImage;
   shapeFile = "weapon_energy_vehicle.dts";
   item      = Pg700;
   ammo 	 = Pg700Ammo;
   projectile = Pg700Bullet;
   projectileType = TracerProjectile;
   emap = true;
   offset = "0 0.5 0";  // L/R - F/B - T/B
   rotation = "0 1 0 180";
   mass = 11;
   
   //ClipStuff
   ClipName = "Pg700Clip";
   ClipPickupName["Pg700Clip"] = "some Pg700 Magazines";
   ShowsClipInHud = 1;
   ClipReloadTime = 4;
   ClipReturn = 45;
   InitialClips = 7;
   //
   HasChallenges = 1;
   ChallengeCt = 9;
   Challenge[1] = "Pg700 Killer\t50\t100\tNone";
   Challenge[2] = "Pg700 Hunter\t100\t250\tNone";
   Challenge[3] = "Pg700 Expert\t250\t500\tGrip";
   Challenge[4] = "Pg700 Master\t500\t1000\tLaser";
   Challenge[5] = "Pg700 God\t1000\t2000\tSilencer";
   Challenge[6] = "Pg700 Bronze Commendation\t2500\t10000\tNone";
   Challenge[7] = "Pg700 Silver Commendation\t5000\t25000\tNone";
   Challenge[8] = "Pg700 Gold Commendation\t10000\t50000\tNone";
   Challenge[9] = "Pg700 Titan Commendation\t25000\t75000\tNone";
   Upgrade[1] = "Grip";
   Upgrade[2] = "Laser";
   Upgrade[3] = "Silencer";
   GunName = "Pg700 Assault Rifle";
   
   RankRequire = $TWM2::RankRequire["Pg700"];


   casing              = ShellDebris;
   shellExitDir        = "1.0 0.3 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 15.0;
   shellVelocity       = 3.0;

   projectileSpread = 3.0 / 1000.0;

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
   stateTimeoutValue[3]          = 0.01;
   stateWaitForTimeout[3]        = false;
   stateTransitionOnTimeout[3]   = "Fire";
   stateTransitionOnTriggerUp[3] = "Spindown";

   //--------------------------------------
   stateName[4]             = "Fire";
   stateSequence[4]            = "Fire";
   stateSequenceRandomFlash[4] = true;
   stateSpinThread[4]       = FullSpeed;
  //stateSound[4]            = ChaingunFireSound;
   stateRecoil[4]           = LightRecoil;
   stateAllowImageChange[4] = false;
   stateScript[4]           = "onFire";
   stateFire[4]             = true;
   stateEjectShell[4]       = true;
   //
   stateTimeoutValue[4]          = 0.08;
   stateTransitionOnTimeout[4]   = "Fire";
   stateTransitionOnTriggerUp[4] = "Spindown";
   stateTransitionOnNoAmmo[4]    = "EmptySpindown";

   //--------------------------------------
   stateName[5]       = "Spindown";
   stateSound[5]      = ChaingunSpinDownSound;
   stateSpinThread[5] = SpinDown;
   //
   stateTimeoutValue[5]            = 0.05;
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

datablock ItemData(Pg700)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_energy_vehicle.dts";
   image        = Pg700Image;
   mass         = 1.0;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
   pickUpName   = "a Pg700 SMG";

   computeCRC = true;
   emap = true;
};

datablock ShapeBaseImageData(Pg700BarrelImage) {
   ammo = Pg700Ammo;
   shapeFile = "weapon_targeting.dts";
   offset = "0.0 0.0 0.0";
   emap = true;
};

datablock ShapeBaseImageData(Pg700ScopeImage) {
   className = WeaponImage;
   shapeFile = "weapon_targeting.dts";
   offset = "0.0 1.4 0.4";
   rotation = "180 0 0 90";

   ammo = Pg700Ammo;

   emap = true;
};

function Pg700Image::onMount(%this,%obj,%slot) {
   Parent::onMount(%this, %obj, %slot);
   %obj.mountImage(Pg700BarrelImage, 3);
   %obj.mountImage(Pg700ScopeImage, 4);
}

function Pg700Image::onUnmount(%this,%obj,%slot) {
   Parent::onUnmount(%this, %obj, %slot);
   %obj.unmountImage(3);
   %obj.unmountImage(4);
}

function Pg700Image::onFire(%data, %obj, %slot) {
   %p = Parent::onFire(%data, %obj, %slot);
   if(%obj.client.UpgradeOn("Silencer", %data.getName())) {
      //zero sound :p
   }
   else {
      ServerPlay3d(ChaingunFireSound, %obj.getPosition());
   }
}
