// A lil' sumthin' by Alviss
// 05/06/09
//------------------------------------------------------------------------------

// A simple patch meant for CCM and it's variants.
// The patch will keep Zombies from infecting and hurting repawning players
// and keep respawning players from doing damage to other players
// under the protection of respawn cloaking/invincibility

//------------------------------------------------------------------------------
// if you want people to be able to shoot while respawn-invincible,
// put:
// $Host::NoSpawnShooting = false;
// into your console

if ($Host::NoSpawnShooting $= "")
  $Host::NoSpawnShooting = true;

//------------------------------------------------------------------------------

package SimpCCMPatch
{
 // Where the infecting business happens
 function Armor::onCollision(%this,%obj,%col,%forceVehicleNode)
 {
   // if the colliding object is a zombie and you're respawning
   if ((%col.IsZombie) && %obj.respawnCloakThread !$= "")
     return; // you'll be safe.

   Parent::onCollision(%this,%obj,%col,%forceVehicleNode);
 }

 // Where shooting damage happens
 function ShapeBase::damage(%this, %sourceObject, %position, %amount, %damageType)
 {
   // if the variable is active
   if ($Host::SpawnShooting)
   {
     // if all the parties check out
     if (isObject(%sourceObject) && %this.isEnabled() && (!%this.isZombie))
     {
      // if the client just respawned
      if (%sourceObject.respawnCloakThread !$= "")
      {
        // pull them out of invincibiltiy and stop the damaging process
        %sourceObject.setInvincible(false);
        %sourceObject.setRespawnCloakOff();
        return;
      }
     }
   }
   // continue the routine normally
   Parent::damage(%this, %sourceObject, %position, %amount, %damageType);
 }

};

if (!isActivePackage(SimpCCMPatch)) {
  echo("Activating CCM Prot Patch");
  ActivatePackage(SimpCCMPatch);
  $Host::NoSpawnShooting = true;
}
