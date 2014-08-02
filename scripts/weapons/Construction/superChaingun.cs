//--------------------------------------
// SuperChaingun
//--------------------------------------

//--------------------------------------------------------------------------
// Projectile
//--------------------------------------

datablock TracerProjectileData(SuperChaingunBullet)
{
   doDynamicClientHits = true;

   directDamage        = 0.125 * 4;
   directDamageType    = $DamageType::SuperChaingun;
   explosion           = ChaingunExplosion;
   splash              = ChaingunSplash;

   hasDamageRadius     = true;
   indirectDamage      = 0.45 * 400;
   damageRadius        = 4.0 * 2;
   radiusDamageType    = $DamageType::SuperChaingun;

   kickBackStrength  = 1750;
   sound             = ChaingunProjectile;

   dryVelocity       = 425.0;
   wetVelocity       = 100.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 3000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 15.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.10;
   crossSize       = 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;

   decalData[0] = ChaingunDecal1;
   decalData[1] = ChaingunDecal2;
   decalData[2] = ChaingunDecal3;
   decalData[3] = ChaingunDecal4;
   decalData[4] = ChaingunDecal5;
   decalData[5] = ChaingunDecal6;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.5 0.5 0.175";
};


//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(SuperChaingunImage)
{
   className = WeaponImage;
   shapeFile = "weapon_chaingun.dts";
   item      = SuperChaingun;

   projectile = SuperChaingunBullet;
   projectileType = TracerProjectile;
   usesEnergy = 1;

   emap = true;

   casing              = ShellDebris;
   shellExitDir        = "0.3 0.5 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 15.0;
   shellVelocity       = 4.0;

   projectileSpread = 1.0 / 1000.0;

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
   stateTimeoutValue[3]          = 0.0;
   stateWaitForTimeout[3]        = false;
   stateTransitionOnTimeout[3]   = "Fire";
   stateTransitionOnTriggerUp[3] = "Spindown";

   //--------------------------------------
   stateName[4]             = "Fire";
   stateSequence[4]            = "Fire";
   stateSequenceRandomFlash[4] = true;
   stateSpinThread[4]       = FullSpeed;
   stateSound[4]            = ChaingunFireSound;
   //stateRecoil[4]           = LightRecoil;
   stateAllowImageChange[4] = false;
   stateScript[4]           = "onFire";
   stateFire[4]             = true;
   stateEjectShell[4]       = true;
   //
   stateTimeoutValue[4]          = 0.01;
   stateTransitionOnTimeout[4]   = "Fire";
   stateTransitionOnTriggerUp[4] = "Spindown";
   stateTransitionOnNoAmmo[4]    = "EmptySpindown";

   //--------------------------------------
   stateName[5]       = "Spindown";
   stateSound[5]      = ChaingunSpinDownSound;
   stateSpinThread[5] = SpinDown;
   //
   stateTimeoutValue[5]            = 0.0;
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

datablock ItemData(SuperChaingun)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_chaingun.dts";
   image        = SuperChaingunImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
   pickUpName   = "a super chaingun";

   computeCRC = true;
   emap = true;
};

function SuperChaingunImage::onFire(%data,%obj,%slot) {
	if (%obj.superChaingunMode == 1) {
		%pos = %obj.getMuzzlePoint(%slot);
		%vec = %obj.getMuzzleVector(%slot);
		%res = containerRayCast(%pos,vectorAdd(%pos,vectorScale(%vec,2000)), $TypeMasks::PlayerObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::StaticObjectType,%obj);
		if (%res)
			%hitLoc = getWords(%res,1,3);
		else
			%hitLoc = vectorAdd(%pos,vectorScale(%vec,2000));
		%p = discharge(%pos,%vec);
		%p.setEnergyPercentage(1);
		createLifeLight(%hitLoc,1,1000);
		addToShock(%p);
		%p.schedule(1000,"delete");
		zap(0,25,%hitLoc);
		%obj.decInventory(%data.ammo,1);
	}
	else if (%obj.superChaingunMode > 1)
                {
                if (!(%obj.lasteffectpulse+2000 < getSimTime()))
                   return;
                %obj.lasteffectpulse = GetSimTime();
                %pos = %obj.getMuzzlePoint(%slot);
		%vec = %obj.getMuzzleVector(%slot);
                %res = containerRayCast(%pos,vectorAdd(%pos,vectorScale(%vec,2000)), $TypeMasks::PlayerObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::StaticObjectType| $TypeMasks::ForceFieldObjectType ,%obj);
		if (%res)
                    Aidpulse(getWords(%res,1,3),%obj.client,%obj.superChaingunMode-2);

                }
       else
		//for (%i = 0; %i < 1; %i++)
		//{
			Parent::onFire(%data, %obj, %slot);
		//}
}

function SuperChaingunImage::onMount(%this,%obj,%slot) {
	%obj.usingSuperChaingun = true;
	if (!%obj.client.superChaingunMode)
		%obj.client.superChaingunMode = 0;
	if (!%obj.client.superChaingunMode2)
		%obj.client.superChaingunMode2 = 0;
    %obj.hasMineModes = 1;
    %obj.hasGrenadeModes = 1;
    displayWeaponInfo(%this, %obj, %obj.client.superChaingunMode, %obj.client.superChaingunMode2);
	WeaponImage::onMount(%this,%obj,%slot);
}

function SuperChaingunImage::onUnmount(%data, %obj, %slot) {
	%obj.usingSuperChaingun = false;
    %obj.hasMineModes = 0;
    %obj.hasGrenadeModes = 0;
	WeaponImage::onUnmount(%data, %obj, %slot);
}

function SuperChaingunImage::changeMode(%this, %obj, %key) {
   switch(%key) {
      case 1:
         //Mine Modes
         %obj.client.superChaingunMode++;
         %obj.client.superChaingunMode2 = 0;
         if (%obj.client.superChaingunMode > 6 - (5 * $host::nopulseSCG))
            %obj.client.superChaingunMode = 0;
      case 2:
         //Grenade Modes
	     %obj.client.superChaingunMode2++;
         if(%obj.client.superChaingunMode != 1)
            %obj.client.superChaingunMode2 = 0;
		 if (%obj.client.superChaingunMode == 1 && %obj.client.superChaingunMode2 == 2)
            %obj.client.superChaingunMode2 = 0;
   }
   displayWeaponInfo(%this, %obj, %obj.client.superChaingunMode, %obj.client.superChaingunMode2);
}
