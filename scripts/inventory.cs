
$host::nopulseSCG = 0; ///No super pulse powers for admins.
//----------------------------------------------------------------------------

// Item Datablocks
//    image = Name of mounted image datablock
//    onUse(%this,%object)

// Item Image Datablocks
//    item = Name of item inventory datablock

// ShapeBase Datablocks
//    max[Item] = Maximum amount that can be caried

// ShapeBase Objects
//    inv[Item] = Count of item in inventory
//----------------------------------------------------------------------------

$TestCheats = 0;

function serverCmdUse(%client,%data)
{
   // Item names from the client must converted
   // into DataBlocks
   // %data = ItemDataBlock[%item];

   %client.getControlObject().use(%data);
}

function serverCmdThrow(%client,%data)
{
   // Item names from the client must converted
   // into DataBlocks
   // %data = ItemDataBlock[%item];
   %client.getControlObject().throw(%data);
}

function serverCmdThrowWeapon(%client,%data)
{
   // Item names from the client must converted
   // into DataBlocks
   // %data = ItemDataBlock[%item];
   %client.getControlObject().throwWeapon();
}

function serverCmdThrowPack(%client,%data)
{
   %client.getControlObject().throwPack();
}

function serverCmdTogglePack(%client,%data)
{
   // this function is apparently never called
   %client.getControlObject().togglePack();
}

function serverCmdThrowFlag(%client)
{
   //Game.playerDroppedFlag(%client.player);
   Game.dropFlag(%client.player);
}

function serverCmdSelectWeaponSlot( %client, %data ) {
	%client.getControlObject().selectWeaponSlot( %data );
	if (%client.player)
		if (%client.player.getObjectMount())
			callEject(%client.player, %data);
}

function callEject(%player, %num) {
	%veh = %player.getObjectMount();
	if (!(%veh.getType() & $TypeMasks::VehicleObjectType))
		return;
	if (%veh.getMountNodeObject(0) == %player) {
		%obj = %veh.getMountNodeObject(%num);
		if(%obj) {
			if (%obj.getType() & $TypeMasks::PlayerObjectType) {
				%obj.getDataBlock().doDismount(%obj, 0, 1);
			}
		}
	}
	else if(%num == 5)
		%player.getDataBlock().doDismount(%player, 0, 1);
}

function serverCmdCycleWeapon( %client, %data ) {
   %client.getControlObject().cycleWeapon( %data );
}

function serverCmdStartThrowCount(%client, %data)
{
   %client.player.throwStart = getSimTime();
}

function serverCmdEndThrowCount(%client, %data)
{
   if(%client.player.throwStart == 0)
      return;

   // throwStrength will be how many seconds the key was held
   %throwStrength = (getSimTime() - %client.player.throwStart) / 150;
   // trim the time to fit between 0.5 and 1.5
   if(%throwStrength > 1.5)
      %throwStrength = 1.5;
   else if(%throwStrength < 0.5)
      %throwStrength = 0.5;

   %throwScale = %throwStrength / 2;
   %client.player.throwStrength = %throwScale;

   %client.player.throwStart = 0;
}

//----------------------------------------------------------------------------

function ShapeBase::throwWeapon(%this)
{
   if(Game.shapeThrowWeapon(%this)) {
      %image = %this.getMountedImage($WeaponSlot);
      %this.throw(%image.item);
      if(!%this.isZombie) {
         %this.client.setWeaponsHudItem(%image.item, 0, 0);
      }
   }
}

function ShapeBase::throwPack(%this)
{
   %image = %this.getMountedImage($BackpackSlot);
   %this.throw(%image.item);
   %this.client.setBackpackHudItem(%image.item, 0);
}

function ShapeBase::throw(%this,%data)
{
   if(!isObject(%data))
      return false;

   if (%this.inv[%data.getName()] > 0) {

      // save off the ammo count on this item
      if( %this.getInventory( %data ) < $AmmoIncrement[%data.getName()] )
         %data.ammoStore = %this.getInventory( %data );
      else
         %data.ammoStore = $AmmoIncrement[%data.getName()];

      // Throw item first...
      %this.throwItem(%data);
      if($AmmoIncrement[%data.getName()] !$= "")
         %this.decInventory(%data,$AmmoIncrement[%data.getName()]);
      else
         %this.decInventory(%data,1);
      return true;
   }
   return false;
}

