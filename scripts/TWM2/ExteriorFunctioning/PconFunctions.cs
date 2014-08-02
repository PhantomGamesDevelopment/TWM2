////////////////////////////////////////////////////////////////////////////////
////////////////// PCON FUNCTIONS V.1.7 (Phantatron)                         ///
////////////////// Yep, same functions from pcon                             ///
////////////////// Script By: Phantom139                                     ///
////////////////////////////////////////////////////////////////////////////////

$host::Nobots = 1;         //hosts can disable this option to spawn bots
//////////

function spawnprojectile(%proj,%type,%pos,%direction,%src) {
if(%src $= "" || !%src) {
   %src = "";
}
%p = new (%type)() {
dataBlock        = %proj;
initialDirection = %direction;
initialPosition  = %pos;
damageFactor     = 1;
sourceObject     = %src;
};
MissionCleanup.add(%p);
//%p.sourceObject = %src;  //fun fun
return %p;
}

function spawnprojectileSourceMod(%proj,%type,%pos,%direction,%src) {
if(%src $= "" || !%src) {
   %src = "";
}
%p = new (%type)() {
dataBlock        = %proj;
initialDirection = %direction;
initialPosition  = %pos;
damageFactor     = 1;
//sourceObject     = %src;
};
MissionCleanup.add(%p);
%p.sourceObject = %src;  //fun fun
return %p;
}

///-------------------------------------------------------------------------------
///-------------------------------------------------------------------------------
//--- Object Commanding
function RenameObject(%Obj)
{
if (%className $= "Generator" || %className $= "Switch"){
   %obj.nametag = %args;
   %freq = %obj.powerfreq;
   setTargetName(%obj.target,addTaggedString("\c8"@%args@"\c6 Frequency "@%freq));
   return;
}

else if (%className $= "teleport"){
   %freq = %obj.Frequency;
   %obj.nametag = %args;
   setTargetName(%obj.target,addTaggedString("\c8"@%args@"\c6 Frequency "@%freq));
   return;
   }
else if (%className $= "waypoint"){
%obj.wp.schedule(10, "delete");
     %waypoint = new  (WayPoint)(){
         dataBlock        = WayPointMarker;
         position         = %obj.getPosition();
         name             = %args;
         scale            = "0.1 0.1 0.1";
         team             = getRandom(0,2);
       };
      MissionCleanup.add(%waypoint);
   %obj.wp=%waypoint;
   return;
   }
else
    setTargetName(%obj.target,addTaggedString("\c8"@%args@"\c6"));
}

//--- Various Kill Commands and Kill Functions in The Mod
function KillClientByType(%client,%type)
{
if(%type==1){  //Kill Client
%client.player.scriptkill($DamageType::Admin);
}
else if(%type==2){ //Blow Up Client
%client.player.blowup();
%client.player.scriptkill($DamageType::Admin);
}
else if(%type==3){ //Nuke Client
BunkerBusterball::onExplode("" ,"",%client.player.position);
}
else if(%type==4){ //3 Second Storm
%zap= new Lightning(Lightning)
   {
   position = %client.player.position;
   rotation = "1 0 0 0";
   scale = "55 55 100";
   dataBlock = "DefaultStorm";
   lockCount = "0";
   homingCount = "0";
   strikesPerMinute = "500";
   strikeWidth = "2.5";
   chanceToHitTarget = "100";
   strikeRadius = "10";
   boltStartRadius = "20"; //altitude the lightning starts from
   color = "0.314961 1.000000 0.576000 1.000000";
   fadeColor = "0.560000 0.860000 0.260000 1.000000";
   useFog = "1";
   shouldCloak = 0;
   };
%zap.schedule(3000, delete);
}
else if(%type==5){ //1 Second Violent Shock
%shock= new Lightning(Lightning)
   {
   position = %target.player.position;
   rotation = "1 0 0 0";
   scale = "55 55 4";
   dataBlock = "DefaultStorm";
   lockCount = "0";
   homingCount = "0";
   strikesPerMinute = "10000";
   strikeWidth = "2.5";
   chanceToHitTarget = "100";
   strikeRadius = "10";
   boltStartRadius = "1"; //altitude the lightning starts from
   color = "0.314961 1.000000 0.576000 1.000000";
   fadeColor = "0.560000 0.860000 0.260000 1.000000";
   useFog = "1";
   shouldCloak = 0;
   };
%shock.schedule(1000, delete);
}
}

