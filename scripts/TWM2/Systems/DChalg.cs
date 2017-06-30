//Daily Challenge System
//Phantom139

//Handles PGD Issued daily challenges

//downloads challenges from PGD
function downloadChallenges() {
   if($TWM2::PGDConnectDisabled) {
      echo("PGD Connect is disabled.");
      return;
   }
   if(!IsServerMain()) {
      error("* Daily Challenges: Restricted To Core Servers Only");
      return;
   }
   echo("* Downloading Challenge List, scheduling next download");
   messageAll('MsgAdminForce', "\c3TWM2: Downloading challenges from PGD server.");
   %connection = new TCPObject(ChallengeDownload);
   %location = "/public/Univ/twm2/";
   %host = $PGDServer;
   //
   %request = "GET" SPC %location SPC "HTTP/1.1\x0aHost: "@%host@"\r\n\r\n";
   %connection.request = %request;

   $ChallengeIndex = 0;

   %connection.connect(%host @ ":80");
   %connection.schedule(5000, "disconnect");
   //
   schedule(1000 * 60 * 60, 0, "downloadChallenges");
}

function downloadChallenges_Manual() {
   if(!IsServerMain()) {
      error("* Daily Challenges: Restricted To Core Servers Only");
      return;
   }
   echo("* Downloading Challenge List, scheduling next download");
   messageAll('MsgAdminForce', "\c3TWM2: Downloading challenges from PGD server.");
   %connection = new TCPObject(ChallengeDownload);
   %location = "/public/Univ/twm2/";
   %host = $PGDServer;
   //
   %request = "GET" SPC %location SPC "HTTP/1.1\x0aHost: "@%host@"\r\n\r\n";
   %connection.request = %request;

   $ChallengeIndex = 0;

   %connection.connect(%host @ ":80");
   %connection.schedule(5000, "disconnect");
}

function ChallengeDownload::onConnected(%this) {
   %this.send(%this.request);
}

function ChallengeDownload::onConnectFailed( %this ) {
   error("Challenges: Connection failed");
}

function ChallengeDownload::onDisconnect(%this) {
   %this.delete();
}

function ChallengeDownload::onLine(%this, %line) {
   if (trim(%line) $= "") {       //is the line a HTTP header?
      if (!%this.readyToRead) {
         %this.readyToRead = true;
      }
   }
   if(!%this.readyToRead) {
      return; //we have no use for this.
   }
   //
   if (strstr(%line, "#TIME ") != -1) {
      //expire date line
      %line = strReplace(%line, "#TIME ", "");
      echo("* Time Line: "@getField(%line, 1)@" -> "@getField(%line, 0)@" / "@getField(%line, 3)@" -> "@getField(%line, 2)@"");
      $CurrentStartOfMonth = getField(%line, 1);
      $CurrentMonthlyChallengeExpire = getField(%line, 0);
      $CurrentWeeklyChallengeExpire = getField(%line, 2);    //aka: end of week
      $CurrentStartOfWeek = getField(%line, 3);
      return;
   }
   else if (strstr(%line, "#CHLG ") != -1) {
      //add the line
      %line = strReplace(%line, "#CHLG ", "");
      echo("* Add To Challenge Line: "@%line@"");
      AddToChallenges(%line);
      return;
   }
   else {

   }
   //
   if (%line $= "#EOF") {
      echo("*EOF Line, Disconnecting");
      %this.disconnect();
      return;
   }
}

//////////////////////////////////////////////////

function doChallengeKillRecording(%sourceObject, %targetObject) {
   if(%sourceObject.team == %targetObject.team) {
      //Teamkill = no recorded challenge :)
      return;
   }
   //killed a player?
   if(isClientControlledPlayer(%targetObject)) {
      //echo("Kill: "@%sourceObject.client@"/"@%targetObject.lastDamagedImage@"");
      recordAction(%sourceObject.client, "PKC", "single\t"@%targetObject.lastDamagedImage@"\t1");
      recordAction(%sourceObject.client, "PKC", "total\t1");
   }
   else {
      //killed a zombie
      %zType = %targetObject.type;
      if(!isSet(%zType)) {
         //illegal target for challenge set, end here
         return;
      }
      recordAction(%sourceObject.client, "ZKC", "total\t1");
      recordAction(%sourceObject.client, "ZKC", "single\t"@%zType@"\t1");
      recordAction(%sourceObject.client, "ZKC", "single\t"@%targetObject.lastDamagedImage@"\t"@%zType@"\t1");
   }
}

