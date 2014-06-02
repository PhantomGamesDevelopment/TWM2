//Support Script, Allows this to work
//Updated 2.1, Signal360's Changes implemented
//Universal Ranks Implemented

//We keep these files hidden so no outsiders can fuck with our stuff
$PGDPHPUploadHandler = "/public/Univ/Buildings/upload.php"; //no touchy
$PGDPHPDelHandler = "/public/Univ/Buildings/delete.php?Filetokill=";
$PGDKeyHandler = "/public/Univ/Ranks/key.php";
$PGDEMailHandler = "/public/Univ/Ranks/antiTamperDispatch.php";

$PGDPHPRankUploadHandler = "/public/Univ/Ranks/upload.php";

$PGDPort = 80; //TCP
$PGDServer = "www.phantomdev.net";

//PGD IS FILE
function PGD_IsFile(%file) {
   if($TWM2::PGDConnectDisabled) {
      echo("PGD Connect is disabled.");
      return false;
   }
   if($PGD::IsFile[%file] $= "" || $PGD::IsFile[%file] == -1) {
      PGD_IsFileDL(%file);
      return schedule(5000, 0, "PGD_IsFile", %file);
   }
   else {
      return $PGD::IsFile[%file];
   }
}

function PGD_IsFileDL(%file) {
   if($TWM2::PGDConnectDisabled) {
      echo("PGD Connect is disabled.");
      return;
   }
   %server = ""@$PGDServer@":"@$PGDPort@"";
   %filename = "/public/Univ/IsFile.php?File="@%file@"";
   if (!isObject(PGDISFile)) {
      %Downloader = new HTTPObject(PGDISFile){};
   }
   else {
      %Downloader = PGDISFile;
   }
   %Downloader.File = %file;
   echo("Getting");
   %Downloader.get(%server, %filename);
}

function PGDISFile::onLine(%this, %line) {
   echo(%line);
   if(strStr(%line, "Not") != -1) {
      $PGD::IsFile[%this.File] = 0;
      %this.schedule(1000, disconnect);
      %this.schedule(1500, delete);
   }
   else {
      $PGD::IsFile[%this.File] = 1;
      %this.schedule(1000, disconnect);
      %this.schedule(1500, delete);
   }
}

function PGDISFile::onConnectFailed(%this) {
   error("-- Could not connect to PGD Is File, re-attempt, 30 sec.");
   $PGD::IsFile[%this.File] = -1;
   //
   schedule(30000, 0, "PGD_IsFile", %this.File);
   %this.disconnect();
   %this.delete();
}

function PGDISFile::onDisconnect(%this) {

}

//END PGD IS FILE

function GetFileLength(%file) {
   new fileobject(LengthReader);
   LengthReader.openforread(%file);
   %bool = 0;
   while(!%bool) {
      %bool = LengthReader.isEOF();
      %Msg = LengthReader.readLine();
      $message = ""@$message@"\n"@%Msg@"";
   }
   %count = strLen($message);
   $message = "";
   return %count;
}

function getFileContents(%file) {
   new fileobject(filereader);
   filereader.openforread(%file);
   %bool = 0;
   while(!%bool) {
      %bool = filereader.isEOF();
      %Msgget = filereader.readLine();
      %msg = ""@%msg@""NL""@%Msgget@"";
   }
   return %msg;
}

//added 2.4
//Prevent custom (unwanted) content in universal loads
function ScanForValidLine(%line) {
   if (getsubstr(%line, 0, 2) $= "//") {
      //commented lines like this cannot possibly deliver custom content.
      //thus they must be alloted.
      return 1;
   }
   else if(getsubstr(trim(%line), 0, 1) $= "") {
      //Blank lines are completely harmless
      return 1;
   }
   else if(getsubstr(%line, 0, 29) $= "%building = new (StaticShape)" ||
      getsubstr(%line, 0, 32) $= "%building = new (ForceFieldBare)" ||
         getsubstr(%line, 0, 24) $= "%building = new (Turret)" ||
            getsubstr(%line, 0, 30) $= "%building = new (BeaconObject)") {
            //this is our official line check, if it's a building DB line
            //it is safe, and valid.
            return 1;
   }
   else {
      //this line has been tampered with, and is invalid.
      return 0;
   }
}
