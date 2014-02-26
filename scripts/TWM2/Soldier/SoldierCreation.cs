function SPSpawnSoldier(%obj) {
   %Cpos = vectorAdd(%obj.getposition() , "0 0 4");
   %type = %obj.spawnType;
   switch(%type) {
      case 1:
         //basic rifleman
         %armor = new player() {
            Datablock = LightMaleHumanArmor;
         };
         %armor.setInventory(G41Rifle, 1, true);
         %armor.setInventory(G41RifleAmmo, 9999, true);
         %armor.ClipCount["G41RifleClip"] = 999;
         %armor.use(G41Rifle);
         
         %name = "Rifleman";
      case 2:
         //basic SMG
         %armor = new player() {
            Datablock = LightMaleHumanArmor;
         };
         %armor.setInventory(Pg700, 1, true);
         %armor.setInventory(Pg700Ammo, 9999, true);
         %armor.ClipCount["Pg700Clip"] = 999;
         %armor.use(Pg700);

         %name = "Sub Machine Gunner";
      case 3:
         //sniper
         %armor = new player() {
            Datablock = LightMaleHumanArmor;
         };
         %armor.setInventory(G17SniperRifle, 1, true);
         %armor.setInventory(G17SniperRifleAmmo, 9999, true);
         %armor.ClipCount["G17Clip"] = 999;
         %armor.use(G17SniperRifle);

         %name = "Sniper";
      case 4:
         //commando/shotgunner
         %armor = new player() {
            Datablock = MediumMaleHumanArmor;
         };
         %armor.setInventory(SCD343, 1, true);
         %armor.setInventory(SCD343Ammo, 9999, true);
         %armor.ClipCount["SCD343Clip"] = 999;
         %armor.use(SCD343);

         %name = "Commando Shotgunner";
      case 5:
         //heavy gunner
         %armor = new player() {
            Datablock = HeavyMaleHumanArmor;
         };
         %armor.setInventory(MG42, 1, true);
         %armor.setInventory(MG42Ammo, 9999, true);
         %armor.ClipCount["MG42Clip"] = 999;
         %armor.use(MG42);

         %name = "Heavy Gunner";
   }
   
   if(%obj.isInTheMission) {
      %armor.isInTheMission = 1;
   }
   //setup
   %armor.type = %type;
   %armor.setTransform(%Cpos);
   %armor.canjump = 1;
   %armor.isSoldier = 1;

   %armor.team = 20;
   
   SoldierGroup.add(%armor);

   %armor.target = createTarget(%armor, %name, "", "Male1", '', %armor.team, PlayerSensor);
   setTargetSensorData(%armor.target, PlayerSensor);
   setTargetSensorGroup(%armor.target, 20);
   setTargetName(%armor.target, addtaggedstring(%name));
   //
   %armor.moveLoop = schedule(1000, %armor, beginSoldierAI, %armor);
}