function recordAction(%client, %action, %variables) {
   %ymd = formattimestring("yymmdd");
   %so = %client.TWM2Controller;
   //echo(""@%client@" - "@%action@" - "@%variables@"");
   //
   switch$(%action) {
      case "CCMP":
         %so.completed[getField(%variables, 0), %ymd] = getField(%variables, 1);
      case "BOSS":
         %so.bossSlayCount[getField(%variables, 0), %ymd] += 1;
      case "PKC":
         //player kill count
         if(getField(%variables, 0) $= "total") {
            %so.totalPlayerKillCount[%ymd] += getField(%variables, 1);
         }
         else {
            %so.PlayerKillCount[%ymd, getField(%variables, 1)] += getField(%variables, 2);
         }
      case "ZKC":
         if(getField(%variables, 0) $= "total") {
            %so.totalZombieKillCount[%ymd] += getField(%variables, 1);
         }
         else {
            //a 3-case would have all of these filed
            if(!isSet(getField(%variables, 3))) {
               %so.ZombieKillCount[%ymd, 0, getField(%variables, 1)] += getField(%variables, 2);
            }
            else {
               %so.ZombieKillCount[%ymd, getField(%variables, 1), getField(%variables, 2)] += getField(%variables, 3);
            }
         }
      case "HSC":
         if(getField(%variables, 0) !$= "zombie") {
            %so.playerHeadshots[%ymd] += getField(%variables, 1);
         }
         else {
            %so.zombieHeadshots[%ymd] += getField(%variables, 1);
         }
      case "KSCC":
         %so.killstreakCalls[%ymd, getField(%variables, 0)] += getField(%variables, 1);
      case "KSKC":
         %so.killstreakKills[%ymd, getField(%variables, 0)] += getField(%variables, 1);
      case "SKC":
         %so.successiveSolo[%ymd, getField(%variables, 0)] += getField(%variables, 1);
      case "SKSC":
         %so.successiveStreak[%ymd, getField(%variables, 0)] += getField(%variables, 1);
      case "BOMBARM":
         %so.bombArm[%ymd, $CurrentMission] += 1;
         %so.bombArmTotal[%ymd] += 1;
      case "BOMBDIS":
         %so.bombDisarm[%ymd, $CurrentMission] += 1;
         %so.bombDisarmTotal[%ymd] += 1;
      case "SABWIN":
         %so.sabotageRoundWins[%ymd, $CurrentMission] += 1;
         %so.sabotageRoundWinTotal[%ymd] += 1;
      case "AREACAP":
         %so.areaCapture[%ymd, $CurrentMission] += 1;
         %so.areaCaptureTotal[%ymd] += 1;
      case "DOMWIN":
         %so.dominationRoundWins[%ymd, $CurrentMission] += 1;
         %so.dominationRoundWinTotal[%ymd] += 1;
	  case "EXPGAIN":
	     %so.dailyEXPGain[%ymd] += getField(%variables, 0);
	  case "BACK":
	     if(getField(%variables, 0) $= "zombie") {
		    %so.zombieBackstabs[%ymd] += 1;
		 }
		 else {
		    %so.playerBackstabs[%ymd] += 1;
		 }
      default:
         //no action recorded
   }
   allCheckCompletion(%client);
   updateChallengeFile(%client);
}

function cleanChallenges() {
   %i = 1;
   while(isSet($Challenges::Challenge[%i])) {
      $Challenges::Challenge[%i] = "";
      %i++;
   }
}

function AddToChallenges(%line) {
   //data prints like so:
   //Type \t Name \t Descrip \t Condition \t Reward \t Expry
   //%cType = getField(%line, 0);
   //%cName = getField(%line, 1);
   //%cDesc = getField(%line, 2);
   //%cCond = getField(%line, 3);
   //%CRewd = getField(%line, 4);
   //%CExpy = getField(%line, 5);
   //
   $ChallengeIndex++;
   $Challenges::Challenge[$ChallengeIndex] = %line;
}

