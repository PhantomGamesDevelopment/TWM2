$TeamDeployableMax[ZSpawnDeployable] = 9999;
//----------------------------------------------------
// Zombie Spawn Point
//---------------------------------------------------------

datablock StaticShapeData(DeployedZSpawnBase) : StaticShapeDamageProfile {
	className	= "lightbase";
	shapeFile	= "pack_deploy_sensor_motion.dts";

	maxDamage	= 2.5;
	destroyedLevel	= 2.5;
	disabledLevel	= 2.0;

	maxEnergy = 50;
	rechargeRate = 0.25;

	explosion	= HandGrenadeExplosion;
	expDmgRadius	= 1.0;
	expDamage	= 0.05;
	expImpulse	= 200;

	dynamicType			= $TypeMasks::StaticShapeObjectType;
	deployedObject		= true;
	cmdCategory			= "DSupport";
	cmdIcon			= CMDSensorIcon;
	cmdMiniIconName		= "commander/MiniIcons/com_deploymotionsensor";
	targetNameTag		= 'Deployed Zombie Spawner';
	deployAmbientThread	= true;
	debrisShapeName		= "debris_generic_small.dts";
	debris			= DeployableDebris;
	heatSignature		= 0;
	needsPower 			= true;
};

datablock ShapeBaseImageData(ZSpawnDeployableImage) {
	mass = 20;
	emap = true;
	shapeFile = "stackable1s.dts";
	item = ZSpawnDeployable;
	mountPoint = 1;
	offset = "0 0 0";
	deployed = DeployedZSpawnBase;
	heatSignature = 0;

	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateTransitionOnTriggerUp[1] = "Idle";

	isLarge = false;
	maxDepSlope = 360;
	deploySound = ItemPickupSound;

	minDeployDis = 0.5;
	maxDeployDis = 50.0;
};

datablock ItemData(ZSpawnDeployable) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "stackable1s.dts";
	mass = 5.0;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = true;
	image = "ZSpawnDeployableImage";
	pickUpName = "a Zombie Spawn pack";
	heatSignature = 0;
	emap = true;
};

function ZSpawnDeployableImage::testObjectTooClose(%item) {
	return "";
}

function ZSpawnDeployableImage::testNoTerrainFound(%item) {}

function ZSpawnDeployable::onPickup(%this, %obj, %shape, %amount) {}

function ZSpawnDeployableImage::onDeploy(%item, %plyr, %slot) {
	%className = "StaticShape";

	%playerVector = vectorNormalize(-1 * getWord(%plyr.getEyeVector(),1) SPC getWord(%plyr.getEyeVector(),0) SPC "0");

	if (vAbs(floorVec(%item.surfaceNrm,100)) $= "0 0 1") {
		%item.surfaceNrm2 = %playerVector;
	}
	else {
		%item.surfaceNrm2 = vectorNormalize(vectorCross(%item.surfaceNrm,"0 0 -1"));
	}

	%rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);

	%deplObj = new (%className)() {
		dataBlock = %item.deployed;
	};

	// set orientation
	%deplObj.setTransform(%item.surfacePt SPC %rot);

	// set the recharge rate right away
	if (%deplObj.getDatablock().rechargeRate) {
		%deplObj.setRechargeRate(%deplObj.getDatablock().rechargeRate);
	}

	// set team, owner, and handle
	%deplObj.team = %plyr.client.Team;
	%deplObj.setOwner(%plyr);
	%deplObj.light.lightBase = %deplObj;

	// set the sensor group if it needs one
	if (%deplObj.getTarget() != -1) {
		setTargetSensorGroup(%deplObj.getTarget(), %plyr.client.team);
	}

	// place the deployable in the MissionCleanup/Deployables group (AI reasons)
	addToDeployGroup(%deplObj);

	//let the AI know as well...
	AIDeployObject(%plyr.client, %deplObj);

	// play the deploy sound
	serverPlay3D(%item.deploySound, %deplObj.getTransform());

	// increment the team count for this deployed object
	$TeamDeployedCount[%plyr.team, %item.item]++;

	addDSurface(%item.surface,%deplObj);

	%deplObj.playThread($PowerThread,"Power");
	%deplObj.playThread($AmbientThread,"ambient");

	// take the deployable off the player's back and out of inventory
	if(!%plyr.client.isSuperAdmin) {
		%plyr.unmountImage(%slot);
		%plyr.decInventory(%item.item, 1);
	}

	// set power frequency
	%deplObj.powerFreq = %plyr.powerFreq;

	// Power object
	checkPowerObject(%deplObj);

	switch(%plyr.packset) {
		case 0:
			%deplobj.ZType = 1;
		case 1:
			%deplobj.ZType = 2;
		case 2:
			%deplobj.ZType = 3;
			%deplobj.numZ = 2;
		case 3:
			%deplobj.ZType = 4;
		case 4:
			%deplobj.ZType = 5;
		case 5:
			%deplobj.ZType = 6;
		case 6:
			%deplobj.ZType = 9;
		case 7:
			%deplobj.ZType = 11;
		case 8:
			%deplobj.ZType = 12;
		case 9:
			%deplobj.ZType = 13;
		case 10:
			%deplobj.ZType = 14;
		case 11:
			%deplobj.ZType = 15;
		case 12:
			%deplobj.ZType = 16;
		case 13:
			%deplobj.ZType = 17;
	}
	%deplobj.spawnTypeSet = %plyr.expertset;

	return %deplObj;
}

