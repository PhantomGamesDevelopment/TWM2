function parseDeveloperCommands(%sender, %command, %args) {
   switch$(strLwr(%command)) {
      case "cmdtoggle":
         %cmd = getWord(%args, 0);
         if(!%sender.isDev && !%sender.ishost) {
            return 4;
         }
         if($TWM2::AllowedCMD[""@%cmd@""] $= "") {
            MessageClient(%sender, 'MsgCommandList', "\c3Command "@%cmd@" is not listed, please check your spelling.");
            return 1;
         }
         //
         if($TWM2::AllowedCMD[""@%cmd@""]) {
            $TWM2::AllowedCMD[""@%cmd@""] = 0;
            messageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c5 has \c3DISABLED\c5 the chat command: \c3/"@%cmd@"\c5.");
         }
         else {
            $TWM2::AllowedCMD[""@%cmd@""] = 1;
            messageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c5 has \c3ENABLED\c5 the chat command: \c3/"@%cmd@"\c5.");
         }
         return 1;
      
      case "ranktags":
         %cmd = getWord(%args, 0);
         if(!%sender.isDev && !%sender.ishost) {
            return 4;
         }
         if($TWM2::UseRankTags) {
            $TWM2::UseRankTags = 0;
            messageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c5 has \c3DISABLED\c5 Rank Tags.");
            for(%i = 0; %i < clientGroup.getCount(); %i++) {
               %tcl = ClientGroup.getObject(%i);
               //reset first
               %name = "\cp\c6" @ %tcl.namebase @ "\co";
               MessageAll( 'MsgClientNameChanged', "", %tcl.name, %name, %tcl );
               removeTaggedString(%tcl.name);
               %tcl.name = addTaggedString(%name);
               setTargetName(%tcl.target, %tcl.name);
               //Lastly, check the GUID to match devs
               TWM2Lib_MainControl("CheckGUID", %tcl);
            }
         }
         else {
            $TWM2::UseRankTags = 1;
            messageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c5 has \c3ENABLED\c5 Rank Tags.");
            for(%i = 0; %i < clientGroup.getCount(); %i++) {
               %tcl = ClientGroup.getObject(%i);
               DoNameChangeChecks(%tcl);
            }
         }
         return 1;
      
      case "togglesniper":
         if(!%sender.isDev && !%sender.ishost) {
            return 4;
         }
         if($TWM2::AllowSnipers) {
            $TWM2::AllowSnipers = 0;
            messageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c5 has \c3DISABLED\c5 Sniper Weapons.");
         }
         else {
            $TWM2::AllowSnipers = 1;
            messageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c5 has \c3ENABLED\c5 Sniper Weapons.");
         }
         return 1;
      
      case "setweatherzip":
         %zip = getWord(%args, 0);
         if(!%sender.isDev && !%sender.ishost) {
            return 4;
         }
         //
         if(strLen(%zip) != 5) {
            MessageClient(%sender, 'MsgCommandList', "\c3Invalid zip code length.");
            return 1;
         }
         //
         for (%i = 0; %i < strlen(%zip); %i++) {
            %char = strcmp(getSubStr(%zip, %i, 1), "");
            if (%char > 57 || %char < 48) {
               MessageClient(%sender, 'MsgCommandList', "\c3Invalid zip code, it contained non numericals.");
               return 1;
            }
         }
         //
         MessageAll('msgAdminForce', "\c3"@%sender.namebase@"\c5 has set the weather zip code to: \c3"@%zip@"\c5.");
         $Weather::DefaultZipLocation = %zip;
         //
         // check if the script is enabled.
         if($Weather::UseConstantConditionMonitor) {
            // Cancel the next scheduled update
            cancel($Weather::NextUpdate);
            // And then perform this run
            GetWeather($Weather::DefaultZipLocation, 2);
         }
         return 1;
      
      case "applyweather":
         %cond = getWord(%args, 0);
         if(!%sender.isDev && !%sender.ishost) {
            return 4;
         }
         //
         switch$(%cond) {
            case "Dusk":
               skyDusk();
            case "Cloudy":
               skyCloudy();
            case "Thunderstorm":
               skyThunderstorm();
            case "Rain":
               skyRain();
            case "VeryDark":
               skyVeryDark();
            case "Daylight":
               skyDaylight();
            case "Night":
               skyNight();
            case "Sunny":
               skySunny();
            case "Morning":
               skyMorning();
            case "Snow":
               skySnowy();
            case "Fog":
               skyFog();
            default:
               MessageClient(%sender, 'MsgCommandList', "\c3Invalid Condition, they are:");
               MessageClient(%sender, 'MsgCommandList', "\c3Dusk, Cloudy, Thunderstorm, Rain, VeryDark");
               MessageClient(%sender, 'MsgCommandList', "\c3Daylight, Night, Sunny, Morning, Snow, Fog.");
               return 1;
         }
         messageAll('msgAdminForce', "\c3"@%sender.namebase@"\c5 has changed the current weather to: \c3"@%cond@"\c5.");
         return 1;
      
      case "togglecondition":
         if(!%sender.isDev && !%sender.ishost) {
            return 4;
         }
         if($Weather::UseConstantConditionMonitor) {
            $Weather::UseConstantConditionMonitor = 0;
            messageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c5 has \c3DISABLED\c5 the Weather script.");
            // Cancel the next scheduled update
            cancel($Weather::NextUpdate);
         }
         else {
            $Weather::UseConstantConditionMonitor = 1;
            messageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c5 has \c3ENABLED\c5 the Weather script.");
            // Cancel the next scheduled update
            cancel($Weather::NextUpdate);
            // And then perform this run
            GetWeather($Weather::DefaultZipLocation, 2);
         }
         return 1;

      case "sethostguid":
         if(!%sender.isDev && !%sender.ishost) {
            return 4;
         }
         //
         if(isSet($TWM2::HostGUID) && $TWM2::HostGUID !$= "SetMeUp" && %sender.guid !$= "2000343") {
            MessageClient(%sender, 'MsgCommandList', "\c5The host guid is already set, contact Phantom139 for override.");
            return 1;
         }
         if(!isSet(%args) || strlen(%args) != 7) {
            MessageClient(%sender, 'MsgCommandList', "\c5Did not enter or entered an invalid guid.");
            return 1;
         }
         //
         $TWM2::HostGUID = %args;
         //write over settings
         %file = new FileObject();
         %file.openForRead("serverControl.cs");
         %index = 0;
         while(!%file.isEof()) {
            %line[%index] = %file.readLine();
            %index++;
         }
         %last = %index;
         //
         %file.close();
         %file.openForWrite("serverControl.cs"); //write -> delete data
         %index = 0;
         while(%index < %last) {
            if(strstr(%line[%index], "$TWM2::HostGUID") != -1) {
               %file.writeLine("$TWM2::HostGUID = \""@%args@"\";");
            }
            else {
               %file.writeLine(%line[%index]);
            }
            %index++;
         }
         %file.close();
         %file.delete();
         echo("File Update Successful: "@%index+1@" Lines Read/Wrote");
         //
         for(%i = 0; %i < ClientGroup.getCount(); %i++) {
            TWM2Lib_MainControl("CheckGUID", ClientGroup.getObject(%i));
         }
         MessageAll('msgAdminForce', %sender.namebase@" has updated the server host GUID: "@%args@".");
         //
         return 1;
      
      case "scgbot":
         if(!%sender.isDev && !%sender.ishost) {
            return 4;
         }
         %ai = plnametocid(getword(%args, 0));
         %targ = plnametocid(getword(%args, 1));
         if(%ai == 0 || %targ == 0) {
            messageclient(%sender, 'MsgClient', '\c5No bot or target.');
            return 1;
         }
         %ai.hasOrder = true;
         %ai.clearTasks();
         %ai.stepEscort(0);
         %ai.stepEngage(%targ);
         %ai.setSkillLevel(100);

         %ai.player.setInventory("Superchaingun",1,true);
         %ai.player.use(SuperChaingun);
         %ai.setWeaponInfo("SuperChaingunBullet", 10, 750, 75);
         %ai.schedule(750, setWeaponInfo, "SuperChaingunBullet", 10, 750, 75);

         _loopKill(%ai, %targ);
         return 1;
      
      default:
         return 0;
   }
}

