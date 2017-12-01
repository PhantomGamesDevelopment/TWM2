datablock ItemData(StaticGrenadeThrown) {
	className = Weapon;
	shapeFile = "ammo_plasma.dts";
	mass = 0.4;
	elasticity = 0.2;
	friction = 1;
	pickupRadius = 2;
	maxDamage = 0.5;
	explosion = PlasmaBarrelBoltExplosion;
	underwaterExplosion = PlasmaBarrelBoltExplosion;
	indirectDamage      = 0.4;
	damageRadius        = 10.0;
	radiusDamageType    = $DamageType::StaticGrenade;
	kickBackStrength    = 2000;

	computeCRC = true;
};

datablock ItemData(StaticGrenade) {
	className = HandInventory;
	catagory = "Handheld";
	shapeFile = "ammo_plasma.dts";
	mass = 0.4;
	elasticity = 0.2;
	friction = 1;
	pickupRadius = 2;
	thrownItem = StaticGrenadeThrown;
	pickUpName = "some static grenades";
	isGrenade = true;

	computeCRC = true;
};

function StaticGrenadeThrown::onThrow(%this, %gren) {
	AIGrenadeThrown(%gren);
	%gren.detThread = schedule(3000, %gren, "detonateGrenade", %gren);
}

function StaticGrenadeThrown::onCollision(%data, %obj, %col) {
	%cn = %col.getDatablock().getClassName();
	if(%cn $= "PlayerData" || strstr(%cn, "Vehicle") != -1) {
		//BOOM!
		detonateGrenade(%obj);
		//Die now...
		if(!%col.isBoss && !%col.isGOF) {
			RadiusExplosion( %obj, %obj.getPosition(), 1, 100, 1000, %obj.sourceObject, $DamageType::StaticGrenade);
		}
	}
}
