$ScoreHudMaxVisible = 16; //maybe 16 for low end people?
function ConstructionGame::updateScoreHud(%game, %client, %tag){
   if (%client.SCMPage $= "")
      %client.SCMPage = 1;
   if (%client.SCMPage $= "SM")
      return;
   $TagToUseForScoreMenu = %tag;
   messageClient( %client, 'ClearHud', "", %tag, 0 );
   messageClient( %client, 'SetScoreHudHeader', "", "" );
   messageClient( %client, 'SetScoreHudHeader', "", "<a:gamelink\tGTP\t1>E.V.A.</a><rmargin:600><just:right><a:gamelink\tNAC\t1>Close</a>" );
   messageClient( %client, 'SetScoreHudSubheader', "", "Main Command Hud" );
   if(!%client.notFirstUse) {
      messageClient( %client, 'SetScoreHudSubheader', "", "Loading TWM Score-Hud" );
      messageClient( %client, 'SetLineHud', "", %tag, 0, "Please Wait... loading news...");
   }
   else {
      scoreCmdMainMenu(%game,%client,%tag,%client.SCMPage);
   }
}

function ConstructionGame::processGameLink(%game, %client, %arg1, %arg2, %arg3, %arg4, %arg5){
%tag = $TagToUseForScoreMenu;
messageClient( %client, 'ClearHud', "", %tag, 1 );
//Stuff
if(%arg1 $= "")
%arg1 = "Null";
if(%arg2 $= "")
%arg2 = "Null";
if(%arg3 $= "")
%arg3 = "Null";
if(%arg4 $= "")
%arg4 = "Null";
if(%arg5 $= "")
%arg5 = "Null";
//end
%scriptController = %client.TWM2Core;
echo("[F2] "@%client.namebase@": "@%arg1@", "@%arg2@", "@%arg3@", "@%arg4@", "@%arg5@".");
switch$ (%arg1)
{
        case "GTP":
             scoreCmdMainMenu(%game,%client,$TagToUseForScoreMenu,%arg2);
             %client.SCMPage = %arg2;
             return;
        //***********************************************************************
        //* TWM STORE
        case "Store":
             %dept = %arg2;
             if(!isSet(%dept)) {
                %index = %client.showCentralStore(%index, %tag);
             }
             else {
                switch$(%dept) {
                   case "ArmorEffect":
                      %index = %client.showArmorEffectsPage(%index, %tag);
                   case "ArmorFlag":
                      %index = %client.showArmorFlagPage(%index, %tag);
                   case "SlotMachine":
                      %index = %client.loadSlotMachine(%index, %tag);
                   default:
                      %index = %client.showCentralStore(%index, %tag);
                }
             }
             return;

        case "ArmorEffect":
             %effect = %arg2;
             %client.setActiveAE(%effect);
             %game.schedule(100, "processGameLink", %client, "Store", "ArmorEffect");
             return;

        case "BuyArmorEffect":
             %effectIndex = %arg2;
             %money = %client.TWM2Core.money;
             if(%money >= getField($Store::Item["ArmorEffect", %effectIndex], 1)) {
                %client.TWM2Core.money -= getField($Store::Item["ArmorEffect", %effectIndex], 1);
                %item = strReplace(getField($Store::Item["ArmorEffect", %effectIndex], 0), " ", "");
                %client.store.purchased[%item] = 1;
                SaveClientFile(%client);
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Armor Effect Purchased.");
                %index++;
             }
             else {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Insufficient Funds To Purchase.");
                %index++;
             }
             %game.schedule(1000, "processGameLink", %client, "Store", "ArmorEffect");
             return;
             
        case "ArmorFlag":
             %flag = %arg2;
             %client.setActiveAF(%flag);
             %game.schedule(100, "processGameLink", %client, "Store", "ArmorFlag");
             return;
        
        case "BuyArmorFlag":
             %flagIndex = %arg2;
             %money = %client.TWM2Core.money;
             if(%money >= getField($Store::Item["ArmorFlag", %flagIndex], 2)) {
                %client.TWM2Core.money -= getField($Store::Item["ArmorFlag", %flagIndex], 2);
                %item = strReplace(getField($Store::Item["ArmorFlag", %flagIndex], 0), " ", "");
                %client.store.purchased[%item] = 1;
                SaveClientFile(%client);
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Armor Flag Purchased.");
                %index++;
             }
             else {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Insufficient Funds To Purchase.");
                %index++;
             }
             %game.schedule(1000, "processGameLink", %client, "Store", "ArmorFlag");
             return;
             
        case "SlotMachine":
             %money = %client.TWM2Core.money;
             if(%money < 50) {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "You cannot play the slot machine.");
                %index++;
                %game.schedule(1000, "processGameLink", %client, "Store", "");
                return;
             }
             %index = %client.generateSlotMachineRoll(%index, %tag);
             return;
             
             
        //*****************************************************************************
             
        case "OrderMisSub":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Missions" );
             if(getCurrentEXP(%client) < $Ranks::MinPoints[59] && %scriptController.officer < 1) {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "You must have the 'Commanding Officer' Rank To Order Missions.");
                %index++;
             }
             else {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Order A Mission, Select a Mission");
                %index++;
                %xI = 0;
                while(isSet($Mission::TWM2Mision[%xI])) {
                   %mis = getField($Mission::TWM2Mision[%xI], 0);
                   if(isSet(getField($Mission::TWM2Mision[%xI], 1))) {
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:16>[NEW] <font:arial:14><a:gamelink\tOrderMis\t"@%mis@"\t1>"@getField($Mission::VarSet[%mis, "TaskDetails"], 0)@"</a> - "@getField($Mission::VarSet[%mis, "TaskDetails"], 2)@" ["@$Mission::VarSet[%mis, "PlayerLimit"]@"P]");
                      %index++;
                   }
                   else {
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<font:arial:14><a:gamelink\tOrderMis\t"@%mis@"\t1>"@getField($Mission::VarSet[%mis, "TaskDetails"], 0)@"</a> - "@getField($Mission::VarSet[%mis, "TaskDetails"], 2)@" ["@$Mission::VarSet[%mis, "PlayerLimit"]@"P]");
                      %index++;
                   }
                   %xI++;
                }
                //messageClient( %client, 'SetLineHud', "", %tag, %index, '<font:arial:14><a:gamelink\tOrderMis\tRainDown\t1>Rain Down</a> - 3:00/Gunship Support [1P]');
                //%index++;
                //messageClient( %client, 'SetLineHud', "", %tag, %index, '<font:arial:14><a:gamelink\tOrderMis\tEnemyAc130Above\t1>Enemy AC-130 Above!</a> - 15:00/Survival-Escape Mission [3P]');
                //%index++;
                //messageClient( %client, 'SetLineHud', "", %tag, %index, '<font:arial:14><a:gamelink\tOrderMis\tSurrounded\t1>Surrounded!</a> - 5:00/Survival Mission [6P]');
                //%index++;
                //messageClient( %client, 'SetLineHud', "", %tag, %index, '<font:arial:16>[NEW]<font:arial:14><a:gamelink\tOrderMis\tSurrounded\t1>Surrounded 2.0!</a> - 10:00/Survival Mission [6P]');
                //%index++;
                //messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tOrderMis\tTheShallowedCity\t1>The Shallowed City</a> - 10:00/City Assault [4P/2R]');
                //%index++;
             }
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Return To Main Menu</a>');
             %index++;
             return;
             
        case "OrderMis":
             %mission = %arg2;
             %task = %arg3;
             switch(%task) {
                case 1:
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "Mission: "@getField($Mission::VarSet[""@%mission@"", "TaskDetails"], 0)@"");
                   %index++;
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "Details: "@getField($Mission::VarSet[""@%mission@"", "TaskDetails"], 1)@"");
                   %index++;
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "Difficulty: "@$Mission::VarSet[""@%mission@"", "Difficulty"]@"");
                   %index++;
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "Est. Time Completion: "@$Mission::VarSet[""@%mission@"", "TimeLimit"] / 60@" Minutes");
                   %index++;
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "Required Players: "@$Mission::VarSet[""@%mission@"", "PlayerReq"]@"");
                   %index++;
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "Max Players: "@$Mission::VarSet[""@%mission@"", "PlayerLimit"]@"");
                   %index++;
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "");
                   %index++;
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tOrderMis\t"@%mission@"\t2>Order Mission</a>");
                   %index++;
                   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tOrderMisSub\t1>Select A Different Mission</a>');
                   %index++;
                   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Return To Main Menu</a>');
                   %index++;
                case 2:
                   CreateTWM2Mission(%client, %mission);
                   closeScoreHudFSERV(%client);
             }
             return;
             
        case "JoinMis":
             AddClientToMission(%client);
             closeScoreHudFSERV(%client);
             return;
             
        case "Missions":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Missions" );
             if(getCurrentEXP(%client) < $Ranks::MinPoints[59] && %scriptController.officer < 1) {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "You must have the 'Commanding Officer' Rank To Order Missions.");
                %index++;
             }
             else {
                messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tOrderMisSub\t1>Order A Mission</a>');
                %index++;
             }
             messageClient( %client, 'SetLineHud', "", %tag, %index, '');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tJoinMis\t1>Join The Mission About To Begin</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Return To Main Menu</a>');
             %index++;
             return;

             
        case "MAINPAGE":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudHeader', "", "<a:gamelink\tGTP\t1>E.V.A.</a><rmargin:600><just:right><a:gamelink\tNAC\t1>Close</a>" );
             messageClient( %client, 'SetScoreHudSubheader', "", "TWM 2 : Server Intro / News" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tNAC\t1>Exit</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Date: "@formattimestring("yy-mm-dd")@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "NEWS:");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "-------");
             %index++;
             for(%i = 1; %i <= $TWM::Ticks; %i++) {
                messageClient( %client, 'SetLineHud', "", %tag, %index, ""@$TWM::Page[%i]@"");
                %index++;
             }
             return;
             
        case "TSSF":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "TWM 2 : The Story Returns" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "It's 2016 Now, The War Against The Zombies Has Mostly Ended, Except For");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Zombie Extremists who's only intent, Revenge, and to Revive Their Once");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Powerful leader Lord Yvex. Even with the zombie's army thinning, strange");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "things are happening in our world, The Harbinger Clan is rising back to power");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "and the zombies, they Have Gotten Smarter, and More Dangerous. This Extremist");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Group, The Fist Of Vengeance is wipping out our new cities, even though we");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "put up everything that is left against them, it seems, that that is not enough.");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Now the war has expanded, advanced... New Weapons on every Side of this war");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "have been unleashed, and now, the great war, has erupted once more, Can you");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "end the war? and destroy the evil Plots of the FoV and stop the harbingers?");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Welcome, To Total Warfare..... 2 ADVANCED WARFARE.");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;
             
        case "PGDConn1":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "PGD Connect" );
             if($TWM2::PGDConnectDisabled) {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "PGD Connect is disabled on this server.");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
                %index++;
                return;
             }
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Set email with: /setEmail");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Current Email: "@%client.emailSet);
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPGDConn2\t1>Connect Account</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;
             
        case "PGDConn2":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "PGD Connect" );
             if($TWM2::PGDConnectDisabled) {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "PGD Connect is disabled on this server.");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
                %index++;
                return;
             }
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Connecting... please wait for response");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             
             PGDConnect_FromInGame(%client);
             return;

        case "TWM":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "TWM Information" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "http://www.phantomdev.net");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;
             
        case "ContSave":
             %client.SCMPage = "SM";
             //PIECE COUNT
             %counter=deployables.getcount();
             for (%n=0;%n<%counter;%n++) {
                 %obj = deployables.getObject(%n);
                 %totalPC++;
             }
             //END
             messageClient( %client, 'SetScoreHudSubheader', "", "Content Saving System (V2.0)" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, 'Created By Phantom139');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, 'To Rename Slots: /NameSlot [Slot #] [New Name]');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:FF0000>[RED] - Not Possible Or Shouldn't Be Done");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:FFFF66>[YELLOW] - Warning");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>[GREEN] - Possible");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             for(%i = 1; %i < $TWM2::PlayerSaveSlots+1; %i++) {
                if($SaveFile::SlotName[%client.guid,%i] $= "") {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:003300>SLOT "@%i@" : "@RunSaveCheck(%client, %i)@"<color:003300>  -  "@RunLoadCheck(%client, %i, %totalPC)@"<color:003300>  -  "@RunDeleteCheck(%client, %i)@"<color:003300>  -  "@CheckSlotStatus(%client,%i)@"");
                %index++;
                }
                else {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:003300>"@$SaveFile::SlotName[%client.guid,%i]@" : "@RunSaveCheck(%client, %i)@"<color:003300>  -  "@RunLoadCheck(%client, %i, %totalPC)@"<color:003300>  -  "@RunDeleteCheck(%client, %i)@"<color:003300>  -  "@CheckSlotStatus(%client,%i)@"");
                %index++;
                }
             }
             if(%client.isadmin) {
                for(%i = $TWM2::PlayerSaveSlots+1; %i < $TWM2::PlayerSaveSlots+$TWM2::AdminSaveSlots+1; %i++) {
                   if($SaveFile::SlotName[%client.guid,%i] $= "") {
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:003300>SLOT "@%i@" : "@RunSaveCheck(%client, %i)@"<color:003300>  -  "@RunLoadCheck(%client, %i, %totalPC)@"<color:003300>  -  "@RunDeleteCheck(%client, %i)@"<color:003300>  -  "@CheckSlotStatus(%client,%i)@"");
                   %index++;
                   }
                   else {
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:003300>"@$SaveFile::SlotName[%client.guid,%i]@" : "@RunSaveCheck(%client, %i)@"<color:003300>  -  "@RunLoadCheck(%client, %i, %totalPC)@"<color:003300>  -  "@RunDeleteCheck(%client, %i)@"<color:003300>  -  "@CheckSlotStatus(%client,%i)@"");
                   %index++;
                   }
                }
             }
             if(%client.issuperadmin) {
                for(%i = $TWM2::PlayerSaveSlots+$TWM2::AdminSaveSlots+1; %i < $TWM2::PlayerSaveSlots+$TWM2::AdminSaveSlots+$TWM2::SuperAdminSaveSlots+1; %i++) {
                   if($SaveFile::SlotName[%client.guid,%i] $= "") {
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:003300>SLOT "@%i@" : "@RunSaveCheck(%client, %i)@"<color:003300>  -  "@RunLoadCheck(%client, %i, %totalPC)@"<color:003300>  -  "@RunDeleteCheck(%client, %i)@"<color:003300>  -  "@CheckSlotStatus(%client,%i)@"");
                   %index++;
                   }
                   else {
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:003300>"@$SaveFile::SlotName[%client.guid,%i]@" : "@RunSaveCheck(%client, %i)@"<color:003300>  -  "@RunLoadCheck(%client, %i, %totalPC)@"<color:003300>  -  "@RunDeleteCheck(%client, %i)@"<color:003300>  -  "@CheckSlotStatus(%client,%i)@"");
                   %index++;
                   }
                }
             }
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;

        case "Save":
             if(!%client.guid) {
             MessageClient(%client,'Deny',"\c1From "@$ChatBot::Name@": Error: You do not have a Registered GUID, please /Auth In.");
             closescorehudfserv(%client);
             return;
             }
             if(%client.cantSave) {
             %x = MFloor($SaveTime::TimeLeft[%client.guid, "Save"] / 60);
             %y = $SaveTime::TimeLeft[%client.guid, "Save"] % 60;
             if(%x > 0) {
             MessageClient(%client,'Deny',"\c1From "@$ChatBot::Name@": You have only recently saved a building, please wait "@%x@" Minutes and "@%y@" Seconds.");
             }
             else {
             MessageClient(%client,'Deny',"\c1From "@$ChatBot::Name@": You have only recently saved a building, please wait "@%y@" Seconds.");
             }
             closescorehudfserv(%client);
             return;
             }
             if(!$Phantom::CSSEnabled && !%client.issuperadmin) {
             MessageClient(%client,'Deny',"\c1From "@$ChatBot::Name@": Content Saving Is Disabled on This Server, Please Contact a server admin.");
             closescorehudfserv(%client);
             return;
             }
             %slot = %arg2;
             $SaveFile::Save[%client.guid,%slot] = PersonalsaveBuilding(%client,99999999,""@%client.guid@"/"@%slot@"",0);
             $SaveFile::PieceCT[%client.guid,%slot] = $SaveBuilding::Saved[$SaveFile::Save[%client.guid,%slot]];  //How many pieces?
             $SaveTime::TimeLeft[%client.guid, "Save"] = $TWM::CSSTimeSave*60; //5 mins
             %client.cantSave = 1;
             schedule(1,0,"ResetSave",%client);
             messageall('MsgAdminForce', "\c3"@ %client.namebase@"\c2 has saved his pieces.");
             MessageClient(%client,'Success',"\c1From "@$ChatBot::Name@": Building Saved To Content Save Slot "@%slot@".");
             export( "$SaveFile::*", "prefs/ContentSave.cs", false );
             closescorehudfserv(%client);
             return;

        case "Load":
             if(!%client.guid) {
             MessageClient(%client,'Deny',"\c1From "@$ChatBot::Name@": Error: You do not have a Registered GUID, please /Auth In.");
             closescorehudfserv(%client);
             return;
             }
             if(%client.cannotBuild) {
             MessageClient(%client,'Deny',"\c1From "@$ChatBot::Name@": Error: You are not permitted to load buildings, due to your revoked building rights.");
             closescorehudfserv(%client);
             return;
             }
             if(%client.cantLoad) {
             %x = MFloor($SaveTime::TimeLeft[%client.guid, "Load"] / 60);
             %y = $SaveTime::TimeLeft[%client.guid, "Load"] % 60;
             if(%x > 0) {
             MessageClient(%client,'Deny',"\c1From "@$ChatBot::Name@": You have only recently loaded a building, please wait "@%x@" Minutes and "@%y@" Seconds.");
             }
             else {
             MessageClient(%client,'Deny',"\c1From "@$ChatBot::Name@": You have only recently loaded a building, please wait "@%y@" Seconds.");
             }
             closescorehudfserv(%client);
             return;
             }
             %slot = %arg2;
             PERSloadBuilding($SaveFile::Save[%client.guid,%slot]);
             $SaveTime::TimeLeft[%client.guid, "Load"] = $TWM::CSSTimeLoad*60; //5 mins
             %client.cantLoad = 1;
             schedule(1,0,"ResetLoad",%client);
             messageall('MsgAdminForce', "\c3"@ %client.namebase@"\c2 is loading a building, Evaluating Power.");
             MessageClient(%client,'Success',"\c1From "@$ChatBot::Name@": Loading Building In Content Save Slot "@%slot@".");
             closescorehudfserv(%client);
             return;

        case "SaveWarn":
             %slot = %arg2;
             if($SaveFile::Save[%client.guid,%slot] !$= "") {
             messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:FF0000>WARNING, PIECES DETECTED, SAVE OVER THE SLOT?");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:FF0000>WARNING, BUILDINGS CANNOT BE RECOVERED IF SAVED OVER");
             %index++;
             }
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Content Saving System" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Save Pieces?   <a:gamelink\tSave\t"@%slot@">Yes</a>     <a:gamelink\tContSave\t1>No</a>");
             %index++;
             return;

        case "DeleteWarn":
             %slot = %arg2;
             if($SaveFile::Save[%client.guid,%slot] $= "") {
             MessageClient(%client,'Deny',"\c1From "@$ChatBot::Name@": Error: No pieces in this slot to delete.");
             closescorehudfserv(%client);
             return;
             }
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Content Saving System" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:FF0000>WARNING, BUILDINGS CANNOT BE RECOVERED IF DELETED");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Continue?   <a:gamelink\tDelYes\t"@%slot@">Yes</a> <a:gamelink\tContSave\t1>No</a>");
             %index++;
             return;

        case "DelYes":
             %slot = %arg2;
             %file = $SaveFile::Save[%client.guid,%slot];
             DeleteFile(%file);
             $SaveFile::PieceCT[%client.guid,%slot] = 0;
             $SaveFile::Save[%client.guid,%slot] = "";
             $SaveFile::SlotName[%client.guid,%slot] = "SLOT "@%slot@"";
             MessageClient(%client,'Success',"\c1From "@$ChatBot::Name@": Pieces in slot "@%slot@" have been deleted.");
             closescorehudfserv(%client);
             return;

        case "Ranks":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Player Information Listings" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select A Client");
             %index++;
             %count=clientgroup.getcount();
             for (%i = 0; %i < %count; %i++){
             %cid = ClientGroup.getObject( %i );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tRanksSM\t"@%cid@">"@%cid.namebase@"</a>");
             %index++;
             }
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             messageClient( %client, 'ClearHud', "", %tag, %index );
             return;
             
         case "inventoryWindow":
            %index = buildInventoryWindow(%client, %tag, %index);
            return;

         case "setScoreInv":
            %subZone = %arg2;
            switch$(%subZone) {
               case "Armor":
                  %client.scoreHudInv[Armor] = %arg3;
               case "Weapon":
                  //pull the current settings
                  %int = 1;
                  while(isSet(%client.scoreHudInv[Weapon, %int])) {
                     if(%client.scoreHudInv[Weapon, %int] $= %client.scoreHudInv[Weapon, %arg3]) {
                        %client.scoreHudInv[Weapon, %int] = "";
                     }
                     %int++;
                  }
                  %slot = %arg3;
                  %client.scoreHudInv[Weapon, %slot] = %arg4;
                  //now do a post set check
                  %xSlot = 1;
                  while(isSet(%client.scoreHudInv[Weapon, %xSlot])) {
                     //no two may co-exist, IE: be the same
                     %iSlot = 1;
                     while(isSet(%client.scoreHudInv[Weapon, %iSlot])) {
                        if(%client.scoreHudInv[Weapon, %iSlot] $= %client.scoreHudInv[Weapon, %xSlot]) {
                           if(%iSlot != %xSlot) {
                              //remove iSlot, proceed
                              %client.scoreHudInv[Weapon, %iSlot] = "";
                           }
                        }
                        %iSlot++;
                     }
                     %xSlot++;
                  }
               case "Pistol":
                  %client.scoreHudInv[Pistol] = %arg3;
               case "Melee":
                  %client.scoreHudInv[Melee] = %arg3;
               case "Pack":
                  %client.scoreHudInv[Pack] = %arg3;
               case "Grenade":
                  %client.scoreHudInv[Grenade] = %arg3;
               case "Mine":
                  %client.scoreHudInv[Mine] = %arg3;
               case "Ability":
                  %client.scoreHudInv[Ability] = %arg3;
               default:
                  error("Unknown Call to setScoreInv: "@%arg2@"/"@%arg3@"/"@%arg4@"");
            }
            Game.processGameLink(%client, "inventoryWindow");
            return;

        case "PrestigeWarn":
           messageClient( %client, 'ClearHud', "", %tag, %index );
           messageClient( %client, 'SetScoreHudSubheader', "", "Officer Ranks" );
           if(getCurrentEXP(%client) < $Ranks::MinPoints[61]) {
              messageClient( %client, 'SetLineHud', "", %tag, %index, "You must have the 'Master Commander' Rank To Proceed.");
              %index++;
              messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Exit</a>');
              %index++;
              return;
           }
           %page = %arg2;
           switch(%page) {
              case 1:
                 %next = %scriptController.officer + 1;
                 if(%scriptController.officer $= "" || %scriptController.officer == 0) {
                    %scriptController.officer = 0;
                    messageClient( %client, 'SetLineHud', "", %tag, %index, "Welcome to the Officer Ranks!");
                    %index++;
                    messageClient( %client, 'SetLineHud', "", %tag, %index, "Congradulations on completing the rank system");
                    %index++;
                    messageClient( %client, 'SetLineHud', "", %tag, %index, "Now it's time to progress further!");
                    %index++;
                 }
                 else {
                    messageClient( %client, 'SetLineHud', "", %tag, %index, "Welcome Back to Officer Ranking!");
                    %index++;
                 }
                 messageClient( %client, 'SetLineHud', "", %tag, %index, "The Officer Ranks are a way to hit the reset button in TWM2.");
                 %index++;
                 messageClient( %client, 'SetLineHud', "", %tag, %index, "You will unlock many new things by proceeding through these");
                 %index++;
                 messageClient( %client, 'SetLineHud', "", %tag, %index, "Officer ranks, yet it will become more difficult.");
                 %index++;
                 messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Cancel</a>');
                 %index++;
                 messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPrestigeWarn\t2>Continue</a>');
                 %index++;
              case 2:
			     if(GetOfficerCap(%next)) {
                    messageClient( %client, 'SetLineHud', "", %tag, %index, "*** This officer rank level is currently locked ***");
                    %index++;				
                    messageClient( %client, 'SetLineHud', "", %tag, %index, "***  Please try again at some other time/date  ***");
                    %index++;							
                    messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Return To Controls</a>');
                    %index++;			
					return;
				 }
                 messageClient( %client, 'SetLineHud', "", %tag, %index, "Although you will restart at the first rank, you gain");
                 %index++;
                 messageClient( %client, 'SetLineHud', "", %tag, %index, "the "@$Prestige::Name[%scriptController.officer]++@"title with your rank.");
                 %index++;
                 messageClient( %client, 'SetLineHud', "", %tag, %index, "This action cannot be undone once your rank is saved");
                 %index++;
                 messageClient( %client, 'SetLineHud', "", %tag, %index, "Are you sure you want to continue?");
                 %index++;
                 messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>No</a>');
                 %index++;
                 messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPrestigeWarn\t3>Yes</a>');
                 %index++;
              case 3:
			     if(GetOfficerCap(%next)) {
                    messageClient( %client, 'SetLineHud', "", %tag, %index, "*** This officer rank level is currently locked ***");
                    %index++;				
                    messageClient( %client, 'SetLineHud', "", %tag, %index, "***  Please try again at some other time/date  ***");
                    %index++;							
                    messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Return To Controls</a>');
                    %index++;			
					return;
				 }			
                 messageClient( %client, 'SetLineHud', "", %tag, %index, "This action CANNOT be undone once your rank is saved");
                 %index++;
                 messageClient( %client, 'SetLineHud', "", %tag, %index, "This is your last chance to turn back");
                 %index++;
                 messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Do Not Promote</a>');
                 %index++;
                 messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPrestigeWarn\t4>Promote Me Now!</a>');
                 %index++;
              case 4:
			     if(GetOfficerCap(%next)) {
                    messageClient( %client, 'SetLineHud', "", %tag, %index, "*** This officer rank level is currently locked ***");
                    %index++;				
                    messageClient( %client, 'SetLineHud', "", %tag, %index, "***  Please try again at some other time/date  ***");
                    %index++;							
                    messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Return To Controls</a>');
                    %index++;			
					return;
				 }			
                 PromoteToPrestige(%client);
                 messageClient( %client, 'SetLineHud', "", %tag, %index, "Congradulations, you have been promoted to the next Officer Rank!");
                 %index++;
                 messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Exit</a>');
                 %index++;
           }
           return;

        case "PersControl":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Personal Settings" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select An Option");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             if(%scriptController.officer < 9) {
                if(getCurrentEXP(%client) >= $Ranks::MinPoints[61]) {
                   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPrestigeWarn\t1>Promote To Next Officer Rank</a>');
                   %index++;
                }
                else {
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "Officer Ranking - Unlocked at Master Commander");
                   %index++;
                }
             }
             if(%scriptController.officer >= 1) {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Current Officer Rank Level: "@%scriptController.officer@"");
                %index++;
             }
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPerks\t1>Perks</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tKillstreaks\t1>Killstreak Superweapons</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tUpdateSettings\t1>Save Settings</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPGDConn1\t1>PGD Connect - In Game</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             messageClient( %client, 'ClearHud', "", %tag, %index );
             return;
             
        case "Killstreaks":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Killstreak Superweapons" );
             %index = GenerateKillstreakMenu(%client, %tag, %index);
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;
             
        case "SetStreakStat":
             %streak = %arg2;
             %stat = %arg3;
             %client.setStreakStatus(%streak, %stat);
             %game.processGameLink(%client, "Killstreaks");
             return;

        case "UpdateSettings":
             UpdateSettings(%client);
             MessageClient(%client, 'msgSaved', "\c5Settings Saved");
             %game.processGameLink(%client, "NAC");
             return;
             
        case "ActivateUpgrade":
             %image = %arg2;
             %upgrade = %arg3;
             %client.DisableAllUpgrades(%image); //disable all first
             %client.ActivateUpgrade(%image, %upgrade);
             %game.processGameLink(%client, "CompletedSub", %image);
             return;

        case "DeActivateUpgrades":
             %image = %arg2;
             %client.DisableAllUpgrades(%image); //disable all
             %game.processGameLink(%client, "CompletedSub", %image);
             return;
             
        case "CompletedSub":
             %image = %arg2;
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Completed Challenges" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select A Upgrade To Use");
             %index++;
             %index = GenerateCompletedSubMenu(%client, %tag, %index, %image);
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponChallenge\t1>Back to weapon challegnes</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;
             
        case "CompletedChallenge":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Completed Challenges" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select A Weapon");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             %index = GenerateCompletedChallegnesMenu(%client, %tag, %index);
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponChallenge\t1>Back to weapon challegnes</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;
             
        case "WeaponTasksSub":
             %image = %arg2;
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Weapon Challenges" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Challenges:");
             %index++;
             %index = GenerateWChallengeSubMenu(%client, %tag, %index, %image);
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponChallenge\t1>Back to weapon challegnes</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;

        case "WeaponsTasks":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Weapon Challenges" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select A Weapon");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             %index = GenerateWeaponChallegnesMenu(%client, %tag, %index);
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponChallenge\t1>Back to weapon challegnes</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;
             
        case "OtherTasks":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Weapon Challenges" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select A Category");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             %index = GenerateChallegnesMenu(%client, %tag, %index);
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponChallenge\t1>Back to weapon challegnes</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;
             
        case "OtherTasksSub":
             %client.SCMPage = "SM";
             %cate = %arg2;
             messageClient( %client, 'SetScoreHudSubheader', "", "Weapon Challenges" );
             %index = GetNonWeapSubMenu(%client, %tag, %index, %cate);
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tOtherTasks\t1>Back to challenge categories</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponChallenge\t1>Back to weapon challegnes</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;

        case "WeaponChallenge":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Weapon Challenges" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select An Option");
             %index++;
             //
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             //
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tCompletedChallenge\t1>Weapon Upgrades</a>');
             %index++;
             //
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponsTasks\t1>Weapon Challenges</a>');
             %index++;
             //
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tOtherTasks\t1>Additional Challenges</a>');
             %index++;
             //
             //%index = CreatePerkMenu(%client, %tag, %index);
             //
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             messageClient( %client, 'ClearHud', "", %tag, %index );
             return;
             
        case "RanksSM":
             messageClient( %client, 'SetScoreHudSubheader', "", ""@%arg2.namebase@"'s Stats Card" );
             %client.SCMPage = "SM";
			 %targetController = %arg2.TWM2Core;
             //Specs
             if(%targetController.officer $= "") {
                %targetController.officer = 0;
             }
             %rank = ""@$Prestige::Name[%targetController.officer]@""@%targetController.rank@"";
             %XP = ((%targetController.millionxp) * 1000000) + %targetController.xp;
             %mula = %targetController.money;
             %phrs = %targetController.phrase;
             %gmeTime = %targetController.gameTime;
             //Game Time
             if(%gmeTime $= "") {
                %gmeTime = 0;
             }
             if(%phrs $= "") {
                %phrs = "I don't have a Phrase";
             }
             else {
                %days = %targetController.gameTime / (60 * 24);
                %timeLeft = %targetController.gameTime % (60 * 24);
                %hours = %timeLeft / 60;
                %timeLeft = %hours % 60;
                %daysFloored = MFloor(%days);
                %hoursFloored = MFloor(%hours);
                %timeString = ""@%daysFloored@" Days, "@%hoursFloored@" Hours, "@%timeLeft@" Minutes";
             }
             //Card
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Rank: "@%rank@", XP Points: "@%XP@", Ranked "@%targetController.topRank@" / "@$Rank::numplayers@".");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Money: $"@%mula@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "TWM2 Time Played: "@%timeString@".");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Phrase: "@%phrs@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "My Medal Collection");
             %index++;
             //
             %index = GetClientMedals(%client, %arg2, %tag, %index);
             //
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "-Weapon Kills-");
             %index++;
             %count = DatablockGroup.getCount();
             for(%i = 0; %i < %count; %i++) {
                %db = DatablockGroup.GetObject(%i);
                if(%db.getName().getClassname() $= "ItemData") {
                   if(%db.getName().classname $= "Weapon") {
                      %Image = %db.getName().image;
                      if(%Image.HasChallenges) {
                         %kills = GetKills(%arg2, %image);
                         if(%kills $= "") {
                            %kills = 0;
                         }
                         if(DoMedalCheck(%client, %image) == 1 && CanUseRankedWeapon(%image, %client) == 1) {
                            messageClient( %client, 'SetLineHud', "", %tag, %index, ""@%Image.GunName@" - Kills: "@%kills@"");
                            %index++;
                         }
                         else {
                            messageClient( %client, 'SetLineHud', "", %tag, %index, "Unknown Weapon - Kills: "@%kills@"");
                            %index++;
                         }
                      }
                   }
                }
             }
             if(%targetController.UAVCalls $= "") {
                %targetController.UAVCalls = 0;
             }
             if(%targetController.AirstrikeCalls $= "") {
                %targetController.AirstrikeCalls = 0;
             }
             if(%targetController.HeliCalls $= "") {
                %targetController.HeliCalls = 0;
             }
             if(%targetController.GMCalls $= "") {
                %targetController.GMCalls = 0;
             }
             if(%targetController.HarrierCalls $= "") {
                %targetController.HarrierCalls = 0;
             }
             if(%targetController.GunHeliCalls $= "") {
                %targetController.GunHeliCalls = 0;
             }
             if(%targetController.SlthAirstrikeCalls $= "") {
                %targetController.SlthAirstrikeCalls = 0;
             }
             if(%targetController.HWCalls $= "") {
                %targetController.HWCalls = 0;
             }
             if(%targetController.CGCalls $= "") {
                %targetController.CGCalls = 0;
             }
             if(%targetController.ArtyCalls $= "") {
                %targetController.ArtyCalls = 0;
             }
             if(%targetController.NukeCalls $= "") {
                %targetController.NukeCalls = 0;
             }
             if(%targetController.ZBCalls $= "") {
                %targetController.ZBCalls = 0;
             }
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "**Killstreak Superweapon Calls**");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "UAV Calls: "@%targetController.UAVCalls@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Airstrikes: "@%targetController.AirstrikeCalls@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Guided Missile Strikes (UAMS): "@%targetController.GMCalls@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Helicopters: "@%targetController.HeliCalls@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Harrier Airstrikes: "@%targetController.HarrierCalls@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Gunship Helicopters: "@%targetController.GunHeliCalls@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Stealth Bombers: "@%targetController.SlthAirstrikeCalls@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Gunships: "@%targetController.HWCalls@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Apaches: "@%targetController.CGCalls@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Artillery Strikes: "@%targetController.ArtyCalls@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Nukes: "@%targetController.NukeCalls@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Z-Bombs: "@%targetController.ZBCalls@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tRanks>Back to P.I.L.</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;

        case "BL15":
             messageClient( %client, 'SetScoreHudSubheader', "", "The Blacklist 15" );
             %client.SCMPage = "SM";
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Top 15 Ranks");
             %index++;
             for(%i = 1; %i < 16; %i++) {
                if(%client.namebase $= $Rank::Top[%i]) {
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>"@%i@". "@$Rank::Top[%i]@" - "@$Rank::TopRank[%i]@" - "@$Rank::TopXP[%i]@"XP");
                   %index++;
                   CompleteNWChallenge(%client, "Acceptance");
                }
                else {
                   messageClient( %client, 'SetLineHud', "", %tag, %index, ""@%i@". "@$Rank::Top[%i]@" - "@$Rank::TopRank[%i]@" - "@$Rank::TopXP[%i]@"XP");
                   %index++;
                }
             }
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;

        case "PC":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Piece Count" );
             %count=clientgroup.getcount();
             %counter=deployables.getcount();
             for (%n=0;%n<%counter;%n++) {
                 %obj = deployables.getObject(%n);
                 %totalPC++;
                 %piececount[%obj.ownerguid]++;
                 if(!%obj.ownerguid)
                 %orphPC++;
                 }
             %count=clientgroup.getcount();
             for (%i = 0; %i < %count; %i++){
                 %cid = ClientGroup.getObject( %i );
                 if(%cid.isAIControlled()) {
                 messageClient( %client, 'SetLineHud', "", $TagToUseForScoreMenu, %index, '<tab:25>\t<clip:195>%1</clip><rmargin:260><just:right>%2',
                 %cid.namebase,'AI' );
                 %index++;
                 }
                 if(!%cid.isAIControlled()) {
                 messageClient( %client, 'SetLineHud', "", $TagToUseForScoreMenu, %index, '<tab:25>\t<clip:195>%1</clip><rmargin:260><just:right>%2',
                 %cid.namebase,%piececount[%cid.guid] );
                 %index++;
                 }
                 }
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Orphanned Pieces : "@%orphPC@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Total Pieces Used: "@%totalPC@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Pieces Left (Apprx) : "@1080 - %totalPC@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             messageClient( %client, 'ClearHud', "", %tag, %index );
             return;
             
        case "Perks":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "PERKS" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "--- ACTIVE PERKS ---");
             %index++;
             GetActivePerks(%client);  //Reload This First
             messageClient( %client, 'SetLineHud', "", %tag, %index, "PRIMARY: "@%client.Perk[1]@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "SECONDARY: "@%client.Perk[2]@"");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "TERTIARY: "@%client.Perk[3]@"");
             %index++;
             //
             //
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "--- AVAILIABLE PERKS ---");
             %index++;
             //
             %index = CreatePerkMenu(%client, %tag, %index);
             //
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             messageClient( %client, 'ClearHud', "", %tag, %index );
             return;

        case "PerkStatus":
           DisableAllPerkGroup(%client, $Perk::PerkToGroup[%arg2]);
           SetPerkStatus(%client, %arg2, 1);
           %game.processGameLink(%client, "Perks");
           return;

        default:
           %client.notFirstUse = 1;
        }
        closeScoreHudFSERV(%client);
}

