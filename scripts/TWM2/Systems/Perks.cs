// Perks.cs
// 3.9.2 =: Major Rework

//PRIMARY - Weapon Modifications
// AP Bullets [c] - Bullets deal 15% increased damage, damage to armored targets increased by 33%
// Handling Expert [c] - Reduces weapon spread by a varying amount (Dependent on the weapon)
// Wind Brake Beacon [c] - Beacon Key Stops Your Fall Instantly
// 3 Second C4 [c] - C4 Detonates in 3 Seconds, not 10
// Martydom [c] - Upon death, drop a live grenade or C4 charge (if available)
// Powder Keg [c] - Explosions deal 25% increased damage

//SECONDARY - Armor And Defense
// Kevlar Armor [c] - Reduce incoming damage from bullets by 20%, explosions by 25%
// Armored Helmet [c] - Removes the headshot multiplier on incoming rounds
// Storm Barrier [c] - Reduce Electrical Damage, blocks lightning
// Lim Zombie Shield [c] - Enhanced your armor with a specialized Lim-Zombie barrier, repelling incoming attackers before requiring a recharge
// Undead Resistant Plating [c] - Zombie Attacks will not infect you
// Radar Phantom [c] - Jam enemy sensors
// Bomb Shadower [c] - You do not show a Sabotage Waypoint
// Second Chance [c] - Spend a team revive to respawn in horde

//TERTIARY - Assets
// Reflexive Reloader [c] - Reduces weapon reload time by 33%
// UAV Disabler [c] - You will not show on enemy UAVs
// Team Gain [c] - All Teammates within 20M Of the Killer Gain XP
// Bandolier [c] - Doubles your maximum clip capacity
// Hardline [c] - Killstreaks require 1 less kill

$Perk::PerkCount[1] = 6;
$Perk::PerkCount[2] = 8;
$Perk::PerkCount[3] = 5;

$Perk::TotalPerks = $Perk::PerkCount[1] + $Perk::PerkCount[2] + $Perk::PerkCount[3];

$Perk::Perk[1] = "AP Bullets";
$Perk::Perk[2] = "Handling Expert";
$Perk::Perk[3] = "Wind Brake Beacon";
$Perk::Perk[4] = "3 Second C4";
$Perk::Perk[5] = "Martydom";
$Perk::Perk[6] = "Powder Keg";
$Perk::Perk[7] = "Kevlar Armor";
$Perk::Perk[8] = "Armored Helmet";
$Perk::Perk[9] = "Storm Barrier";
$Perk::Perk[10] = "Lim Zombie Shield";
$Perk::Perk[11] = "Undead Resistant Plating";
$Perk::Perk[12] = "Radar Phantom";
$Perk::Perk[13] = "Bomb Shadower";
$Perk::Perk[14] = "Second Chance";
$Perk::Perk[15] = "Reflexive Reloader";
$Perk::Perk[16] = "UAV Disabler";
$Perk::Perk[17] = "Team Gain";
$Perk::Perk[18] = "Bandolier";
$Perk::Perk[19] = "Hardline";

$Perk::PerkToID["AP Bullets"] = 1;
$Perk::PerkToID["Handling Expert"] = 2;
$Perk::PerkToID["Wind Brake Beacon"] = 3;
$Perk::PerkToID["3 Second C4"] = 4;
$Perk::PerkToID["Martydom"] = 5;
$Perk::PerkToID["Powder Keg"] = 6;
$Perk::PerkToID["Kevlar Armor"] = 7;
$Perk::PerkToID["Armored Helmet"] = 8;
$Perk::PerkToID["Storm Barrier"] = 9;
$Perk::PerkToID["Lim Zombie Shield"] = 10;
$Perk::PerkToID["Undead Resistant Plating"] = 11;
$Perk::PerkToID["Radar Phantom"] = 12;
$Perk::PerkToID["Bomb Shadower"] = 13;
$Perk::PerkToID["Second Chance"] = 14;
$Perk::PerkToID["Reflexive Reloader"] = 15;
$Perk::PerkToID["UAV Disabler"] = 16;
$Perk::PerkToID["Team Gain"] = 17;
$Perk::PerkToID["Bandolier"] = 18;
$Perk::PerkToID["Hardline"] = 19;


