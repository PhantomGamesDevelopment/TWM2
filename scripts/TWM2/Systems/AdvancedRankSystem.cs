$TWM::RanksDirectory = "Server/TWM/Saved";
// This is where all of the ranks will be saved
// I highly recommend leaving this alone

function LoadRanksBase() {
   echo("Loading The Ranking System Base");
   findTopRanks();
   //Modified 2.6, top ranks handled by PGD now
}

function CreateClientRankFile(%client) {
   if(!isSet(%client)) {
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
   %scriptController.money = 0;
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
   //
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
    //anti-Hack system.
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
      //break the function run through here
   }
   if(%j <= 0) {
      return;
   }
   %name = %client.namebase;
   %scriptController = %client.TWM2Core;
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
         //UpdateRankFile(%client);
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
    %nonMilXP = %scriptControler.xp;
    return %milXP @ "" @ %nonMilXP;
}

//PRESTIGE RANKS
function PromoteToPrestige(%client) {
   %scriptController = %client.TWM2Core;
   %savedGameTime = %scriptController.gameTime;
   %savedPhrs = %scriptController.phrase;
   %savedMoney = %scriptController.money;
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
   %client.TWM2Core.money = %savedMoney;
   %client.TWM2Core.rank = "Private";
   %client.TWM2Core.phrase = %savedPhrs;
   %client.TWM2Core.gameTime = %savedGameTime;
   %client.TWM2Core.officer = %next;
   //and save the new file
   //%scriptController.save(%file);
   SaveClientFile(%client);

   MessageAll('msgSpecial', "\c5"@%client.namebase@" has promoted to Officer level "@%next@".");
   recordAction(%client, "", ""); //record blank action for the challenges to pick off any officer challenges

   switch(%next) {
      case 1:
         schedule(1000, 0, "CompleteNWChallenge", %client, "Prestige1");
      case 2:
         schedule(1000, 0, "CompleteNWChallenge", %client, "Prestige1");
         schedule(1500, 0, "CompleteNWChallenge", %client, "Prestige2");
      case 3:
         schedule(1000, 0, "CompleteNWChallenge", %client, "Prestige1");
         schedule(1500, 0, "CompleteNWChallenge", %client, "Prestige2");
         schedule(2000, 0, "CompleteNWChallenge", %client, "Prestige3");
      case 4:
         schedule(1000, 0, "CompleteNWChallenge", %client, "Prestige1");
         schedule(1500, 0, "CompleteNWChallenge", %client, "Prestige2");
         schedule(2000, 0, "CompleteNWChallenge", %client, "Prestige3");
         schedule(2500, 0, "CompleteNWChallenge", %client, "Prestige4");
      case 5 or 6 or 7 or 8:
         schedule(1000, 0, "CompleteNWChallenge", %client, "Prestige1");
         schedule(1500, 0, "CompleteNWChallenge", %client, "Prestige2");
         schedule(2000, 0, "CompleteNWChallenge", %client, "Prestige3");
         schedule(2500, 0, "CompleteNWChallenge", %client, "Prestige4");
         schedule(3000, 0, "CompleteNWChallenge", %client, "Prestige5");
      case 9:
         schedule(1000, 0, "CompleteNWChallenge", %client, "Prestige1");
         schedule(1500, 0, "CompleteNWChallenge", %client, "Prestige2");
         schedule(2000, 0, "CompleteNWChallenge", %client, "Prestige3");
         schedule(2500, 0, "CompleteNWChallenge", %client, "Prestige4");
         schedule(3000, 0, "CompleteNWChallenge", %client, "Prestige5");
         schedule(3500, 0, "CompleteNWChallenge", %client, "Prestige9");
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

function GeneratePrestigeChallengeMenu(%client, %tag, %index) {
   if(%client.CheckNWChallengeCompletion("Prestge1")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Instructive Private - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Instructive Private - Reach Officer Level 1.");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Prestge2")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Excelling Private - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Excelling Private - Reach Officer Level 2.");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Prestge3")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Champion Private - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Champion Private - Reach Officer Level 3.");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Prestge4")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Prestigious Private - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Prestigious Private - Reach Officer Level 4.");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Prestge5")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Supreme Private - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Supreme Private - Reach Officer Level 5.");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Prestge9")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Phantom's Vengeance - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Phantom's Vengeance - Reach Oficer Level 9.");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("GameEnder")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Game Ender - Done.");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Game Ender - Call in a Fission Bomb.");
      %index++;
   }
   //
   return %index; 
}

function EXPWillBreakRankCap(%client) {
   %script = %client.TWM2Core;
   %rN = %script.rankNumber;
   %officer = %script.officer;
   //Phantom139: updated here, now handles officer ranks so we can apply numbers above 61 to restrict up to a officer level
   %currentRankNumber = (%officer*$Rank::RankCount) + %rN;
 	  //apply the new check here                                   |LEAVE THIS, apply ONLY on base rank|
   if(fetchCap("Level", %currentRankNumber + 1) && (getCurrentEXP(%client) >= $Ranks::MinPoints[%rN+1])) {
      return true;
   }
   else {
      return false;
   }
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
   %script.money += %variable; //money is kept no matter what
   //
   if(EXPWillBreakRankCap(%client)) {
      messageClient(%client, 'msgClient', "\c5TWM2: "@%tagToGain@"\c3 Further Progression Locked [RANK CAP]");
      return;
   }
   if(%multi > 1) {
      messageClient(%client, 'msgClient', "\c5TWM2: "@%tagToGain@"\c3+"@%variable@" EXP (X"@%multi@")");
   }
   else {
      messageClient(%client, 'msgClient', "\c5TWM2: "@%tagToGain@"\c3+"@%variable@" EXP");
   }
   $XPArray[%client] += %variable;
   updateClientRank(%client);
}
