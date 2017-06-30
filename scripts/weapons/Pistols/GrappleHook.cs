//--------------------------------------------------------------------------
// Grapling Hook
// Function Credits: Xenolith
//--------------------------------------------------------------------------

//--------------------------------------
// Rifle and item...
//--------------------------------------
datablock TargetProjectileData(GrappleRope) {
   directDamage        	= 0.0;
   hasDamageRadius     	= false;
   indirectDamage      	= 0.0;
   damageRadius        	= 0.0;
   velInheritFactor    	= 1.0;

   maxRifleRange       	= 1000;
   beamColor           	= "0.0 0.1 0.0";

   startBeamWidth			= 0.20;
   pulseBeamWidth 	   = 0.15;
   beamFlareAngle 	   = 3.0;
   minFlareSize        	= 0.0;
   maxFlareSize        	= 400.0;
   pulseSpeed          	= 6.0;
   pulseLength         	= 0.150;

   textureName[0]      	= "special/nonlingradient";
   textureName[1]      	= "special/flare";
   textureName[2]      	= "special/pulse";
   textureName[3]      	= "special/expFlare";
   beacon               = true;
};

datablock SeekerProjectileData(GrappleShot) {
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   directDamage        = 0.0;
   radiusDamageType    = $DamageType::RPG;
   kickBackStrength    = 3500;

   explosion           = "BlasterExplosion";
   splash              = MissileSplash;
   velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
                                 // is cranked up to full so the missile doesn't start
                                 // out behind the player when the player is moving
                                 // very quickly - bramage
                                 
   lifetimeMS          = 500; // z0dd - ZOD, 4/14/02. Was 6000
   muzzleVelocity      = 200.0;
   maxVelocity         = 300.0; // z0dd - ZOD, 4/14/02. Was 80.0
   turningSpeed        = 54.0;
   acceleration        = 50.0;

   explodeOnDeath = false;

   proximityRadius     = 3;

   sound = MissileProjectileSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0.2 0.05 0";

   useFlechette = true;
   flechetteDelayMs = 10;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = true;
};

datablock ItemData(GrappleHook) {
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = GrappleHookImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
   pickUpName = "a grapling hook";

   computeCRC = true;

};

datablock ShapeBaseImageData(GrappleHookImage) {
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = GrappleHook;
   offset = "0 0 0";

   projectile = GrappleShot;
   projectileType = SeekerProjectile;

   usesEnergy = true;
   minEnergy = 60;

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
   stateTransitionOnTriggerDown[2] = "Fire";
   
   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 5.0;
   stateFire[3] = true;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   stateScript[3] = "onFire";
   
   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateSound[4] = MortarReloadSound;
   stateEjectShell[4]       = true;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";
   stateScript[4] = "onReload";
   
   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";
   
   stateName[6] = "DryFire";
   stateTimeoutValue[6] = 0.3;
   stateSound[6] = ChaingunDryFireSound;
   stateTransitionOnTimeout[6] = "Ready";
};

function GrappleHookImage::onFire(%data, %obj, %slot) {
   if($TWM::PlayingWarTower) {
      %obj.client.beamer = 0;
      if(isObject(%obj.beamproj)) {
         %obj.beamproj.schedule(100, "delete");
      }
      messageClient(%obj.client, 'MsgBeamerStatus', "\c5Cannot Grapple in this game mode.~wfx/weapons/ELF_underwater.wav");
      return;
   }
   if($TWM2::PlayingSabo) {
      if(%obj == Game.bomb.Carrier) {
         %obj.client.beamer = 0;
         if(isObject(%obj.beamproj)) {
            %obj.beamproj.schedule(100, "delete");
         }
         messageClient(%obj.client, 'MsgBeamerStatus', "\c5Cannot Grapple With The Bomb.~wfx/weapons/ELF_underwater.wav");
         return;
      }
   }
   if(%obj.client.beamer == 1) {
      %obj.client.beamer = 0;
      if(isObject(%obj.beamproj)) {
         %obj.beamproj.schedule(100, "delete");
      }
      messageClient(%obj.client, 'MsgBeamerStatus', '~wfx/weapons/ELF_underwater.wav');
   }
   else {
      %p = Parent::OnFire(%data, %obj, %slot);
   }
}