function ShapeBase::use(%this, %data) {
	if(%data $= Grenade) {
        //[3.9] Phantom139: Stripped out all of the x specific stuff here in favor of a cleaner system
        if(%this.hasGrenadeModes) {
           //2: Grenade Modes
           if(getSimTime() > (%this.changeModeTime + $WeaponModes::SwitchDelay)) {
              %this.getMountedImage($WeaponSlot).changeMode(%this, 2);
              %this.changeModeTime = getSimTime();
           }
           if(%this.changingModeBlocksUse) {
              return;
           }
        }
		// figure out which grenade type you're using
		for(%x = 0; $InvGrenade[%x] !$= ""; %x++) {
			if(%this.inv[$NameToInv[$InvGrenade[%x]]] > 0) {
				%data = $NameToInv[$InvGrenade[%x]];
				break;
			}
		}
	}
	else if(%data $= "Backpack") {
		%pack = %this.getMountedImage($BackpackSlot);
		// if you don't have a pack but have placed a satchel charge, detonate it
		if(!%pack && (%this.thrownChargeId > 0) && %this.thrownChargeId.armed ) {
			if (!$Host::SatchelChargeEnabled) {
				if (isObject(%this.client)) {
					if ($Host::Purebuild == 1)
						messageAll('msgClient','\c2%1 just tried to detonate a Satchel Charge!',%this.client.name);
					else
						messageTeam(%this.client.team,'msgClient','\c2%1 just tried to detonate a Satchel Charge!',%this.client.name);
					%this.client.clearBackPackIcon();
				}
				%this.thrownChargeId.delete();
				%this.thrownChargeId = "0";
				return;
			}
			else {
				%this.playAudio( 0, SatchelChargeExplosionSound );
				schedule( 800, %this, "detonateSatchelCharge", %this );
				return true;
			}
		}
		return false;
	}
	else if(%data $= Beacon) {
		%data.onUse(%this);
		if (%this.inv[%data.getName()] > 0)
		return true;
	}
	// Pack modes
	if (%data $= "RepairKit") {
		if ($Host::ExpertMode == 1) { // Only use in Expert Mode
			if (%this.hasForceField) {
				%this.expertSet++;
				if (%this.expertSet > $expertSettings["forcefield"])
					%this.expertSet = 0;
				%line = $expertSetting["forcefield",%this.expertSet];
				bottomPrint(%this.client,%line,2,1);
				return;
			}
                        //[most] Again using the unified plugin code. See item.cs for reference.
                        else if(%this.getMountedImage(2) && GetWord($packSettings[%this.getMountedImage(2).getName()],0)) {
                             %changed = %this.getMountedImage(2).ChangeMode(%this,1,1);
                             return "";
                        }
                        //[most]       
			else if (%this.hasGravField) {
				%this.expertSet++;
				if (%this.expertSet > $expertSettings["gravfield"])
					%this.expertSet = 0;
				%line = $expertSetting["gravfield",%this.expertSet];
				bottomPrint(%this.client,%line,2,1);
				return;
			}
			else if (%this.hasFloor) {
				%this.expertSet++;
				if (%this.expertSet > $expertSettings["floor"])
					%this.expertSet = 0;
				%line = $expertSetting["floor",%this.expertSet];
				bottomPrint(%this.client,"Floor set to" SPC %line,2,1);
				return;
			}
			else if (%this.hasBlast) {
				%this.expertSet++;
				if (%this.expertSet > $expertSettings["blast"])
					%this.expertSet = 0;
				%line = $expertSetting["blast",%this.expertSet];
				bottomPrint(%this.client,%line,2,1);
				return;
			}
			else if (%this.hasWalk) {
				%this.expertSet++;
				if (%this.expertSet > $expertSettings["walk"])
					%this.expertSet = 0;
				%line = $expertSetting["walk",%this.expertSet];
				bottomPrint(%this.client,%line,2,1);
				return;
			}
			else if (%this.hasMSpine) {
				%this.expertSet++;
				if (%this.expertSet > $expertSettings["mspine"])
					%this.expertSet = 0;
				%line = $expertSetting["mspine",%this.expertSet];
				bottomPrint(%this.client,%line,2,1);
				return;
			}
			else if (%this.hasSwitch) {
				%this.expertSet++;
				if (%this.expertSet > $expertSettings["switch"])
					%this.expertSet = 0;
				%line = $expertSetting["switch",%this.expertSet];
				bottomPrint(%this.client,%line,2,1);
				return;
			}
			else if (%this.hasTripwire) {
				%this.expertSet++;
				if (%this.expertSet > $expertSettings["tripwire"])
					%this.expertSet = 0;
				%line = $expertSetting["tripwire",%this.expertSet];
				bottomPrint(%this.client,%line,2,1);
				return;
			}
			else if (%this.hasTree) {
				%this.expertSet++;
				if (%this.expertSet > $expertSettings["tree"])
					%this.expertSet = 0;
				%line = $expertSetting["tree",%this.expertSet];
				bottomPrint(%this.client,"Tree set to " @ %line * 100 @ "% scale",2,1);
				return;
			}
			else if (%this.hasTele) {
				%this.expertSet++;
				if (%this.expertSet > $expertSettings["telepad"])
					%this.expertSet = 0;
				%line = $expertSetting["telepad",%this.expertSet];
				bottomPrint(%this.client,"Telepad set to " @ %line,2,1);
				return;
			}
  	        else if (%this.hasZspawn) {
				%this.expertSet++;
				if (%this.expertSet > $expertSettings["Zspawn"])
					%this.expertSet = 0;
				%line = $expertSetting["ZSpawn",%this.expertSet];
				bottomPrint(%this.client,"ZSpawn type set to:" SPC %line,2,1);
				return;
			}
			else if (%this.hasdoor) {
                ChangedoorMode(%this, 1);
				return;
			}
			else if (%this.hasCard) {
                ChangeCardMode(%this, 1);
				return;
			}
   			if (%this.getDamageLevel() != 0 && $Host::Purebuild == 1) {
				%this.applyRepair(0.2);
				messageClient(%this.client, 'MsgRepairKitUsed', '\c2Repair Kit Used.');
				return;
			}
		}
	}
	// Weapon modes
	if (%data $= "Mine") {
        //[3.9] Phantom139: Stripped out all of the x specific stuff here in favor of a cleaner system
        if(%this.hasMineModes) {
           //1: Mine Modes
           if(getSimTime() > (%this.changeModeTime + $WeaponModes::SwitchDelay)) {
              %this.getMountedImage($WeaponSlot).changeMode(%this, 1);
              %this.changeModeTime = getSimTime();
           }
           if(%this.changingModeBlocksUse) {
              return;
           }
        }
	}
	// default case
	if (isObject(%data)) {
		if (%this.inv[%data.getName()] > 0) {
			%data.onUse(%this);
			return true;
		}
	}
	return false;
}

