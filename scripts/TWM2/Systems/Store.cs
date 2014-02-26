//Store.cs
//Phantom139, TWM2 3.6

//Handles all operations with client mula.

$Store::Item["ArmorEffect", 0] = "Fireworks\t750000\tOn death, sends out a burst of red flares.";
$Store::Item["ArmorEffect", 1] = "False Explosion\t1250000\tAn explosion so real, but so false.";
$Store::Item["ArmorEffect", 2] = "Thunderstorm\t2000000\tEmit a great deal of sparks from your armor.";
$Store::Item["ArmorEffect", 3] = "Spontaneous Combustion\t3000000\tIt almost appears that you are always on fire.";
$Store::Item["ArmorEffect", 4] = "Guardian Flare\t5000000\tA glimmer of light, that follows you around.";
$Store::Item["ArmorEffect", 5] = "Hologram\t10000000\tMislead chasers with false player images.";
//
$Store::Item["ArmorFlag", 0] = "Silver Flag\t5\t0\tShines in the light of day.";
$Store::Item["ArmorFlag", 1] = "Blue Flag\t7\t0\tBlue as the sky, cold as snow.";
$Store::Item["ArmorFlag", 2] = "Red Flag\t8\t0\tHonor the call of battle, and show your pride.";
$Store::Item["ArmorFlag", 3] = "Gold Flag\t9\t0\tTime and dedication shows your honor.";

function GameConnection::storeCreate(%client) {
   %name = "Container_"@%client.guid@"/ClientStore"@%client.guid;
   %check = nameToID(%name);
   if(isObject(%check)) {
      %client.store = %check;
      %client.store.nextSlot = 0;
      %client.store.nextLoto = 0;
      echo("* Applying Client Store.");
   }
   else {
      %client.store = new ScriptObject("ClientStore"@%client.guid) {
         nextSlot = 0;
         nextLoto = 0;
      };
      %client.container.add(%client.store);
      echo("* Creating Client Store");
   }
}

//Store Operations
function GameConnection::showCentralStore(%client, %index, %tag) {
   messageClient( %client, 'SetScoreHudSubheader', "", "Central Store" );
   %client.SCMPage = "SM";
   //
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Current Money: $"@%client.TWM2Core.money@"");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "================================");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "=                           SELECT AN OPTION                                  =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "================================");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tStore\tArmorEffect> * Armor Effects</a>");
   %index++;
   if(%client.TWM2Core.officer >= 5) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tStore\tArmorFlag> * Armor Flags</a>");
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, " * Armor Flags (Unlocked At 'Surpreme' Officer Level)");
      %index++;
   }
   messageClient( %client, 'SetLineHud', "", %tag, %index, "================================");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tStore\tSlotMachine> * Slot Machine</a>");
   %index++;
   //messageClient( %client, 'SetLineHud', "", %tag, %index, " * Bounties");
   //%index++;                                                                 //Maybe some other time *Phantom
   messageClient( %client, 'SetLineHud', "", %tag, %index, "================================");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
   %index++;
   //
   return %index;
}

function GameConnection::showArmorEffectsPage(%client, %index, %tag) {
   messageClient( %client, 'SetScoreHudSubheader', "", "Central Store" );
   %client.SCMPage = "SM";
   //
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Current Money: $"@%client.TWM2Core.money@"");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "================================");
   %index++;
   //
   %effectIndex = 0;
   while(isSet($Store::Item["ArmorEffect", %effectIndex])) {
      if(%client.getActiveAE() $= getField($Store::Item["ArmorEffect", %effectIndex], 0)) {
         messageClient( %client, 'SetLineHud', "", %tag, %index, ""@getField($Store::Item["ArmorEffect", %effectIndex], 0)@" [ACTIVE]");
         %index++;
      }
      else {
         if(%client.hasStoreItem(getField($Store::Item["ArmorEffect", %effectIndex], 0))) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tArmorEffect\t"@getField($Store::Item["ArmorEffect", %effectIndex], 0)@">"@getField($Store::Item["ArmorEffect", %effectIndex], 0)@"</a> - "@getField($Store::Item["ArmorEffect", %effectIndex], 2)@"");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tBuyArmorEffect\t"@%effectIndex@">"@getField($Store::Item["ArmorEffect", %effectIndex], 0)@"</a>[$"@getField($Store::Item["ArmorEffect", %effectIndex], 1)@"] - "@getField($Store::Item["ArmorEffect", %effectIndex], 2)@"");
            %index++;
         }
      }
      %effectIndex++;
   }
   //
   if(%client.getActiveAE() !$= "none") {
      messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tArmorEffect\t0>Disable Armor Effect</a>');
      %index++;
   }
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
   %index++;
   return %index;
}

