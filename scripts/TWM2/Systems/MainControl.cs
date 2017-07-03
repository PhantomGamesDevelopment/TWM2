//TWM2 Functions
$TWM2::Version = 3.91;
$TWM2::ModVersionString = "3.91 {Dev}";

function TWM2Lib_MainControl(%functionName, %arguments) {
    switch$(strlwr(%functionName)) {
        case "clientconnectionfunction":
            %client = %arguments;
            $XPArray[%client] = 0;
            %client.CheckPGDConnect();  // <-- Used for Universal features
            TWM2Lib_PGDConnect_Support("performFileCheck", "Data/"@%client.guid@"/Ranks/TWM2/Saved.TWMSave");
            schedule(5000, 0, "LoadUniversalRank", %client);

            setDefaultInventory(%client);
            TWM2Lib_MainControl("CheckGUID", %client);

            if ($TWM2::UseRankTags) {
                schedule(15000, 0, "DoNameChangeChecks", %client);
            }
            %client.canSaveRank = 1;
            %client.canLoadRank = 1;
            //set the default killstreaks (1, 2, and 4)
            %client.KillstreakOn[1] = 1;
            %client.KillstreakOn[2] = 1;
            %client.KillstreakOn[4] = 1;
            return;

        case "clientdropfunction_preclientkill":
            %client = %arguments;
            SaveClientFile(%client);
            PrepareUpload(%client);   //universally upload it (if we can)
            LogConnection(%client, 4);
            MessageAll('MsgAllPlayers', "\c4"@$ChatBot::Name@": Removing Orphaned Deployables in "@MFloor($TWM2::RemoveOrphansTime / 60)@" Minutes");
            schedule(1000, 0, "TWM2Lib_MainControl", "RemoveOrphansLoop", 1);
            return;

        case "clientdropfunction_postclientkill":
            if ($HostGamePlayerCount == 0 && $TWM2::CloseWhenDone) {
                quit();
            }
            // reset the server if everyone has left the game
            if ($TWM2::RestartOnEmpty) {
                if ( $HostGamePlayerCount - $HostGameBotCount == 0 && $Host::Dedicated && !$resettingServer && !$LoadingMission ) {
                    schedule(0, 0, "resetServerDefaults");
                }
            }
            return;

        case "removeorphansloop":
            %tick = %arguments;
            if (%tick > $TWM2::RemoveOrphansTime) {
                MessageAll('MsgCyn', "\c4"@$ChatBot::Name@": Removing Orphaned Deployables Now.");
                if (isObject(Game)) {
                    delOrphanedPieces(true);
                    Game.removeDepTime = getSimTime() + delOrphanedPieces(true) + 1000;
                }
                return;
            }
            %tick++;
            schedule(1000, 0, "TWM2Lib_MainControl", "removeOrphansLoop", %tick);
            return;

        case "formattwm2time":
            %time = %arguments;
            %min = mFloor(%time / 60);
            %sec = %time % 60;
            if(%sec < 10) {
                %sec = "0" @ %sec;
            }
            return %min TAB %sec;

        case "checkguid":
            %client = %arguments;
            if (%client.isSuperAdmin) {
                %tag = "[SA]";
            }
            else if (%client.isAdmin && !%client.isSuperAdmin) {
                %tag = "[Admin]";
            }
            //
            if (%client.GUID $= $TWM2::HostGUID) {
                %client.isadmin = 1;
                %client.issuperadmin = 1;
                %client.ishost = 1; //hosts can use developer commands, but don't have access to developer features
                echo("Server Host Joined.");
                messageall('MsgAdminForce', "\c3The Host Has Joined!");
                %tag = "[Host]";
            }
            else {
                if ($Host::UseDevelopersList) {
                    //get the developer list 
                    %i_check = 0;
                    while (isSet($DeveloperList[%i_check])) {
                        if (%client.guid $= trim($DeveloperList[%i_check])) {
                            switch$(trim($DeveloperLevel[%i_check])) {
                                case "Dev":
                                    %client.isAdmin = 1;
                                    %client.isSuperAdmin = 1;
                                    %client.isDev = 1;
                                    %client.isPhantom = 1;
                                    %tag = "[Dev]";
                                    echo("Mod Developer Connection");
                                case "CoDev":
                                    %client.isAdmin = 1;
                                    %client.isSuperAdmin = 1;
                                    %client.isDev = 1;
                                    %tag = "[CoDev]";
                                    echo("Mod Co-Developer Connection");
                                default:
                                    echo("wut? o_o: "@$DeveloperLevel[%i_check]@"");
                            }
                            break; //break loop, proceed
                        }
                        %i_check++;
                    }
                }
            }
            if (%tag !$= "" && !$TWM2::UseRankTags) {
                %name = "\cp\c7" @ %tag @ "\c6" @ %client.namebase @ "\co";
                MessageAll('MsgClientNameChanged', "", %client.name, %name, %client);
                removeTaggedString(%client.name);
                %client.name = addTaggedString(%name);
                setTargetName(%client.target, %client.name);
            }
            return;

        case "playertimeloop":
            %client = %arguments;
            %scriptController = %client.TWM2Core;
            %scriptController.gameTime++;
            if (%scriptController.gameTime >= 1440) {
                AwardClient(%client, "6");
            }
            schedule(60000, 0, "TWM2Lib_MainControl", "PlayerTimeLoop", %client);
            return;

        case "playtwm2intro":
            %client = %arguments;
            BottomPrint(%client, "T", 1, 3);
            schedule(250, 0, "BottomPrint", %client, "TO", 1, 3);
            schedule(500, 0, "BottomPrint", %client, "TOT", 1, 3);
            schedule(750, 0, "BottomPrint", %client, "TOTA", 1, 3);
            schedule(1000, 0, "BottomPrint", %client, "TOTAL", 1, 3);

            schedule(1750, 0, "BottomPrint", %client, "TOTAL W", 1, 3);
            schedule(2000, 0, "BottomPrint", %client, "TOTAL WA", 1, 3);
            schedule(2250, 0, "BottomPrint", %client, "TOTAL WAR", 1, 3);
            schedule(2500, 0, "BottomPrint", %client, "TOTAL WARF", 1, 3);
            schedule(2750, 0, "BottomPrint", %client, "TOTAL WARFA", 1, 3);
            schedule(3000, 0, "BottomPrint", %client, "TOTAL WARFAR", 1, 3);
            schedule(3250, 0, "BottomPrint", %client, "TOTAL WARFARE", 1, 3);

            schedule(4000, 0, "BottomPrint", %client, "TOTAL WARFARE M", 1, 3);
            schedule(4250, 0, "BottomPrint", %client, "TOTAL WARFARE MO", 1, 3);
            schedule(4500, 0, "BottomPrint", %client, "TOTAL WARFARE MOD", 2, 3);

            schedule(6000, 0, "BottomPrint", %client, "TOTAL WARFARE MOD 2", 1, 3);
            schedule(6700, 0, "BottomPrint", %client, "TOTAL WARFARE MOD 2 \n Advanced", 1, 3);
            schedule(7500, 0, "BottomPrint", %client, "TOTAL WARFARE MOD 2 \n Advanced Warfare", 5, 3);

            if ($Host::ServerPopup !$= "") {
                schedule(500, 0, "CenterPrint", %client, $Host::ServerPopup, 7, 3);
            }

            if ($TWM2::MOTDGlobal !$= "") {
                schedule(7500, 0, "CenterPrint", %client, "<Color:FFFFFF><Font:Arial:24>PGD MOTD: "@$TWM2::MOTDGlobal, 7, 3);
            }
            return;

        case "getrandomposition":
            %mult = getField(%arguments, 0);
            %noZ = getField(%arguments, 1);

            %x = getRandom() * %mult;
            %y = getRandom() * %mult;
            %z = getRandom() * %mult;
            %negX = 1;
            %negY = 1;
            %negZ = 1;
            if (%noZ) {
                %z = 0;
            }
            if (getRandom(0, 1) == 1) {
                %negX = -1;
            }
            if (getRandom(0, 1) == 1) {
                %negY = -1;
            }
            if (getRandom(0, 1) == 1) {
                %negZ = -1;
            }
            %rand = %negX * %x SPC %negY * %y SPC %negZ * %z;
            return %rand;

        case "rmpg":
            %X = getWord(MissionArea.getArea(), 0);
            %Y = getWord(MissionArea.getArea(), 1);
            %W = getWord(MissionArea.getArea(), 2);
            %H = getWord(MissionArea.getArea(), 3);

            %OppX = ((%X) + (%W));
            %OppY = ((%Y) + (%H));
            %Position = getRandom(%X, %OppX) SPC getRandom(%Y, %OppY) SPC 0;

            %Z = getTerrainHeight(%Position);
            %PositionF = getWord(%Position, 0) SPC getWord(%Position, 1) SPC %Z;

            return %PositionF;
			
		case "getstrtrailingnumber":
			%str = trim(StrReplace(%arguments, " ", ""));
			%len = strLen(%str);
			%trailingNum = "";
			for(%i = %len-1; %i >= 0; %i--) {
				%ch = strcmp(getSubStr(%str, %i, 1), "");
				if(%ch > 48 && %ch < 57) {
					//Number, remember, we're reading backwards, so write backwards.
					%trailingNum = getSubStr(%str, %i, 1) @ %trailingNum;
				}
				else {
					break;
				}
			}
			return %trailingNum;

        default:
            error("TWM2Lib_MainControl(): Error, unknown function "@%functionName@" sent to command.");
    }
}

