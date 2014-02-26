datablock StaticShapeData(DeployedDoor) : StaticShapeDamageProfile {
className = "door";
shapeFile = "Pmiscf.dts"; // b or dmiscf.dts, alternate

maxDamage = 2;
destroyedLevel = 2;
disabledLevel = 1.5;

isShielded = true;
energyPerDamagePoint = 60;
maxEnergy = 100;
rechargeRate = 0.25;

explosion = HandGrenadeExplosion;
expDmgRadius = 3.0;
expDamage = 0.1;
expImpulse = 200.0;

dynamicType = $TypeMasks::StaticShapeObjectType;
deployedObject = true;
cmdCategory = "DSupport";
cmdIcon = CMDSensorIcon;
cmdMiniIconName = "commander/MiniIcons/com_deploymotionsensor";
targetNameTag = 'Door';
deployAmbientThread = true;
debrisShapeName = "debris_generic_small.dts";
debris = DeployableDebris;
heatSignature = 0;
needsPower = true;
};

datablock ShapeBaseImageData(DoorDeployableImage) {
mass = 1;
emap = true;
shapeFile = "stackable1s.dts";
item = DoorDeployable;
mountPoint = 1;
offset = "0 0 0";
deployed = DeployedDoor;
heatSignature = 0;

stateName[0] = "Idle";
stateTransitionOnTriggerDown[0] = "Activate";

stateName[1] = "Activate";
stateScript[1] = "onActivate";
stateTransitionOnTriggerUp[1] = "Idle";

isLarge = false;
maxDepSlope = 360;
deploySound = ItemPickupSound;

minDeployDis = 0;
maxDeployDis = 50.0;
};

datablock ItemData(DoorDeployable) {
className = Pack;
catagory = "Deployables";
shapeFile = "stackable1s.dts";
mass = 1;
elasticity = 0.2;
friction = 0.6;
pickupRadius = 1;
rotate = true;
image = "DoorDeployableImage";
pickUpName = "an Advanced Door Pack";
heatSignature = 0;
emap = true;
};

function DoorDeployableImage::testObjectTooClose(%item) {
return "";
}

function DoorDeployableImage::testNoTerrainFound(%item) {
// don't check this for non-Landspike turret deployables
}

function DoorDeployable::onPickup(%this, %obj, %shape, %amount) {
// created to prevent console errors
}

function doorDeployableImage::onMount(%data, %obj, %node) {
%obj.hasDoor = true; // set for blastcheck
%obj.packSet = 0;
%obj.packSet[0] = 0;
%obj.packSet[1] = 0;
%obj.packSet[2] = 0;
%obj.expertSet = 0;
displayPowerFreq(%obj);

}

function doorDeployableImage::onUnmount(%data, %obj, %node) {
%obj.hasDoor = "";
%obj.packSet = 0;
%obj.packSet[0] = 0;
%obj.packSet[1] = 0;
%obj.packSet[2] = 0;
%obj.expertSet = 0;
}