function GameConnection::showArmorFlagPage(%client, %index, %tag) {
   messageClient( %client, 'SetScoreHudSubheader', "", "Central Store" );
   %client.SCMPage = "SM";
   //
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Current Money: $"@%client.TWM2Core.money@"");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "================================");
   %index++;
   //
   %flagIndex = 0;
   while(isSet($Store::Item["ArmorFlag", %flagIndex])) {
      if(%client.flagType $= trim(getWord(getField($Store::Item["ArmorFlag", %flagIndex], 0), 0))) {
         messageClient( %client, 'SetLineHud', "", %tag, %index, ""@getField($Store::Item["ArmorFlag", %flagIndex], 0)@" [ACTIVE]");
         %index++;
      }
      else {
         if(%client.hasStoreItem(getField($Store::Item["ArmorFlag", %flagIndex], 0))) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tArmorFlag\t"@getField($Store::Item["ArmorFlag", %flagIndex], 0)@">"@getField($Store::Item["ArmorFlag", %flagIndex], 0)@"</a> - "@getField($Store::Item["ArmorFlag", %flagIndex], 3)@"");
            %index++;
         }
         else {
            if(%client.TWM2Core.officer < getField($Store::Item["ArmorFlag", %flagIndex], 1)) {
               messageClient( %client, 'SetLineHud', "", %tag, %index, ""@getField($Store::Item["ArmorFlag", %flagIndex], 0)@" - Higher Officer Rank Required.");
               %index++;
            }
            else {
               messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tBuyArmorFlag\t"@%flagIndex@">"@getField($Store::Item["ArmorFlag", %flagIndex], 0)@"</a>[$"@getField($Store::Item["ArmorFlag", %flagIndex], 2)@"] - "@getField($Store::Item["ArmorFlag", %flagIndex], 3)@"");
               %index++;
            }
         }
      }
      %flagIndex++;
   }
   //
   if(%client.useFlag != 0) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tArmorFlag\t0>Disable Armor Flag</a>');
      %index++;
   }
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
   %index++;
   return %index;
}

//ARMOR EFFECTS
function GameConnection::hasStoreItem(%client, %item) {
   %item = strReplace(%item, " ", "");
   if(!isSet(%client.store.purchased[%item])) {
      %client.store.purchased[%item] = 0;
   }
   return %client.store.purchased[%item];
}

function GameConnection::getActiveAE(%client) {
   %e = %client.activeArmorEffect;
   if(!isSet(%e)) {
      return "none";
   }
   return %e;
}

function GameConnection::setActiveAE(%client, %effect) {
   //
   if(%effect $= "0") {
      %client.activeArmorEffect = "";
      return;
   }
   //
   if(!%client.hasStoreItem(%effect)) {
      messageClient(%client, 'msgFail', "\c3Error: You do not have this armor effect");
      %client.activeArmorEffect = "";
      return;
   }
   //
   else {
      %client.activeArmorEffect = %effect;
      switch$(%effect) {
         case "Thunderstorm":
            ThunderstormLoop(%client);
         case "Spontaneous Combustion":
            SpontaniousLoop(%client);
         case "Guardian Flare":
            createGuardianFlare(%client);
         case "Hologram":
            DoHologramLoop(%client);
         default:
            //unknown, or nothing needed
      }
   }
}

