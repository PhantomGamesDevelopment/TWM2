//UniversalRanks.cs
//Coded: 1-2-10
//By: Phantom139, And alot of improvements by Signal360

//This script handles all TCP Object related code for the Universal Rank System
//It will access the server to check for a universal rank file, if it exists, it
//deletes the local file and downloads the unioversal one for use, if it does not
//exist it scans the server to see if it is a "Main" Server. If the server is "Main",
//The rank is saved to the universal server for usage in other servers.

//------------------------------------------------------------------------
//This first part of the script is the IsServerMain Handler.
//It Downloads the server key from PGD and verifies it to the key
//The Server Host Specifies.
//------------------------------------------------------------------------
$ServerType = "Satellite Server";
$PGDDebugMode = 0;
function currentEpochTime() {
	rubyEval("tsEval '$temp=' + Time.now.to_i.to_s + ';'");
	return $temp;
}

function CreateHash(%user, %password, %name) {
   //%salt  = base64_encode(formatTimeString("dd.mm.yy-nn")); //prevent a replay attack?
   %epoch = currentEpochTime();   //now using epoch time for a better salt
   %salt = base64_encode(getSubStr(%epoch, 0, strLen(%epoch)-3)); // removing last second to prevent problems
   %nonce = sha1sum(%user @ %name);                               // if there is a small delay, it also means
   %str = %user @ ":" @ %password;                                // the hash will be different every 10 secs.
   %hash = sha1sum(%nonce @ sha1sum(%str) @ %salt);
   if($PGDDebugMode == 1) {
      MessageDevs("\c3Epoch: "@getSubStr(%epoch, 0, strLen(%epoch)-2)@"");
      MessageDevs("\c3Salt: "@%salt@"");
      MessageDevs("\c3Nonce: "@%nonce@"");
      MessageDevs("\c3Hash: "@%hash@"");
   }
   return %hash;
}

function IsServerMain() {
   if($ServerType $= "Core Server")
      return 1;
   else
      return 0;
}
//function GetKey(){CheckCore();}

function CheckCore() {
   echo("*PGD: Performing Server Core Status Check");
   $TCP::ConnectionContainer.addTaskToList($PGDServer,
                                           $PGDKeyHandler,
                                           "generatePGDCoreCheck",
                                           "");
}

function TCPConnectionList::generatePGDCoreCheck(%this) {
   echo("*Generating Request");
   %user     = getField($TWM2::PGDCredentials, 0);
   %password = getField($TWM2::PGDCredentials, 1);
   %name     = sha1Sum($Host::GameName);
   %hash     = CreateHash(%user, %password, %name);

   %this.getRandomSeperator(16);
   %header = %this.assembleHTTP1_1Header("POST", "Tribes 2", "Content-Type: multipart/form-data; boundary="@%this.currentSeparator@"\r\n");
   %dispo = %this.makeDisposition(user, %user);
   %dispo = %dispo @ %this.makeDisposition(hash, %hash);
   %dispo = %dispo @ %this.makeDisposition(gname, %name, 1);

   %header = %header @ "Content-Length: " @ strLen(%dispo) @ "\r\n\r\n";

   $TCP::ConnectionObject.serverIndex = %counter;
   $TCP::ConnectionObject.lineEval = "validatePGDCore";
   return %header @ %dispo;
}

function PGDConnection::validatePGDCore(%this, %line) {
   if(strStr(%line, "pgd_debug") != -1) {
      if($PGDDebugMode == 1) {
         MessageDevs("\c5PGD DEBUG:" SPC %line);
      }
   }
   if (trim(%line) $= "yes")
      $ServerType = "Core Server";
   else if (trim(%line) $= "no")
      $ServerType = "Satellite Server";
}

//------------------------------------------------------------------------
//This second part of the script handles the downloading of existing files
//It uses the PGDISFile from UniversalSupport.cs as well as scans for a current
//file.
//------------------------------------------------------------------------

