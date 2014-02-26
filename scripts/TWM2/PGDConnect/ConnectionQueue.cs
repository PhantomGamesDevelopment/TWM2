//ConnectionQueue.cs
//Phantom139 for TWM2
//This script manages multiple connection handlers.

$TCP::GenericServerPort = 80;
$TCP::ConnectionContainer = new ScriptObject() {
   class = "TCPConnectionList";

   connectionStatus = 0;            //Current status indicated by object
   currentConnection = "";          //Current web address connected to
   currentSeparator = "";           //Used for FORM POST data separation
   currentTask = "";                //Current function being used by the container
   dropConnection = 1;              //Auto Drop after task completion
   dropTimer = 7500;                //time in MS until autoDrop is performed
   
   useHTTP = 0;                     //switch to the HTTP Object

   requestData = "";

   //create the inner TCP object for controlling the connections
   //TCPConnection = new TCPObject(PGDConnection) {};
   TCPQueue[0] = "";
};
$TCP::ConnectionObject = new TCPObject(PGDConnection);
$HTTP::ConnectionObject = new HTTPObject(PGDConnection_HTTP);
$BufferLine = 0;

//control functions for the connection list
function TCPConnectionList::establishConnection(%this) {
   %this.currentTask = %this@".establishConnection();";
   if(%this.TCPQueue[0] $= "") {
      error("Task breakoff, queue slot 0 is empty but attempt to a connection?");
      return;
   }
   
   if(%this.useHTTP) {
      //echo("* Switching to HTTP Object connection for file download");
      echo("Getting: "@getField(%this.TCPQueue[0], 0)@" => "@getField(%this.TCPQueue[0], 1)@"");
      $Buffer[$HTTP::ConnectionObject] = -1;
      $HTTP::ConnectionObject.get(getField(%this.TCPQueue[0], 0)@":80", getField(%this.TCPQueue[0], 1));
      %this.useHTTP = 0;
   }
   else {
      //%request = %this.call(getField(%this.TCPQueue[0], 2), getField(%this.TCPQueue[0], 3));
      //echo(%this@"."@getField(%this.TCPQueue[0], 2)@"("@getField(%this.TCPQueue[0], 3)@");");
      %request = eval(%this@"."@getField(%this.TCPQueue[0], 2)@"("@getField(%this.TCPQueue[0], 3)@");");
      //echo("Request: "@%request);
      %this.requestData = %request;

      if(%this.requestData $= "NIL_REQUEST") {
         //task has been invalidated for some reason, push next one on the queue
         %this.performNextTask();
         return;
      }

      $TCP::ConnectionObject.connect(getField(%this.TCPQueue[0], 0) @ ":" @ $TCP::GenericServerPort);
      if(%this.dropConnection) {
         %this.autoDrop = $TCP::ConnectionObject.schedule(%this.dropTimer, "disconnect");
      }
   }
}

function TCPConnectionList::addTaskToList(%this, %host, %location, %task, %args) {
   %this.currentTask = %this@".addTaskToList("@%host@", "@%location@", "@%task@", "@%args@");";
   //echo("Adding TCP: "@%host@":"@%location@" === "@%task@" ("@%args@")");

   if(%this.TCPQueue[0] $= "") {
      //front of the list.
      %this.TCPQueue[0] = %host TAB %location TAB %task TAB %args;
      if(%task $= "http") {
         %this.useHTTP = 1;
      }
      %this.establishConnection(); //connect now.
   }
   else {
      %check = 1;
      while(%this.TCPQueue[%check] !$= "") {
         %check++;
      }
      %this.TCPQueue[%check] = %host TAB %location TAB %task TAB %args;
   }
}

function TCPConnectionList::performNextTask(%this) {
   cancel(%this.autoDrop);
   %this.currentTask = %this@".performNextTask();";

   if(%this.TCPQueue[1] !$= "") {
      //there is another task in the list, update the list
      %this.TCPQueue[0] = ""; //this task is complete
      %check = 1;
      while(%this.TCPQueue[%check] !$= "") {
         %this.TCPQueue[%check-1] = %this.TCPQueue[%check];
         %this.TCPQueue[%check] = "";
         %check++;
      }
      echo("Performing next task");
      if(getField(%this.TCPQueue[0], 2) $= "http") {
         %this.useHTTP = 1;
      }
      %this.establishConnection();
   }
   else {
     echo("No next task, completing");
     %this.TCPQueue[0] = "";
   }
}

function TCPConnectionList::getRandomSeperator(%this, %length) {
   %this.currentTask = %this@".getRandomSeperator("@%length@");";

   %alphanumeric = "abcdefghijklmnopqrstuvwxyz0123456789";
   for(%i = 0; %i < %length; %i++) {
      %index = getRandom(strLen(%alphanumeric));
      %letter = getSubStr(%alphanumeric, %index, 1);
      %UpperC = getRandom(0, 1);
      if(%UpperC) {
         %letter = strUpr(%letter);
      }
      else {
         %letter = strLwr(%letter);
      }
      %seq = %seq @ %letter;
   }

   %this.currentSeparator = %seq;
}

function TCPConnectionList::makeDisposition(%this, %_name, %_content, %_isEnd) {
   %this.currentTask = %this@".makeDisposition("@%_name@", "@%_content@", "@%_isEnd@");";

   if(%_isEnd) {
      %dispo = "--" @ %this.currentSeparator @ "\r\nContent-Disposition: form-data; name=\""@%_name@"\"\r\n\r\n"@%_content@"\r\n--" @ %this.currentSeparator @ "--";
   }
   else {
      %dispo = "--" @ %this.currentSeparator @ "\r\nContent-Disposition: form-data; name=\""@%_name@"\"\r\n\r\n"@%_content@"\r\n";
   }
   return %dispo;
}

