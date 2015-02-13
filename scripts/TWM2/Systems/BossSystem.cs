function InitiateBoss(%Boss, %name) {
   if($TWM::PlayingHorde || $TWM::PlayingHelljump) {
      error("SERVER: Cannot initiate boss, in horde/helljump");
      return;
   }
   
   if(!isObject($TWM2::BossManager)) {
      $TWM2::BossManager = new scriptObject() {
         class = "BossManager";
      };
   }
   $TWM2::BossManager.bossKills = 0;
   $TWM2::BossManager.bossObject = %Boss;
   $TWM2::BossManager.activeBoss = %name;

   $TWM2::BossGoing = 1;
   switch$(%name) {
      case "Yvex":
         %print = "<color:FF0000>BOSS BATTLE \n LORD YVEX";
      case "CnlWindshear":
         %print = "<color:FF0000>BOSS BATTLE \n COLONEL WINDSHEAR";
      case "GhostOfLightning":
         %print = "<color:FF0000>BOSS BATTLE \n GHOST OF LIGHTNING";
      case "Vengenor":
         %print = "<color:FF0000>BOSS BATTLE \n GENERAL VENGENOR";
      case "LordRog":
         %print = "<color:FF0000>BOSS BATTLE \n LORD ROG";
      case "Insignia":
         %print = "<color:FF0000>BOSS BATTLE \n MAJOR INSIGNIA";
      case "Vardison1":
         %print = "<color:FF0000>BOSS BATTLE \n LORD VARDISON";
      case "Vardison2":
         %print = "<color:FF0000>BOSS ALERT \n LORD VARDISON HAS ENTERED HIS SECOND FORM";
      case "Vardison3":
         %print = "<color:FF0000>BOSS ALERT \n LORD VARDISON HAS ENTERED HIS FINAL FORM";
      case "Trebor":
         %print = "<color:FF0000>BOSS BATTLE \n LORDRANIUS TREVOR";
      case "Stormrider":
         %print = "<color:FF0000>BOSS BATTLE \n COMMANDER STORMRIDER";
      case "GhostOfFire":
         %print = "<color:FF0000>BOSS BATTLE \n GHOST OF FIRE";
      case "ShadeLord":
         %print = "<color:FF0000>BOSS BATTLE \n THE SHADE LORD";
   }
   //INITIATE TO CLIENTS
   %count = ClientGroup.getCount();
   for(%i = 0; %i < %count; %i++) {
      %cl = ClientGroup.getObject(%i);
      BottomPrint(%cl, ""@%print@"", 5, 3);
      %cl.damageToBoss = 0;
   }
   %boss.isBoss = 1; // the isBoss Flag helps us out with things
   BossCheckUp(%boss, %name);
   
   if($BossMaxHealth[%name] $= "") {
      $BossMaxHealth[%name] = %boss.getMaxDamage();
   }
}

