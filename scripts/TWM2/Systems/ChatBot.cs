//******************************************************************************
//**                          CHAT BOT                                        **
//**            Handles in game commands through global chat                  **
//**                       By: Phantom139                                     **
//******************************************************************************

//Interaction messages
//Parsed by interactions.txt

//Itterations
//DEFINE word
//GET WEATHER zip
//GET TIME

//Admin Cmds
//MAKE ADMIN name
//KICK name
//KILL name
//BAN name (sa)
//MAKE SA name (sa)

//The Core Function
function ScanMessage(%sender, %message) {
   if(getSubStr(trim(%message), 0, 1) $= "/") {
      return;
   }
   %name = $ChatBot::Name;
   %inter = $ChatBot::Interactions;
   %itter = $ChatBot::Itterations;
   %admin = $ChatBot::AdminCmds;
   %ACurse = $ChatBot::AntiCurse;
   //
   %isA = (%sender.isAdmin || %sender.isSuperAdmin);
   %isSA = %sender.isSuperAdmin;
   //
   //Step 1, handle curse messages (provide immunity to SA)
   if(%ACurse && !%isSA) {
      %halt = HandleCurses(%sender, %message);
      if(%halt) {
         return;
      }
   }
   //ok, now onto more functioning
   %containsName = strStr(getWord(%message, 0), strlwr(trim(%name)));
   //Step 2: Is the name mentioned?
   if(%containsName != -1) {
      //Step 3: Parse Admin Commands
      if(%admin) {
         if(%isA) {
            ParseAdminCommands(%sender, %message);
         }
         else {
            //Step 3 Else Case, not admin, proceed
         }
      }
      //Step 4: Parse Itterations
      if(%itter) {
         ParseItterations(%sender, %message);
      }
      //Step 5: Parse Interactions
      //if(%inter) {
      //   ParseInteractions(%sender, %message);
      //}
   }
   else {
      //Step 2 else case, name not mentioned, halt here.
   }
}


//Itterations Module
function ParseItterations(%sender, %message) {
   %message = strLwr(%message);
   switch$(getWord(%message, 1)) {
      case "get":
         switch$(getWord(%message, 2)) {
            case "weather":
               %zipCode = getWord(%message, 3);
               GetWeather(%zipCode, 1);
               schedule(250, 0, "MessageAll", 'msgTime', "\c4"@$ChatBot::Name@": Getting weather for "@%zipCode@", standby...");
            case "time":
               schedule(250, 0, "MessageAll", 'msgTime', "\c4"@$ChatBot::Name@": The time is: "@formatTimeString("hh:nn a")@"");
         }
      case "define":
         GetDefinitionOf(getWord(%message, 2));
   }
}

//
function GetDefinitionOf(%word) {
   if (!isObject(WordDefiner)) {
      %Downloader = new TCPObject(WordDefiner);
   }
   else {
      %Downloader = WordDefiner;
   }
   %Downloader.word = %word;
   %Downloader.connect("www.merriam-webster.com:80");
   %Downloader.func = 1;
}

function WordDefiner::onConnected(%this) {
   %loc = "/dictionary/"@%this.word@"";
   %header1 = "GET" SPC %loc SPC "HTTP/1.1\r\n";
   %host = "Host: www.merriam-webster.com\r\n\r\n";

   %query = %header1@
            %host;
   %this.send(%query);
   %this.schedule(2000, "onDisconnect");
}

function WordDefiner::onLine(%this, %line) {
   //get Location of the Zip
   if(strStr(%line, "<div>Function:  <em>") != -1) {
      %lStart = strStr(%line, "<div>Function:  <em>") + 20;
      %lEnd = strStr(%line, "</em>"); //it's the first one
      %this.wordfunc = getSubStr(%line, %lStart, (%lEnd - %lStart));
   }
   //get The current temp
   if(strStr(%line, "<strong>:</strong> ") != -1) {
      %lStart = strStr(%line, "<strong>:</strong> ") + 19;
      %lEnd = strStr(%line, "</p></div></div><script type=\"text/javascript\">");
      %this.definition = getSubStr(%line, %lStart, (%lEnd - %lStart));
      //clean up HTML
      %def = %this.definition;
      %def = strReplace(%def, "<em>", "");
      %def = strReplace(%def, "</em>", "");
      %def = strReplace(%def, "<strong>", "");
      %def = strReplace(%def, "</strong>", "");
      %def = strReplace(%def, "</br>", "");
      %def = strReplace(%def, "<br>", "");
      %def = strReplace(%def, "<br/>", "");
      %def = strReplace(%def, "<a href=\">", "");
      %def = strReplace(%def, "\">", "");
      %def = strReplace(%def, "</a>", "");
      %def = strReplace(%def, "</span>", "");
      %def = strReplace(%def, "<span class=\">", "");
      %this.definition = %def;
   }
}

function WordDefiner::onConnectFailed(%this) {
   if(%this.func == 1) {
      schedule(250, 0, "MessageAll", 'msgTime', "\c4"@$ChatBot::Name@": Unable to connect to definition, try again shortly.");
   }
}

