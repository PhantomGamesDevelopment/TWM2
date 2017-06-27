function makePersonHumanFZomb(%trans, %client){
   %client.player.delete();
   %player = new Player() {
      dataBlock = "LightMaleHumanArmor";
   };

   %player.setTransform( %trans );
   MissionCleanup.add(%player);

   // setup some info
   %player.setOwnerClient(%client);
   %player.team = 1;
   %client.outOfBounds = false;
   %player.setEnergyLevel(0);
   %client.player = %player;

   // updates client's target info for this player
   %player.setTarget(%client.target);
   setTargetDataBlock(%client.target, %player.getDatablock());
   setTargetSensorData(%client.target, PlayerSensor);
   setTargetSensorGroup(%client.target, 1);
   %client.setSensorGroup(1);

   %client.setControlObject(%player);
   %player.client.clearBackpackIcon();
   buyFavorites(%client);

   %player.iszombie = 0;
}

function makePersonZombie(%trans, %client, %type){
   %client.player.delete();

   %armorInfo = DoZombPlayerMaker(%client, %type);
   %player = GetField(%armorInfo, 0);
   %Function = GetField(%armorInfo, 1);
   if(%Function !$= "") {
      schedule(50, 0, %Function, %player);
   }

   %player.setTransform( %trans );

   // setup some info
   %player.setOwnerClient(%client);
   %player.team = 0;
   %client.outOfBounds = false;
   %player.setEnergyLevel(0);
   %client.player = %player;

   // updates client's target info for this player
   %player.setTarget(%client.target);
   setTargetDataBlock(%client.target, %player.getDatablock());
   setTargetSensorData(%client.target, PlayerSensor);
   setTargetSensorGroup(%client.target, 30);
   %client.setSensorGroup(30);

   %client.setControlObject(%player);
   %player.client.clearBackpackIcon();

   %player.iszombie = 1;

//   ZombieBloodLust(%player,0);
//   zombieAttackImpulse(%player,80);
}