function BossCheckUp(%boss, %name) {
   %percentage = (mFloor(%boss.getDamageLeft()*100) / mFloor(%boss.getMaxDamage()*100)) * 100;
   MessageAll('MsgSPCurrentObjective1', "", "Boss Battle: "@$TWM2::BossName[%name]@" [Boss Kill Count: "@$TWM2::BossManager.bossKills@"]");
   MessageAll('MsgSPCurrentObjective2', "", "Boss HP: "@mFloor(%boss.getDamageLeft()*100)@"/"@mFloor(%boss.getMaxDamage()*100)@" ("@%percentage@"%)");

   if(%name !$= "CnlWindshear" && %name !$= "Trebor" && %name !$= "Stormrider") {
      if(!isObject(%boss) || %boss.getState() $= "dead") {
         if(%name $= "Vardison1") {
            %count = ClientGroup.getCount();
            for(%i = 0; %i < %count; %i++) {
               %cl = ClientGroup.getObject(%i);
               recordAction(%cl, "BOSS", "Vardison1");
            }
            SpawnVardison2(%boss.getPosition());
            return;
         }
         if(%name $= "Vardison2") {
            %count = ClientGroup.getCount();
            for(%i = 0; %i < %count; %i++) {
               %cl = ClientGroup.getObject(%i);
               recordAction(%cl, "BOSS", "Vardison2");
            }
            SpawnVardison3(%boss.getPosition());
            return;
         }
         //the boss has been defeated, horrah!!!
         %count = ClientGroup.getCount();
         for(%i = 0; %i < %count; %i++) {
            %cl = ClientGroup.getObject(%i);
            if(%cl.damageToBoss > 0) {
               %cl.GiveBossAward(%name);
            }
         }
         $TWM2::BossGoing = 0;
         MessageAll('MsgSPCurrentObjective1', "", "Welcome to TWM2!");
         MessageAll('MsgSPCurrentObjective2', "", "Phantom139, DoL, Signal360");
         return;
      }
      schedule(1000, 0, "BossCheckUp", %boss, %name);
   }
   else {
      if(!isObject(%boss)) {
         %count = ClientGroup.getCount();
         for(%i = 0; %i < %count; %i++) {
            %cl = ClientGroup.getObject(%i);
            if(%cl.damageToBoss) {
               %cl.GiveBossAward(%name);
            }
         }
         MessageAll('MsgSPCurrentObjective1', "", "Welcome to TWM2!");
         MessageAll('MsgSPCurrentObjective2', "", "Phantom139, DoL, Signal360");
         $TWM2::BossGoing = 0;
         return;
      }
      schedule(1000, 0, "BossCheckUp", %boss, %name);
   }
}

function GameConnection::GiveBossAward(%client, %bossName) {
   %scriptController = %client.TWM2Core;
   %file = ""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave";
   //you earn less EXP every time you defeat a specific boss, so tread lightly on those defeat counts :)
   
   recordAction(%client, "BOSS", %bossName);
   
   if(!isSet(%scriptController.bossDefeatCount[%bossName])) {
      %scriptController.bossDefeatCount[%bossName] = 0;
   }
   if(%bossName $= "Yvex") {
      AwardClient(%client, "1");
   }
   else if(%bossName $= "CnlWindshear") {
      AwardClient(%client, "8");
   }
   else if(%bossName $= "GhostOfLightning") {
      AwardClient(%client, "9");
   }
   else if(%bossName $= "Vengenor") {
      AwardClient(%client, "10");
   }
   else if(%bossName $= "LordRog") {
      AwardClient(%client, "11");
   }
   else if(%bossName $= "Insignia") {
      AwardClient(%client, "12");
   }
   else if(%bossName $= "GhostOfFire") {
      AwardClient(%client, "27");
   }
   else if(%bossName $= "Stormrider") {
      AwardClient(%client, "28");
   }
   else if(%bossName $= "ShadeLord") {
      AwardClient(%client, "30");
   }
   //VARDISON
   else if(%bossName $= "Vardison3") {
      AwardClient(%client, 13);
   }
   else if(%bossName $= "Trebor") {
      AwardClient(%client, 15);
   }
   //rank writing
   %scriptController.bossDefeatCount[%bossName]++;
   %scriptController.save(%file);
   
   %damageCount = %client.damageToBoss;
   %maxHP = $BossMaxHealth[%bossName];
   
   %ratio = %damageCount / %maxHP;
   
   %award = mFloor(($TWM2::BossXPAward[%bossName] * %ratio) / %scriptController.bossDefeatCount[%bossName]);
   GainExperience(%client, %award, ""@%bossName@" defeated, congratulations! ");
   CheckBossChallenge(%client, %bossName);
}

function FindValidTarget(%boss, %counter) {   //This is usefull
   if(%counter $= "") {
      %counter = 10; //10 attempts
   }
   for(%i = 0; %i < %counter; %i++) {
      %test = ClientGroup.getObject(getRandom(0, ClientGroup.getCount()));
      if(isObject(%test)) {
         %tPL = %test.getControlObject();
         if(isObject(%tPL)) {
            if(isPlayer(%tPL)) {
               if(%tPL.getState() !$= "dead") {
                  //Got one!
                  return %test;
               }
            }
            else {
               return %test;
            }
         }
      }
   }
   return -1; //Found nothing.
}

