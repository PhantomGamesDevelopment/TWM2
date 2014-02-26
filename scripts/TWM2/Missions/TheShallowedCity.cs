function OnTimeZeroTheShallowedCity(%group) {
   for(%i = 1; %i <= %group.Participants; %i++) {
      MessageClient(%group.Participant[%i], 'msgShip', "\c5Command: 10 minutes have expired, speed it up!.");
   }
}

function StartTWM2MisTheShallowedCity(%group) {
   loadShallowedCity(%group);
}

//
function loadShallowedCity(%group) {

}