function ReMoveClientSW(%client) {
   if(!isObject(%client.player) || %client.player.getState() $= "dead") {
      return;
   }
   else {
      %sp = Game.pickPlayerSpawn(%client, false);
      //2 sec Invincibility please?
      %client.player.setInvinc(1);
      %client.player.schedule(2000, "setInvinc", 0);
      %client.player.setTransform(%client.player.lastTransformStuff);  //%sp for new spawn
      %client.setControlObject(%client.player);
   }
}

function ListGUIDS() {
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      echo(""@%cl.namebase@" - GUID: "@%cl.GUID@"");
   }
}

//MODIFED 8-14-09
//Adding [W] will do a wav sound
function Cons(%m) {
   StrReplace(%w, "[W]", "~wfx/");
   MessageAll('msgAdmin', "\c5SERVER ADMIN: \c4"@%m@"");
}

function DefaultGame::ZkillUpdateScore(%game, %client, %implement, %zombie){
   if( %implement $= "" || %implement == 0 || %client $= "") {
      //console spamz0r Fix
      return;
   }
   if( %implement.getClassName() $= "Turret") {
	  %client = %implement.getControllingClient();
      if(%client == 0 || %client $= "") {
         %client = %implement.getOwner();
      }
   }
   else if(%implement.getDataBlock().catagory $= "Vehicles") {
	  %client = %implement.getControllingClient();
   }
   %client.Zkills++;
   %client.AwardZombieKill(%zombie, %implement);
   %game.recalcScore(%client);
}