function CheckBossChallenge(%client, %boss) {
   %scriptController = %client.TWM2Core;
   %dc = %scriptController.bossDefeatCount[%boss];
   switch$(%boss) {
      case "Yvex":
         if(%dc >= 3) {
            CompleteNWChallenge(%client, "Yvex1");
         }
         if(%dc >= 5) {
            CompleteNWChallenge(%client, "Yvex2");
         }
         if(%dc >= 10) {
            CompleteNWChallenge(%client, "Yvex3");
         }
      case "CnlWindshear":
         if(%dc >= 3) {
            CompleteNWChallenge(%client, "CWS1");
         }
         if(%dc >= 5) {
            CompleteNWChallenge(%client, "CWS2");
         }
         if(%dc >= 10) {
            CompleteNWChallenge(%client, "CWS3");
         }
      case "GhostOfLightning":
         if(%dc >= 3) {
            CompleteNWChallenge(%client, "GOL1");
         }
         if(%dc >= 5) {
            CompleteNWChallenge(%client, "GOL2");
         }
         if(%dc >= 10) {
            CompleteNWChallenge(%client, "GOL3");
         }
      case "Vegenor":
         if(%dc >= 3) {
            CompleteNWChallenge(%client, "Veg1");
         }
         if(%dc >= 5) {
            CompleteNWChallenge(%client, "Veg2");
         }
         if(%dc >= 10) {
            CompleteNWChallenge(%client, "Veg3");
         }
      case "LordRog":
         if(%dc >= 2) {
            CompleteNWChallenge(%client, "LRog1");
         }
         if(%dc >= 4) {
            CompleteNWChallenge(%client, "LRog2");
         }
         if(%dc >= 7) {
            CompleteNWChallenge(%client, "LRog3");
         }
      case "Insignia":
         if(%dc >= 2) {
            CompleteNWChallenge(%client, "Ins1");
         }
         if(%dc >= 4) {
            CompleteNWChallenge(%client, "Ins2");
         }
         if(%dc >= 7) {
            CompleteNWChallenge(%client, "Ins3");
         }
      case "Vardison3":
         if(%dc >= 1) {
            CompleteNWChallenge(%client, "Vard1");
         }
         if(%dc >= 3) {
            CompleteNWChallenge(%client, "Vard2");
         }
         if(%dc >= 5) {
            CompleteNWChallenge(%client, "Vard3");
         }
      case "Trebor":
         if(%dc >= 2) {
            CompleteNWChallenge(%client, "Treb1");
         }
         if(%dc >= 4) {
            CompleteNWChallenge(%client, "Treb2");
         }
         if(%dc >= 7) {
            CompleteNWChallenge(%client, "Treb3");
         }
   }
}

