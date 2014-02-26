function GameConnection::specifyLastContructionAction(%this, %action, %obj, %info) {
   if(%this $= "" || !ClientGroup.isMember(%this)) {
      return;
   }

   %this.lastAction = %action;
   %this.lastObj = %obj;
   %this.lastCN = %obj.getClassName();
   %this.lastDB = %obj.getDatablock().getName();
   %this.lastPieceDetails = %info;
}
function GameConnection::undoLastConstructionAction(%this) {
   if(%this $= "" || !ClientGroup.isMember(%this)) {
      return;
   }

   %action = %this.lastAction;
   if(!isSet(%action)) {
      return;
   }
   switch$(%action) {
      case "delete":
         %buffer = "%this.lastObj = new ("@%this.lastCN@") () { datablock="@%this.lastDB@";"@%this.lastpieceDetails@" };";
         if(isSet(%buffer)) {
            eval(%buffer);
         }
         %this.specifyLastContructionAction("undelete", %this.lastObj, "");
      case "undelete":
         %this.lastObj.getDatablock().disassemble(%this.player, %this.lastObj);
   }
}
function pullObjectDetails(%object) {
   if(!isObject(%object)) {
      return "";
   }
   %i = 1;
   while(isSet($Detail::Save[%i])) {
      %det = %object@"."@$Detail::Save[%i];
      eval("$Undo::SaveStream["@%object@"] = "@%det@";");
      if(isSet($Undo::SaveStream[%object])) {
         %write = ""@$Detail::Save[%i]@" = \""@$Undo::SaveStream[%object]@"\"";
         %table = %table@%write@";";
      }
      %i++;
   }
   return %table;
}

//For Construction Tool
function BuildDeconList() {
   echo("* Building Construction Tool Deconstruction List *");
   $ReverseDeployItem[DeployedStationInventory] = InventoryDeployable;
   $ReverseDeployItem[DeployedMotionSensor] = MotionSensorDeployable;
   $ReverseDeployItem[DeployedPulseSensor] = PulseSensorDeployable;
   $ReverseDeployItem[TurretDeployedOutdoor] = TurretOutdoorDeployable;
   $ReverseDeployItem[TurretDeployedFloorIndoor] = TurretIndoorDeployable;
   $ReverseDeployItem[TurretDeployedWallIndoor] = TurretIndoorDeployable;
   $ReverseDeployItem[TurretDeployedCeilingIndoor] = TurretIndoorDeployable;
   $ReverseDeployItem[TurretDeployedBase] = TurretBasePack;
   $ReverseDeployItem[TurretDeployedCamera] = CameraGrenade;
   $ReverseDeployItem[TelePadDeployedBase] = TelePadPack;
   $ReverseDeployItem[DeployedSpine] = "poof spineDeployable";
   $ReverseDeployItem[DeployedSpine2] = "poof spineDeployable";
   $ReverseDeployItem[DeployedSpine3] = "poof spineDeployable";
   $ReverseDeployItem[DeployedWoodSpine] = "poof spineDeployable";
   $ReverseDeployItem[Deployedfloor] = "poof floorDeployable";
   $ReverseDeployItem[Deployedwall] = "poof wallDeployable";
   $ReverseDeployItem[Deployedwwall] = "poof wwallDeployable";
   $ReverseDeployItem[Deployedmspine] = "poof mspineDeployable";
   $ReverseDeployItem[DeployedDoor] = "poof DoorDeployable";
   $ReverseDeployItem[DeployedEnergizer] = EnergizerDeployable;
   $ReverseDeployItem[Deployedmspinering] = "poof nothing";
   $ReverseDeployItem[DiscTurretDeployed] = DiscTurretDeployable;
   $ReverseDeployItem[StationInventory] = LargeInventoryDeployable;
   $ReverseDeployItem[DeployedJumpad] = JumpadDeployable;
   $ReverseDeployItemDeployedBeacon = Beacon;
   $ReverseDeployItem[DeployedLogoProjector] = LogoProjectorDeployable;
   $ReverseDeployItem[LaserDeployed] = TurretLaserDeployable;
   $ReverseDeployItem[MissileRackTurretDeployed] = TurretMissileRackDeployable;
   $ReverseDeployItem[SensorMediumPulse] = MediumSensorDeployable;
   $ReverseDeployItem[SensorLargePulse] = LargeSensorDeployable;
   $ReverseDeployItem[DeployedLightBase] = "LightDeployable";
   $ReverseDeployItem[DeployedTripwire] = "TripwireDeployable";
   $ReverseDeployItem[DeployedEscapePod] = "EscapePodDeployable";
   $ReverseDeployItem[DeployedZSpawnBase] = "poof";
   $ReverseDeployItem[EmitterDep] = "poof EmitterDepPack";
   $ReverseDeployItem[AudioDep] = "poof AudioDepPack";
   $ReverseDeployItem[DispenserDep] = "poof DispenserDepPack";

   $ReverseDeployItem[DeployedCardPack] = "CardPackDeployable";
   $ReverseDeployItem[SpawnPointDeployedBase] = "SpawnPointPack";
   
   $ReverseDeployItem[DeployableVehiclePadBottom] = "VehiclePadPack";
   $ReverseDeployItem[DeployableVehiclePad] = "VehiclePadPack";
   $ReverseDeployItem[DeployableVehicleStation] = "VehiclePadPack";
   $ReverseDeployItem[DeployableVehiclePad2] = "VehiclePadPack";

   //[most]

   for (%i = 0;%i < 21;%i++) {
      $ReverseDeployItemDeployedForcefield[%i] = "ForceFieldDeployable";
   }
   for (%i = 0;%i < 5;%i++) {
	  $ReverseDeployItemDeployedGravityField[%i] = "GravityFieldDeployable";
   }
   for (%i = 0;%i < 14;%i++) {
	  $ReverseDeployItemDeployedTree[%i] = "TreeDeployable";
   }
   for (%i = 0;%i < 13;%i++) {
	  $ReverseDeployItemDeployedCrate[%i] = "CrateDeployable";
   }
   for (%i = 0;%i < 17;%i++) {
	  $ReverseDeployItemDeployedDecoration[%i] = "DecorationDeployable";
   }
   // power
   $ReverseDeployItem[GeneratorLarge] = GeneratorDeployable;
   $ReverseDeployItem[SolarPanel] = SolarPanelDeployable;
   $ReverseDeployItem[DeployedSwitch] = SwitchDeployable;
   // DeconTarget
   $ReverseDeployItem[DeployedLTarget] = "poof";
}

