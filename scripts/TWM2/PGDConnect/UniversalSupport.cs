//UniversalSupport.cs
//PGD Connect support functions library
//TWM2 3.9a Update, Library style function and new validation methods
//TWM2 2.1 Update, Signal360's Changes implemented

//We keep these files hidden so no outsiders can mess with our stuff
$PGDPHPUploadHandler = "/public/Univ/Buildings/upload.php"; //no touchy
$PGDPHPDelHandler = "/public/Univ/Buildings/delete.php?Filetokill=";
$PGDKeyHandler = "/public/Univ/Ranks/key.php";
$PGDEMailHandler = "/public/Univ/Ranks/antiTamperDispatch.php";

$PGDPHPRankUploadHandler = "/public/Univ/Ranks/upload.php";

$PGDPort = 80; //TCP
$PGDServer = "www.phantomdev.net";

function TWM2Lib_PGDConnect_Support(%functionName, %arg1, %arg2, %arg3, %arg4) {
    switch$(strlwr(%functionName)) {
        case "isserverfile":
            if ($TWM2::PGDConnectDisabled) {
                echo("PGD Connect is disabled.");
                return false;
            }
            %file = %arg1;
            if ($PGD::IsFile[%file] $= "" || $PGD::IsFile[%file] == -1) {
                TWM2Lib_PGDConnect_Support("performFileCheck", %file);
                return schedule(5000, 0, "TWM2Lib_PGDConnect_Support", "isServerFile", %file);
            }
            else {
                return $PGD::IsFile[%file];
            }

        case "performfilecheck":
            if ($TWM2::PGDConnectDisabled) {
                echo("PGD Connect is disabled.");
                return;
            }
            %file = %arg1;
            %server = $PGDServer@":"@$PGDPort;
            %filename = "/public/Univ/IsFile.php?File="@%file;
            if (!isObject(PGDISFile)) {
                %Downloader = new HTTPObject(PGDISFile) { };
            }
            else {
                %Downloader = PGDISFile;
            }
            %Downloader.File = %file;
            echo("Connecting to PGD, testing file "@ %file);
            %Downloader.get(%server, %filename);

        case "filelength":
            %file = %arg1;
            new fileobject(LengthReader);
            LengthReader.openforread(%file);
            %bool = 0;
            while (!%bool) {
                %bool = LengthReader.isEOF();
                %Msg = LengthReader.readLine();
                $message = $message@"\n"@%Msg;
            }
            %count = strLen($message);
            $message = "";
            return %count;

        case "filecontents":
            %file = %arg1;
            new fileobject(filereader);
            filereader.openforread(%file);
            %bool = 0;
            while (!%bool) {
                %bool = filereader.isEOF();
                %Msgget = filereader.readLine();
                %msg = %msg @ "\n" @ %Msgget;
            }
            return %msg;

        case "filevalidator_building":
            %line = %arg1;
            if (getsubstr(%line, 0, 2) $= "//") {
                //commented lines like this cannot possibly deliver custom content.
                //thus they must be alloted.
                return 1;
            }
            else if (getsubstr(trim(%line), 0, 1) $= "") {
                //Blank lines are completely harmless
                return 1;
            }
            else if (getsubstr(%line, 0, 29) $= "%building = new (StaticShape)" ||
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

        case "filevalidator_rank":
            %line = %arg1;
            %trimmed = strlwr(stripChars(trim(%line), " "));
            if(getsubstr(%trimmed, 0, 7) $= "phrase=" || getsubstr(%trimmed, 0, 2) $= "//") {
                return 1;
            }
            else {
                if (!strStr(%line, "function") || !strStr(%line, "eval") || !strStr(%line, "call") || !strStr(%line, "schedule")) {
                    return 0;
                }
                return 1;
            }

        default:
            error("TWM2Lib_PGDConnect_Support(): error, unknown function "@ %functionName @" called.");
    }
}

//PGDISFILE Object Functions
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
//END