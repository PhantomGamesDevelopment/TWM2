datablock TriggerData(deathTrigger) {
   tickPeriodMS = 1000;
};

function deathTrigger::onEnterTrigger(%data, %obj, %colObj) {
   if(%colObj == game.Bomb) {
      MessageAll('msgWhoops', "\c5SABOTAGE: Bomb Reset.");
      Game.bomb.setPosition($SabotageGame::BombLocation[$CurrentMission]);

   }
   if(!isObject(%colObj) || %colObj.getState() $= "dead") {
      return;
   }
   %colObj.setInvinc(false);
   %colObj.scriptkill($DamageType::FellOff);
}
function deathTrigger::onLeaveTrigger(%data, %obj, %colObj) {
//Nothing
}

function deathTrigger::onTickTrigger(%data, %obj, %colObj) {
//Nothing
}








datablock TriggerData(TransTrigger) {
   tickPeriodMS = 1000;
};
//--------------------------------
function TransTrigger::onEnterTrigger(%data, %obj, %colObj)
{
         if(!isObject(%colObj) || %colObj.getState() $= "dead") {
         return;
         }
         if($CurrentMission $= "SideSwipe") {//<<Add map .mis file name here
            $PositionCount = 3;
            $Position[0] = "-82.2 -286.5 354";
            $Position[1] = "6.16 -196.2 395";
            $Position[2] = "-81.3 -95.7 354";
         }
         else if($CurrentMission $= "EngelamHimmel")//<<Add map .mis file name here
         {
           $PositionCount = 20;
                   $Position[0] = "-85 1 252";//<<Add [X] [Y] [Z] coordinates from the map
                   $Position[1] = "40 37 182";
                   $Position[2] = "-43 -10.1 182";
                   $Position[3] = "-153 34 182";
                   $Position[4] = "-240 -1 182";
                   $Position[5] = "-101 3 182";
                   $Position[6] = "-100 -136 182";
                   $Position[7] = "-100 125 182";
                   $Position[8] = "139 16 182";
                   $Position[9] = "-337 17 182";
                   $Position[10] = "-114 1 252";
                   $Position[11] = "56 -5 182";
                   $Position[12] = "-101 15 182";
                   $Position[13] = "-229 34 182";
                   $Position[14] = "-170 -1 182";
                   $Position[15] = "-101 -30 182";
                   $Position[16] = "-100 -65 182";
                   $Position[17] = "-101 83 182";
                   $Position[18] = "98 10 182";
                   $Position[19] = "-297 23 182";
         }
         else
         {
           messageclient(%colObj.client,'MsgClient', "\c0No map coordinates found on "@$Host::Map@".");
           %colObj.noPos = 1;
           return;
         }
         %TargPos = $Position[mFloor(getRandom() * $PositionCount)];//This gets a random target position from the list(s) above
         %colObj.setTransform(%TargPos);//moves the colliding object(the player) to the target position
}
function TransTrigger::onLeaveTrigger(%data, %obj, %colObj)
{
         if(!isObject(%colObj) || %colObj.getState() $= "dead") {
         return;
         }
         if(%colObj.noPos)
         {
         //%colObj.scriptkill($DamageType::KillTrig);
         %colObj.noPos = 0;
         }
         else return;
}
function TransTrigger::onTickTrigger(%data, %obj, %colObj) {
}

//
















datablock TriggerData(invincDisableTrigger) {
   tickPeriodMS = 1000;
};

function invincDisableTrigger::onEnterTrigger(%data, %obj, %colObj) {
   if(!isObject(%colObj) || %colObj.getState() $= "dead") {
      return;
   }
   bottomPrint(%colObj.client, "You are no longer invincible...", 2, 2);
   %colObj.setInvinc(false);
}
function invincDisableTrigger::onLeaveTrigger(%data, %obj, %colObj) {
//Nothing
}

function invincDisableTrigger::onTickTrigger(%data, %obj, %colObj) {
//Nothing
}





























datablock TriggerData(MissionEndTrigger) {
   tickPeriodMS = 1000;
};

function MissionEndTrigger::onEnterTrigger(%data, %obj, %colObj) {
   CompleteMission(TWM2Mission);
}
function MissionEndTrigger::onLeaveTrigger(%data, %obj, %colObj) {
//Nothing
}

function MissionEndTrigger::onTickTrigger(%data, %obj, %colObj) {
//Nothing
}