$Detail::Save[1] = "forthelol";
$Detail::Save[2] = "ownerGUID";
$Detail::Save[3] = "position";
$Detail::Save[4] = "rotation";
$Detail::Save[5] = "scale";
$Detail::Save[6] = "team";
$Detail::Save[7] = "needsFit";
$Detail::Save[8] = "grounded";
$Detail::Save[9] = "deployed";
$Detail::Save[10] = "impulse";
$Detail::Save[11] = "velocityMod";
$Detail::Save[12] = "gravityMod";
$Detail::Save[13] = "appliedForce";
$Detail::Save[14] = "powerFreq";
$Detail::Save[15] = "isSwitchedOff";
$Detail::Save[16] = "switchRadius";
$Detail::Save[17] = "holoBlock";
$Detail::Save[18] = "noSlow";
$Detail::Save[19] = "static";
$Detail::Save[20] = "timed";
$Detail::Save[21] = "beamRange";
$Detail::Save[22] = "tripMode";
$Detail::Save[23] = "fieldMode";
$Detail::Save[24] = "nametoset";
$Detail::Save[25] = "SwitchTimer";
$Detail::Save[26] = "cankill";
$Detail::Save[27] = "canmove";
$Detail::Save[28] = "closedscale";
$Detail::Save[29] = "hasslided";
$Detail::Save[30] = "isdoor";
$Detail::Save[31] = "issliding";
$Detail::Save[32] = "lv";
$Detail::Save[33] = "moving";
$Detail::Save[34] = "openedscale";
$Detail::Save[35] = "powercontrol";
$Detail::Save[36] = "prevscale";
$Detail::Save[37] = "state";
$Detail::Save[38] = "timeout";
$Detail::Save[39] = "toggletype";
$Detail::Save[40] = "frequency";
$Detail::Save[41] = "teleMode";
$Detail::Save[42] = "isPersonal";
$Detail::Save[43] = "NameHolder";
$Detail::Save[44] = "GUIDHolder";
$Detail::Save[45] = "cardColor";
$Detail::Save[46] = "CardSetting";
$Detail::Save[47] = "isSeeker";
$Detail::Save[48] = "initialBarrel";
$Detail::Save[49] = "lTarget";
$Detail::Save[50] = "lMain";
$Detail::Save[51] = "left";
$Detail::Save[52] = "right";
$Detail::Save[53] = "wp";


