datablock ParticleData(burnParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.1;
   inheritedVelFactor   = 0.1;

   lifetimeMS           = 500;
   lifetimeVarianceMS   = 50;

   textureName          = "special/cloudflash";

   spinRandomMin = -10.0;
   spinRandomMax = 10.0;

   colors[0]     = "1 0.18 0.03 0.4";
   colors[1]     = "1 0.18 0.03 0.3";
   colors[2]     = "1 0.18 0.03 0.0";
   sizes[0]      = 2.0;
   sizes[1]      = 1.0;
   sizes[2]      = 0.8;
   times[0]      = 0.0;
   times[1]      = 0.6;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(burnEmitter) {
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;

   ejectionOffset = 0.2;
   ejectionVelocity = 10.0;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 10.0;

   particles = "burnParticle";
};

function burnloop(%obj){
   if(!isobject(%obj)) {
	  return;
   }
   if(%obj.onfire == 0) {
     return;
   }
   if(%obj.firecount >= %obj.maxfirecount){
	  %obj.firecount = "";
	  %obj.maxfirecount = 0;
	  %obj.onfire = 0;
      return;
   }
   else {
	  %obj.damage(0, %obj.getposition(), 0.01, $DamageType::Burn);
      %obj.lastDamagedImage = "flamerImage";
   	  %fire = new ParticleEmissionDummy(){
   	     position = vectoradd(%obj.getPosition(),"0 0 0.5");
   	     dataBlock = "defaultEmissionDummy";
   	     emitter = "BurnEmitter";
      };
	  MissionCleanup.add(%fire);
	  %fire.schedule(100, "delete");
	  %obj.firecount++;
	  schedule(100, %obj, "burnloop", %obj);
   }
}

function ApplyEMP(%client) {
   if(%client.isEMPd) {
      //applying second emp = laggy + bad
      return;
   }
   %client.isEMPd = 1;
   EMPEKill(%client.player);
   //echo("EMP: applied EMP to "@%client@" - "@%client.player@"");
}

function KillEMP(%client) {
   %client.isEMPd = 0;
   %client.player.stopZap();
   messageClient(%client, 'msgDieEMP', "\c5Armor: Electronic Stability has returned.");
   //echo("EMP: kill EMP: "@%client@"");
}

function EMPEKill(%obj) {
   if(%obj.client.isEMPd) {
      if(!isObject(%obj) || %obj.getState() $= "Dead") {
         //echo("EMP: "@%obj@" dead, sending Re-EMP to "@%obj.client@"");
         ReEMPLoop(%obj.client);
         return;
      }
      %obj.setEnergyLevel(0.0);
      %obj.zapObject();
      schedule(100, 0, "EMPEKill", %obj);
   }
   else {
      %obj.stopZap();
      return;
   }
}

function ReEMPLoop(%client) {
   if(!%client.isEMPd) {
      //echo("EMP: RE-EMP: "@%client@" no longer EMP");
      return;
   }
   if(!isObject(%client.player) || %client.player.getState() $= "Dead") {
      //echo("EMP: RE: dead, trying to re-loop on "@%client@"");
      schedule(500, 0, "ReEMPLoop", %client);
      return;
   }
   EMPLoop(%client.player);
   EMPEKill(%client.player);
   //echo("EMP: Re-EMP: "@%client@" - "@%client.player@"");
}