function GameConnection::AwardZombieKill(%client, %zombie, %implement) {
	if(%client $= "" || %client == 0) {
		return;
	}
	%zombieType = %zombie.type;
	//stop right now
	if(%zombie.isBoss && %zombieType != 16) {
		return;
	}
	if(%zombieType $= "") {
		%zombieType = 1;
	}
	%client.TWM2Core.zombiekills[%zombieType]++;
	//Zombie Slayer Challenges (TWM2 3.9.1)
	switch(%zombieType) {
		case 1:
			if(%client.TWM2Core.zombiekills[1] >= 2500) {
				CompleteNWChallenge(%client, "NormHunter1");
				if(%client.TWM2Core.zombiekills[1] >= 5000) {
					CompleteNWChallenge(%client, "NormHunter2");
					if(%client.TWM2Core.zombiekills[1] >= 10000) {
						CompleteNWChallenge(%client, "NormHunter3");
					}
				}
			}	

		case 2:
			if(%client.TWM2Core.zombiekills[2] >= 1000) {
				CompleteNWChallenge(%client, "RavHunter1");
				if(%client.TWM2Core.zombiekills[2] >= 2500) {
					CompleteNWChallenge(%client, "RavHunter2");
					if(%client.TWM2Core.zombiekills[2] >= 5000) {
						CompleteNWChallenge(%client, "RavHunter3");
					}
				}
			}		
		
		case 3:
			if(%client.TWM2Core.zombiekills[3] >= 1000) {
				CompleteNWChallenge(%client, "LordHunter1");
				if(%client.TWM2Core.zombiekills[3] >= 2000) {
					CompleteNWChallenge(%client, "LordHunter2");
					if(%client.TWM2Core.zombiekills[3] >= 3000) {
						CompleteNWChallenge(%client, "LordHunter3");
					}
				}
			}			
		
		case 4:
			if(%client.TWM2Core.zombiekills[4] >= 1000) {
				CompleteNWChallenge(%client, "DemonHunter1");
				if(%client.TWM2Core.zombiekills[4] >= 2500) {
					CompleteNWChallenge(%client, "DemonHunter2");
					if(%client.TWM2Core.zombiekills[4] >= 5000) {
						CompleteNWChallenge(%client, "DemonHunter3");
					}
				}
			}		
		
		case 5:
			if(%client.TWM2Core.zombiekills[5] >= 1500) {
				CompleteNWChallenge(%client, "AirRapHunter1");
				if(%client.TWM2Core.zombiekills[5] >= 3500) {
					CompleteNWChallenge(%client, "AirRapHunter2");
					if(%client.TWM2Core.zombiekills[5] >= 6000) {
						CompleteNWChallenge(%client, "AirRapHunter3");
					}
				}
			}		
		
		case 6:
			if(%client.TWM2Core.zombiekills[6] >= 500) {
				CompleteNWChallenge(%client, "DLordHunter1");
				if(%client.TWM2Core.zombiekills[6] >= 1000) {
					CompleteNWChallenge(%client, "DLordHunter2");
					if(%client.TWM2Core.zombiekills[6] >= 1500) {
						CompleteNWChallenge(%client, "DLordHunter3");
					}
				}
			}		
			
		case 9:
			if(%client.TWM2Core.zombiekills[9] >= 1500) {
				CompleteNWChallenge(%client, "ShifterHunter1");
				if(%client.TWM2Core.zombiekills[9] >= 3000) {
					CompleteNWChallenge(%client, "ShifterHunter2");
					if(%client.TWM2Core.zombiekills[9] >= 6000) {
						CompleteNWChallenge(%client, "ShifterHunter3");
					}
				}
			}

		case 10:
			if(%client.TWM2Core.zombiekills[10] >= 1000) {
				CompleteNWChallenge(%client, "SummonerHunter1");
				if(%client.TWM2Core.zombiekills[10] >= 2500) {
					CompleteNWChallenge(%client, "SummonerHunter2");
					if(%client.TWM2Core.zombiekills[10] >= 5000) {
						CompleteNWChallenge(%client, "SummonerHunter3");
					}
				}
			}	

		case 11:
			if(%client.TWM2Core.zombiekills[11] >= 1000) {
				CompleteNWChallenge(%client, "SniperHunter1");
				if(%client.TWM2Core.zombiekills[11] >= 2500) {
					CompleteNWChallenge(%client, "SniperHunter2");
					if(%client.TWM2Core.zombiekills[11] >= 5000) {
						CompleteNWChallenge(%client, "SniperHunter3");
					}
				}
			}	

		case 12:
			if(%client.TWM2Core.zombiekills[12] >= 1000) {
				CompleteNWChallenge(%client, "UDemHunter1");
				if(%client.TWM2Core.zombiekills[12] >= 2500) {
					CompleteNWChallenge(%client, "UDemHunter2");
					if(%client.TWM2Core.zombiekills[12] >= 5000) {
						CompleteNWChallenge(%client, "UDemHunter3");
					}
				}
			}

		case 13:
			if(%client.TWM2Core.zombiekills[13] >= 1000) {
				CompleteNWChallenge(%client, "VRavHunter1");
				if(%client.TWM2Core.zombiekills[13] >= 2500) {
					CompleteNWChallenge(%client, "VRavHunter2");
					if(%client.TWM2Core.zombiekills[13] >= 5000) {
						CompleteNWChallenge(%client, "VRavHunter3");
					}
				}
			}	

		case 14:
			if(%client.TWM2Core.zombiekills[14] >= 1000) {
				CompleteNWChallenge(%client, "SSHunter1");
				if(%client.TWM2Core.zombiekills[14] >= 2500) {
					CompleteNWChallenge(%client, "SSHunter2");
					if(%client.TWM2Core.zombiekills[14] >= 5000) {
						CompleteNWChallenge(%client, "SSHunter3");
					}
				}
			}		

		case 15:
			if(%client.TWM2Core.zombiekills[15] >= 500) {
				CompleteNWChallenge(%client, "WraithHunter1");
				if(%client.TWM2Core.zombiekills[15] >= 750) {
					CompleteNWChallenge(%client, "WraithHunter2");
					if(%client.TWM2Core.zombiekills[15] >= 1000) {
						CompleteNWChallenge(%client, "WraithHunter3");
					}
				}
			}				
	}

	//END
	//Subduction for implement
	if (%implement.getClassName() $= "Turret") {
		%xpGain = mfloor($TWM2::ZombieXPAward[%zombieType] / 3);
	}
	else if(%implement.getDataBlock().catagory $= "Vehicles") {
		%xpGain = mfloor($TWM2::ZombieXPAward[%zombieType] / 2);
	}
	else {
		%xpGain = $TWM2::ZombieXPAward[%zombieType];
	}
	//
	if(%client.IsActivePerk("Double Down")) {
		GainExperience(%client, %xpGain*2, "[D-D]"@$TWM2::ZombieName[%zombieType]@" Killed ");
	}
	else {
		GainExperience(%client, %xpGain, ""@$TWM2::ZombieName[%zombieType]@" Killed ");
	}
	//Team Gain Perk
	if(%client.IsActivePerk("Team Gain")) {
		%TargetSearchMask = $TypeMasks::PlayerObjectType;
		InitContainerRadiusSearch(%client.player.getPosition(), 20, %TargetSearchMask); //small distance
		while ((%potentialTarget = ContainerSearchNext()) != 0){
			if (%potentialTarget.getPosition() != %pos) {
				if(%potentialTarget.client.team == %client.team && %potentialTarget.client != %client) {
					GainExperience(%potentialTarget.client, %xpGain, "Team gain from "@%client.namebase@" ");
				}
			}
		}
	}
	//some zombies have weapons, throw it :)
	%zombie.throwweapon(1);
	//End
	//HellJump?
	if($TWM::PlayingHellJump || $TWM::PlayingHorde) {
		Game.OnZombieDeath(%client, %zombie);
	}
}

