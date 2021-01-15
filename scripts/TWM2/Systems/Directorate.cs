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
   //
   loadSettings(%client);
}
