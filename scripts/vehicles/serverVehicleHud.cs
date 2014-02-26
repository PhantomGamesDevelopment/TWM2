// bill - menu definitions
// Each menu has a name beginning with mnu - this defines the number of entries
$VehicleMenuNumEntries["mnuMain"] = 7;
// mnuName, index, field = data
// submenu - whether or not it is a submenu
// datablock - the associated datablock
// displayed - what should be displayed
$VehicleMenu["mnuMain", 0, "submenu"] = true;
$VehicleMenu["mnuMain", 0, "datablock"] = mnuLightFlyers;
$VehicleMenu["mnuMain", 0, "displayed"] = "Light Flyers -->";
$VehicleMenu["mnuMain", 1, "submenu"] = true;
$VehicleMenu["mnuMain", 1, "datablock"] = mnuAssaultFlyers;
$VehicleMenu["mnuMain", 1, "displayed"] = "Assault Flyers -->";
$VehicleMenu["mnuMain", 2, "submenu"] = true;
$VehicleMenu["mnuMain", 2, "datablock"] = mnuLightGround;
$VehicleMenu["mnuMain", 2, "displayed"] = "Light Ground -->";
$VehicleMenu["mnuMain", 3, "submenu"] = true;
$VehicleMenu["mnuMain", 3, "datablock"] = mnuHeavyGround;
$VehicleMenu["mnuMain", 3, "displayed"] = "Heavy Ground -->";
$VehicleMenu["mnuMain", 4, "submenu"] = true;
$VehicleMenu["mnuMain", 4, "datablock"] = mnuTransports;
$VehicleMenu["mnuMain", 4, "displayed"] = "Transports -->";
$VehicleMenu["mnuMain", 5, "submenu"] = true;
$VehicleMenu["mnuMain", 5, "datablock"] = mnuDeployables;
$VehicleMenu["mnuMain", 5, "displayed"] = "Deployables -->";
$VehicleMenu["mnuMain", 6, "submenu"] = true;
$VehicleMenu["mnuMain", 6, "datablock"] = mnuHelicopters;
$VehicleMenu["mnuMain", 6, "displayed"] = "Helicopters -->";

// Light Flyers menu
$VehicleMenuNumEntries["mnuLightFlyers"] = 3;
$VehicleMenu["mnuLightFlyers", 0, "submenu"] = false;
$VehicleMenu["mnuLightFlyers", 0, "datablock"] = ScoutFlyer;
$VehicleMenu["mnuLightFlyers", 0, "displayed"] = "F-39 Fighter";
$VehicleMenu["mnuLightFlyers", 1, "submenu"] = false;
$VehicleMenu["mnuLightFlyers", 1, "datablock"] = StrikeFlyer;
$VehicleMenu["mnuLightFlyers", 1, "displayed"] = "F-41 Strike Fighter";
$VehicleMenu["mnuLightFlyers", 2, "submenu"] = false;
$VehicleMenu["mnuLightFlyers", 2, "datablock"] = Harrier;
$VehicleMenu["mnuLightFlyers", 2, "displayed"] = "Plasma Harrier";

// Assault Flyers menu
$VehicleMenuNumEntries["mnuAssaultFlyers"] = 3;
$VehicleMenu["mnuAssaultFlyers", 0, "submenu"] = false;
$VehicleMenu["mnuAssaultFlyers", 0, "datablock"] = BomberFlyer;
$VehicleMenu["mnuAssaultFlyers", 0, "displayed"] = "Thundersword Bomber";
$VehicleMenu["mnuAssaultFlyers", 1, "submenu"] = false;
$VehicleMenu["mnuAssaultFlyers", 1, "datablock"] = HarbingerGunship;
$VehicleMenu["mnuAssaultFlyers", 1, "displayed"] = "Harbinger Gunship";
$VehicleMenu["mnuAssaultFlyers", 2, "submenu"] = false;
$VehicleMenu["mnuAssaultFlyers", 2, "datablock"] = AC130;
$VehicleMenu["mnuAssaultFlyers", 2, "displayed"] = "AC-130 Gunship";

// Light Ground menu
$VehicleMenuNumEntries["mnuLightGround"] = 1;
$VehicleMenu["mnuLightGround", 0, "submenu"] = false;
$VehicleMenu["mnuLightGround", 0, "datablock"] = ScoutVehicle;
$VehicleMenu["mnuLightGround", 0, "displayed"] = "Wildcat Hoverbike";