$Perk::Descrip[1] = "Bullets deal 15% increased damage, damage to armored targets increased by 33%";
$Perk::Descrip[2] = "Reduces weapon spread by a varying amount (Dependent on the weapon)";
$Perk::Descrip[3] = "Instantly Stop a fall with your beacon key";
$Perk::Descrip[4] = "Your C4 Detonates in 3 seconds instead of 10 seconds";
$Perk::Descrip[5] = "Upon death, drop a live grenade or C4 charge (if available)";
$Perk::Descrip[6] = "Explosions deal 25% increased damage";
$Perk::Descrip[7] = "Reduce incoming damage from bullets by 20%, explosions by 25%";
$Perk::Descrip[8] = "Removes the headshot multiplier on incoming rounds";
$Perk::Descrip[9] = "Protects you from electrical attacks";
$Perk::Descrip[10] = "Enhanced your armor with a specialized Lim-Zombie barrier, repelling incoming attackers before requiring a recharge";
$Perk::Descrip[11] = "Prevents you from being infected";
$Perk::Descrip[12] = "Jam enemy sensors";
$Perk::Descrip[13] = "You do not show a Sabotage Waypoint";
$Perk::Descrip[14] = "Spend a team revive to respawn in horde";
$Perk::Descrip[15] = "Reduces weapon reload time by 33%";
$Perk::Descrip[16] = "You will not show on enemy UAVs";
$Perk::Descrip[17] = "Allies near you will gain XP for your kills";
$Perk::Descrip[18] = "Doubles your maximum clip capacity";
$Perk::Descrip[19] = "Killstreaks require 1 less kill to earn";


$Perk::PerkToGroup["AP Bullets"] = 1;
$Perk::PerkToGroup["Handling Expert"] = 1;
$Perk::PerkToGroup["Wind Brake Beacon"] = 1;
$Perk::PerkToGroup["3 Second C4"] = 1;
$Perk::PerkToGroup["Martydom"] = 1;
$Perk::PerkToGroup["Powder Keg"] = 1;
//----------------------------------------
$Perk::PerkToGroup["Kevlar Armor"] = 2;
$Perk::PerkToGroup["Armored Helmet"] = 2;
$Perk::PerkToGroup["Storm Barrier"] = 2;
$Perk::PerkToGroup["Lim Zombie Shield"] = 2;
$Perk::PerkToGroup["Undead Resistant Plating"] = 2;
$Perk::PerkToGroup["Radar Phantom"] = 2;
$Perk::PerkToGroup["Bomb Shadower"] = 2;
$Perk::PerkToGroup["Second Chance"] = 2;
//----------------------------------------
$Perk::PerkToGroup["Reflexive Reloader"] = 3;
$Perk::PerkToGroup["UAV Disabler"] = 3;
$Perk::PerkToGroup["Team Gain"] = 3;
$Perk::PerkToGroup["Bandolier"] = 3;
$Perk::PerkToGroup["Hardline"] = 3;


//Asset Function
function GameConnection::IsActivePerk(%client, %perk) {
   if(%client.ActivePerk[""@%perk@""] == 1) {
      return true;
   }
   else {
      return false;
   }
}

function DisableAllPerkGroup(%client, %PerkGroup) {
   for(%i = 1; %i <= $Perk::TotalPerks; %i++) {
      if(GetPerkGroup(%i) == %PerkGroup) {
         %client.ActivePerk[$Perk::Perk[%i]] = 0;
      }
   }
}

function GetActivePerks(%client) {
   for(%i = 1; %i <= $Perk::TotalPerks; %i++) {
      if(%client.IsActivePerk($Perk::Perk[%i])) {
         if(GetPerkGroup(%i) == 1) {
            %client.Perk[1] = $Perk::Perk[%i];
         }
         else if(GetPerkGroup(%i) == 2) {
            %client.Perk[2] = $Perk::Perk[%i];
         }
         else if(GetPerkGroup(%i) == 3) {
            %client.Perk[3] = $Perk::Perk[%i];
         }
      }
   }
}

function GetPerkGroup(%perkVal) {
   if(%perkVal <= $Perk::PerkCount[1]) {
      return 1;
   }
   else if( %perkVal > $Perk::PerkCount[1] && %perkVal <= $Perk::PerkCount[1] + $Perk::PerkCount[2]) {
      return 2;
   }
   else {
      return 3;
   }
}

