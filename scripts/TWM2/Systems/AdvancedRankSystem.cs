$TWM::RanksDirectory = "Server/TWM/Saved";
// This is where all of the ranks will be saved
// I highly recommend leaving this alone

function LoadRanksBase() {
   echo("Loading The Ranking System Base");
   findTopRanks();
   //Modified 2.6, top ranks handled by PGD now
}

function CreateClientRankFile(%client) {
   if(!isSet(%client) || %client.guid $= "") {
      return;
   }
   if(%client.donotupdate) {
      echo("Stopped rank file make on "@%client@", server denies access (probably loading univ rank)");
      return;
   }
   $ModFile.openforWrite(""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave");
   $ModFile.WriteLine("//Ranks & Settings File For GUID "@%client.guid@" / Name: "@%client.namebase@"");
   $ModFile.WriteLine("//Created On "@formattimestring("yy-mm-dd")@", Total Warfare Mod 2 "@$TWM2::Version@"");
   $ModFile.close();
   ClientContainer(%client);
   %scriptController = %client.TWM2Core;
   if(!isObject(%scriptController)) {
      //yikes, no script object controller yet, time to create it
      %soNAME = "TWM2Client_"@%client.guid@"";
      %client.TWM2Core = new ScriptObject(%soNAME) {};
      %scriptController = %client.TWM2Core;  
   }
   //now apply the base settings for this new file.
   %scriptController.name = %client.namebase;
   %scriptController.xp = 0;
   %scriptController.rank = "Private";
   %scriptController.phrase = "None Set";
   %scriptController.gameTime = 0;
   %scriptController.millionxp = 0;
   //and save the new file
   %client.container.add(%scriptController);
   //
   echo("Ranks File For "@%client.namebase@" created");
   exec(""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave");
   MessageAll('WelcomeTheNoob',"\c4"@$ChatBot::Name@": Welcome To Total Warfare Mod For The First Time "@%client.namebase@".");
}

function LoadClientRankfile(%client) {
	if(!isSet(%client) || %client.guide $= "") {
		messageClient(%player.client, 'LeaveMissionArea', '\c1Alert: No GUID detected on your client object, please re-connect to the server...~wfx/misc/warning_beep.wav');
		return;
	}
	%client.donotupdate = 0;
	echo("Attempting To Load "@%client.namebase@"'s Ranks File");
	%file = ""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave";
	if(!isFile(%file)) {
		echo(""@%client.namebase@" does not have a save file, creating one.");
		CreateClientRankFile(%client);
	}
	else {
		LoadClientFile(%client);
	}
	//define a new script object for the client, if it does not yet exist
	%soNAME = "Container_"@%client.guid@"/TWM2Client_"@%client.guid@"";
	%object = nameToId(%soNAME);
	if(!isObject(%object)) {
		echo("TWM2 Rank/Setting Client Controller Object is non-existant, creating");
		%client.TWM2Core = new ScriptObject("TWM2Client_"@%client.guid) {};
		%client.container.add(%client.TWM2Core);
	}
	else {
		echo("Found TWM2 Rank/Setting Client Controller for "@%client@" -> "@%object@"");
		%client.TWM2Core = %object;
	}
	//Check Officer Challenges.
	for(%i = %client.TWM2Core.officer; %i > 0; %i--) {
		%oChN = "Prestige"@%i;
		CompleteNWChallenge(%client, %oChN);
	}   
	TWM2Lib_MainControl("PlayerTimeLoop", %client); //post load functions
}

function UpdateClientRank(%client) {
	if(!isSet(%client) || %client.guid $= "") {
		return;
	}
	if(%client.donotupdate) {
		echo("Stopped rank up check on "@%client@", server denies access (probably loading univ rank)");
		return;
	}
	%scriptController = %client.TWM2Core;
	if($XPArray[%client] <= 0) {
		return; //kill it here, no need to go into the loop
	}
	if(%scriptController.officer $= "") {
		%scriptController.officer = 0;
	}
	%file = ""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave";
	//If I ever do so implement an EXP cap, here is where it is placed
	%multi = $EXPMulti[$TWM2Core_Code, formattimestring("yymmdd"), sha1sum($TWM2Core_Code TAB TWM2Lib_MainControl("FormatTWM2Time", formattimestring("yymmdd")))];
	if(!isSet(%multi) || %multi < 1) {
		%multi = 1;
	}
	// convert it to second form
	if(!isSet(%scriptController.millionxp)) {
		%scriptController.millionxp = 0;
	}
	if((%scriptController.xp + $XPArray[%client]) >= 1000000) {
		%scriptController.xp = 0;
		%scriptController.millionxp++;
		$XPArray[%client] = (%scriptController.xp + $XPArray[%client]) - 1000000;
		if($XPArray[%client] < 0) {
			//Hmmm.... something wierd going on here...
			$XPArray[%client] = 0;
		}
	}
	%scriptController.xp += $XPArray[%client];

	checkForXPAwards(%client);
	$XPArray[%client] = 0;
	%j = $Rank::RankCount;
	runRankUpdateLoop(%client, %j, 1);
}

function runRankUpdateLoop(%client, %j, %continue) {
	if(!%continue) {
		return;
	}
	if(%j <= 0) {
		return;
	}
	%name = %client.namebase;
	%scriptController = %client.TWM2Core;   
	//perform rank update
	if(getCurrentEXP(%client) >= $Ranks::MinPoints[%j]){
		if(%scriptController.rank !$= $Ranks::NewRank[%j] && !fetchCap("Level", ((%scriptController.officer)*$Rank::RankCount)+%j)) {
			%scriptController.rankNumber = %j;
			if($TWM2::UseRankTags) {
				DoNameChangeChecks(%client);
			}
			%scriptController.rank = $Ranks::NewRank[%j];
			if($Prestige::Name[%scriptController.officer] >= 1) {
				$Prestige::Name[%scriptController.officer] = "";
			}
			messageAll('msgclient',"\c2"@%name@" has become a "@$Prestige::Name[%scriptController.officer]@""@$Ranks::NewRank[%j]@" with a XP of "@printCurrentEXP(%client)@"!");
			messageclient(%client, 'Msgclient', "~wfx/Bonuses/Nouns/General.wav");
			bottomPrint(%client, "Excelent work "@%name@", you have been promoted to the rank of: "@$Prestige::Name[%scriptController.officer]@""@$Ranks::NewRank[%j]@"!", 5, 2 );
			echo("Promotion: "@%name@" to Rank "@$Ranks::NewRank[%j]@", XP: "@getCurrentEXP(%client)@".");
			if(%j == $Rank::RankCount && %scriptController.officer < $OfficerCap[$TWM2Core_Code, sha1sum(formattimestring("yymmdd"))]) {
				messageclient(%client, 'Msgclient', "\c5Congratulations, you have reached the maximum rank in TWM2 and have unlocked the ability to enter an officer rank. To proceed, open the [F2] menu and select the Settings option.");
			}
			SaveClientFile(%client);
			//
			if(!$TWM2::PGDConnectDisabled) {
				PrepareUpload(%client);
			}
			%j = 1;
			runRankUpdateLoop(%client, %j, 0);
		}
	}
	else {
		%j--;
		runRankUpdateLoop(%client, %j, 1);
	}
}

function fetchCap(%type, %index) {
	if(%type $= "Officer") {
		if(!isSet($OfficerCap[$TWM2Core_Code, sha1sum(formattimestring("yymmdd"))]) || $OfficerCap[$TWM2Core_Code, sha1sum(formattimestring("yymmdd"))] <= 0) {
			return false;
		}
		else {
			if(%index >= $OfficerCap[$TWM2Core_Code, sha1sum(formattimestring("yymmdd"))]) {
				return true;
			}
			else {
				return false;
			}
		}    
	}
	else if(%type $= "Level") {
		if(!isSet($RankCap[$TWM2Core_Code, sha1sum(formattimestring("yymmdd"))]) || $RankCap[$TWM2Core_Code, sha1sum(formattimestring("yymmdd"))] <= 0) {
			return false;
		}
		else {
			if(%index >= $RankCap[$TWM2Core_Code, sha1sum(formattimestring("yymmdd"))]) {
				return true;
			}
			else {
				return false;
			}
		}       
	}
	else if(%type $= "EXP") {
		echo("fetchCap(): Call to EXP cap made, however the EXP cap has been depricated. use trace(1) to log the call stack.");
		return false;
	}
}

function checkForXPAwards(%client) {
   %scriptController = %client.TWM2Core;
   %xp = getCurrentEXP(%client);
   if(%xp >= 3000000 && !$Medals::AboutDamnTime[%client.guid]) {
      AwardClient(%client, "5");
   }
   else if(%xp >= 250000 && !$Medals::HonorsC[%client.guid]) {
      AwardClient(%client, "4");
   }
   else if(%xp >= 25000 && !$Medals::HonorsB[%client.guid]) {
      AwardClient(%client, "3");
   }
   else if(%xp >= 2500 && !$Medals::HonorsA[%client.guid]) {
      AwardClient(%client, "2");
   }
}

function getCurrentEXP(%client) {
   %scriptController = %client.TWM2Core;
   %xp = %scriptController.xp + (1000000*%scriptController.millionxp);
   return %xp;
}

function printCurrentEXP(%client) {
    //print function shows a more readable version of EXP
    %scriptController = %client.TWM2Core;
    %milXP = %scriptController.millionxp;
    %nonMilXP = %scriptController.xp;
    return (%milXP == 0 ? "" : %milXP) @ "" @ %nonMilXP;
}

//PRESTIGE RANKS
function PromoteToPrestige(%client) {
   %scriptController = %client.TWM2Core;
   %savedGameTime = %scriptController.gameTime;
   %savedPhrs = %scriptController.phrase;
   if(%scriptController.officer $= "" || %scriptController.officer == 0) {
      %next = 1;
   }
   else {
      %next = %scriptController.officer++;
   }
   
   if(fetchCap("Officer", %next)) {
      error("Client "@%client@"["@%client.getAddress()@"]("@%client.namebase@":"@%client.guid@") attempting to hack past cap.");
      error("It is recommended you report these details to Phantom139 (phantom139@phantomdev.net) ASAP.");
      error("Client has been informed of this, if it is reported to be a mistake, inform Phantom139 of possible code error");
      messageClient(%client, 'msgAlert', "\c3Alert! You have performed an Illegal action(trying to promote to an officer rank beyond cap level)\nIf you believe this is a mistake, you should inform the server host ASAP.");
	  return;
   }

   DumpStats(%client);
   
   %file = ""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave";
   
   %name = "ClientSettings"@%client.guid@"";
   %check = nameToID(%name);
   if(isObject(%check)) {
      %check.delete(); //kill current settings, as they are no longer valid.
   }
   %script = new ScriptObject(%name) {};
   %client.container.add(%script);

   //now apply the base settings for this new file.
   %client.TWM2Core.name = %client.namebase;
   %client.TWM2Core.xp = 0;
   %client.TWM2Core.millionxp = 0;
   %client.TWM2Core.rank = "Private";
   %client.TWM2Core.phrase = %savedPhrs;
   %client.TWM2Core.gameTime = %savedGameTime;
   %client.TWM2Core.officer = %next;
   //and save the new file
   //%scriptController.save(%file);
   SaveClientFile(%client);

   MessageAll('msgSpecial', "\c5"@%client.namebase@" has promoted to Officer level "@%next@".");
   recordAction(%client, "", ""); //record blank action for the challenges to pick off any officer challenges
   
   for(%i = %next; %i > 0; %i--) {
      %oChN = "Prestige"@%i;
	  CompleteNWChallenge(%client, %oChN);
   }
}

//STAT Cleaner:
//Phantom139: Changed in 3.4 to support the newer system
function DumpStats(%c) { 
   echo("Resetting" SPC %c.guid@"'s stats.");
   %sO = %c.TWM2Core;
   %sO.delete();
   %soNAME = "TWM2Client_"@%c.guid@"";
   %c.TWM2Core = new ScriptObject(%soNAME) {};
   %c.container.add(%c.TWM2Core);
   // this is now our cleaned object file, it will be populated shortly
}

//Direct calls to needed function, replaces
//old system.
function GainExperience(%client, %variable, %tagToGain) {
   %todaysDate = sha1sum(formattimestring("yymmdd"));
   %script = %client.TWM2Core;
   //
   %multi = $EXPMulti[$TWM2Core_Code, formattimestring("yymmdd"), sha1sum($TWM2Core_Code TAB TWM2Lib_MainControl("FormatTWM2Time", formattimestring("yymmdd")))];
   if(!isSet(%multi) || %multi < 1) {
      %multi = 1;
   }
   %variable *= %multi;
   %variable = mFloor(%variable);
   //
   if(%multi > 1) {
      messageClient(%client, 'msgClient', "\c5TWM2: "@%tagToGain@"\c3+"@%variable@" EXP (Bonus Multiplier: "@%multi@")");
   }
   else {
      messageClient(%client, 'msgClient', "\c5TWM2: "@%tagToGain@"\c3+"@%variable@" EXP");
   }
   $XPArray[%client] += %variable;
   updateClientRank(%client);
}

function WipeStats(%client) {
   %scriptController = %client.TWM2Core;
   %savedGameTime = %scriptController.gameTime;
   %savedPhrs = %scriptController.phrase;
   if(%scriptController.officer < 15) {
      error("Client "@%client@" is attempting to wipe stats??? Not max level...");
	  return;
   }

   DumpStats(%client);
   
   %file = ""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave";
   
   %name = "ClientSettings"@%client.guid@"";
   %check = nameToID(%name);
   if(isObject(%check)) {
      %check.delete(); //kill current settings, as they are no longer valid.
   }
   %script = new ScriptObject(%name) {};
   %client.container.add(%script);

   //now apply the base settings for this new file.
   %client.TWM2Core.name = %client.namebase;
   %client.TWM2Core.xp = 0;
   %client.TWM2Core.millionxp = 0;
   %client.TWM2Core.rank = "Private";
   %client.TWM2Core.phrase = %savedPhrs;
   %client.TWM2Core.gameTime = %savedGameTime;
   %client.TWM2Core.officer = 0;
   //and save the new file
   //%scriptController.save(%file);
   SaveClientFile(%client);

   MessageAll('msgAdminForce', "\c5"@%client.namebase@" has hit the reset button and is back at level 1!!!");
   recordAction(%client, "", ""); //record blank action for the challenges to pick off any officer challenges
}