//Skin & bot set
function SetSkin(%client,%newskin) {
if (!IsObject(%client))
return "Invalid client!";

FreeClientTarget(%client);
%client.skin = addtaggedstring(%newskin);
%client.target = allocClientTarget(%client, %client.name, %client.skin, %client.voiceTag, '_ClientConnection', %client.team, 0, %client.voicePitch);

if (IsObject(%client.player))
%client.player.setTarget(%client.target);

return %client SPC %newskin;
}

function customizebot(%bot,%race,%sex,%name,%skin,%voicetag,%pitch)
{
    %bot.race = %race;
    %bot.sex = %sex;
    %bot.voice = addtaggedstring(%voicetag);
    freeclienttarget(%bot);
   %bot.target = allocClientTarget(%bot, %bot.name, %bot.skin, %bot.voiceTag, '_ClientConnection', 0, 0, %bot.voicePitch);
}
//End

function closeScoreHudFSERV(%client) {
serverCmdHideHud(%client, 'scoreScreen');
//ResetQuiz(%client, $TagToUseForScoreMenu, "ALL", 1);
commandToClient(%client, 'setHudMode', 'Standard', "", 0);
%client.SCMPage = 1;
%client.notFirstUse = 1;
}

function scoreCmdMainMenu(%game,%client,%tag,%page) {
messageClient( %client, 'ClearHud', "", %tag, 1 );
if (!isobject(cmdobject)) generateCMDObj();
   messageClient( %client, 'SetScoreHudSubheader', "", "Main Menu Page " @ %page);
if (%page > 1) {
   %pgToGo = %page - 1;
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t%1>Previous Page</a>',%pgToGo);
   %index++;
   }
%cmdsToDisp = 15 * %page;
%start = (%page - 1) * 15;
for (%i=%start; %i < %cmdsToDisp;%i++) {
    %line = CmdObject.cmd[%i];
    if (%line !$= "") {
       messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\t%1>%2</a>',getword(%line,0),getwords(%line,1));
       %index++;
    }
}
if (%cmdsToDisp < (CmdObject.commands + 1)) {
   %pgToGo = %page + 1;
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t%1>Next Page</a>',%pgToGo);
   %index++;
   }
if (%page > 1) {
   messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>First Page</a>");
   %index++;
   }
messageClient( %client, 'ClearHud', "", %tag, %index );
}


