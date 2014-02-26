function clientPressInsert(%client) {
   // start controls
   if(%client.canAcceptDenyPieces) {
      %sender = %client.pieceTransferFrom;
      // last check here
      if(%client.recipientOf[%sender] == 1) {
         //accept it!
         TransferPieces(%sender, %client);
         //clear vars
         %client.canAcceptDenyPieces = 0;
         %client.recipientOf[%sender] = 0;
         %client.pieceTransferFrom = 0;
      }
   }
}

function clientPressDelete(%client) {
   // start controls
   if(%client.canAcceptDenyPieces) {
      %sender = %client.pieceTransferFrom;
      // last check here
      if(%client.recipientOf[%sender] == 1) {
         //deny it!
         messageClient(%sender, 'msgCli', "\c3"@%client.namebase@" has denied your piece transfer request.");
         messageClient(%client, 'msgCli', "\c3You have denied "@%sender.namebase@"'s piece transfer request.");
         //clear vars
         %client.canAcceptDenyPieces = 0;
         %client.recipientOf[%sender] = 0;
         %client.pieceTransferFrom = 0;
      }
   }
}
