//SuccessiveKills.cs
//Phantom139

//Handles the successive and specific weapon kill listing

function PerformSuccessiveKills(%player, %damageType) {
   if(!isObject(%player) || !%player.isAlive()) {
      return;
   }	
   cancel(%player.successiveReset);
   %currentKills = %player.killsinarow2;
   %currentDTKills = %player.kills[%damageType];
   %player.successive++;
   //
   if(%player.successive >= 2) {
      DoSuccessivePrint(%player);
   }
   DoStreakPrint(%player, %currentKills, %damageType);
   //
   %player.successiveReset = schedule(4000, 0, "ResetSuccessive", %player);
}

function ResetSuccessive(%player) {
   if(!isObject(%player) || !%player.isAlive()) {
      return;
   }
   %player.successive = 0;
}

function DoStreakPrint(%player, %kills, %damageType) {
   %client = %player.client;
   recordAction(%client, "SKSC", %kills@"\t1");
   switch(%kills) {
      case 5:
         BottomPrint(%client, "<color:EE0000><Font:Arial Bold:18>KILLING SPREE", 3, 3);
         GainExperience(%client, 25, "Killing Spree ");
      case 10:
         BottomPrint(%client, "<color:EE0000><Font:Arial Bold:18>SERIAL KILLER", 3, 3);
         GainExperience(%client, 50, "Serial Killer ");
      case 15:
         BottomPrint(%client, "<color:EE0000><Font:Arial Bold:18>MASS MURDERER", 3, 3);
         GainExperience(%client, 100, "Mass Murderer ");
      case 20:
         BottomPrint(%client, "<color:EE0000><Font:Arial Bold:18>GOD LIKE", 3, 3);
         GainExperience(%client, 250, "God Like ");
      case 25:
         BottomPrint(%client, "<color:EE0000><Font:Arial Bold:18>IMMORTAL", 3, 3);
         GainExperience(%client, 500, "Immortal ");
   }
}

function DoSuccessivePrint(%player) {
   if(!isObject(%player) || !%player.isAlive()) {
      return;
   }	
   %client = %player.client;
   messageClient(%client, 'msgSoundFX', "~wfx/misc/MA1.wav");
   recordAction(%client, "SKC", %player.successive@"\t1");
   switch(%player.successive) {
      case 2:
         CenterPrint(%client, "<color:EE0000><Font:Arial Bold:18>DOUBLE KILL", 3, 3);
      case 3:
         CenterPrint(%client, "<color:EE0000><Font:Arial Bold:18>TRIPLE KILL", 3, 3);
         GainExperience(%client, 15, "Triple Kill Bonus ");
      case 4:
         CenterPrint(%client, "<color:EE0000><Font:Arial Bold:18>OVERKILL", 3, 3);
      case 5:
         CenterPrint(%client, "<color:EE0000><Font:Arial Bold:18>KILLING FRENZY", 3, 3);
         GainExperience(%client, 50, "Killing Frenzy Bonus ");
      case 6:
         CenterPrint(%client, "<color:EE0000><Font:Arial Bold:18>KILLTACULAR", 3, 3);
      case 7:
         CenterPrint(%client, "<color:EE0000><Font:Arial Bold:18>KILLTROCITY", 3, 3);
      case 8:
         CenterPrint(%client, "<color:EE0000><Font:Arial Bold:18>KILLTACULAR", 3, 3);
         GainExperience(%client, 250, "Killtacular Bonus ");
      case 9:
         CenterPrint(%client, "<color:EE0000><Font:Arial Bold:18>KILLPOCALYPSE", 3, 3);
      case 10:
         CenterPrint(%client, "<color:EE0000><Font:Arial Bold:18>ANNIHILIATOR", 3, 3);
         GainExperience(%client, 1000, "Annihilator Bonus ");
      default:
         CenterPrint(%client, "<color:EE0000><Font:Arial Bold:18>INSANITY - "@%player.successive@" SUCCESSIVE", 3, 3);
   }
}