function DoZombPlayerMaker(%client, %type) {
   switch(%type) {
      case 1:
         %player = new Player() {
            dataBlock = "ZombieArmor";
         };
         %function = "";
         MessageClient(%client, 'MsgZClient', "\c5TWM2: You are now a zombie.. GO INFECT SOME HUMANS!!!");
         MessageClient(%client, 'MsgZClient', "\c3Controls: [Jet] Lunge");
      case 2:
         %player = new Player() {
            dataBlock = "FZombieArmor";
         };
         %function = "RavengerForwardImpulse";
         MessageClient(%client, 'MsgZClient', "\c5TWM2: You are now a ravenger zombie. Go hit those humans!!!");
      case 3:
         %player = new Player() {
            dataBlock = "LordZombieArmor";
         };
	     %player.mountImage(ZBack, 4);
	     %player.mountImage(ZDummyslotImg, 5);
	     %player.mountImage(ZDummyslotImg2, 6);
         %function = "";
         MessageClient(%client, 'MsgZClient', "\c5TWM2: You are now a zombie lord!!!");
         MessageClient(%client, 'MsgZClient', "\c3Controls: [Fire] Shoot, [Mine] Lift Targets");
      case 4:
         %player = new Player() {
            dataBlock = "DemonZombieArmor";
         };
         %player.mountImage(ZdummyslotImg, 4);
         %player.isSniperZombie = 0;
         %function = "";
         MessageClient(%client, 'MsgZClient', "\c5TWM2: You are now a demon zombie!!! :p");
         MessageClient(%client, 'MsgZClient', "\c3Controls: [Fire] Fireball");
      case 5:
         %player = new Player() {
            dataBlock = "RapierZombieArmor";
         };
         %player.mountImage(ZWingImage, 3);
	     %player.mountImage(ZWingImage2, 4);
	     %player.setActionThread("scoutRoot",true);
         %function = "RapierLoop";
         MessageClient(%client, 'MsgZClient', "\c5TWM2: You are now an air rapier zombie");
         MessageClient(%client, 'MsgZClient', "\c3Controls: [Jet] Fly, [Fire] Speed");
      case 6:
         %player = new Player() {
            dataBlock = "DemonMotherZombieArmor";
         };
         %player.mountImage(ZdummyslotImg, 4);
         MessageClient(%client, 'MsgZClient', "\c5TWM2: You are now a demon lord zombie");
         MessageClient(%client, 'MsgZClient', "\c3Controls: [Jet] Jump Attacks, [Fire] Shoot, [Grenade] Missile, [Mine] Firestorm");
         %player.noHS = 1;
      case 7:
         %player = new Player() {
            dataBlock = "YvexZombieArmor";
         };
         MessageClient(%client, 'MsgZClient', "\c5TWM2: You are now lord yvex :p");
         MessageClient(%client, 'MsgZClient', "\c3Controls: [Jet] Random Nightmare, [Fire] Pulse Attack, [Grenade] Summon Missile, [Mine] Group Summon");
         %player.isBoss = 1;
      case 8:
         %player = new Player() {
            dataBlock = "LordRogZombieArmor";
         };
         MessageClient(%client, 'MsgZClient', "\c5TWM2: You are now lord rog :p");
         MessageClient(%client, 'MsgZClient', "\c3Controls: [Jet] Group Summon, [Fire] Blade Of Vengence, [Mine] Random Attack");
         %player.isBoss = 1;
      case 9:
         %player = new Player() {
            dataBlock = "ShifterZombieArmor";
         };
         MessageClient(%client, 'MsgZClient', "\c5TWM2: You are now a shifter zombie");
         MessageClient(%client, 'MsgZClient', "\c3Controls: [Jet] Teleport");
      case 10:
         %player = new Player() {
            dataBlock = "SummonerZombieArmor";
         };
         MessageClient(%client, 'MsgZClient', "\c5TWM2: You are now a summoner zombie");
         MessageClient(%client, 'MsgZClient', "\c3Controls: [Jet] Open Summon Portal");
      case 11:
         %player = new Player() {
            dataBlock = "DemonZombieArmor";
         };
         MessageClient(%client, 'MsgZClient', "\c5TWM2: You are now a sniper zombie");
         MessageClient(%client, 'MsgZClient', "\c3Controls: [Fire] Shoot, [Jet] Backpedal");
         %player.isSniperZombie = 1;
         %player.mountImage(ZSniperImage1, 4);
         %player.mountImage(ZSniperImage2, 5);
      case 12:
         %player = new Player() {
            dataBlock = "DemonUltraZombieArmor";
         };
         MessageClient(%client, 'MsgZClient', "\c5TWM2: You are now a demon ultra zombie");
         MessageClient(%client, 'MsgZClient', "\c3Controls: [Jet] Rapid Speed, [Fire] Fireball");
         %player.noHS = 1;
      case 13:
         %player = new Player() {
            dataBlock = "FZombieArmor";
         };
         %player.mountImage(ZExplosivePack, 4);
         %function = "CRavengerForwardImpulse";
         MessageClient(%client, 'MsgZClient', "\c5TWM2: You are now a volatile ravenger zombie, time to make some explosions!");
         MessageClient(%client, 'MsgZClient', "\c3Controls: [Jet] Detonate C4 Pack");
      case 15:
   	     %player = new player(){
            Datablock = "WraithZombieArmor";
         };
         %player.setInventory(Mp26, 1, true);
         %player.setInventory(Mp26Ammo, 9999, true);
         %player.use(Mp26);
         MessageClient(%client, 'MsgZClient', "\c5TWM2: You are now a wraith zombie, unleash your sheilded fury");
         MessageClient(%client, 'MsgZClient', "\c3Controls: [Jet] Forward Lunge");
      //case 15:
      case 16:
         %player = new player() {
            Datablock = "ROGZombieArmor";
         };
         %player.NoHS = 1;
         //%player.isBoss = 1;
         %player.isPlayerRog = 1;

	     %player.mountImage(ZdummyslotImg, 4);
         %player.mountImage(ZMG42BaseImage, 5);
         %player.mountImage(ROGSAWImage1, 6);
         %player.mountImage(ROGSAWImage2, 7);
         %player.mountImage(ROGSAWImage3, 8);

         %player.shotsfired = 0;
         RapierShieldApply(%player);
         MessageClient(%client, 'MsgZClient', "\c5TWM2: You are now General Rog, Commander of The Air Rapiers");
         MessageClient(%client, 'MsgZClient', "\c3Controls: [Jet] zMG42 Barage, [Mine] Elite Rapier Strike, [Hold Fire] Launch Rapier Swarm");
      case 17:
         %player = new Player() {
            dataBlock = "EliteDemonZombieArmor";
         };
         %player.mountImage(ZdummyslotImg, 4);
         %player.NoHS = 1;
         %function = "";
         MessageClient(%client, 'MsgZClient', "\c5TWM2: You are now an Elite Demon Zombie, one of Rog's Elite Lieutenant Zombie");
         MessageClient(%client, 'MsgZClient', "\c3Controls: [Fire] Firestorm Artillery Fireball, [Mine] Elite Rapier Strike");
   }
   //
   %player.zombieType = %type;
   
   ZombieGroup.add(%player);
   return %player TAB %function;
}

