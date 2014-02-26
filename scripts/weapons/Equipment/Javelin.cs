datablock SeekerProjectileData(JavelinMissile) {
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   hasDamageRadius     = true;
   indirectDamage      = 1.8; //Requires Lock
   damageRadius        = 18.0;
   radiusDamageType    = $DamageType::Missile;
   kickBackStrength    = 2000;
   
   ImageSource         = "JavelinImage";

   explosion           = "SatchelMainExplosion";
   splash              = MissileSplash;
   velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
                                 // is cranked up to full so the missile doesn't start
                                 // out behind the player when the player is moving
                                 // very quickly - bramage

   baseEmitter         = MissileSmokeEmitter;
   delayEmitter        = MissileFireEmitter;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = 30000;
   muzzleVelocity      = 10.0;
   maxVelocity         = 150.0;
   turningSpeed        = 110.0;
   acceleration        = 350.0;

   proximityRadius     = 3;

   terrainAvoidanceSpeed         = 180;
   terrainScanAhead              = 25;
   terrainHeightFail             = 12;
   terrainAvoidanceRadius        = 100;

   flareDistance = 200;
   flareAngle    = 30;

   sound = MissileProjectileSound;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.2 0.05 0";

   useFlechette = true;
   flechetteDelayMs = 550;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = false;
};



//--------------------------------------------------------------------------
// Ammo
//--------------------------------------

datablock ItemData(JavelinAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_missile.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some javelin rockets";

   computeCRC = true;

};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ItemData(Javelin)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_mortar.dts";
   image = JavelinImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a javelin launcher";

   computeCRC = true;
   emap = true;
};

datablock ShapeBaseImageData(JavelinImage) {
   className = WeaponImage;
   shapeFile = "weapon_mortar.dts";
   item = Javelin;
   ammo = JavelinAmmo;
   offset = "0 0 0";
   armThread = lookms;
   emap = true;

   projectile = JavelinMissile;
   projectileType = SeekerProjectile;

   isSeeker     = true;
   seekRadius   = 700;
   maxSeekAngle = 8;
   seekTime     = 0.5;
   minSeekHeat  = 0.01;  // the heat that must be present on a target to lock it.
   
   //ClipStuff
   ClipName = "JavelinClip";
   ClipPickupName["JavelinClip"] = "A Few Javelin Missiles";
   ShowsClipInHud = 1;
   ClipReloadTime = 6;
   ClipReturn = 1;
   InitialClips = 2;
   //
   HasChallenges = 1;
   ChallengeCt = 7;
   Challenge[1] = "Javelin Hunter\t100\t500\tLaser";
   Challenge[2] = "Javelin Expert\t250\t1000\tNone";
   Challenge[3] = "Javelin Mastery\t500\t2500\tNone";
   Challenge[4] = "Javelin Bronze Commendation\t1000\t10000\tNone";
   Challenge[5] = "Javelin Silver Commendation\t2500\t25000\tNone";
   Challenge[6] = "Javelin Gold Commendation\t5000\t50000\tNone";
   Challenge[7] = "Javelin Titan Commendation\t10000\t75000\tNone";
   Upgrade[1] = "Laser";
   GunName = "Javelin";

   RankRequire = $TWM2::RankRequire["Javelin"];

   // only target objects outside this range
   minTargetingDistance             = 80;

   stateName[0]                     = "Activate";
   stateTransitionOnTimeout[0]      = "ActivateReady";
   stateTimeoutValue[0]             = 0.5;
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
   stateTimeoutValue[4]             = 2.5;
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
   stateTransitionOnNoTarget[7]     = "DryFire";
   stateTransitionOnTarget[7]       = "Fire";

   stateName[8]                     = "CheckWet";
   stateTransitionOnWet[8]          = "WetFire";
   stateTransitionOnNotWet[8]       = "CheckTarget";

   stateName[9]                     = "WetFire";
   stateTransitionOnNoAmmo[9]       = "NoAmmo";
   stateTransitionOnTimeout[9]      = "Reload";
   stateSound[9]                    = MissileFireSound;
   stateRecoil[3]                   = LightRecoil;
   stateTimeoutValue[9]             = 0.4;
   stateScript[9]                   = "onWetFire";
   stateAllowImageChange[9]         = false;
};

function JavelinImage::onFire(%data,%obj,%slot) {
   %obj.decInventory(%data.ammo,1);
   if(%obj.inv[%data.ammo] == 0) { //Added Phantom139, TWM2
      AttemptReload(%data, %obj, %slot);
   }

   %p = new (SeekerProjectile)() {
      dataBlock        = JavelinMissile;
      initialDirection = "0 0 10";
      initialPosition  = %obj.getPosition();
      damageFactor     = 1;
      sourceObject     = %obj;
   };
   %p.WeaponImageSource = %data.getName();
   
   MissileSet.add(%p);

   %target = %obj.getLockedTarget();
   if(%target) {
      //go up first!!!
      //%p.setPositionTarget(vectorAdd(%obj.getPosition(), "0 0 200"));
      %p.schedule(3500, "setObjectTarget", %target);
      schedule(3501, 0, "SetMissileTargeted", %target, %p);
   }
   else if(%obj.isLocked()) {
      //%p.setPositionTarget(vectorAdd(%obj.getPosition(), "0 0 200"));
      //echo("Javelin Lock: "@%obj.getLockedPosition()@"");
      %p.JavBeac = new BeaconObject() {
          dataBlock = "BomberBeacon";
          beaconType = "vehicle";
          position = %obj.getLockedPosition();
      };
      %p.schedule(3500, "setObjectTarget", %p.JavBeac);
      schedule(3500, 0, "SetMissileTargeted", %p.JavBeac, %p);
      %p.JavBeac.schedule(10000, "Delete");
   }
   else {
      //no lock, this missile must self destruct...
      %p.setNoTarget();
      %p.getDatablock().onExplode(%p, %p.getPosition(), 1);
   }
}