//Used for functions dealing with object moving, scaling, ect.
function isLegalPos(%client, %position, %radius) {
   %mask = $TypeMasks::StationObjectType | $TypeMasks::GeneratorObjectType
    | $TypeMasks::SensorObjectType | $TypeMasks::TurretObjectType
    | $TypeMasks::ForceFieldObjectType | $TypeMasks::StaticObjectType;

   InitContainerRadiusSearch(%position, %radius, %mask);
   while ((%hitObj = containerSearchNext()) != 0) {
      if(%hitObj.squareSize !$= "") {
         //ignore terrain
      }
      else {
         %owner = %hitObj.getOwner();

         if(%owner != %client) {
            //echo(&hitObj.getOwner());
            if(%owner.pieceSecured) {
               //secure piece that's not mine, we have an illegal position
               return 0;
            }
         }
      }
   }
   return 1;
}

//This function actually applies piece security functions to the object.
//It is called by most if not all object move/scale operations
function PostOperationCheck(%piece) {
   %owner = %piece.getOwner();
   %position = %piece.getPosition();
   //apply the check
   if(!isLegalPos(%owner, %position, 25) && !%owner.isSuperAdmin) {
      messageClient(%owner, 'msgCli', "\c3ERROR: After Edit Position is too close to secured pieces, deleting");
      %piece.getDatablock().disassemble("", %piece); //delete the piece in an non-/undo manner
   }
   //no god boxes
   if($TWM2::GodBoxDisabled) {
      //get the piece's size
      %size = %piece.getRealSize();
      %x = getWord(%size, 0);
      %y = getWord(%size, 1);
      %z = getWord(%size, 2);
      //now we perform checking of radius using the three scale sizes
      //if all three find objects, it must exist within the box bounds
      //and therefore is illegal

      //check 'x'
      InitContainerRadiusSearch(%piece.getWorldBoxCenter(), %x, $TypeMasks::StaticShapeObjectType | $TypeMasks::ForceFieldObjectType);
      while ((%potentialTarget = ContainerSearchNext()) != 0) {
         if(%potentialTarget != %piece) {
            %badX = true;
         }
      }
      //check 'y'
      InitContainerRadiusSearch(%piece.getWorldBoxCenter(), (%y * 0.75), $TypeMasks::StaticShapeObjectType | $TypeMasks::ForceFieldObjectType);
      while ((%potentialTarget = ContainerSearchNext()) != 0) {
         if(%potentialTarget != %piece) {
            %badY = true;
         }
      }
      //check 'z'
      InitContainerRadiusSearch(%piece.getWorldBoxCenter(), %z, $TypeMasks::StaticShapeObjectType | $TypeMasks::ForceFieldObjectType);
      while ((%potentialTarget = ContainerSearchNext()) != 0) {
         if(%potentialTarget != %piece) {
            %badZ = true;
         }
      }
      
      if(%badX && %badY && %badZ) {
         //bad.
         messageClient(%owner, 'msgCli', "\c3ERROR: Enclosed piece configurations have been disabled by the host.");
         %piece.getDatablock().disassemble("", %piece); //delete the piece in an non-/undo manner
      }
      
   }
   updatePieceSettings(%piece);
}

function updatePieceSettings(%object) {

	if (isObject(%object.pzone))
		%object.pzone.setTransform(%object.getTransform());
	if (isObject(%object.trigger))
		adjustTrigger(%object);
	if (isObject(%object.beam))
		tp_adjustBeam(%object);
	if (isObject(%object.holo))
		adjustHolo(%object);
	if (isObject(%object.light))
		adjustLight(%object);
	if (isObject(%object.lMain))
		adjustLMain(%object);
	if (%object.getDataBlock().needsPower) {
		%object.powerCount = "";
		checkPowerObject(%object);
	}

   %block = %object.getDatablock().getName();
   switch$(%block) {
      //specific pieces require these functions to be called, in which case, our function
      //here can handle them
      case "":
      case "":
   }
}

