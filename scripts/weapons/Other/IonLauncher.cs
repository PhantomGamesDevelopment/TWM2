datablock ItemData(IonLauncherAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_missile.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "Some Ion Launcher Missiles";

   computeCRC = true;
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ItemData(IonLauncher)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_grenade_launcher.dts";
   image = IonLauncherImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   pickUpName = "an Ion Launcher";

   computeCRC = true;
   emap = true;
};

datablock ShapeBaseImageData(IonLauncherImage)
{
   className = WeaponImage;
   shapeFile = "weapon_grenade_launcher.dts";
   item = IonLauncher;
   ammo = IonLauncherAmmo;
   offset = "0 -1 0";
   armThread = lookms;
   emap = true;

   projectile = IonMissile;
   projectileType = seekerprojectile;
   projectileSpread = 1.0 / 1000.0;

   //ClipStuff
   ClipName = "IonLauncherClip";
   ClipPickupName["IonLauncherClip"] = "A Few Ion Missile boxes";
   ShowsClipInHud = 1;
   ClipReloadTime = 6;
   ClipReturn = 2;
   InitialClips = 7;
   
   GunName = "LUX-4 Ion Launcher";
   //
   
   MedalRequire = 1;
   
   isSeeker     = true;
   seekRadius   = 1000;
   maxSeekAngle = 8;
   seekTime     = 0.5;
   minSeekHeat  = 0.1;  // the heat that must be present on a target to lock it.

   // only target objects outside this range
   minTargetingDistance             = 40;

   stateName[0]                     = "Activate";
   stateTransitionOnTimeout[0]      = "ActivateReady";
   stateTimeoutValue[0]             = 2.0;
   stateSequence[0]                 = "Activate";
   stateSound[0]                    = MissileSwitchSound;

   stateName[1]                     = "ActivateReady";
   stateTransitionOnLoaded[1]       = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "CheckWet";

   stateName[3]                     = "Fire";
   stateTransitionOnTimeout[3]      = "Reload";
   stateTimeoutValue[3]             = 0.4;
   stateFire[3]                     = true;
   stateRecoil[3]                   = LightRecoil;
   stateAllowImageChange[3]         = false;
   stateSequence[3]                 = "Fire";
   stateScript[3]                   = "onFire";
   stateSound[3]                    = MissileFireSound;

   stateName[4]                     = "Reload";
   stateTransitionOnNoAmmo[4]       = "NoAmmo";
   stateTransitionOnTimeout[4]      = "Ready";
   stateTimeoutValue[4]             = 4.5;
   stateAllowImageChange[4]         = false;
   stateSequence[4]                 = "Reload";
   stateSound[4]                    = MissileReloadSound;

   stateName[5]                     = "NoAmmo";
   stateTransitionOnAmmo[5]         = "Reload";
   stateSequence[5]                 = "NoAmmo";
   stateTransitionOnTriggerDown[5]  = "DryFire";

   stateName[6]                     = "DryFire";
   stateSound[6]                    = MissileDryFireSound;
   stateTimeoutValue[6]             = 1.0;
   stateTransitionOnTimeout[6]      = "ActivateReady";

   stateName[7]                     = "CheckTarget";
   stateTransitionOnNoTarget[7]     = "Fire";
   stateTransitionOnTarget[7]       = "Fire";

   stateName[8]                     = "CheckWet";
   stateTransitionOnWet[8]          = "DryFire";
   stateTransitionOnNotWet[8]       = "CheckTarget";

   stateName[9]                     = "WetFire";
   stateTransitionOnNoAmmo[9]       = "NoAmmo";
   stateTransitionOnTimeout[9]      = "Reload";
   stateTimeoutValue[9]             = 0.4;
   stateScript[9]                   = "onWetFire";
   stateAllowImageChange[9]         = false;
};

function IonLauncherImage::onFire(%data,%obj,%slot) {
   %p = Parent::onFire(%data, %obj, %slot);
   MissileSet.add(%p);

   %target = %obj.getLockedTarget();
   if(%target)
      %p.setObjectTarget(%target);
   else if(%obj.isLocked())
      %p.setPositionTarget(%obj.getLockedPosition());
   else
      %p.setNoTarget();
}

function IonLauncherImage::onWetFire(%data, %obj, %slot) {
   %p = Parent::onFire(%data, %obj, %slot);
   MissileSet.add(%p);

   %p.setObjectTarget(0);
}