function VRavengerForwardImpulse(%obj){
   if(!isObject(%obj)) {
	  return;
   }
   if(%obj.getState() $= "Dead") {
	  return;
   }
   %obj.setActionThread("scoutRoot",true);
   if(vectorLen(%obj.getVelocity()) < 150) {
	  %obj.applyImpulse(%obj.getPosition(),vectorScale(%obj.getForwardVector(),$Zombie::Fforwardspeed * 0.45));
   }
   schedule(100, 0, "VRavengerForwardImpulse", %obj);
}

function RavengerForwardImpulse(%obj){
   if(!isObject(%obj)) {
	  return;
   }
   if(%obj.getState() $= "Dead") {
	  return;
   }
   %obj.setActionThread("scoutRoot",true);
   if(vectorLen(%obj.getVelocity()) < 200) {
	  %obj.applyImpulse(%obj.getPosition(),vectorScale(%obj.getForwardVector(),$Zombie::Fforwardspeed * 0.6));
   }
   schedule(100, 0, "RavengerForwardImpulse", %obj);
}

function RapierLoop(%obj){
   if(!isObject(%obj)) {
	  return;
   }
   if(%obj.getState() $= "Dead") {
	  return;
   }
   %obj.setActionThread("scoutRoot",true);
   schedule(60, 0, "RapierLoop", %obj);
}

function ZombieBloodLust(%obj, %count){
   if(!isObject(%obj))
	return;
   if (%obj.getState() $= "dead")
	return;
   %obj.setDamageFlash(0.5);
   if(%count == 10){
	serverPlay3d("ZombieMoan",%obj.getWorldBoxCenter());
	%count = 0;
   }
   %count++;
   schedule(200, %obj, "ZombieBloodLust", %obj, %count);
}

function zombieAttackImpulse(%obj, %count){
   if(!isObject(%obj))
	return;
   if (%obj.getState() $= "dead")
	return;
   if(%obj.setcancelimpulse)
    return;
   if(!%obj.infected || %obj.isboss) {
      return;
   }
   %pos = %obj.getposition();
   InitContainerRadiusSearch(%pos, %count, $TypeMasks::PlayerObjectType);
   while ((%objtarget = containerSearchNext()) != 0){
	if(isObject(%objtarget) && %objtarget !$= %obj){
	   %objarmortype = %objtarget.getdatablock().getname();
	   if(%objarmortype $= "ZombieArmor" || %objarmortype $= "FZombieArmor" || %objarmortype $= "LordZombieArmor" || %objarmortype $= "DemonZombieArmor" || %objarmortype $= "RapierZombieArmor")
	      %objiszomb = 1;
	   if(!(%objiszomb) && %objtarget.iszombie != 1){
		%vec = vectorNormalize(vectorSub(%objtarget.getWorldBoxCenter(),%obj.getWorldBoxCenter()));
		if(vectorDist(%vec,%obj.getForwardVector()) <= 0.5){
		   if(%count <= 200){
		      %impulse = (%count / 20) * 90;
		      %up = (%count / 100) * 90;
		   }
		   else{
		      %impulse = 900;
		      %up = 180;
		   }
		   %vec = vectorScale(%vec,%impulse);
		   %vec = vectorAdd(%vec,"0 0 "@%up);
		   %obj.applyimpulse(%obj.getPosition(),%vec);
		   %count++;
   		   schedule(500, 0, "zombieAttackImpulse", %obj, %count);
		   return;
		}
         }
      }
	%objiszomb = 0;
   }
   %count++;
   schedule(500, 0, "zombieAttackImpulse", %obj, %count);
}