// Heavy Ground menu
$VehicleMenuNumEntries["mnuHeavyGround"] = 3;
$VehicleMenu["mnuHeavyGround", 0, "submenu"] = false;
$VehicleMenu["mnuHeavyGround", 0, "datablock"] = AssaultVehicle;
$VehicleMenu["mnuHeavyGround", 0, "displayed"] = "Beowulf Assault Tank";
$VehicleMenu["mnuHeavyGround", 1, "submenu"] = false;
$VehicleMenu["mnuHeavyGround", 1, "datablock"] = SandstormTank;
$VehicleMenu["mnuHeavyGround", 1, "displayed"] = "Sandstorm AA Tank";
$VehicleMenu["mnuHeavyGround", 2, "submenu"] = false;
$VehicleMenu["mnuHeavyGround", 2, "datablock"] = CentaurVehicle;
$VehicleMenu["mnuHeavyGround", 2, "displayed"] = "Centaur Advanced Artillery";

// Transports menu
$VehicleMenuNumEntries["mnuTransports"] = 1;
$VehicleMenu["mnuTransports", 0, "submenu"] = false;
$VehicleMenu["mnuTransports", 0, "datablock"] = HAPCFlyer;
$VehicleMenu["mnuTransports", 0, "displayed"] = "Havoc Aerial APC";

//Deployables menu
$VehicleMenuNumEntries["mnuDeployables"] = 1;
$VehicleMenu["mnuDeployables", 0, "submenu"] = false;
$VehicleMenu["mnuDeployables", 0, "datablock"] = MobileBaseVehicle;
$VehicleMenu["mnuDeployables", 0, "displayed"] = "Jericho Mobile Base";

// Helicopters
$VehicleMenuNumEntries["mnuHelicopters"] = 4;
$VehicleMenu["mnuHelicopters", 0, "submenu"] = false;
$VehicleMenu["mnuHelicopters", 0, "datablock"] = CombatHelicopter;
$VehicleMenu["mnuHelicopters", 0, "displayed"] = "Combat Helicopter";
$VehicleMenu["mnuHelicopters", 1, "submenu"] = false;
$VehicleMenu["mnuHelicopters", 1, "datablock"] = GunshipHelicopter;
$VehicleMenu["mnuHelicopters", 1, "displayed"] = "Gunship Helicopter";
$VehicleMenu["mnuHelicopters", 2, "submenu"] = false;
$VehicleMenu["mnuHelicopters", 2, "datablock"] = ApacheHelicopter;
$VehicleMenu["mnuHelicopters", 2, "displayed"] = "Apache Helicopter";
$VehicleMenu["mnuHelicopters", 3, "submenu"] = false;
$VehicleMenu["mnuHelicopters", 3, "datablock"] = Hind;
$VehicleMenu["mnuHelicopters", 3, "displayed"] = "Hind Gunship";


