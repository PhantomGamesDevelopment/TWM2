//ServerInteraction.cs
//Updated TWM2 3.9a, removed depricated EXP Cap commands

$TWM2Core_Interface    = "www.phantomdev.net" TAB "www.public.phantomdev.net"; //don't touch, server connections
$TWM2ServerInfo_Loc    = "/Univ/ssiInterface.php";

//connects to the server
function establishPGDConnection() {
   if($TWM2::PGDConnectDisabled) {
      echo("PGD Connect is disabled.");
      return;
   }
   echo("ServerInteraction Initiated... wait for connection...");
   if (!isObject(serverInterfacing)) {
      %connection = new TCPObject(serverInterfacing);
   }
   else {
      %connection = serverInterfacing;
   }
   %connection.connect(getField($TWM2Core_Interface, 1) @ ":" @ $PGDPort);
   //set the code!
   $TWM2Core_Code = sha1sum($TWM2Core_Interface TAB formattimestring("yymmdd"));
   //schedule next interaction with the server
   schedule(60000 * 30, 0, "establishPGDConnection"); //every 30 minutes is good.
}

function establishPGDConnection_manual() {
   echo("ServerInteraction Initiated... wait for connection...");
   if (!isObject(serverInterfacing)) {
      %connection = new TCPObject(serverInterfacing);
   }
   else {
      %connection = serverInterfacing;
   }
   %connection.connect(getField($TWM2Core_Interface, 1) @ ":" @ $PGDPort);
   //set the code!
   $TWM2Core_Code = sha1sum($TWM2Core_Interface TAB formattimestring("yymmdd"));
}

//
function serverInterfacing::onConnected(%this) {
   echo("Connection established with Phantom Games Development (www.phantomdev.net)");
   %this.schedule(15000, "disconnect");
   %loc = $TWM2ServerInfo_Loc;
   %query = "GET" SPC %loc SPC "HTTP/1.1\x0aHost: "@getField($TWM2Core_Interface, 1)@"\r\n\r\n";

   echo("Sending Request Query to Phantom Games Server.");
   %this.send(%query);
}

function serverInterfacing::onLine(%this, %line) {
	if (trim(%line) $= "") {       //is the line a HTTP header?
		if (!%this.readyToRead) {
			%this.readyToRead = true;
		}
	}
	if(!%this.readyToRead) {
		return; //we have no use for this.
	}
	//read necessary data
	switch$(getWord(%line, 0)) {
		case "ApplyDevList":
			%list = getWords(%line, 1);
			%list = strreplace(%list, "TAB", "\t"); //boom! 
			for(%i = 0; %i < getFieldCount(%list); %i++) {
				%FieldGUID = getSubStr(getField(%list, %i), 0, strstr(getField(%list, %i), ":"));
				%FieldLEVEL = getSubStr(getField(%list, %i), strLen(%FieldGUID) + 1, strLen(getField(%list, %i)));
				$DeveloperList[%i] = %FieldGUID;
				$DeveloperLevel[%i] = %FieldLEVEL;
				echo("Developers "@%i@": "@$DeveloperList[%i]@" -> "@$DeveloperLevel[%i]@"");
			}
		
		case "SetHighRank":
			$RankCap[$TWM2Core_Code, sha1sum(formattimestring("yymmdd"))] = getWord(%line, 1);
			echo("PGD: Highest Rank Set To "@getWord(%line, 1)@"");
		
		case "SetHighOfficer":
			$OfficerCap[$TWM2Core_Code, sha1sum(formattimestring("yymmdd"))] = getWord(%line, 1);
			echo("PGD: Highest Officer Rank Set To "@getWord(%line, 1)@"");	
		
		case "SetEXPMultiplier":
			$EXPMulti[$TWM2Core_Code, formattimestring("yymmdd"), sha1sum($TWM2Core_Code TAB TWM2Lib_MainControl("FormatTWM2Time" , formattimestring("yymmdd")))] = getWord(%line, 1);
			echo("PGD: EXP Multiplier is now: "@getWord(%line, 1)@"");
		
		default:
			echo("PGD: Depricated command "@getWord(%line, 0)@" issued, ignored...");			
	}
}
