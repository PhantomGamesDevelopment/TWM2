// Acid Cannon

datablock TracerProjectileData(AcidCannonPulse) {
	doDynamicClientHits = true;

	projectileShapeName = "";
	directDamage        = 0.3;
	directDamageType    = $DamageType::AcidCannon;
	hasDamageRadius     = true;
	indirectDamage      = 0.35;
	damageRadius        = 10.0;
	kickBackStrength    = 0.0;
	radiusDamageType    = $DamageType::AcidCannon;
	sound          	    = BlasterProjectileSound;
	explosion           = ZAcidBallExplosion;

	ImageSource         = "AcidCannonImage";

	dryVelocity       = 150.0;
	wetVelocity       = 150.0;
	velInheritFactor  = 1.0;
	fizzleTimeMS      = 1200;
	lifetimeMS        = 1500;
	explodeOnDeath    = false;
	reflectOnWaterImpactAngle = 0.0;
	explodeOnWaterImpact      = true;
	deflectionOnWaterImpact   = 0.0;
	fizzleUnderwaterMS        = -1;

	activateDelayMS = 100;

	tracerLength    = 5;
	tracerAlpha     = false;
	tracerMinPixels = 3;
	tracerColor     = "0 1 0 1";
	tracerTex[0]  	 = "special/landSpikeBolt";
	tracerTex[1]  	 = "special/landSpikeBoltCross";
	tracerWidth     = 0.5;
	crossSize       = 0.79;
	crossViewAng    = 0.990;
	renderCross     = true;
	emap = true;
};

function AcidCannonPulse::onExplode(%data, %proj, %pos, %mod) {
   Parent::OnExplode(%data, %proj, %pos, %mod);
   %TargetSearchMask = $TypeMasks::PlayerObjectType;
   InitContainerRadiusSearch(%pos, %data.damageRadius, %TargetSearchMask);
   while ((%potentialTarget = ContainerSearchNext()) != 0) {
      if(%potentialTarget.isAlive()) {
         if(((%potentialTarget.client.team == %proj.sourceObject.client.team) && $TeamDamage) || %potentialTarget.client.team != %proj.sourceObject.client.team) {
            %potentialTarget.setWhiteout(1.0);
         }
      }
   }
}

datablock ShapeBaseImageData(AcidCannonImage) {
	className = WeaponImage;
	shapeFile = "weapon_energy.dts";
	item = AcidCannon;
	projectile = AcidCannonPulse;
	projectileType = TracerProjectile;

	HasChallenges = 0;
	//RankRequire = $TWM2::RankRequire["PulsePhaser"];
	//No rank required, this weapon is picked up from killed zombies

	usesEnergy = true;
	fireEnergy = 4;
	minEnergy = 4;

	stateName[0] = "Activate";
	stateTransitionOnTimeout[0] = "ActivateReady";
	stateTimeoutValue[0] = 0.5;
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
	stateTimeoutValue[3] = 0.3;
	stateFire[3] = true;
	stateRecoil[3] = NoRecoil;
	stateAllowImageChange[3] = false;
	stateSequence[3] = "Fire";
	//stateSound[3] = PPWFireSound;
	stateScript[3] = "onFire";

	stateName[4] = "Reload";
	stateTransitionOnNoAmmo[4] = "NoAmmo";
	stateTransitionOnTimeout[4] = "Ready";
	stateTimeoutValue[4] = 4.2;
	stateAllowImageChange[4] = false;
	stateSequence[4] = "Reload";

	stateName[5] = "NoAmmo";
	stateTransitionOnAmmo[5] = "Reload";
	stateSequence[5] = "NoAmmo";
	stateTransitionOnTriggerDown[5] = "DryFire";

	stateName[6] = "DryFire";
	stateTimeoutValue[6] = 0.3;
	stateSound[6] = BlasterDryFireSound;
	stateTransitionOnTimeout[6] = "Ready";
};

datablock ItemData(AcidCannon) {
	className = Weapon;
	catagory = "Spawn Items";
	shapeFile = "weapon_energy.dts";
	image = AcidCannonImage;
	mass = 1;
	alwaysAllowPickup = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 2;
	pickUpName = "an Acid Cannon";
};

function AcidCannonImage::onMount(%this, %obj, %slot) {
	Parent::onMount(%this, %obj, %slot);
}

function AcidCannonImage::onUnmount(%this,%obj,%slot) {
	Parent::onUnmount(%this, %obj, %slot);
}

function AcidCannonImage::onFire(%data,%obj,%slot) {
	AcidCannonAttack(%obj, %slot, 0);
}

function AcidCannonAttack(%object, %slot, %chargecount) {
	%muzzleVec = %object.getMuzzleVector(%slot);
	%muzzlePt = %object.getMuzzlePoint(%slot);
	if(%chargecount <= 5) {
		if(%chargecount == 1) {
			serverPlay3D(AcidCannonPreFireSound, %muzzlePt);
		}
		%charge = new ParticleEmissionDummy() {
			position = %muzzlePt;
			dataBlock = "defaultEmissionDummy";
			emitter = "BurnEmitter";
		};
		MissionCleanup.add(%charge);
		%charge.schedule(100, "delete");
	}
	if(%chargecount == 7 || %chargecount == 9) {
		%p = new TracerProjectile() {
			dataBlock        = AcidCannonPulse;
			initialDirection = %muzzleVec;
			initialPosition  = %muzzlePt;
			sourceObject     = %object;
			sourceSlot       = 0;
		};
		MissionCleanup.add(%p);
		serverPlay3D(CentaurArtilleryFireSound, %muzzlePt);
	}
	//
	if(%chargecount <= 9) {
		%chargecount++;
		schedule(100, 0, "AcidCannonAttack", %object, %slot, %chargecount);
	}
}