function createNewDecoy(%client,%name,%race,%armor,%sex,%enum) {
%gun = %client.givingto;
if(%enum == 1)
%emote = "sitting";
else if(%enum == 2)
%emote = "standing";
//armor
if(%race == 1 && %armor == 1 && %sex == 1)
%mountimage = LightMaleHumanArmor;
else if(%race == 1 && %armor == 2 && %sex == 1)
%mountimage = MediumMaleHumanArmor;
else if(%race == 1 && %armor == 3 && %sex == 1)
%mountimage = HeavyMaleHumanArmor;
else if(%race == 1 && %armor == 1 && %sex == 2)
%mountimage = LightFemaleHumanArmor;
else if(%race == 1 && %armor == 2 && %sex == 2)
%mountimage = MediumFemaleHumanArmor;
else if(%race == 1 && %armor == 3 && %sex == 2)
%mountimage = HeavyFemaleHumanArmor;
else if(%race == 2 && %armor == 1 && %sex == 1)
%mountimage = LightMaleBiodermArmor;
else if(%race == 2 && %armor == 2 && %sex == 1)
%mountimage = MediumMaleBiodermArmor;
else if(%race == 2 && %armor == 3 && %sex == 1)
%mountimage = HeavyMaleBiodermArmor;
     %objdecoy = new Player()
     {
      dataBlock = %mountimage;
      Position = %client.player.getPosition();
      };
   %objdecoy.target = createTarget(%objdecoy, %name, "", "Male1", '', 0, PlayerSensor);
   setTargetDataBlock(%objdecoy.target, %objdecoy.getDatablock());
   setTargetSensorData(%objdecoy.target, PlayerSensor);
   setTargetSensorGroup(%objdecoy.target, 0);
   setTargetName(%objdecoy.target, addtaggedstring(%name));
   %objdecoy.setActionThread(%emote,true);
   %objdecoy.isStatic = true;
   %client.player.decoy = %objdecoy;
   %objdecoy.isDecodic = 1;
   %objdecoy.setTransform(VectorAdd(%client.player.getPosition(),"0 0 0") SPC rot(%client.player));
   %objdecoy.disableMove(true);
}

function gougeloop(%client) {
if(!%client.blinded){
return;
}
else {
%client.player.setDamageFlash(1);
}
schedule(100,0,"gougeloop",%client);
}

//Scream Sound
datablock AudioProfile(NightmareScreamSound)
{
   filename    = "voice/male1/avo.deathcry_02.wav";
   description = AudioClose3d;
   preload = true;
};

