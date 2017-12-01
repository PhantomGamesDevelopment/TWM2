datablock AudioProfile(BOVHitSound) {
	filename    = "fx/misc/flag_snatch.wav";
	description = AudioClose3d;
	preload = true;
};

datablock ShapeBaseImageData(BOVButt) {
	shapeFile = "weapon_disc.dts";
	mountPoint = 1;

	offset = "0.0 0.8 0.55"; // L/R - F/B - T/B
	rotation = "2.0 -2.0 3.0 45"; // L/R - F/B - T/B
};

datablock ShapeBaseImageData(BoVSwing) {
	shapeFile = "weapon_disc.dts";
	mountPoint = 1;

	offset = "0.0 1.45 -0.4"; // L/R - F/B - T/B
	rotation = "0 0 1 180"; // L/R - F/B - T/B
};

function swingbackbov(%obj) {
	%obj.unmountImage(6);
	%obj.mountImage(BOVButt, 5);
}

datablock LinearProjectileData(BOVhit) {
	projectileShapeName = "disc.dts";
	emitterDelay        = -1;
	directDamage        = 0.9;
	radiusDamageType    = $DamageType::BladeOfVengance;
	kickBackStrength    = 1750;

	ImageSource         = "BOVImage";

	sound 				= discProjectileSound;
	explosion           = "ChaingunExplosion";
	underwaterExplosion = "ChaingunExplosion";
	splash              = DiscSplash;

	dryVelocity       = 200;
	wetVelocity       = 200;
	velInheritFactor  = 0.5;
	fizzleTimeMS      = 25;
	lifetimeMS        = 25;
	explodeOnDeath    = true;
	reflectOnWaterImpactAngle = 15.0;
	explodeOnWaterImpact      = true;
	deflectionOnWaterImpact   = 0.0;
	fizzleUnderwaterMS        = 5000;

	activateDelayMS = 200;

	hasLight    = true;
	lightRadius = 6.0;
	lightColor  = "0.175 0.175 0.5";
};


//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(BOVImage) {
	className = WeaponImage;
	shapeFile = "turret_muzzlepoint.dts";
	item = BOV;
	projectile = BOVhit;
	projectileType = LinearProjectile;

	usesEnergy = true;
	fireEnergy = 20;
	minEnergy = 30;

	MedalRequire = 1;

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

datablock ItemData(BOV) {
	className = Weapon;
	catagory = "Spawn Items";
	shapeFile = "weapon_disc.dts";
	image = BOVImage;
	mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 2;
	pickUpName = "a blade of vengance";
};

function BOVImage::onMount(%data, %obj, %node) {
	if(!%obj.client.hasMedal(11) && !%obj.client.isAiControlled()) {
		%obj.throwweapon(1);
		%obj.throwweapon(0);		
		messageClient(%obj.client, 'MsgClient', "\c3You must aquire the 'Revenge Avoided Again' Medal to use this.");
		return;
	}	
	%obj.meleeIMG = %obj.mountImage(BOVButt, 5);
}

function BOVImage::onUnMount(%data, %obj, %node) {
	%obj.unmountImage(5);
}

function BOVImage::onFire(%data, %obj, %node) {
	if(!%obj.client.hasMedal(11) && !%obj.client.isAiControlled()) {
		messageClient(%obj.client, 'MsgClient', "\c3You must aquire the 'Revenge Avoided Again' Medal to use this.");
		return;
	}
	if(%obj.cannotuseBOV) {  //in the kill anim?
		return;
	}
	//
	%obj.unmountImage(5);
	%obj.meleeIMG = %obj.mountImage(BoVSwing, 6);
	%obj.backswing = schedule(300, 0, "swingbackbov", %obj);

	%p = new (LinearProjectile)() {
		dataBlock = BOVhit;
		initialDirection = %obj.getMuzzleVector(%slot);
		initialPosition = %obj.getMuzzlePoint(%slot);
		sourceObject = %obj;
		sourceSlot = %slot;
	};
	MissionCleanup.add(%p);
}