//------------------------------------------------------------------------------
datablock AudioProfile(VehicleAppearSound)
{
   filename    = "fx/vehicles/inventory_pad_appear.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ActivateVehiclePadSound)
{
   filename = 	"fx/powered/vehicle_pad_on.wav";
   description = AudioClose3d;
   preload = true;
};

datablock StationFXVehicleData( VehicleInvFX )
{
   lifetime = 6.0;

   glowTopHeight = 1.5;
   glowBottomHeight = 0.1;
   glowTopRadius = 12.5;
   glowBottomRadius = 12.0;
   numGlowSegments = 26;
   glowFadeTime = 3.25;

   armLightDelay = 2.3;
   armLightLifetime = 3.0;
   armLightFadeTime = 1.5;
   numArcSegments = 10.0;

   sphereColor = "0.1 0.1 0.5";
   spherePhiSegments = 13;
   sphereThetaSegments = 8;
   sphereRadius = 12.0;
   sphereScale = "1.05 1.05 0.85";

   glowNodeName = "GLOWFX";

   leftNodeName[0]   = "LFX1";
   leftNodeName[1]   = "LFX2";
   leftNodeName[2]   = "LFX3";
   leftNodeName[3]   = "LFX4";

   rightNodeName[0]  = "RFX1";
   rightNodeName[1]  = "RFX2";
   rightNodeName[2]  = "RFX3";
   rightNodeName[3]  = "RFX4";


   texture[0] = "special/stationGlow";
   texture[1] = "special/stationLight2";
};

//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
function serverCmdBuyVehicle(%client, %blockName) {
   if(!%client.canUseVeh(%blockName)) {
      //Hi thyth, lookie lookie, I patched it! Sincerely, Phantom139
      bottomPrint(%client, "Trying to magicly spawn locked vehicles, Eh?", 2, 2);
      return;
   }
         // NEW CODE BEGINS HERE
         // set a persistent variable to save what they selected
         %client.vehicleHudSelection = %blockName;

         // If they selected a submenu,
         // redir them to the submenu that is appropriate
         if( getSubStr(%blockName,0,3) $= "mnu" ) {
             //echo("got menu");
             // they want a submenu - force the client to redisplay the hud
             commandToClient(%client, 'StationVehicleShowHud');
             // done
             return;
         }

         // otherwise they are buying a vehicle
// NEW CODE ENDS HERE
   %team = %client.getSensorGroup();

   if(vehicleCheck(%blockName, %team))
   {
      %station = %client.player.station.pad;
      if( (%station.ready) && (%station.station.vehicle[%blockName]) )
      {
         %trans = %station.getTransform();
         %pos = getWords(%trans, 0, 2);
         %matrix = VectorOrthoBasis(getWords(%trans, 3, 6));
         %yrot = getWords(%matrix, 3, 5);
         %p = vectorAdd(%pos,vectorScale(%yrot, -3));

         //%adjust = vectorMultiply(realVec(%station,"0 0 4"),"1 1 3");
	 //%adjustUp = getWord(%adjust,2);
	 //%adjust = getWords(%adjust,0,1) SPC ((%adjustUp * 0.5) + (mAbs(%adjustUp) * -0.5));
         %p =  VectorAdd(%p,RealVec(%station,"0 0 7"));

//         error(%blockName);
//         error(%blockName.spawnOffset);


         ///[Most]
         ///Updated Build code for rotatable vehicle pad.
         %p = vectorAdd(%p, RealVec(%station,VectorAdd(%blockName.spawnOffset,"0 0 1")));
         %forward = VectorCross(VectorCross("0 0 1",realvec(%station,"1 0 0")),"0 0 1");
          
         %rot = FullRot("0 0 1",%forward);
         %rrot = RotAdd(%rot,"0 0 1 3.14");
         //%rrot= %rot;
         %rot = getWords(%rrot, 0,2);
         %angle = getWord(%rrot, 3);
         //[Most]
         %mask = $TypeMasks::VehicleObjectType | $TypeMasks::PlayerObjectType |
                 $TypeMasks::StationObjectType | $TypeMasks::TurretObjectType;
	      InitContainerRadiusSearch(%p, %blockName.checkRadius, %mask);

	      %clear = 1;
         for (%x = 0; (%obj = containerSearchNext()) != 0; %x++)
         {
            if((%obj.getType() & $TypeMasks::VehicleObjectType) && (%obj.getDataBlock().checkIfPlayersMounted(%obj)))
            {
               %clear = 0;
               break;
            }
            else
               if (%obj !=%station.station)
               %removeObjects[%x] = %obj;
         }
         if(%clear)
         {
            %fadeTime = 0;
            for(%i = 0; %i < %x; %i++)
            {
               if(%removeObjects[%i].getType() & $TypeMasks::PlayerObjectType)
               {
                  %pData = %removeObjects[%i].getDataBlock();
                  if(!%removeObjects[%i].isBoss) {
                     %pData.damageObject(%removeObjects[%i], 0, "0 0 0", 1000, $DamageType::VehicleSpawn);
                  }
                  else {
                     //nice try, guess what. you die!
                     MessageAll('msgPwnt', %client.namebase@" tried to spawn a vehicle on a boss..... tsk tsk tsk... guess he'll learn the hard way.");
                     %client.player.getDatablock().damageObject(%client.player, 0, "0 0 0", 1000, $DamageType::VehicleSpawn);
                  }
               }
               else
               {
                  %removeObjects[%i].mountable = 0;
                  %removeObjects[%i].startFade( 1000, 0, true );
                  %removeObjects[%i].schedule(1001, "delete");
                  %fadeTime = 1500;
               }
            }
            schedule(%fadeTime, 0, "createVehicle", %client, %station, %blockName, %team , %p, %rot, %angle);
         }
         else
            MessageClient(%client, "", 'Can\'t create vehicle. A player is on the creation pad.');
      }
   }
}

function createVehicle(%client, %station, %blockName, %team , %pos, %rot, %angle)
{
   %obj = %blockName.create(%team);
   if(%obj)
   {
      %station.ready = false;
      %obj.team = %team;
      %obj.useCreateHeight(true);
      %obj.schedule(5500, "useCreateHeight", false);
      %obj.getDataBlock().isMountable(%obj, false);
      %obj.getDataBlock().schedule(6500, "isMountable", %obj, true);
      vehicleListAdd(%blockName, %obj);
      MissionCleanup.add(%obj);

      %turret = %obj.getMountNodeObject(10);
      if(%turret > 0)
      {
         %turret.setCloaked(true);
         %turret.schedule(4800, "setCloaked", false);
      }

      %obj.setCloaked(true);
      %obj.setTransform(%pos @ " " @ %rot @ " " @ %angle);

      %obj.schedule(3700, "playAudio", 0, VehicleAppearSound);
      %obj.schedule(4800, "setCloaked", false);

      if(%client.player.lastVehicle)
      {
         %client.player.lastVehicle.lastPilot = "";
         vehicleAbandonTimeOut(%client.player.lastVehicle);
         %client.player.lastVehicle = "";
      }
      %client.player.lastVehicle = %obj;
      %obj.lastPilot = %client.player;
      %station.playAudio($ActivateSound, ActivateVehiclePadSound);
      %ppos = VectorAdd(%station.getTransform(),RealVec(%station,"0 0 2"));
      if (%station.getDatablock().getName() $= "DeployableVehiclePad")
         {    
         %station.playThread($ActivateThread,"activate2");
         %up = realvec(%station,"0 0 1");
         %forward = realvec(%station,"1 0 0");         
         %p1 = CreateEmitter(%ppos,DVPADE);
         %p2 = CreateEmitter(%ppos,DVPADE);
         %p1.setRotation(FullRot(%up,%forward));
         %p2.setRotation(FullRot(VectorScale(%up,-1),%forward));
         %p1.schedule(5000,"delete");
         %p2.schedule(5000,"delete");         
         }
      else if (%station.getDatablock().getName() $= "DeployableVehiclePad2")
         {
         %station.playThread($ActivateThread,"activate");
         %up = realvec(%station,"0 0 1");
         %forward = realvec(%station,"1 0 0");         
         %p1 = CreateEmitter(%ppos,DVPADE);
         %p2 = CreateEmitter(%ppos,DVPADE);
         %p1.setRotation(FullRot(%up,%forward));
         %p2.setRotation(FullRot(VectorScale(%up,-1),%forward));
         %p1.schedule(5000,"delete");
         %p2.schedule(5000,"delete");
         }
      else
         {
         %station.playThread($ActivateThread,"activate2");
      // play the FX
      %fx = new StationFXVehicle()
      {
         dataBlock = VehicleInvFX;
         stationObject = %station;
      };
         }
//[[CHANGE]]!! If player is telebuying.. put him incontrol...
      if ( (%client.isVehicleTeleportEnabled()) && (!%client.telebuy))
         %obj.getDataBlock().schedule(5000, "mountDriver", %obj, %client.player);
      else
         {
         if(%obj.getDataBlock().canControl)
           {
           //serverCmdResetControlObject(%client);
           %client.setControlObject(%obj);
           commandToClient(%client, 'ControlObjectResponse', true, getControlObjectType(%obj,%client.player));
           %obj.clientControl = %client;
           }
         }
//[[End CHANGE]]
   }
   if(%obj.getTarget() != -1)
      setTargetSensorGroup(%obj.getTarget(), %client.getSensorGroup());
   // We are now closing the vehicle hud when you buy a vehicle, making the following call
   // unnecessary (and it breaks stuff, too!)
   //VehicleHud.updateHud(%client, 'vehicleHud');
}

//------------------------------------------------------------------------------
function VehicleData::mountDriver(%data, %obj, %player)
{
   if(isObject(%obj) && %obj.getDamageState() !$= "Destroyed")
   {
      %player.startFade(1000, 0, true);
      schedule(1000, 0, "testVehicleForMount", %player, %obj);
      %player.schedule(1500,"startFade",1000, 0, false);
   }
}

function testVehicleForMount(%player, %obj)
{
   if(isObject(%obj) && %obj.getDamageState() !$= "Destroyed")
      %player.getDataBlock().onCollision(%player, %obj, 0);
}


//------------------------------------------------------------------------------
function VehicleData::checkIfPlayersMounted(%data, %obj)
{
   for(%i = 0; %i < %obj.getDatablock().numMountPoints; %i++)
      if (%obj.getMountNodeObject(%i))
         return true;
   return false;
}

//------------------------------------------------------------------------------
function VehicleData::isMountable(%data, %obj, %val)
{
   %obj.mountable = %val;
}

//------------------------------------------------------------------------------
function vehicleCheck(%blockName, %team)
{
   if(($VehicleMax[%blockName] - $VehicleTotalCount[%team, %blockName]) > 0)
      return true;
//   else
//   {
//      for(%i = 0; %i < $VehicleMax[%blockName]; %i++)
//      {
//         %obj = $VehicleInField[%blockName, %i];
//         if(%obj.abandon)
//         {
//            vehicleListRemove(%blockName, %obj);
//            %obj.delete();
//            return true;
//         }
//      }
//   }
   return false;
}

//------------------------------------------------------------------------------
// new vehicle menu code by Crashed
function VehicleHud::updateHud( %obj, %client, %tag )
{
   %station = %client.player.station;
   for ( %i = 0; %i < 7; %i++ )   //Thanx BadShot
      messageClient( %client, 'RemoveLineHud', "", %tag, %i ); //Thanx BadShot
   %team = %client.getSensorGroup();

   // if they don't have a menu default to main.
   %menu = %client.vehicleHudSelection;
   if($VehicleMenuNumEntries[%menu] < 1) %menu = "mnuMain";

   // Print the menu.
   for( %i = 0; %i < $VehicleMenuNumEntries[%menu]; %i++ ) {
        %submenu = $VehicleMenu[%menu, %i, "submenu"];
        %dbName = $VehicleMenu[%menu, %i, "datablock"];
        %displayed = $VehicleMenu[%menu, %i, "displayed"];
        // Set the availability count appropriately
        if(%submenu == true)
                    %numAvail = 1;
        else if (%station.vehicle[%dbName])
             %numAvail = $VehicleMax[%dbName] - $VehicleTotalCount[%team, %dbName];
        else
            %numAvail = 0;
        //echo("sending to client i " @ %i @ " submenu " @ %submenu @ " dbname " @ %dbName @ " displayed " @ %displayed @ " numAvail " @ %numAvail);
        // Send update to client
         if(%client.canUseVeh(%dbName)) {
            messageClient( %client, 'SetLineHud', "", %tag, %i, %displayed, "", %dbName, %numAvail );
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %i, "LOCKED - Schematics Not Found", "", mnuMain, 1);
         }
   }
   // Add a "return to main menu" option if we aren't there already
   if(%menu !$= "mnuMain") {
     messageClient( %client, 'SetLineHud', "", %tag, %i, "<-- Main Menu", "", mnuMain, 1);
     %station.lastCount = %i + 1;
   } else %station.lastCount = %i;

   // Reset the selection var
   %client.vehicleHudSelection = "";
   // we're done
   return;

}
// end new code


