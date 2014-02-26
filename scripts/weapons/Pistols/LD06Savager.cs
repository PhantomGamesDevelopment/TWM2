datablock ItemData(LD06SavagerAmmo) {
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_chaingun.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some LD06Savager rounds";

   computeCRC = true;

};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(LD06SavagerImage) {
   className = WeaponImage;
   shapeFile = "weapon_plasma.dts";
   item      = LD06Savager;
   ammo 	 = LD06SavagerAmmo;
   projectile = Wp400Pellet;
   projectileType = TracerProjectile;
   emap = true;
   mass = 20;

   casing              = ShellDebris;
   shellExitDir        = "0.5 0.0 1.0"; // L/R - F/B - T/B
   shellExitOffset     = "0.15 -0.51 -0.1"; // L/R - F/B - T/B
   shellExitVariance   = 10.0;
   shellVelocity       = 4.0;

   //ClipStuff
   ClipName = "LD06SavagerClip";
   ClipPickupName["LD06SavagerClip"] = "some LD06Savager Clip Boxes";
   ShowsClipInHud = 0;
   ClipReloadTime = 5;
   ClipReturn = 10;
   InitialClips = 999;
   HellClipCount = 7;
   //
   HasChallenges = 1;
   ChallengeCt = 8;
   Challenge[1] = "LD06 Killer\t50\t100\tNone";
   Challenge[2] = "LD06 Expert\t100\t250\tNone";
   Challenge[3] = "LD06 Master\t250\t500\tNone";
   Challenge[4] = "LD06 God\t500\t1000\tSilencer";
   Challenge[5] = "LD06 Bronze Commendation\t1000\t10000\tNone";
   Challenge[6] = "LD06 Silver Commendation\t2500\t25000\tNone";
   Challenge[7] = "LD06 Gold Commendation\t5000\t50000\tNone";
   Challenge[8] = "LD06 Titan Commendation\t15000\t75000\tNone";
   Upgrade[1] = "Silencer";
   GunName = "LD06 Savager";
   
   
   MedalRequire = 1;
   

   projectileSpread = 10.0 / 1000.0;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.3;
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
   stateEjectShell[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateScript[3] = "onFire";
   stateSound[3] = SniperFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.6;
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
   stateSound[7]      = ChaingunDryFireSound;
   stateTimeoutValue[7]        = 1.0;
   stateTransitionOnTimeout[7] = "Ready";

   stateName[8]               = "CheckWet";
   stateTransitionOnWet[8]    = "WetFire";
   stateTransitionOnNotWet[8] = "Fire";
};

datablock ItemData(LD06Savager)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_plasma.dts";
   image        = LD06SavagerImage;
   mass         = 1.0;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
   pickUpName   = "a LD06Savager";

   computeCRC = true;
   emap = true;
};

function LD06SavagerImage::onMount(%this,%obj,%slot) {
   Parent::onMount(%this, %obj, %slot);
}

function LD06SavagerImage::onFire(%data,%obj,%slot) {
    %obj.applyKick(-250);
    %obj.decInventory(%data.ammo,1);
    
    serverPlay3D(ShotgunFireSound, %obj.getPosition());

    %vector = %obj.getMuzzleVector(%slot);
    %mp = %obj.getMuzzlePoint(%slot);

	for (%i=0; %i < 5; %i++) {
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
      %p.WeaponImageSource = %data.getName();
      %p.ImageSource         = "LD06SavagerImage";
    }
    if(%obj.inv[LD06SavagerAmmo] == 0) { //Added Phantom139, TWM2
       AttemptReload(%data, %obj, %slot);
    }
}
