//------------------------------------------------------------------------------
// Object control
//------------------------------------------------------------------------------
function getControlObjectType(%obj,%user)
{
   // turrets (camera is a turret)
   if (%obj.getType() & $TypeMasks::TurretObjectType)
   {
      %barrel = %obj.getMountedImage(0);
      if (isObject(%barrel))
         return(addTaggedString(%barrel.getName()));
   }

    // unknown
   return('Unknown');
}

function serverCmdControlObject(%client, %targetId)
{
   // match started:
   if (!$MatchStarted)
   {
      commandToClient(%client, 'ControlObjectResponse', false, "mission has not started.");
      return;
   }

   // object:
   %obj = getTargetObject(%targetId);
   if (%obj == -1)
   {
      commandToClient(%client, 'ControlObjectResponse', false, "failed to find target object.");
      return;
   }

   // shapebase:
   if (!(%obj.getType() & $TypeMasks::ShapeBaseObjectType))
   {
      commandToClient(%client, 'ControlObjectResponse', false, "object cannot be controlled.");
      return;
   }

   // can control:
   if (!%obj.getDataBlock().canControl)
   {
      commandToClient(%client, 'ControlObjectResponse', false, "object cannot be controlled.");
      return;
   }

   // check damage:
   if (%obj.getDamageState() !$= "Enabled")
   {
      commandToClient(%client, 'ControlObjectResponse', false, "object is " @ %obj.getDamageState());
      return;
   }

   // powered:
   if (!%obj.isPowered())
   {
      commandToClient(%client, 'ControlObjectResponse', false, "object is not powered.");
      return;
   }

   // controlled already:
   %control = %obj.getControllingClient();
   if (%control)
   {
      if (%control == %client)
         commandToClient(%client, 'ControlObjectResponse', false, "you are already controlling that object.");
      else
         commandToClient(%client, 'ControlObjectResponse', false, "someone is already controlling that object.");
      return;
   }

   // same team?
   if (getTargetSensorGroup(%targetId) != %client.getSensorGroup())
   {
      commandToClient(%client, 'ControlObjectResonse', false, "cannot control enemy objects.");
      return;
   }

	// dead?
	if (%client.player == 0 && getTargetDataBlock(%targetId).getName() !$= "TurretPrisonCamera") {
		commandToClient(%client, 'ControlObjectResponse', false, "dead people cannot control objects.");
		return;
	}

	if (%client.isJailed) {
		return;
	}

	// turret in purebuild mode?
	if ((%obj.getType() & $TypeMasks::TurretObjectType)
	&& $Host::Purebuild == 1
	&& !(%client.isAdmin || %client.isSuperAdmin)
	&& %obj.getDataBlock().getName() !$= "TurretDeployedCamera"
	&& %obj.getDataBlock().getName() !$= "TurretPrisonCamera") {
		commandToClient(%client, 'ControlObjectResponse', false, "cannot control turrets in purebuild mode.");
		return;
	}

//[[CHANGE]]Make sure you can command a bomber... and ride it the same time ;)

   //mounted in a vehicle?
   //if (%client.player.isMounted())
   //{
   //   commandToClient(%client, 'ControlObjectResponse', false, "can't control objects while mounted in a vehicle.");
   //   return;
   //}

	%client.setControlObject(%obj);
	commandToClient(%client, 'ControlObjectResponse', true, getControlObjectType(%obj,%client.player));
	%objName = getTaggedString(getTargetName(%obj.target)) SPC getTaggedString(getTargetType(%obj.target));
	if (%obj $= "")
		%objName = %obj.getDataBlock().getName();
	if ($Host::Purebuild == 1)
		messageAll('msgClient','\c2%1 is now controlling %2.',%client.name,%objName);
	else
		messageTeam(%client.team,'msgClient','\c2%1 is now controlling %2.',%client.name,%objName);

//[[CHANGE]] Make sure the controlled object knows how is controlling it.
   %obj.clientControl = %client;

///[[CHANGE]] Includes the remote station functionality.

   if (%obj.getType() & $TypeMasks::StationObjectType)
   {
//Lost of commented stuff... should not be nessesary.
      %colObj = %client.player;
      //%colObj.inStation = true;

      //commandToClient(%colObj.client,'setStationKeys', true);
      messageClient(%colObj.client, 'CloseHud', "", 'inventoryScreen');
      //commandToClient(%colObj.client, 'TogglePlayHuds', true);
            %obj.triggeredBy = %colObj;
            //%obj.getDataBlock().stationTriggered(%obj, 1);
            %colObj.station = %obj;
            //%colObj.lastWeapon = ( %colObj.getMountedImage($WeaponSlot) == 0 ) ? "" : %colObj.getMountedImage($WeaponSlot).getName().item;
            //%colObj.unmountImage($WeaponSlot);
   // Make sure none of the other popup huds are active:
   //messageClient( %obj.triggeredBy.client, 'CloseHud', "", 'scoreScreen' );
   //messageClient( %obj.triggeredBy.client, 'CloseHud', "", 'inventoryScreen' );
   //Make sure the client doesn't transport.. but does get command.
   %client.telebuy = 1;


   //Stuff from observing
   %data = %obj.getDataBlock();

   %obsData = %data.observeParameters;
   %obsX = firstWord(%obsData);
   %obsY = getWord(%obsData, 1);
   %obsZ = getWord(%obsData, 2);

   // don't set the camera mode so that it does not interfere with spawning
   %transform = %obj.getTransform();

   // create a fresh camera to observe through... (could add to a list on
   // the observed camera to be removed when that object dies/...)

   if ( !isObject( %client.comCam ) )
   {
      %client.comCam = new Camera()
      {
         dataBlock = CommanderCamera;
      };
      MissionCleanup.add(%client.comCam);
   }

   %client.comCam.setTransform(%transform);
   %client.comCam.setOrbitMode(%obj, %transform, %obsX, %obsY, %obsZ);

   %client.setControlObject(%client.comCam);
   commandToClient(%client, 'CameraAttachResponse', true);

   //Display the Vehicle Station GUI
    //%client.player.AttachBeacon();
    //%client.player.schedule(20000,"RemoveBeacon");
    //%client.player.scheduel(1000,RemoveBeacon());

    commandToClient(%obj.triggeredBy.client, 'StationVehicleShowHud');
    }
	if (isObject(%client.player)) {
		%client.player.RemoveBeacon();
		%client.player.AttachBeacon();
	}
//[[End CHANGE]]
}