function doorDeployableImage::onDeploy(%item, %plyr, %slot) {
 %className = "StaticShape";
	%grounded = 0;
	if (%item.surface.getClassName() $= TerrainBlock)
		%grounded = 1;

	%playerVector = vectorNormalize(-1 * getWord(%plyr.getEyeVector(),1) SPC getWord(%plyr.getEyeVector(),0) SPC "0");

	if (%item.surfaceinher == 0) {
		if (vAbs(floorVec(%item.surfaceNrm,100)) $= "0 0 1"){
			%item.surfaceNrm2 = %playerVector;
            }
		else{
			%item.surfaceNrm2 = vectorNormalize(vectorCross(%item.surfaceNrm,"0 0 1"));
            }
    }

	%rot1    = fullRot(%item.surfaceNrm,%item.surfaceNrm2);

    %scale1 = "0.5 6 160";
    %scale2 = "0.5 8 160";
	%dataBlock = %item.deployed;

		%space = rayDist(%item.surfacePt SPC %item.surfaceNrm,%scale1,$AllObjMask);
		if (%space != getWord(%scale1,1))
			%type  = true;
		%scale1 = getWord(%scale1,0) SPC getWord(%scale1,0) SPC %space;


        %mCenter = "0 0 -0.5";
		%pad = pad(%item.surfacePt SPC %item.surfaceNrm SPC %item.surfaceNrm2,%scale2,%mCenter);
		%scale2 = getWords(%pad,0,2);
		%item.surfacePt2 = getWords(%pad,3,5);

        //%vec1 = realVec(getWord(%item.surface,0),%item.surfaceNrm);
        //%vec1 = realVec(%pad,%item.surfaceNrm);
        %vec1 =validateVal(MatrixMulVector("0 0 0",%item.surfaceNrm));

	if (!%scaleIsSet){
		%scale1 = vectorMultiply(%scale1,1/4 SPC 1/3 SPC 2);
        %scale2 = vectorMultiply(%scale2,1/4 SPC 1/3 SPC 2);
        %x = (getWord(%scale2,1)/0.166666)*0.125;
        %scale3 = %x SPC 0.166666 SPC getWord(%scale1,2);
        }



	%dir1 = VectorNormalize(vectorSub(%item.surfacePt,%item.surfacePt2));
	%adjust1 = vectorNormalize(vectorProject(%dir1,vectorCross(%item.surfaceNrm,%item.surfaceNrm2)));
    %offset1 = -0.5;
    %adjust1 = vectorScale(%adjust1,-0.5 * %offset1);

    %deplObj = new (%className)() {
		dataBlock = %dataBlock;
		scale = %scale3;
	};
    %deplObj.closedscale = %scale3;
    %deplObj.openedscale = getwords(%scale3,0,1) SPC 0.1;
    //
    %deplObj.savedclosedscale = %scale3;
    %deplObj.savedopenedscale = getwords(%scale3,0,1) SPC 0.1;
    //
//////////////////////////Apply settings//////////////////////////////

	// exact:
	//%deplObj.setTransform(%item.surfacePt SPC %rot1);
    %deplObj.setTransform(vectorAdd(VectorAdd(%item.surfacePt2, VectorScale(%vec1,getword(%scale3,2)/-4)),%adjust1) SPC %rot1);

	// misc info
	addDSurface(%item.surface,%deplObj);
	// [[Settings]]:

	%deplObj.grounded = %grounded;
	%deplObj.needsFit = 1;
    %deplObj.isdoor   = 1;
	// [[Normal Stuff]]:

	// set team, owner, and handle
	%deplObj.team = %plyr.client.team;
	%deplObj.setOwner(%plyr);

	// set power frequency
	%deplObj.powerFreq = %plyr.powerFreq;

	// set the sensor group if it needs one
	if (%deplObj.getTarget() != -1)
		setTargetSensorGroup(%deplObj.getTarget(), %plyr.client.team);

	// place the deployable in the MissionCleanup/Deployables group (AI reasons)
	addToDeployGroup(%deplObj);

	//let the AI know as well...
	AIDeployObject(%plyr.client, %deplObj);

	// play the deploy sound
	serverPlay3D(%item.deploySound, %deplObj.getTransform());

	// increment the team count for this deployed object
	$TeamDeployedCount[%plyr.team, %item.item]++;

	%deplObj.deploy();
    %owner  = %deplObj.getOwner();    //this is for leveled cards

if(%plyr.packSet[1] == 0) {
%deplobj.timeout = "0";
}
else if(%plyr.packSet[1] == 1) {
%deplobj.timeout = ".5";
}
else if(%plyr.packSet[1] == 2) {
%deplobj.timeout = "1";
}
else if(%plyr.packSet[1] == 3) {
%deplobj.timeout = "2";
}
else if(%plyr.packSet[1] == 4) {
%deplobj.timeout = "3";
}
else if(%plyr.packSet[1] == 5) {
%deplobj.timeout = "4";
}

if(%plyr.packSet[2] == 1) {
%deplobj.cankill = 1;
}
else {
%deplobj.cankill = 0;
}

%deplobj.hasslided=0;

if (%plyr.packSet[0]==0)
%deplobj.toggletype=0;

if (%plyr.packSet[0]==1)
%deplobj.toggletype=1;

if (%plyr.packSet[0]==2) {
%deplobj.toggletype=0;
%deplobj.powercontrol=1; //staticshape.cs function StaticShapeData::onGainPowerEnabled |and| function StaticShapeData::onLosePowerDisabled
}                        //for power togle code

if (%plyr.packSet[0]==3) {
%deplobj.toggletype=2;
%deplobj.powercontrol=1;
}
if (%plyr.packSet[0]==4) {
%deplobj.toggletype=3;
%deplobj.powercontrol=1;
}
//collision door
if (%plyr.packSet[0]==5){
%deplobj.toggletype=0;
%deplobj.Collision = true;
%deplobj.lv =0;
}
//Level 1 Door
if (%plyr.packSet[0]==6){
%deplobj.toggletype=0;
%deplobj.Collision = true;
%deplobj.lv =1;
%plyr.client.haslev1[%plyr.client.GUID] = 1;
}
//Level 2 Door
if (%plyr.packSet[0]==7){
%deplobj.toggletype=0;
%deplobj.Collision = true;
%deplobj.lv =2;
%plyr.client.haslev2[%plyr.client.GUID] = 1;
}
//Level 3 Door
if (%plyr.packSet[0]==8){
%deplobj.toggletype=0;
%deplobj.Collision = true;
%deplobj.lv =3;
%plyr.client.haslev3[%plyr.client.GUID] = 1;
}
//owner door
if (%plyr.packSet[0]==9){
%deplobj.toggletype=0;
%deplobj.Collision = true;
%deplobj.lv =4;
}
//admin door
if (%plyr.packSet[0]==10){
%deplobj.toggletype=0;
%deplobj.Collision = true;
%deplobj.lv =5;
}
//super admin door
if (%plyr.packSet[0]==11){
%deplobj.toggletype=0;
%deplobj.Collision = true;
%deplobj.lv =6;
}
//developer door
if (%plyr.packSet[0]==12){
%deplobj.toggletype=0;
%deplobj.Collision = true;
%deplobj.lv =7;
}
%deplobj.canmove = true;

// Power object
checkPowerObject(%deplObj);
	return %deplObj;
}