//format
//CMD indentifier displayname
//CMDHELP identifier help message for cmd gonna implement it
//after noobs get their hands on the base script first

function GenerateCMDObj() {
new fileobject("fIn");
fIn.openforread("scripts/TWM2/cmddisplaylist.txt");
if (isobject(cmdobject)) cmdobject.delete();
   new scriptObject("CmdObject") {commands=0;};
while (!fIn.iseof()) {
      %line = fIn.readline();
      if (getword(%line,0) $= "CMD") {
         CmdObject.cmd[CmdObject.commands] = getwords(%line,1);
         CmdObject.commands++;
      }
}

fIn.close();
fIn.delete();
}

// CONTENT SAVING
function CheckSlotStatus(%cl,%slot) {
   if($SaveFile::Save[%cl.guid,%slot] $= "") {
      %Stat = "This Slot Is Empty";
   }
   else {
      %Stat = "This Slot Has "@$SaveFile::PieceCT[%cl.guid,%slot]@" Saved Pieces";
   }
   return %Stat;
}

//Checks to see if the file CAN, or Should be loaded
function RunLoadCheck(%cl, %slot, %PC) {
   if(%cl.cantLoad || $SaveFile::PieceCT[%client.guid,%slot] > %PC) {
   %str = "<color:FF0000><a:gamelink\tLoad\t"@%slot@">Load</a>"; //Return the Red Link
   return %str;
   }
   else {
   %str = "<color:33FF00><a:gamelink\tLoad\t"@%slot@">Load</a>"; //Return the Green Link
   return %str;
   }
}