function allCheckCompletion(%client) {
   for(%i = 1; isSet($Challenges::Challenge[%i]); %i++) {
      %cType = getField($Challenges::Challenge[%i], 0);
      if(%cType == 1) {
         checkCompletion(%client, %i);      //daily
      }
	  else {
         checkMultiCompletion(%client, %i);
	  }
   }
   //
}

function checkCompletion(%client, %cID) {
   %challenge = $Challenges::Challenge[%cID];
   %cType = trim(getField(%challenge, 0));
   %cCond = getsubstr(getField(%challenge, 3), 1, strlen(getField(%challenge, 3)));
   %so = %client.TWM2Controller;
   %dateStr = formattimestring("yymmdd");
   //cannot complete the same one twice :P
   if(%so.completed[%cid, %dateStr]) {
      return;
   }
   //
   switch$(getWord(%cCond, 0)) {
      case "E":
         %killCount = getWord(%cCond, 1);
         %killDB = getWord(%cCond, 2) $= "A" ? 0 : getWord(%cCond, 2);
         if(%killDB != 0) {
            if(%so.PlayerKillCount[%dateStr, %killDB] >= %killCount) {
               %done = true;
            }
         }
         else {
            if(%so.totalPlayerKillCount[%dateStr] >= %killCount) {
               %done = true;
            }
         }
      case "Z":
         %killCount = getWord(%cCond, 1);
         %killedType = getWord(%cCond, 2) $= "A" ? -1 : getWord(%cCond, 2);
         %killDB = getWord(%cCond, 3) $= "A" ? 0 : getWord(%cCond, 3);
         if(%killDB != 0) {
            if(%killedType != -1) {
               if(%so.ZombieKillCount[%dateStr, %killDB, %killedType] >= %killCount) {
                  %done = true;
               }
            }
            else {
               if(%so.ZombieKillCount[%dateStr, %killDB, ""] >= %killCount) {
                  %done = true;
               }
            }
         }
         else {
            if(%killedType != -1) {
               if(%so.ZombieKillCount[%dateStr, 0, %killedType] >= %killCount) {
                  %done = true;
               }
            }
            else {
               if(%so.totalZombieKillCount[%dateStr] >= %killCount) {
                  %done = true;
               }
            }
         }
      case "HS":
         %counter = getWord(%cCond, 1);
         %type = getWord(%cCond, 2) $= "E" ? "E" : "Z";
         if(%type $= "E") {
            if(%so.playerHeadshots[%dateStr] >= %counter) {
               %done = true;
            }
         }
         else {
            if(%so.zombieHeadshots[%dateStr] >= %counter) {
               %done = true;
            }
         }
      case "KS":
         %type = getWord(%cCond, 1);
         %ammount = getWord(%cCond, 2);
         if(%so.killstreakCalls[%dateStr, %type] >= %ammount) {
            %done = true;
         }
      case "KSK":
         %type = getWord(%cCond, 1);
         %ammount = getWord(%cCond, 2);
         if(%so.killstreakKills[%dateStr, %type] >= %ammount) {
            %done = true;
         }
      case "SK":
         %soloType = getWord(%cCond, 1);
         %ammount = getWord(%cCond, 2);
         if(%so.successiveSolo[%dateStr, %soloType] >= %ammount) {
            %done = true;
         }
      case "SKS":
         %streakType = getWord(%cCond, 1);
         %ammount = getWord(%cCond, 2);
         if(%so.successiveStreak[%dateStr, %streakType] >= %ammount) {
            %done = true;
         }
      case "Prestige":
	     %level = getWord(%cCond, 1);
		 if(%client.TWM2Core.officer >= %level) {
		    %done = true;
		 }
      case "Boss":
         %name = getWord(%cCond, 1);
         %amount = getWord(%cCond, 2);
         if(%so.bossSlayCount[%name, %dateStr] >= %amount) {
            %done = true;
         }
      case "Back":
	     %zOrA = getWord(%cCond, 1);
		 %amount = getWord(%cCond, 2);
		 if(%zOrA $= "Z") {
		    if(%so.zombieBackstabs[%dateStr] >= %amount) {
			   %done = true;
			}
		 }
		 else {
	 	    if(%so.playerBackstabs[%dateStr] >= %amount) {
			   %done = true;
			}
	 	 }
      default:
         error("Unknown challenge in parser...");
   }
   if(%done) {
      %cName = getField(%challenge, 1);
      %CRewd = getField(%challenge, 4);
      CenterPrint(%client, "CHALLENGE COMPLETED\n"@%cName@"", 3, 3);
      GainExperience(%client, %cRewd, %cName@" Challenge Completed");
      recordAction(%client, "CCMP", ""@%cid@"\t1");
   }
}

