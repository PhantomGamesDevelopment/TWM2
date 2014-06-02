// ============================================================
// Project            :  TWM2
// File               :  .\scripts\TWM2\PGDConnect\ServerInteraction.cs
// Copyright          :  2010, Phantom Games Development
// Author             :  Robert Fritzen (Phantom139)
// Created on         :  Tuesday, November 02, 2010 9:15 AM
//
// Editor             :  TorqueDev v. 1.2.3430.42233
//
// Description        :  Handles the server interactions with PGD
//                    :  Servers: [CORE] [SATELITE]
// ============================================================

$Generic_Rank_Cap      = 75000;          //if we cannot get a valid connection
$TWM2Core_Interface    = "www.phantomdev.net" TAB "www.tacticaluprising.phantomdev.net"; //don't touch, server connections
$TWM2ServerInfo_Loc    = "/ssiInterface.php";

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
      case "SetEXPCap":
	     $EXPCap[$TWM2Core_Code, sha1sum(formattimestring("yymmdd"))] = getWord(%line, 1);
		 echo("PGD: Daily Rank Cap Has Been Set To: "@getWord(%line, 1)@"");
		 for(%i = 0; %i < ClientGroup.getCount(); %i++) {
	        %client = ClientGroup.getObject(%i);
			%client.TWM2Core.noMoreEXP[sha1sum(formattimestring("yymmdd"))] = 0;
	     }
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
	     $EXPMulti[$TWM2Core_Code, formattimestring("yymmdd"), sha1sum($TWM2Core_Code TAB FormatTWM2Time(formattimestring("yymmdd")))] = getWord(%line, 1);
		 echo("PGD: EXP Multiplier is now: "@getWord(%line, 1)@"");			
   }
}