function Deployeddoor::onDestroyed(%this, %obj, %prevState) {
	if (%obj.isRemoved)
		return;
	%obj.isRemoved = true;
	Parent::onDestroyed(%this, %obj, %prevState);
	$TeamDeployedCount[%obj.team, TdoorDeployable]--;
	remDSurface(%obj);
	%obj.schedule(500,"delete");
	cascade(%obj);
	fireBallExplode(%obj,1);
}

function Deployeddoor::disassemble(%data,%plyr,%hTgt) {
	disassemble(%data,%plyr,%hTgt);
}

function doorDeployableImage::onMount(%data,%obj,%node) {
	%obj.hasdoor = true; // set for mspinecheck
%obj.packSet = 0;
%obj.packSet[0] = 0;
%obj.packSet[1] = 0;
%obj.packSet[2] = 0;
%obj.expertSet = 0;
	displayPowerFreq(%obj);
}

function doorDeployableImage::onUnmount(%data,%obj,%node) {
	%obj.hasdoor = "";
%obj.packSet = 0;
%obj.packSet[0] = 0;
%obj.packSet[1] = 0;
%obj.packSet[2] = 0;
%obj.expertSet = 0;
}
////////////////////
////////////////////
function open(%obj){
if (!isObject(%obj))
   return;
if (%obj.canmove == true){
   %obj.moving    = "open";      //wat direction its moving
   %obj.prevscale =%obj.scale; //scale befor this change
   %obj.closedscale=%obj.scale;//scale it is while fully closed
   %obj.canmove = false;
}
if (getWord(%obj.scale,2)<0.3){
   %obj.issliding = 0;
   %obj.scale =  getWord(%obj.scale,0) SPC getWord(%obj.scale,1) SPC 0.1;
   %obj.settransform(%obj.gettransform());
   %obj.state = "opened";         //last state used for savebuilding
   %obj.openedscale  = %obj.scale;
   if (%obj.toggletype ==0)
      schedule(%obj.timeout*1000,0,"close",%obj,1);
   else
       %obj.canmove = true;
   return;
   }
%obj.scale = getWord(%obj.scale,0) SPC getWord(%obj.scale,1) SPC getWord(%obj.prevscale,2)-0.4;
%obj.settransform(%obj.gettransform());
%obj.prevscale=%obj.scale;
schedule(40,0,"open",%obj);
}
/////////////////////
/////////////////////
function close(%obj,%timeout){
if (!isObject(%obj))
   return;
if (%obj.canmove == true){
   %obj.moving     = "close";      //wat direction its moving
   %obj.prevscale  =%obj.scale; //scale befor this change
   %obj.openedscale=%obj.scale;//scale it is while fully opened
   %obj.canmove = false;
}

if (getWord(%obj.scale,2)>getWord(%obj.closedscale,2)-0.2){
   %obj.issliding = 0;
   %obj.scale =getWord(%obj.scale,0) SPC getWord(%obj.scale,1) SPC getWord(%obj.closedscale,2);
   %obj.settransform(%obj.gettransform());
   %obj.state = "closed";        //last state used for savebuilding
   %obj.closedscale = %obj.scale;
   %obj.canmove = true;
   return;
   }
%obj.scale = getWord(%obj.scale,0) SPC getWord(%obj.scale,1) SPC getWord(%obj.prevscale,2)+0.4;
%obj.settransform(%obj.gettransform());
%obj.prevscale=%obj.scale;
//killer doors (Advanced Door Pack)
%killcheck=ContainerRayCast(%obj.getedge("1 1 1"), %obj.getedge("-1 -1 -1"), $typemasks::Playerobjecttype, %obj);
if (isplayer(%killcheck) && %obj.cankill) {
doorkill(%killcheck);
}
// -End-
schedule(40,0,"close",%obj);
}

