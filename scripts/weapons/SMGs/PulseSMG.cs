datablock TracerProjectileData(PulseSMGBullet) {
   doDynamicClientHits = true;

   directDamage        = 0.08;
   explosion           = "BlasterExplosion";
   splash              = ChaingunSplash;

   directDamageType    = $DamageType::ShrikeBlaster;
   kickBackStrength  = 0.0;

   sound = ShrikeBlasterProjectileSound;
   
   ImageSource         = "PulseSMGImage";

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

   tracerLength    = 30.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = "1.0 1.0 1.0 1.0";
	tracerTex[0]  	 = "special/shrikeBolt";
	tracerTex[1]  	 = "special/shrikeBoltCross";
	tracerWidth     = 0.55;
   crossSize       = 0.99;
   crossViewAng    = 0.990;
   renderCross     = true;

};

//--------------------------------------------------------------------------
// Ammo
//--------------------------------------

datablock ItemData(PulseSMGAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_chaingun.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some PulseSMG ammo";

   computeCRC = true;
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(PulseSMGImage)
{
   className = WeaponImage;
   shapeFile = "weapon_energy_vehicle.dts";
   item      = PulseSMG;
   ammo 	 = PulseSMGAmmo;
   projectile = PulseSMGBullet;
   projectileType = TracerProjectile;
   emap = true;
   offset = "0 0.5 0";  // L/R - F/B - T/B
   rotation = "0 1 0 180";
   mass = 11;

   //ClipStuff
   ClipName = "PulseSMGClip";
   ClipPickupName["PulseSMGClip"] = "some PulseSMG Magazines";
   ShowsClipInHud = 1;
   ClipReloadTime = 4;
   ClipReturn = 45;
   InitialClips = 7;
   //
   HasChallenges = 1;
   ChallengeCt = 9;
   Challenge[1] = "Pulse SMG Killer\t50\t100\tNone";
   Challenge[2] = "Pulse SMG Hunter\t100\t250\tNone";
   Challenge[3] = "Pulse SMG Expert\t250\t500\tGrip";
   Challenge[4] = "Pulse SMG Master\t500\t1000\tLaser";
   Challenge[5] = "Pulse SMG God\t1000\t2000\tSilencer";
   Challenge[6] = "Pulse SMG Bronze Commendation\t2500\t10000\tNone";
   Challenge[7] = "Pulse SMG Silver Commendation\t5000\t25000\tNone";
   Challenge[8] = "Pulse SMG Gold Commendation\t10000\t50000\tNone";
   Challenge[9] = "Pulse SMG Titan Commendation\t25000\t75000\tNone";
   Upgrade[1] = "Grip";
   Upgrade[2] = "Laser";
   Upgrade[3] = "Silencer";
   GunName = "Pulse SMG";

   RankRequire = $TWM2::RankRequire["Pg700"];
   PrestigeRequire = 3;

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

datablock ItemData(PulseSMG)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_energy_vehicle.dts";
   image        = PulseSMGImage;
   mass         = 1.0;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
   pickUpName   = "a Pulse SMG";

   computeCRC = true;
   emap = true;
};

datablock ShapeBaseImageData(PulseSMGBarrelImage) {
   ammo = PulseSMGAmmo;
   shapeFile = "weapon_targeting.dts";
   offset = "0.0 0.0 0.0";
   emap = true;
};

function PulseSMGImage::onMount(%this,%obj,%slot) {
   Parent::onMount(%this, %obj, %slot);
   %obj.mountImage(PulseSMGBarrelImage, 3);
}

function PulseSMGImage::onUnmount(%this,%obj,%slot) {
   Parent::onUnmount(%this, %obj, %slot);
   %obj.unmountImage(3);
   %obj.unmountImage(4);
}

function PulseSMGImage::onFire(%data, %obj, %slot) {
   %p = Parent::onFire(%data, %obj, %slot);
   if(%obj.client.UpgradeOn("Silencer", %data.getName())) {
      //zero sound :p
   }
   else {
      ServerPlay3d(ChaingunFireSound, %obj.getPosition());
   }
}