function nightmareloop(%causer,%viewer,%type) {
if(%type == 1) {       //slow drain to death
   %enum = getRandom(1,5);
   switch(%enum) {
   case 1:
   %emote = "sitting";
   case 2:
   %emote = "standing";
   case 3:
   %emote = "death3";
   case 4:
   %emote = "death2";
   case 5:
   %emote = "death4";
   }
   if(!isobject(%viewer.player) || %viewer.player.getState() $= "dead") {
   %viewer.nightmared = 0;
   return;
   }
   if(!isobject(%causer.player) || %causer.player.getState() $= "dead") {
   %viewer.nightmared = 0;
   %viewer.player.setMoveState(false);
   messageclient(%sender, 'MsgClient', '\c2The source of your nightmare has been destroyed.');
   return;
   }
   %viewer.player.setMoveState(true);
   %viewer.nightmared = 1;
   %viewer.player.setActionThread(%emote,true);
   %viewer.player.setWhiteout(1.8);
   %viewer.player.setDamageFlash(1.5);
   %causer.player.playShieldEffect("1 1 1");
   serverPlay3D(NightmareScreamSound, %viewer.player.position);
   schedule(500,0,"nightmareloop",%causer, %viewer, %type);
   %viewer.player.damage(0, %viewer.player.position, 0.01, $DamageType::admin);
   %causer.player.applyRepair(0.01);
   BottomPrint(%viewer,""@%causer.namebase@"'s Nightmare Is Draining your will to live.",5,1);
   BottomPrint(%causer,"Your life is being replenished by sucking "@%viewer.namebase@"'s Dream Energy.",5,1);
   messageclient(%viewer, 'MsgClient', "~wvoice/fem1/avo.deathcry_02.wav");
   messageclient(%viewer, 'MsgClient', "~wvoice/fem2/avo.deathcry_02.wav");
   messageclient(%viewer, 'MsgClient', "~wvoice/fem3/avo.deathcry_02.wav");
   messageclient(%viewer, 'MsgClient', "~wvoice/fem4/avo.deathcry_02.wav");
   messageclient(%viewer, 'MsgClient', "~wvoice/fem5/avo.deathcry_02.wav");
   messageclient(%viewer, 'MsgClient', "~wvoice/male1/avo.deathcry_02.wav");
   messageclient(%viewer, 'MsgClient', "~wvoice/male2/avo.deathcry_02.wav");
   messageclient(%viewer, 'MsgClient', "~wvoice/male3/avo.deathcry_02.wav");
   messageclient(%viewer, 'MsgClient', "~wvoice/male4/avo.deathcry_02.wav");
   messageclient(%viewer, 'MsgClient', "~wvoice/male5/avo.deathcry_02.wav");
   messageclient(%viewer, 'MsgClient', "~wvoice/derm1/avo.deathcry_02.wav");
   messageclient(%viewer, 'MsgClient', "~wvoice/derm2/avo.deathcry_02.wav");
   messageclient(%viewer, 'MsgClient', "~wvoice/derm3/avo.deathcry_02.wav");
   }
else if(%type == 2) {        // Sleep = death
   if(!isobject(%viewer.player) || %viewer.player.getState() $= "dead") {
   %viewer.tirednightmared = 0;
   return;
   }
   if(!%viewer.tirednightmared) {
   %viewer.Nightmareticks = 0;
   return;
   }
   if(%viewer.nightmareticks $= "") {
   %viewer.nightmareticks = 0;
   }
   %viewer.Nightmareticks++;
   schedule(500,0,"nightmareloop",%causer, %viewer, %type);
      if(%viewer.Nightmareticks < 20) {
      %viewer.player.setWhiteout(0.3);
      BottomPrint(%viewer,"Something is happening, you suddenly feel tired.",5,1);
      }
      else if(%viewer.Nightmareticks >= 20 && %viewer.Nightmareticks < 70) {
      %viewer.player.setWhiteout(0.5);
      %viewer.player.damage(0, %viewer.player.position, 0.01, $DamageType::admin);   //Maybe they will understand...
      BottomPrint(%viewer,"You feel really fatigued, and are slowly drifting to sleep.",5,1);
      }
      else if(%viewer.Nightmareticks >= 70 && %viewer.Nightmareticks < 100) {
      %viewer.player.setWhiteout(0.8);
      %viewer.player.damage(0, %viewer.player.position, 0.02, $DamageType::admin);   //Maybe they will understand...
      CenterPrint(%viewer,"HURRY GET TO THE INVENTORY STATION!!!",5,1);
      BottomPrint(%viewer,"YOU UNDERSTAND NOW, THIS NIGHTMARE IS GOING TO KILL YOU",5,1);
      }
      else if(%viewer.Nightmareticks >= 100) {
      %viewer.player.setWhiteout(1.8);
      %viewer.player.damage(0, %viewer.player.position, 100.0, $DamageType::admin);
      BottomPrint(%viewer,"You fell asleep, and the nightmare killed you.",10,1);
      }
   }
else if(%type == 3) {   //I believe I can fly, I DONT belive in GRAVITY!!!
   if(!isobject(%viewer.player) || %viewer.player.getState() $= "dead") {
   %viewer.flynightmared = 0;
   return;
   }
   if(!%viewer.flynightmared) {
   %viewer.Nightmareticks = 0;
   return;
   }
   if(%viewer.nightmareticks $= "") {
   %viewer.nightmareticks = 0;
   }
   %viewer.Nightmareticks++;
   schedule(500,0,"nightmareloop",%causer, %viewer, %type);
      if(%viewer.nightmareticks < 25) {
      %viewer.player.setVelocity("0 0 300");
      BottomPrint(%viewer,"WOOOOOOOOOOHOOOOOOO FLYING",5,1);
      }
      else if(%viewer.nightmareticks >= 25 && %viewer.nightmareticks < 27) {
      %viewer.player.setVelocity("0 0 -300");
      BottomPrint(%viewer,"Oh s***!!!!!",5,1);
      }
      else if(%viewer.nightmareticks > 27) {
      CenterPrint(%viewer,"Only now you realized that even Nightmares... Are Real.",10,1);
      %viewer.flynightmared = 0;
      }
   }
}
