datablock AudioProfile(S3FireSound)
{
   filename    = "fx/vehicles/tank_chaingun.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock TracerProjectileData(S3Bullet)
{
   doDynamicClientHits = true;

   directDamage        = 0.7;
   directDamageType    = $DamageType::S3;
   explosion           = "ChaingunExplosion";
   splash              = ChaingunSplash;
   HeadMultiplier      = 1.5;
   LegsMultiplier      = 0.35;

   HeadShotKill        = 1;

   kickBackStrength  = 15.0;
   sound 		   = ChaingunProjectile;

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
   
   ImageSource         = "S3RifleImage";

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

datablock ItemData(S3RifleAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_chaingun.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "Some S3 Rifle Bullets";

   computeCRC = true;

};

datablock ShapeBaseImageData(S3RifleImage)
{
   className = WeaponImage;
   shapeFile = "weapon_sniper.dts";
   mass = 10;
   item = S3Rifle;
   ammo = S3RifleAmmo;
   projectile = S3Bullet;
   projectileType = TracerProjectile;
   emap = true;
   
   //ClipStuff
   ClipName = "S3RifleClip";
   ClipPickupName["S3RifleClip"] = "some S3 Clip Cartridges";
   ShowsClipInHud = 1;
   ClipReloadTime = 4;
   ClipReturn = 10;
   InitialClips = 6;
   //
   //Challenges
   HasChallenges = 1;
   ChallengeCt = 9;
   Challenge[1] = "S3 Killer\t50\t100\tNone";
   Challenge[2] = "S3 Extremist\t100\t150\tGrip";
   Challenge[3] = "S3 Expert\t250\t250\tLaser";
   Challenge[4] = "S3 Master\t500\t500\tSilencer";
   Challenge[5] = "S3 God\t1500\t1000\tBurst Clip";
   Challenge[6] = "S3 Bronze Commendation\t5000\t10000\tNone";
   Challenge[7] = "S3 Silver Commendation\t10000\t25000\tNone";
   Challenge[8] = "S3 Gold Commendation\t25000\t50000\tNone";
   Challenge[9] = "S3 Titan Commendation\t50000\t75000\tNone";
   Upgrade[1] = "Grip";
   Upgrade[2] = "Laser";
   Upgrade[3] = "Silencer";
   Upgrade[4] = "Burst Clip";
   GunName = "S3 Combat Rifle";
   //

   casing              = ShellDebris;
   shellExitDir        = "1.0 0.3 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 15.0;
   shellVelocity       = 3.0;

   projectileSpread = 3.0 / 1000.0;

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
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.01;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateScript[3] = "onFire";
   stateEmitterTime[3] = 0.2;
   //stateSound[3] = S3FireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.9;
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
   stateSound[7]      = PlasmaFireWetSound;
   stateTimeoutValue[7]        = 1.0;
   stateTransitionOnTimeout[7] = "Ready";

   stateName[8]               = "CheckWet";
   stateTransitionOnWet[8]    = "WetFire";
   stateTransitionOnNotWet[8] = "Fire";
};

datablock ItemData(S3Rifle)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_sniper.dts";
   image        = S3RifleImage;
   mass         = 1.0;
   elasticity   = 0.0;
   friction     = 0.6;
   pickupRadius = 2;
   pickUpName   = "an S3 Combat Rifle";

   computeCRC = true;
   emap = true;
};

function S3RifleImage::OnFire(%data, %obj, %slot) {
   if(%obj.client.UpgradeOn("Burst Clip", %data.getName())) {
   
      %spread = %data.projectileSpread;
      if(%obj.client !$= "") {
         if(%obj.client.IsActivePerk("Advanced Grip")) {
            %spread = %spread / 2.5;
         }
      }
   
      %vec2 = %obj.getMuzzleVector(%slot);
      %x = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
      %y = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
      %z = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
      %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
      %vector2 = MatrixMulVector(%mat, %vec2);
      
      %vec3 = %obj.getMuzzleVector(%slot);
      %x = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
      %y = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
      %z = (getRandom() - 0.5) * 2 * 3.1415926 * %spread;
      %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
      %vector3 = MatrixMulVector(%mat, %vec3);
   
      %p1 = Parent::OnFire(%data, %obj, %slot);

      %p2 = Schedule(100, 0, "spawnprojectile", %data.Projectile, %data.ProjectileType, vectorAdd(%obj.getPosition(), "-.45 0 1.4"), %vector2, %obj);
      %p2.WeaponImageSource = "S3RifleImage";
      schedule(100, 0, "ServerPlay3D", "S3FireSound", %obj.getPosition());

      %p3 = Schedule(250, 0, "spawnprojectile", %data.Projectile, %data.ProjectileType, vectorAdd(%obj.getPosition(), "-.45 0 1.4"), %vector3, %obj);
      %p3.WeaponImageSource = "S3RifleImage";
      schedule(250, 0, "ServerPlay3D", "S3FireSound", %obj.getPosition());
   }
   else if(%obj.client.UpgradeOn("Silencer", %data.getName())) {
      %p = Parent::OnFire(%data, %obj, %slot);
   }
   else {
      %p = Parent::OnFire(%data, %obj, %slot);
      serverPlay3d(S3FireSound, %obj.getPosition());
   }
}