function BOVhit::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal) {
	%dMod = 1;
	//
    if(%targetObject.rapierShield) {
       MessageClient(%projectile.sourceObject.client, 'MsgDeflect', "The Target Is Immortal.");
       return;
    }
	//
	if(%targetObject.isBoss) {
		%dMod += 7;
	}
	if(%targetObject.isZombie) {
		%dMod += 100;
	}	
	%source = %projectile.SourceObject;
	%hitObj = %targetObject;

	%muzzlePos = %source.getMuzzlePoint(0);
	%muzzleVec = %source.getMuzzleVector(0);

	// extra damage for head shot or less for close range shots
	if(!(%hitObj.getType() & ($TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType)) && (%hitObj.getDataBlock().getClassName() $= "PlayerData")) {
		if(%hitObj.getDataBlock().getClassName() $= "PlayerData") {
			// Now we see if we hit from behind...
			%forwardVec = %hitobj.getForwardVector();
			%objDir2D   = getWord(%forwardVec, 0) @ " " @ getWord(%forwardVec,1) @ " " @ "0.0";
			%objPos     = %hitObj.getPosition();
			%dif        = VectorSub(%objPos, %muzzlePos);
			%dif        = getWord(%dif, 0) @ " " @ getWord(%dif, 1) @ " 0";
			%dif        = VectorNormalize(%dif);
			%dot        = VectorDot(%dif, %objDir2D);
			// 120 Deg angle test...
			// 1.05 == 60 degrees in radians
			if (%dot >= mCos(1.05)) {
				// Rear hit
				%source.applyRepair("0.45"); //we get a bonus repair for rear
				if(%source.team == %hitObj.team && !$TeamDamage) {
					ServerPlay3d(BOVHitSound, %targetObject.getPosition());
					return; 
				}
				%source.cannotuseBOV = 1;
				if(!%hitObj.IsinvincibleC) {
					DoBOVRearKill(%source, %hitObj, 0);
				}
				return;
			}
		}
		ServerPlay3d(BOVHitSound, %targetObject.getPosition());
		//The Blade Only Works On Players
		%totalDamage = %data.directDamage * dMod;
		%targetObject.damage(%projectile.sourceObject, %position, %totalDamage, %data.directDamageType);
		if(isObject(%source) || %source.getState() !$= "dead") {
			%source.applyRepair("0.15"); //15%
		}
	}
}

function DoBOVRearKill(%source, %target, %count) {
	%count++;
	if(!isObject(%source) || %source.getState() $= "dead") {
		%target.setMoveState(false);
		return;
	}
	%source.setMoveState(true);
	%target.setMoveState(true);
	%target.clearInventory(); //ha, no guns for You!
	//lift
	if(%count <= 15) {
		%ZPos = %count * 0.025;
		%newpos = vectoradd(%target.getPosition(),"0 0 "@%ZPos@"");
		%target.setTransform(%newpos);
		%target.setvelocity("0 0 0");
	}
	else if(%count == 16) {
		%newpos = vectoradd(%target.getPosition(),"0 0 "@%ZPos * -1@"");
		%target.setTransform(%newpos);
		%target.setvelocity("0 0 0");
	}
	else if(%count == 17) {
		if(%target.isZombie) {
			recordAction(%source.client, "BACK", "zombie");
			if(%target.isPlayerRog && %target.getControllingClient() !$= "") {
				if(!%source.client.CheckNWChallengeCompletion("CompletelyUnexpected")) {
					CompleteNWChallenge(%source.client, "CompletelyUnexpected");
				}		 
			}
		}
		else {
			recordAction(%source.client, "BACK", "player");
			if(!%source.client.CheckNWChallengeCompletion("Assassin")) {
				CompleteNWChallenge(%source.client, "Assassin");
			}
		}	   
		ServerPlay3d(BOVHitSound, %target.getPosition());
		ServerPlay3d(BOVHitSound, %target.getPosition());
		ServerPlay3d(BOVHitSound, %target.getPosition());
		%target.damage(%source, %target.getposition(), 9999, $DamageType::BladeOfVengance_Assassination);
		//
		if(%target.client !$= "") { //a Player.. goodie
			MessageAll('MessageAll', "\c0"@%target.client.namebase@" was assassinated by "@%source.client.namebase@".");
		}
		//Challenges...
		%source.cannotuseBOV = 0;
		%source.setMoveState(false);
		return;
	}
	schedule(100,0,"DoBOVRearKill", %source, %target, %count);
}