function TCPConnectionList::makeUploadDisposition(%this, %_name, %_content, %_fileContent, %_isEnd) {
   %this.currentTask = %this@".makeUploadDisposition("@%_name@", "@%_content@", "@%_fileContent@", "@%_isEnd@");";

   if(%_isEnd) {
      %dispo = "--" @ %this.currentSeparator @ "\r\nContent-Disposition: form-data; name=\""@%_name@"\"; filename=\""@%_content@"\"\r\nContent-Type: application/octet-stream\r\n"@%_fileContent@"\r\n--" @ %this.currentSeparator @ "--";
   }
   else {
      %dispo = "--" @ %this.currentSeparator @ "\r\nContent-Disposition: form-data; name=\""@%_name@"\"; filename=\""@%_content@"\"\r\nContent-Type: application/octet-stream\r\n"@%_fileContent@"\r\n";
   }
   return %dispo;
}

function TCPConnectionList::assembleHTTP1_1Header(%this, %_command, %_userAgent, %_extra) {
   %this.currentTask = %this@".assembleHTTP1_1Header("@%_command@", "@%_userAgent@", "@%_extra@");";
   %header = %_command SPC getField(%this.TCPQueue[0], 1) SPC "HTTP/1.1\r\n" @
                    "Host: "@getField(%this.TCPQueue[0], 0)@"\r\n" @
                    "User-Agent: "@%_userAgent@"\r\nConnection: close\r\n" @
                    %_extra;
   return %header;
}

function TCPConnectionList::dropConnection(%this) {
   %this.currentTask = %this@".dropConnection();";
   $TCP::ConnectionObject.disconnect();
}

//functions for the TCP object alone
function PGDConnection::onConnected(%this) {
   //--$PGDConnection::Connected = true;
   if($DebugMode) {
      echo("DEBUG: "@$TCP::ConnectionContainer.requestData);
   }
   $TCP::ConnectionContainer.connectionStatus = 0;
   %this.doingSomething = true;
   %this.response = "";
   %this.send($TCP::ConnectionContainer.requestData);
}

function PGDConnection::onConnectFailed( %this ) {
   //--$PGDConnection::Connected = false;
   $TCP::ConnectionContainer.connectionStatus = 1;
   %this.response = "CERROR_CONNECTFAILED";
   //CloseMessagePopup();
   //MessageBoxOK("Connection Error", "Unable to connect to a server \nPlease try again later.");
   error("Connection Error Occured");
   //move up the task list
   $TCP::ConnectionContainer.currentConnection = "";
   $TCP::ConnectionContainer.currentTask = "";
   $TCP::ConnectionContainer.performNextTask();
}

function PGDConnection::onDNSFailed( %this ) {
   //--$PGDConnection::Connected = false;
   $TCP::ConnectionContainer.connectionStatus = 2;
   %this.response = "CERROR_DNSFAIL";
   //CloseMessagePopup();
   //MessageBoxOK("DNS Error", "Your DNS server was unable to resolve a host name \nPlease try again later.");
   error("DNS Error Occured");
   //move up the task list
   $TCP::ConnectionContainer.currentConnection = "";
   $TCP::ConnectionContainer.currentTask = "";
   $TCP::ConnectionContainer.performNextTask();
}

function PGDConnection::onLine(%this, %line) {
   if($debugMode) {
      echo(%line);
   }

   if (trim(%line) $= "") {       //is the line a HTTP header?
     if (!%this.readyToRead) {
        %this.readyToRead = true;
     }
   }
   if(!%this.readyToRead) {
      return; //we have no use for this.
   }

   %this.response = %this.response @ %line;
   if(%this.lineEval !$= "") {
      eval(%this@"."@%this.lineEval@"("@%line@");");
      //%this.call(%this.lineEval, %line);
   }
}

function PGDConnection::onDisconnect(%this) {
   $TCP::ConnectionContainer.connectionStatus = 0;
   $TCP::ConnectionContainer.currentConnection = "";
   $TCP::ConnectionContainer.currentTask = "";
   //
   $TCP::ConnectionContainer.performNextTask();
   //
   if(isSet(%this.finishFunction)) {
      eval(%this@"."@%this.finishFunction@"("@%this.response@");");
      //%this.call(%this.finishFunction, %this.response);
   }
}

//===== HTTP =====
function PGDConnection_HTTP::onLine(%this, %line) {
   if($debugMode) {
      echo(%line);
   }

   //full line evaluations are not supported by HTTP objects
   //we must use a buffer to safely transmit data instead
   $Buffer[%this, $Buffer[%this]++] = %line;
   if(%this.lineEval !$= "") {
      eval(%this@"."@%this.lineEval@"("@%line@");");
      //%this.call(%this.lineEval, %line);
   }
}

function PGDConnection_HTTP::onDisconnect(%this) {
   $TCP::ConnectionContainer.connectionStatus = 0;
   $TCP::ConnectionContainer.currentConnection = "";
   $TCP::ConnectionContainer.currentTask = "";
   //
   $TCP::ConnectionContainer.performNextTask();
   //
   if(isSet(%this.finishFunction)) {
      eval(%this@"."@%this.finishFunction@"();");
      //%this.call(%this.finishFunction, %this.response);
   }
}
