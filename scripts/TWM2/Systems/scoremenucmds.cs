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
	switch$ (%arg1) {
        case "GTP":
             scoreCmdMainMenu(%game,%client,$TagToUseForScoreMenu,%arg2);
             %client.SCMPage = %arg2;
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
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "Mission Time Window: "@$Mission::VarSet[""@%mission@"", "TimeLimit"] / 60@" Minutes");
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
			 
		case "StatResetWarn":
			messageClient( %client, 'ClearHud', "", %tag, %index );
			messageClient( %client, 'SetScoreHudSubheader', "", "Stat Reset" );
			if(getCurrentEXP(%client) < $Ranks::MinPoints[61] && %scriptController.officer < 15) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, "You must have the 'Harbinger Master Commander' Rank To Proceed.");
				%index++;
				messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Exit</a>');
				%index++;
				return;
			}
			%page = %arg2;	
			switch(%page) {
				case 1:
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Congratulations on reaching the end of the rank progression!");
					%index++;				
					messageClient( %client, 'SetLineHud', "", %tag, %index, "But, are you longing for that progression adventure once more?");
					%index++;
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Do you feel like there's nothing to strive for anymore?");
					%index++;				
					messageClient( %client, 'SetLineHud', "", %tag, %index, "By reaching the last rank, you can choose to do a FULL RESET.");
					%index++;	
					messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Get Me Out of Here</a>');
					%index++;
					messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tStatResetWarn\t2>Learn More</a>');
					%index++;

				case 2:
					messageClient( %client, 'SetLineHud', "", %tag, %index, "By proceeding through here, you can reset at rank zero...");
					%index++;				
					messageClient( %client, 'SetLineHud', "", %tag, %index, "It will be like you've never played TWM2 before");
					%index++;
					messageClient( %client, 'SetLineHud', "", %tag, %index, "All medals, challenges, unlocks will be removed.");
					%index++;
					messageClient( %client, 'SetLineHud', "", %tag, %index, "You only get to keep your play time and phrase.");
					%index++;					
					messageClient( %client, 'SetLineHud', "", %tag, %index, "But, you can do it all over again!!!");
					%index++;	
					messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Get Me Out of Here</a>');
					%index++;
					messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tStatResetWarn\t3>Continue</a>');
					%index++;	

				case 3:
					messageClient( %client, 'SetLineHud', "", %tag, %index, "DANGER: THIS ACTION IS IRREVERSABLE!!!");
					%index++;				
					messageClient( %client, 'SetLineHud', "", %tag, %index, "THIS IS YOUR LAST CHANCE TO ABORT");
					%index++;
					messageClient( %client, 'SetLineHud', "", %tag, %index, "CLICK BELOW AT YOUR OWN RISK!!!");
					%index++;				
					messageClient( %client, 'SetLineHud', "", %tag, %index, "");
					%index++;	
					messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>No, I\'m Not Thinking Clearly!!!</a>');
					%index++;
					messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tStatResetWarn\t4>Wipe Me From TWM2 Existence</a>');
					%index++;	

				case 4:
					WipeStats(%client);
					messageClient( %client, 'SetLineHud', "", %tag, %index, "It has been done... Now... Begone!!!");
					%index++;
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Thank you for using the Phantom139 Memory wipe Services...");
					%index++;					
					messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Exit</a>');
					%index++;					
				
			}
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
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Congratulations on reaching the rank of Master Commander");
						%index++;
						messageClient( %client, 'SetLineHud', "", %tag, %index, "But if you thought you were done.... you thought wrong...");
						%index++;
						messageClient( %client, 'SetLineHud', "", %tag, %index, "The Officer Ranks are your next step of progression in TWM2.");
						%index++;
						messageClient( %client, 'SetLineHud', "", %tag, %index, "");
						%index++;	
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Officer promotion effectively hits that reset button on your account");
						%index++;
						messageClient( %client, 'SetLineHud', "", %tag, %index, "restarting you at level one with zero EXP, but you'll move forward");
						%index++;					
						messageClient( %client, 'SetLineHud', "", %tag, %index, "This action is not in vain, as you unlock some cool new items!");
						%index++;
						messageClient( %client, 'SetLineHud', "", %tag, %index, "To assist your path, you'll gain some new EXP gain methods to help.");
						%index++;	
						messageClient( %client, 'SetLineHud', "", %tag, %index, "So, with that in mind, are you ready to move on to the next step?");
						%index++;					
					}
					else {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "Welcome Back to Officer Ranking!");
						%index++;
						messageClient( %client, 'SetLineHud', "", %tag, %index, "You made it again! Master Commander once more!");					
						%index++;
						messageClient( %client, 'SetLineHud', "", %tag, %index, "However, as you expected, you're still not done yet!");
						%index++;
						messageClient( %client, 'SetLineHud', "", %tag, %index, "As a reminder, you'll lose it all, but gain more.");
						%index++;		
						messageClient( %client, 'SetLineHud', "", %tag, %index, "So, are you ready to enter the next office rank?");
						%index++;					
					}
					messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Cancel</a>');
					%index++;
					messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPrestigeWarn\t2>Continue</a>');
					%index++;
					
				case 2:
					if(fetchCap("Officer", %next)) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "*** This officer rank level is currently locked ***");
						%index++;				
						messageClient( %client, 'SetLineHud', "", %tag, %index, "***  Please try again at some other time/date  ***");
						%index++;							
						messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Return To Controls</a>');
						%index++;			
						return;
					}
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Although you will restart at the level 1, you gain");
					%index++;
					messageClient( %client, 'SetLineHud', "", %tag, %index, "the "@trim($Prestige::Name[%scriptController.officer + 1])@" title with your rank.");
					%index++;
					messageClient( %client, 'SetLineHud', "", %tag, %index, "");					
					%index++;	
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Rewards Earned for Promoting to Officer Level "@%scriptController.officer + 1@":");
					%index++;
					messageClient( %client, 'SetLineHud', "", %tag, %index, " * 1 Additional Killstreak Slot");
					%index++;
					messageClient( %client, 'SetLineHud', "", %tag, %index, " * "@$Prestige::Rewards[%scriptController.officer + 1]);
					%index++;					
					messageClient( %client, 'SetLineHud', "", %tag, %index, "");
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
					if(fetchCap("Officer", %next)) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "*** This officer rank level is currently locked ***");
						%index++;				
						messageClient( %client, 'SetLineHud', "", %tag, %index, "***  Please try again at some other time/date  ***");
						%index++;							
						messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Return To Controls</a>');
						%index++;			
						return;
					}			
					messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:FF0000>WARNING</color> This action CANNOT be undone!!!");
					%index++;
					messageClient( %client, 'SetLineHud', "", %tag, %index, "This is your last chance to turn back");
					%index++;
					messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Do Not Promote</a>');
					%index++;
					messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPrestigeWarn\t4>Shut Up And Promote Me Now!</a>');
					%index++;
					
				case 4:
					if(fetchCap("Officer", %next)) {
						messageClient( %client, 'SetLineHud', "", %tag, %index, "*** This officer rank level is currently locked ***");
						%index++;				
						messageClient( %client, 'SetLineHud', "", %tag, %index, "***  Please try again at some other time/date  ***");
						%index++;							
						messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Return To Controls</a>');
						%index++;			
						return;
					}			
					PromoteToPrestige(%client);
					messageClient( %client, 'SetLineHud', "", %tag, %index, "Congratulations, you have promoted to a new officer rank!!!");
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
             if(%scriptController.officer < $OfficerCap[$TWM2Core_Code, sha1sum(formattimestring("yymmdd"))]) {
                if(getCurrentEXP(%client) >= $Ranks::MinPoints[61]) {
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPrestigeWarn\t1>Promote To Officer Level "@%scriptController.officer + 1@"</a>");
                   %index++;
                }
                else {
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "Officer Ranking - Requires Master Commander (Level 62)");
                   %index++;
                }
             }
             else {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Maximum Officer Level Achieved, Congratulations!!!");
                %index++; 
                messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:FF0000><a:gamelink\tStatResetWarn\t1>Reset My Stats</a>: Reset To Level 1, Officer 0</color>");
                %index++;				
             }
             if(%scriptController.officer >= 1) {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Current Officer Rank Level: "@%scriptController.officer@" ("@trim($Prestige::Name[%scriptController.officer])@")");
                %index++;
             }
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponUpgrades\t1>Weapon Attachments & Upgrades</a>');
             %index++;			 
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPerks\t1>Perks</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tKillstreaks\t1>Killstreak Superweapons</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tUpdateSettings\t1>Save Game Settings</a>');
             %index++;
			 if(!%client.IsPGDConnected()) {
				messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPGDConn1\t1>PGD Connect - In Game</a>');
				%index++;
			 }
			 else {
				messageClient( %client, 'SetLineHud', "", %tag, %index, 'PGD Connect Status: <color:33FF00>Connected</color>');
				%index++;			 
			 }
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
             %game.processGameLink(%client, "WeaponUpgradesSub", %image);
             return;

        case "DeActivateUpgrades":
             %image = %arg2;
             %client.DisableAllUpgrades(%image); //disable all
             %game.processGameLink(%client, "WeaponUpgradesSub", %image);
             return;
             
        case "WeaponUpgradesSub":
             %image = %arg2;
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Personal Settings" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select A Upgrade To Use");
             %index++;
             %index = GenerateCompletedSubMenu(%client, %tag, %index, %image);
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;			 
             messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tWeaponTasksSub\t"@%image@">Jump to Weapon Challenge Page</a>");
             %index++;			 
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponUpgrades\t1>Return to Weapon List</a>');
             %index++;			 
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Return to Settings Menu</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;
             
        case "WeaponUpgrades":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Personal Settings" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select A Weapon");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             %index = GenerateCompletedChallegnesMenu(%client, %tag, %index);
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPersControl\t1>Return to Settings Menu</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;
             
        case "WeaponTasksSub":
             %image = %arg2;
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Challenges" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Challenges:");
             %index++;
             %index = GenerateWChallengeSubMenu(%client, %tag, %index, %image);
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;			 
             messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tWeaponUpgradesSub\t"@%image@">Jump to Weapon Attachments Page</a>");
             %index++;			 
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponsTasks\t1>Return to Weapon List</a>');
             %index++;			 
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tChallenge\t1>Return to Challenge Menu</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;

        case "WeaponsTasks":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Challenges" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select A Weapon");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             %index = GenerateWeaponChallegnesMenu(%client, %tag, %index);
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tChallenge\t1>Return to Challenge Menu</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;
             
        case "OtherTasks":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Challenges" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select A Category");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             %index = GenerateChallengesMenu(%client, %tag, %index);
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tChallenge\t1>Return to Challenge Menu</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;
             
        case "OtherTasksSub":
             %client.SCMPage = "SM";
             %cate = %arg2;
             messageClient( %client, 'SetScoreHudSubheader', "", "Challenges" );
             %index = GenerateChallengeSubMenu(%client, %cate, %tag, %index);
             messageClient( %client, 'SetLineHud', "", %tag, %index, "");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tOtherTasks\t1>Return to General Tasks</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tChallenge\t1>Return to Challenge Menu</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;

        case "Challenge":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Challenges" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "TWM2 Challenges");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tOtherTasks\t1>General Tasks</a>');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tWeaponsTasks\t1>Weapon Specific Challenges</a>');
             %index++;			 
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
             %XP = printCurrentEXP(%arg2);
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
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Rank: "@%rank@", XP Points: "@%XP@".");
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
                   //CompleteNWChallenge(%client, "Acceptance");
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

