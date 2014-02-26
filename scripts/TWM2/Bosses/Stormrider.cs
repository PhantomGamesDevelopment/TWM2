//Stormrider.cs
//Ported From TWM1
//Code Updated By Phantom139
// * 2-18-14: Did some housecleaning, harvested some unneeded functions here.

function UltrDroneBattle(%pos, %radius, %number, %teamlow, %teamhigh, %maxskill, %slth){
   for(%i = 0; %i < %number; %i++){
      %startpos = vectorAdd(%pos,(getRandom(0, %radius) - (%radius / 2))@" "@(getRandom(0, %radius) - (%radius / 2))@" 0");
      %rotation = "0 0 1 "@getRandom(1,360);
      if(%teamlow != %teamhigh) {
         %team = getRandom(%teamlow, %teamhigh);
      }
      else {
         %team = %teamlow;
      }
      StartUltrDrone(%startpos,%rotation,%team,getRandom(1,%maxskill), %slth);
   }
}

//This sets up the drone and the functions needed to start the drone.
function StartUltrDrone(%pos, %rotation, %team, %skill, %slth) {
   if(%team $= "") {
      %team = 0;
   }
   if(%pos $= "") {
      %pos = "0 0 300";
   }
   if(%rotation $= "") {
      %rotation = "1 0 0 0";
   }
   if(%skill !$= "ace") {
      if(%skill $= "" || %skill < 1) {
         %skill = 10;
      }
      else if(%skill > 10) {
         %skill = 10;
      }
   }
   %Drone = new FlyingVehicle() {
      dataBlock    = StormSeigeDrone;
      position     = %pos;
      rotation     = %rotation;
      team         = %team;
   };
   MissionCleanUp.add(%Drone);

   setTargetSensorGroup(%Drone.getTarget(), %team);

   %Drone.isdrone = 1;
   %drone.dodgeGround = 0;

   if(%slth) {
      %Drone.setCloaked(true);
   }

   if(%skill $= "ace"){
      %skill = 10;
      %drone.isace = 1;
   }

   %drone.skill = 0.2 + (%skill / 12.5);

   schedule(100, 0, "DroneForwardImpulse", %drone);
   schedule(101, 0, "DronefindTarget", %drone);
   schedule(102, 0, "DroneScanGround", %drone);

   return %drone;
}

function StartStormrider(%position) {
   %pos = vectoradd(%position, "0 0 500");
   %pos2 = vectoradd(%position, "15 0 500");
   %pos3 = vectoradd(%position, "-15 0 500");
   %drone = UltrDroneBattle(%pos, 500, 1, 6, 6, "ace", 0); //yes this bad guy is stealthed
   %d2 = DroneBattle(%pos2, 500, 1, 6, 6, 100, 0); //his Pal
   %d3 = DroneBattle(%pos3, 500, 1, 6, 6, 100, 0); //his Other Pal
   %drone.isUltr = 1;
   %drone.isBoss = 1;
   %d2.isUltrally = 1;
   %d3.isUltrally = 1;
   UltraBossAbilities(%drone);
   
   InitiateBoss(%drone, "Stormrider");
}

