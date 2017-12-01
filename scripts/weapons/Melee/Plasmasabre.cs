//ALL CREDIT TO THE HALO MOD
// ORIGINALLY CODED BY: SoldierOfLight

datablock AudioProfile(BeamExpSound) {
	filename = "fx/vehicles/shrike_blaster_projectile_impact.wav";
	description = AudioClosest3d;
	preload = true;
};

datablock ExplosionData(PlasmasaberExplosion) {
	explosionShape = "disc_explosion.dts";
	faceViewer           = true;
	explosionscale = 0.1;
	scale = 0.1;
	soundProfile = BeamExpSound;

	playSpeed = 2;

	sizes[0] = 0.1;
	sizes[1] = 0.1;
	times[0] = 0.0;
	times[1] = 1.0;
};

datablock TracerProjectileData(PlasmasaberExpCreater) {
	className = "TracerProjectileData";
	Explosion = "PlasmasaberExplosion";
	dryVelocity = "0.1";
	wetVelocity = "0.1";
	fizzleTimeMS = "32";
	lifetimeMS = "32";
	explodeOnDeath = "1";
	crossSize = "0.1";
	renderCross = "0";
	isFXUnit = "1";
};

datablock TargetProjectileData(PlasmasaberBeam) {
	directDamage        	= 0.0;
	hasDamageRadius     	= false;
	indirectDamage      	= 0.0;
	damageRadius        	= 0.0;
	velInheritFactor    	= 1.0;

	maxRifleRange       	= 1.5;
	beamColor           	= "1 1 0";

	startBeamWidth		= 0.1;
	pulseBeamWidth 	    = 0.1;
	beamFlareAngle 	    = 3.0;
	minFlareSize        	= 0.0;
	maxFlareSize        	= 0.0;
	pulseSpeed          	= 0.0;
	pulseLength         	= 0.0;

	textureName[0]      	= "special/nonlingradient";
	textureName[1]      	= "special/flare";
	textureName[2]      	= "special/pulse";
	textureName[3]      	= "skins/glow_Red";
};

datablock ItemData(Plasmasaber) {
	className = Weapon;
	catagory = "Spawn Items";
	//$$ TODO - real shape file
	shapeFile = "stackable4m.dts";
	image = PlasmasaberImage;
	mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 2;
	pickUpName = "a beam sword";

	computeCRC = true;
	emap = true;
};

datablock ShapeBaseImageData(PlasmasaberImage) {
	className = WeaponImage;
	shapeFile = "turret_muzzlepoint.dts";
	offset = "0.1 0.7 0.2";
	item = Plasmasaber;
	emap = true;
	projectile = EnergyBolt;
	projectileType = EnergyProjectile;

	MedalRequire = 1;

	// State Data
	stateName[0] = "Activate";
	stateTransitionOnTimeout[0] = "ActivateReady";
	stateTimeoutValue[0] = 0.2;
	stateSequence[0] = "Activate";
	stateSound[0] = ChaingunSwitchSound;

	stateName[1] = "ActivateReady";
	stateTimeoutValue[1] = 0.01;
	stateScript[1] = "onActivateReady";
	stateTransitionOnTimeout[1] = "Ready";

	stateName[2] = "Ready";
	stateScript[2] = "onReady";
	stateTransitionOnTriggerDown[2] = "Fire";

	stateName[3] = "Fire";
	stateTransitionOnTimeout[3] = "Reload";
	stateFire[3] = true;
	stateAllowImageChange[3] = false;
	stateScript[3] = "onFire";

	stateName[4]                  = "Reload";
	stateTransitionOnTimeout[4]   = "Ready";
	stateTimeoutValue[4]          = 1.5;
	stateAllowImageChange[4]      = false;
};

datablock ShapeBaseImageData(PlasmasaberImage2) {
	shapeFile = "weapon_energy_vehicle.dts";
	offset = "0.1 0.2 0";
	rotation = "1 0 0 -10";
};