//Check Multi-Completion (handles weekly & monthly challenges)
function checkMultiCompletion(%client, %cID) {
   %challenge = $Challenges::Challenge[%cID];
   %cType = trim(getField(%challenge, 0));
   %cCond = getsubstr(getField(%challenge, 3), 1, strlen(getField(%challenge, 3)));
   %so = %client.TWM2Controller;
   %dateStr = formattimestring("yymmdd");
   //cannot complete the same one twice :P
   if(%so.completed[%cid, %dateStr]) {
      return;
   }
   //
   switch$(getWord(%cCond, 0)) {
      case "E":
         %killCount = getWord(%cCond, 1);
         %killDB = getWord(%cCond, 2) $= "A" ? 0 : getWord(%cCond, 2);
         if(%killDB != 0) {
		    if(%cType == 2) {
		       if(getCurrentWeekTotal(%client, PlayerKillCount, %killDB) >= %killCount) {
			      %done = true;
			   }
		    }
		    else {
		       if(getCurrentMonthTotal(%client, PlayerKillCount, %killDB) >= %killCount) {
			      %done = true;
			   }		 
		    }			
         }
         else {
		    if(%cType == 2) {
		       if(getCurrentWeekTotal(%client, totalPlayerKillCount) >= %killCount) {
			      %done = true;
			   }
		    }
		    else {
		       if(getCurrentMonthTotal(%client, totalPlayerKillCount) >= %killCount) {
			      %done = true;
			   }		 
		    }			
         }
      case "Z":
         %killCount = getWord(%cCond, 1);
         %killedType = getWord(%cCond, 2) $= "A" ? -1 : getWord(%cCond, 2);
         %killDB = getWord(%cCond, 3) $= "A" ? 0 : getWord(%cCond, 3);
         if(%killDB != 0) {
            if(%killedType != -1) {
		       if(%cType == 2) {
		          if(getCurrentWeekTotal(%client, ZombieKillCount, %killDB SPC %killedType) >= %killCount) {
			         %done = true;
			      }
		       }
		       else {
		          if(getCurrentMonthTotal(%client, ZombieKillCount, %killDB SPC %killedType) >= %killCount) {
			         %done = true;
			      }		 
		       }			
            }
            else {
		       if(%cType == 2) {
		          if(getCurrentWeekTotal(%client, ZombieKillCount, %killDB SPC "") >= %killCount) {
			         %done = true;
			      }
		       }
		       else {
		          if(getCurrentMonthTotal(%client, ZombieKillCount, %killDB SPC "") >= %killCount) {
			         %done = true;
			      }		 
		       }					
            }
         }
         else {
            if(%killedType != -1) {
		       if(%cType == 2) {
		          if(getCurrentWeekTotal(%client, ZombieKillCount, 0 SPC %killedType) >= %killCount) {
			         %done = true;
			      }
		       }
		       else {
		          if(getCurrentMonthTotal(%client, ZombieKillCount, 0 SPC %killedType) >= %killCount) {
			         %done = true;
			      }		 
		       }					
            }
            else {
		       if(%cType == 2) {
		          if(getCurrentWeekTotal(%client, totalZombieKillCount) >= %killCount) {
			         %done = true;
			      }
		       }
		       else {
		          if(getCurrentMonthTotal(%client, totalZombieKillCount) >= %killCount) {
			         %done = true;
			      }		 
		       }	
            }
         }
      case "HS":
         %counter = getWord(%cCond, 1);
         %type = getWord(%cCond, 2) $= "E" ? "E" : "Z";
         if(%type $= "E") {
		    if(%cType == 2) {
		       if(getCurrentWeekTotal(%client, playerHeadshots) >= %counter) {
			      %done = true;
			   }
		    }
		    else {
		       if(getCurrentMonthTotal(%client, playerHeadshots) >= %counter) {
			      %done = true;
			   }		 
		    }	
         }
         else {
		    if(%cType == 2) {
		       if(getCurrentWeekTotal(%client, zombieHeadshots) >= %counter) {
			      %done = true;
			   }
		    }
		    else {
		       if(getCurrentMonthTotal(%client, zombieHeadshots) >= %counter) {
			      %done = true;
			   }		 
		    }	
         }
      case "KS":
         %type = getWord(%cCond, 1);
         %ammount = getWord(%cCond, 2);
		 if(%cType == 2) {
		    if(getCurrentWeekTotal(%client, killstreakCalls, %type) >= %ammount) {
			   %done = true;
			}
		 }
		 else {
		    if(getCurrentMonthTotal(%client, killstreakCalls, %type) >= %ammount) {
			   %done = true;
			}		 
		 }				
      case "KSK":
         %type = getWord(%cCond, 1);
         %ammount = getWord(%cCond, 2);
		 if(%cType == 2) {
		    if(getCurrentWeekTotal(%client, killstreakKills, %type) >= %ammount) {
			   %done = true;
			}
		 }
		 else {
		    if(getCurrentMonthTotal(%client, killstreakKills, %type) >= %ammount) {
			   %done = true;
			}		 
		 }		
      case "SK":
         %soloType = getWord(%cCond, 1);
         %ammount = getWord(%cCond, 2);
		 if(%cType == 2) {
		    if(getCurrentWeekTotal(%client, successiveSolo, %soloType) >= %ammount) {
			   %done = true;
			}
		 }
		 else {
		    if(getCurrentMonthTotal(%client, successiveSolo, %soloType) >= %ammount) {
			   %done = true;
			}		 
		 }
      case "SKS":
         %streakType = getWord(%cCond, 1);
         %ammount = getWord(%cCond, 2);
		 if(%cType == 2) {
		    if(getCurrentWeekTotal(%client, successiveStreak, %streakType) >= %ammount) {
			   %done = true;
			}
		 }
		 else {
		    if(getCurrentMonthTotal(%client, successiveStreak, %streakType) >= %ammount) {
			   %done = true;
			}		 
		 }		
	  //easy enough :)
      case "Prestige":
	     %level = getWord(%cCond, 1);
		 if(%client.TWM2Core.officer >= %level) {
		    %done = true;
		 }
      case "Boss":
         %name = getWord(%cCond, 1);
         %amount = getWord(%cCond, 2);
         if(%cType == 2) {
            if(getCurrentWeekTotal(%client, bossSlayCount, %name) >= %amount) {
               %done = true;
            }
         }
         else {
            if(getCurrentMonthTotal(%client, bossSlayCount, %name) >= %amount) {
               %done = true;
            }
         }
      case "Back":
	     %zOrA = getWord(%cCond, 1);
		 %amount = getWord(%cCond, 2);
		 if(%zOrA $= "Z") {
			if(%cType == 2) {
		       if(getCurrentWeekTotal(%client, zombieBackstabs) >= %amount) {
			      %done = true;
			   }
	        }
			else {
		       if(getCurrentMonthTotal(%client, zombieBackstabs) >= %amount) {
			      %done = true;
			   }		    
			}
		 }
		 else {
			if(%cType == 2) {
		       if(getCurrentWeekTotal(%client, playerBackstabs) >= %amount) {
			      %done = true;
			   }
	        }
			else {
		       if(getCurrentMonthTotal(%client, playerBackstabs) >= %amount) {
			      %done = true;
			   }		    
			}
	 	 }
      default:
         error("Unknown challenge in parser...");
   }
   if(%done) {
      %cName = getField(%challenge, 1);
      %CRewd = getField(%challenge, 4);
      CenterPrint(%client, ""@(%cType == 2 ? "WEEKLY" : "MONTHLY")@" CHALLENGE COMPLETED\n"@%cName@"", 3, 3);
      GainExperience(%client, %cRewd, %cName@" Challenge Completed");
      recordAction(%client, "CCMP", ""@%cid@"\t1");
   }
}