function ShapeBase::pickup(%this,%obj,%amount) {
	%data = %obj.getDatablock();
	%delta = %this.incInventory(%data,%amount);

	if (%delta)
		%data.onPickup(%obj,%this,%delta);
	return %delta;
}

function ShapeBase::hasInventory(%this, %data)
{
   // changed because it was preventing weapons cycling correctly (MES)
   return (%this.inv[%data] > 0);
}

function ShapeBase::maxInventory(%this,%data) {
	if($TestCheats && %this.getDatablock().max[%data.getName()] !$= "")
		return 99999;
	else
		return %this.getDatablock().max[%data.getName()];
}

function ShapeBase::incInventory(%this,%data,%amount) {
	%max = %this.maxInventory(%data);
	%cv = %this.inv[%data.getName()];
    //
    //echo(%max TAB %cv TAB %amount TAB %data.alwaysAllowPickup);
    //
	if (%cv < (%max + %data.alwaysAllowPickup)) {
		if (%cv + %amount > (%max + %data.alwaysAllowPickup))
			%amount = %max - %cv;
		%this.setInventory(%data,%cv + %amount);
		%data.incCatagory(%this); // Inc the players weapon count
		return %amount;
	}
	return 0;
}

function ShapeBase::decInventory(%this,%data,%amount) {
   if(%data $= "") {
      return 0;
   }
   %name = %data.getName();
   %cv = %this.inv[%name];
   if (%cv > 0) {
      if (%cv < %amount)
         %amount = %cv;
      %this.setInventory(%data,%cv - %amount, true);
      %data.decCatagory(%this); // Dec the players weapon count
      return %amount;
   }
   return 0;
}

function SimObject::decCatagory(%this)
{
   //function was added to reduce console err msg spam
}

function SimObject::incCatagory(%this)
{
   //function was added to reduce console err msg spam
}

