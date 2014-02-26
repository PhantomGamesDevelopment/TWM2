//Messenger.cs
//Phantom139 -TWM2
//Displays random messages over hour intervals

function RMSLoop() {
   $nextRMSLoop = schedule(getRandom(30,60)*60*1000, 0, RMSLoop);
   %message = getRandomMessage_RMS();
   CenterPrintAll(%message, 15, 3);
}

if(!isEventScheduled($nextRMSLoop)) {
   $nextRMSLoop = RMSLoop();
}

function getRandomMessage_RMS() {

}