//keep a running weekly total
function getCurrentWeekTotal(%client, %field, %additional_variables) {
   if(!isSet(%client) || !isSet(%field)) {
      return 0;
   }
   %scriptObj = %client.TWM2Controller;
   if(isSet(%additional_variables)) {
      %var_count = getWordCount(%additional_variables);
	  %vStr = "";
	  %integer = 0;
	  while(isSet(getWord(%additional_variables, %integer))) {
	     %vStr = %vStr @","@ getWord(%additional_variables, %integer);
		 %integer++;
	  }
   }
   //get the start and end day numbers of the week.
   %sOW = getSubStr($CurrentStartOfWeek, 6, 2);
   %eOW = %sOW + 7;  //7 day total.
   //if we cross over months, we don't care, because the file is killed at month's end.
   %cTotal = 0;
   for(%i = %sOW; %i < %eOW; %i++) {
      %iStor = %i;
      if(%i < 10) {
         %iStor = "0"@%i;
      }
      %day = %year@%month@%iStor;
	  if(isSet(%vStr)) {
	     eval("%cTotal += isSet("@%scriptObj@"."@%field@"["@%day SPC %vStr@"]) ? ("@%scriptObj@"."@%field@"["@%day SPC %vStr@"]) : 0;");
      }
	  else {
	     eval("%cTotal += isSet("@%scriptObj@"."@%field@"["@%day@"]) ? ("@%scriptObj@"."@%field@"["@%day@"]) : 0;");
      }
   }
   return %cTotal;
}