function ShapeBase::setInventory(%this,%data,%value,%force)
{
   if (!isObject(%data))
      return;

   %name = %data.getName();
   if (%value < 0)
      %value = 0;
   else
   {
      if (!%force)
      {
         // Impose inventory limits
         %max = %this.maxInventory(%data);
         if (%value > (%max + %data.alwaysAllowPickup))
            %value = (%max + %data.alwaysAllowPickup);
      }
   }
   if (%this.inv[%name] != %value)
   {
      %this.inv[%name] = %value;
      %data.onInventory(%this,%value);

      if ( %data.className $= "Weapon" )
      {
         if ( %this.weaponSlotCount $= "" )
            %this.weaponSlotCount = 0;

         %cur = -1;
         for ( %slot = 0; %slot < %this.weaponSlotCount; %slot++ )
         {
            if ( %this.weaponSlot[%slot] $= %name )
            {
               %cur = %slot;
               break;
            }
         }

         if ( %cur == -1 )
         {
            // Put this weapon in the next weapon slot:
            if ( %this.weaponSlot[%this.weaponSlotCount - 1] $= "TargetingLaser" )
            {
               %this.weaponSlot[%this.weaponSlotCount - 1] = %name;
               %this.weaponSlot[%this.weaponSlotCount] = "TargetingLaser";
            }
            else
               %this.weaponSlot[%this.weaponSlotCount] = %name;
            %this.weaponSlotCount++;
         }
         else
         {
            // Remove the weapon from the weapon slot:
            for ( %i = %cur; %i < %this.weaponSlotCount - 1; %i++ )
               %this.weaponSlot[%i] = %this.weaponSlot[%i + 1];
            %this.weaponSlot[%i] = "";
            %this.weaponSlotCount--;
         }
      }

      %this.getDataBlock().onInventory(%data,%value);
   }
   return %value;
}

function ShapeBase::getInventory(%this,%data)
{
   if ( isObject( %data ) )
      return( %this.inv[%data.getName()] );
   else
      return( 0 );
}

// z0dd - ZOD, 9/13/02. Streamlined.
function ShapeBase::hasAmmo( %this, %weapon )
{
   if(%weapon $= LaserRifle)
      return( %this.getInventory( EnergyPack ) );

   if (%weapon.image.ammo $= "")
   {
      if (%weapon $= TargetingLaser)
      {
         return( false );
      }
      else
      {
         return( true );
      }
   }
   else
   {
      return( %this.getInventory( %weapon.image.ammo ) > 0 );
   }
}

function SimObject::onInventory(%this, %obj)
{
   //function was added to reduce console error msg spam
}

function ShapeBase::throwItem(%this,%data)
{
   %item = new Item() {
      dataBlock = %data;
      rotation = "0 0 1 " @ (getRandom() * 360);
   };

   %item.ammoStore = %data.ammoStore;
   MissionCleanup.add(%item);
   %this.throwObject(%item);
}

function ShapeBase::throwObject(%this,%obj)
{
   //-------------------------------------------
   // z0dd - ZOD, 5/27/02. Fixes flags hovering
   // over friendly player when collision occurs
   if(%obj.getDataBlock().getName() $= "Flag")
      %obj.static = false;
   //-------------------------------------------

   //if the object is being thrown by a corpse, use a random vector
   if (%this.getState() $= "Dead")
   {
      %vec = (-1.0 + getRandom() * 2.0) SPC (-1.0 + getRandom() * 2.0) SPC getRandom();
      %vec = vectorScale(%vec, 10);
   }

   // else Initial vel based on the dir the player is looking
   else
   {
      %eye = %this.getEyeVector();
      %vec = vectorScale(%eye, 20);
   }

   // Add a vertical component to give the item a better arc
   %dot = vectorDot("0 0 1",%eye);
   if (%dot < 0)
      %dot = -%dot;
   %vec = vectorAdd(%vec,vectorScale("0 0 8",1 - %dot));

   // Add player's velocity
   %vec = vectorAdd(%vec,%this.getVelocity());
   %pos = getBoxCenter(%this.getWorldBox());

   //since flags have a huge mass (so when you shoot them, they don't bounce too far)
   //we need to up the %vec so that you can still throw them...
   if (%obj.getDataBlock().getName() $= "Flag")
      %vec = vectorScale(%vec, 40);

   //
   %obj.setTransform(%pos);
   %obj.applyImpulse(%pos,%vec);
   %obj.setCollisionTimeout(%this);
   %data = %obj.getDatablock();
   %data.onThrow(%obj,%this);

   //call the AI hook
   AIThrowObject(%obj);
}

function ShapeBase::clearInventory(%this)
{
   %this.setInventory(RepairKit,0);
   %this.setInventory(Mine,0);
   %this.setInventory(C4,0);

   %this.setInventory(ConstructionTool,0);
   %this.setInventory(Grenade,0);
   %this.setInventory(FlashGrenade,0);
   %this.setInventory(ConcussionGrenade,0);
   %this.setInventory(FlareGrenade,0);
   %this.setInventory(CameraGrenade, 0);
   %this.setInventory(StaticGrenade,0);
   
   //MY way of doing this (alot better :) )
   %count = DatablockGroup.getCount();
   for(%i = 0; %i < %count; %i++) {
      %db = DatablockGroup.GetObject(%i);
      if(%db.getName().getClassname() $= "ItemData") {
         if(%db.getName().classname $= "Weapon") {
            %Item = %db.getName();
            %this.setInventory(%Item, 0);
            %this.setInventory(%Item @ "ammo", 0);
         }
      }
   }


   // take away any pack the player has
   %curPack = %this.getMountedImage($BackpackSlot);
   if(%curPack > 0)
      %this.setInventory(%curPack.item, 0);

}