function GameConnection::CanUsePerk(%client, %perkVal) {
   %scriptController = %client.TWM2Core;
   %xp = getCurrentEXP(%client);
   switch(%perkVal) {
      case 3:
         if(%client.hasMedal(8)) {
            return true;
         }
         else {
            return false;
         }
      case 9:
         if(%client.hasMedal(9)) {
            return true;
         }
         else {
            return false;
         }
      case 13:
         if(%client.CheckNWChallengeCompletion("3For5Sabo")) {
            return true;
         }
         else {
            return false;
         }
      case 14:
         if(%client.CheckNWChallengeCompletion("ArmyOf50Stopped")) {
            return true;
         }
         else {
            return false;
         }
      default:
         error("TWM2: Perks.cs: Invalid Value Passed to CanUsePerk");
         return false;
   }
}

function CreatePerkMenu(%client, %tag, %index) {
   messageClient( %client, 'SetLineHud', "", %tag, %index, "-* Primary Perks *-");
   %index++;
   for(%i = 1; %i <= $Perk::PerkCount[1]; %i++) {
      if(%client.CanUsePerk(%i)) {
         if(!%client.IsActivePerk($Perk::Perk[%i])) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPerkStatus\t"@$Perk::Perk[%i]@">"@$Perk::Perk[%i]@"</a> - "@$Perk::Descrip[%i]@"");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, ""@$Perk::Perk[%i]@" - ACTIVE - "@$Perk::Descrip[%i]@"");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "This Perk Is Not Availible For Use");
         %index++;
      }
   }
   //
   messageClient( %client, 'SetLineHud', "", %tag, %index, "");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "-* Secondary Perks *-");
   %index++;
   for(%i = $Perk::PerkCount[1] + 1; %i <= $Perk::PerkCount[1]+$Perk::PerkCount[2]; %i++) {
      if(%client.CanUsePerk(%i)) {
         if(!%client.IsActivePerk($Perk::Perk[%i])) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPerkStatus\t"@$Perk::Perk[%i]@">"@$Perk::Perk[%i]@"</a> - "@$Perk::Descrip[%i]@"");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, ""@$Perk::Perk[%i]@" - ACTIVE - "@$Perk::Descrip[%i]@"");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "This Perk Is Not Availible For Use");
         %index++;
      }
   }
   //
   messageClient( %client, 'SetLineHud', "", %tag, %index, "");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "-* Tertiary Perks *-");
   %index++;
   for(%i = $Perk::PerkCount[1] + $Perk::PerkCount[2] + 1; %i <= $Perk::PerkCount[1] + $Perk::PerkCount[2] + $Perk::PerkCount[3]; %i++) {
      if(%client.CanUsePerk(%i)) {
         if(!%client.IsActivePerk($Perk::Perk[%i])) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPerkStatus\t"@$Perk::Perk[%i]@">"@$Perk::Perk[%i]@"</a> - "@$Perk::Descrip[%i]@"");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, ""@$Perk::Perk[%i]@" - ACTIVE - "@$Perk::Descrip[%i]@"");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "This Perk Is Not Availible For Use");
         %index++;
      }
   }
   return %index;
}

function SetPerkStatus(%client, %perk, %status) {
   %client.ActivePerk[%perk] = %status;
   if(%status == 1) {
      MessageClient(%client, 'MsgPerkOn', "TWM2: PERK "@%perk@" ACTIVE");
      //Perk details
      if(%perk $= "Radar Phantom") {
         setTargetSensorData(%client.target, JammerSensorObjectActive);
      }
   }
   else {
      //Perk details
      if(%perk $= "Radar Phantom") {
         setTargetSensorData(%client.target, PlayerSensor);
      }
   }
}

function DoPerksStuff(%client, %player) {
   if(%client.IsActivePerk("Radar Phantom")) {
      setTargetSensorData(%client.target, JammerSensorObjectActive);
   }
   if(%client.IsActivePerk("Wind Brake Beacon")) {
      %client.player.AirBrakes = 3;
   }
   if(%client.IsActivePerk("Lim Zombie Shield")) {
      %client.player.LimHits = 2;
   }   
}



//Perks
//For Lim Zombie
function RepelZombie(%zombie, %player) {
   %tgtPos = %zombie.getWorldBoxCenter();
   %distance2 = VectorDist(%tgtPos, %player.getPosition());
   %distance = mfloor(%distance2);
   %vec = VectorNormalize(VectorSub(%player.getPosition(), %tgtpos));
   %nForce = 2000;                              //buh bye!
   %forceVector = vectorScale(%vec, -1*%nForce);

   %zombie.applyImpulse(%zombie.getPosition(), %forceVector);
   %zombie.playShieldEffect("1 1 1");
}

function resetLimCharges(%player) {
	if(!isObject(%player) || %player.getState() $= "dead") {
	   return;
	}
	%player.LimHits = 2;
}