//keep a running monthly total
function getCurrentMonthTotal(%client, %field, %additional_variables) {
   if(!isSet(%client) || !isSet(%field)) {
      return 0;
   }
   %scriptObj = %client.TWM2Controller;
   if(isSet(%additional_variables)) {
      %var_count = getWordCount(%additional_variables);
	  %vStr = "";
	  %integer = 0;
	  while(isSet(getWord(%additional_variables, %integer))) {
	     %vStr = %vStr @","@ getWord(%additional_variables, %integer);
		 %integer++;
	  }
   }
   //
   %year = getSubStr($CurrentStartOfMonth, 0, 4);
   %month = getSubStr($CurrentStartOfMonth, 4, 2);
   //
   %sOM = getSubStr($CurrentStartOfMonth, 6, 2);
   %eOM = getSubStr($CurrentMonthlyChallengeExpire, 6, 2);
   %cTotal = 0; //hold the counter at 0
   for(%i = %sOM; %i <= %eOM; %i++) {
      %iStor = %i;
      if(%i < 10) {
         %iStor = "0"@%i;
      }
      %day = %year@%month@%iStor;
	  if(isSet(%vStr)) {
	     eval("%cTotal += isSet("@%scriptObj@"."@%field@"["@%day SPC %vStr@"]) ? ("@%scriptObj@"."@%field@"["@%day SPC %vStr@"]) : 0;");
      }
	  else {
	     eval("%cTotal += isSet("@%scriptObj@"."@%field@"["@%day@"]) ? ("@%scriptObj@"."@%field@"["@%day@"]) : 0;");
      }
   }
   //
   return %cTotal;
}


function loadChallengeData(%client) {
   //Daily Challenges = Core Servers Only
   if(!IsServerMain()) {
      error("* Daily Challenges: Restricted To Core Servers Only");
      return;
   }
   //
   %object = NameToID("Container_"@%client.guid@"/CCD_"@%client.guid);
   if(!isObject(%object)) {
      %name = "CCD_"@%client.guid@"";
      %client.TWM2Controller = new ScriptObject(%name) {};
      %client.container.add(%client.TWM2Controller);
   }
   else {
      %client.TWM2Controller = %object;
   }
   //
   updateChallengeFile(%client);
}

