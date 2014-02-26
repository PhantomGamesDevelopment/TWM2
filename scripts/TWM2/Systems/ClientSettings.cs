//This Saves A Clients Settings on the Server
//And Then Sets them when a client rejoins

//Phantom139: I have changed this system throughtout for the TWM2 3.4 release
//this new system can now support pretty much ANYTHING that the client can change :)

function UpdateSettings(%client) {
   %name = "ClientSettings"@%client.guid@"";
   %script = new ScriptObject(%name) {};
   //Obtain Perks/Killstreaks
   for(%i = 1; %i <= $Perk::TotalPerks; %i++) {
      if(%client.IsActivePerk($Perk::Perk[%i])) {
         %script.savedperk[%p++] = $Perk::Perk[%i];
      }
   }
   %streaks = %client.GatherActiveStreaks();
   for(%i = 0; %i < getFieldCount(%streaks); %i++) {
      %script.savedstreak[%k++] = getField(%streaks, %i);
   }
   //Obtain Weapon Upgrades
   %img = 0;
   while(isSet($InvWeapon[%img])) {
      %block = $NameToInv[$InvWeapon[%img]].image;
      %script.weaponupgrade[%block] = %client.weaponUpgrade[%block];
      %img++;
   }
   %pistol = 0;
   while(isSet($InvPistol[%pistol])) {
      %block = $NameToInv[$InvPistol[%pistol]].image;
      %script.weaponupgrade[%block] = %client.weaponUpgrade[%block];
      %pistol++;
   }
   //Misc
   
   //Save All Data To Client's File
   if(!%client.container.isMember(%script)) {
      %client.container.add(%script);
   }
   SaveClientFile(%client);
}

//Called at rank load, this applies the correct info
function loadSettings(%client) {
   %name = "Container_"@%client.guid@"/ClientSettings"@%client.guid@"";
   %script = NameToID(%name);
   if(%script == -1) {
      echo("* No Settings For "@%client@"");
   }
   //
   else {
      //activate perks
      %i = 1;
      while(isSet(%script.savedperk[%i])) {
         SetPerkStatus(%client, %script.savedperk[%i], 1);
         %i++;
      }
      //deaactivate base 3 streaks
      %client.setStreakStatus(1, 0);
      %client.setStreakStatus(2, 0);
      %client.setStreakStatus(4, 0);
      //activate killstreaks
      %k = 1;
      while(isSet(%script.savedstreak[%k])) {
         %client.setStreakStatus(%script.savedstreak[%k], 1);
         %k++;
      }
      //upgrades
      %img = 0;
      while(isSet($InvWeapon[%img])) {
         %block = $NameToInv[$InvWeapon[%img]].image;
         %client.ActivateUpgrade(%block, %script.weaponupgrade[%block]);
         %img++;
      }
      %pistol = 0;
      while(isSet($InvPistol[%pistol])) {
         %block = $NameToInv[$InvPistol[%pistol]].image;
         %client.ActivateUpgrade(%block, %script.weaponupgrade[%block]);
         %pistol++;
      }
   }
}