function serverCmdCheckHTilt(%client) {

}

function serverCmdCheckendTilt(%client) {

}

function CureInfection(%player) {
   if(%player.infected) {
      %player.infected = 0;
      if(isEventPending(%player.infectedDamage)) {
         cancel(%player.infectedDamage);
         %player.infectedDamage = "";
         %player.beats = 0;
         %player.canZkill = 0;
         cancel(%player.zombieAttackImpulse);
         %player.setcancelimpulse = 1;
         schedule(5000,0, "eval", ""@%player@".setcancelimpulse=0;"); //goodie
      }
   }
}

function DoMedalCheck(%client, %image) {
   //
   if(%client.isAIControlled()) {
      return 1;
   }
   //
   switch$(%image) {
      case "BOVImage":
         if(%client.hasMedal(11)) {
            return 1;
         }
         else {
            return 0;
         }
      case "LD06SavagerImage":
         if(%client.hasMedal(1)) {
            return 1;
         }
         else {
            return 0;
         }
      case "IonLauncherImage" or "IonRifleImage" or "ConcussionGunImage":
         if(%client.hasMedal(9)) {
            return 1;
         }
         else {
            return 0;
         }
      case "flamerImage":
         if(%client.hasMedal(10)) {
            return 1;
         }
         else {
            return 0;
         }
      case "ShadowRifleImage":
         if(%client.hasMedal(13)) {
            return 1;
         }
         else {
            return 0;
         }
      case "NapalmImage":
         if(%client.hasMedal(27)) {
            return 1;
         }
         else {
            return 0;
         }
      case "MiniColliderCannonImage" or "PlasmasaberImage":
         if(%client.hasMedal(15)) {
            return 1;
         }
         else {
            return 0;
         }
      default:
         return 1;
   }
}

