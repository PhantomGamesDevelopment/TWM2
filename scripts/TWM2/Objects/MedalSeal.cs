//--------------------------------------------------------------------------
// Jumpad
//--------------------------------------------------------------------------

$TeamDeployableMax[MedalSealDeployable] = 9999;

datablock StaticShapeData(DeployedMedalSeal) : StaticShapeDamageProfile {
	className = "jumpad";
	shapeFile = "nexusbase.dts"; // dmiscf.dts, alternate
	maxDamage = 2.0;
	destroyedLevel = 2.0;
	disabledLevel = 2.0;
    mass = 1;
	elasticity = 0.1;
	friction = 0.9;
	collideable = 1;
	pickupRadius = 1;
	sticky=false;

	impulse = 5000;

	hasLight = true;
	lightType = "PulsingLight";
	lightColor = "0.1 0.8 0.8 1.0";
	lightTime = "100";
	lightRadius = "3";

	explosion      = HandGrenadeExplosion;
	expDmgRadius = 3.0;
	expDamage = 0.1;
	expImpulse = 200.0;
	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;
	cmdCategory = "DSupport";
	cmdIcon = CMDSensorIcon;
	cmdMiniIconName = "commander/MiniIcons/com_deploymotionsensor";

	targetNameTag = 'SEAL';
	targetTypeTag = '';
	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
};

datablock ShapeBaseImageData(MedalSealDeployableImage) {
    mass = 1;
	emap = true;
	shapeFile = "stackable1s.dts";
	item = MedalSealDeployable;
	mountPoint = 1;
	offset = "0 0 0";
	deployed = DeployedMedalSeal;
	heatSignature = 0;
	collideable = 1;
	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateTransitionOnTriggerUp[1] = "Idle";

	isLarge = true;
	maxDepSlope = 360; // 30
	deploySound = ItemPickupSound;

	minDeployDis = 0.5;
	maxDeployDis = 5.0;
};

datablock ItemData(MedalSealDeployable) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "stackable1s.dts";
    mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = true;
	image = "MedalSealDeployableImage";
	pickUpName = "a jump pad pack";
	heatSignature = 0;
	emap = true;
};

function MedalSealDeployable::onPickup(%this, %obj, %shape, %amount) {
	// created to prevent console errors
}

function MedalSealDeployableImage::onDeploy(%item, %plyr, %slot) {
	%className = "StaticShape";

	%playerVector = vectorNormalize(-1 * getWord(%plyr.getEyeVector(),1) SPC getWord(%plyr.getEyeVector(),0) SPC "0");

	if (vAbs(floorVec(%item.surfaceNrm,100)) $= "0 0 1")
		%item.surfaceNrm2 = %playerVector;
	else
		%item.surfaceNrm2 = vectorNormalize(vectorCross(%item.surfaceNrm,"0 0 -1"));

	%deplObj = new (%className)() {
		dataBlock = %item.deployed;
		scale = "1 1 .2";
	};

	// set orientation
	%deplObj.setDeployRotation(getWords(%item.surfacePt, 0, 1) SPC getWord(%item.surfacePt, 2) + 0.1, %item.surfaceNrm);

	// set the recharge rate right away
	if (%deplObj.getDatablock().rechargeRate)
		%deplObj.setRechargeRate(%deplObj.getDatablock().rechargeRate);

	if (%deplObj.getTarget() != -1)
		setTargetSensorGroup(%deplObj.getTarget(), %plyr.client.team);

	// set team, owner, and handle
	%deplObj.team = %plyr.client.Team;
	%deplObj.setOwner(%plyr);
 
    %deplObj.powerFreq = %plyr.powerFreq;

	// place the deployable in the MissionCleanup/Deployables group (AI reasons)
	addToDeployGroup(%deplObj);

	//let the AI know as well...
	AIDeployObject(%plyr.client, %deplObj);

	// play the deploy sound
	serverPlay3D(%item.deploySound, %deplObj.getTransform());

	$TeamDeployedCount[%plyr.team, %item.item]++;

	addDSurface(%item.surface,%deplObj);
	return %deplObj;
}

function DeployedMedalSeal::onCollision(%data,%obj,%col) {
	if (%col.getClassName() !$= "Player")
		return;
    //
    %needs = %obj.targetNeeds;
    %error = %obj.targetNeedsInvalid;
    //
    if(%col.client.CheckNWChallengeCompletion(%needs)) {
       toggleMedalSwitch(%obj, -1, %col);
    }
    else {
       BottomPrint(%col.client, %error, 3, 5);
    }
}

function DeployedMedalSeal::onDestroyed(%this, %obj, %prevState) {
	if (%obj.isRemoved)
		return;
	%obj.isRemoved = true;
	Parent::onDestroyed(%this, %obj, %prevState);
	$TeamDeployedCount[%obj.team, MedalSealDeployable]--;
	remDSurface(%obj);
	%obj.schedule(500, "delete");
	fireBallExplode(%obj,1);
}

function MedalSealDeployableImage::onMount(%data, %obj, %node) {
	%obj.hasJumpad = true; // set for jumpadcheck
	%obj.packSet = 0;
}

function MedalSealDeployableImage::onUnmount(%data, %obj, %node) {
	%obj.hasJumpad = "";
	%obj.packSet = 0;
}

function toggleMedalSwitch(%obj,%state,%col) {
	if (%obj.isRemoved)
		return;
	// TODO - prevent switching while waiting for timed delay / cancel timed delay if switch is hit?
	%switchDelay = 1000;
	if (%state $= "")
		%state = -1;
	if (%col == 0 || %col $= "")
		%force = true;
	if (!%force) {
		if (%col.getClassName() !$= "Player")
			return;
		if (%col.getState() $= "Dead" || %col.FFZapped == true)
			return;
		if (!(%obj.switchTime < getSimTime())) {
			messageClient(%col.client, 'msgClient', '\c2Must wait %1 seconds between switching states.',mCeil((%obj.switchTime - getSimTime())/1000));
			return;
		}
	}
	if ((%obj.isSwitchedOff || (%force == true && %state == true)) && !(%force == true && %state == false)) {
		%state = true;
		%obj.isSwitchedOff = "";
	}
	else {
		%state = false;
		%obj.isSwitchedOff = true;
	}
	%switchCount = 0;
	%count = getWordCount($PowerList);
	// TODO - report number of successes and failures
	for(%i=0;%i<%count;%i++) {
		%powerObj = getWord($PowerList,%i);
		if (vectorDist(%obj.getPosition(),%powerObj.getPosition()) < 200
		&& !%powerObj.isRemoved && %obj.powerFreq == %powerObj.powerFreq
		&& %obj.team == %powerObj.team) {
			toggleGenerator(%powerObj,%state);
			%switchCount++;
		}
	}
	if (%state == true) {
		%obj.play3D(SwitchToggledSound);
		%obj.playThread($AmbientThread,"ambient");
		if (!%force)
			messageClient(%col.client, 'msgClient', '\c2%1 objects attempted switched on.',%switchCount);
	}
	else {
		%obj.play3D(SwitchToggledSound);
		%obj.stopThread($AmbientThread);
		if (!%force)
			messageClient(%col.client, 'msgClient', '\c2%1 objects attempted switched off.',%switchCount);
	}
	%obj.switchTime = getSimTime() + %switchDelay;
}
