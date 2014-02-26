//ArmorEnergyShields.cs
//Phantom139

//Handles the energy shields on certain armors (bosses/zombies/maybe players :) )

function Player::damageShields(%player, %amount) {
   //cancel current recharge if they are doing so
   if(!isSet(%player.getDatablock().shieldHealthCharge)) {
      %player.getDatablock().shieldHealthCharge = 0.05; //default
   }
   cancel(%player.shieldRechargeLoop);
   %player.shieldRechargeLoop = %player.schedule(15000, "rechargeShields", %player.getDatablock().shieldHealthCharge); //set next one.
   //cut shield health first, then check to see if they are down...
   if((%amount - %player.shieldHealth) < 0) {
      %final = 0;
   }
   else {
      %final = %amount - %player.shieldHealth;
   }
   %player.shieldHealth -= %amount;
   if(%player.shieldHealth <= 0) {
      %player.shieldHealth = 0;
   }
   return %final;
}

function Player::rechargeShields(%player, %amount) {
   if(!isPlayer(%player)) {
      return;
   }
   if(!%player.isAlive()) {
      return;
   }
   if(!isSet(%player.getDatablock().shieldHealthCharge)) {
      %player.getDatablock().shieldHealthCharge = 0.05; //default
   }
   if(%player.getShieldPercent() < 100) {
      //flash them to show recharge
      %player.playShieldEffect("1 1 1");
      //
      %player.shieldHealth += %amount;
      %player.shieldRechargeLoop = %player.schedule(75, "rechargeShields", %player.getDatablock().shieldHealthCharge);
   }
   //
   if(%player.shieldHealth > %player.getMaxShieldHealth()) {
      %player.shieldHealth = %player.getMaxShieldHealth();
   }
}

function Player::getShieldPercent(%player) {
   return ((%player.getShieldHealth() / %player.getMaxShieldHealth()) * 100);
}

//returns a numeric indication of a player's sheild level
function Player::getShieldHealth(%player) {
   return %player.shieldHealth;
}

function Player::getMaxShieldHealth(%player) {
   return %player.getDatablock().maxShieldLevel;
}

function Player::activeShieldEffect(%player) {
   if(!isPlayer(%player)) {
      return;
   }
   if(!%player.isAlive()) {
      return;
   }
   if(%player.getMaxShieldHealth() > 0) {
      //
      if(%player.shieldHealth > 0) {
         %player.zapObject();
      }
      %player.schedule(1750, "activeShieldEffect");
   }
}
