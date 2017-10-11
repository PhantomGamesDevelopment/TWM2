function parseSuperAdminCommands(%sender, %command, %args) {
   switch$(strLwr(%command)) {
      //sacmds: list all super admin commands
      case "sacmds":

         return 1;
         
      //tktoggle: enable/disable Friendly Fire EXP gain
      case "tktoggle":
         if(!%sender.isSuperAdmin) {
            return 3;
         }
         if($TWM2::FFAMode) {
            $TWM2::FFAMode = 0;
            MessageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c2 Disabled FFA Mode.");
         }
         else {
            $TWM2::FFAMode = 1;
            MessageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c2 Enabled FFA Mode.");
         }
         return 1;
      
      //sa: SA chat only
      case "sa":
         if (!%sender.issuperadmin) {
            return 3;
         }
         %count = ClientGroup.getCount();
         for (%i = 0; %i < %count; %i++) {
            %cl = ClientGroup.getObject( %i );
            if(%cl.issuperadmin) {
               messageclient(%cl, 'MsgClient', "\c5[SA]"@%sender.namebase@" : \c4"@%args@"");
            }
         }
         return 1;
      
      //makeSA: obvious enough
      case "makesa":
         if (!%sender.issuperadmin) {
            return 3;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         if (%target==0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         %target.isAdmin = true;
         %target.isSuperAdmin = true;
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Made \c3"@ %target.namebase@"\c2 SA.");
         messageclient(%target, 'MsgClient', '\c5 You are now a Super-Admin');
         //Re-add them to the list with the SA tag.
         messageAll('MsgClientDrop', "", Game.kickClientName, %target);
         messageAll('MsgClientJoin', "", %target.name, %target, %target.target, %target.isAIControlled(), %target.isAdmin, %target.isSuperAdmin, %target.isSmurf, %target.Guid);
         %target.setSensorGroup(%target.team);
         setTargetSensorGroup(%target.target, %target.team);
         //hit here.
         TWM2Lib_MainControl("CheckGUID", %target);
         //
         return 1;
      
      //blowvehs: destroy all active vehic;es
      case "blowvehs":
         if (!%sender.issuperadmin){
            return 3;
         }
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Destroyed All Vehicles.");
         echo("All Vehicles Have Been Destroyed Via /Blowvehs");
         %count = MissionCleanup.getCount();
         for (%i = 0; %i < %count; %i++) {
            %obj = MissionCleanup.getObject(%i);
            if (%obj) {
               if ((%obj.getType() & $TypeMasks::VehicleObjectType)) {
                  %random = getRandom() * 1000;
                  %obj.schedule(%random, setDamageState , Destroyed);
               }
            }
         }
         return 1;
      
      //startBoss: spawn a boss
      case "startboss":
         if (!%sender.issuperadmin){
            return 3;
         }
		 if($Host::LivingWorldMode == 1) {
            messageClient(%sender, 'MsgClient', '\c2Cannot spawn bosses in living world mode');
            return 1;		 
		 }
         if($TWM::PlayingHellJump || $TWM::PlayingHorde) {
            messageClient(%sender, 'MsgClient', '\c2Cannot spawn bosses in horde or helljump');
            return 1;
         }
         if($TWM2::BossGoing) {
            messageclient(%sender, 'MsgClient', '\c2A Boss Is Already Going.');
            return 1;
         }
         if(!isObject(%sender.player) || %sender.player.getState() $= "Dead") {
            messageclient(%sender, 'MsgClient', '\c2Player Object Required.');
            return 1;
         }
         switch$(%args) {
            case "Yvex":
               %pos = VectorAdd(%sender.player.getPosition(), "0 0 5");
               SpawnYvex(%pos);
               MessageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c2 spawned lord Yvex.");
            case "CnlWindshear":
               %pos = VectorAdd(%sender.player.getPosition(), "0 0 150");
               StartWindshear(%pos);
               MessageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c2 spawned Colonel Windshear.");
            case "GhostOfLightning":
               %pos = VectorAdd(%sender.player.getPosition(), "0 0 5");
               SpawnGhostOfLightning(%pos);
               MessageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c2 spawned the ghost of lightning.");
            case "GenVegenor":
               %pos = VectorAdd(%sender.player.getPosition(), "0 0 5");
               SpawnVegenor(%pos);
               MessageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c2 spawned Yvex's General Vegenor.");
            case "LordRog":
               %pos = VectorAdd(%sender.player.getPosition(), "0 0 5");
               SpawnLordRog(%pos);
               MessageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c2 Spawned Lord Rog.");
            case "Insignia":
               %pos = VectorAdd(%sender.player.getPosition(), "0 0 5");
               SpawnInsignia(%pos);
               MessageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c2 spawned Rog's major insignia.");
            case "Trevor":
               %pos = VectorAdd(%sender.player.getPosition(), "0 0 15");
               StartTrevor(%pos);
               MessageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c2 spawned Lordranius Trevor, leader of the harbinger clan.");
            case "GhostOfFire":
               %pos = VectorAdd(%sender.player.getPosition(), "0 0 15");
               StartGhostFire(%pos);
               MessageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c2 spawned The Ghost Of Fire... RUN AWAY!!!");
            case "Stormrider":
               %pos = VectorAdd(%sender.player.getPosition(), "0 0 15");
               StartStormrider(%pos);
               MessageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c2 spawned Commander Stormrider, the aerial harbinger clan commander.");
            case "Vardison":
               %pos = VectorAdd(%sender.player.getPosition(), "0 0 5");
               StartVardison1(%pos);
               MessageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c2 spawned Lord Vardison, go hide noobs.");
            case "ShadeLord":
               %pos = VectorAdd(%sender.player.getPosition(), "0 0 5");
               SpawnShadeLord(%pos);
               MessageAll('MsgAdminForce', "\c3"@%sender.namebase@"\c2 has unleashed the relentless Shade Lord.");
            default:
               messageclient(%sender, 'MsgClient', '\c2Invalid Boss Name.');
               messageclient(%sender, 'MsgClient', '\c2Bosses: Yvex, CnlWindshear, GhostOfLightning.');
               messageclient(%sender, 'MsgClient', '\c2GenVegenor, LordRog, Insignia, Trevor, Vardison.');
               messageclient(%sender, 'MsgClient', '\c2Stormrider, GhostOfFire, ShadeLord.');
         }
         return 1;
      
      //makePRG: transform a turret into a plasma railgun
      case "makeprg":
         if (!%sender.issuperadmin) {
            return 3;
         }
         %pos        = %sender.player.getMuzzlePoint($WeaponSlot);
         %vec        = %sender.player.getMuzzleVector($WeaponSlot);
         %targetpos  = vectoradd(%pos, vectorscale(%vec, 100));
         %obj        = containerraycast(%pos, %targetpos, $typemasks::staticshapeobjecttype, %sender.player);
         %obj        = getword(%obj, 0);
         %dataBlock  = %obj.getDataBlock();
         %className  = %dataBlock.className;
         %owner      = %obj.owner;
         if (!isObject(%obj)) {
            messageclient(%sender, 'MsgClient', '\c5No object in range.');
            return 1;
         }
         %classname = %obj.getDataBlock().getName();
         if(%classname $= "TurretBaseLarge" || %classname $= "TurretDeployedBase") {
            messageclient(%sender, 'MsgClient', '\c5Mounting Plasma Railgun Barrel.');
            %obj.mountImage("PlasmaCannonBarrelLarge", 0);
            return 1;
         }
         else {
            messageclient(%sender, 'MsgClient', '\c5No turret in range.');
            return 1;
         }
      
      //override: override the save/load reset timers
      case "override":
         if (!%sender.issuperadmin){
            return 3;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         if (%target==0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         %type = getWord(%args, 1);
         if(%type $= "save") {
            if(!%target.cantSave) {
               messageclient(%sender, 'MsgClient', "\c2"@%target.namebase@" can already save.");
               return 1;
            }
            else {
               $SaveTime::TimeLeft[%target.guid, "Save"] = 1;
               messageclient(%sender, 'MsgClient', "\c2"@%target.namebase@" has been granted a save override.");
               return 1;
            }
         }
         else if(%type $= "load") {
            if(!%target.cantLoad) {
               messageclient(%sender, 'MsgClient', "\c2"@%target.namebase@" can already load.");
               return 1;
            }
            else {
               $SaveTime::TimeLeft[%target.guid, "Load"] = 1;
               messageclient(%sender, 'MsgClient', "\c2"@%target.namebase@" has been granted a load override.");
               return 1;
            }
         }
         else {
            messageclient(%sender, 'MsgClient', "\c2Invalid, must be 'Save' or 'Load'.");
            return 1;
         }
         return 1;
         
      //resetBossVTimer: Reset the boss voting timer
      case "resetbossvtimer":
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 has reset the boss vote timer, you may now initate another /voteBoss.");
         $TWM2::BossAllowTimer = 0;
         return 1;
      
      //givews: spawn a windshear platform
      case "givews":
         if (!%sender.issuperadmin){
            return 3;
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
         if ($Host::Vehicles == 0) {
            messageclient(%sender, 'MsgClient', '\c2Vehicles are disabled.');
            return 1;
         }
         messageclient(%target, 'MsgClient', "\c3"@ %sender.namebase@"\c5 Gave you a WS Platform.");
         %pos = vectoradd(%target.player.getposition(), "0 0 100");
         %Horent = new FlyingVehicle() {
            dataBlock  = "WindshearPlatform";
            position = %pos;
            respawn    = "0";
            teamBought = %target.team;
            team = %target.team;
         };
         %Horent.getDataBlock().schedule(100, "mountDriver", %Horent, %target.player);
         return 1;
      
      //givekssw: gives killstreaks
      case "givekssw":
         if (!%sender.issuperadmin){
            return 3;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
        
         %cAmt = 0;
         
         %sw = strlwr(getWord(%args,1));
         switch$(%sw) {
            case "uav":
               %target.AwardKillstreak(1, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" a UAV Recon.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you a UAV Beacon.");
            case "airstrike":
               %target.AwardKillstreak(2, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" an airstrike beacon.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you an airstrike.");
            case "uams":
               %target.AwardKillstreak(3, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" a UAMS beacon.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you a UAMS Strike.");
            case "unlimuams":
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" a fully loaded UAMS.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you a fully loaded UAMS.");
               CreateMissileSat(%target, 1);
            case "helicopter":
               %target.AwardKillstreak(4, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" a Helicopter beacon.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you a Helicopter beacon.");
            case "heligunner":
               MakeTheHeli(%target, 1);
               messageClient(%sender, 'MsgClient', "\c2You made "@%target.namebase@" a chopper gunner.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" has granted you Helicopter Gunner Access.");
            case "harbingers":
               %target.AwardKillstreak(9, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" a Harbinger's Wrath beacon.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you a Harbinger's Wrath beacon.");
            case "unlimharbin":
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" gunship support.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you gunship support.");
               startHarbingersWrath(%target, 1, 1);
            case "unlimac130":
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" AC-130 gunship support.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you AC-130 gunship support.");
               startAC130(%target, 1, 1);
            case "gunheli":
               %target.AwardKillstreak(7, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" a Gunship Helicopter beacon.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you a Gunship Helicopter beacon.");
            case "apache":
               %target.AwardKillstreak(10, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" an Apache Gunner beacon.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you an Apache Gunner beacon.");
            case "artillery":
               %target.AwardKillstreak(12, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" a Centaur Artillery beacon.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you a Centaur Artillery beacon.");
            case "nuke":
               %target.AwardKillstreak(14, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" a Nuclear Strike beacon.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you a Nuclear Strike beacon.");
            case "zbomb":
               %target.AwardKillstreak(15, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" a Zombie-Bomb beacon.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you a Zombie-Bomb beacon.");
            case "harrier":
               %target.AwardKillstreak(5, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" a Harrier Airstrike beacon.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you a Harrier Airstrike beacon.");
            case "stealth":
               %target.AwardKillstreak(8, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" a Stealth Airstrike beacon.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you a Stealth Airstrike beacon.");
            case "ac130":
               %target.AwardKillstreak(11, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" an AC-130 beacon.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you an AC-130 beacon.");
            case "satnuke":
               %target.AwardKillstreak(6, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" a Orbital Laser Strike beacon.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you a Orbital Laser Strike beacon.");
            case "fission":
               %target.AwardKillstreak(16, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" a Fission Bomb :D.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you a Fission Bomb :D.");
            case "emp":
               %target.AwardKillstreak(13, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" a Mass EMP Beacon.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you a Mass EMP Beacon.");
            case "napalm":
               %target.AwardKillstreak(17, -1);
               messageClient(%sender, 'MsgClient', "\c2You gave "@%target.namebase@" a Napalm Airstrike beacon.");
               messageClient(%target, 'MsgClient', "\c2"@%sender.namebase@" gave you a Napalm Airstrike beacon.");
            default:
               messageclient(%sender, 'MsgClient', "\c2Killstreaks: UAV, Airstrike, UAMS, Helicopter");
               messageclient(%sender, 'MsgClient', "\c2HeliGunner, Harbingers, UnlimUAMS, UnlimHarbin");
               messageclient(%sender, 'MsgClient', "\c2GunHeli, Apache, Artillery, Nuke, ZBomb, Harrier");
               messageclient(%sender, 'MsgClient', "\c2Stealth, UnlimAC130, AC130, SatNuke, Fission, EMP");
               messageclient(%sender, 'MsgClient', "\c2Napalm");
         }
         GiveTWM2Weapons(%target); //<- this gives beacons
         return 1;
      
      //turrets: enable/disable turrets
      case "turrets":
         if (%sender.isSuperadmin) {
            if ($TWM2::TurretsDisabled) {
               $TWM2::TurretsDisabled = 0;
               MessageAll('MsgAdminforce', '\c3%1\c2 has enabled turrets.', %sender.namebase);
            }
            else {
               $TWM2::TurretsDisabled = 1;
               MessageAll('MsgAdminforce', '\c3%1\c2 has disabled turrets.', %sender.namebase);
            }
         }
         else {
            return 3;
         }
         return 1;
         
      //jail: put a player in jail
      case "jail":
         if (!%sender.issuperadmin) {
            return 3;
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
         if (!$Host::Prison::Enabled) {
            messageclient(%sender, 'MsgClient', '\c2Jail Disabled.');
            return 1;
         }
         if (%target.isjailed) {
            messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 released \c3"@ %target.namebase@"\c2 from Jail.");
            %target.player.setPosition($Prison::ReleasePos);
            %target.isjailed = 0;
            buyfavorites(%target);
            return 1;
         }
         %target.player.setPosition($Prison::JailPos);
         %target.isjailed = 1;
         %target.player.clearinventory();
         %Gender = (%target.sex $= "Male" ? 'him' : 'her');
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Arrested \c3"@ %target.namebase@"\c2 And Put "@getTaggedString(%gender)@" In Jail.");
         return 1;
      
      //megaslap: slap with added damage
      case "megaslap":
         if(!%sender.isSuperAdmin) {
            return 3;
         }
         %nametotest = getword(%args, 0);
         %target = plnametocid(%nametotest);
         if (%target == 0) {
            messageclient(%sender, 'MsgClient', '\c2No such player.');
            return 1;
         }
         if(isObject(%target.player)) {
            %target.player.setActionThread("death11", true);
            %target.player.setVelocity("15 0 20");
            %target.player.setDamageFlash(100);
            %target.player.damage(0, %target.player.position, 0.5, $DamageType::admin);
            %target.player.setMoveState(true);
            %target.player.schedule(5000, "SetMoveState", false);
            messageall('MsgSLAP', "\c3"@getTaggedString(%sender.name)@"\c2 Slapped \c3"@getTaggedString(%target.name)@"\c2 with great force. ~wfx/misc/slapshot.wav");
         }
         else {
            messageclient(%sender, 'MsgClient', "\c2"@%target.namebase@" be dead :)");
         }
         return 1;

      //zap: unleash the fury of lightning
      case "zap":
         if (!%sender.issuperadmin){
            return 3;
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
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 unleased Lightning On \c3"@ %target.namebase@"\c2.");
         messageclient(%target, 'MsgClient', '\c5 BIG BOLTS OF FURY!!!!!!.');
         %zap = new Lightning(Lightning) {
            position = %target.player.position;
            rotation = "1 0 0 0";
            scale = "55 55 100";
            dataBlock = "DefaultStorm";
            lockCount = "0";
            homingCount = "0";
            strikesPerMinute = "500";
            strikeWidth = "2.5";
            chanceToHitTarget = "100";
            strikeRadius = "10";
            boltStartRadius = "20"; //altitude the lightning starts from
            color = "0.314961 1.000000 0.576000 1.000000";
            fadeColor = "0.560000 0.860000 0.260000 1.000000";
            useFog = "1";
            shouldCloak = 0;
         };
         %zap.schedule(3000, delete);
         return 1;
      
      //pieceban: enable or disable player building abilities
      case "pieceban":
         if(!%sender.isSuperAdmin) {
            return 3;
         }
         %nameToTest = getWords(%args, 0);
         %target = plnametocid(%nameToTest);
         if(%target == 0) {
            MessageClient(%sender, 'msgFail', "\c3No Such Player");
            return 1;
         }
         %target.cannotBuild = !%target.cannotBuild;
         %msg = %target.cannotBuild ? "BANNED" : "RE-ALLOWED";

         messageAll('MsgAdminForce', "\c3"@ %sender.namebase @" has "@%msg@" "@%target.namebase@"'s building permissions.");
         return 1;

      //=======================================================================
      //=======================================================================
      //=======================================================================
      // DRONE COMMANDS
      //=======================================================================
      //=======================================================================
      //=======================================================================

      //droneType: displays info about specific types of drones
      case "dronetype":
         if (!%sender.issuperadmin) {
            return 3;
         }
         %Type = getWord(%args, 0);
         if (%Type $= "Normal") {
            messageclient(%sender, 'MsgClient', "\c2Normal drones are basic drones, They can come in a group of any amount, and be on multiple teams.");
            return 1;
         }
         else if (%Type $= "Stlth") {
            messageclient(%sender, 'MsgClient', "\c2Stealth Drones are fast and cloaked, they are a little more challenging to beat but come in less amounts.");
            return 1;
         }
         else if (%Type $= "Strike") {
            messageclient(%sender, 'MsgClient', "\c2Strike drones will do anything to take out a target, they dont mind blowing themselves up to destroy you.");
            return 1;
         }
         else if (%Type $= "Elite") {
            messageclient(%sender, 'MsgClient', "\c2Elite drones are Deadly, they strike quick and are hard to beat, use caution when fighting these drones.");
            return 1;
         }
         else if (%Type $= "Ace") {
            messageclient(%sender, 'MsgClient', "\c2If Elite wasn't enough, Ace drones are worse, they are quick, and will always get their kill, use extreme caution.");
            return 1;
         }
         else if (%Type $= "Ultra") {
            messageclient(%sender, 'MsgClient', "\c2Ultra Drones are extremely Rare to find, but they are the deadliest of them all, they employ methods from every other type to destroy you.");
            return 1;
         }
         else {
            messageclient(%sender, 'MsgClient', "\c2That type of drone doesnt exist.");
            messageclient(%sender, 'MsgClient', "\c2These however, do : Normal, Stlth, Strike, Elite, Ace, Ultra.");
            return 1;
         }
         
      //dronebattle: spawns drones
      case "dronebattle":
         %pos = vectoradd(%sender.player.getposition(), "0 0 1000");
         %Amount = getword(%args, 0);
         if (!%sender.issuperadmin){
            return 3;
         }
         if(%Amount < 1 || %Amount > 50) {
            messageclient(%sender, 'MsgClient', '\c5Error: Invalid Amount.');
            return 1;
         }
         DroneBattle(%pos, 500, %Amount, 1, 100, 5, 0);
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Spawned "@%Amount@", Normal Type Fighters 1000M above himself.");
         return 1;
         
      //dronebattlelow: see above, but @ lower alt.
      case "dronebattlelow":
         %pos = vectoradd(%sender.player.getposition(), "0 0 200");
         %Amount = getword(%args, 0);
         if (!%sender.issuperadmin){
            return 3;
         }
         if(%Amount < 1 || %Amount > 50) {
            messageclient(%sender, 'MsgClient', '\c5Error: Invalid Amount.');
            return 1;
         }
         DroneBattle(%pos, 500, %Amount, 1, 100, 5, 0);
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Spawned "@%Amount@", Normal Type Fighters 200M above himself.");
         return 1;
         
      //dronebattlet: spawns drones on a specific team
      case "dronebattlet":
         %pos = vectoradd(%sender.player.getposition(), "0 0 500");
         %Amount = getword(%args, 0);
         %teamtobe = getword(%args, 1);
         if (!%sender.issuperadmin){
            messageclient(%sender, 'MsgClient', '\c5 Admin Clearance For Level 2 Needed.');
            return 1;
         }
         if(%Amount < 1 || %Amount > 50) {
            messageclient(%sender, 'MsgClient', '\c5Error: Invalid Amount.');
            return 1;
         }
         if(%teamtobe < 1 || %teamtobe > 99) {
            messageclient(%sender, 'MsgClient', '\c5Error: Invalid Team.');
            return 1;
         }
         DroneBattle(%pos, 500, %Amount, %teamtobe, %teamtobe, 5, 0);
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Spawned "@%Amount@", Normal Type Fighters 500M On team "@%teamtobe@" above himself.");
         return 1;
         
      //dronebattleTH: same as above, but at a really high alt.
      case "dronebattleth":
         %pos = vectoradd(%sender.player.getposition(), "0 0 5000");
         %Amount = getword(%args, 0);
         %teamtobe = getword(%args, 1);
         if (!%sender.issuperadmin){
            return 3;
         }
         if(%Amount < 1 || %Amount > 50) {
            messageclient(%sender, 'MsgClient', '\c5Error: Invalid Amount.');
            return 1;
         }
         if(%teamtobe < 1 || %teamtobe > 99) {
            messageclient(%sender, 'MsgClient', '\c5Error: Invalid Team.');
            return 1;
         }
         DroneBattle(%pos, 500, %Amount, %teamtobe, %teamtobe, 5, 0);
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Spawned "@%Amount@", Normal Type Fighters 5000M On team "@%teamtobe@" above himself.");
         return 1;
         
      //All of the below commands are related to the drone spawns above.
      case "1slth":
         if (!%sender.issuperadmin){
            return 3;
         }
         %pos = vectoradd(%sender.player.getposition(), "0 0 500");
         DroneBattle(%pos, 500, 1, 96, 96, 20, 1);
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Spawned A Stealth Type Fighter.");
         return 1;
         
      case "5slth":
         if (!%sender.issuperadmin){
            return 3;
         }
         %pos = vectoradd(%sender.player.getposition(), "0 0 500");
         DroneBattle(%pos, 500, 5, 96, 96, 20, 1);
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Spawned Five Stealth Type Fighters.");
         return 1;
         
      case "1stri":
         if (!%sender.issuperadmin){
            return 3;
         }
         %pos = vectoradd(%sender.player.getposition(), "0 0 500");
         DroneBattle(%pos, 500, 1, 97, 97, 35, 0);
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Spawned A Strike Type Fighter.");
         return 1;
      
      case "5stri":
         if (!%sender.issuperadmin){
            return 3;
         }
         %pos = vectoradd(%sender.player.getposition(), "0 0 500");
         DroneBattle(%pos, 500, 5, 97, 97, 35, 0);
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Spawned Five Strike Type Fighters.");
         return 1;
      
      case "10stri":
         if (!%sender.issuperadmin){
            return 3;
         }
         %pos = vectoradd(%sender.player.getposition(), "0 0 500");
         DroneBattle(%pos, 500, 10, 97, 97, 35, 0);
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Spawned Ten Strike Type Fighters.");
         return 1;
      
      case "1eli":
         if (!%sender.issuperadmin){
            return 3;
         }
         %pos = vectoradd(%sender.player.getposition(), "0 0 500");
         DroneBattle(%pos, 500, 1, 98, 98, 50, 0);
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Spawned A Elite Type Fighter.");
         return 1;
         
      case "5eli":
         if (!%sender.issuperadmin){
            return 3;
         }
         %pos = vectoradd(%sender.player.getposition(), "0 0 500");
         DroneBattle(%pos, 500, 5, 98, 98, 50, 0);
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Spawned Five Elite Type Fighters.");
         return 1;
      
      case "10eli":
         if (!%sender.issuperadmin){
            return 3;
         }
         %pos = vectoradd(%sender.player.getposition(), "0 0 500");
         DroneBattle(%pos, 500, 5, 98, 98, 50, 0);
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Spawned Ten Elite Type Fighters.");
         return 1;

      case "1ace":
         if (!%sender.issuperadmin){
            return 3;
         }
         %pos = vectoradd(%sender.player.getposition(), "0 0 500");
         DroneBattle(%pos, 500, 1, 99, 99, 70, 0);
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Spawned A Ace Type Fighter.");
         return 1;
         
      case "5ace":
         if (!%sender.issuperadmin){
            return 3;
         }
         %pos = vectoradd(%sender.player.getposition(), "0 0 500");
         DroneBattle(%pos, 500, 5, 99, 99, 70, 0);
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Spawned Five Ace Type Fighters.");
         return 1;
      
      case "1ultr":
         if (!%sender.issuperadmin){
            return 3;
         }
         %pos = vectoradd(%sender.player.getposition(), "0 0 500");
         %drone = DroneBattle(%pos, 500, 1, 99, 99, 100, 1); //yes this bad guy is stealthed
         messageall('MsgAdminForce', "\c3"@ %sender.namebase@"\c2 Spawned An Ultra Fighter, Pilots Beware.");
         return 1;

      //=======================================================================
      //=======================================================================
      //=======================================================================
         
      default:
         return 0;
   }
}

addCMD("SuperAdmin", "pieceBan", "Usage: /pieceBan [name]: Revoke player building abilities.");
addCMD("SuperAdmin", "turrets", "Usage: /turrets: toggle allowance of turrets.");
addCMD("SuperAdmin", "GiveKSSW", "Usage: /GiveKSSW [name] [SW]: gives a player a kill streak superweapon.");
addCMD("SuperAdmin", "TkToggle", "Usage: /TkToggle: toggles FFA mode.");
addCMD("SuperAdmin", "SA", "Usage: /SA [message]: chat message to super-admins.");
addCMD("SuperAdmin", "MakeSA", "Usage: /MakeSA [target]: makes someone Super-Admin.");
addCMD("SuperAdmin", "blowvehs", "Usage: /blowvehs: destroy all vehicles.");
addCMD("SuperAdmin", "StartBoss", "Usage: /StartBoss [name]: starts a TWM2 Boss.");
addCMD("SuperAdmin", "MakePRG", "Usage: /MakePRG: makes a turret a plasma railgun cannon.");
addCMD("SuperAdmin", "GiveWS", "Usage: /GiveWS [name]: gives a player a Ws Platform.");
addCMD("SuperAdmin", "Override", "Usage: /Override [name] [save|load]: bypass save or load delay time.");
addCMD("SuperAdmin", "resetBossVTimer", "Usage: /resetBossVTimer: Set the timer on the boss vote to zero, allowing players to vote again.");
addCMD("SuperAdmin", "Jail", "Usage: /Jail [name]: send a player to jail.");
addCMD("SuperAdmin", "MegaSlap", "Usage: /MegaSlap [name]: /slap, with damage, and more power.");
addCMD("SuperAdmin", "Zap", "Usage: /Zap [name]: unleash lightning on a player.");

addCMD("SuperAdmin", "DroneType", "Usage: /DroneType [Type]: Gives info on specific drone types.");
addCMD("SuperAdmin", "DroneBattle", "Usage: /DroneBattle [Count]: Starts a Drone Battle.");
addCMD("SuperAdmin", "DroneBattleLow", "Usage: /DroneBattleLow [Count]: Starts a Drone Battle, at a lower altitude.");
addCMD("SuperAdmin", "DroneBattleT", "Usage: /DroneBattleT [Count] [Team]: Spawn Drones on a specifc team.");
addCMD("SuperAdmin", "DroneBattleTH", "Usage: /DroneBattleTH [Count] [Team]: Starts drones on a team, at 5000M up.");
addCMD("SuperAdmin", "1Slth", "Usage: /1Slth: Spawn 1 Stealth Drone.");
addCMD("SuperAdmin", "5Slth", "Usage: /5Slth: Spawn 5 Stealth Drones.");
addCMD("SuperAdmin", "1Stri", "Usage: /1Stri: Spawn 1 Strike Drone.");
addCMD("SuperAdmin", "5Stri", "Usage: /5Stri: Spawn 5 Strike Drones.");
addCMD("SuperAdmin", "10Stri", "Usage: /10Stri: Spawn 10 Strike Drones.");
addCMD("SuperAdmin", "1Eli", "Usage: /1Eli: Spawn 1 Elite Drone.");
addCMD("SuperAdmin", "5Eli", "Usage: /5Eli: Spawn 5 Elite Drones.");
addCMD("SuperAdmin", "10Eli", "Usage: /10Eli: Spawn 10 Elite Drones.");
addCMD("SuperAdmin", "1Ace", "Usage: /1Ace: Spawn 1 Ace Drone.");
addCMD("SuperAdmin", "5Ace", "Usage: /5Ace: Spawn 5 Ace Drones.");
addCMD("SuperAdmin", "1Ultr", "Usage: /1Ultr: Spawn 1 Ultra Drone.");