function GameConnection::setActiveAF(%client, %flag) {
   if(%flag $= "0") {
      %client.flagType = "";
      %client.useFlag = 0;
      return;
   }
   //
   if(!%client.hasStoreItem(%flag)) {
      messageClient(%client, 'msgFail', "\c3Error: You do not have this armor flag");
      %client.flagType = "";
      %client.useFlag = 0;
      return;
   }
   else {
      switch$(%flag) {
         case "Silver Flag":
            %client.flagType = "Silver";
            %client.useFlag = 1;
         case "Blue Flag":
            %client.flagType = "Blue";
            %client.useFlag = 1;
         case "Red Flag":
            %client.flagType = "Red";
            %client.useFlag = 1;
         case "Gold Flag":
            %client.flagType = "Gold";
            %client.useFlag = 1;
         default:
            %client.flagType = "";
            %client.useFlag = 0;
      }
   }
}

//this is called when someone dies.
function GameConnection::playDeathArmorEffect(%client) {
   switch$(%client.getActiveAE()) {
      case "none":
         return; //stop here
      case "Fireworks":
         DeathEffect_Fireworks(%client.player.getPosition());
      case "False Explosion":
         DeathEffect_FalseExpl(%client.player.getPosition());
   }
}

//* FIREWORKS
//* $750,000
//* A bright explosion of Red Flares when you die

datablock FlareProjectileData(FireworksRedFlareProj) {
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = false;
   kickBackStrength    = 1500;
   useLensFlare        = false;

   sound 			   = FlareGrenadeBurnSound;
   explosion           = FlareGrenadeExplosion;
   velInheritFactor    = 0.5;

   texture[0]          = "special/flare3";
   texture[1]          = "special/LensFlare/flare00";
   size                = 4.0;

   baseEmitter         = FlareEmitter;

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.2;
   armingDelayMS     = 6000;
   muzzleVelocity    = 15.0;
   drag = 0.1;
   gravityMod = 0.15;
};

datablock FlareProjectileData(GuardianFlare) {
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = false;
   kickBackStrength    = 1500;
   useLensFlare        = false;

   sound 			   = FlareGrenadeBurnSound;
   explosion           = FlareGrenadeExplosion;
   velInheritFactor    = 0.5;

   texture[0]          = "special/flare3";
   texture[1]          = "special/LensFlare/flare00";
   size                = 4.0;

   baseEmitter         = FlareEmitter;

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.2;
   armingDelayMS     = 100;
   muzzleVelocity    = 15.0;
   drag = 0.1;
   gravityMod = 0.15;
};

function DeathEffect_Fireworks(%position) {
   for(%i = 0; %i < getRandom(15, 25); %i++) {
      %dir = vectorAdd(getRandomPosition(1, 0), "0 0 2"); //all up
      %fW = new (FlareProjectile)() {
         datablock = FireworksRedFlareProj;
         initialPosition = %position;
         initialDirection = %dir;
      };
      MissionCleanup.add(%fW);
   }
}

//* FALSE EXPLOSION
//* $1,250,000
//* A detonation that looks so real, you won't believe it's not

function DeathEffect_FalseExpl(%position) {
   %c4 = new Item() {
      datablock = C4Deployed;
      position = %position;
      scale = ".1 .1 .1";
   };
   MissionCleanup.add(%c4);
   %c4.isFalse = 1;
   schedule(500, 0, "C4GoBoom", %c4);
}

//* Thunderstorm
//* $2,000,000
//* Sparks fly in all directions from your active armor