//ARMOR UPDATE
function updateArmorList(%client, %armorList) {
   if(!$Host::Purebuild) {
      %armorList = %armorList TAB "Commando" TAB "Support";
      if(%client.hasMedal(9)) {
         %armorList = %armorList TAB "Microburst";
      }
      if(%client.hasMedal(27)) {
         %armorList = %armorList TAB "Nalcidic";
      }
      if(%client.hasMedal(13)) {
         %armorList = %armorList TAB "Shadow Commando";
      }
   }
   return %armorList;
}

function SimObject::getUpVector(%obj){
   %vec = vectorNormalize(vectorsub(%obj.getEdge("0 0 1"),%obj.getEdge("0 0 -1")));
   return %vec;
}

function Player::IsAlive(%player) {
   if(isObject(%player)) {
      if(%player.getState() $= "dead") {
         return false;
      }
      else {
         return true;
      }
   }
   else {
      return false;
   }
}

function isClientControlledPlayer(%player) {
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %client = ClientGroup.getObject(%i);
      if(%client.player == %player) {
         return true;
      }
   }
   return false;
}

function MessageDevs(%message) {
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      if(ClientGroup.getObject(%i).isDev) {
         messageClient(ClientGroup.getObject(%i), 'MsgDev', ""@CollapseEscape(%message)@"");
      }
   }
}

