$Zombie::RAAMThread = "cel1";
//************************************************************
//*****************Zomb Creation Stuff************************
//************************************************************

function ZcreateLoop(%obj)
{
   if(isObject(%obj))
   {
	if(%obj.timedout == 0){
	   if(%obj.numZ <= 2 || %obj.numZ $= ""){
		ZPCreateZombie(%obj);
		if(%obj.numZ $= "")
		   %obj.numZ = 0;
		%obj.numZ++;
		%obj.timedout = 1;
		schedule(10000, 0, "eval", ""@%obj@".timedout = 0;");
	   }
	}
   	%obj.ZCLoop = schedule(2000, 0, "ZcreateLoop", %obj);
   }
}

//this is for when a ZSpawn spawns a zombie
function ZPCreateZombie(%obj){
   if($Game::ZombieCount > $TWM2::MaxZombies || !$TWM2::CanSpawnZ) {
      return;
   }
   %Cpos = vectorAdd(%obj.getposition() , "0 0 4");
   if(%obj.Ztype $= ""){
      %obj.ZType = 1;
   }
   switch(%obj.ZType) {
      case 1:
         %Zombie = new player(){
            Datablock = "ZombieArmor";
         };
         %command = "Zombiemovetotarget";
      case 2:
         %Zombie = new player(){
            Datablock = "FZombieArmor";
         };
         %command = "FZombiemovetotarget";
      case 3:
         %Zombie = new player(){
            Datablock = "LordZombieArmor";
         };
         %Zombie.mountImage(ZHead, 3);
         %Zombie.mountImage(ZBack, 4);
         %Zombie.mountImage(ZDummyslotImg, 5);
         %Zombie.mountImage(ZDummyslotImg2, 6);
         %Zombie.setInventory(AcidCannon, 1, true);
         %Zombie.use(AcidCannon);
         %zombie.firstFired = 0;
         %zombie.canmove = 1;
         %command = "LZombiemovetotarget";
      case 4:
         %Zombie = new player(){
            Datablock = "DemonZombieArmor";
         };
         %zombie.mountImage(ZdummyslotImg, 4);
         %command = "DZombiemovetotarget";
      case 5:
         %Zombie = new player(){
            Datablock = "RapierZombieArmor";
         };
         %Zombie.mountImage(ZWingImage, 3);
         %Zombie.mountImage(ZWingImage2, 4);
         %zombie.setActionThread("scoutRoot",true);
         %command = "RZombiemovetotarget";
      case 6:
         DemonMotherCreate(%cPos);
         return;
      case 7 or 8:
         //Skip 7 (Yvex), and 8 (Lord-Rog)
      case 9:
         %Zombie = new player(){
            Datablock = "ShifterZombieArmor";
         };
         %command = "ShifterZombiemovetotarget";
      case 10:
         //the Z-Spawner cannot be permitted to spawn summoner zombies
      case 11:
         %Zombie = new player(){
            Datablock = "DemonZombieArmor";
         };
         %zombie.mountImage(ZSniperImage1, 4);
         %zombie.mountImage(ZSniperImage2, 5);
         %command = "SniperZombiemovetotarget";
      case 12:
         %Zombie = new player(){
            Datablock = "DemonUltraZombieArmor";
         };
         %command = "UDemonZombiemovetotarget";
         %zombie.NoHS = 1; //blocks HSs
      case 13:
         %Zombie = new player(){
            Datablock = "FZombieArmor";
         };
         %zombie.mountImage(ZExplosivePack, 4);
         %command = "VRavZombiemovetotarget";
      case 14:
         %Zombie = new player(){
            Datablock = "SSZombieArmor";
         };
         %zombie.mountImage(SSZombImage2, 4);
         %zombie.mountImage(SSZombImage3, 5);
         %command = "SSZombiemovetotarget"; //Damned Nazi Zombies!!! "SS"
      case 15:
         %Zombie = new player(){
            Datablock = "WraithZombieArmor";
         };
         %Zombie.setInventory(Mp26, 1, true);
         %Zombie.setInventory(Mp26Ammo, 999, true);
         %Zombie.use(Mp26);
         StartWraithAI(%Zombie);
      case 16:
         %Zombie = new player() {
            Datablock = "ROGZombieArmor";
         };
         %Zombie.NoHS = 1;
         %Zombie.isBoss = 1;

         %zombie.mountImage(ZdummyslotImg, 4);
         %zombie.mountImage(ZMG42BaseImage, 5);
         %zombie.mountImage(ROGSAWImage1, 6);
         %zombie.mountImage(ROGSAWImage2, 7);
         %zombie.mountImage(ROGSAWImage3, 8);

         %zombie.shotsfired = 0;
         RapierShieldApply(%zombie);
         %command = "GeneralRogmovetotarget";
      case 17:
         %Zombie = new player(){
            Datablock = "EliteDemonZombieArmor";
         };
         %zombie.mountImage(ZdummyslotImg, 4);
         %command = "EDZombiemovetotarget";
      default:
         %Zombie = new player(){
            Datablock = "ZombieArmor";
         };
         %command = "Zombiemovetotarget";
   }


   //Missions
   if(%obj.isInTheMission) {
      %zombie.isInTheMission = 1;
   }
   //

   %zombie.type = %obj.Ztype;
   %Zombie.setTransform(%Cpos);
   %zombie.canjump = 1;
   %zombie.isZombie = 1;
   %zombie.HasCP = 1;
   
   %Zombie.team = 30;
   %zname = $TWM2::ZombieName[%obj.ZType]; // <- To Hosts, Enjoy, You can
                                      //Change the Zombie Names now!!!
   %zombie.target = createTarget(%zombie, %zname, "", "Derm3", '', %zombie.team, PlayerSensor);
   setTargetSensorData(%zombie.target, PlayerSensor);
   setTargetSensorGroup(%zombie.target, 30);
   setTargetName(%zombie.target, addtaggedstring(%zname));
   setTargetSkin(%zombie.target, 'Horde');
   
   $Game::ZombieCount++;

   if(%obj.spawnTypeset == 1)
	%obj.numZ = 3;
   else
      %zombie.CP = %obj;
   %zombie.hastarget = 1;
   ZombieGroup.add(%Zombie);
   if(isSet(%command)) {
      schedule(1000, %zombie, %command, %zombie);
   }
}

