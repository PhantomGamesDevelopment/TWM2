$TeamDeployableMax[SpawnPointPack] = 9999;
datablock ShapeBaseImageData(SpawnPointDeployableImage) {
mass = 15;
emap = true;
shapeFile = "stackable1s.dts";
item = SpawnPointPack;
mountPoint = 1;
offset = "0 0 0";
deployed = SpawnPointDeployedBase;
heatSignature = 0;

stateName[0] = "Idle";
stateTransitionOnTriggerDown[0] = "Activate";

stateName[1] = "Activate";
stateScript[1] = "onActivate";
stateTransitionOnTriggerUp[1] = "Idle";

isLarge = true;
maxDepSlope = 360;
deploySound = StationDeploySound;

minDeployDis = 0.5;
maxDeployDis = 5.0;
};

datablock ItemData(SpawnPointPack) {
className = Pack;
catagory = "Deployables";
shapeFile = "stackable1s.dts";
mass = 3.0;
elasticity = 0.2;
friction = 0.6;
pickupRadius = 1;
rotate = false;
image = "SpawnPointDeployableImage";
pickUpName = "a spawn point deployable";
heatSignature = 0;
joint = "2 2 2";
computeCRC = true;
emap = true;
};

datablock StaticShapeData(SpawnPointDeployedBase) : StaticShapeDamageProfile {
className = "StaticShape";
shapeFile = "nexuscap.dts";

maxDamage = 2.00;
destroyedLevel = 2.00;
disabledLevel = 1.35;

isShielded = true;
energyPerDamagePoint = 250;
maxEnergy = 100;
rechargeRate = 1;
isspawnpoint=1;

explosion = ShapeExplosion; // DeployablesExplosion;
expDmgRadius = 18.0;
expDamage = 0.1;
expImpulse = 200.0;

dynamicType = $TypeMasks::StationObjectType;
deployedObject = true;
cmdCategory = "DSupport";
cmdIcon = CMDSwitchIcon;
cmdMiniIconName = "commander/MiniIcons/com_switch_grey";
targetNameTag = 'Deployed';
targetTypeTag = 'Spawn Point';

debrisShapeName = "debris_generic.dts";
debris = DeployableDebris;

heatSignature = 0;
needsPower = false;

humSound = SensorHumSound;
pausePowerThread = true;
sensorData = TelePadBaseSensorObj;
sensorRadius = TelePadBaseSensorObj.detectRadius;
sensorColor = "0 212 45";
firstPersonOnly = true;

lightType = "PulsingLight";
lightColor = "0 1 0 1";
lightTime = 1200;
lightRadius = 6;
};

function SpawnPointDeployedBase::onDestroyed(%this,%obj,%prevState) {
if (%obj.isRemoved)
return;
%obj.isRemoved = true;
Parent::onDestroyed(%this,%obj,%prevState);
$TeamDeployedCount[%obj.team,SpawnPointPack]--;
%obj.isRemoved = true;
remDSurface(%obj);
%obj.beam.schedule(150,"delete");
%obj.schedule(500,"delete");
}

function SpawnPointDeployedBase::disassemble(%data,%plyr,%obj) {
%obj.isRemoved = true;
disassemble(%data,%plyr,%obj);
}

function SpawnPointPack::onPickup(%this,%obj,%shape,%amount) {
}

function SpawnPointDeployableImage::onDeploy(%item,%plyr,%slot) {
%className = "StaticShape";
%item.surfacePt = vectorAdd(%item.surfacePt,vectorScale(%item.surfaceNrm,0.4));
%playerVector = vectorNormalize(getWord(%plyr.getEyeVector(),1) SPC -1 * getWord(%plyr.getEyeVector(),0) SPC "0");
%item.surfaceNrm2 = %playerVector;
if (vAbs(floorVec(%item.surfaceNrm,100)) $= "0 0 1")
%item.surfaceNrm2 = vectorScale(%playerVector,-1);
else
%item.surfaceNrm2 = vectorNormalize(vectorCross(%item.surfaceNrm,"0 0 1"));
%rot = fullRot(vectorScale(%item.surfaceNrm,-1),%item.surfaceNrm2);
%deplObj = new (%className)() {
dataBlock = SpawnPointDeployedBase;
scale = "1 1 1";
deployed = true;
};
%deplObj.setTransform(%item.surfacePt SPC %rot);
%deplObj.team = %plyr.client.team;
%deplObj.setOwner(%plyr);
%deplObj.powerFreq = %plyr.powerFreq;
if (%deplObj.getTarget() != -1)
setTargetSensorGroup(%deplObj.getTarget(),%plyr.client.team);
%frequency = %plyr.packSet;
addToDeployGroup(%deplObj);
AIDeployObject(%plyr.client,%deplObj);
serverPlay3D(%item.deploySound,%deplObj.getTransform());
$TeamDeployedCount[%plyr.team,%item.item]++;
addDSurface(%item.surface,%deplObj);
if (%plyr.packSet == 0) %deplobj.ispersonal=1;
//%plyr.unmountImage(%slot);
//%plyr.decInventory(%item.item,1);
checkPowerObject(%deplObj);
messageclient(%plyr.client,'MsgClient',"\c3Spawn point placed. Type /setspawn while pointing at it to set your spawn point.");
return %deplObj;
}

function SpawnPointDeployableImage::onMount(%data,%obj,%node) {
%obj.hasSpawn = true;
%obj.packSet = 0;
displayPowerFreq(%obj);
}

function SpawnPointDeployableImage::onUnmount(%data,%obj,%node) {
%obj.hasSpawn = "";
%obj.packSet = 0;
}