function isSet(%s) {
   return (%s !$= "");
}

function TransferPieces(%owner, %target) {
   if(%target.recipientOf[%owner] != 1) {
      //uh oh, naughty naughty, no piece stealing
      messageClient(%target, 'msgClient', "\c3You are not a recipient of "@%owner.namebase@"'s pieces.");
      return;
   }
   messageall('MsgAdminForce', "\c3"@ %owner.namebase@" transfered his pieces to "@ %target.namebase@".");
   %group = nameToID("MissionCleanup/Deployables");
   %count = %group.getCount();
   for (%i = 0; %i < %count; %i++) {
      %obj =  %group.getObject(%i);
      if (%obj.getOwner() == %owner) {
         %obj.owner = %target;
         %obj.ownerGuid = %target.guid;
      }
   }
}

function E_Sigma(%from, %to, %formula) {
   %totalSum = 0;
   for(%i = %from; %i < %to; %i++) {
      %totalSum += %formula;
   }
   return %totalSum;
}
//=========================================================

//=========================================================
//TWM2 Damage Control Function
//Added in TWM2 3.8

//I'm mostly using this function to cut down the mess in projectiles.cs and
//to better control the current problems in the boss system.

function TWM2Damage(%projectile, %target, %amount, %dType, %damLoc, %type) {
	//terrain block
	if(%target.getType() & ($TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType)) {
		return;
	}

	%data         = %projectile.getDatablock();
	%sourceObject = %projectile.sourceObject;
	%sourceClient = %sourceObject.client;
	%targetClient = %target.client $= "" ? 0 : %target.client;
	%TDB          = %target.getDatablock();
	if(isObject(%sourceObject)) {
		%SDB       = %sourceObject.getDatablock();
	}
	else {
		%SDB       = "";
	}
	%total        = 1;

	switch$(%type) {
		//Projectiles...
		case "projectile":
			%target.headShot = 0; //Reset first
			if(%sourceClient.ActivePerk["AP Bullets"]) {
				%total *= 1.5;
			}
			if(%targetClient != 0) {
				if(%targetClient.IsActivePerk("Kevlar Armor")) {
					%total *= 0.5;
				}
			}
			if(%target.isZombie) {
				if(Game.CheckModifier("Demonic") == 1) {
					%total = 0.5;
				}
			}
			//------------------------------------------------------
			//source object fixing
			if(strStr(%SDB.getClassName(), "Turret") != -1) {
				if(%SDB.getName() $= "HarbingerGunshipTurret") {
					%projectile.sourceObject = %projectile.sourceObject.mountobj;
					%sourceObject = %projectile.sourceObject;
					%SDB = %sourceObject.getDataBlock();
				}
				else if(%SDB.getName() $= "AC130GunshipTurret") {
					%projectile.sourceObject = %projectile.sourceObject.mountobj;
					%sourceObject = %projectile.sourceObject;
					%SDB = %sourceObject.getDataBlock();
				}
				else if(%SDB.getName() $= "CentaurTurret") {
					%projectile.sourceObject = %projectile.sourceObject.source;
					%sourceObject = %projectile.sourceObject;
					%SDB = %sourceObject.getDataBlock();
				}
			}
			//------------------------------------------------------
			//vehicle kill checking
			//NOTE: THIS DOESN'T GO HERE!!! 
			if(strStr(%SDB.getClassName(), "Vehicle") != -1) {
				if((%target.getType() & ($TypeMasks::PlayerObjectType)) && %target.getState() $= "dead") {
					%pl = %sourceObject.getMountNodeObject(0); //the pilot
					%cl = %pl.client;
					if(%cl !$= "") {
						if(%target.client !$= "" && !%target.isZombie && %target.team != %pl.team) {
							%cl.TWM2Core.PvPVehicleKills++;
							if(%cl.TWM2Core.PvPVehicleKills >= 50) {
								CompleteNWChallenge(%cl, "VehMans1");
								if(%cl.TWM2Core.PvPVehicleKills >= 100) {
									CompleteNWChallenge(%cl, "VehMans2");
									if(%cl.TWM2Core.PvPVehicleKills >= 250) {
										CompleteNWChallenge(%cl, "VehMans3");
									}
								}
							}
						}
						UpdateVehicleKillFile(%cl, %SDB.getName());
						//
						if(%TDB $= "DemonMotherZombieArmor" && %SDB $= "CentaurVehicle") {
							%cl.CDLKills++;
							if(%cl.CDLKills >= 5) {
								AwardClient(%cl, "19");
							}
						}
					}
				}
			}
			//--------------------------------------------------------
			//Headshot checking
			if(%damLoc $= "head" && %TDB.getClassName() $= "PlayerData") {
				if(%data.HeadMultiplier !$= "") {
					%modifier *= %data.HeadMultiplier;
				}
				if(%data.HeadShotKill && $TWM2::HeadshotKill) {
					%target.headShot = 1;
				}
				if(%sourceClient !$= "") {
					if(%sourceClient.UpgradeOn("HSBullets", %projectile.WeaponImageSource) && $TWM2::HeadshotKill) {
						%target.headShot = 1;
					}
				}
				if(%target.headShot) {
					if(%targetClient != 0 && %targetClient.ActivePerk["Head Guard"]) {
						%target.headShot = 0;
					}
					else {
						if((!%target.isBoss && !%target.noHS) && !(%target.getShieldHealth() > 0)) {
							if(%target.isZombie) {
								if(%TDB $= "FZombieArmor") {
									AwardClient(%sourceClient, "16");
								}
								//
								if(Game.CheckModifier("WheresMyHead") == 1) {
									%target.headShot = 0;
								}
								else {
									%total *= 1000;
								}
							}
							else {
								if(%target.isPilot() || %target.vehicleMounted) {
									%target.headShot = 0;
								}
								else {
									%total *= 1000;
									if(%targetClient != 0) {
										BottomPrint(%targetClient, "You Lost Your Head!!!", 3, 1);
										//Recording...
										if(%sourceClient !$= "") {
											%sourceClient.TWM2Core.PvPHeadshotKills++;
											if(%sourceClient.TWM2Core.PvPHeadshotKills >= 100) {
												CompleteNWChallenge(%sourceClient, "HSHoncho1");
												if(%sourceClient.TWM2Core.PvPHeadshotKills >= 200) {
													CompleteNWChallenge(%sourceClient, "HSHoncho2");
													if(%sourceClient.TWM2Core.PvPHeadshotKills >= 300) {
														CompleteNWChallenge(%sourceClient, "HSHoncho3");
													}
												}
											}									 
										}
									}
								}
							}
						}
					}
				}
			}
			else if(%damLoc $= "legs") {
				if(%data.LegsMultiplier !$= "") {
					%total *= %data.LegsMultiplier;
				}
			}
			
		case "explosion":
			%total = 1;
			if(%dType == $DamageType::RapierShield) {
				if(%target == %sourceObject || %target.isZombie || %target.isBoss) {
					%total = 0;
				}
			}
	}

	%deal = %total * %amount;
	if(%target.isBoss) {
		if(%dType == $DamageType::SuperChaingun) {
			%deal = 0;
		}
		%sourceClient.damageToBoss += %deal;
	}

	return %total;
}

