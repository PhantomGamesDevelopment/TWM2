//--------------------------------------------------------------------------
// Projectile
//--------------------------------------

datablock TracerProjectileData(meleehit)
{
   doDynamicClientHits = true;

   directDamage        = 0.75;
   directDamageType    = $DamageType::melee;
   explosion           = ChaingunExplosion;
   splash              = ChaingunSplash;
   HeadMultiplier      = 10.0;

   hasDamageRadius     = true;
   indirectDamage      = 0.001;
   damageRadius        = 0.1;
   radiusDamageType    = $DamageType::melee;

   kickBackStrength  = 1500;
   sound             = ChaingunProjectile;
   
   ImageSource         = "MeleeImage";

   dryVelocity       = 50.0; // z0dd - ZOD, 8-12-02. Was 425.0
   wetVelocity       = 50.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 6;
   lifetimeMS        = 60;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 0.1;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.01;
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
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(MeleeImage)
{
   className = WeaponImage;
   shapeFile = "turret_muzzlepoint.dts";
   item = melee;
   projectile = meleehit;
   projectileType = TracerProjectile;

   usesEnergy = true;
   fireEnergy = 20;
   minEnergy = 30;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 1.0;
   stateSequence[0] = "Activate";
   stateSound[0] = BlasterSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.5;
   stateFire[3] = true;
   stateRecoil[3] = NoRecoil;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   stateScript[3] = "onFire";

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";
   
   stateName[6] = "DryFire";
   stateTimeoutValue[6] = 1.0;
   stateSound[6] = BlasterDryFireSound;
   stateTransitionOnTimeout[6] = "Ready";
};

datablock ItemData(melee)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_energy.dts";
   image = meleeImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a gun butt";
};

datablock ShapeBaseImageData(MeleeButt) 
{ 
   shapeFile = "weapon_targeting.dts"; 
   mountPoint = 1; 

   offset = "0.0 0.8 0.55"; // L/R - F/B - T/B
   rotation = "2.0 -2.0 3.0 45"; // L/R - F/B - T/B
}; 

datablock ShapeBaseImageData(MeleeButtSwing) 
{ 
   shapeFile = "weapon_targeting.dts"; 
   mountPoint = 1; 

   offset = "0.0 1.45 -0.4"; // L/R - F/B - T/B
   rotation = "0 0 1 180"; // L/R - F/B - T/B
};

function MeleeImage::onMount(%data, %obj, %node) {
   %obj.mountImage(MeleeButt, 5);
}

function MeleeImage::onUnMount(%data, %obj, %node) {
   %obj.unmountImage(5);
}

function MeleeImage::onFire(%data, %obj, %node) {
   %obj.unmountImage(5);
   %obj.mountImage(MeleeButtSwing, 6);
   %obj.backswing = schedule(300, 0, "swingback", %obj);

   %p = new (TracerProjectile)() { 
	dataBlock = meleehit; 
	initialDirection = %obj.getMuzzleVector(%slot); 
	initialPosition = %obj.getMuzzlePoint(%slot); 
	sourceObject = %obj; 
	sourceSlot = %slot; 
	vehicleObject = %vehicle; 
   };
   MissionCleanup.add(%p); 
}

function swingback(%obj) {
   %obj.unmountImage(6);
   %obj.mountImage(MeleeButt, 5);
} 

function meleehit::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal)
{
	// extra damage for head shot or less for close range shots
	if(!(%targetObject.getType() & ($TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType)) &&
        (%targetObject.getDataBlock().getClassName() $= "PlayerData"))
	{
	   %damLoc = firstWord(%targetObject.getDamageLocation(%position));
	   if(%damLoc $= "head")
	   {
		%targetObject.getOwnerClient().headShot = 1;
		%modifier *= %data.HeadMultiplier;
	   }
	   else
	   {
		%modifier = 1;
		%targetObject.getOwnerClient().headShot = 0;
	   }
	}

    %targetObject.damage(%projectile.sourceObject, %position, %modifier * %data.directDamage, %data.directDamageType);
}