//file update
function updateChallengeFile(%client) {
   if(!isSet(%client) || !ClientGroup.isMember(%client)) {
      return;
   }
   if(%client.cannotReset) {
      return;
   }
   //if the expry date is smaller than the current date, clear the current file for write
   %so = %client.TWM2Controller;
   //%file = ""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave";
   //exec(%file);
   //
   %cDATE = formattimestring("yymmdd");
   if(%so.expireDate <= 0) {
      if(isObject(%so)) {
         %so.delete();
      }
      %client.TWM2Controller = new ScriptObject("CCD_"@%client.guid@"") {};
      %client.container.add(%client.TWM2Controller);
      //
      if(!isSet($CurrentMonthlyChallengeExpire)) {
         %setTo = %cDATE + 31;  //temp, will auto adjust at the end of the month.
      }
      else {
         %setTo = $CurrentMonthlyChallengeExpire;
      }
      %client.TWM2Controller.expireDate = %setTo;
      SaveClientFile(%client); //give em a save
   }
   //delete if expired and ONLY if the expire is present.
   if(%cDATE > %client.TWM2Controller.expireDate) {
      %client.TWM2Controller.expireDate = -1;
      //
      echo("Daily Challenge File Expired for "@%client@", preparing new file.");
      //
      %client.resetAttemps++;
      if(%client.resetAttemps >= 3) {
         error("Client "@%client@" reset attempts > 3, disabling challenge interpreter");
         %client.cannotReset = 1;
         return;
      }
      schedule(1500, 0, "updateChallengeFile", %client);
      return;
   }
}

function GenerateDWMChallengeMenu(%client, %tag, %index) {
   %dateStr = formattimestring("yymmdd");
   messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>Green Indicates A Completed Challenge");
   %index++;   
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Uncolored Indicates An Active Challenge");
   %index++;  
   messageClient( %client, 'SetLineHud', "", %tag, %index, "");
   %index++;   
   messageClient( %client, 'SetLineHud', "", %tag, %index, "PGD Daily Challenge News:");
   %index++;   
   messageClient( %client, 'SetLineHud', "", %tag, %index, "6/29/17: PGD Challenges reacitvated, now running 5x daily");
   %index++;   
   messageClient( %client, 'SetLineHud', "", %tag, %index, "4/20/16: New Daily Challenge System is Live!");
   %index++; 
   messageClient( %client, 'SetLineHud', "", %tag, %index, "==========================");
   %index++;   
   for(%i = 1; isSet($Challenges::Challenge[%i]); %i++) {
      %challenge = $Challenges::Challenge[%i];
      %cType = getField(%challenge, 0);
      %cName = getField(%challenge, 1);
      %cDesc = getField(%challenge, 2);
      %CRewd = getField(%challenge, 4);
      //
      if(%cType == 1) {
         if(%client.TWM2Controller.completed[%i, %dateStr]) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>*DAILY* "@%cName@" - Completed");
            %index+=2;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "=*DAILY* "@%cName@" - "@%cDesc@" *"@%CRewd@"EXP");
            %index+=2;
         }
      }
      //
      else if(%cType == 2) {
         if(%client.TWM2Controller.completed[%i, %dateStr]) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>*WEEKLY* "@%cName@" - Completed");
            %index+=2;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "*WEEKLY* "@%cName@" - "@%cDesc@" *"@%CRewd@"EXP");
            %index+=2;
         }
      }
      else {
         if(%client.TWM2Controller.completed[%i, %dateStr]) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<color:33FF00>*MONTHLY* "@%cName@" - Completed");
            %index+=2;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "*MONTHLY* "@%cName@" - "@%cDesc@" *"@%CRewd@"EXP");
            %index+=2;
         }
      }
   }
   return %index;
}


//Challenge List Conditions Generic

//E - Player Kills
// # Players
//  Killed With Gun Datablock or A any gun
//Z - Zombie Kills
// # Zombies
//  # Type or A any
//   Killed With Gun Datablock or A any gun
//HS - Headshots
// # Headshots
//  E Players or Z Zombies
//KS - Killstreak Usage
// # Killstreak ID
//  # Ammount Used
//KSK - Killstreak Kills
// # Killstreak ID
//  # Kills
//SK - Successive Solo Kills        * like double kill, tripple kill, ect.
// # Kills in a row
//  # Times
//SKS - Successive Kill Streaks
// # Kills in a row
//  # Times

//SPECIAL CASES

//Prestige - Prestige
// # Prestige Promote To
//Back - Backstab
// E Player or Z Zombie
//  # of backstabs