function LoadUniversalRank(%client) {
   //A Little PGD Connect Ad.
   %client.donotupdate = 1;
   if(!%client.IsPGDConnected()) {
      %client.donotupdate = 0;
      messageClient(%client, 'msgPGDRequired', "\c5PGD: PGD Connect account required to load universal ranks.");
      messageClient(%client, 'msgPGDRequired', "\c5PGD: Sign up for PGD Connect today, It's Fast, Easy, and FREE!");
      messageClient(%client, 'msgPGDRequired', "\c5See: www.public.phantomdev.net/SMF/ in the PGD Section");
      messageClient(%client, 'msgPGDRequired', "\c5For more details.");
      schedule(500, 0, "LoadClientRankfile", %client);
      return 1;
   }
   //IS FILE
   if(!PGD_IsFile("Data/"@%client.guid@"/Ranks/TWM2/Saved.TWMSave")) {
      %client.donotupdate = 0;
      messageClient(%client, 'msgPGDRequired', "\c5PGD: PGD Connect confirms you do not have a universal rank.");
      messageClient(%client, 'msgPGDRequired', "\c5PGD: Play on a main server to start progressing one today!");
      messageClient(%client, 'msgPGDRequired', "\c5PGD: Loading your local rank file for the time being...");
      schedule(500, 0, "LoadClientRankfile", %client);
      return 1;
   }
   //Passed, we have a universal rank, time to loady :)
   MessageClient(%client, 'msgAccess', "\c5PGD: Universal Rank Load Requested, adding to connection queue, please standby.");
   echo("Client:" SPC %client.namebase SPC "needs universal rank load. It will be stored locally.");
   //Cache Create
   %file = "/public/Univ/Data/"@%client.guid@"/Ranks/TWM2/Saved.TWMSave";
   $HTTP::ConnectionObject.client = %client;
   $HTTP::ConnectionObject.finishFunction = "onCompleteRankDownload";
   $TCP::ConnectionContainer.addTaskToList($PGDServer,
                                           %file,
                                           "http",
                                           "");
}

function PGDConnection_HTTP::onCompleteRankDownload(%this) {
   echo("download complete... evaluating and applying rank");
   %client = %this.client;

   %fileO = new FileObject();
   %fileO.openForWrite($TWM::RanksDirectory@"/"@%this.client.guid@"/Saved.TWMSave");
   for (%i = 0; %i < $Buffer[%this]; %i++) {
      %fileO.writeLine($Buffer[%this, %i]);
      $Buffer[%this, %i] = "";
   }
   $Buffer[%this] = 0;
   %fileO.close();
   %fileO.delete();
   
   schedule(100, 0, LoadClientRankFile, %client);
   
   messageClient(%client, 'msgComplete', "\c3PGD: Your rank has been successfully downloaded.");
}

//------------------------------------------------------------------------
//This third part of the script handles the uploading of rank files
//It uses the IsServerMain above to check if the server is "Allowed" to upload
//rank files. If it can, it handles the uploading.
//------------------------------------------------------------------------

function PrepareUpload(%client) {
   if (!isServerMain()) {
       return;
   }
   if(!%client.IsPGDConnected()) {
      messageClient(%client, 'msgPGDRequired', "\c5PGD: PGD Connect account required to perform this action.");
   }
   else {
      messageClient(%client, 'msgPGDRequired', "\c5PGD: Adding your upload request to the connection queue list.");
      $TCP::ConnectionContainer.client = %client;
      $TCP::ConnectionContainer.addTaskToList($PGDServer,
                                              $PGDPHPRankUploadHandler,
                                              "GeneratePGDUploadRequest",
                                              "");
   }
}

