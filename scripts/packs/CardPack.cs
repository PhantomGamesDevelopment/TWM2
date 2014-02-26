$TeamDeployableMax[CardPackDeployable]      = 9999;

datablock StaticShapeData(DeployedCardPack) : StaticShapeDamageProfile
{
   className = Sensor;
   shapeFile = "stackable2s.dts";
   maxDamage = 0.6;
   destroyedLevel = 0.6;
   disabledLevel = 0.4;
   explosion = DeployablesExplosion;
   dynamicType = $TypeMasks::SensorObjectType;

   deployedObject = true;

   cmdCategory = "DSupport";
   cmdIcon = CMDSensorIcon;
   cmdMiniIconName = "commander/MiniIcons/com_deploymotionsensor";
   targetNameTag = 'Door Card Pack';
   targetTypeTag = '';
   deployAmbientThread = true;

   debrisShapeName = "debris_generic_small.dts";
   debris = DeployableDebris;
   heatSignature = 0;
};

datablock ShapeBaseImageData(CardPackDeployableImage)
{
   shapeFile = "pack_deploy_sensor_motion.dts";
   item = CardPackDeployable;
   mountPoint = 1;
   offset = "0 0 0";
   deployed = DeployedCardPack;

   stateName[0] = "Idle";
   stateTransitionOnTriggerDown[0] = "Activate";

   stateName[1] = "Activate";
   stateScript[1] = "onActivate";
   stateTransitionOnTriggerUp[1] = "Idle";

   maxDepSlope = 360;
   deploySound = MotionSensorDeploySound;
   emap = true;
   heatSignature = 1;

   minDeployDis                       =  0.5;
   maxDeployDis                       =  5.0;  //meters from body
};

datablock ItemData(CardPackDeployable)
{
   className = Pack;
   catagory = "Deployables";
   shapeFile = "pack_deploy_sensor_motion.dts";
   mass = 2.0;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 1;
   rotate = false;
   image = "CardPackDeployableImage";
   pickUpName = "a Card Pack pack";

   computeCRC = true;
   emap = true;
   heatSignature = 0;

   //maxSensors = 3;
   maxSensors = 9999;
};

function CardPackDeployable::onPickup(%this, %obj, %shape, %amount) {
	// created to prevent console errors
}

function CardPackDeployableImage::onDeploy(%item, %plyr, %slot) {
	%className = "StaticShape";

	%playerVector = vectorNormalize(-1 * getWord(%plyr.getEyeVector(),1) SPC getWord(%plyr.getEyeVector(),0) SPC "0");
	%item.surfaceNrm2 = %playerVector;
	%rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);

	%deplObj = new (%className)() {
		dataBlock = %item.deployed;
	};

	// set orientation
	%deplObj.setTransform(%item.surfacePt SPC %rot);

	// set the recharge rate right away
	if (%deplObj.getDatablock().rechargeRate)
		%deplObj.setRechargeRate(%deplObj.getDatablock().rechargeRate);

	// set team, owner, and handle
	%deplObj.team = %plyr.client.Team;
	%deplObj.setOwner(%plyr);

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

	addDSurface(%item.surface,%deplObj);

	// take the deployable off the player's back and out of inventory
	//%plyr.unmountImage(%slot);
	//%plyr.decInventory(%item.item, 1);
    //apply new settings to the pack
    %deplObj.NameHolder = %plyr.client.namebase;
    %deplObj.GUIDHolder = %plyr.client.GUID;
    if(%plyr.packSet[0] == 0) {
       %deplObj.cardColor = 1;
       %DepldName1 = "Green";
    }
    else if(%plyr.packSet[0] == 1) {
       %deplObj.cardColor = 2;
       %DepldName1 = "Yellow";
    }
    else if(%plyr.packSet[0] == 2) {
       %deplObj.cardColor = 3;
       %DepldName1 = "Red";
    }

    if(%plyr.packSet[1] == 0) {
       %deplObj.CardSetting = 1;
       %DepldName2 = "";
    }
    else if(%plyr.packSet[1] == 1) {
       %deplObj.CardSetting = 2;
       %DepldName2 = "Remover";
    }

//    setTargetName(%deplObj.target, ""@%DepldName1@" Card "@%DepldName2@"");

	return %deplObj;
}