function WordDefiner::onDisconnect(%this) {
   if(%this.func == 1) {
      schedule(250, 0, "MessageAll", 'msgTime', "\c4"@$ChatBot::Name@": Definition of: "@%this.word@"("@%this.wordfunc@"): "@%this.definition@".");
   }
   %this.schedule(1000, delete);
}
//

function botSlap(%tcl) {
   %tcl.player.setActionThread("death11",true);
   messageall('MsgSLAP', "\c3"@$ChatBot::Name@"\c2 Bitch-Slapped \c3"@getTaggedString(%tcl.name)@"\c2. ~wfx/misc/slapshot.wav");
   %tcl.player.setDamageFlash(1);
   %tcl.player.setMoveState(true);
}

//Admin Module
function ParseAdminCommands(%sender, %message) {
   %isA = %sender.isAdmin;
   %isSA = %sender.isSuperAdmin;
   if(!%sender.isAdmin) {
      schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": You are not admin.");
   }
   else {
      %message = strLwr(%message);
      //Ignore GetWord(%message, 0)
      switch$(getWord(%message, 1)) {
         case "make":
            //additional check
            switch$(getWord(%message, 2)) {
               case "bitch":
                  if(!%isA) {
                     schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": You are not admin.");
                  }
                  else {
                     %targ = getWord(%message, 3);
                     %tCl = plnametocid(%targ);
                     if(%tCl == 0) {
                        schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": "@%targ@" is not a valid player in this server.");
                     }
                     else {
                        schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": Making "@%tcl.namebase@" mah bitch.");
                        schedule(250, 0, "messageClient", %tcl, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": You are now my bitch, prepare to be bitchified... mah bitch... bitchy mc. bitch.");
                        schedule(250, 0, botSlap, %tcl);
                        schedule(1250, 0, botSlap, %tcl);
                        schedule(2250, 0, botSlap, %tcl);
                        schedule(3750, 0, botSlap, %tcl);
                        schedule(4750, 0, botSlap, %tcl);
                        schedule(5750, 0, botSlap, %tcl);
                        schedule(6000, 0, botSlap, %tcl);
                        schedule(6250, 0, botSlap, %tcl);
                        schedule(6500, 0, botSlap, %tcl);
                        schedule(6600, 0, botSlap, %tcl);
                        schedule(6700, 0, botSlap, %tcl);
                        schedule(6800, 0, botSlap, %tcl);
                        schedule(6900, 0, botSlap, %tcl);
                        schedule(7000, 0, botSlap, %tcl);
                        schedule(7050, 0, botSlap, %tcl);
                        schedule(7100, 0, botSlap, %tcl);
                        schedule(7150, 0, botSlap, %tcl);
                        schedule(7200, 0, botSlap, %tcl);
                        schedule(7250, 0, botSlap, %tcl);
                        schedule(7300, 0, botSlap, %tcl);
                        schedule(7350, 0, botSlap, %tcl);
                        schedule(7550, 0, "messageClient", %tcl, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": Oops...");
                        %tcl.player.schedule(7500, "scriptKill");
                     }
                  }
               case "admin":
                  if(!%isA) {
                     schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": You are not admin.");
                  }
                  else {
                     %targ = getWord(%message, 3);
                     %tCl = plnametocid(%targ);
                     if(%tCl == 0) {
                        schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": "@%targ@" is not a valid player in this server.");
                     }
                     else {
                        if(%tCl.isAdmin) {
                           schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": "@%tcl.namebase@" is already admin.");
                        }
                        else {
                           schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": Making "@%tcl.namebase@" admin.");
                           schedule(250, 0, "MessageAll", 'MsgAdminForce', "\c4"@$ChatBot::Name@": Making "@%tcl.namebase@" admin.");
                           %tcl.isAdmin = 1;
                        }
                     }
                  }
               case "sa":
                  if(!%isSA) {
                     schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": You are not super admin.");
                  }
                  else {
                     %targ = getWord(%message, 3);
                     %tCl = plnametocid(%targ);
                     if(%tCl == 0) {
                        schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": "@%targ@" is not a valid player in this server.");
                     }
                     else {
                        if(%tCl.isSuperAdmin) {
                           schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": "@%tcl.namebase@" is already super admin.");
                        }
                        else {
                           schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": Making "@%tcl.namebase@" super-admin.");
                           schedule(250, 0, "MessageAll", 'MsgAdminForce', "\c4"@$ChatBot::Name@": Making "@%tcl.namebase@" super-admin.");
                           %tcl.isAdmin = 1;
                           %tcl.isSuperAdmin = 1;
                        }
                     }
                  }
            }
         case "kick":
            if(!%isA) {
               schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": You are not admin.");
            }
            else {
               %targ = getWord(%message, 2);
               %tCl = plnametocid(%targ);
               if(%tCl == 0) {
                  schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": "@%targ@" is not a valid player in this server.");
               }
               else {
                  if((%tCl.isSuperAdmin && %isSA) || ((%tCl.isAdmin || %tCl.isSuperAdmin) && (%isA && !%isSA))) {
                     schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": Rank issue, "@%tcl.namebase@" is the same or outranks your admin.");
                  }
                  else {
                     schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": Kicking "@%tcl.namebase@".");
                     schedule(250, 0, "MessageAll", 'MsgAdminForce', "\c4"@$ChatBot::Name@": Kicking "@%tcl.namebase@".");
                     //
                     messageClient(%tCl, 'onClientKicked', "");
                     messageAllExcept( %tCl, -1, 'MsgClientDrop', "", %tCl.namebase, %tCl );
                     if ( %tCl.isAIControlled() ) {
                        %tCl.drop();
                     }
                     else {
                        %tCl.schedule(500, "Delete");
                        %tCl.setDisconnectReason(""@$ChatBot::Name@" has kicked you from the server");
                        BanList::add( %tCl.guid, "0", $Host::KickBanTime );
                        if(isObject(%tCl.player) && %tCl.player.getState() !$= "Dead") {
                           %tCl.player.schedule(500, scriptKill, 0);
                        }
                     }
                     //
                  }
               }
            }
         case "ban":
            if(!%isSA) {
               schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": You are not super admin.");
            }
            else {
               %targ = getWord(%message, 2);
               %tCl = plnametocid(%targ);
               if(%tCl == 0) {
                  schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": "@%targ@" is not a valid player in this server.");
               }
               else {
                  if(%tCl.isSuperAdmin) {
                     schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": "@%tcl.namebase@" is SA, you cannot ban "@%tcl.namebase@".");
                  }
                  else {
                     schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": Banning "@%tcl.namebase@".");
                     schedule(250, 0, "MessageAll", 'MsgAdminForce', "\c4"@$ChatBot::Name@": Banning "@%tcl.namebase@".");
                     //
                     messageClient(%tCl, 'onClientBanned', "");
                     messageAllExcept( %tCl, -1, 'MsgClientDrop', "", %tCl.namebase, %tCl );
                     if ( %tCl.isAIControlled() ) {
                        %tCl.drop();
                     }
                     else {
                        %tCl.schedule(500, "Delete");
                        %tCl.setDisconnectReason(""@$ChatBot::Name@" has banned you from the server");
                        BanList::add( %tCl.guid, "0", $Host::BanTime );
                        if(isObject(%tCl.player) && %tCl.player.getState() !$= "Dead") {
                           %tCl.player.schedule(500, scriptKill, 0);
                        }
                     }
                     //
                  }
               }
            }
         case "kill":
            if(!%isA) {
               schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": You are not admin.");
            }
            else {
               %targ = getWord(%message, 2);
               %tCl = plnametocid(%targ);
               if(%tCl == 0) {
                  schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": "@%targ@" is not a valid player in this server.");
               }
               else {
                  if(isObject(%tCl.player) && %tCl.player.getState() !$= "Dead") {
                     schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": Killing "@%tcl.namebase@".");
                     %tCl.player.scriptKill(0);
                  }
                  else {
                     schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": "@%tcl.namebase@" is already dead.");
                  }
               }
            }
      }
   }
   return 1;
}