//This is for creation of a zombie at a location using the console
function StartAZombie(%pos, %type){
   if(!isObject(Game)) {
      error("UE Blocked...");
      return;
   }
   if($Game::ZombieCount > $TWM2::MaxZombies || !$TWM2::CanSpawnZ) {
      return;
   }
   //
   if(%type $= "") {
      %type = 1;
   }
   if(%type == 7 || %type == 8 || %type <= 0) {
      %type = 1;
   }
   //
   if(%Type == 1){
      %Zombie = new player(){
	     Datablock = "ZombieArmor";
   	  };
	  %command = "Zombiemovetotarget";
      if(Game.CheckModifier("YouCantSeeMe") == 1) {
         %Zombie.setCloaked(true);
      }
   }
   if(%Type == 2){
  	  %Zombie = new player(){
	     Datablock = "FZombieArmor";
   	  };
	  %command = "FZombiemovetotarget";
   }
   if(%Type == 3){
      %Zombie = new player(){
	     Datablock = "LordZombieArmor";
   	  };
	  %zombie.client = $zombie::Lclient;
	  %Zombie.mountImage(ZHead, 3);
	  %Zombie.mountImage(ZBack, 4);
	  %Zombie.mountImage(ZDummyslotImg, 5);
	  %Zombie.mountImage(ZDummyslotImg2, 6);
      %Zombie.setInventory(AcidCannon, 1, true);
      %Zombie.use(AcidCannon);
	  %zombie.firstFired = 0;
	  %zombie.canmove = 1;
	  %command = "LZombiemovetotarget";
   }
   if(%type == 4){
	  %Zombie = new player(){
	     Datablock = "DemonZombieArmor";
	  };
	  %zombie.mountImage(ZdummyslotImg, 4);
	  %command = "DZombiemovetotarget";
   }
   if(%type == 5) {
	  %Zombie = new player(){
	     Datablock = "RapierZombieArmor";
	  };
	  %Zombie.mountImage(ZWingImage, 3);
	  %Zombie.mountImage(ZWingImage2, 4);
	  %zombie.setActionThread("scoutRoot",true);
	  %command = "RZombiemovetotarget";
   }
   if(%type == 6) {
      return DemonMotherCreate(%pos);
   }
   //Skip 7 (Yvex), and 8 (Rog)
   if(%type == 9){
	  %Zombie = new player(){
	     Datablock = "ShifterZombieArmor";
	  };
	  %command = "ShifterZombiemovetotarget";
   }
   if(%type == 10){
	  %Zombie = new player(){
	     Datablock = "SummonerZombieArmor";
	  };
	  %command = "SummonerZombiemovetotarget";
   }
   if(%type == 11){
	  %Zombie = new player(){
	     Datablock = "DemonZombieArmor";
	  };
	  %zombie.mountImage(ZSniperImage1, 4);
      %zombie.mountImage(ZSniperImage2, 5);
	  %command = "SniperZombiemovetotarget";
   }
   if(%type == 12){
	  %Zombie = new player(){
	     Datablock = "DemonUltraZombieArmor";
	  };
	  %command = "UDemonZombiemovetotarget";
      %zombie.NoHS = 1; //blocks HSs
   }
   if(%Type == 13){
  	  %Zombie = new player(){
	     Datablock = "FZombieArmor";
   	  };
	  %zombie.mountImage(ZExplosivePack, 4);
	  %command = "VRavZombiemovetotarget";
   }
   if(%Type == 14){
  	  %Zombie = new player(){
	     Datablock = "SSZombieArmor";
   	  };
	  %zombie.mountImage(SSZombImage2, 4);
      %zombie.mountImage(SSZombImage3, 5);
	  %command = "SSZombiemovetotarget"; //Damned Nazi Zombies!!! "SS"
   }
   if(%Type == 15){
   	 %Zombie = new player(){
        Datablock = "WraithZombieArmor";
   	 };
     %Zombie.isZombie = 1;
     %Zombie.setInventory(Mp26, 1, true);
     %Zombie.setInventory(Mp26Ammo, 999, true);
     %Zombie.use(Mp26);
     StartWraithAI(%Zombie);
   }
   if(%Type == 16) {
      %Zombie = new player() {
         Datablock = "ROGZombieArmor";
      };
      %Zombie.NoHS = 1;
      %Zombie.isBoss = 1;
      
	  %zombie.mountImage(ZdummyslotImg, 4);
      %zombie.mountImage(ZMG42BaseImage, 5);
      %zombie.mountImage(ROGSAWImage1, 6);
      %zombie.mountImage(ROGSAWImage2, 7);
      %zombie.mountImage(ROGSAWImage3, 8);

      %zombie.shotsfired = 0;
      RapierShieldApply(%zombie);
      %command = "GeneralRogmovetotarget";
   }
   if(%type == 17){
	  %Zombie = new player(){
	     Datablock = "EliteDemonZombieArmor";
	  };
	  %zombie.mountImage(ZdummyslotImg, 4);
	  %command = "EDZombiemovetotarget";
   }
   if(%type == 18) {
	  %Zombie = new player(){
	     Datablock = "RapierEliteZombieArmor";
	  };
	  %Zombie.mountImage(ZWingImage, 3, true, 'beagle');
	  %Zombie.mountImage(ZWingImage2, 4, true, 'beagle');
	  %zombie.setActionThread("scoutRoot",true);
   }
   
   //
   if(!isObject(%zombie)) {
      echo("Zero zombie error, spawning normal");
      %type = 1;
      %zombie = new player(){
	     Datablock = "ZombieArmor";
   	  };
	  %command = "Zombiemovetotarget";
      if(Game.CheckModifier("YouCantSeeMe") == 1) {
         %zombie.setCloaked(true);
      }
   }

   %Zombie.team = 30;
   %zname = $TWM2::ZombieName[%type]; // <- To Hosts, Enjoy, You can
                                      //Change the Zombie Names now!!!
   %zombie.target = createTarget(%zombie, %zname, "", "Derm3", '', %zombie.team, PlayerSensor);
   setTargetSensorData(%zombie.target, PlayerSensor);
   setTargetSensorGroup(%zombie.target, 30);
   setTargetName(%zombie.target, addtaggedstring(%zname));
   setTargetSkin(%zombie.target, 'Horde');
   //
   %zombie.type = %type;
   %Zombie.setTransform(%pos);
   %zombie.canjump = 1;
   %zombie.hastarget = 1;
   %zombie.isZombie = 1;
   ZombieGroup.add(%Zombie);
   
   $Game::ZombieCount++;
   
   if(isSet(%command)) {
      schedule(1000, %zombie, %command, %zombie);
   }
   return %zombie;
}