function TCPConnectionList::GeneratePGDUploadRequest(%this) {
   //pre-generation stuff
   
   %client = %this.client;
   %file = $TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave";
   %fileBase = FileBase(%file) @ ".TWMSave";
   %fileCont = getFileContents(%file);
   
   %user     = getField($TWM2::PGDCredentials, 0);
   %password = getField($TWM2::PGDCredentials, 1);
   %name     = sha1Sum($Host::GameName);
   %hash     = CreateHash(%user, %password, %name);

   %this.getRandomSeperator(16);
   %header = %this.assembleHTTP1_1Header("POST", "Tribes 2", "Content-Type: multipart/form-data; boundary="@%this.currentSeparator@"\r\n");
   %dispo = %this.makeDisposition(guid, %client.guid);
   %dispo = %dispo @ %this.makeDisposition(user, %user);
   %dispo = %dispo @ %this.makeDisposition(hash, %hash);
   %dispo = %dispo @ %this.makeDisposition(gname, %name);
   %dispo = %dispo @ %this.makeDisposition(version, $TWM2::Version);
   %dispo = %dispo @ %this.makeDisposition(mod, "TWM2");
   %dispo = %dispo @ %this.makeUploadDisposition(uploadedfile, %fileBase, %fileCont, 1);

   %header = %header @ "Content-Length: " @ strLen(%dispo) @ "\r\n\r\n";

   $TCP::ConnectionObject.client = %client;
   $TCP::ConnectionObject.lineEval = "evalRankUpload";
   return %header @ %dispo;
}

//Handle Errors
function PGDConnection::evalRankUpload(%this, %line) {
   if($PGDDebugMode) 
      echo(%line);
   %ok = false;
   if(strStr(%line, "pgd_ban") != -1) {
      messageClient(%this.client, 'msgPGDRequired', "\c2PGD: You are banned. \c3"@%line@".");
   }
   if(strStr(%line, "pgd_debug") != -1) {
      if($PGDDebugMode == 1) {
         MessageDevs("\c5PGD DEBUG:" SPC %line);
      }
   }
   switch$ (%line) {
      case "file_upload_ok":
           %ok = true;
           messageClient(%this.client, 'msgdone', "\c5PGD: Your Rank was saved successfully.");
      case "not_registered":
           messageClient(%this.client, 'msgPGDRequired', "\c5PGD: PGD Connect account required to perform this action.");
      case "incompatible_version":
           error("PGD: This version is no longer supported by PGD Ranks");
           messageClient(%this.client, 'msgPGDRequired', "\c5PGD: This version of TWM2 is no longer supported.");
      case "invalid_hash":
           messageClient(%this.client, 'msgPGDRequired', "\c5PGD: The authentication hash sent to the server"NL
                                                         "was not valid. (invalid characters, or no input!)");
           $ServerType = "Satellite Server";
      case "incorrect_hash":
           messageClient(%this.client, 'msgPGDRequired', "\c5PGD: The authentication hash sent to the server"NL
                                                         "was not correct.");
           $ServerType = "Satellite Server";
      case "invalid_guid":
           messageClient(%this.client, 'msgPGDRequired', "\c5PGD: Your GUID had invalid characters in it,"NL
                                                         "try again, when it hasn't been tampered with >_>");
      case "no_guid_input":
           messageClient(%this.client, 'msgPGDRequired', "\c5PGD: No GUID was sent to the server. Try again!");
      case "bad_user":
           messageClient(%this.client, 'msgPGDRequired', "\c5PGD: The username for this server was not filled"NL
                                                         "in, or it had invalid characters in it.");
           $ServerType = "Satellite Server";
      case "bad_version_input":
           messageClient(%this.client, 'msgPGDRequired', "\c5PGD: The version field was not filled in.");
	  case "save_denied":
	       messageClient(%this.client, 'msgPGDRequired', "\c5PGD: The server has denied your file save request.");
      case "sql_error":
           messageClient(%this.client, 'msgPGDRequired', "\c5PGD: There is a problem with the SQL database on the server!");
      default:
           return;
   }
   if ( %ok )
      echo( "Universal Rank Save: OK" );
   else
      error( "Universal Rank Save: fail (" @ %line @ ")" );
}