function DeployedCardPack::onCollision(%data,%obj,%col) {
	if (%col.getClassName() !$= "Player") {
		return;
    }
    if(%col.client.guid == %obj.GUIDHolder) {
       messageclient(%col.client, 'MsgClient', "\c2The Device Does not Respond To It's Owner.");
       return;
    }
    //
    if(%obj.CardSetting == 1) {
          if(%obj.cardColor == 1) {
             messageclient(%col.client, 'MsgClient', "\c2You have recived a Green Card to "@%obj.NameHolder@"'s Doors.");
             %col.client.haslev1[%obj.GUIDHolder] = 1;
          }
          else if(%obj.cardColor == 2) {
             messageclient(%col.client, 'MsgClient', "\c2You have recived a Yellow Card to "@%obj.NameHolder@"'s Doors.");
             %col.client.haslev2[%obj.GUIDHolder] = 1;
          }
          else if(%obj.cardColor == 3) {
             messageclient(%col.client, 'MsgClient', "\c2You have recived a Red Card to "@%obj.NameHolder@"'s Doors.");
             %col.client.haslev3[%obj.GUIDHolder] = 1;
          }
    }
    else if(%obj.CardSetting == 2) {
          if(%obj.cardColor == 1) {
             messageclient(%col.client, 'MsgClient', "\c2The Device Takes your Green Card to "@%obj.NameHolder@"'s Doors.");
             %col.client.haslev1[%obj.GUIDHolder] = 0;
          }
          else if(%obj.cardColor == 2) {
             messageclient(%col.client, 'MsgClient', "\c2The Device Takes your Yellow Card to "@%obj.NameHolder@"'s Doors.");
             %col.client.haslev2[%obj.GUIDHolder] = 0;
          }
          else if(%obj.cardColor == 3) {
             messageclient(%col.client, 'MsgClient', "\c2The Device Takes your Red Card to "@%obj.NameHolder@"'s Doors.");
             %col.client.haslev3[%obj.GUIDHolder] = 0;
          }
    }
}

function CardPackDeployableImage::onMount(%data, %obj, %node) {
%obj.hasCard = true; // set for blastcheck
%obj.packSet = 0;
%obj.packSet[0] = 0;  //Card Color
%obj.packSet[1] = 0;  //Give/take
%obj.expertSet = 0;
displayPowerFreq(%obj);

}

function CardPackDeployableImage::onUnmount(%data, %obj, %node) {
%obj.hasCard = false;
%obj.packSet = 0;
%obj.packSet[0] = 0;
%obj.packSet[1] = 0;
%obj.expertSet = 0;
}

function ChangeCardMode(%this, %PriSec) {
if(%PriSec == 1) {    //Primary
%this.ExpertSet++;
%this.packSet[%this.ExpertSet] = 0;    //Reset Secondary Mode TO Prevent Errors
if (%this.ExpertSet > 1) {
%this.ExpertSet = 0;
}
DisplayCardInfo(%this,%PriSec);
return;
}
else {            //Secondary
%this.packSet[%this.ExpertSet]++;
//Check Primaries
if(%this.ExpertSet == 0 && %this.packSet[%this.ExpertSet] > 2) {
%this.packSet[%this.ExpertSet] = 0;
}
else if(%this.ExpertSet == 1 && %this.packSet[%this.ExpertSet] > 1) {
%this.packSet[%this.ExpertSet] = 0;
}
DisplayCardInfo(%this,%PriSec);
return;
}
}

function DisplayCardInfo(%plyr, %Var) {
   if(%Var == 1) {
      switch(%plyr.ExpertSet) {
      case 0:
      bottomPrint(%plyr.client,"Card Pack [P]: Select Card Color",2,1);
      case 1:
      bottomPrint(%plyr.client,"Card Pack [P]: Select Card Settings",2,1);
      }
   }
   else if(%Var == 2) {
      switch(%plyr.ExpertSet) {
         case 0:
            switch(%plyr.packSet[0]) {
               case 0:
               bottomPrint(%plyr.client,"Card Pack [S]: Green Card",2,1);
               case 1:
               bottomPrint(%plyr.client,"Card Pack [S]: Yellow Card",2,1);
               case 2:
               bottomPrint(%plyr.client,"Card Pack [S]: Red Card",2,1);
            }
         case 1:
            switch(%plyr.packSet[1]) {
               case 0:
               bottomPrint(%plyr.client,"Card Pack [S]: Give Card",2,1);
               case 1:
               bottomPrint(%plyr.client,"Card Pack [S]: Strip Card",2,1);
            }
      }
   }
}

