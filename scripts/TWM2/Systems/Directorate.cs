//Directorate.cs
//Phantom139, TWM2 3.6

//Handles all client connection operations when they join

function ClientContainer(%client) {
   //
   echo("*Creating/Loading Client Container For "@%client@"/"@%client.guid@"");
   //
   %name = "Container_"@%client.guid;
   %check = nameToID(%name);
   if(isObject(%check)) {
      echo("*Container Found for "@%client@", applying");
      %client.container = %check;
   }
   else {
      %client.container = new SimSet(%name) {};
   }
}

//brand new saver, saves the client's file container, which in turn will save all other stuffs.
function SaveClientFile(%client) {
   if(!ClientGroup.isMember(%client)) {
      return;
   }
   echo("Saving "@%client.namebase@"'s File");
   %file = ""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave";
   %client.container.save(%file);
}

function LoadClientFile(%client) {
   %file = ""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave";
   exec(%file);
   ClientContainer(%client);
   Patch35FileTo36(%client); //check if they need to be patched to the new 3.6 system
   //
   loadChallengeData(%client);
   loadSettings(%client);
   %client.storeCreate();
}

//TWM2 3.5 -> 3.6
function Patch35FileTo36(%client) {
   %container = %client.container;
   %rank = NameToID("TWM2Client_"@%client.guid);
   %chal = NameToID("CCD_"@%client.guid);
   %sett = NameToID("ClientSettings"@%client.guid);
   //begin patch
   if(!%container.isMember(%rank)) {
      %container.add(%rank);
   }
   if(!%container.isMember(%chal)) {
      %container.add(%chal);
   }
   if(!%container.isMember(%sett)) {
      %container.add(%sett);
   }
   //save new file
   SaveClientFile(%client);
}