function doorkill(%obj) {
   if(!%obj.isBoss) {
      %obj.scriptkill();
      messageall(1,'%1 Tastes the wrath of a door.', %obj.client.name);
   }
}

//MODES

function ChangedoorMode(%this, %PriSec) {
if(%PriSec == 1) {    //Primary
%this.ExpertSet++;
%this.packSet[%this.ExpertSet] = 0;    //Reset Secondary Mode TO Prevent Errors
if (%this.ExpertSet > 2) {
%this.ExpertSet = 0;
}
DisplayDoorInfo(%this,%PriSec);
return;
}
else {            //Secondary
%this.packSet[%this.ExpertSet]++;
//Check Primaries
if(%this.ExpertSet == 0 && %this.packSet[%this.ExpertSet] > 12) {
%this.packSet[%this.ExpertSet] = 0;
}
else if(%this.ExpertSet == 1 && %this.packSet[%this.ExpertSet] > 5) {
%this.packSet[%this.ExpertSet] = 0;
}
else if(%this.ExpertSet == 2 && %this.packSet[%this.ExpertSet] > 1) {
%this.packSet[%this.ExpertSet] = 0;
}
DisplayDoorInfo(%this,%PriSec);
return;
}
}

function DisplayDoorInfo(%plyr, %Var) {
   if(%Var == 1) {
      switch(%plyr.ExpertSet) {
      case 0:
      bottomPrint(%plyr.client,"Advanced Door [P]: Select Door Type",2,1);
      case 1:
      bottomPrint(%plyr.client,"Advanced Door [P]: Select Door Timeout",2,1);
      case 2:
      bottomPrint(%plyr.client,"Advanced Door [P]: Select Door Kill Settings",2,1);
      }
   }
   else if(%Var == 2) {
      switch(%plyr.ExpertSet) {
         case 0:
            switch(%plyr.packSet[0]) {
               case 0:
               bottomPrint(%plyr.client,"Advanced Door [S]: Normal Door",2,1);
               case 1:
               bottomPrint(%plyr.client,"Advanced Door [S]: Toggle Door",2,1);
               case 2:
               bottomPrint(%plyr.client,"Advanced Door [S]: PWR Change Normal Door",2,1);
               case 3:
               bottomPrint(%plyr.client,"Advanced Door [S]: Close When Powered Door",2,1);
               case 4:
               bottomPrint(%plyr.client,"Advanced Door [S]: Open When Powered Door",2,1);
               case 5:
               bottomPrint(%plyr.client,"Advanced Door [S]: Contact Access Door",2,1);
               case 6:
               bottomPrint(%plyr.client,"Advanced Door [S]: Green Level Access Door",2,1);
               case 7:
               bottomPrint(%plyr.client,"Advanced Door [S]: Yellow Level Access Door",2,1);
               case 8:
               bottomPrint(%plyr.client,"Advanced Door [S]: Red Level Access Door",2,1);
               case 9:
               bottomPrint(%plyr.client,"Advanced Door [S]: Owner Contact Access Door",2,1);
               case 10:
               bottomPrint(%plyr.client,"Advanced Door [S]: Admin Contact Access Door",2,1);
               case 11:
               bottomPrint(%plyr.client,"Advanced Door [S]: Super-Admin Contact Access Door",2,1);
               case 12:
               bottomPrint(%plyr.client,"Advanced Door [S]: Developer Contact Access Door",2,1);
            }
         case 1:
            switch(%plyr.packSet[1]) {
               case 0:
               bottomPrint(%plyr.client,"Advanced Door [S]: No Timeout",2,1);
               case 1:
               bottomPrint(%plyr.client,"Advanced Door [S]: 0.5 Second Timeout",2,1);
               case 2:
               bottomPrint(%plyr.client,"Advanced Door [S]: 1 Second Timeout",2,1);
               case 3:
               bottomPrint(%plyr.client,"Advanced Door [S]: 2 Second Timeout",2,1);
               case 4:
               bottomPrint(%plyr.client,"Advanced Door [S]: 3 Second Timeout",2,1);
               case 5:
               bottomPrint(%plyr.client,"Advanced Door [S]: 4 Second Timeout",2,1);
            }
         case 2:
            switch(%plyr.packSet[2]) {
               case 0:
               bottomPrint(%plyr.client,"Advanced Door [S]: Don't Kill",2,1);
               case 1:
               bottomPrint(%plyr.client,"Advanced Door [S]: Kill",2,1);
            }
      }
   }
}
