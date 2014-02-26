// ---- PGD Loader -------------------------------------------------------------
function LoadUniversalBuilding(%client, %file) {
   %server = ""@$PGDServer@":"@$PGDPort@"";
   %filename = "/public/Univ/Data/"@%client.guid@"/Buildings/"@%file@".cs";
   if (!isObject(Univ_Loader)) {
      %Downloader = new HTTPObject(Univ_Loader){};
   }
   else {
      %Downloader = Univ_Loader;
   }
   //If the server crashes here, let everyone know why
   MessageAll('MsgAdminForce', "\c3"@%client.namebase@" is loading a universally saved building.");
   echo("Client:" SPC %client.namebase SPC "requests universal load.");
   %Downloader.client = %client;
   %Downloader.load = %file;
   %Downloader.valid = 1;
   $PGDBuffer[%client, %file] = "";
   %Downloader.get(%server, %filename);
}

function Univ_Loader::onLine(%this, %line) {
   %validity = ScanForValidLine(%line);
   if(!%validity) {
      messageClient(%this.client, 'MsgClient', "\c5PGD: ERROR, you are requesting a corrupted file.");
      messageClient(%this.client, 'MsgClient', "\c5Corrupted files contain custom content not signed by the server.");
      messageClient(%this.client, 'MsgClient', "\c5ABORTING CONNECTION.");
      %this.valid = 0;
      %this.disconnect();
      return;
   }
   $PGDBuffer[%this.client, %this.load] = $PGDBuffer[%this.client, %this.load] @ "\n" @ %line;
}

function Univ_Loader::onConnectFailed(%this) {
   error("-- Could not connect to PGD.");
   messageClient(%this.client, 'MsgClient', "\c5PGD: Your Building could not be loaded, the server may be offline or going through maintenance.");
   error("Universal Load: fail (connection)");
}

function Univ_Loader::onDisconnect(%this) {
   if(!%this.valid) {
      echo("Universal Load: Corrupt File");
      $PGDBuffer[%this.client, %this.load] = ""; //clear the buffer.
      %this.delete();
      return;
   }
   eval($PGDBuffer[%this.client, %this.load]); //evaluate the buffer here, which will make things much faster.
   $PGDBuffer[%this.client, %this.load] = ""; //clear the buffer.
   echo("Universal Load: OK");
   %this.delete();
}
