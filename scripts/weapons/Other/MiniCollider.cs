datablock ShapeBaseImageData(MiniColliderCannonImage) {
   className = WeaponImage;
   shapeFile = "turret_mortar_large.dts";
   item      = MiniColliderCannon;
   usesenergy = true;

   projectile = MiniColliderShell;
   projectileType = GrenadeProjectile;

   emap = true;

   MedalRequire = 1;
   
   HasChallenges = 1;
   ChallengeCt = 8;
   Challenge[1] = "Collider Hunter\t100\t100\tNone";
   Challenge[2] = "Collider Expert\t250\t500\tNone";
   Challenge[3] = "Collider Master\t500\t1000\tDouble Burst";
   Challenge[4] = "Collider God\t1000\t2000\tTriple Burst";
   Challenge[5] = "Collider Bronze Commendation\t2500\t10000\tNone";
   Challenge[6] = "Collider Silver Commendation\t5000\t25000\tNone";
   Challenge[7] = "Collider Gold Commendation\t10000\t50000\tNone";
   Challenge[8] = "Collider Titan Commendation\t25000\t75000\tNone";
   Upgrade[1] = "Double Burst";
   Upgrade[2] = "Triple Burst";
   GunName = "PRTCL-995 MCC";

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = MortarReloadSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 4.6;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   stateSound[3] = CentaurArtilleryFireSound;
   stateScript[3] = "onFire";

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateSound[4] = MortarSwitchSound;
   stateEjectShell[4]       = true;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6] = "DryFire";
   stateTimeoutValue[6] = 0.3;
   stateSound[6] = ChaingunDryFireSound;
   stateTransitionOnTimeout[6] = "Ready";
};

//item//
datablock ItemData(MiniColliderCannon) {
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "turret_mortar_large.dts";
   image        = MiniColliderCannonImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
   pickUpName   = "a PRTCL-995 Mini Collider Cannon";

   computeCRC = true;
   emap = true;
};

function MiniColliderCannonImage::onFire(%data, %obj, %slot) {
   %vector = %obj.getMuzzleVector(%slot);
   %mp = %obj.getMuzzlePoint(%slot);
   if(%obj.client.UpgradeOn("Double Burst", %data.getName())) {
      %p1 = Parent::OnFire(%data, %obj, %slot);
      %p2 = Schedule(100, 0, "spawnprojectile", %data.Projectile, %data.ProjectileType,
         %obj.getMuzzlePoint(%slot), %vector, %obj);
      %p2.WeaponImageSource = "MiniColliderCannonImage";
      schedule(100, 0, "ServerPlay3D", "CentaurArtilleryFireSound", %obj.getPosition());
   }
   else if(%obj.client.UpgradeOn("Triple Burst", %data.getName())) {
      %p1 = Parent::OnFire(%data, %obj, %slot);
      %p2 = Schedule(100, 0, "spawnprojectile", %data.Projectile, %data.ProjectileType,
         %obj.getMuzzlePoint(%slot), %vector, %obj);
      %p2.WeaponImageSource = "MiniColliderCannonImage";
      schedule(100, 0, "ServerPlay3D", "CentaurArtilleryFireSound", %obj.getPosition());
      %p3 = Schedule(200, 0, "spawnprojectile", %data.Projectile, %data.ProjectileType,
         %obj.getMuzzlePoint(%slot), %vector, %obj);
      %p3.WeaponImageSource = "MiniColliderCannonImage";
      schedule(100, 0, "ServerPlay3D", "CentaurArtilleryFireSound", %obj.getPosition());
   }
   else {
      %p = Parent::OnFire(%data, %obj, %slot);
   }
}
