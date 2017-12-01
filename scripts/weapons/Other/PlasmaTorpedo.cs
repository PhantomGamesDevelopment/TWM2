datablock TracerProjectileData(PlasmaTorpedoProjectile) {
	doDynamicClientHits = true;

	directDamage        = 0.0;
	hasDamageRadius     = true;
	indirectDamage      = 1.5;
	damageRadius        = 25.0;
	kickBackStrength    = 4000;
	radiusDamageType    = $DamageType::PTorpdeo;

	ImageSource         = "PlasmaTorpedoImage";

	explosion           = "SatchelMainExplosion";
	splash              = ChaingunSplash;

	sound = ShrikeBlasterProjectileSound;

	dryVelocity       = 425.0;
	wetVelocity       = 100.0;
	velInheritFactor  = 1.0;
	fizzleTimeMS      = 1000;
	lifetimeMS        = 1000;
	explodeOnDeath    = false;
	reflectOnWaterImpactAngle = 0.0;
	explodeOnWaterImpact      = false;
	deflectionOnWaterImpact   = 0.0;
	fizzleUnderwaterMS        = 3000;

	tracerLength    = 45.0;
	tracerAlpha     = false;
	tracerMinPixels = 6;
	tracerColor     = "1.0 0.0 0.0 0.0";
	tracerTex[0]  	 = "special/shrikeBolt";
	tracerTex[1]  	 = "special/shrikeBoltCross";
	tracerWidth     = 0.55;
	crossSize       = 0.99;
	crossViewAng    = 0.990;
	renderCross     = true;
};

datablock ShapeBaseImageData(PlasmaTorpedoImage) {
	className = WeaponImage;
	shapeFile = "turret_fusion_large.dts";
	item      = PlasmaTorpedo;
	usesenergy = true;

	projectile = PlasmaTorpedoProjectile;
	projectileType = TracerProjectile;

	emap = true;

	//RankRequire = $TWM2::RankRequire["SCD343"];
	PrestigeRequire = 9;

	HasChallenges = 1;
	ChallengeCt = 8;
	Challenge[1] = "PTC Hunter\t100\t100\tNone";
	Challenge[2] = "PTC Expert\t250\t500\tNone";
	Challenge[3] = "PTC Master\t500\t1000\tNone";
	Challenge[4] = "PTC God\t1000\t2000\tNone";
	Challenge[5] = "PTC Bronze Commendation\t2500\t10000\tNone";
	Challenge[6] = "PTC Silver Commendation\t5000\t25000\tNone";
	Challenge[7] = "PTC Gold Commendation\t10000\t50000\tNone";
	Challenge[8] = "PTC Titan Commendation\t25000\t75000\tNone";
	GunName = "Plasma Torpedo Cannon";

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
	stateTimeoutValue[3] = 7.6;
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

datablock ItemData(PlasmaTorpedo) {
	className    = Weapon;
	catagory     = "Spawn Items";
	shapeFile    = "turret_mortar_large.dts";
	image        = PlasmaTorpedoImage;
	mass         = 1;
	elasticity   = 0.2;
	friction     = 0.6;
	pickupRadius = 2;
	pickUpName   = "a Plasma Torpedo Cannon";

	computeCRC = true;
	emap = true;
};