$TWM2::CLogLocation = "Logs/"@formattimestring("mm-dd-yy")@"";
function LogMessage(%client, %msg) {
   if (!IsFile(""@$TWM2::CLogLocation@"/ChatLog.cs")){
      new fileobject(Clog);
      Clog.openforwrite(""@$TWM2::CLogLocation@"/ChatLog.cs");
      Clog.writeline(""@%client.namebase@"("@%client.guid@") ["@formattimestring("hh:nn a, mm-dd-yy")@"] : "@%msg@"");
      Clog.close();
      Clog.delete();
   }
   else {
      new fileobject(Clog);
      Clog.openforappend(""@$TWM2::CLogLocation@"/ChatLog.cs");
      Clog.writeline(""@%client.namebase@"("@%client.guid@") ["@formattimestring("hh:nn a, mm-dd-yy")@"] : "@%msg@"");
      Clog.close();
      Clog.delete();
   }
}

function LogConnection(%client, %type) {
   if(%type == 1) {
      %st = "Connect";
   }
   else if(%type == 2) {
      %st = "Disconnect (Kicked)";
   }
   else if(%type == 3) {
      %st = "Disconnect (Banned)";
   }
   else if(%type == 4) {
      %st = "Disconnect";
   }
   if (!IsFile(""@$TWM2::CLogLocation@"/ConnectionLog.cs")){
      new fileobject(Clog);
      Clog.openforwrite(""@$TWM2::CLogLocation@"/ConnectionLog.cs");
      Clog.writeline(""@%client.namebase@"("@%client.guid@"), "@%client.GetAddress()@", ["@formattimestring("hh:nn a, mm-dd-yy")@"] : "@%st@"");
      Clog.close();
      Clog.delete();
   }
   else {
      new fileobject(Clog);
      Clog.openforappend(""@$TWM2::CLogLocation@"/ConnectionLog.cs");
      Clog.writeline(""@%client.namebase@"("@%client.guid@"), "@%client.GetAddress()@", ["@formattimestring("hh:nn a, mm-dd-yy")@"] : "@%st@"");
      Clog.close();
      Clog.delete();
   }
}