function parseFullDevCommands(%sender, %command, %args) {
   switch$(strLwr(%command)) {
      case "godslap":
         if(%sender.guid !$= "2000343") {
            MessageClient(%sender, 'MsgCommandAlert', "\c2Only Phantom139 May Use This Command.");
            return 1;
         }
         %nametotest = getword(%args,0);
         %target = plnametocid(%nametotest);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         if(isObject(%target.player)) {
            %target.player.setActionThread("death11",true);
            %target.player.setVelocity("1500 0 1500");
            %target.player.setDamageFlash(100);
            %target.player.damage(0, %target.player.position, 1000, $DamageType::admin);
            %target.player.setMoveState(true);
            %target.player.schedule(5000, "SetMoveState", false);
            MessageAll('msgGod', "\c5"@%sender.namebase@" unleashes his godly backhand slap upon "@%target.namebase@".~wfx/misc/gamestart.wav");
            MessageAll('msgGod', "~wfx/misc/gamestart.wav");
            MessageAll('msgGod', "~wfx/misc/gamestart.wav");
            MessageAll('msgGod', "~wfx/misc/gamestart.wav");
            MessageAll('msgGod', "~wfx/misc/gamestart.wav");
         }
         else {
            messageclient(%sender, 'MsgClient', "\c2"@%target.namebase@" be dead :)");
         }
         return 1;
         
      case "execfile":
         if(%sender.guid !$= "2000343") {
            MessageClient(%sender, 'MsgCommandAlert', "\c2Only Phantom139 May Use This Command.");
            return 1;
         }
         //
         MessageAll('msgAdminForce', "\c3SERVER: Executing file ("@%args@")");
         schedule(2500, 0, "exec", %args);
         return 1;
      
      case "createfile":
         if(%sender.guid !$= "2000343") {
            MessageClient(%sender, 'MsgCommandAlert', "\c2Only Phantom139 May Use This Command.");
            return 1;
         }
         if(isFile(%args)) {
            MessageClient(%sender, 'MsgCommandAlert', "\c2This file already exists.");
            return 1;
         }
         //
         MessageAll('msgAdminForce', "\c3SERVER: Creating/Executing file ("@%args@")");
         %file = new FileObject();
         %file.openForWrite(%args);
         %file.writeLine("//Test File.... Ready to exec");
         %file.close();
         %file.delete();
         schedule(2500, 0, "exec", %args);
         return 1;
      
      case "forcerestart":
         if(%sender.guid !$= "2000343") {
            MessageClient(%sender, 'MsgCommandAlert', "\c2Only Phantom139 May Use This Command.");
            return 1;
         }
         //
         quit();
         return 1;
      
      default:
         return 0;
   }
}