function GenerateBossChallengeMenu(%client, %tag, %index) {
   if(%client.CheckNWChallengeCompletion("Yvex1")) {
      if(%client.CheckNWChallengeCompletion("Yvex2")) {
         if(%client.CheckNWChallengeCompletion("Yvex3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Shadowy Desecration - Done");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Shadowy Desecration - Defeat Lord Yvex 10 Times");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Darkness Rising - Defeat Lord Yvex 5 Times");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Nightmarish Enterprise - Defeat Lord Yvex 3 Times");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("CWS1")) {
      if(%client.CheckNWChallengeCompletion("CWS2")) {
         if(%client.CheckNWChallengeCompletion("CWS3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Harbinger's Bane - Done");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Harbinger's Bane - Defeat Colonel Windshear 10 Times");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Aerieal Nightmare - Defeat Colonel Windshear 5 Times");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Fortress In The Sky - Defeat Colonel Windshear 3 Times");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("GOL1")) {
      if(%client.CheckNWChallengeCompletion("GOL2")) {
         if(%client.CheckNWChallengeCompletion("GOL3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Severe Thunderstorm - Done");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Severe Thunderstorm - Defeat The Ghost Of Lightning 10 Times");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "The Shocking Truth - Defeat The Ghost Of Lightning 5 Times");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Envious Lightning - Defeat The Ghost Of Lightning 3 Times");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Veg1")) {
      if(%client.CheckNWChallengeCompletion("Veg2")) {
         if(%client.CheckNWChallengeCompletion("Veg3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Firestorm Ender - Done");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Firestorm Ender - Defeat General Vegenor 10 Times");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Burning Frenzy - Defeat General Vegenor 5 Times");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Flaming Revolt - Defeat General Vegenor 3 Times");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("LRog1")) {
      if(%client.CheckNWChallengeCompletion("LRog2")) {
         if(%client.CheckNWChallengeCompletion("LRog3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Payback's A Bitch - Done");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Payback's A Bitch - Defeat Lord Rog 7 Times");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Return to Returner - Defeat Lord Rog 4 Times");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Revenge Halter - Defeat Lord Rog 2 Times");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Ins1")) {
      if(%client.CheckNWChallengeCompletion("Ins2")) {
         if(%client.CheckNWChallengeCompletion("Ins3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Gravitational Influx - Done");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Gravitational Influx - Defeat Major Insignia 7 Times");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "No Gravity, No Problem - Defeat Major Insignia 4 Times");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "El Shipitor - Defeat Major Insignia 2 Times");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Treb1")) {
      if(%client.CheckNWChallengeCompletion("Treb2")) {
         if(%client.CheckNWChallengeCompletion("Treb3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Tank Halter - Done");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Tank Halter - Defeat Lordranius Trebor 7 Times");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Harbinger Denied - Defeat Lordranius Trebor 4 Times");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Precious Cargo - Defeat Lordranius Trebor 2 Times");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("Vard1")) {
      if(%client.CheckNWChallengeCompletion("Vard2")) {
         if(%client.CheckNWChallengeCompletion("Vard3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Outevil The Wicked - Done");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Outevil The Wicked - Defeat Lord Vardison 5 Times");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Glare The Dark - Defeat Lord Vardison 3 Times");
         %index++;
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Shining Star - Defeat Lord Vardison");
      %index++;
   }
   //
   if(%client.CheckNWChallengeCompletion("VardEasy")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "The Standard Experience - Done");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "The Standard Experience - Defeat Lord Vardison on Easy Difficulty");
      %index++;
   }
   if(%client.CheckNWChallengeCompletion("VardNorm")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Demon Hunter - Done");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Demon Hunter - Defeat Lord Vardison on Normal Difficulty");
      %index++;
   }
   if(%client.CheckNWChallengeCompletion("VardHard")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Master Demon Slayer - Done");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Master Demon Slayer - Defeat Lord Vardison on Hard Difficulty");
      %index++;
   }
   if(%client.CheckNWChallengeCompletion("VardWtf")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "God of the Shadow Realm - Done [You are a freaking boss master]");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "God of the Shadow Realm - Against all odds, emerge victorious against WTF difficulty Lord Vardison");
      %index++;
   }
   //
   return %index;
}

function BossManager::addKill(%this, %tObj) {
   %this.bossKills++;
}

//Load The Boss Files
exec("scripts/TWM2/Bosses/LordYvex.cs");
exec("scripts/TWM2/Bosses/ColonelWindshear.cs");
exec("scripts/TWM2/Bosses/GhostOfLightning.cs");
exec("scripts/TWM2/Bosses/GeneralVegenor.cs");
exec("scripts/TWM2/Bosses/LordRog.cs");
exec("scripts/TWM2/Bosses/MajorInsignia.cs");
exec("scripts/TWM2/Bosses/LordraniusTrebor.cs");
exec("scripts/TWM2/Bosses/Stormrider.cs");
exec("scripts/TWM2/Bosses/GhostOfFire.cs");
exec("scripts/TWM2/Bosses/ShadeLord.cs");