function UltraBossAbilities(%drone) {
   if(!isObject(%drone)) {
      return;
   }
   %drone.setCloaked(false); //disable cloak?
   %rand = getRandom(1,13);

   switch(%rand) {
   
      //1: Double Missile Strike: Targets Single Player with Two Missiles
      case 1:
         %target = DroneFindNearestPilot(2000, %drone);
         if(%target.player) {
            %SPos1 = vectorAdd(%drone.getPosition(),"3 0 0");
            %SPos2 = vectorAdd(%drone.getPosition(),"-3 0 0");
            %p1 = new SeekerProjectile() {
               dataBlock        = LordRogStiloutte;
               initialDirection = "0 0 10";
               initialPosition  = %SPos1;
               sourceObject     = %drone;
               sourceSlot       = 6;
            };
            %p2 = new SeekerProjectile() {
               dataBlock        = LordRogStiloutte;
               initialDirection = "0 0 10";
               initialPosition  = %SPos1;
               sourceObject     = %drone;
               sourceSlot       = 6;
            };
            MissionCleanup.add(%p1);
            MissionCleanup.add(%p2);
            schedule(1000, 0, WindshearAttack_FUNC, "MissileDrop", %target.player SPC %p1);
            schedule(1000, 0, WindshearAttack_FUNC, "MissileDrop", %target.player SPC %p2);
            MessageAll('MessageAll', "\c4Stormrider: Fire!");
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": No targets for me!");
         }
         
      //2: Quad Strike: Fire Four Missiles out in a + pattern which then aquire a single target.
      case 2:
         %target = DroneFindNearestPilot(2000, %drone);
         if(%target.player) {
            %SPos = vectorAdd(%drone.getPosition(),"0 0 3");
            %dirs = "5 0 0\t-5 0 0\t0 5 0\t0 -5 0";
            for(%i = 0; %i < getFieldCount(%dirs); %i++) {
               %newMissile = new SeekerProjectile() {
                  dataBlock        = ShoulderMissile;
                  initialDirection = getField(%dirs, %i);
                  initialPosition  = %SPos;
                  sourceObject     = %drone;
                  sourceSlot       = 6;
               };
               MissionCleanup.add(%newMissile);
               schedule(1000, 0, WindshearAttack_FUNC, "MissileDrop", %target.player SPC %newMissile);
            }
             MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Have fun with these "@getTaggedString(%target.name)@"!");
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Bah, no targets, no fun.");
         }
         
      //3: IR Beam: A single enemy target gets locked on with a heat beam making missiles 100% accurate for a time period
      case 3:
         %target = DroneFindNearestPilot(2000, %drone);
         if(%target.player) {
            HeatLoop(%target.player, 0);
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Lets see what happens when missiles are completely precice on you, "@getTaggedString(%target.name)@"!");
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": I guess it's time to start scanning.");
         }
         
      //4: Auxilary Pod Swarm: Unleash a swarm of hybrid strike missiles on a single enemy target
      case 4:
         %target = DroneFindNearestPilot(2000, %drone);
         if(%target.player) {
            FireanotherSidewinder(%drone, %target.player, 3);
            schedule(700, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(1400, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(2100, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(2800, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(3500, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(4200, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(4900, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            //Rapid Shot Missiles
            schedule(5000, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(5200, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(5400, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(5600, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(5800, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(6000, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(6200, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(6400, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(6500, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Taste my fury "@getTaggedString(%target.name)@"!");
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Aww, My missiles were ready.");
         }
          
      //5: Focused Missile Fire: Similar to APS, but more accurate with less shots near the end
      case 5:
         %target = DroneFindNearestPilot(2000, %drone);
         if(%target.player) {
            FireanotherSidewinder(%drone, %target.player, 3);
            schedule(700, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(1400, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(2100, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(2800, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(3500, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(4200, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(4900, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(5600, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(6300, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            schedule(7000, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 3);
            // Quick Shots
            schedule(8000, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(8100, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(8200, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            schedule(8300, 0, "WindshearAttack_FUNC", "SidewinderLaunch", %drone SPC %target.player SPC 1);
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": I have missiles with your name on them "@getTaggedString(%target.name)@"!");
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Aww, My missile strike was ready.");
         }
         
      //6: Clear Skies: Launch a wave on anti-missile flares blocking all missiles from striking Stormrider for a short time.
      case 6:
         schedule(700, 0, "WindshearAttack_FUNC", "Flare", %drone);
         schedule(1400, 0, "WindshearAttack_FUNC", "Flare", %drone);
         schedule(2100, 0, "WindshearAttack_FUNC", "Flare", %drone);
         schedule(2800, 0, "WindshearAttack_FUNC", "Flare", %drone);
         schedule(3500, 0, "WindshearAttack_FUNC", "Flare", %drone);
         schedule(4200, 0, "WindshearAttack_FUNC", "Flare", %drone);
         schedule(4900, 0, "WindshearAttack_FUNC", "Flare", %drone);
         schedule(5600, 0, "WindshearAttack_FUNC", "Flare", %drone);
         schedule(6300, 0, "WindshearAttack_FUNC", "Flare", %drone);
         schedule(7000, 0, "WindshearAttack_FUNC", "Flare", %drone);
         // Quick Shots
         schedule(8000, 0, "WindshearAttack_FUNC", "Flare", %drone);
         schedule(8100, 0, "WindshearAttack_FUNC", "Flare", %drone);
         schedule(8200, 0, "WindshearAttack_FUNC", "Flare", %drone);
         schedule(8300, 0, "WindshearAttack_FUNC", "Flare", %drone);
         MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Hahaha, Your Missiles are worthless Now!");
         
      //7: Anti-Ground Chaingun: Fire a rapid strike anti-matter chaingun which does heavy damage to hit targets
      case 7:
         %target = DroneFindNearestPilot(2000, %drone);
         if(%target.player) {
            FireSniperShots(%drone, %target.player, 3);
            for(%i = 0; %i < 700; %i++) {
               %time = %i * 10;
               schedule(%time, 0,"FireSniperShots",%drone, %target.player);
            }
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Time to Use My CG, "@getTaggedString(%target.name)@"!");
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": We'll see who can withstand the CG... Oh wait, there's nobody there.");
         }
         
      //8: Photon Seekers: Launch a wave of Photon Seekers to strike a single target
      case 8:
         %target = DroneFindNearestPilot(2000, %drone);
         if(%target.player) {
            FireSeekerPhotons(%drone, %target.player);
            schedule(1500, 0, "FireSeekerPhotons", %drone, %target.player);
            schedule(3000, 0, "FireSeekerPhotons", %drone, %target.player);
            schedule(4500, 0, "FireSeekerPhotons", %drone, %target.player);
            schedule(6000, 0, "FireSeekerPhotons", %drone, %target.player);
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Here, "@getTaggedString(%target.name)@", Catch!");
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Close up the Seekers. No Targets To hit.");
         }
          
      //9: Advanced Photon Wave: Launch an additional Photon Missile compared to #8
      case 9:
         %target = DroneFindNearestPilot(2000,%drone);
         if(%target.player) {
            FireSeekerPhotons(%drone,%target.player);
            schedule(700, 0, "FireSeekerPhotons",%drone,%target.player);
            schedule(1400, 0, "FireSeekerPhotons",%drone,%target.player);
            schedule(2100, 0, "FireSeekerPhotons",%drone,%target.player);
            schedule(2800, 0, "FireSeekerPhotons",%drone,%target.player);
            schedule(3500, 0, "FireSeekerPhotons",%drone,%target.player);
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Try these out for size, "@getTaggedString(%target.name)@"!");
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Heh, No enemies in the area.");
         }
         
      //10: Photon Storm: Launch Photon Missiles to the point of overheating the internal system.
      case 10:
         %target = DroneFindNearestPilot(2000, %drone);
         if(%target.player) {
            FireSeekerPhotons(%drone, %target.player);
            schedule(500, 0, "FireSeekerPhotons", %drone, %target.player);
            schedule(1000, 0, "FireSeekerPhotons", %drone, %target.player);
            schedule(1500, 0, "FireSeekerPhotons", %drone, %target.player);
            schedule(2000, 0, "FireSeekerPhotons", %drone, %target.player);
            schedule(2500, 0, "FireSeekerPhotons", %drone, %target.player);
            schedule(3000, 0, "FireSeekerPhotons", %drone, %target.player);
            schedule(3500, 0, "FireSeekerPhotons", %drone, %target.player);
            schedule(4000, 0, "FireSeekerPhotons", %drone, %target.player);
            schedule(4500, 0, "FireSeekerPhotons", %drone, %target.player);
            schedule(5000, 0, "FireSeekerPhotons", %drone, %target.player);
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": I have some fun plasma missiles for you, "@getTaggedString(%target.name)@"!");
         }
         else {
            MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Meh, No targets for my plasma seekers.");
         }
         
      //11: Tactical Camo: Engage an invisibility module to blend in with the sky.
      case 11:
         MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Engage Stealth!");
         %drone.setCloaked(true);
          
      //11: Wingmen: Call in 4 Harbinger Aerial Drones to engage air and ground targets.
      case 12:
         MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": My Buddies will handle You!");
         %SPos1 = vectorAdd(%drone.getPosition(), "15 0 0");
         %SPos2 = vectorAdd(%drone.getPosition(), "-15 0 0");
         %SPos3 = vectorAdd(%drone.getPosition(), "0 15 0");
         %SPos4 = vectorAdd(%drone.getPosition(), "0 -15 0");
         %d2 = DroneBattle(%SPos1, 500, 1, 6, 6, 100, 0); //his Pal
         %d3 = DroneBattle(%SPos2, 500, 1, 6, 6, 100, 0); //his Other Pal
         %d4 = DroneBattle(%SPos3, 500, 1, 6, 6, 100, 0); //his Pal's Pal
         %d5 = DroneBattle(%SPos4, 500, 1, 6, 6, 100, 0); //his Pal's Pal's Buddy
         %d2.isUltrally = 1;
         %d3.isUltrally = 1;
         %d4.isUltrally = 1;
         %d5.isUltrally = 1;
         
      //DE: Reinforcements: Call in support from Harbinger Drones
      default:
         %SPos1 = vectorAdd(%drone.getPosition(),"15 0 0");
         %SPos2 = vectorAdd(%drone.getPosition(),"15 0 0");
         %d2 = DroneBattle(%SPos1, 500, 1, 6, 6, 100, 0); //his Pal
         %d3 = DroneBattle(%SPos2, 500, 1, 6, 6, 100, 0); //his Other Pal
         %d2.isUltrally = 1;
         %d3.isUltrally = 1;
         MessageAll('MessageAll', "\c4"@$TWM2::BossName["Stormrider"]@": Get Moving, targets to be hunted!");
   }
   schedule(30000,0,"UltraBossAbilities",%drone);
}

function FireSniperShots(%drone,%target){
if(!isObject(%drone) || !isObject(%target) || %target.getState() $= "dead") {
return;
}

   %vec = vectorsub(%target.getworldboxcenter(),%drone.getMuzzlePoint(6));
   %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/100));

   %x = (getRandom() - 0.5) * 2 * 3.1415926 * (6/1000);
   %y = (getRandom() - 0.5) * 2 * 3.1415926 * (6/1000);
   %z = (getRandom() - 0.5) * 2 * 3.1415926 * (6/1000);
   %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
   %nvec = MatrixMulVector(%mat, %vec);

%p = new TracerProjectile()
{
dataBlock        = M1Bullet;
initialDirection = %nvec;
initialPosition  = %drone.getMuzzlePoint(6);
sourceObject     = %drone;
sourceSlot       = 6;
};
}

function FireSeekerPhotons(%drone,%target){
   if(!isObject(%drone) || !isObject(%target) || %target.getState() $= "dead") {
      return;
   }

   %proj = createSeekingProjectile(PhotonMissileProj, linearflareprojectile, vectorAdd(%drone.getPosition(), "0 0 5")
                           , "0 0 3", %drone, %target, 100);

   MissionCleanup.add(%proj);
   %proj.PhotonMuzVec = %drone.getMuzzleVector(5);
   schedule( 100,0, "PhotonShockwaveFunc" ,%drone,%Proj);
}

//**********************************
//*          Projectiles           *
//**********************************
function PhotonMissileProj::onExplode(%data, %proj, %pos, %mod)// ok this is where everything is canceled... the scheds.. this is the buggy part
{
parent::onexplode(%data, %proj, %pos, %mod);
cancel(%proj.seeksched);
cancel(%proj.PhotonShockwaveSched);
cancel(%proj.seekschedcheck);
}