function ThunderstormLoop(%client) {
   %player = %client.player;
   if(!%player.isAlive() || %client.getActiveAE() !$= "Thunderstorm") {
      return;
   }
   %c = createEmitter(%player.position, PBCExpEmitter, "1 0 0");      //Rotate it
   %c.schedule(150, "Delete");
   schedule(getRandom(1300, 2500), 0, "ThunderstormLoop", %client);
}

//* Spontaneous Combustion
//* $3,000,000
//* It almost appears that you are on fire... always

function SpontaniousLoop(%client) {
   %player = %client.player;
   if(!%player.isAlive() || %client.getActiveAE() !$= "Spontaneous Combustion") {
      return;
   }
   %fire = new ParticleEmissionDummy(){
   	  position = %player.getPosition();
   	  dataBlock = "defaultEmissionDummy";
      emitter = "BurnEmitter";
   };
   MissionCleanup.add(%fire);
   %fire.schedule(250, "delete");
   schedule(250, 0, "SpontaniousLoop", %client);
}

//* Guardian Flare
//* $5,000,000
//* This beacon of light follows you around at all times.

function createGuardianFlare(%client) {
   %player = %client.player;
   if(!%player.isAlive() || %client.getActiveAE() !$= "Guardian Flare") {
      return;
   }
   %flare = new (FlareProjectile)() {
      dataBlock        = GuardianFlare;
      initialDirection = "0 0 3";
      initialPosition  = vectorAdd(%player.getPosition(), "0 0 5");
      sourceObject     = %player;
      sourceSlot = 0;
   };
   MissionCleanup.add(%flare);
   %flare.PhotonMuzVec = %player.getMuzzleVector(0);
   schedule( 100, 0, "guardianFlare", %player, %flare);
}

function guardianFlare(%player, %flare) {
   %projpos = %flare.position;
   %projdir = %flare.initialdirection;
   %type = %player.getClassName();
   if(!isobject(%flare)) {
      return;
   }
   if(isobject(%flare)) {
      %flare.delete();
   }
   if(!%player.isAlive() || %player.client.getActiveAE() !$= "Guardian Flare") {
      return;
   }
   //( ... )the projs now have a max turn angle like real missile ub3r l33t;;;; nm wtf afasdf
   %test = getWord(%projdir, 0);
   %test2 = getWord(%projdir, 1);
   %test3 = getWord(%projdir, 2);

   %projdir = vectornormalize(vectorsub(%player.position, %projpos));
   %testa = getWord(%projdir, 0);
   %testa2 = getWord(%projdir, 1);
   %testa3 = getWord(%projdir, 2);

   // now it's time for my mad math skills..... i used microsoft calculator to figure this one out =0... was a brainbuster for me to think of how this would work
   %testthing = %test - %testa; //oh u can rename all this test stuff but make sure u get it right =/ dont break plz
   %testfin = %testthing / 10;  //!!!!!!!!!! OK HERE!!!! is where the max angle thing is... increase for lower turn angle and reduce for a higher turn angle
   %testfinal = %testfin * -1;   //^^^^^ *side note for the one above this* dont div by zero unless yer dumb (...) div by i think 1 if you want it to seek with a 360 max turn angle angle... kinda gay though if u do that
   %testfinale = %testfinal + %test;

   %testthing2 = %test2 - %testa2;
   %testfin2 = %testthing2 / 10; //change here too .. this is for the y axis  btw it's best if u leave my setting of 10 on ... it's the most balanced well nm change it to what u want but you really should leave it around this number like 9ish
   %testfinal2 = %testfin2 * -1;
   %testfinale2 = %testfinal2 + %test2;

   %testthing3 = %test3 - %testa3;
   %testfin3 = %testthing3 / 20; //z- axis this one is for i think.. mmm idea... you try playing with dif max angles for xyz for maybe like a sidewinder effect =?
   %testfinal3 = %testfin3 * -1;
   %testfinale3 = %testfinal3 + %test3;

   %haxordir = %testfinale SPC %testfinale2 SPC %testfinale3; //final dir.. .....

   %flare = new (FlareProjectile)() {
      dataBlock        = GuardianFlare;
      initialDirection = %haxordir;
      initialPosition  = %projpos;
      sourceslot = %player;
   };
   %flare.sourceobject = %player;
   MissionCleanup.add(%flare);

   %flare.seeksched = schedule(100, 0, "guardianFlare", %player, %flare);
}