//----------------------------------------------------------------------------
function ShapeBase::cycleWeapon( %this, %data )
{
   if ( %this.weaponSlotCount == 0 )
      return;

   %slot = -1;
   if ( %this.getMountedImage($WeaponSlot) != 0 )
   {
      %curWeapon = %this.getMountedImage($WeaponSlot).item.getName();
      for ( %i = 0; %i < %this.weaponSlotCount; %i++ )
      {
         //error("curWeaponName == " @ %curWeaponName);
         if ( %curWeapon $= %this.weaponSlot[%i] )
         {
            %slot = %i;
            break;
         }
      }
   }

   if ( %data $= "prev" )
   {
      // Previous weapon...
      if ( %slot == 0 || %slot == -1 )
      {
         %i = %this.weaponSlotCount - 1;
         %slot = 0;
      }
      else
         %i = %slot - 1;
   }
   else
   {
      // Next weapon...
      if ( %slot == ( %this.weaponSlotCount - 1 ) || %slot == -1 )
      {
         %i = 0;
         %slot = ( %this.weaponSlotCount - 1 );
      }
      else
         %i = %slot + 1;
   }

   %newSlot = -1;
   while ( %i != %slot )
   {
      if ( %this.weaponSlot[%i] !$= ""
        && %this.hasInventory( %this.weaponSlot[%i] )
        && %this.hasAmmo( %this.weaponSlot[%i] ) )
      {
         // player has this weapon and it has ammo or doesn't need ammo
         %newSlot = %i;
         break;
      }

      if ( %data $= "prev" )
      {
         if ( %i == 0 )
            %i = %this.weaponSlotCount - 1;
         else
            %i--;
      }
      else
      {
         if ( %i == ( %this.weaponSlotCount - 1 ) )
            %i = 0;
         else
            %i++;
      }
   }

   if ( %newSlot != -1 )
      %this.use( %this.weaponSlot[%newSlot] );
}

//----------------------------------------------------------------------------
function ShapeBase::selectWeaponSlot( %this, %data )
{
   if ( %data < 0 || %data > %this.weaponSlotCount
     || %this.weaponSlot[%data] $= "" || %this.weaponSlot[%data] $= "TargetingLaser" )
      return;

   %this.use( %this.weaponSlot[%data] );
}

//----------------------------------------------------------------------------

function serverCmdGiveAll(%client)
{
   if($TestCheats)
   {
      %player = %client.player;
      %player.setInventory(RepairKit,999);
      %player.setInventory(Mine,999);
      //%player.setInventory(MineAir,999);
      //%player.setInventory(MineLand,999);
      //%player.setInventory(MineSticky,999);
      %player.setInventory(Grenade,999);
      %player.setInventory(FlashGrenade,999);
      %player.setInventory(FlareGrenade,999);
      %player.setInventory(ConcussionGrenade,999);
      %player.setInventory(CameraGrenade, 999);
      %player.setInventory(Blaster,1);
      %player.setInventory(Plasma,1);
      %player.setInventory(Chaingun, 1);
      %player.setInventory(Disc,1);
      %player.setInventory(GrenadeLauncher, 1);
      %player.setInventory(SniperRifle, 1);
      %player.setInventory(ELFGun, 1);
      %player.setInventory(Mortar, 1);
      %player.setInventory(MissileLauncher, 1);
      %player.setInventory(ShockLance, 1);
      %player.setInventory(TargetingLaser, 1);
      %player.setInventory(MissileLauncherAmmo, 999);
      %player.setInventory(GrenadeLauncherAmmo, 999);
      %player.setInventory(MortarAmmo, 999);
      %player.setInventory(PlasmaAmmo,999);
      %player.setInventory(ChaingunAmmo, 999);
      %player.setInventory(DiscAmmo, 999);
      %player.setInventory(Beacon, 999);

      %player.setInventory(NerfGun,1);
      %player.setInventory(NerfBallLauncher,1);
      %player.setInventory(NerfBallLauncherAmmo,999);
      %player.setInventory(MergeTool,1);

      %player.setInventory(TractorGun, 1);
      %player.setInventory(TransGun, 1);
   }
}