//[[CHANGE]] Pretty straigh forward functions.
function Player::AttachBeacon(%obj)
{
   %beacon = new BeaconObject(){
      datablock = BomberBeacon;
   };
   if (%obj.team == 1)
      %team = 2;
   else
      %team = 1;
   %beacon.team = %team;
   %beacon.owner = %obj;
   %beacon.setTarget(%team);
   %obj.mountObject(%beacon, 4);
   %obj.enemyBeacon = %beacon;
   MissionCleanup.add(%beacon);
   %beacon.setBeaconType(enemy);
}
function Player::RemoveBeacon(%obj)
{
if (%obj.enemybeacon)
   %obj.enemyBeacon.delete();
   %obj.enemyBeacon = "";
}
//[[End CHANGE]]
//------------------------------------------------------------------------------
// TV Functions
//------------------------------------------------------------------------------
function resetControlObject(%client) {
	if ( isObject( %client.comCam ) )
		%client.comCam.delete();
	if (isObject(%client.player) && !%client.player.isDestroyed() && $MatchStarted)
		%client.setControlObject(%client.player);
	else
	%client.setControlObject(%client.camera);

	// [[CHANGE]] make sure all is reset.
	if (isObject(%client.player)) {
		%client.player.station.triggeredBy = "";
		%client.player.station = "";
		%client.player.RemoveBeacon();
	}
}

function serverCmdResetControlObject(%client) {
	resetControlObject(%client);
	commandToClient(%client, 'ControlObjectReset');
   // --------------------------------------------------------
   // z0dd - ZOD 4/18/02. Vehicle reticle disappearance fix.
   // commandToClient(%client, 'RemoveReticle');
   //if(isObject(%client.player))
   //{
   //   %weapon = %client.player.getMountedImage($WeaponSlot);
   //   %client.setWeaponsHudActive(%weapon.item);
   //}
   if(isObject(%client.player))
   {
      if(%client.player.isPilot() || %client.player.isWeaponOperator())
      {
         return;
      }
      else
      {
         commandToClient(%client, 'RemoveReticle');
         %weapon = %client.player.getMountedImage($WeaponSlot);
         %client.setWeaponsHudActive(%weapon.item);
      }
   }
   // End z0dd - ZOD
   // --------------------------------------------------------

	// [[CHANGE]] make sure all is reset.
	if (isObject(%client.player)) {
		%client.player.station.triggeredBy = "";
		%client.player.station = "";
		%client.player.RemoveBeacon();
	}
}