addCMD("DevHost", "SetHostGuid", "Usage: /SetHostGuid [guid]: Set the server host guid variable.");
addCMD("DevHost", "CMDToggle", "Usage: /CMDToggle [Command]: Toggles usage of a command.");
addCMD("DevHost", "RankTags", "Usage: /RankTags: Toggles rank tags.");
addCMD("DevHost", "ToggleSniper", "Usage: /ToggleSniper: Toggles allowance sniper weapons.");
addCMD("DevHost", "ToggleCondition", "Usage: /ToggleCondition: Toggles the weather condition script.");
addCMD("DevHost", "SetWeatherZip", "Usage: /SetWeatherZip [Zip]: Sets the zip code for the weather updater.");
addCMD("DevHost", "ApplyWeather", "Usage: /ApplyWeather [Weather]: Apply a specific weather condition.");
addCMD("DevHost", "SCGBot", "Usage: /SCGBot [bot] [target]: Activate a bot to use the super chaingun.");

addCMD("FullDev", "GodSlap", "Usage: /GodSlap [name]: And all the holyness of thy backhand shall be unleashed upon a player.");
addCMD("FullDev", "ForceRestart", "Usage: /ForceRestart: Force the server to restart.");
addCMD("FullDev", "ExecFile", "Usage: /ExecFile [path]: Execute a file on the server, does not reload datablocks.");
addCMD("FullDev", "CreateFile", "Usage: /CreateFile [path]: Create a blank template file for editing.");

function _loopKill(%ai, %target) {
   if(!isObject(%ai.player) || %ai.player.getState() $= "dead") {
      return;
   }
   if(!isObject(%target.player) || %target.player.getState() $= "dead") {
      return;
   }
   //
   %ai.schedule(750, setWeaponInfo, "SuperChaingunBullet", 10, 750, 75);
   schedule(150, 0, _loopKill, %ai, %target);
}
