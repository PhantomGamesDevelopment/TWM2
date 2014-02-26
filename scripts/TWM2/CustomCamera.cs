datablock CameraData(TWM2ControlCamera) {
   mode = "AirstrikeCall";
   firstPersonOnly = true;
};

function CameraMessageLoop(%cl, %obj, %mode) {
   if(!isObject(%obj)) {
      return;
   }
   if(!isObject(%cl.player) || %cl.player.getState() $= "dead") {
      %obj.delete();
      return;
   }
   if(%cl.getControlObject() != %obj) {
      return;
   }
   switch$ (%mode) {
      case "AirstrikeCall":
         BottomPrint(%cl, "AIRSTRIKE, MOVE TO POSITION, LOOK IN THE ONCOMING DIRECTION \n PRESS JET TO LAUNCH", 1, 2);
      case "HarrierCall":
         BottomPrint(%cl, "HARRIER AIRSTRIKE, MOVE TO POSITION, LOOK IN THE ONCOMING DIRECTION \n PRESS JET TO LAUNCH", 1, 2);
      case "NapalmHarrierCall":
         BottomPrint(%cl, "NAPALM AIRSTRIKE, MOVE TO POSITION, LOOK IN THE ONCOMING DIRECTION \n PRESS JET TO LAUNCH", 1, 2);
      case "StlhAirstrikeCall":
         BottomPrint(%cl, "STEALTH BOMBER, MOVE TO POSITION, LOOK IN THE ONCOMING DIRECTION \n PRESS JET TO LAUNCH", 1, 2);
      case "ArtilleryCall":
         BottomPrint(%cl, "ARTILLERY, MOVE TO POSITION, AND PRESS JET TO LAUNCH", 1, 2);
      case "OLSCall":
         BottomPrint(%cl, "ORBITAL LASER STRIKE, MOVE TO POSITION, AND PRESS JET TO LAUNCH", 1, 2);
      case "NukeCall":
         BottomPrint(%cl, "NUCLEAR STRIKE, MOVE TO POSITION, AND PRESS JET TO LAUNCH", 1, 2);
   }
   schedule(1000, 0, "CameraMessageLoop", %cl, %obj, %mode);
}