//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
function VehicleHud::clearHud( %obj, %client, %tag, %count ) {
	for ( %i = 0; %i < %count; %i++ )
		messageClient( %client, 'RemoveLineHud', "", %tag, %i );
}

//------------------------------------------------------------------------------
function checkVehSet(%obj, %initpos) {
	if ((%obj !$= "") && (%obj >= %initpos) && (%obj <= (%initpos + 5)))
		return true;
	else
		return false;
}

//------------------------------------------------------------------------------
function serverCmdEnableVehicleTeleport( %client, %enabled )
{
   %client.setVehicleTeleportEnabled( %enabled );
}


//CAN USE
function GameConnection::canUseVeh(%client, %dbName) {
   if(%dbName $= "HarbingerGunship") {
      if(%client.hasMedal(8)) {
         return 1;
      }
      else {
         return 0;
      }
   }
   else if(%dbName $= "CentaurVehicle") {
      if(%client.hasMedal(15)) {
         return 1;
      }
      else {
         return 0;
      }
   }
   else if(%dbName $= "StrikeFlyer") {
      if(%client.CheckNWChallengeCompletion("NapalmHarrier3")) {
         return 1;
      }
      else {
         return 0;
      }
   }
   else if(%dbName $= "Harrier") {
      if(%client.CheckNWChallengeCompletion("Harrier3")) {
         return 1;
      }
      else {
         return 0;
      }
   }
   else if(%dbName $= "CombatHelicopter") {
      if(%client.CheckNWChallengeCompletion("Helicopter3")) {
         return 1;
      }
      else {
         return 0;
      }
   }
   else if(%dbName $= "GunshipHelicopter") {
      if(%client.CheckNWChallengeCompletion("GunHeli3")) {
         return 1;
      }
      else {
         return 0;
      }
   }
   else if(%dbName $= "ApacheHelicopter") {
      if(%client.CheckNWChallengeCompletion("Apache3")) {
         return 1;
      }
      else {
         return 0;
      }
   }
   else if(%dbName $= "Hind") {
      if(%client.CheckNWChallengeCompletion("Hind3")) {
         return 1;
      }
      else {
         return 0;
      }
   }
   else if(%dbName $= "AC130") {
      if(%client.CheckNWChallengeCompletion("Gunship3")) {
         return 1;
      }
      else {
         return 0;
      }
   }
   else {
      return 1;
   }
}
