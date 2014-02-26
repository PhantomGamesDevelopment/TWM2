datablock ItemData(C4Deployed) {
   shapeFile = "pack_upgrade_satchel.dts";
   explosion = SatchelMainExplosion;
   underwaterExplosion = UnderwaterSatchelMainExplosion;
   mass = 1;
   elasticity = 0.1;
   friction = 0.9;
   rotate = false;
   pickupRadius = 0;
   noTimeout = true;
   maxDamage = 0.6;

   kickBackStrength    = 4000;

   computeCRC = true;
};


datablock ItemData(C4) {
   className = HandInventory;
   catagory = "Handheld";
   shapeFile = "ammo_mine.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.7;
   pickupRadius = 2;

   thrownItem = C4Deployed;
   pickUpName = "some C4 Explosives";

   computeCRC = true;
};

function C4GoBoom(%C4) {
   %C4.setDamageState(Destroyed);
}

function C4Deployed::onThrow(%this, %mine, %thrower) {
   if($TWM2::PlayingSabo) {
      MessageClient(%thrower.client, 'MsgC4IsBanned', "\c5C4 Mines are banned in this game mode");
      %mine.delete();
      return;
   }
   %mine.armed = false;
   %mine.damaged = 0;
   %mine.detonated = false;
   %mine.depCount = 0;
   %mine.theClient = %thrower.client;
   %time = 10;
   if(%thrower.client.IsActivePerk("3 Second C4")) {
      %time = 3;
   }
   schedule(%time * 1000, 0, "C4GoBoom", %mine);
   MessageClient(%thrower.client, 'MsgBoom', "\c5C4 Deployed, "@%time@" Seconds to Detonate.");
}

function C4Deployed::onDestroyed(%data, %obj, %lastState) {
   %dmgRadius = 20 * $SatchelChargeMultiplier;
   %dmgMod = 1.0 * $SatchelChargeMultiplier;
   %expImpulse = limitSatchelImpulse(2500 * $SatchelChargeMultiplier);
   %dmgType = $DamageType::SatchelCharge;
   if(!%obj.isFalse) {
      RadiusExplosion(%obj, %obj.getPosition(), %dmgRadius, %dmgMod, %expImpulse, %obj.sourceObject, %dmgType);
   }
   %obj.schedule(500, "Delete");
}
