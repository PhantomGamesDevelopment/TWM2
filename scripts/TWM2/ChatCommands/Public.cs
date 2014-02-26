function parsePublicCommands(%sender, %command, %args) {
   switch$(strLwr(%command)) {
      //Help: Displays all avaliable chat commands
      case "help":
         messageclient(%sender, 'MsgClient', "\c5TWM2 Chat Commands.");
         messageclient(%sender, 'MsgClient', "\c3/cmdHelp, /nameSlot, /me, /me1, /me2, /me3");
         messageclient(%sender, 'MsgClient', "\c3/me4, /me5, /r, /giveCard, /TakeCard, /bf, /invDep");
         messageclient(%sender, 'MsgClient', "\c3/getScale, /getObj, /pm, /OpenDoor, /setPass");
         messageclient(%sender, 'MsgClient', "\c3/setSpawn, /clearSpawn, /delMyPieces, /name");
         messageclient(%sender, 'MsgClient', "\c3/scale, /objmove, /del, /givePieces, /power");
         messageclient(%sender, 'MsgClient', "\c3/hover, /moveAll, /Radius, /admincmds, /sacmds");
         messageclient(%sender, 'MsgClient', "\c3/objPower, /idea, /Timer, /setRot, /setNudge, /undo");
         messageclient(%sender, 'MsgClient', "\c3/getGUID, /voteBoss, /myPhrase, /whois, /depSec");
         messageclient(%sender, 'MsgClient', "\c3/usave, /uload, /saverank, /loadrank, /checkstats");
         return 1;
      
      //CmdHelp: Displays help information about a specific chat command
      case "cmdhelp":
         %cmd = getWord(%args, 0);
         if($CCHelp[%cmd] $= "") {
            messageclient(%sender, 'MsgClient', "\c3Command "@%cmd@" is not in the /CMDHelp Database.");
            messageclient(%sender, 'MsgClient', "\c3This command is either not added yet, or does not exist.");
            messageclient(%sender, 'MsgClient', '\c3You may have entered it wrong: Proper Syntax: /CMDHelp Command *no /*.');
         }
         else {
            messageclient(%sender, 'MsgClient', "\c2/"@%cmd@": "@$CCHelp[%cmd]@"");
         }
         return 1;

      //NameSlot: used for CSS, changes the display name of a building
      case "nameslot":
         %slot = getWord(%args,0);
         %name = getWords(%args,1);
         if($SaveFile::Save[%sender.guid,%slot] $= "") {
            messageclient(%sender, 'MsgClient', "\c2There is nothing saved in slot "@%slot@".");
            return 1;
         }
         if(%name $= "") {
            messageclient(%sender, 'MsgClient', "\c2Please Specify a Name.");
            return 1;
         }
         $SaveFile::SlotName[%sender.guid,%slot] = ""@%name@"";
         export( "$SaveFile::*", "prefs/ContentSave.cs", false );
         messageclient(%sender, 'MsgClient', "\c2Slot "@%slot@" Has Been Named "@%name@".");
         return 1;
         
      //me: Command serries for Role Playing, displays text under different \c# tags based on call
      case "me":
         messageall('MsgAdmin', "\c0"@deTag(getTaggedString(%sender.name)) SPC %args);
         return 1;

      case "me1":
         messageall('MsgAdmin', "\c1"@deTag(getTaggedString(%sender.name)) SPC %args);
         return 1;
         
      case "me2":
         messageall('MsgAdmin', "\c2"@deTag(getTaggedString(%sender.name)) SPC %args);
         return 1;

      case "me3":
         messageall('MsgAdmin', "\c3"@deTag(getTaggedString(%sender.name)) SPC %args);
         return 1;
         
      case "me4":
         messageall('MsgAdmin', "\c4"@deTag(getTaggedString(%sender.name)) SPC %args);
         return 1;
         
      case "me5":
         messageall('MsgAdmin', "\c5"@deTag(getTaggedString(%sender.name)) SPC %args);
         return 1;

      //r: radio static message used with \c3, good for RPs
      case "r":
         messageall("MsgStatic", "\c3[R]-"@deTag(getTaggedString(%sender.name))@": "@%args@"~wfx/misc/static.wav");
         return 1;
      
      //giveCard: used for card doors, this grants access to players
      case "givecard":
         %nametotest = getword(%args,0);
         %target = plnametocid(%nametotest);
         %level = getword(%args,1);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         if(%level == 1) {
            %target.haslev1[%sender.GUID] = 1;
            messageclient(%sender, 'MsgClient', "\c2Green Access Card Given to "@getTaggedString(%target.name)@".");
            messageclient(%target, 'MsgClient', "\c2"@getTaggedString(%sender.name)@" Gave you a Green Access Card.");
            return 1;
         }
         else if(%level == 2) {
            %target.haslev2[%sender.GUID] = 1;
            messageclient(%sender, 'MsgClient', "\c2Yellow Access Card Given to "@getTaggedString(%target.name)@".");
            messageclient(%target, 'MsgClient', "\c2"@getTaggedString(%sender.name)@" Gave you a Yellow Access Card.");
            return 1;
         }
         else if(%level == 3) {
            %target.haslev3[%sender.GUID] = 1;
            messageclient(%sender, 'MsgClient', "\c2Red Access Card Given to "@getTaggedString(%target.name)@".");
            messageclient(%target, 'MsgClient', "\c2"@getTaggedString(%sender.name)@" Gave you a Red Access Card.");
            return 1;
         }
         else {
            messageclient(%sender, 'MsgClient', '\c2Invalid Level, 1 - Green, 2 - Yellow, 3 - Red.');
            return 1;
         }
      
      //takeCard: same as /giveCard with the opposite effect of removing the cards instead of adding them
      case "takecard":
         %nametotest = getword(%args,0);
         %target = plnametocid(%nametotest);
         %level = getword(%args,1);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         if(%level == 1) {
            %target.haslev1[%sender.GUID] = 0;
            messageclient(%sender, 'MsgClient', "\c2Green Access Card Taken From "@getTaggedString(%target.name)@".");
            messageclient(%target, 'MsgClient', "\c2"@getTaggedString(%sender.name)@" Took Your Green Access Card.");
            return 1;
         }
         else if(%level == 2) {
            %target.haslev2[%sender.GUID] = 0;
            messageclient(%sender, 'MsgClient', "\c2Yellow Access Card Taken From "@getTaggedString(%target.name)@".");
            messageclient(%target, 'MsgClient', "\c2"@getTaggedString(%sender.name)@" Took Your Yellow Access Card.");
            return 1;
         }
         else if(%level == 3) {
            %target.haslev3[%sender.GUID] = 0;
            messageclient(%sender, 'MsgClient', "\c2Red Access Card Taken From "@getTaggedString(%target.name)@".");
            messageclient(%target, 'MsgClient', "\c2"@getTaggedString(%sender.name)@" Took Your Red Access Card.");
            return 1;
         }
         else {
            messageclient(%sender, 'MsgClient', '\c2Invalid Level, 1 - Green, 2 - Yellow, 3 - Red.');
            return 1;
         }
      
      //bf: Buy loadout favorites
      case "bf":
         if($Host::Purebuild == 1) {
   	        buyFavorites(%sender);
            return 1;
         }
         else if(!$Host::Purebuild && %sender.isSuperAdmin && $Host::AdminNoPureBF) {
            buyFavorites(%sender);
            return 1;
         }
         else {
            messageclient(%sender, 'MsgClient', "\c5Purebuild Is Off");
            return 1;
         }
      
      //invDep: sets the invincibility status of an object
      case "invdep":
         if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2You must have a player object.');
            return 1;
         }
         %pos        = %sender.player.getMuzzlePoint($WeaponSlot);
         %vec        = %sender.player.getMuzzleVector($WeaponSlot);
         %targetpos  = vectoradd(%pos, vectorscale(%vec, 100));
         %obj        = containerraycast(%pos, %targetpos, $typemasks::staticshapeobjecttype, %sender.player);
         %obj        = getword(%obj, 0);
         %dataBlock  = %obj.getDataBlock();
         %className  = %dataBlock.className;

         if (!isObject(%obj)) {
            messageclient(%sender, 'MsgClient', '\c5No Object in range.');
            return 1;
         }
         if (%obj.ownerguid != %sender.guid && !%sender.isSuperAdmin){
            messageclient(%sender, 'MsgClient', "\c2You do not own this.");
            return 1;
         }
         if (!Deployables.isMember(%obj)) {
            messageClient(%sender, 'MsgClient', "\c2That piece is part of the map and cannot be deleted.");
            return 1;
         }
         %obj.Invincible = !%obj.Invincible;
         messageclient(%sender, 'MsgClient', "\c2object invincibility toggled to "@%obj.Invincible@".");
         return 1;
      
      //getScale: returns the piecewise scale of an object
      case "getscale":
         if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2You must have a player object.');
            return 1;
         }
         %pos        = %sender.player.getMuzzlePoint($WeaponSlot);
         %vec        = %sender.player.getMuzzleVector($WeaponSlot);
         %targetpos  = vectoradd(%pos,vectorscale(%vec, 100));
         %obj        = containerraycast(%pos, %targetpos, $typemasks::staticshapeobjecttype, %sender.player);
         %obj        = getword(%obj, 0);
         %dataBlock  = %obj.getDataBlock();
         %className  = %dataBlock.className;
         %owner      = %obj.owner;
         if (!isObject(%obj)) {
            messageclient(%sender, 'MsgClient', '\c5No object in range.');
            return 1;
         }
         %scale = %obj.getRealSize();
         messageclient(%sender, 'MsgClient', "\c5This object's ("@%obj@") scale is "@%scale@".");
         return 1;
      
      //getObj: returns details about an object
      case "getobj":
         if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2You must have a player object.');
            return 1;
         }
         %pos        = %sender.player.getMuzzlePoint($WeaponSlot);
         %vec        = %sender.player.getMuzzleVector($WeaponSlot);
         %targetpos  = vectoradd(%pos,vectorscale(%vec,100));
         %obj        = containerraycast(%pos,%targetpos,$AllObjMask,%sender.player);
         %obj        = getword(%obj,0);
         %dataBlock  = %obj.getDataBlock();
         %className  = %dataBlock.className;
         %owner      = %obj.owner;
         if (!isObject(%obj)) {
            messageclient(%sender, 'MsgClient', '\c5No object in range.');
            return 1;
         }
         messageclient(%sender, 'MsgClient', "\c5ObjectID: ("@%obj@") DB: "@%obj.getDatablock().getName()@".");
         return 1;

      //pm: private messaging
      case "pm":
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         %message = getwords(%args, 1);
         if (%target==0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         messageclient(%target, 'MsgClient',"\c1From "@%sender.namebase@": "@%message);
         messageclient(%sender, 'MsgClient',"\c1To "@%target.namebase@": "@%message);
         echo("\c2Private Message from: "@%sender.namebase@" to: "@%target.namebase@"\c1 "@%message);
         return 1;

      //opendoor: used to open doors... obvious enough.
      case "opendoor":
         %pos        = %sender.player.getMuzzlePoint($WeaponSlot);
         %vec        = %sender.player.getMuzzleVector($WeaponSlot);
         %targetpos  = vectoradd(%pos,vectorscale(%vec, 100));
         %obj        = containerraycast(%pos, %targetpos, $typemasks::staticshapeobjecttype, %sender.player);
         %obj        = getword(%obj, 0);
         %dataBlock  = %obj.getDataBlock();
         %className  = %dataBlock.className;
         %owner      = %obj.owner;
         %obj.issliding = 0;
         if (%obj.Collision == true) {
            return 1;
         }
         if (%obj.canmove == false) {
            return 1;
         }
         if (%obj.isdoor != 1){
            messageclient(%sender, 'MsgClient', '\c5No door in range.');
            return 1;
         }
         if (!isObject(%obj)) {
            messageclient(%sender, 'MsgClient', '\c5No door in range.');
            return 1;
         }
         if (%obj.powercontrol == 1) {
            messageclient(%sender, 'MsgClient', '\c5This door is controlled by a power supply.');
            return 1;
         }
         %pass = %args;
         if (%obj.pw $= %pass) {
            if (%obj.toggletype ==1){
               if (%obj.moving $= "close" || %obj.moving $= "") {
                  schedule(10,0,"open",%obj);
                  return 1;
               }
               else if (%obj.moving $="open"){
                  schedule(10,0,"close",%obj);
                  return 1;
               }
            }
            else {
               schedule(10,0,"open",%obj);
               return 1;
            }
         }
         if (%obj.pw !$= %pass) {
            messageclient(%sender,'MsgClient',"\c2Password Denied.");
            return 1;
         }
      
      //setPass: set the password on a door
      case "setpass":
         %pos = %sender.player.getMuzzlePoint($WeaponSlot);
         %vec = %sender.player.getMuzzleVector($WeaponSlot);
         %targetpos = vectoradd(%pos, vectorscale(%vec, 100));
         %obj = containerraycast(%pos, %targetpos, $typemasks::staticshapeobjecttype, %sender.player);
         %obj = getword(%obj, 0);
         %dataBlock = %obj.getDataBlock();
         %className = %dataBlock.className;
         if (%classname !$= "door") {
            messageclient(%sender, 'MsgClient', '\c2No door in range.');
            return 1;
         }
         if (%obj.owner != %sender && %obj.owner !$="") {
            messageclient(%sender, 'MsgClient', '\c2You do not own this door.');
            return 1;
         }
         if (!isobject(%obj)) {
            messageclient(%sender, 'MsgClient', '\c2No door in range.');
            return 1;
         }
         if (%obj.Collision $= true) {
            messageclient(%sender, 'MsgClient', '\c2Collision doors can not have passwords.');
            return 1;
         }
         if(isObject(%obj) && %obj.owner == %sender) {
            %pw = getword(%args,0);
            %obj.pw = %pw;
            messageclient(%sender, 'MsgClient', '\c2Password set, password is %1.',%pw);
            return 1;
         }
      
      //setSpawn: sets the player's spawnpoint to a spawnpoint object
      case "setspawn":
         %pos = %sender.player.getMuzzlePoint($WeaponSlot);
         %vec = %sender.player.getMuzzleVector($WeaponSlot);
         %targetpos = vectoradd(%pos, vectorscale(%vec, 100));
         %obj = containerraycast(%pos, %targetpos, $typemasks::staticshapeobjecttype, %sender.player);
         %obj = getword(%obj, 0);
         %dataBlock = %obj.getDataBlock();
         if (!isobject(%obj) || %datablock.isSpawnpoint != 1) {
            messageclient(%sender, 'MsgClient', '\c2No spawn point in range.');
            return 1;
         }
         if ((%obj.owner != %sender) && (%obj.isPersonal == 1)) {
            messageclient(%sender, 'MsgClient', '\c2This is a personal spawn point, you cannot spawn here.');
            return 1;
         }
         if (%obj.team != %sender.team) {
            messageclient(%sender, 'MsgClient', '\c2This spawn point is not on your team.');
            return 1;
         }
         %sender.spawnpoint = %obj;
         messageclient(%sender, 'MsgClient', '\c2Spawn point set to this location, use /clearSpawn to reset');
         return 1;
      
      //clearSpawn: resets the spawnpoint of a player to the map default
      case "clearspawn":
         %sender.spawnpoint =0;
         messageclient(%sender, 'MsgClient', '\c2Spawn point set to default location.');
         return 1;
      
      //delMyPieces: remove all of the pieces belonging to the calling player
      case "delmypieces":
         messageclient(%sender, 'MsgClient', "\c2You have deleted your pieces.");
         %group = nameToID("MissionCleanup/Deployables");
         %count = %group.getCount();
         for (%i = 0; %i < %count; %i++) {
            %obj = %group.getObject(%i);
            if (%obj.getOwner() == %sender) {
               %random = getRandom(500, 5000);
               %obj.getDataBlock().schedule(%random, "disassemble", %sender.player, %obj);
            }
         }
         return 1;
      
      //name: rename an object
      case "name":
         %pos          = %sender.player.getMuzzlePoint($WeaponSlot);
         %vec          = %sender.player.getMuzzleVector($WeaponSlot);
         %targetpos    = vectoradd(%pos, vectorscale(%vec, 100));
         %obj          = containerraycast(%pos, %targetpos, $typemasks::staticshapeobjecttype, %sender.player);
         %obj          = getword(%obj, 0);
         %dataBlock    = %obj.getDataBlock();
         %className    = %dataBlock.className;

         if (%obj.getowner() != %sender && !%sender.isadmin){
            messageclient(%sender, 'MsgClient', "\c2You do not own this.");
            return 1;
         }
         if (%className $= "waypoint"){
            %obj.wp.schedule(10, "delete");
            %waypoint = new  (WayPoint)(){
               dataBlock        = WayPointMarker;
               position         = %obj.getPosition();
               name             = %args;
               scale            = "0.1 0.1 0.1";
               team             = %sender.team;
            };
            MissionCleanup.add(%waypoint);
            %obj.nametoset = %args;
            %obj.wp = %waypoint;
            return 1;
         }
         else {
            setTargetName(%Obj.target, addTaggedString(collapseEscape(%args)));
            %obj.nametoset = %args;
            return 1;
         }
      
      //scale: resize an object to any given coords
      case "scale":
         %pos         = %sender.player.getMuzzlePoint($WeaponSlot);
         %vec         = %sender.player.getMuzzleVector($WeaponSlot);
         %targetpos   = vectoradd(%pos, vectorscale(%vec, 100));
         %obj         = containerraycast(%pos, %targetpos, $typemasks::staticshapeobjecttype, %sender.player);
         %obj         = getword(%obj,0);
         if (%obj < 1) {
            return 1;
         }
         %objectScale = getwords(%obj.getScale(), 0, 2);
         %dataBlock   = %obj.getDataBlock();
         %name        = %dataBlock.getname();
         %className   = %dataBlock.className;
         %old         = %obj.getRealSize();
         if (!isObject(%obj)) {
            messageclient(%sender, 'MsgClient', '\c5No Object in range.');
            return 1;
         }
         if (!Deployables.isMember(%obj)) {
            messageClient(%sender, 'MsgClient', "\c2That piece is part of the map and cannot be resized.");
            return 1;
         }
         if (%obj.ownerguid != %sender.guid && (%sender.isAdmin !=1 && (%obj.ownerguid !$="" && %obj.powerfreq !$=""))){
            messageclient(%sender, 'MsgClient', "\c2You do not own this.");
            return 1;
         }

         %x = getWord(%args, 0);
         %y = getWord(%args, 1);
         %z = getWord(%args, 2);

         if((%x < 0.1 || %x > 500) && %x !$= "x") {
            messageclient(%sender, 'MsgClient', "\c2Error: X Size Not Specified or Invalid");
            return 1;
         }
         if((%y < 0.1 || %y > 500) && %y !$= "x") {
            messageclient(%sender, 'MsgClient', "\c2Error: Y Size Not Specified or Invalid");
            return 1;
         }
         if((%z < 0.1 || %z > 500) && %z !$= "x") {
            messageclient(%sender, 'MsgClient', "\c2Error: Z Size Not Specified or Invalid");
            return 1;
         }
         if(%x $= "x") {
            %x = getword(%obj.getScale(), 0);
         }
         if(%y $= "x") {
            %y = getword(%obj.getScale(), 1);
         }
         if(%z $= "x") {
            %z = getword(%obj.getScale(), 2);
         }
         if(%x <= 0 || %y <= 0 || %z <= 0) {
            messageclient(%sender, 'MsgClient', "\c2Error: Missing Side Value");
            return 1;
         }
         %set = %x SPC %y SPC %z;
         %fullscale = %set;
         //Redone 6/19/09
         if(%classname $= "spine" || %classname $= "mspine" || %classname $= "spine2" || %classname $= "wall" || %classname $= "wwall" || %classname $= "floor" || %classname $= "door") {
            %fullscale = VectorMultiply(%fullscale, "0.250 0.333333 2");   //thanks krash.
         }
         //APPLY
         //ensure sizes have not been modified
         %obj.setCloaked(true);
         %obj.schedule(150, "setCloaked", false);
         //
         messageclient(%sender, 'MsgClient', "\c2Rescaling "@%obj@", To "@%set@", From "@%old@".");
         %obj.SetRealSize(%fullscale);
         %obj.scale = %fullscale;
         %obj.settransform(%obj.gettransform());
         
         PostOperationCheck(%obj);
         return 1;
      
      //objMove: chat command based piece nudge
      case "objmove":
         if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2You must have a player object.');
            return 1;
         }
         %pos        = %sender.player.getMuzzlePoint($WeaponSlot);
         %vec        = %sender.player.getMuzzleVector($WeaponSlot);
         %targetpos  = vectoradd(%pos, vectorscale(%vec, 100));
         %obj        = containerraycast(%pos, %targetpos, $typemasks::staticshapeobjecttype, %sender.player);
         %obj        = getword(%obj,0);
         %dataBlock  = %obj.getDataBlock();
         %className  = %dataBlock.className;
         %objpos     = %obj.getposition();
         %move       = getwords(%args, 0);
         %moveto     = vectorAdd(%objpos, %move);

         if (!isObject(%obj)) {
            messageclient(%sender, 'MsgClient', '\c5No Object in range.');
            return 1;
         }
         if (%obj.ownerguid != %sender.guid && (!%sender.isAdmin && (%obj.ownerguid !$="" && %obj.powerfreq !$=""))){
            messageclient(%sender, 'MsgClient', "\c2You do not own this.");
            return 1;
         }
         if (!Deployables.isMember(%obj)) {
            messageClient(%sender, 'MsgClient', "\c2That piece is part of the map and cannot be moved.");
            return 1;
         }
         if (%obj.isdoor == 1) { //only move doors that are fully closed and not moving
            if (%obj.canmove == false){
               messageclient(%sender, 'MsgClient', "\c2You cannot move a door that is already moving.");
               return 1;
            }
            if(%obj.state !$= "closed" && %obj.state !$= ""){
               messageclient(%sender, 'MsgClient', "\c2You can only move fully closed doors.");
               return 1;
            }
         }

         if(mAbs(getWord(%args, 0)) > 5000 || mAbs(getWord(%args, 1)) > 5000 || (mAbs(getWord(%args, 2)) > 5000 && getWord(%args, 2) !$= "grd")) {
            messageclient(%sender, 'MsgClient', '\c2Invalid coordinate provided.');
            return 1;
         }
         
         %z = getWord(%args, 2);
         %obj.setCloaked(true);
         %obj.schedule(150, "setCloaked", false);
         %obj.setposition(%moveto);
         if(%z $= "grd") {
            %NewPos = %obj.getPosition();
            %x = getWord(%NewPos, 0);
            %y = getWord(%NewPos, 1);
            %z = GetTerrainHeight(%NewPos); //fun fun :)
            %goto = ""@%x@" "@%y@" "@%z@"";
            %obj.setposition(%goto);
         }
         adjustTrigger(%obj); //inv fix
         messageclient(%sender, 'MsgClient', "\c2object moved "@%move@".");
         
         PostOperationCheck(%obj);
         return 1;
      
      //del: deletes an object belonging to the player
      case "del":
         if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2You must have a player object.');
            return 1;
         }
         %pos        = %sender.player.getMuzzlePoint($WeaponSlot);
         %vec        = %sender.player.getMuzzleVector($WeaponSlot);
         %targetpos  = vectoradd(%pos, vectorscale(%vec, 100));
         %obj        = containerraycast(%pos, %targetpos, $typemasks::staticshapeobjecttype, %sender.player);
         %obj        = getword(%obj, 0);
         %dataBlock  = %obj.getDataBlock();
         %className  = %dataBlock.className;

         if (!isObject(%obj)) {
            messageclient(%sender, 'MsgClient', '\c5No Object in range.');
            return 1;
         }
         if (%obj.ownerguid != %sender.guid && !%sender.isSuperAdmin){
            messageclient(%sender, 'MsgClient', "\c2You do not own this.");
            return 1;
         }
         if (!Deployables.isMember(%obj)) {
            messageClient(%sender, 'MsgClient', "\c2That piece is part of the map and cannot be deleted.");
            return 1;
         }
         %obj.delete();
         messageclient(%sender, 'MsgClient', "\c2object deleted.");
         return 1;
      
      //givePieces: call a piece transfer to another player
      case "givepieces":
         %nametotest = getword(%args, 0);
         %target=plnametocid(%nametotest);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         //
         %target.recipientOf[%sender] = 1;
         %target.canAcceptDenyPieces = 1;
         %target.pieceTransferFrom = %sender;
         messageClient(%sender, 'msgCli', "\c3Offering your pieces to "@%target.namebase@".");
         messageClient(%target, 'msgCli', "\c3"@%sender.namebase@" wants to transfer his pieces to you.");
         messageClient(%target, 'msgCli', "\c3Press INSERT to accept or DELETE to deny.");
         //
         return 1;
      
      //power: sets the player's current power frequence
      case "power":
         %client = %sender.player;
         if(%args > 150) {
            %client.powerfreq = 1;
            messageclient(%sender, 'MsgClient', "\c5Max Freq 150");
            messageclient(%sender, 'MsgClient', "\c2Setting To Freq 1");
            return 1;
         }
         %sender.player.powerFreq = %args;
         messageclient(%sender, 'MsgClient', "\c2Setting To Freq "@%args@".");
         return 1;
      
      //hover: used to create a moving hoverpad for a player
      case "hover":
         if (!IsObject(%Sender.player)){
            messageclient(%sender, 'MsgClient', '\c5No player object.');
            return 1;
         }
         if (!$host::Purebuild){
            messageclient(%sender, 'MsgClient', '\c5Purebuild is off.');
            return 1;
         }
         if (%Sender.ishovering == 0){
            %Pad = new StaticShape() {
               dataBlock = DeployedSpine;
               scale = ".3 .3 1";
               position = "0 0 0";
            };
            %Pad.setCloaked(true);
            %Pad.setowner(%Sender);
            %Pad.isHoverPad = 1;
            %Sender.HoverPad = %Pad;
            %Sender.ishovering = 1;
            Hover(%Sender);
            messageclient(%sender, 'MsgClient', '\c5Now hovering...');
            return 1;
         }
         else {
            %Sender.ishovering = 0;
            messageclient(%sender, 'MsgClient', '\c5Stopped hovering.');
            %Sender.HoverPad.delete();
            return 1;
         }
      
      //moveAll: moves every piece owned by the player some coordinate value
      case "moveall":
         if(%sender.cantMoveAll) {
            messageclient(%sender, 'MsgClient', "\c5You have only recently moved all deployables.");
            return 1;
         }
         %x = getWord(%args, 0);
         %y = getWord(%args, 1);
         %z = getWord(%args, 2);
         if(%x $= "" || (%x > 5000 && !%sender.isAdmin)) {
            messageclient(%sender, 'MsgClient', "\c5Missing or Invalid 'X' Variable.");
            return 1;
         }
         if(%y $= "" || (%y > 5000 && !%sender.isAdmin)) {
            messageclient(%sender, 'MsgClient', "\c5Missing or Invalid 'Y' Variable.");
            return 1;
         }
         if(%z $= "" || (%z > 5000 && !%sender.isAdmin)) {
            messageclient(%sender, 'MsgClient', "\c5Missing or Invalid 'Z' Variable.");
            return 1;
         }
         %move = %x SPC %y SPC %z;
         %group = nameToID("MissionCleanup/Deployables");
         %count = %group.getCount();
         for (%i = 0; %i < %count; %i++) {
            %obj = %group.getObject(%i);
            if (%obj.getOwner() == %sender) {
               %newPos = vectorAdd(%obj.getPosition(), %move);
               %obj.setPosition(%newPos);
   	           adjustTrigger(%obj);
               
               PostOperationCheck(%obj);
            }
         }
         schedule(30000,0,eval, ""@%sender@".cantMoveAll = 0;");
         %sender.cantMoveAll = 1;
         messageclient(%sender, 'MsgClient', "\c5Moving your pieces "@%move@".");
         return 1;
      
      //radius: sets the power radius on a generator, solar panel, or a switch
      case "radius":
         if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2You must have a player object.');
            return 1;
         }
         %pos         = %sender.player.getMuzzlePoint($WeaponSlot);
         %vec         = %sender.player.getMuzzleVector($WeaponSlot);
         %targetpos   = vectoradd(%pos, vectorscale(%vec, 100));
         %obj         = containerraycast(%pos, %targetpos, $typemasks::staticshapeobjecttype, %sender.player);
         %obj         = getword(%obj, 0);
         %dataBlock   = %obj.getDataBlock();
         %name        = %dataBlock.getname();
         if (%obj < 1) {
            return 1;
         }
         %rad         = getword(%args, 0);
         %obj.setCloaked(true);
         %obj.schedule(150, "setCloaked", false);
         %obj.switchRadius = %rad;
         messageclient(%sender, 'MsgClient', "\c5Radius Set to "@%rad@".");
         return 1;
      
      //objPower: set the power frequency a specific object uses
      case "objpower":
         if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2You must have a player object.');
            return 1;
         }
         %pos        = %sender.player.getMuzzlePoint($WeaponSlot);
         %vec        = %sender.player.getMuzzleVector($WeaponSlot);
         %targetpos  = vectoradd(%pos, vectorscale(%vec, 100));
         %obj        = containerraycast(%pos, %targetpos, $typemasks::staticshapeobjecttype, %sender.player);
         %obj        = getword(%obj, 0);
         %dataBlock  = %obj.getDataBlock();
         %className  = %dataBlock.className;
         %owner      = %obj.GetOwner();
         if (!isObject(%obj)) {
            messageclient(%sender, 'MsgClient', '\c5No object in range.');
            return 1;
         }
         if(%owner != %sender) {
            messageclient(%sender, 'MsgClient', '\c5You do not own this.');
            return 1;
         }
         %freq = getWord(%args, 0);
         if(%freq < 0 || %freq > 150) {
            messageclient(%sender, 'MsgClient', '\c5Invalid Power Freq.');
            return 1;
         }
         %obj.setCloaked(true);
         %obj.schedule(300, "SetCloaked", false);
         %obj.powerFreq = %freq;
         checkPowerObject(%obj);
         messageclient(%sender, 'MsgClient', "\c5Object "@%obj@", power Freq Set To "@%freq@".");
         return 1;
      
      //timer: used by timed switches, sets the turn off/on time for these objects
      case "timer":
         if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
            messageclient(%sender, 'MsgClient', '\c2You must have a player object.');
            return 1;
         }
         %pos        = %sender.player.getMuzzlePoint($WeaponSlot);
         %vec        = %sender.player.getMuzzleVector($WeaponSlot);
         %targetpos  = vectoradd(%pos, vectorscale(%vec, 100));
         %obj        = containerraycast(%pos, %targetpos, $typemasks::staticshapeobjecttype, %sender.player);
         %obj        = getword(%obj, 0);
         %dataBlock  = %obj.getDataBlock();
         %className  = %dataBlock.className;
         %owner      = %obj.GetOwner();
         if (!isObject(%obj)) {
            messageclient(%sender, 'MsgClient', '\c5No object in range.');
            return 1;
         }
         if(%owner != %sender) {
            messageclient(%sender, 'MsgClient', '\c5You do not own this.');
            return 1;
         }
         if(%dataBlock.getName() !$= "DeployedSwitch") {
            messageclient(%sender, 'MsgClient', '\c5Object is not a switch.');
            return 1;
         }
         %time = getWord(%args, 0);
         if(%time < 1.5) {
            messageclient(%sender, 'MsgClient', '\c5Time must be larger than 1.5 seconds.');
            return 1;
         }
         %obj.SwitchTimer = %time * 1000;
         messageclient(%sender, 'MsgClient', "\c5Switch "@%obj@", time delay Set To "@%time@" seconds.");
         return 1;
      
      //setRot: set the rotate angle on the construction tool
      case "setrot":
         if(%args $= "" || %args < 0 || %args > 360) {
            %sender.RotateAngle = 22.5;
            messageclient(%sender, 'MsgClient', "\c2Rotate Angle Reset.");
            return 1;
         }
         messageclient(%sender, 'MsgClient', "\c2Rotate Angle Set To \c3"@%args@"\c2.");
         %sender.RotateAngle = %args;
         return 1;
      
      //setNudge: set's the power of the Nudge function on MIST
      case "setnudge":
         if(%args $= "" || (%args < 0) || %args > 100) {
            %sender.MoveSetting = 0.1;
            MessageClient(%sender, 'MsgClient', "\c2Move Tool Nudge Set To Default (0.1).");
            return 1;
         }
         %sender.MoveSetting = %args;
         MessageClient(%sender, 'MsgClient', "\c2Move Tool Nudge Set To "@%args@".");
         return 1;
      
      //undo: un-do the previous construction action.
      case "undo":
         messageClient(%sender, 'msgClient', "\c3Undoing previous action.");
         %sender.undoLastConstructionAction();
         return 1;
      
      //getGUID: returns the player's GUID # to them
      case "getguid":
         MessageClient(%sender, 'MsgClient', "\c2Your GUID is: \c3"@%sender.guid@"\c2.");
         return 1;

      //voteBoss: start a vote to spawn a boss
      case "voteboss":
         if(!$TWM2::AllowBossVotes) {
             messageclient(%sender, 'MsgClient', '\c2The server host has disabled boss votes.');
             return 1;
         }
         if($TWM::PlayingHorde || $TWM::PlayingHelljump) {
            messageclient(%sender, 'MsgClient', '\c2No bosses allowed in horde or helljump.');
            return 1;
         }
         if($TWM2::BossAllowTimer != 0) {
            %min = mFloor($TWM2::BossAllowTimer / 60);
            %sec = $TWM2::BossAllowTimer % 60;
            if(%sec < 10) {
               %sec = "0"@%sec;
            }
            messageclient(%sender, 'MsgClient', "\c2Boss Votes Not allowed for another "@%min@":"@%sec@"");
            return 1;
         }
         %Boss = getWord(%args, 0);
         if (!isObject(RndCli())) {
            messageclient(%sender, 'MsgClient', '\c2No Players seem to be spawned..');
            return 1;
         }
         else if (!%sender.canvote) {
            messageclient(%sender, 'MsgClient', '\c2You cannot vote yet.');
            return 1;
         }
         else if($TWM2::BossGoing) {
            messageclient(%sender, 'MsgClient', '\c2A Boss Is Already Spawned.');
            return 1;
         }
         else if (!isBoss(strlwr(%Boss))) {
            messageclient(%sender, 'MsgClient', '\c2Invalid Boss Name.');
            messageclient(%sender, 'MsgClient', '\c2Bosses: Yvex, CnlWindshear, GOL, GOF, Stormrider, DAVardison.');
            messageclient(%sender, 'MsgClient', '\c2GenVeg, LordRog, Insignia, Trebor, Vardison, ShadeLord.');
            return 1;
         }
         else {
            if ( Game.scheduleVote !$= "" ) {
               messageclient(%sender, 'MsgClient', '\c2A Vote is in progress..');
               return 1;
            }
            %BossFName = BossFullname(%Boss); //get the boss' full name
            for ( %idx = 0; %idx < ClientGroup.getCount(); %idx++ ) {
               %cl = ClientGroup.getObject( %idx );
               if ( !%cl.isAIControlled() ) {
	              messageClient( %cl, 'VoteStarted', '\c2%1 wants to start boss \c1%2\c2!', %sender.name, %BossFName);
                  %clientsVoting++;
               }
            }
            for ( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ ) {
               %cl = ClientGroup.getObject( %clientIndex );
               if ( !%cl.isAIControlled() ) {
                  messageClient(%cl, 'openVoteHud', "", %clientsVoting, ($Host::VotePassPercent / 100));
               }
            }
            $TWM2::BossAllowTimer = 20 * 60; //60 sec * 60 = 60 minutes, or 1 hour
            schedule(1000, 0, "LowerBossAllowTime");
            clearVotes();
            %sender.canVote = false;
            schedule(20*1000, 0, resetVotePrivs, %sender);
            Game.voteType = "BossVote";
            Game.BVoteboss = %Boss;
            Game.scheduleVote = schedule( ($Host::VoteTime * 1000), 0, "BossVoteEval", %Boss, false);
            %sender.vote = true;
            messageAll('addYesVote', "");
            return 1;
         }
      
      //myPhrase: sets the players custom phrase as shown on F2 and PGD Connect
      case "myphrase":
         %clientController = %sender.TWM2Core;
         %clientController.phrase = %args;
         messageClient(%sender, 'msgClient', "\c5TWM2: You have set your phrase to: "@$Rank::Phrase[%sender.GUID]@"");
         updateClientRank(%sender);
         UpdateRankFile(%sender);
         return 1;
      
      //whoIs: displays player information given their auth info
      case "whois":
         if (!strlen(trim(%args))) {
            messageClient(%sender, 'NoArgs', "\c2No Name/GUID?");
         }
         else {
            %reqcl = plnametocid(%args);
            if (%reqcl == 0) {
               //search through clients and look for a matching guid
               %isguid = 1;
               %count = ClientGroup.getCount();
               for (%i = 0; %i < %count; %i++) {
                  %c = ClientGroup.getObject(%i);
                  if (%c.guid == %args) {
                     %reqcl = %c;
                     break;
                  }
               }
               if (%reqcl == 0) {
                  messageClient(%sender, 'NoCl', "\c2No Client was found with that name/guid.");
                  return 1;
               }
            }
            %auth     = %reqcl.getAuthInfo();
            %name     = stripChars(detag(getTaggedString(%reqcl.name)), "\cp\co\c6\c7\c8\c9");
            %namebase = stripChars(detag(%reqcl.namebase),"\cp\co\c6\c7\c8\c9" );
            %realname = stripChars(detag(getField(%auth,0)), "\cp\co\c6\c7\c8\c9");
            %guid     = %reqcl.guid;
            %lastjoin = %reqcl.joinedtime;
            %ip       = strReplace(%reqcl.getAddress(), "IP:", "");
            %ip       = getSubStr(%ip, 0, strPos(%ip, ":"));
            %admin    = %reqcl.isAdmin + %reqcl.isSuperadmin;

            %isastr   = (%admin == 2 ? "is a Super Admin" : (%admin == 0 ? "isn't an Admin" : "is an Admin"));
            %smurf    = (strcmp(%name, %realname) == 0 ? 0 : 1);
            %nstr     = (%smurf == 1 ? "("@%realname@")" : "");
            if (%isguid) {
               %nstr = trim(%nstr) SPC "is" SPC %guid;
            }
            messageClient(%sender, 'WhoisReply', "\c2"@%name SPC %nstr);
            messageClient(%sender, 'WhoisReply', "\c2\t" SPC %name SPC "is"  SPC (%smurf ? "not a Smurf" : "a Smurf"));
            messageClient(%sender, 'WhoisReply', "\c2\t" SPC %name @   "'s GUID is" SPC %guid);
            messageClient(%sender, 'WhoisReply', "\c2\t" SPC %name SPC "is connecting from" SPC %ip);
            messageClient(%sender, 'WhoisReply', "\c2\t" SPC %name SPC (%reqcl.isAIControlled() ? "is" : "isn't") SPC "a bot");
            messageClient(%sender, 'WhoisReply', "\c2\t" SPC %name SPC (%reqcl.isPGDConnected() ? "is" : "isn't") SPC "PGD Connected");
            messageClient(%sender, 'WhoisReply', "\c2\t" SPC %name SPC "last connected on" SPC %lastjoin);
            messageClient(%sender, 'WhoisReply', "\c2\t" SPC %name SPC %isastr);
         }
         return 1;

       //depSec: toggles permission to build on or near a players pieces
      case "depsec":
         %statString = %sender.pieceSecured ? "are no longer" : "are now";
         %sender.pieceSecured = !%sender.pieceSecured;
         messageClient(%sender, 'msgClient', "\c3Deploy rights on your pieces "@%statString@" secured.");
         return 1;
      
      //uSave: univerally save a building in a CSS slot
      case "usave":
         if(!%sender.IsPGDConnected()) {
            messageClient(%client, 'msgPGDRequired', "\c5PGD: PGD Connect account required to perform this action.");
            return 1;
         }
         else {
            echo("Client:" SPC %sender.namebase SPC "requests universal save.");
            %file = strReplace(%args, ".cs", "");
            %file = %file @ ".cs";
            %file = "Buildings/Admin/"@%sender.guid@"/" @ %file;
            if(isFile(%file)) {
               MessageAll('MsgAdminForce', "\c3"@%sender.namebase@" is universally saving a building.");
               Univ_ServerConnect(%sender, %file, "Save");
            }
            else {
               messageClient(%client, 'msgPGDRequired', "\c5PGD: That slot/file does not exist");
            }
            return 1;
         }
      
      //uLoad: load a universally saved building
      case "uload":
         if(!%sender.IsPGDConnected()) {
            messageClient(%sender, 'msgPGDRequired', "\c5PGD: PGD Connect account required to perform this action.");
            return 1;
         }
         if(%sender.cantLoad) {
            messageClient(%sender, 'msgPGDRequired', "\c5PGD: You have only recently loaded.");
            return 1;
         }
         $SaveTime::TimeLeft[%sender.guid, "Load"] = $TWM::CSSTimeLoad*60; //5 mins
         %sender.cantLoad = 1;
         schedule(1,0,"ResetLoad",%sender);
         %args = strReplace(%args, ".cs", "");
         LoadUniversalBuilding(%sender, %args);
         return 1;
      
      //saveRank: save your rank to PGD
      case "saverank":
         if(!%sender.canSaveRank) {
            messageClient(%sender, 'MsgClient', "\c5PGD: You have only recently saved your rank.");
            return 1;
         }
         if(!%sender.IsPGDConnected()) {
            messageClient(%sender, 'msgPGDRequired', "\c5PGD: PGD Connect account required to perform this action.");
            return;
         }
         if($IsAuthed $= false) {
            messageClient(%sender, 'msgPGDRequired', "\c5PGD: This is a Satellite Server, only core servers can save ranks.");
            return;
         }
         SaveClientFile(%sender);
         PrepareUpload(%sender);
         %sender.canSaveRank = 0;
         schedule(60000 * 5, 0, "eval", ""@%sender@".canSaveRank = 1;");
         return 1;
      
      //loadRank: load your rank from PGD
      case "loadrank":
         if(!%sender.canLoadRank) {
            messageClient(%sender, 'MsgClient', "\c5PGD: You have only recently re-loaded your rank.");
            return 1;
         }
         if(!%sender.IsPGDConnected()) {
            messageClient(%sender, 'msgPGDRequired', "\c5PGD: PGD Connect account required to perform this action.");
            return;
         }
         LoadUniversalRank(%sender);
         %sender.canLoadRank = 0;
         schedule(60000 * 5, 0, "eval", ""@%sender@".canLoadRank = 1;");
         return 1;

      //checkStats: check the current rank information on a player
      case "checkstats":
         %clientController = %sender.TWM2Core;
         %todaysDate = sha1sum(formattimestring("yymmdd"));
         if(%args $= "") {
            if(%clientController.officer $= "") {
               %clientController.officer = 0;
            }
            %name = %sender.NameBase;
            %Rank = ""@$Prestige::Name[%clientController.officer]@""@%clientController.rank@"";
            %Stats = getCurrentEXP(%sender);
            for(%i = $Rank::RankCount; %i >= 0; %i--){
               if(%stats >= $Ranks::MinPoints[%i]){
                  %nextrank = ""@$Prestige::Name[%clientController.officer]@""@$Ranks::NewRank[(%i + 1)]@"";
                  %nextrankXP = $Ranks::MinPoints[(%i + 1)];
                  %i = 0;
               }
            }
            if(%Stats >= $Ranks::MinPoints[$Rank::RankCount]) {
               messageClient(%sender, 'MsgClient', "\c2Your Rank is "@%Rank@", You Currently Have "@%stats@" XP, and you have gained "@%clientController.xpGain[%todaysDate]@" EXP today.");
               return 1;
            }
            else {
               messageClient(%sender, 'MsgClient', "\c2Your Rank is "@%Rank@", You Currently Have "@%stats@" XP, and you have gained "@%clientController.xpGain[%todaysDate]@" EXP today. Your next rank is "@%nextrank@" and you need "@(%nextrankXP - %stats)@" XP.");
               return 1;
            }
         }
         else {
            %nametotest = getword(%args, 0);
            %target = plnametocid(%nametotest);
            if (%target==0) {
               messageclient(%sender, 'MsgClient', '\c2No such player.');
               return 1;
            }
            //
            %targetController = %target.TWM2Core;
            if(%targetController.officer $= "") {
               %targetController.officer = 0;
            }
            %Rank = ""@$Prestige::Name[%targetController.officer]@""@%targetController.rank@"";
            %Stats = getCurrentEXP(%target);
            messageClient(%sender, 'MsgClient', "\c2"@%target.namebase@"'s Rank is "@%Rank@" and "@%target.namebase@"'s XP is "@%stats@".");
            return 1;
         }
         
      //setEmail: used for the PGD IGC interface
      case "setemail":
         if(!isSet(%args)) {
            return 1;
         }
         %sender.emailSet = %args;
         messageClient(%sender, 'msgSent', "\c3SERVER: Email set to "@%args@"");
         return 1;
         
      case "msset":
         %pos        = %sender.player.getMuzzlePoint($WeaponSlot);
         %vec        = %sender.player.getMuzzleVector($WeaponSlot);
         %targetpos  = vectoradd(%pos, vectorscale(%vec, 100));
         %obj        = containerraycast(%pos, %targetpos, $typemasks::staticshapeobjecttype, %sender.player);
         %obj        = getword(%obj,0);
         %dataBlock  = %obj.getDataBlock().getName();
         %className  = %dataBlock.className;
         %owner      = %obj.owner;
         if (!isobject(%obj)) {
            messageclient(%sender, 'MsgClient', '\c5No object in range.');
            return 1;
         }
         if(%obj.getOwner() != %sender) {
            messageclient(%sender, 'MsgClient', '\c5Not yours.');
            return 1;
         }
         if(%dataBlock !$= "DeployedMedalSeal") {
            messageclient(%sender, 'MsgClient', '\c5Not a Medal Seal.');
            return 1;
         }
         //
         %arg1 = strLwr(getWord(%args, 0));
         %arg2 = strLwr(getWord(%args, 1));
         switch$(%arg1) {
            case "set":
               switch$(%arg2) {
                  case "challengreq":
                     %medal = getWord(%args, 2);
                     %obj.targetNeeds = %medal;
                     messageclient(%sender, 'MsgClient', "\c5Requirement Set: "@%medal@"");
                  case "notmetmsg":
                     %msg = getWords(%args, 2);
                     %obj.targetNeedsInvalid = %msg;
                     messageclient(%sender, 'MsgClient', "\c5Message Set: "@%msg@"");
                  default:
                     messageclient(%sender, 'MsgClient', '\c5Unknown Second Argument - notmetmsg/challengreq.');
                     return 1;
               }
            default:
               messageclient(%sender, 'MsgClient', '\c5Unknown First Argument - set.');
               return 1;
         }
         return 1;
         
      //None Matching Case:
      default:
         return 0;
   }
}