function RunSaveCheck(%cl, %slot) {
   if(%cl.cantSave) {
   %str = "<color:FF0000><a:gamelink\tSaveWarn\t"@%slot@">Save</a>"; //Return the Red Link
   return %str;
   }
   else if($SaveFile::Save[%cl.guid,%slot] $= "" && !%cl.cantSave && $Phantom::CSSEnabled) {
   %str = "<color:33FF00><a:gamelink\tSaveWarn\t"@%slot@">Save</a>"; //Return the Green Link
   return %str;
   }
   else {
   %str = "<color:FFFF66><a:gamelink\tSaveWarn\t"@%slot@">Save</a>"; //Return the Yellow Link
   return %str;
   }
}

function RunDeleteCheck(%cl, %slot) {
   if($SaveFile::Save[%cl.guid,%slot] $= "") {
   %str = "<color:FF0000><a:gamelink\tDeleteWarn\t"@%slot@">Delete</a>"; //Return the Red Link
   return %str;
   }
   else {
   %str = "<color:FFFF66><a:gamelink\tDeleteWarn\t"@%slot@">Delete</a>"; //Return the Yellow Link
   return %str;
   }
}











//------------------------------------
function PGDConnect_FromInGame(%client) {
   if($TWM2::PGDConnectDisabled) {
      echo("PGD Connect is disabled.");
      return;
   }
   %guid = %client.guid;
   %email = %client.emailSet;
   if(!isSet(%email)) {
      MessageClient(%client, 'msgClient', "\c3SERVER: Must set an email address");
      return;
   }
   //======
   $PGD::IsPGDConnected[%guid] = 0; //some funky setting always brings this to 1 before reg. happens
   %tcp = new TCPObject(TCPIGCObject);
   
   %tcp.client = %client;
   %tcp.guid = %client.guid;
   %tcp.email = %client.emailSet;
   %tcp.connect("www.phantomdev.net:80");
   
   %tcp.timeout = %tcp.schedule(10000, disconnect);
}