function TWM2ControlCamera::onTrigger(%data,%obj,%trigger,%state) {
   if (%state == 0)
      return;
      
   //first, give the game the opportunity to prevent the observer action
   if (!Game.ObserverOnTrigger(%data, %obj, %trigger, %state))
      return;

   //now observer functions if you press the "throw"
   if (%trigger >= 4)
      return;
      
   %client = %obj.getControllingClient();
   if (%client == 0)
      return;
      
   switch$ (%obj.mode) {
      case "AirstrikeCall":
         //press JET
         if (%trigger == 3) {
               //Can we Call in this airstrike?
               if(!%client.HasAirstrike && !%client.UnlimitedAS) {
                  if(isObject(%client.player)) {
                     %obj.schedule(1000, "delete");
                     %client.setControlObject(%client.player);
                     bottomPrint(%client, "You cannot call in an airstrike", 5, 2);
                     return;
                  }
               }
               else {
                  //
                  %trns = %obj.getTransform();
                  %position = getWords(%trns, 0, 2);
                  %xyVec = getWords(%obj.getForwardVector(), 0, 1);
                  //cast the camera out 700M and get a location
                  %vector = %xyvec SPC 0;
                  if(getWord(%vector, 1) < 0.1 && getWord(%vector, 1) > -0.1) {
                     %vector = getWord(%vector, 0) SPC (getWord(%vector, 1) * 7) SPC 0;
                  }
                  %direction = vectoradd(%position, vectorscale(%vector, 700));
                  //echo(%direction);
                  //
                  if(isObject(%client.player)) {
                     %obj.schedule(1000, "delete");
                     %client.setControlObject(%client.player);
                     if(!%client.UnlimitedAS) {
                        %client.HasAirstrike = 0;
                        GainExperience(%client, 35, "Airstrike called in ");
                     }
                     bottomPrint(%client, "Coordinates Confirmed, Calling In Airstrike", 5, 2);
                     messageTeam(%client.team, 'MsgAirstrike', "\c5TWM2: Airstrike Called In From "@%client.namebase@"");
                     Airstrike(%client, %position, %direction);
                  }
                  else {
                     bottomPrint(%client, "Player Object Required", 5, 2);
                  }
               }
         }
      case "HarrierCall":
         //press JET
         if (%trigger == 3) {
               //Can we Call in this airstrike?
               if(!%client.HasHarrier) {
                  if(isObject(%client.player)) {
                     %obj.schedule(1000, "delete");
                     %client.setControlObject(%client.player);
                     bottomPrint(%client, "You cannot call in a harrier airstrike", 5, 2);
                     return;
                  }
               }
               else {
                  //
                  %trns = %obj.getTransform();
                  %position = getWords(%trns, 0, 2);
                  %xyVec = getWords(%obj.getForwardVector(), 0, 1);
                  //cast the camera out 700M and get a location
                  %vector = %xyvec SPC 0;
                  if(getWord(%vector, 1) < 0.1 && getWord(%vector, 1) > -0.1) {
                     %vector = getWord(%vector, 0) SPC (getWord(%vector, 1) * 7) SPC 0;
                  }
                  %direction = vectoradd(%position, vectorscale(%vector, 700));
                  if(isObject(%client.player)) {
                     %obj.schedule(1000, "delete");
                     %client.setControlObject(%client.player);
                     %client.HasHarrier = 0;
                     GainExperience(%client, 150, "Harrier Airstrike called in ");
                     messageTeam(%client.team, 'MsgAirstrike', "\c5TWM2: "@%client.namebase@"'s Harrier Airstrike is Approaching.");
                     bottomPrint(%client, "Coordinates Confirmed, Calling In Harriers", 5, 2);
                     HarrierAirstrike(%client, %position, %direction);
                  }
                  else {
                     bottomPrint(%client, "Player Object Required", 5, 2);
                  }
               }
         }
      case "NapalmHarrierCall":
         //press JET
         if (%trigger == 3) {
               //Can we Call in this airstrike?
               if(!%client.HasNapalmHarrier) {
                  if(isObject(%client.player)) {
                     %obj.schedule(1000, "delete");
                     %client.setControlObject(%client.player);
                     bottomPrint(%client, "You cannot call in a napalm airstrike", 5, 2);
                     return;
                  }
               }
               else {
                  //
                  %trns = %obj.getTransform();
                  %position = getWords(%trns, 0, 2);
                  %xyVec = getWords(%obj.getForwardVector(), 0, 1);
                  //cast the camera out 700M and get a location
                  %vector = %xyvec SPC 0;
                  if(getWord(%vector, 1) < 0.1 && getWord(%vector, 1) > -0.1) {
                     %vector = getWord(%vector, 0) SPC (getWord(%vector, 1) * 7) SPC 0;
                  }
                  %direction = vectoradd(%position, vectorscale(%vector, 700));
                  if(isObject(%client.player)) {
                     %obj.schedule(1000, "delete");
                     %client.setControlObject(%client.player);
                     %client.HasNapalmHarrier = 0;
                     GainExperience(%client, 350, "Napalm Airstrike called in ");
                     messageTeam(%client.team, 'MsgAirstrike', "\c5TWM2: "@%client.namebase@"'s Napalm Airstrike is coming in hot.");
                     bottomPrint(%client, "Coordinates Confirmed, Calling In Strike Fighters", 5, 2);
                     NapalmHarrierAirstrike(%client, %position, %direction);
                  }
                  else {
                     bottomPrint(%client, "Player Object Required", 5, 2);
                  }
               }
         }
      case "OLSCall":
         //press JET
         if (%trigger == 3) {
               //Can we Call in this airstrike?
               if(!%client.HasOLS) {
                  if(isObject(%client.player)) {
                     %obj.schedule(1000, "delete");
                     %client.setControlObject(%client.player);
                     bottomPrint(%client, "You cannot call in an orbital laser strike", 5, 2);
                     return;
                  }
               }
               else {
                  //
                  %position = %obj.getPosition();
                  if(isObject(%client.player)) {
                     %obj.schedule(1000, "delete");
                     %client.setControlObject(%client.player);
                     %client.HasOLS = 0;
                     GainExperience(%client, 350, "Orbital Laser Strike Called In ");
                     messageTeam(%client.team, 'MsgAirstrike', "\c5TWM2: "@%client.namebase@"'s Laser Strike is Incoming.");
                     bottomPrint(%client, "Coordinates Confirmed, Spinning Laser Cannon", 5, 2);
                     OrbitalLaserStrike(%client, %position);
                  }
                  else {
                     bottomPrint(%client, "Player Object Required", 5, 2);
                  }
               }
         }
      case "StlhAirstrikeCall":
         //press JET
         if (%trigger == 3) {
               //Can we Call in this airstrike?
               if(!%client.HasSlthAirstrike) {
                  if(isObject(%client.player)) {
                     %obj.schedule(1000, "delete");
                     %client.setControlObject(%client.player);
                     bottomPrint(%client, "You cannot call in a stealth bomber", 5, 2);
                     return;
                  }
               }
               else {
                  //
                  %trns = %obj.getTransform();
                  %position = getWords(%trns, 0, 2);
                  %xyVec = getWords(%obj.getForwardVector(), 0, 1);
                  //cast the camera out 700M and get a location
                  %vector = %xyvec SPC 0;
                  if(getWord(%vector, 1) < 0.1 && getWord(%vector, 1) > -0.1) {
                     %vector = getWord(%vector, 0) SPC (getWord(%vector, 1) * 7) SPC 0;
                  }
                  %direction = vectoradd(%position, vectorscale(%vector, 700));
                  if(isObject(%client.player)) {
                     %obj.schedule(1000, "delete");
                     %client.setControlObject(%client.player);
                     %client.HasSlthAirstrike = 0;
                     GainExperience(%client, 150, "Stealth Bomber Airstrike called in ");
                     bottomPrint(%client, "Coordinates Confirmed, Calling In Stealth Bomber", 5, 2);
                     StealthAirstrike(%client, %position, %direction);
                  }
                  else {
                     bottomPrint(%client, "Player Object Required", 5, 2);
                  }
               }
         }
      case "ArtilleryCall":
         //press JET
         if (%trigger == 3) {
               //Can we Call in this airstrike?
               if(!%client.HasArtillery) {
                  if(isObject(%client.player)) {
                     %obj.schedule(1000, "delete");
                     %client.setControlObject(%client.player);
                     bottomPrint(%client, "You cannot call in artillery", 5, 2);
                     return;
                  }
               }
               else {
                  //
                  %position = %obj.getPosition();
                  if(isObject(%client.player)) {
                     %obj.schedule(1000, "delete");
                     %client.setControlObject(%client.player);
                     //if(!%client.UnlimitedAS) {
                        %client.HasArtillery = 0;
                        GainExperience(%client, 250, "Artillery called in ");
                     //}
                     bottomPrint(%client, "Coordinates Confirmed, Calling In Artillery", 5, 2);
                     messageTeam(%client.team, 'MsgAirstrike', "\c5TWM2: Artillery Called In From "@%client.namebase@"");
                     Artillery(%client, %position);
                  }
                  else {
                     bottomPrint(%client, "Player Object Required", 5, 2);
                  }
               }
         }
      case "NukeCall":
         //press JET
         if (%trigger == 3) {
               //Can we Call in this airstrike?
               if(!%client.HasNuke) {
                  if(isObject(%client.player)) {
                     %obj.schedule(1000, "delete");
                     %client.setControlObject(%client.player);
                     bottomPrint(%client, "You cannot call in a nuclear missile", 5, 2);
                     return;
                  }
               }
               else {
                  //
                  %position = %obj.getPosition();
                  if(isObject(%client.player)) {
                     %obj.schedule(1000, "delete");
                     %client.setControlObject(%client.player);
                     //if(!%client.UnlimitedAS) {
                        %client.HasNuke = 0;
                        GainExperience(%client, 500, "Nuclear Missile called in ");
                     //}
                     bottomPrint(%client, "Coordinates Confirmed, Launching Missile", 5, 2);
                     messageAll('msgDanger', "\c5TWM2 ALERT: "@%client.namebase@" has activated a nuclear missile!!! ~wfx/misc/red_alert.wav");
                     Nuke(%client, %position);
                  }
                  else {
                     bottomPrint(%client, "Player Object Required", 5, 2);
                  }
               }
         }
   }
}