//* Hologram
//* $10,000,000
//* False player images follow your every move.

function doHologramLoop(%client) {
   if(%client.player.IsAlive() && %client.getActiveAE() $= "Hologram") {
      %race = %client.race;
      if(%race $= "Bioderm") {
         createBiodermProjection(%client.player);
      }
      else {
         createHumanProjection(%client.player);
      }
      //schedule next hologram
      schedule(1250, 0, "doHologramLoop", %client);
   }
}




























//******************************************************************************
function GameConnection::loadSlotMachine(%client, %index, %tag) {
   if(getSimTime() < %client.store.nextSlot) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "You are currently locked out of the slot machine, try back later.");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tStore>Return To Store</a>');
      %index++;
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "***************************");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "** LINE EM UP BONUS SLOT **");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "**  LINE UP 3 TO SCORE   **");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "**  THREE SEVENS BONUS   **");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "***************************");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tSlotMachine>[$50] PLAY MACHINE</a>');
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tStore>Return To Store</a>');
      %index++;
   }
   return %index;
}

function GameConnection::generateSlotMachineRoll(%client, %index, %tag) {
   %client.TWM2Core.money -= 50; //start cost
   for(%i = 1; %i <= 9; %i++) {
      %roll[%i] = getRandom(1, 7);
      %rollStr = %rollStr TAB %roll[%i];
   }
   messageClient( %client, 'SetLineHud', "", %tag, %index, "* "@%roll[1]@" == "@%roll[2]@" == "@%roll[3]@" *");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "* "@%roll[4]@" == "@%roll[5]@" == "@%roll[6]@" *");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "* "@%roll[7]@" == "@%roll[8]@" == "@%roll[9]@" *");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "***************************");
   %index++;
   //
   %client.TWM2Core.money += getSlotWinnings(%rollStr);
   messageClient( %client, 'SetLineHud', "", %tag, %index, "* WINNINGS: $"@getSlotWinnings(%rollStr)@"");
   %index++;
   //
   %client.store.nextSlot = getSimTime() + 25000;  //25 seconds between runs
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tStore>Return To Store</a>');
   %index++;
   return %index;
}

function getSlotWinnings(%string) {
   %winning = 0;
   //tear the string
   for(%i = 0; %i < getFieldCount(%string); %i++) {
      %roll[%i+1] = getField(%string, %i);
   }
   //check for winning spins
   for(%x = 1; %x <= 3; %x++) {
      %one = ((%x-1)*3) + 1;
      %two = ((%x-1)*3) + 2;
      %three = ((%x-1)*3) + 3;
      //*FIRST CASE: FULL CROSS: 5 5 5
      if(%roll[%one] $= %roll[%two] && %roll[%two] $= %roll[%three]) {
         %winning += 100;
         if(%roll[%one] $= "7" && %roll[%two] $= "7" && %roll[%three] $= "7") {
            //jackpot !
            %winning += 9000;
         }
      }
   }
   //*SECOND CASE: Cross Diagonal X
   //Only two possiblities here, I'll just if it.
   if(%roll[1] $= %roll[5] && %roll[5] $= %roll[9]) {
      %winning += 500;
      if(%roll[1] $= "7" && %roll[5] $= "7" && %roll[9] $= "7") {
         %winning += 45000;
      }
   }
   if(%roll[3] $= %roll[5] && %roll[5] $= %roll[7]) {
      %winning += 500;
      if(%roll[3] $= "7" && %roll[5] $= "7" && %roll[7] $= "7") {
         %winning += 45000;
      }
   }
   //
   return %winning;
}

//******************************************************************************