function TCPIGCObject::onConnected(%this) {

   %sep = getRandomSeparator(16);
   %loc = "/public/Univ/submit.php";
   %header1 = "POST" SPC %loc SPC "HTTP/1.1\r\n";
   %host = "Host: www.phantomdev.net\r\n";
   %header2 = "Connection: close\r\nUser-Agent: Tribes 2\r\n";
   %contType = "Content-Type: multipart/form-data; boundary="@%sep@"\r\n";
   %guidReq = "--"@%sep@"\r\nContent-Disposition: form-data; name=\"guid\"\r\n\r\n"@%this.guid@"";
   %emailReq = "--"@%sep@"\r\nContent-Disposition: form-data; name=\"email\"\r\n\r\n"@%this.email@"";
   %payload = %guidReq@"\r\n"@%emailReq@"\r\n--"@%sep@"--";
   %qlen = strLen(%payload);
   %contentLeng = "Content-Length: "@%qlen@"\r\n\r\n";
   %query = %header1@%host@%header2@%contType@%contentLeng@%payload;
   echo("Connected to Phantom Games Server, Sending Connection Data...");
   if($debugmode == 1) {
      echo(%query);
   }
   %this.send(%query);
}

function TCPIGCObject::onLine(%this, %line) {
   echo(%line);
   if(strstr(%line, "Data added to PGD Connect") != -1) {
      MessageClient(%this.client, 'msgClient', "\c3SERVER: Data registered to PGD Connect...");
      %this.disconnect();
      $PGD::IsPGDConnected[%this.guid] = 1;
   }
   else if(strstr(%line, "This account is already registered") != -1) {
      MessageClient(%this.client, 'msgClient', "\c3SERVER: You have already registered to PGD connect.");
      %this.disconnect();
      $PGD::IsPGDConnected[%this.guid] = 1;
   }
   else {

   }
}

function TCPIGCObject::onDisconnect(%this) {
   closeScoreHudFSERV(%this.client);
   %this.delete();
}