function GrappleShot::onExplode(%data, %proj, %pos, %mod) {
   %obj = %proj.sourceObject;
   %continue = 0;
   if (%obj.client.beamer == 0 || %obj.client.beamer $= "") {
      %obj.client.beamer = 1;
      messageClient(%obj.client, 'MsgBeamerStatus', '~wfx/weapons/cg_metal2.wav');
      %theight = getTerrainHeight(%pos);
      %maxHeight = %tHeight + 75;
      if(getWord(%pos, 2) > %maxHeight) {
         %TargetSearchMask = $AllObjMask;
         InitContainerRadiusSearch(%pos, 1, %TargetSearchMask); //small distance
         while ((%potentialTarget = ContainerSearchNext()) != 0){
            if (%potentialTarget.getPosition() != %pos) {
               %continue = 1;
            }
         }
         if(!%continue) {
            messageClient(%obj.client, 'MsgBeamerStatus', '~wfx/weapons/ELF_underwater.wav');
            messageClient(%obj.client, 'MsgBeamerStatus', "Grapples cannot fire this high.");
            return;
         }
      }
	  beamerOn(%data, %obj, %pos);
   }
}

//Functions By: Xenolith
//Modified to suit my needs
function beamerOn(%data, %obj, %hit) {
   %beamtime = 100;
   if(isObject(%obj.beamproj)) {
      %obj.beamproj.schedule(%beamtime, "delete");
   }
   if (%obj.client.beamer != 1) {
      return;
   }
   %obj.setHeat(1.0);  //max out the heat signature
   %hitobj = getWord(%hit, 0);
   %hitpos = getWord(%hit, 0) @ " " @ getWord(%hit, 1) @ " " @ getWord(%hit, 2);

   %xhit = getword(%hitpos, 0);
   %yhit = getword(%hitpos, 1);
   %zhit = getword(%hitpos, 2);
   %xobj = getword(%obj.position, 0);
   %yobj = getword(%obj.position, 1);
   %zobj = getword(%obj.position, 2);
   
   %x = %xhit - %xobj;
   %y = %yhit - %yobj;
   %z = %zhit - %zobj;
   %displacement = %x @ " " @ %y @ " " @ %z;
   
   %x = getword(%displacement, 0);
   %y = getword(%displacement, 1);
   %z = getword(%displacement, 2);

   %distance = mSqrt(%x * %x + %y * %y + %z * %z);
    
   %x = %x / %distance;
   %y = %y / %distance;
   %z = %z / %distance;
   %displacement = %x @ " " @ %y @ " " @ %z;
    
   if (%distance < 5  || %obj.client.beamer == 0) {
      %obj.client.beamer = 0;
      messageClient(%obj.client, 'MsgBeamerStatus', '~wfx/weapons/ELF_underwater.wav');
      if(isObject(%obj.beamproj)) {
         %obj.beamproj.schedule(%beamtime, "delete");
      }
      %velocity = VectorScale(%displacement, 5);
      %obj.setVelocity(%velocity);
      return;
   }
   %z = %z + 0.25;
   %fix = %x @ " " @ %y @ " " @ %z;

   if (%distance < 55) {
      %velocity = VectorScale(%fix, (%distance * 2));
   }
   else {
      %velocity = VectorScale(%fix, 110);  //was 500
   }
   %obj.setVelocity(%velocity);

   %muzzlePos = %obj.getMuzzlePoint(0);
   %obj.beamproj = new (TargetProjectile)() {
	      dataBlock        = GrappleRope;
	      initialDirection = %displacement;
	      initialPosition  = %muzzlePos;
          damageFactor     = 0;
	      sourceSlot       = 0;  //%slot
	      fadeTime			= 0.2;
   };
   schedule(%beamtime, 0, "beamerOn", %data, %obj, %hit);
}

function GrappleHookImage::onUnmount(%this, %obj, %slot) {
   Parent::onUnmount(%this, %obj, %slot);
   if(isObject(%obj.lastProjectile)) {
      %obj.lastProjectile.delete();
   }
   if(%obj.client.beamer == 1) {
      %obj.client.beamer = 0;
      if(isObject(%obj.beamproj)) {
         %obj.beamproj.schedule(100, "delete");
      }
      messageClient(%obj.client, 'MsgBeamerStatus', '~wfx/weapons/ELF_underwater.wav');
   }
}
