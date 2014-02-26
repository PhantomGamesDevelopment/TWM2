datablock TracerProjectileData(M4A1Bullet) {
   doDynamicClientHits = true;

   directDamage        = 0.09;
   directDamageType    = $DamageType::M4A1;
   explosion           = "ChaingunExplosion";
   splash              = ChaingunSplash;
   HeadMultiplier      = 1.25;
   LegsMultiplier      = 0.75;

   kickBackStrength  = 15.0;
   sound 				= ChaingunProjectile;
   
   ImageSource         = "M4A1Image";

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

datablock ItemData(M4A1Ammo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_chaingun.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some M4A1 ammo";

   computeCRC = true;
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(M4A1Image)
{
   className = WeaponImage;
   shapeFile = "weapon_energy_vehicle.dts";
   item      = M4A1;
   ammo 	 = M4A1Ammo;
   projectile = M4A1Bullet;
   projectileType = TracerProjectile;
   emap = true;
   offset = "0 0.5 0";  // L/R - F/B - T/B
   rotation = "0 1 0 180";
   mass = 11;
   
   //ClipStuff
   ClipName = "M4A1Clip";
   ClipPickupName["M4A1Clip"] = "some M4A1 Magazines";
   ShowsClipInHud = 1;
   ClipReloadTime = 4;
   ClipReturn = 30;
   InitialClips = 7;
   //
   HasChallenges = 1;
   ChallengeCt = 11;
   Challenge[1] = "M4A1 Killer\t50\t100\tGrip";
   Challenge[2] = "M4A1 Hunter\t100\t250\tLaser";
   Challenge[3] = "M4A1 Expert\t250\t500\tShotgun Attachment";
   Challenge[4] = "M4A1 Master\t500\t1000\tGrenade Launcher Attachment";
   Challenge[5] = "M4A1 Legend\t750\t1500\tMini-Launcher Attachment";
   Challenge[6] = "M4A1 God\t1000\t2000\tSilencer";
   Challenge[7] = "M4A1 Supremacy\t2500\t5000\tUnlimited Attachment Ammo";
   Challenge[8] = "M4A1 Bronze Commendation\t5000\t10000\tNone";
   Challenge[9] = "M4A1 Silver Commendation\t12500\t25000\tNone";
   Challenge[10] = "M4A1 Gold Commendation\t25000\t50000\tNone";
   Challenge[11] = "M4A1 Titan Commendation\t50000\t75000\tNone";
   Upgrade[1] = "Grip";
   Upgrade[2] = "Laser";
   Upgrade[3] = "Shotgun Attachment";
   Upgrade[4] = "Grenade Launcher Attachment";
   Upgrade[5] = "Mini-Launcher Attachment";
   Upgrade[6] = "Silencer";
   GunName = "M4A1 Assault Rifle";
   
   RankRequire = $TWM2::RankRequire["M4A1"];


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

datablock ItemData(M4A1)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_energy_vehicle.dts";
   image        = M4A1Image;
   mass         = 1.0;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
   pickUpName   = "an M4A1";

   computeCRC = true;
   emap = true;
};

function M4A1::onInventory(%this,%obj,%amount) {
   Parent::onInventory(%this, %obj, %amount);
   %obj.canUseM4A1Attachment = 1;
   if(%obj.client.UpgradeOn("Shotgun Attachment", M4A1Image)) {
      %obj.M4A1ShotgunClip = 5;
   }
   if(%obj.client.UpgradeOn("Grenade Launcher Attachment", M4A1Image)) {
      %obj.M4A1GLClip = 3;
   }
   if(%obj.client.UpgradeOn("Mini-Launcher Attachment", M4A1Image)) {
      %obj.M4A1MLClip = 3;
   }
}

datablock ShapeBaseImageData(M4A1BarrelImage) {
   ammo = M4A1Ammo;
   shapeFile = "weapon_targeting.dts";
   offset = "0.0 0.0 0.0";
   emap = true;
};

datablock ShapeBaseImageData(M4A1ScopeImage) {
   className = WeaponImage;
   shapeFile = "weapon_targeting.dts";
   offset = "0.0 1.4 0.4";
   rotation = "180 0 0 90";

   ammo = M4A1Ammo;

   emap = true;
};

function M4A1Image::onMount(%this,%obj,%slot) {
   Parent::onMount(%this, %obj, %slot);
   %obj.mountImage(M4A1BarrelImage, 3);
   %obj.mountImage(M4A1ScopeImage, 4);
}

function M4A1Image::onUnmount(%this,%obj,%slot) {
   Parent::onUnmount(%this, %obj, %slot);
   %obj.unmountImage(3);
   %obj.unmountImage(4);
}

function M4A1Image::onFire(%data, %obj, %slot) {
   %p = Parent::onFire(%data, %obj, %slot);
   if(%obj.client.UpgradeOn("Silencer", %data.getName())) {
      //zero sound :p
   }
   else {
      ServerPlay3d(ChaingunFireSound, %obj.getPosition());
   }
}

function M4A1RefreshAttachment(%obj) {
   %obj.canUseM4A1Attachment = 1;
}