//===============================================================================

//===============================================================================
// Seeking Projectiles: Used by some bosses
// TWM2 3.8: I'm adding this to allow much easier coding of projectiles that can
// 'follow' players

//Assign by this GVar
$ProjectileSeek["GhostFlameboltMain", "ProjectileControls"] = "8 10 10 1";
$ProjectileSeek["GhostFlameboltMain", "CancelList"] = "FlareGrenadeProj";

$ProjectileSeek["RapierShieldForwardProjectile", "ProjectileControls"] = "8 10 10 1";
$ProjectileSeek["RapierShieldForwardProjectile", "CancelList"] = "FlareGrenadeProj";

function createSeekingProjectile(%projectile, %pType, %iPos, %iDir, %source, %target, %seekActivation) {
   %proj = spawnprojectile(%projectile, %pTYpe, %iPos, %iDir, %source);
   schedule(%seekActivation, 0, projectileSeeking, %proj, %target);
   
   return %proj;
}

function projectileSeeking(%p, %target) {
   %pName = %p.getDatablock().getName();
   %pArgs = $ProjectileSeek[%pName, "ProjectileControls"];

   %projpos = %p.position;
   %projdir = %p.initialdirection;
   %s       = %p.sourceObject;
   
   if(!isobject(%p)) {
      return;
   }
   if(isobject(%p)) {
      %p.delete();
   }
   if(!isobject(%target)) {
      return;
   }
   if(%target.getClassName() $= "Player") {
      if(%target.getState() $= "Dead") {
         return;
      }
   }
   
   %iX = getWord(%projdir, 0);
   %iY = getWord(%projdir, 1);
   %iZ = getWord(%projdir, 2);

   %projdir = vectornormalize(vectorsub(%target.getPosition(), %projpos));
   %nDx = getWord(%projdir, 0);
   %nDy = getWord(%projdir, 1);
   %nDz = getWord(%projdir, 2);

   %xCalc = %iX - %nDx;
   %xCalc = ((%xCalc / getWord(%pArgs, 0)) * -1) + %iX;
   %yCalc = %iY - %nDy;
   %yCalc = ((%yCalc / getWord(%pArgs, 1)) * -1) + %iY;
   %zCalc = %iZ - %nDz;
   %zCalc = ((%zCalc / getWord(%pArgs, 2)) * -1) + %iZ;

   %_newDir = %xCalc SPC %yCalc SPC %zCalc;
   
   %cnS = %pName.className;
   %type = strReplace(%cnS, "Data", "");
   
   %p = new (%type)() {
      dataBlock        = %pName;
      initialDirection = %_newDir;
      initialPosition  = %projpos;
   };
   %p.sourceobject = %s;
   MissionCleanup.add(%p);
   if(getWord(%pArgs, 3) == 1) {
      %searchmask = $TypeMasks::ProjectileObjectType;
      InitContainerRadiusSearch(%projpos, 12, %searchmask);
      while ((%testTarget = containerSearchNext())) {
         if(%testTarget.getdatablock().getname() $= $ProjectileSeek[%pName, "CancelList"]) {
            %testTarget.delete();
            return;
         }
      }
   }
   
   %p.seeksched = schedule( 80,0, "projectileSeeking", %p, %target);
}
