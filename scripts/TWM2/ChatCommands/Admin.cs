function parseAdminCommands(%sender, %command, %args) {
   switch$(strLwr(%command)) {
      //admincmds: lists the avaliable mod admin commands
      case "admincmds":
         if (!%sender.isadmin) {
            return 2;
         }
         messageclient(%sender, 'MsgClient', '\c5TWM2 Admin Commands.');
         messageclient(%sender, 'MsgClient', '\c3/moveme, /moveto, /kill, /goto, /summon');
         messageclient(%sender, 'MsgClient', '\c3/removePieces, /giveOrphans, /forcePieces');
         messageclient(%sender, 'MsgClient', '\c3/myname, /setname, /cancelVote, /A, /getPos');
         messageclient(%sender, 'MsgClient', '\c3/bp, /cp, /confiscate, /gag, /ZCmds, /passVote');
         messageclient(%sender, 'MsgClient', '\c3/getDBs, /giveGun, /TwoTeams, /slap, /freeze');
         messageclient(%sender, 'MsgClient', '\c3/warn');
         return 1;
         
      //moveme: moves a player in worldspace coords
      case "moveme":
         if (!%sender.isadmin) {
            return 2;
         }
         if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2You must have a player object.');
            return 1;
         }
         %currentpos = %sender.player.getposition();
         %movepos = %args;
         if(mAbs(getWord(%movePos, 0)) > 5000 || mAbs(getWord(%movePos, 1)) > 5000 || mAbs(getWord(%movePos, 2)) > 5000) {
            messageclient(%sender, 'MsgClient', '\c2Invalid coordinate provided.');
            return 1;
         }
         %gototarget = vectoradd(%currentpos, %movepos);
         %sender.player.settransform(%gototarget);
         messageclient(%sender,'MsgClient', "\c5Moving "@%args@".");
         return 1;
      
      //moveto: teleports directly to the given worldspace coords
      case "moveto":
         if (!%sender.isadmin) {
            return 2;
         }
         if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2You must have a player object.');
            return 1;
         }
         %movepos = %args;
         if(mAbs(getWord(%movePos, 0)) > 10000 || mAbs(getWord(%movePos, 1)) > 10000 || mAbs(getWord(%movePos, 2)) > 10000) {
            messageclient(%sender, 'MsgClient', '\c2Invalid coordinate provided.');
            return 1;
         }
         %sender.player.settransform(%movepos);
         messageclient(%sender,'MsgClient', "\c2Moving to \c3"@%args@".");
         return 1;
      
      //kill: simple enough, smite a player dead
      case "kill":
         if (!%sender.isadmin) {
            return 2;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         if(!isObject(%target.player) || %target.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2The Target Player must have a player object.');
            return 1;
         }
         %target.player.scriptkill($DamageType::Admin);
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 killed \c3"@ %target.namebase@"\c2.");
         messageclient(%target, 'MsgClient', "\c5you were Pinged by "@ %sender.namebase@", Enjoy your pain");
         return 1;
      
      //goto: teleport to a given player
      case "goto":
         if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2The Target Player must have a player object.');
            return 1;
         }
         if (!%sender.isadmin) {
            return 2;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         if(!isObject(%target.player) || %target.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2The Target Player must have a player object.');
            return 1;
         }
         %targetpos = %target.player.getposition();
         %transoffset = getrandom(-2,2) SPC getrandom(-2,2) SPC getrandom(-2,2);
         %gototarget = vectoradd(%targetpos, %transoffset);
         %sender.player.settransform(%gototarget);
         %sender.player.setvelocity("0 0 0");
         messageclient(%sender,'MsgClient', "\c5you warped to " @ %target.namebase @ ".");
         messageclient(%target,'MsgClient', "\c5" @ %sender.namebase @ " has come to you.");
         return 1;
      
      //summon: teleport a player to you
      case "summon":
         if (!%sender.isadmin) {
            return 2;
         }
         if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2You must have a player object.');
            return 1;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         if(!isObject(%target.player) || %target.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2The Target Player must have a player object.');
            return 1;
         }
         if(%target.player.isPilot() == true) {
            messageclient(%sender, 'MsgClient', '\c2You cant summon pilots.');
            return 1;
         }
         %senderpos = %sender.player.getposition();
         %transoffset = getrandom(-2,2) SPC getrandom(-2,2) SPC 2;
         %gotosender = vectoradd(%senderpos, %transoffset);
         %target.player.settransform(%gotosender);
         %target.player.setvelocity("0 0 0");
         messageclient(%sender, 'MsgClient', "\c3You summoned "@%target.namebase@ ".");
         messageclient(%target, 'MsgClient', "\c3The admin has summoned you.");
         return 1;
      
      //removePieces: delete a specific player's pieces
      case "removepieces":
         if (!%sender.isadmin) {
            return 2;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         if (%target.isadmin && %sender.isadmin) {
            messageclient(%sender, 'MsgClient', '\c2You cant remove Other Admin pieces.');
            return 1;
         }
         if (%target.issuperadmin && %sender.isadmin) {
            messageclient(%sender, 'MsgClient', '\c2You cant remove SA pieces.');
            return 1;
         }
         if (%target.issuperadmin && %sender.issuperadmin) {
            messageclient(%sender, 'MsgClient', '\c2You cant remove Other SA pieces.');
            return 1;
         }
         if (%target == %sender) {
            messageclient(%sender, 'MsgClient', '\c2The Command for self piece removal is /delmypieces');
            return 1;
         }
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Removed \c3"@ %target.namebase@"'s\c2 Pieces for Violations.");
         %group = nameToID("MissionCleanup/Deployables");
         %count = %group.getCount();
         for (%i = 0; %i < %count; %i++) {
            %obj = %group.getObject(%i);
            if (%obj.getOwner() == %target) {
               %random = getRandom(500,3000);
               %obj.getDataBlock().schedule(%random,"disassemble",%sender.player,%obj);
            }
         }
         return 1;
      
      //giveOrphans: give all unowned pieces to a player
      case "giveorphans":
         if (!%sender.isadmin) {
            return 2;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 gave all orphanned pieces to \c3"@ %target.namebase@"\c2.");
         %group = nameToID("MissionCleanup/Deployables");
         %count = %group.getCount();
         for (%i = 0; %i < %count; %i++) {
            %obj = %group.getObject(%i);
            if (%obj.getOwner() $= "") {
               %obj.owner = %target;
               %obj.ownerGuid = %target.guid;
            }
         }
         return 1;
      
      //forcePieces: same effect as giveorphans, but it gives an owned players pieces away
      case "forcepieces":
         if (!%sender.isadmin) {
            return 2;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such target player.');
            return 1;
         }
         //
         %nametotest2 = getword(%args,1);
         %target2 = plnametocid(%nametotest2);
         if (%target2 == 0) {
            messageclient(%sender, 'MsgClient', '\c2Reciving player not located, Giving to you.');
            %target2 = %sender;
            return 1;
         }
         //
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 gave \c3"@%target.namebase@"'s\c2 pieces to \c3"@ %target2.namebase@"\c2.");
         %group = nameToID("MissionCleanup/Deployables");
         %count = %group.getCount();
         for (%i = 0; %i < %count; %i++) {
            %obj = %group.getObject(%i);
            if (%obj.getOwner() $= %target) {
               %obj.owner = %target2;
               %obj.ownerGuid = %target2.guid;
            }
         }
         return 1;
      
      //myname: change your player name
      case "myname":
         if (!%sender.isadmin) {
            return 2;
         }
         %name = getwords(%args,0);
         if(strstr(%name, "Phantom139") != -1) {
            if(%sender.guid !$= "2000343") {
               messageclient(%sender, 'MsgClient', '\c2That name is reserved.');
               return 1;
            }
         }
         %oldname = %sender.namebase;
         if (%name $= "default") {
            %authInfo = %sender.getAuthInfo();
            %name = getField( %authInfo, 0 );
            %tag = getField(%authInfo, 6);
            %append = getField(%authInfo, 7 );
            if ( %append ) {
               %name = "\cp\c6" @ %name @ "\c7" @ %tag @ "\co";
            }
            else {
               %name = "\cp\c7" @ %tag @ "\c6" @ %name @ "\co";
            }
            messageclient(%sender, 'MsgClient', "\c2Your Name Has Reset");
            MessageAll( 'MsgClientNameChanged', "", %sender.name, %name, %sender );
            removeTaggedString(%sender.name);
            %sender.name = addTaggedString(%name);
            setTargetName(%sender.target, %sender.name);
            checkGUID(%sender);
            return 1;
         }
         %oldName = getTaggedString(%sender.name);
         %sender.name = addTaggedString(collapseEscape(%name));
         setTargetName(%sender.target, %sender.name);
         messageAll('MsgClientNameChanged', "", %oldName, %sender.name, %sender);
         messageclient(%sender, 'MsgClient', "\c2Your Name Has Been Set To "@%args@"");
         return 1;
      
      //setname: change another player's name
      case "setname":
         if (!%sender.isadmin) {
            return 2;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         %nametoset=getwords(%args,1);
         %oldname = %target.namebase;
         if(strstr(%nametoset, "Phantom139") != -1) {
            if(%target.guid !$= "2000343") {
               messageclient(%sender, 'MsgClient', '\c2That name is reserved.');
               return 1;
            }
         }
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         if (%target.isdev) {
            messageclient(%sender, 'MsgClient', '\c2No changing Dev names.');
            return 1;
         }
         if (%target.isadmin && %sender.isadmin && !%sender.isSuperAdmin && !%sender.isDev) {
            messageclient(%sender, 'MsgClient', "\c2Error: ranking Issue, "@%target.namebase@" is your level of admin.");
            return 1;
         }
         if (%target.issuperadmin && %sender.isadmin && %sender.isSuperAdmin && !%sender.isDev) {
            messageclient(%sender, 'MsgClient', "\c2Error: ranking Issue, "@%target.namebase@" is your level of admin.");
            return 1;
         }
         if (%target.issuperadmin && %sender.isadmin && !%sender.isSuperAdmin && !%sender.isDev) {
            messageclient(%sender, 'MsgClient', "\c2Error: Over-ranking Issue, "@%target.namebase@" outranks you");
            return 1;
         }
         if (%nametoset $= "default") {
            %authInfo = %target.getAuthInfo();
            %name = getField( %authInfo, 0 );
            %tag = getField(%authInfo, 6);
            %append = getField(%authInfo, 7 );
            if ( %append ) {
               %name = "\cp\c6" @ %name @ "\c7" @ %tag @ "\co";
            }
            else {
               %name = "\cp\c7" @ %tag @ "\c6" @ %name @ "\co";
            }
            messageclient(%sender, 'MsgClient', "\c2"@%oldname@"'s Name Has Reset");
            messageclient(%target, 'MsgClient', "\c2"@%sender.namebase@" Reset your name");
            MessageAll( 'MsgClientNameChanged', "", %target.name, %name, %target );
            removeTaggedString(%target.name);
            %target.name = addTaggedString(%name);
            setTargetName(%target.target, %target.name);
            checkGUID(%target);
            return 1;
         }
         %oldName = getTaggedString(%Target.name);
         %Target.name = addTaggedString(collapseEscape(%nametoset));
         setTargetName(%Target.target, %Target.name);
         messageAll('MsgClientNameChanged', "", %oldName, %Target.name, %Target);
         messageclient(%target, 'MsgClient', "\c2Your Name Has Been Set To "@%name@"");
         return 1;
      
      //cancelvote: cancel a current vote in progress
      case "cancelvote":
         if (!%sender.isadmin) {
            return 2;
         }
         if(game.schedulevote !$="") {
            cancel(game.ScheduleVote);
            Game.scheduleVote = "";
            Game.kickClient = "";
            clearVotes();
            %count = clientgroup.getcount();
            for(%i = 0; %i < %count; %i++) {
               messageClient(clientgroup.getobject(%i), 'closeVoteHud', "");
            }
            messageAll('MsgAdminForce', "\c2Vote Canceled by \c3"@%sender.namebase@"\c2.");
            return 1;
         }
         else {
            messageclient(%sender, 'MsgClient', '\c2No Vote To Cancel.');
            return 1;
         }
      
      //a: admin only chat
      case "a":
         if (!%sender.isadmin) {
            return 2;
         }
         %count = ClientGroup.getCount();
         for (%i = 0; %i < %count; %i++) {
            %cl = ClientGroup.getObject( %i );
            if(%cl.isadmin) {
               messageclient(%cl, 'MsgClient', "\c5[A]"@%sender.namebase@" : \c4"@%args@"");
            }
         }
         return 1;
      
      //getPos: get your current worldspace positions
      case "getpos":
         if (!%sender.isadmin) {
            return 2;
         }
         if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2You must have a player object.');
            return 1;
         }
         messageclient(%sender, 'MsgClient', "\c2Your current map position is: \c3"@%sender.player.getPosition()@"\c2.");
         return 1;
      
      //bp: bottom print message to everyone
      case "bp":
         if (!%sender.isadmin) {
            return 2;
         }
         MessageAll('MsgAdminPrint',"\c5[BP]"@%sender.namebase@": \c4"@%args@"");
         BottomPrintAll(""@%sender.namebase@" : "@%args, 5, 3);
         return 1;
      
      //cp: center print message to everyone
      case "cp":
         if (!%sender.isadmin) {
            return 2;
         }
         MessageAll('MsgAdminPrint',"\c5[CP]"@%sender.namebase@": \c4"@%args@"");
         CenterPrintAll(""@%sender.namebase@" : "@%args, 5, 3);
         return 1;
      
      //confiscate: revoke a player's admin weapons
      case "confiscate":
         if (!%sender.isadmin) {
            return 2;
         }
         if(%sender.isAdmin && !%sender.isSuperAdmin) {
            if (%sender.isconfiscated) {
               if(%sender.Confisc["BySA"]) {
                  messageclient(%sender, 'MsgClient', '\c5You cannot undo an SA confiscate');
                  return 1;
               }
               else {
                  messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 can now use his weapons again");
                  messageclient(%sender, 'MsgClient', '\c5 You are re-allowed to your weapons');
                  %sender.isconfiscated =0;
                  return 1;
               }
            }
            else {
               %sender.isconfiscated =1;
               %sender.getsadminguns = 0;
               if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
                  return 1;
               }
               else {
                  %sender.player.use(SuperChaingun);
                  %sender.player.throwweapon(1);
                  %sender.player.throwweapon(0);
               }
               %Gender = (%sender.sex $= "Male" ? 'his' : 'her');
               messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Revoked "@getTaggedString(%gender)@" admin Weapons");
               messageclient(%sender, 'MsgClient', '\c5 You lost all of your admin weapons');
               return 1;
            }
         }
         else {
            %nametotest = getword(%args, 0);
            %target = plnametocid(%nametotest);
            if(%nametotest $= "") {
               if (%sender.isconfiscated) {
                  messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 can now use his weapons again");
                  messageclient(%sender, 'MsgClient', '\c5 You are re-allowed to your weapons');
                  %sender.isconfiscated =0;
                  return 1;
               }
               else {
                  %sender.isconfiscated =1;
                  %sender.getsadminguns = 0;
                  %sender.getsSAguns = 0;
                  if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
                     return 1;
                  }
                  else {
                     %sender.player.use(SuperChaingun);
                     %sender.player.throwweapon(1);
                     %sender.player.throwweapon(0);
                  }
                  %Gender = (%sender.sex $= "Male" ? 'his' : 'her');
                  messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Revoked "@getTaggedString(%gender)@" admin Weapons");
                  messageclient(%sender, 'MsgClient', '\c5 You lost all of your admin weapons');
                  return 1;
               }
            }
            else {
               if (%target == 0) {
                  messageclient(%sender, 'MsgClient', '\c2No such player.');
                  return 1;
               }
               if (%target.isconfiscated) {
                  if(%target.isAdmin && !%target.isSuperAdmin && %target.Confisc["BySA"] == 1) {
                     messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 allowed \c3"@ %target.namebase@" To get Weapons again");
                     messageclient(%target, 'MsgClient', '\c5 You are re-allowed to weapons');
                     %target.isconfiscated =0;
                     %target.Confisc["BySA"] = 0;
                     return 1;
                  }
                  else if(%target.isSuperAdmin){
                     messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 allowed \c3"@ %target.namebase@" To get Weapons again");
                     messageclient(%target, 'MsgClient', '\c5 You are re-allowed to weapons');
                     %target.isconfiscated =0;
                     return 1;
                  }
               }
               else {
                  if(%target.isAdmin && !%target.isSuperAdmin) {
                     %target.isconfiscated =1;
                     %target.getsSAguns = 0;
                     %target.getsadminguns = 0;
                     %target.Confisc["BySA"] = 1;
                  }
                  else if(%target.isSuperAdmin) {
                     %target.isconfiscated =1;
                     %target.getsSAguns = 0;
                     %target.getsadminguns = 0;
                  }
                  if(!isObject(%target.player) || %target.player.getState() $= "dead") {
                     return 1;
                  }
                  else {
                     %target.player.use(SuperChaingun);
                     %target.player.throwweapon(1);
                     %target.player.throwweapon(0);
                  }
                  messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Revoked \c3"@ %target.namebase@"'s Weapons");
                  messageclient(%target, 'MsgClient', '\c5 You lost all of your admin weapons');
                  return 1;
               }
            }
         }
      
      //gag: mute a player
      case "gag":
         if (!%sender.isadmin) {
            return 2;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         %time = getword(%args, 1);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         if(!%sender.isSuperAdmin && %sender.isAdmin && %target.isAdmin) {
            messageclient(%sender, 'MsgClient', '\c2You cannot gag another Admin.');
            return 1;
         }
         if(!%sender.isDev && %sender.isSuperAdmin && %target.isSuperAdmin) {
            messageclient(%sender, 'MsgClient', '\c2You cannot gag another SA.');
            return 1;
         }
         if(%time == 0 || %time $= "") {
            if(!%target.isgagged) {
               messageall('MsgAdminForce', "\c3"@%sender.namebase@"\c2 Gagged \c3"@%target.namebase@"!");
               %target.isgagged = 1;
               return 1;
            }
            else {
               messageall('MsgAdminForce', "\c3"@%sender.namebase@"\c2 Un-gagged \c3"@%target.namebase@"!");
               %target.isgagged = 0;
               return 1;
            }
         }
         else {
            if(!%target.isgagged) {
               messageall('MsgAdminForce', "\c3"@%sender.namebase@"\c2 Gagged \c3"@%target.namebase@"\c2 for \c3"@%time@"\c2 seconds!");
               %target.isgagged = 1;
               schedule(%time * 1000,0, eval, ""@%target@".isgagged = 0;");
               return 1;
            }
         }
      
      //passvote: passes a current vote in progress
      case "passvote":
         if (!%sender.isAdmin) {
            return 2;
         }
         else if (Game.scheduleVote $= "") {
            MessageClient(%sender, 'MsgClient', "\c2No vote is currently in progress..");
         }
         else {
            MessageAll('Msg', '\c3%1 \c2has passed the vote.', %sender.namebase);
            //Housekeeping..
            for (%cl = 0; %cl < ClientGroup.getCount(); %cl++) {
               %client = ClientGroup.getObject(%cl);
               messageClient(%client, 'clearVoteHud', "");
               messageClient(%client, 'closeVoteHud', "");
               %client.vote = "";
            }
            //Evaluate the vote.
            if (Game.voteType !$= "BossVote") {
               %cmd = Game.voteType;
               %arg1 = Game.Varg1;
               %arg2 = Game.Varg2;
               %arg3 = Game.Varg3;
               %arg4 = Game.Varg4;
               Game.evalVote(%cmd, %sender, %arg1, %arg2, %arg3, %arg4);
            }
            else {
               messageAll('MsgVotePassed', '\c1%1\c2 spawned by vote.', BossFullname(Game.BVoteboss));
               VoteBoss_StartBoss(Game.BVoteboss);
            }
            cancel(Game.scheduleVote);
            Game.scheduleVote = "";
         }
         return 1;
      
      //getdbs: list all avaliable datablocks for /givegun
      case "getdbs":
         if (!%sender.isadmin){
            return 2;
         }
         %count = DatablockGroup.getCount();
         %x = 0;
         for(%i = 0; %i < %count; %i++) {
            %db = DatablockGroup.GetObject(%i);
            if(%db.getName().getClassname() $= "ItemData") {
               if(%db.getName().classname $= "Weapon" && !%db.isKSSW) {
                  %Item[%x] = %db.getName();
                  %x++;
               }
            }
         }
         %a = 0;
         for(%r = %x; %r > 0; %r -= 4) {
            %msg[%a] = ""@%Item[%r]@" "@%Item[%r-1]@" "@%Item[%r-2]@" "@%Item[%r-3]@"";
            messageclient(%sender, 'MsgClient', "\c5"@%msg[%a]@"");
            %a++;
         }
         return 1;
      
      //givegun: give a weapon to a player
      case "givegun":
         if (!%sender.isadmin){
            return 2;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         if(!isObject(%target.player) || %target.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2The target player must have a player object.');
            return 1;
         }
         %gun = getword(%args,1);
         if(%gun.getClassname() !$= "ItemData") {
            messageclient(%sender, 'MsgClient', '\c2Unknown Item, use /getDBs to get items.');
            return 1;
         }
         else {
            if(!%gun.isKSSW) {
               %Target.player.setInventory(%gun, 1, true);
               %Target.player.setInventory(%gun @ "ammo", 999, true);
            }
            else {
               messageclient(%sender, 'MsgClient', '\c2You cannot give killsteaks out with this command.');
            }
            return 1;
         }
         if(!%sender.nomssages) {
            messageclient(%target, 'MsgClient', "\c2You Recieved A "@%gun@" From "@%sender.namebase@".");
            messageclient(%sender, 'MsgClient', "\c2You Gave "@%target.namebase@" a "@%gun@".");
            return 1;
         }
      
      //twoteams: toggle two teams or one team
      case "twoteams":
         if (!%sender.isadmin){
            return 2;
         }
         if (!$TWM::twoteams) {
            $TWM::twoteams = true;
            messageall('MsgAdminForce',"\c2There are now two teams");
            game.numteams=2;
            setSensorGroupCount(3);
            return 1;
         }
         else {
            $TWM::twoteams = false;
            messageall('MsgAdminForce',"\c2There is now only one team");
            game.numteams = 1;
            setSensorGroupCount(2);
            for(%i = 0; %i < ClientGroup.getCount(); %i++) {
               %cl = ClientGroup.getObject(%i);
               game.clientChangeTeam(%cl, 1, 0);
            }
            return 1;
         }
      
      //slap: slaps a player doen for 3 seconds
      case "slap":
         if(!%sender.isAdmin) {
            return 2;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         if(isObject(%target.player)) {
            %target.player.setActionThread("death11",true);
            messageall('MsgSLAP', "\c3"@getTaggedString(%sender.name)@"\c2 Slapped \c3"@getTaggedString(%target.name)@"\c2. ~wfx/misc/slapshot.wav");
            %target.player.setDamageFlash(1);
            %target.player.setMoveState(true);
            %target.player.schedule(3000, "SetMoveState", false);
         }
         else {
            messageclient(%sender, 'MsgClient', "\c2"@%target.namebase@" be dead :)");
         }
         return 1;
      
      //freeze: EMP Lock a player
      case "freeze":
         if (!%sender.isAdmin) {
            messageclient(%sender,'msgclient',"Admin Clearance for Level 1 Required.");
            return 1;
         }
         %name = getword(%args, 0);
         %time = getword(%args, 1);
         %target = plnametocid(%name);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', "Unknown player: "@%args@".");
            return 1;
         }
         if (%time $= "" || %time <= 0) {
            %time = 60; //60 seconds.
         }
         messageall('Msgall', "\c3"@%sender.namebase@"\c2 froze \c3"@%target.namebase@"\c2 for \c3"@%time@"\c2 second(s).");
         messageclient(%target, 'MsgClient', "You have been frozen by "@%sender.namebase@" for "@%time@" second(s).");
         %target.player.isemped = 0;
         %ff = PlayerEmpLock(%target.player,%time*1000); //Not sure if this is correct
         %target.player.setDamageFlash(1);
         %target.player.setMoveState(false);
         %target.player.setMoveState(true);
         %target.player.schedule(%time*1000, setMoveState, false);
         %ff.schedule(%time*1000, "Delete");
         return 1;
      
      //warn: administer a warning to a player
      case "warn":
         if(!%sender.isAdmin) {
            messageclient(%sender, 'MsgClient', '\c5Admin Clearance For Level 1 Needed.');
            return 1;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         messageAll('msgAdminForce', "\c3"@%sender.namebase@" has issued a warning to "@%target.namebase@" for misconduct.");
         messageClient(%target, 'msgAlert', "\c3"@%sender.namebase@" has issued you a warning for misconduct, cease your actions.");
         BottomPrint(%target, "You have recieved a warning for misconduct\nCease your actions at once!", 3, 3);
         return 1;
      
      //=======================================================================
      //=======================================================================
      //=======================================================================
      // ZOMBIE COMMANDS
      //=======================================================================
      //=======================================================================
      //=======================================================================
      
      //zCmds: list all zombie commands
      case "zcmds":
         messageclient(%sender, 'MsgClient', '\c5TWM2 Zombie Chat Commands');
         messageclient(%sender, 'MsgClient', '\c3/BuyZPack, /SpawnZ, /KillZombies, /cure');
         messageclient(%sender, 'MsgClient', '\c3/MakeZ');
         return 1;
      
      //buyZPack: adds a zombie spawn pack to your inventory
      case "buyzpack":
         if (!%sender.isadmin){
            if(!%sender.isZombieCommander) {
               return 2;
            }
         }
         if($TWM::PlayingHellJump || $TWM::PlayingHorde) {
            messageClient(%sender, 'MsgClient', '\c2Cannot spawn bosses in horde or helljump');
            return 1;
         }
         if(isObject(%sender.player)) {
            if(%sender.player.getMountedImage($Backpackslot) !$= "")
   	           %sender.getControlObject().throwPack();
            %sender.player.setinventory(ZSpawnDeployable,1,true);
            return 1;
         }
         
      //spawnz: spawn zombies
      case "spawnz":
         //Initial Checks
         if (!%sender.isadmin){
            if(!%sender.isZombieCommander) {
               return 2;
            }
         }
         if($TWM::PlayingHellJump || $TWM::PlayingHorde) {
            messageClient(%sender, 'MsgClient', '\c2Cannot spawn bosses in horde or helljump');
            return 1;
         }
         if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2You must have a player object.');
            return 1;
         }
         //How many?
         %amount = getWord(%args, 0);
         if(%amount < 1) {
            messageclient(%sender, 'MsgClient', '\c5HAHAHA.. NEGATIVE ZOMBIES... HA... Thats funny!!');
            return 1;
         }
         if(%amount > 100) {
            messageclient(%sender, 'MsgClient', '\c5Thats too many...');
            return 1;
         }
         // Zombie Selection
         %adminRequired = 1; //1- Admin, 2- SA, 3- Dev
         %type = getWord(%args, 1);
         if(%type $= "Norm") { %var = 1; %adminRequired = 1; %message = "spawned a swarm of * zombies";}
         else if(%type $= "Rav") { %var = 2; %adminRequired = 1; %message = "spawned a swarm of * ravenger zombies";}
         else if(%type $= "Lord") { %var = 3; %adminRequired = 1; %message = "started a * Lord FRENZY";}
         else if(%type $= "Dem") { %var = 4; %adminRequired = 1; %message = "spawned a swarm of * demon zombies";}
         else if(%type $= "Rap") { %var = 5; %adminRequired = 1; %message = "spawned a swarm of * air rapier zombies";}
         else if(%type $= "DLord") { %var = 6; %adminRequired = 2; %message = "unleashed * demon lord zombies";}
         else if(%type $= "Shift") { %var = 9; %adminRequired = 1; %message = "spawned a swarm of * shifter zombies";}
         else if(%type $= "Summon") { %var = 10; %adminRequired = 2; %message = "spawned * summoner zombies";}
         else if(%type $= "Sniper") { %var = 11; %adminRequired = 1; %message = "spawned a swarm of * sniper zombies";}
         else if(%type $= "ULord") { %var = 12; %adminRequired = 1; %message = "spawned a swarm of * ultra demon zombies";}
         else if(%type $= "VRav") { %var = 13; %adminRequired = 1; %message = "spawned a swarm of * volatile ravenger zombies";}
         else if(%type $= "SS") { %var = 14; %adminRequired = 1; %message = "spawned a swarm of * slingshot zombies, pilots beware";}
         else if(%type $= "Wraith") { %var = 15; %adminRequired = 2; %message = "spawned a swarm of * wraith spec-ops zombies";}
         else if(%type $= "Rog") { %var = 16; %adminRequired = 2; %message = "unleashes General Rog";}
         else {
            messageclient(%sender, 'MsgClient', '\c2Unknown Type, These Are Accepted:');
            messageclient(%sender, 'MsgClient', '\c2Norm, Rav, Lord, Dem, Rap, DLord, Shift');
            messageclient(%sender, 'MsgClient', '\c2Summon, Snipe, ULord, VRav, SS, Wraith, Rog');
            return 1; //stop here
         }
         if(%amount > 10 && %var == 10) {
            messageclient(%sender, 'MsgClient', '\c5Summoners = More Zombies, thats too many pal.');
            return 1;
         }
         if(%var == 16) {
            %amount = 1;
         }
         //Admin Level Check
         if(%adminRequired == 3 && !%sender.isDev && !%sender.isHost) {
            messageclient(%sender, 'MsgClient', "\c2Error: Admin Clearance For Level 3 Required For This Zombie Type");
            return 1;
         }
         else if(%adminRequired == 2 && !%sender.isSuperAdmin) {
            messageclient(%sender, 'MsgClient', "\c2Error: Admin Clearance For Level 2 Required For This Zombie Type");
            return 1;
         }
         //*****
         // Assign The Position
         //*****
         //*****
         %pos1 = %sender.player.getposition();
         %pos2 = "0 0 20";
         %Fpos = vectoradd(%pos1,%pos2);
         //*****
         %message = strReplace(%message, "*", %amount);
         messageall('MsgAdminForce', "\c3"@%sender.namebase@"\c2 "@%message@".");
         for(%i = 0; %i < %amount; %i++) {
            %time = %i * 500;
            schedule(%time, 0, StartAZombie, %Fpos, %var);
         }
         return 1;
         
      //killzombies: eliminate all zombies, cure all infections
      case "killzombies":
         if (!%sender.issuperadmin){
            return 3; //while I agree this belongs with the SA stuff, It's nice to keep the zombie commands together
         }
         %count = ZombieGroup.getCount();
         for(%i = 0; %i < %count; %i++) {
            %obj = ZombieGroup.getObject(%i);
            if(isObject(%obj)) {
               if(%obj.infected) {
                  %obj.infected = 0;
                  messageclient(%obj.client, 'MsgClient', '\c2You have Been Cured.');
                  if(isEventPending(%obj.infectedDamage)) {
                     cancel(%obj.infectedDamage);
                     %obj.infectedDamage = "";
                     %obj.beats = 0;
                     %obj.canZkill = 0;
                     %obj.setcancelimpulse = 1;
                     schedule(1000,0, "resetattackImpulse" ,%obj); //goodie
                  }
               }
               if(%obj.iszombie) {
                  %obj.scriptkill($DamageType::admin);
               }
               else {
                  continue;
               }
            }
         }
         messageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c2 Eliminated All Zombies and Cured All Infections.");
         return 1;
         
      //cure: cure an infection, un-zombify a player
      case "cure":
         if (!%sender.isadmin){
            return 2;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         if(!isSet(trim(%args))) {
            %target = %sender;
         }
         if(!isObject(%target.player) || %target.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2The Target Player must have a player object.');
            return 1;
         }
         if(%target.player.iszombie) {
            %targetlastpos = %target.player.getworldboxcenter();
            makePersonHumanFZomb(%targetlastpos, %target);
         }
         CureInfection(%target.player);
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Cured \c3"@%target.namebase@"'s\c2 Infection.");
         messageclient(%target, 'MsgClient', "\c5Your Infection Has Been Cured");
         return 1;

      //makez: make a player a zombeh
      case "makez":
         if (!%sender.isadmin){
            if(!%sender.isZombieCommander) {
               return 2;
            }
         }
         if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2You must have a player object.');
            return 1;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         if(!isObject(%target.player) || %target.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2The Target Player must have a player object.');
            return 1;
         }
         // Zombie Selection
         %adminRequired = 1; //1- Admin, 2- SA, 3- Dev/host
         %type = getWord(%args, 1);
         if(%type $= "Norm") { %var = 1; %adminRequired = 1; }
         else if(%type $= "Rav") { %var = 2; %adminRequired = 1; }
         else if(%type $= "Lord") { %var = 3; %adminRequired = 1; }
         else if(%type $= "Dem") { %var = 4; %adminRequired = 1; }
         else if(%type $= "Rap") { %var = 5; %adminRequired = 1; }
         else if(%type $= "DLord") { %var = 6; %adminRequired = 2; }
         else if(%type $= "Yvex") { %var = 7; %adminRequired = 3; }
         else if(%type $= "LordRog") { %var = 8; %adminRequired = 3; }
         else if(%type $= "Shift") { %var = 9; %adminRequired = 1; }
         else if(%type $= "Summon") { %var = 10; %adminRequired = 2; }
         else if(%type $= "Snipe") { %var = 11; %adminRequired = 2; }
         else if(%type $= "ULord") { %var = 12; %adminRequired = 2; }
         else if(%type $= "VRav") { %var = 13; %adminRequired = 1; }
         else if(%type $= "Wraith") { %var = 15; %adminRequired = 1; }
         else if(%type $= "Rog") { %var = 16; %adminRequired = 2; }
         else {
            messageclient(%sender, 'MsgClient', '\c2Unknown Type, These Are Accepted:');
            messageclient(%sender, 'MsgClient', '\c2Norm, Rav, Lord, Dem, Rap, DLord, Snipe');
            messageclient(%sender, 'MsgClient', '\c2Yvex, LordRog, Shift, Summon, ULord, VRav');
            messageclient(%sender, 'MsgClient', '\c2Wraith, Rog');
            return 1; //stop here
         }
         //
         //Admin Level Check
         if(%adminRequired == 3 && !%sender.isDev && !%sender.isHost) {
            messageclient(%sender, 'MsgClient', "\c2Error: Admin Clearance For Level 3 Required For This Zombie Type");
            return 1;
         }
         else if(%adminRequired == 2 && !%sender.isSuperAdmin) {
            messageclient(%sender, 'MsgClient', "\c2Error: Admin Clearance For Level 2 Required For This Zombie Type");
            return 1;
         }
         %pos1 = %sender.player.getposition();
         %pos2 = "0 0 20";
         %Fpos = vectoradd(%pos1,%pos2);
         //*****
         makePersonZombie(%Fpos, %target, %var);
         return 1;
      
      //=======================================================================
      //=======================================================================
      default:
         return 0;
   }
}

addCMD("Admin", "ZCmds", "Usage: /ZCmds: Lists zombie commands.");
addCMD("Admin", "BuyZPack", "Usage: /BuyZPack: gives you a zombie spawner.");
addCMD("Admin", "SpawnZ", "Usage: /SpawnZ [Amount] [Type]: spawns zombies.");
addCMD("Admin", "KillZombies", "Usage: /KillZombies: kills all zombies.");
addCMD("Admin", "Cure", "Usage: /Cure [Target]: cures the zombie infection in a player.");
addCMD("Admin", "makeZ", "Usage: /makeZ [target] [Type]: makes player zombies.");

addCMD("Admin", "AdminCmds", "Usage: /AdminCmds: Lists all admin commands.");
addCMD("Admin", "Freeze", "Usage: /Freeze [name]: EMP Locks a player.");
addCMD("Admin", "passVote", "Usage: /passVote: passes a vote in progress.");
addCMD("Admin", "getPos", "Usage: /getPos: gets your current position on the map (XYZ).");
addCMD("Admin", "moveme", "Usage: /moveme [x] [y] [z]: moves you relative to your current position.");
addCMD("Admin", "moveto", "Usage: /moveto [x] [y] [z]: moves to the specified position.");
addCMD("Admin", "Kill", "Usage: /Kill [name]: Instantly kill a player.");
addCMD("Admin", "goto", "Usage: /goto [name]: teleports you to a player.");
addCMD("Admin", "summon", "Usage: /summon [name]: brings you a player.");
addCMD("Admin", "removePieces", "Usage: /removePieces [name]: deletes a player's deployables.");
addCMD("Admin", "ForcePieces", "Usage: /ForcePieces [target] [name]: gives a target's pieces to another.");
addCMD("Admin", "giveorphans", "Usage: /giveorphans [name]: gives all orphaned deployables to a player.");
addCMD("Admin", "MyName", "Usage: /MyName [name]: sets your name, you can use tags in this command, 'default' will reset your name.");
addCMD("Admin", "SetName", "Usage: /setname [target name] [new name]: Sets the name of a player, you can use tags in this command, 'default' resets it.");
addCMD("Admin", "cancelvote", "Usage: /cancelvote: instantly fail a current vote.");
addCMD("Admin", "A", "Usage: /A [message]: chat message to admins.");
addCMD("Admin", "bp", "Usage: /bp [message]: sends a bottom print to all players.");
addCMD("Admin", "cp", "Usage: /cp [message]: sends a center print to all players.");
addCMD("Admin", "confiscate", "Usage: /confiscate [SA Only - name]: removes your admin guns, SA - Removes your guns or the guns of other admins.");
addCMD("Admin", "gag", "Usage: /gag [name] [time or blank]: disables an annoying player's ability to talk.");
addCMD("Admin", "GetDBs", "Usage: /GetDBs: Lists all Item DB's, used for /giveGun.");
addCMD("Admin", "giveGun", "Usage: /GiveGun [name] [DB]: Gives an object to a player.");
addCMD("Admin", "TwoTeams", "Usage: /TwoTeams: Opens a second team, used for Team combat in Single Team Maps.");
addCMD("Admin", "slap", "Usage: /slap [name]: punish a player by slapping them down to the ground for 3 seconds.");
addCMD("Admin", "warn", "Usage: /warn [name]: warn a player for misconduct.");