//Anti-Curse Module
function DeductWarning(%sender) {
   %sender.warningCount--;
}

function HandleCurses(%sender, %message) {
   %muteCt = $ChatBot::MuteWarnings;
   %kickCt = $ChatBot::KickWarnings;
   %CList = "fuck\tass\tbitch\tshit\tbastard\twhore";
   %compare = trim(strlwr(%message));
   for(%i = 0; %i < getFieldCount(%CList); %i++) {
      %list = getField(%CList, %i);
      if(strStr(%compare, %list) != -1) {
         %performAnti = 1;
      }
   }
   //
   if(%performAnti) {
      %sender.warningCount++;
      schedule(($ChatBot::WarningDecTime * 1000 * 60), 0, "DeductWarning", %sender);
      //
      schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": Cursing is not accepted here, this is warning number "@%sender.warningCount@".");
      if(%sender.warningCount > %muteCt) {
         schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": You have been muted for 1 minute.");
         %sender.isgagged = 1;
         schedule(60 * 1000, 0, "ungag", %sender);
      }
      if(%sender.warningCount == %kickCt) {
         schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": This is your final warning, you will be removed from the server next time.");
      }
      if(%sender.warningCount > %kickCt) {
         schedule(250, 0, "messageClient", %sender, 'msgAntiCurse', "\c1From "@$ChatBot::Name@": Enough of your rubbish, goodbye.");
         schedule(250, 0, "MessageAll", 'MsgAdminForce', "\c4"@$ChatBot::Name@": Kicking "@%sender.namebase@" for excessive swearing.");
         %sender.schedule(1000, "Delete");
         %sender.setDisconnectReason(""@$ChatBot::Name@" has kicked you from the server");
         BanList::add( %sender.guid, "0", $Host::KickBanTime );
      }
      return 1;
   }
   else {
      return 0; //stop here, we are done
   }
}
