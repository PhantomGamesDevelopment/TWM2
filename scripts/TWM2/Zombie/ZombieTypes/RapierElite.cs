datablock PlayerData(RapierEliteZombieArmor) : MediumMaleBiodermArmor {
   maxDamage = 3.0;
   minImpactSpeed = 50;
   speedDamageScale = 0.015;
   maxEnergy =  80;
   repairRate = 0.0033;
   energyPerDamagePoint = 75.0; // shield energy required to block one point of damage

   rechargeRate = 0.256;
   jetForce = 25.22 * 130 * 1.5;
   underwaterJetForce = 25.22 * 130 * 1.5;
   underwaterVertJetFactor = 1.5;
   jetEnergyDrain =  1.0;
   underwaterJetEnergyDrain =  0.6;
   minJetEnergy = 1;
   maxJetHorizontalPercentage = 0.8;

   boundingBox = "2.0 2.0 1.2";

   damageScale[$DamageType::M1700] = 2.0;

	max[RepairKit]			= 0;
	max[Mine]			= 0;
	max[Grenade]			= 0;
};

function RapierEliteAirstrike(%cl, %pos) {
   %offSet[0] = "700 0 100";
   %offSet[1] = "725 0 100";
   %offSet[2] = "750 0 100";

   %player = %cl.player;
   if(!isObject(%player) || %player.getState() $= "dead") {
      return;
   }
   for(%i = 0; %i < 3; %i++) {
      %z = StartAZombie(vectorAdd(%pos, %offSet[%i]), 18);
      //issue move order
   }
}

function RapierEliteDropAcidBomb(%zombie, %pos, %cl) {
   if(!isObject(%zombie) || %zombie.getState() $= "dead") {
      return;
   }
   %p = new (TracerProjectile)() {
      dataBlock        = EliteRapierAcidBomb;
      initialDirection = "0 0 -5";
      initialPosition  = %zombie.getPosition();
      sourceObject     = %zombie;
      sourceSlot       = 0;
   };
   %p.theClient = %cl;
   MissionCleanup.add(%p);
   if(isObject(%cl.player)) {
      %p.sourceObject = %cl.player;
   }
}