//This is for when someone is killed by a zombie and spawns a new one
function CreateZombie(%obj){
   if($Game::ZombieCount > $TWM2::MaxZombies || !$TWM2::CanSpawnZ || $TWM::PlayingInfection || $TWM::PlayingHorde || $TWM::PlayingHellJump) {
      return;
   }
   %Cpos = %obj.getposition();
   %Zombie = new player(){
	Datablock = "ZombieArmor";
   };
   %Zombie.setTransform(%Cpos);
   %zombie.type = 1;
   %zombie.canjump = 1;
   %zombie.hastarget = 1;
   %zombie.isZombie = 1;
   
   %Zombie.team = 30;
   %zname = $TWM2::ZombieName[1]; // <- To Hosts, Enjoy, You can
                                      //Change the Zombie Names now!!!
   %zombie.target = createTarget(%zombie, %zname, "", "Derm3", '', %zombie.team, PlayerSensor);
   setTargetSensorData(%zombie.target, PlayerSensor);
   setTargetSensorGroup(%zombie.target, 30);
   setTargetName(%zombie.target, addtaggedstring(%zname));
   setTargetSkin(%zombie.target, 'Horde');
   
   ZombieGroup.add(%Zombie);
   
   //Phantom139 - new zombie stand up stuff :P
   %zombie.zapObject();
   revivestand(%zombie, 0);
   //end
   
   schedule(1000, %zombie, "Zombiemovetotarget", %zombie);
}










//New Zombie Drop-Spawning
//Phantom139, TWM2 3.5
//Now zombies can come into the battlefield in different ways

// *Single Drop Pod
// *Group Pod
// *Hunter Dropship
