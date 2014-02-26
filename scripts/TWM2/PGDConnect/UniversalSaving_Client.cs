//Universal Saving System
//By Phantom139

//Client Script
//1. Creates and Manages the connection to the PGD Server.
//2. Handles Clients Trying To Save.
//3. Handles the file to be saved.
//4. Sends the File to the server.
//5. Deletes Saved Files on the Server

function GameConnection::IsPGDConnected(%client) {
   %guid = %client.guid;
   if(!%client) {
      return 0;
   }
   if($PGD::IsPGDConnected[%guid] $= "") {
      %client.CheckPGDConnect();
      return %client.schedule(2000, "IsPGDConnected");
   }
   else {
      return $PGD::IsPGDConnected[%guid];
   }
}
//---------- PGD Saving --------------------------------------------------------

function GameConnection::CheckPGDConnect(%client) {
   %guid = %client.guid;
   %server = ""@$PGDServer@":"@$PGDPort@"";
   if (!isObject(GUIDGrabber))
      %Downloader = new HTTPObject(GUIDGrabber){};
   else %Downloader = GUIDGrabber;
   %filename = "/Univ/IsConnected.php?guid="@%guid@""; //File Location
   %Downloader.guid = %guid;
   %Downloader.get(%server, %filename);
}

function GUIDGrabber::onLine(%this, %line) {
   %txt = deTag(%line);
   if (strstr(%txt, "yes")) {
      $PGD::IsPGDConnected[%this.guid] = 1;
      %this.disconnect();
      %this.schedule(1000, delete);
   }
   else {
      $PGD::IsPGDConnected[%this.guid] = 0;
      %this.disconnect();
      %this.schedule(1000, delete);
   }
}

function Univ_ServerConnect(%client, %file, %svDl) {
   %connection = Univ_SaveClient;
   if(!%client || %client $= "") {
      error("Error: No Client Specified");
      return;
   }
   if(!isFile(%file)) {
      error("Error: File specified does not exist, or is a non permitted file type");
      return;
   }
   //We already have an existing connection, Lets Delete it
   if (isObject(%connection)) {
      %connection.disconnect();
      %connection.delete();
   }
   new TCPObject(%connection);
   %connection.client = %client;
   %connection.guid = %client.guid;
   if(%svDl $= "Save") {
      %connection.save = 1;
      if(!%client.IsPGDConnected()) {
         messageClient(%client, 'msgPGDRequired', "\c5PGD: PGD Connect account required to perform this action.");
         return;
      }
      else {
         %len = GetFileLength(%file);
         %connection.orgfile = %file;
         %connection.file = %file;   //what are we sending?
         %connection.filebase = FileBase(%file) @ ".cs";
      }
      %connection.connect(""@$PGDServer@":"@$PGDPort@"");
   }
   else {
      %connection.fileTag = FileBase(%file); //what are we sending?
      %connection.delete = 1;
      %connection.connect(""@$PGDServer@":"@$PGDPort@"");
   }
}

function Univ_SaveClient::onConnected(%this) {
   %this.schedule(15000, "disconnect");
   if(%this.save == 1) {
      %sep = getRandomSeparator(16);
      %filecont = getFileContents(%this.orgfile);
      %loc = $PGDPHPUploadHandler;
      %header1 = "POST" SPC %loc SPC "HTTP/1.1\r\n";
      %host = "Host: "@$PGDServer@"\r\n";
      %header2 = "Connection: close\r\nUser-Agent: Tribes 2\r\n";
      %contType = "Content-Type: multipart/form-data; boundary="@%sep@"\r\n";
      %guidReq = "--"@%sep@"\r\nContent-Disposition: form-data; name=\"guid\"\r\n\r\n"@%this.guid@"";
      %fileReq = "--"@%sep@"\r\nContent-Disposition: form-data; name=\"uploadedfile\"; filename=\""@%this.filebase@"\"\r\nContent-Type: application/octet-stream";
      %payload = %guidReq@"\r\n"@%filereq@"\r\n"@%filecont@"--"@%sep@"--";
      %qlen = strLen(%payload);
      %contentLeng = "Content-Length: "@%qlen@"\r\n\r\n";
      %query = %header1@%host@%header2@%contType@%contentLeng@%payload;
      echo("Connected to Phantom Games Server, Sending File Data...");
	  %this.send(%query);
   }
   else {
      %loc = $PGDPHPDelHandler@"?guid="@%this.guid@"&filetokill="@%this.file@"";
      %query = "GET" SPC %loc SPC "HTTP/1.1\x0aHost: "@$PGDServer@"\r\n\r\n";
      echo("Sending:"SPC%query);
      echo("Connected to Phantom Games Server, Deleting File");
	  %this.send(%query);
   }
}

function Univ_SaveClient::get(%connection, %server, %query) {
   %connection.server = %server;
   %connection.query = %query;
   %connection.connect(%server);
}

//Handle Errors
function Univ_SaveClient::onLine(%this, %line) {
   //echo("PGD Server Response: "@%line@".");
   if(strstr(%line, "file_upload_ok") != -1) {
      messageClient(%this.client, 'msgdone', "\c5PGD: Your File was Saved Successfully.");
      echo("Universal Save: OK");
   }
   else if(strstr(%line, "not_registered") != -1)
   {
      messageClient(%client, 'msgPGDRequired', "\c5PGD: PGD Connect account required to perform this action.");
   }
   else
   {
      error("PGD Error: "@%line);
   }
}

function Univ_SaveClient::onDisconnect(%this) {
    %this.delete();
}

function Univ_SaveClient::onConnectFailed(%this) {
   error("-- Could not connect to PGD.");
   messageClient(%this.client, 'MsgClient', "\c5PGD: Your Building could not be saved, the server may be offline or going through maintenance.");
   error("Universal Save: fail (connection)");
   %this.delete();
   return;
}