function DeployedZSpawnBase::onDestroyed(%this,%obj,%prevState) {
	if (%obj.isRemoved) {
		return;
	}
	%obj.isRemoved = true;
	Parent::onDestroyed(%this,%obj,%prevState);
	$TeamDeployedCount[%obj.team, ZSpawnDeployable]--;
	remDSurface(%obj);
	%obj.schedule(500, "delete");
	if (%obj.ZCloop !$= "") {
		Cancel(%obj.ZCLoop);
	}
}

function DeployedZSpawnBase::disassemble(%data,%plyr,%obj) {
	if (%obj.ZCloop !$= "") {
		Cancel(%obj.ZCLoop);
	}
	disassemble(%data,%plyr,%obj);
}

function ZSpawnDeployableImage::onMount(%data, %obj, %node) {
	%obj.hasZSpawn = true;
	%obj.expertset = 0;
}

function ZSpawnDeployableImage::onUnmount(%data, %obj, %node) {
	%obj.hasZSpawn = "";	
}

function DeployedZSpawnBase::onGainPowerEnabled(%data,%obj) {
	if(%obj.spawnTypeSet == 1) {
		%obj.numz = 0;
	}
	if (%obj.ZCloop !$= "") {
		Cancel(%obj.ZCLoop);
	}
	%obj.ZCLoop = schedule(1000, 0, "ZcreateLoop", %obj);
	Parent::onGainPowerEnabled(%data,%obj);
}

function DeployedZSpawnBase::onLosePowerDisabled(%data,%obj) {
	if (%obj.ZCloop !$= "") {
		Cancel(%obj.ZCLoop);
	}
	Parent::onLosePowerDisabled(%data,%obj);
}

//Phantom139: Personal note, rework this at some point using a built in datablock function and
// a simtimer such that we can remove that eval statement.
function ZcreateLoop(%obj) {
	if(isObject(%obj)) {
		if(%obj.timedout == 0){
			if(%obj.numZ <= 2 || %obj.numZ $= "") {
				TWM2Lib_Zombie_Core("SpawnZombie", "zPack", %obj);
				if(%obj.numZ $= "") {
					%obj.numZ = 0;
				}
				%obj.numZ++;
				%obj.timedout = 1;
				schedule(10000, 0, "eval", ""@%obj@".timedout = 0;");
			}
		}
		%obj.ZCLoop = schedule(2000, 0, "ZcreateLoop", %obj);
	}
}