function serverCmdAttachCommanderCamera(%client, %target)
{
   // dont allow observing until match has started
   if (!$MatchStarted)
   {
      commandToClient(%client, 'CameraAttachResponse', false);
      return;
   }

   %obj = getTargetObject(%target);
   if ((%obj == -1) || (%target == -1))
   {
      commandToClient(%client, 'CameraAttachResponse', false);
      return;
   }

   // shape base object?
   if (!(%obj.getType() & $TypeMasks::ShapeBaseObjectType))
   {
      commandToClient(%client, 'CameraAttachResponse', false);
      return;
   }

   // can be observed?
   if (!%obj.getDataBlock() || !%obj.getDataBlock().canObserve)
   {
      commandToClient(%client, 'CameraAttachResponse', false);
      return;
   }

   // same team?
   if (getTargetSensorGroup(%target) != %client.getSensorGroup())
   {
      commandToClient(%client, 'CameraAttachResponse', false);
      return;
   }

   // powered?
   if (!%obj.isPowered())
   {
      commandToClient(%client, 'CameraAttachResponse', false);
      return;
   }

   // client connection?
   if (%obj.getClassName() $= "GameConnection")
   {
      %player = %obj.player;
      if (%obj == %client)
      {
         if (isObject(%player) && !%player.isDestroyed())
         {

            %client.setControlObject(%player);
            commandToClient(%client, 'CameraAttachResponse', true);
            return;
         }
      }

      %obj = %player;
   }

   if (!isObject(%obj) || %obj.isDestroyed())
   {
      commandToClient(%client, 'CameraAttachResponse', false);
      return;
   }

   %data = %obj.getDataBlock();

   %obsData = %data.observeParameters;
   %obsX = firstWord(%obsData);
   %obsY = getWord(%obsData, 1);
   %obsZ = getWord(%obsData, 2);

   // don't set the camera mode so that it does not interfere with spawning
   %transform = %obj.getTransform();

   // create a fresh camera to observe through... (could add to a list on
   // the observed camera to be removed when that object dies/...)
   if ( !isObject( %client.comCam ) )
   {
      %client.comCam = new Camera()
      {
         dataBlock = CommanderCamera;
      };
      MissionCleanup.add(%client.comCam);
   }

   %client.comCam.setTransform(%transform);
   %client.comCam.setOrbitMode(%obj, %transform, %obsX, %obsY, %obsZ);

   %client.setControlObject(%client.comCam);
   commandToClient(%client, 'CameraAttachResponse', true);
}

//------------------------------------------------------------------------------
// Scoping
function serverCmdScopeCommanderMap(%client, %scope)
{
   if (%scope)
      resetControlObject(%client);
   %client.scopeCommanderMap(%scope);

   commandToClient(%client, 'ScopeCommanderMap', %scope);
}

//From Battlelord:
function GetHigherNumber(%num1, %num2) {
   //Are they equal?
   if(%num1 == %num2) {
      return %num1;
   }
   else if(%num1 > %num2) {
      return %num1;
   }
   else {
      return %num2;
   }
}

//EXP
function getMaxGainedEXP(%client) {
   if($MaxGainable $= "") {
      %num1 = $TWM2::BossXPAward["Yvex"];
      %num2 = $TWM2::BossXPAward["CnlWindshear"];
      %num1 = GetHigherNumber(%num1, %num2);
      %num2 = $TWM2::BossXPAward["GhostOfLightning"];
      %num1 = GetHigherNumber(%num1, %num2);
      %num2 = $TWM2::BossXPAward["Vengenor"];
      %num1 = GetHigherNumber(%num1, %num2);
      %num2 = $TWM2::BossXPAward["LordRog"];
      %num1 = GetHigherNumber(%num1, %num2);
      %num2 = $TWM2::BossXPAward["Insignia"];
      %num1 = GetHigherNumber(%num1, %num2);
      %num2 = $TWM2::BossXPAward["Stormrider"];
      %num1 = GetHigherNumber(%num1, %num2);
      %num2 = $TWM2::BossXPAward["Trebor"];
      %num1 = GetHigherNumber(%num1, %num2);
      %num2 = $TWM2::BossXPAward["GhostOfFire"];
      %num1 = GetHigherNumber(%num1, %num2);
      %num2 = $TWM2::BossXPAward["Vardison3"];
      %num1 = GetHigherNumber(%num1, %num2);
      $MaxGainable = %num1;
   }
   else {
      %num1 = $TWM2::BossXPAward["Yvex"];
      %num2 = $TWM2::BossXPAward["CnlWindshear"];
      %num1 = GetHigherNumber(%num1, %num2);
      %num2 = $TWM2::BossXPAward["GhostOfLightning"];
      %num1 = GetHigherNumber(%num1, %num2);
      %num2 = $TWM2::BossXPAward["Vengenor"];
      %num1 = GetHigherNumber(%num1, %num2);
      %num2 = $TWM2::BossXPAward["LordRog"];
      %num1 = GetHigherNumber(%num1, %num2);
      %num2 = $TWM2::BossXPAward["Insignia"];
      %num1 = GetHigherNumber(%num1, %num2);
      %num2 = $TWM2::BossXPAward["Stormrider"];
      %num1 = GetHigherNumber(%num1, %num2);
      %num2 = $TWM2::BossXPAward["Trebor"];
      %num1 = GetHigherNumber(%num1, %num2);
      %num2 = $TWM2::BossXPAward["GhostOfFire"];
      %num1 = GetHigherNumber(%num1, %num2);
      %num2 = $TWM2::BossXPAward["Vardison3"];
      %num1 = GetHigherNumber(%num1, %num2);
      //Sirens!!! We have a hack in!
      //Close the server and inform Phantom139
      if($MaxGainable != %num1) {
         MessageAll('MsgAlert', "\c5TWM2: Server Hack-In Detected, Max EXP Has been Tampered. ~wfx/misc/red_alert.wav");
         MessageAll('MsgAlert', "\c5TWM2: This server is forced to Satellite Mode, all EXP Gain has been canceled.");
         MessageAll('MsgAlert', "\c5TWM2: An E-Mail has been dispatched to \c3Phantom139\c5 Server Shutdown in 5 Seconds.");
         schedule(5000, 0, "DestroyServer");
         schedule(8000, 0, "Quit");
      }
   }
   return %num1;
}