function PlasmasaberImage::onMount(%data, %obj, %slot) {
	if(%obj.isZombie || %obj.isBoss) {
		%obj.setInventory(Plasmasaber, 0, true);
	}
	Parent::onMount(%data,%obj,%slot);
	%obj.mountImage(PlasmasaberImage2, 4);
	%obj.usingPlasmasaber = 1;
}

function PlasmasaberImage::onUnmount(%data, %obj, %slot) {
	Parent::onUnmount(%data,%obj,%slot);
	%obj.unMountImage(4);
	%obj.usingPlasmasaber = 0;
	cancel(%obj.Plasmasaberloop);
	if(isObject(%obj.beam1)) {
		%obj.beam1.delete();
		%obj.beam1 = "";
	}
}

function PlasmasaberImage::onActivateReady(%data, %obj, %slot) { //small explosion
	%p = new TracerProjectile() {
		datablock = "PlasmasaberExpCreater";
		initialPosition = %obj.getMuzzlePoint(%slot);
		initialDirection = %obj.getMuzzleVector(%slot);
		sourceObject = %obj;
	};
}

function PlasmasaberImage::onReady(%data, %obj, %slot) { //create beams
	Plasmasaberloop(%obj);
}

function PlasmasaberLoop(%obj) {
	if(!isObject(%obj) || %obj.getState() $= "dead") {
		return;
	}
	if(%obj.usingPlasmasaber == 0) {
		if(isObject(%obj.beam1)) {
			%obj.beam1.delete();
		}
		return;
	}
	%vec1 = %obj.getMuzzleVector(4);
	%vec = %obj.getMuzzleVector(0);
	%vec2d = getWords(%vec,0,1) SPC "0";
	%left = vectorCross(%vec2d,"0 0 1");
	%up = vectorCross(%vec,%left);
	%up = vectorScale(%up,0.2);
	%down = vectorScale(%up,-1);
	%pos1 = %obj.getMuzzlePoint(5);
	%dir1 = vectorAdd(%vec1,%down);
	if(!isObject(%obj.beam1) && !%obj.isCloaked()) {
		%obj.beam1 = new TargetProjectile() {
			datablock = "PlasmasaberBeam";
			initialPosition = %pos1;
			initialDirection = %dir1;
			sourceSlot = 4;
			sourceObject = %obj;
		};
	}
	%obj.Plasmasaberloop = schedule(25, 0, Plasmasaberloop, %obj);
}

function PlasmasaberImage::onFire(%data, %obj, %slot) { //apply damage
	%vec = %obj.getMuzzleVector(%slot);
	%pos = %obj.getMuzzlePoint(%slot);
	%vec = vectorScale(%vec, 1.5);
	%end = vectorAdd(%pos, %vec);
	%mask = $TypeMasks::VehicleObjectType | $TypeMasks::PlayerObjectType | $TypeMasks::StaticShapeObjectType;
	initContainerRadiusSearch(%end, 1.0, %mask);
	while((%target = containerSearchNext()) != 0) {
		if(%target == %obj) {
			return;
		}
		//if(!%target.isBoss) {
		//	%target.damage(%obj, %target.getWorldBoxCenter(), 10.4, $DamageType::Plasmasaber);
		//}
		//else {
		//	%target.damage(%obj, %target.getWorldBoxCenter(), 0.4, $DamageType::Plasmasaber);
		//}
		//TWM2 3.9.2: Balance Update, Bump Up Plasmasaber damage for all enemies
		%target.damage(%obj, %target.getWorldBoxCenter(), 5.0, $DamageType::Plasmasaber);
		%p = new TracerProjectile() {
			datablock = "PlasmasaberExpCreater";
			initialPosition = %target.getPosition();
			initialDirection = %obj.getMuzzleVector(%slot); //who cares?
			sourceObject = %obj;
		};
	}
}

function Plasmasaber::onThrow(%data, %obj, %plyr) {
	%pos = %obj.getPosition();
	%p = new TracerProjectile() {
		datablock = "PlasmasaberExpCreater";
		initialPosition = %pos;
		initialDirection = "0 0 1";
		sourceObject = %obj;
	};
}