addCMD("Public", "Help", "Usage: /help: displays mod help commands.");
addCMD("Public", "Whois", "Usage: /Whois [name or guid]: displays information about a player.");
addCMD("Public", "MyPhrase", "Usage: /MyPhrase [phrase]: sets your personal phrase for your rank card.");
addCMD("Public", "VoteBoss", "Usage: /VoteBoss [name]: votes to start a boss.");
addCMD("Public", "getGUID", "Usage: /getGUID: gives you your GUID.");
addCMD("Public", "SetNudge", "Usage: /SetNudge [Val]: sets your move tool's move snap.");
addCMD("Public", "SetRot", "Usage: /SetRot [Angle]: set your construction tool's rotation angle.");
addCMD("Public", "CMDHelp", "Usage: /CMDHelp [Command]: tells you about a command.");
addCMD("Public", "NameSlot", "Usage: /NameSlot [Save Slot] [Name]: Names a CSS Slot.");
addCMD("Public", "me", "Usage: /me [Text]: Sends a message under the \c0 Tag.");
addCMD("Public", "me1", "Usage: /me1 [Text]: Sends a message under the \c1 Tag.");
addCMD("Public", "me2", "Usage: /me2 [Text]: Sends a message under the \c2 Tag.");
addCMD("Public", "me3", "Usage: /me3 [Text]: Sends a message under the \c3 Tag.");
addCMD("Public", "me4", "Usage: /me4 [Text]: Sends a message under the \c4 Tag.");
addCMD("Public", "me5", "Usage: /me5 [Text]: Sends a message under the \c5 Tag.");
addCMD("Public", "r", "Usage: /r [Text]: Sends a radio message with the \c3 tag, good for RPs.");
addCMD("Public", "givecard", "Usage: /givecard [name] [Level# 1,2,or 3]: Gives a player a card for leveled doors.");
addCMD("Public", "takecard", "Usage: /takecard [name] [Level# 1,2,or 3]: remove a player's card for leveled doors.");
addCMD("Public", "GetScale", "Usage: /GetScale: Displays the size of an object.");
addCMD("Public", "getobj", "Usage: /getobj: Displays object information.");
addCMD("Public", "pm", "Usage: /pm [name] [message]: sends a private message to clients, ![name] [message] is another way.");
addCMD("Public", "opendoor", "Usage: /opendoor [pass ?]: opens a door.");
addCMD("Public", "setpass", "Usage: /setpass [pass]: sets a password on Normal & Toggle Doors.");
addCMD("Public", "bf", "Usage: /bf: gives you your current loadout in the inventory, purebuild must be on.");
addCMD("Public", "setSpawn", "Usage: /setSpawn: sets your spawn point.");
addCMD("Public", "clearspawn", "Usage: /clearspawn: sets you to spawn at the default location.");
addCMD("Public", "DelMyPieces", "Usage: /Delmypieces: deletes all objects you have deployed.");
addCMD("Public", "name", "Usage: /name [name]: names an object, you can use tags in this command.");
addCMD("Public", "scale", "Usage: /scale [# or x] [# or x] [# or x]: Sets the size of an object, 'x' will leave the current size on that axis.");
addCMD("Public", "objmove", "Usage: /objmove [#] [#] [#|Grd]: moves an object.");
addCMD("Public", "del", "Usage: /del: Deletes an object you own.");
addCMD("Public", "invDep", "Usage: /invDep: Toggles Object Invincibility.");
addCMD("Public", "power", "Usage: /power [#]: sets your current power frequency.");
addCMD("Public", "givePieces", "Usage: /givePieces [name]: gives your pieces to another player.");
addCMD("Public", "hover", "Usage: /hover: allows you to maintain your position in mid air.");
addCMD("Public", "radius", "Usage: /radius [#]: sets the radius of a switch.");
addCMD("Public", "MoveAll", "Usage: /MoveAll [X] [Y] [Z]: Moves All You Your Pieces");
addCMD("Public", "ObjPower", "Usage: /ObjPower [freq]: sets the power freq of an object.");
addCMD("Public", "Timer", "Usage: /Timer [time > 1.5]: sets the switch delay on a switch.");
addCMD("Public", "DepSec", "Usage: /DepSec: secure deploy rights on your pieces.");
addCMD("Public", "undo", "Usage: /undo: undo your last construction action.");
addCMD("Public", "checkStats", "Usage: /checkStats [name or blank]: check the current rank info on a player.");
addCMD("Public", "uSave", "Usage: /uSave [slot #]: Save a building on the PGD server for loading in other servers.");
addCMD("Public", "uLoad", "Usage: /uLoad [slot #]: Load a universally saved building.");
addCMD("Public", "LoadRank", "Usage: /LoadRank: load your universal rank if it failed.");
addCMD("Public", "SaveRank", "Usage: /SaveRank: save your universal rank if it failed.");
addCMD("Public", "setEmail", "Usage: /setEmail [email]: set email for PGD IGC.");
addCMD("Public", "msSet", "Usage: /msSet [set] [args]: Medal Seal setup.");
