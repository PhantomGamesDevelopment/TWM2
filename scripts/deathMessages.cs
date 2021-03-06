

/////////////////////////////////////////////////////////////////////////////////////////////////
// %1 = Victim's name                                                                          //
// %2 = Victim's gender (value will be either "him" or "her")                                  //
// %3 = Victim's possessive gender	(value will be either "his" or "her")                      //
// %4 = Killer's name                                                                          //
// %5 = Killer's gender (value will be either "him" or "her")                                  //
// %6 = Killer's possessive gender (value will be either "his" or "her")                       //
// %7 = implement that killed the victim (value is the object number of the bullet, disc, etc) //
/////////////////////////////////////////////////////////////////////////////////////////////////

$DeathMessageCampingCount = 1;
$DeathMessageCamping[0] = '\c0%1 was killed for camping near the Nexus.';

 //Out of Bounds deaths
$DeathMessageOOBCount = 1;
$DeathMessageOOB[0] = '\c0%1 was killed for loitering outside the mission area.';

$DeathMessageLavaCount = 4;
$DeathMessageLava[0] = '\c0%1\'s last thought before falling into the lava : \'Oops\'.';
$DeathMessageLava[1] = '\c0%1 makes the supreme sacrifice to the lava gods.';
$DeathMessageLava[2] = '\c0%1 looks surprised by the lava - but only briefly.';
$DeathMessageLava[3] = '\c0%1 wimps out by jumping into the lava and trying to make it look like an accident.';

$DeathMessageLightningCount = 3;
$DeathMessageLightning[0] = '\c0%1 was killed by lightning!';
$DeathMessageLightning[1] = '\c0%1 caught a lightning bolt!';
$DeathMessageLightning[2] = '\c0%1 stuck %3 finger in Mother Nature\'s light socket.';

//these used when a player presses ctrl-k
$DeathMessageSuicideCount = 5;
$DeathMessageSuicide[0] = '\c0%1 blows %3 own head off!';
$DeathMessageSuicide[1] = '\c0%1 ends it all. Cue violin music.';
$DeathMessageSuicide[2] = '\c0%1 kills %2self.';
$DeathMessageSuicide[3] = '\c0%1 goes for the quick and dirty respawn.';
$DeathMessageSuicide[4] = '\c0%1 self-destructs in a fit of ennui.';

$DeathMessageVehPadCount = 1;
$DeathMessageVehPad[0] = '\c0%1 got caught in a vehicle\'s spawn field.';

$DeathMessageFFPowerupCount = 3;
$DeathMessageFFPowerup[0] = '\c0%1 got caught up in a forcefield during power up.';
$DeathMessageFFPowerup[1] = '\c0%1 finds %2self in a forcefield powering up.';
$DeathMessageFFPowerup[2] = '\c0%1 gets %2self fried in a forcefield.';

$DeathMessageRogueMineCount = 1;
$DeathMessageRogueMine[$DamageType::Mine, 0] = '\c0%1 is all mine.';

//these used when a player kills himself (other than by using ctrl - k)
$DeathMessageSelfKillCount = 5;
$DeathMessageSelfKill[$DamageType::Blaster, 0] = '\c0%1 kills %2self with a blaster.';
$DeathMessageSelfKill[$DamageType::Blaster, 1] = '\c0%1 makes a note to watch out for blaster ricochets.';
$DeathMessageSelfKill[$DamageType::Blaster, 2] = '\c0%1\'s blaster kills its hapless owner.';
$DeathMessageSelfKill[$DamageType::Blaster, 3] = '\c0%1 deftly guns %2self down with %3 own blaster.';
$DeathMessageSelfKill[$DamageType::Blaster, 4] = '\c0%1 has a fatal encounter with %3 own blaster.';

$DeathMessageSelfKill[$DamageType::Plasma, 0] = '\c0%1 kills %2self with plasma.';
$DeathMessageSelfKill[$DamageType::Plasma, 1] = '\c0%1 turns %2self into plasma-charred briquettes.';
$DeathMessageSelfKill[$DamageType::Plasma, 2] = '\c0%1 swallows a white-hot mouthful of %3 own plasma.';
$DeathMessageSelfKill[$DamageType::Plasma, 3] = '\c0%1 immolates %2self.';
$DeathMessageSelfKill[$DamageType::Plasma, 4] = '\c0%1 experiences the joy of cooking %2self.';

$DeathMessageSelfKill[$DamageType::Disc, 0] = '\c0%1 kills %2self with a disc.';
$DeathMessageSelfKill[$DamageType::Disc, 1] = '\c0%1 catches %3 own spinfusor disc.';
$DeathMessageSelfKill[$DamageType::Disc, 2] = '\c0%1 heroically falls on %3 own disc.';
$DeathMessageSelfKill[$DamageType::Disc, 3] = '\c0%1 helpfully jumps into %3 own disc\'s explosion.';
$DeathMessageSelfKill[$DamageType::Disc, 4] = '\c0%1 plays Russian roulette with %3 spinfusor.';

$DeathMessageSelfKill[$DamageType::Grenade, 0] = '\c0%1 destroys %2self with a grenade!';   //applies to hand grenades *and* grenade launcher grenades
$DeathMessageSelfKill[$DamageType::Grenade, 1] = '\c0%1 took a bad bounce from %3 own grenade!';
$DeathMessageSelfKill[$DamageType::Grenade, 2] = '\c0%1 pulled the pin a shade early.';
$DeathMessageSelfKill[$DamageType::Grenade, 3] = '\c0%1\'s own grenade turns on %2.';
$DeathMessageSelfKill[$DamageType::Grenade, 4] = '\c0%1 blows %2self up real good.';

$DeathMessageSelfKill[$DamageType::Mortar, 0] = '\c0%1 kills %2self with a mortar!';
$DeathMessageSelfKill[$DamageType::Mortar, 1] = '\c0%1 hugs %3 own big green boomie.';
$DeathMessageSelfKill[$DamageType::Mortar, 2] = '\c0%1 mortars %2self all over the map.';
$DeathMessageSelfKill[$DamageType::Mortar, 3] = '\c0%1 experiences %3 mortar\'s payload up close.';
$DeathMessageSelfKill[$DamageType::Mortar, 4] = '\c0%1 suffered the wrath of %3 own mortar.';

$DeathMessageSelfKill[$DamageType::Missile, 0] = '\c0%1 kills %2self with a missile!';
$DeathMessageSelfKill[$DamageType::Missile, 1] = '\c0%1 runs a missile up %3 own tailpipe.';
$DeathMessageSelfKill[$DamageType::Missile, 2] = '\c0%1 tests the missile\'s shaped charge on %2self.';
$DeathMessageSelfKill[$DamageType::Missile, 3] = '\c0%1 achieved missile lock on %2self.';
$DeathMessageSelfKill[$DamageType::Missile, 4] = '\c0%1 gracefully smoked %2self with a missile!';

$DeathMessageSelfKill[$DamageType::Mine, 0] = '\c0%1 kills %2self with a mine!';
$DeathMessageSelfKill[$DamageType::Mine, 1] = '\c0%1\'s mine violently reminds %2 of its existence.';
$DeathMessageSelfKill[$DamageType::Mine, 2] = '\c0%1 plants a decisive foot on %3 own mine!';
$DeathMessageSelfKill[$DamageType::Mine, 3] = '\c0%1 fatally trips on %3 own mine!';
$DeathMessageSelfKill[$DamageType::Mine, 4] = '\c0%1 makes a note not to run over %3 own mines.';

$DeathMessageSelfKill[$DamageType::SatchelCharge, 0] = '\c0%1 goes out with a bang!';  //applies to most explosion types
$DeathMessageSelfKill[$DamageType::SatchelCharge, 1] = '\c0%1 fall down...go boom.';
$DeathMessageSelfKill[$DamageType::SatchelCharge, 2] = '\c0%1 explodes in that fatal kind of way.';
$DeathMessageSelfKill[$DamageType::SatchelCharge, 3] = '\c0%1 experiences explosive decompression!';
$DeathMessageSelfKill[$DamageType::SatchelCharge, 4] = '\c0%1 splashes all over the map.';

$DeathMessageSelfKill[$DamageType::Ground, 0] = '\c0%1 lands too hard.';
$DeathMessageSelfKill[$DamageType::Ground, 1] = '\c0%1 finds gravity unforgiving.';
$DeathMessageSelfKill[$DamageType::Ground, 2] = '\c0%1 craters on impact.';
$DeathMessageSelfKill[$DamageType::Ground, 3] = '\c0%1 pancakes upon landing.';
$DeathMessageSelfKill[$DamageType::Ground, 4] = '\c0%1 loses a game of chicken with the ground.';

$DeathMessageSelfKill[$DamageType::RPG, 0] = '\c0%1 Was pointing the RPG tube the wrong way.';
$DeathMessageSelfKill[$DamageType::RPG, 1] = '\c0%1 Accidentally drops a few RPG shells, they explode.';
$DeathMessageSelfKill[$DamageType::RPG, 2] = '\c0%1 Mis-fired %3 RPG.';
$DeathMessageSelfKill[$DamageType::RPG, 3] = '\c0%1 Blows %2self out of existance with an RPG.';
$DeathMessageSelfKill[$DamageType::RPG, 4] = '\c0%1 Nails %3 face with an RPG.';

$DeathMessageSelfKill[$DamageType::Fire, 0] = '\c0%1 crisped %2self with a flamethrower.';
$DeathMessageSelfKill[$DamageType::Fire, 1] = '\c0%1 obviously wanted to feel third degree burns.';
$DeathMessageSelfKill[$DamageType::Fire, 2] = '\c0%1 burns %2self with %3 flamethrower.';
$DeathMessageSelfKill[$DamageType::Fire, 3] = '\c0%1 burns %2self down to cinders.';
$DeathMessageSelfKill[$DamageType::Fire, 4] = '\c0%1 engulfs %2self in flames.';

$DeathMessageSelfKill[$DamageType::FissionBomb, 0] = '\c0%1 Unleashed a fission bomb.';
$DeathMessageSelfKill[$DamageType::FissionBomb, 1] = '\c0%1 Unleashed a fission bomb.';
$DeathMessageSelfKill[$DamageType::FissionBomb, 2] = '\c0%1 Unleashed a fission bomb.';
$DeathMessageSelfKill[$DamageType::FissionBomb, 3] = '\c0%1 Unleashed a fission bomb.';
$DeathMessageSelfKill[$DamageType::FissionBomb, 4] = '\c0%1 Unleashed a fission bomb.';

//used when a player is killed by a teammate
$DeathMessageTeamKillCount = 1;
$DeathMessageTeamKill[$DamageType::Blaster, 0] = '\c0%4 TEAMKILLED %1 with a blaster!';
$DeathMessageTeamKill[$DamageType::Plasma, 0] = '\c0%4 TEAMKILLED %1 with a plasma rifle!';
$DeathMessageTeamKill[$DamageType::Bullet, 0] = '\c0%4 TEAMKILLED %1 with a chaingun!';
$DeathMessageTeamKill[$DamageType::Disc, 0] = '\c0%4 TEAMKILLED %1 with a spinfusor!';
$DeathMessageTeamKill[$DamageType::Grenade, 0] = '\c0%4 TEAMKILLED %1 with a grenade!';
$DeathMessageTeamKill[$DamageType::Laser, 0] = '\c0%4 TEAMKILLED %1 with a laser rifle!';
$DeathMessageTeamKill[$DamageType::Elf, 0] = '\c0%4 TEAMKILLED %1 with an ELF projector!';
$DeathMessageTeamKill[$DamageType::Mortar, 0] = '\c0%4 TEAMKILLED %1 with a mortar!';
$DeathMessageTeamKill[$DamageType::Missile, 0] = '\c0%4 TEAMKILLED %1 with a missile!';
$DeathMessageTeamKill[$DamageType::Shocklance, 0] = '\c0%4 TEAMKILLED %1 with a shocklance!';
$DeathMessageTeamKill[$DamageType::Mine, 0] = '\c0%4 TEAMKILLED %1 with a mine!';
$DeathMessageTeamKill[$DamageType::SatchelCharge, 0] = '\c0%4 blew up TEAMMATE %1!';
$DeathMessageTeamKill[$DamageType::Impact, 0] = '\c0%4 runs down TEAMMATE %1!';
$DeathMessageTeamKill[$DamageType::SuperChaingun, 0] = '\c0%4 TEAMKILLED %1 with a super chaingun!';
$DeathMessageTeamKill[$DamageType::Pistol, 0] = '\c0%4 TEAMKILLED %1 with a colt pistol!';
$DeathMessageTeamKill[$DamageType::Melee, 0] = '\c0%4 Swings at TEAMMATE %1, and doesn\'t stop in time!';
$DeathMessageTeamKill[$DamageType::S3, 0] = '\c0%4 Nailed TEAMMATE %1 with an S3 Combat Rifle!';
$DeathMessageTeamKill[$DamageType::W1700, 0] = '\c0%4 Blasts TEAMMATE %1 with a W1700 Shotgun!';
$DeathMessageTeamKill[$DamageType::G41, 0] = '\c0%4 Kills TEAMMATE %1 with Semi-Automatic G41 Fire!';
$DeathMessageTeamKill[$DamageType::R700, 0] = '\c0%4 Blasts TEAMMATE %1 from Long Range with %6 R700!';
$DeathMessageTeamKill[$DamageType::MP26, 0] = '\c0%4 Kills TEAMMATE %1 with a MP26 SMG!';
$DeathMessageTeamKill[$DamageType::Pg700, 0] = '\c0%4 Unloads Pg700 Rounds on TEAMMATE %1!';
$DeathMessageTeamKill[$DamageType::M1, 0] = '\c0%4 Nails TEAMMATE %1 with an M1 Sniper Rifle!';
$DeathMessageTeamKill[$DamageType::RP432, 0] = '\c0%4 Shreds up TEAMMATE %1 with %6 RP432 MG!';
$DeathMessageTeamKill[$DamageType::RPG, 0] = '\c0%4 Blows TEAMMATE %1 away with an RPG!';
$DeathMessageTeamKill[$DamageType::GravBolt, 0] = '\c0%4 ships TEAMMATE %1 to the upper atmosphere with a grav cannon!';
$DeathMessageTeamKill[$DamageType::LaserRifle, 0] = '\c0%4 burns TEAMMATE %1 with %6 RSA Laser Rifle';
$DeathMessageTeamKill[$DamageType::Fire, 0] = '\c0%4 burns TEAMMATE %1 with a flamethrower';
$DeathMessageTeamKill[$DamageType::SA2400, 0] = '\c0%4 Nails TEAMMATE %1 with a SA2400';
$DeathMessageTeamKill[$DamageType::DesertEagle, 0] = '\c0%4 blasts TEAMMATE %1 with a desert eagle pistol';
$DeathMessageTeamKill[$DamageType::FissionBomb, 0] = '\c0%4 kills %1 with a Fission Bomb.';


//these used when a player is killed by an enemy
$DeathMessageCount = 5;
$DeathMessage[$DamageType::Blaster, 0] = '\c0%4 kills %1 with a blaster.';
$DeathMessage[$DamageType::Blaster, 1] = '\c0%4 pings %1 to death.';
$DeathMessage[$DamageType::Blaster, 2] = '\c0%1 gets a pointer in blaster use from %4.';
$DeathMessage[$DamageType::Blaster, 3] = '\c0%4 fatally embarrasses %1 with %6 pea shooter.';
$DeathMessage[$DamageType::Blaster, 4] = '\c0%4 unleashes a terminal blaster barrage into %1.';

$DeathMessage[$DamageType::Plasma, 0] = '\c0%4 roasts %1 with the plasma rifle.';
$DeathMessage[$DamageType::Plasma, 1] = '\c0%4 gooses %1 with an extra-friendly burst of plasma.';
$DeathMessage[$DamageType::Plasma, 2] = '\c0%4 entices %1 to try a faceful of plasma.';
$DeathMessage[$DamageType::Plasma, 3] = '\c0%4 introduces %1 to the plasma immolation dance.';
$DeathMessage[$DamageType::Plasma, 4] = '\c0%4 slaps The Hot Kiss of Death on %1.';

$DeathMessage[$DamageType::Bullet, 0] = '\c0%4 rips %1 up with the chaingun.';
$DeathMessage[$DamageType::Bullet, 1] = '\c0%4 happily chews %1 into pieces with %6 chaingun.';
$DeathMessage[$DamageType::Bullet, 2] = '\c0%4 administers a dose of Vitamin Lead to %1.';
$DeathMessage[$DamageType::Bullet, 3] = '\c0%1 suffers a serious hosing from %4\'s chaingun.';
$DeathMessage[$DamageType::Bullet, 4] = '\c0%4 bestows the blessings of %6 chaingun on %1.';

$DeathMessage[$DamageType::Disc, 0] = '\c0%4 demolishes %1 with the spinfusor.';
$DeathMessage[$DamageType::Disc, 1] = '\c0%4 serves %1 a blue plate special.';
$DeathMessage[$DamageType::Disc, 2] = '\c0%4 shares a little blue friend with %1.';
$DeathMessage[$DamageType::Disc, 3] = '\c0%4 puts a little spin into %1.';
$DeathMessage[$DamageType::Disc, 4] = '\c0%1 becomes one of %4\'s greatest hits.';

$DeathMessage[$DamageType::Grenade, 0] = '\c0%4 eliminates %1 with a grenade.';   //applies to hand grenades *and* grenade launcher grenades
$DeathMessage[$DamageType::Grenade, 1] = '\c0%4 blows up %1 real good!';
$DeathMessage[$DamageType::Grenade, 2] = '\c0%1 gets annihilated by %4\'s grenade.';
$DeathMessage[$DamageType::Grenade, 3] = '\c0%1 receives a kaboom lesson from %4.';
$DeathMessage[$DamageType::Grenade, 4] = '\c0%4 turns %1 into grenade salad.';

$DeathMessage[$DamageType::Laser, 0] = '\c0%1 becomes %4\'s latest pincushion.';
$DeathMessage[$DamageType::Laser, 1] = '\c0%4 picks off %1 with %6 laser rifle.';
$DeathMessage[$DamageType::Laser, 2] = '\c0%4 uses %1 as the targeting dummy in a sniping demonstration.';
$DeathMessage[$DamageType::Laser, 3] = '\c0%4 pokes a shiny new hole in %1 with %6 laser rifle.';
$DeathMessage[$DamageType::Laser, 4] = '\c0%4 caresses %1 with a couple hundred megajoules of laser.';

$DeathMessage[$DamageType::Elf, 0] = '\c0%4 fries %1 with the ELF projector.';
$DeathMessage[$DamageType::Elf, 1] = '\c0%4 bug zaps %1 with %6 ELF.';
$DeathMessage[$DamageType::Elf, 2] = '\c0%1 learns the shocking truth about %4\'s ELF skills.';
$DeathMessage[$DamageType::Elf, 3] = '\c0%4 electrocutes %1 without a sponge.';
$DeathMessage[$DamageType::Elf, 4] = '\c0%4\'s ELF projector leaves %1 a crispy critter.';

$DeathMessage[$DamageType::Mortar, 0] = '\c0%4 obliterates %1 with the mortar.';
$DeathMessage[$DamageType::Mortar, 1] = '\c0%4 drops a mortar round right in %1\'s lap.';
$DeathMessage[$DamageType::Mortar, 2] = '\c0%4 delivers a mortar payload straight to %1.';
$DeathMessage[$DamageType::Mortar, 3] = '\c0%4 offers a little "heavy love" to %1.';
$DeathMessage[$DamageType::Mortar, 4] = '\c0%1 stumbles into %4\'s mortar reticle.';

$DeathMessage[$DamageType::Missile, 0] = '\c0%4 intercepts %1 with a missile.';
$DeathMessage[$DamageType::Missile, 1] = '\c0%4 watches %6 missile touch %1 and go boom.';
$DeathMessage[$DamageType::Missile, 2] = '\c0%4 got sweet tone on %1.';
$DeathMessage[$DamageType::Missile, 3] = '\c0By now, %1 has realized %4\'s missile killed %2.';
$DeathMessage[$DamageType::Missile, 4] = '\c0%4\'s missile smacks directly into %1.';

$DeathMessage[$DamageType::Shocklance, 0] = '\c0%4 reaps a harvest of %1 with the shocklance.';
$DeathMessage[$DamageType::Shocklance, 1] = '\c0%4 feeds %1 the business end of %6 shocklance.';
$DeathMessage[$DamageType::Shocklance, 2] = '\c0%4 stops %1 dead with the shocklance.';
$DeathMessage[$DamageType::Shocklance, 3] = '\c0%4 eliminates %1 in close combat.';
$DeathMessage[$DamageType::Shocklance, 4] = '\c0%4 ruins %1\'s day with one zap of a shocklance.';

$DeathMessage[$DamageType::Mine, 0] = '\c0%4 kills %1 with a mine.';
$DeathMessage[$DamageType::Mine, 1] = '\c0%1 doesn\'t see %4\'s mine in time.';
$DeathMessage[$DamageType::Mine, 2] = '\c0%4 gets a sapper kill on %1.';
$DeathMessage[$DamageType::Mine, 3] = '\c0%1 puts his foot on %4\'s mine.';
$DeathMessage[$DamageType::Mine, 4] = '\c0One small step for %1, one giant mine kill for %4.';

$DeathMessage[$DamageType::SatchelCharge, 0] = '\c0%4 buys %1 a ticket to the moon.';  //satchel charge only
$DeathMessage[$DamageType::SatchelCharge, 1] = '\c0%4 blows %1 into low orbit.';
$DeathMessage[$DamageType::SatchelCharge, 2] = '\c0%4 makes %1 a hugely explosive offer.';
$DeathMessage[$DamageType::SatchelCharge, 3] = '\c0%4 turns %1 into a cloud of satchel-vaporized armor.';
$DeathMessage[$DamageType::SatchelCharge, 4] = '\c0%4\'s satchel charge leaves %1 nothin\' but smokin\' boots.';

$DeathMessageHeadshotCount = 3;
$DeathMessageHeadshot[$DamageType::Laser, 0] = '\c0%4 drills right through %1\'s braincase with %6 laser.';
$DeathMessageHeadshot[$DamageType::Laser, 1] = '\c0%4 pops %1\'s head like a cheap balloon.';
$DeathMessageHeadshot[$DamageType::Laser, 2] = '\c0%1 loses %3 head over %4\'s laser skill.';


//These used when a player is run over by a vehicle
$DeathMessageVehicleCount = 5;
$DeathMessageVehicle[0] = '\c0%4 runs down %1.';
$DeathMessageVehicle[1] = '\c0%1 acquires that run-down feeling from %4.';
$DeathMessageVehicle[2] = '\c0%4 transforms %1 into tribal roadkill.';
$DeathMessageVehicle[3] = '\c0%1 makes a painfully close examination of %4\'s front bumper.';
$DeathMessageVehicle[4] = '\c0%1\'s messy death leaves a mark on %4\'s vehicle finish.';

$DeathMessageVehicleCrashCount = 5;
$DeathMessageVehicleCrash[ $DamageType::Crash, 0 ] = '\c0%1 fails to eject in time.';
$DeathMessageVehicleCrash[ $DamageType::Crash, 1 ] = '\c0%1 becomes one with his vehicle dashboard.';
$DeathMessageVehicleCrash[ $DamageType::Crash, 2 ] = '\c0%1 drives under the influence of death.';
$DeathMessageVehicleCrash[ $DamageType::Crash, 3 ] = '\c0%1 makes a perfect three hundred point landing.';
$DeathMessageVehicleCrash[ $DamageType::Crash, 4 ] = '\c0%1 heroically pilots his vehicle into something really, really hard.';

$DeathMessageVehicleFriendlyCount = 3;
$DeathMessageVehicleFriendly[0] = '\c0%1 gets in the way of a friendly vehicle.';
$DeathMessageVehicleFriendly[1] = '\c0Sadly, a friendly vehicle turns %1 into roadkill.';
$DeathMessageVehicleFriendly[2] = '\c0%1 becomes an unsightly ornament on a team vehicle\'s hood.';

$DeathMessageVehicleUnmannedCount = 3;
$DeathMessageVehicleUnmanned[0] = '\c0%1 gets in the way of a runaway vehicle.';
$DeathMessageVehicleUnmanned[1] = '\c0An unmanned vehicle kills the pathetic %1.';
$DeathMessageVehicleUnmanned[2] = '\c0%1 is struck down by an empty vehicle.';

//These used when a player is killed by a nearby equipment explosion
$DeathMessageExplosionCount = 3;
$DeathMessageExplosion[0] = '\c0%1 was killed by exploding equipment!';
$DeathMessageExplosion[1] = '\c0%1 stood a little too close to the action!';
$DeathMessageExplosion[2] = '\c0%1 learns how to be collateral damage.';

//These used when an automated turret kills an  enemy player
$DeathMessageTurretKillCount = 3;
$DeathMessageTurretKill[$DamageType::PlasmaTurret, 0] = '\c0%1 is killed by a plasma turret.';
$DeathMessageTurretKill[$DamageType::PlasmaTurret, 1] = '\c0%1\'s body now marks the location of a plasma turret.';
$DeathMessageTurretKill[$DamageType::PlasmaTurret, 2] = '\c0%1 is fried by a plasma turret.';

$DeathMessageTurretKill[$DamageType::AATurret, 0] = '\c0%1 is killed by an AA turret.';
$DeathMessageTurretKill[$DamageType::AATurret, 1] = '\c0%1 is shot down by an AA turret.';
$DeathMessageTurretKill[$DamageType::AATurret, 2] = '\c0%1 takes fatal flak from an AA turret.';

$DeathMessageTurretKill[$DamageType::ElfTurret, 0] = '\c0%1 is killed by an ELF turret.';
$DeathMessageTurretKill[$DamageType::ElfTurret, 1] = '\c0%1 is zapped by an ELF turret.';
$DeathMessageTurretKill[$DamageType::ElfTurret, 2] = '\c0%1 is short-circuited by an ELF turret.';

$DeathMessageTurretKill[$DamageType::MortarTurret, 0] = '\c0%1 is killed by a mortar turret.';
$DeathMessageTurretKill[$DamageType::MortarTurret, 1] = '\c0%1 enjoys a mortar turret\'s attention.';
$DeathMessageTurretKill[$DamageType::MortarTurret, 2] = '\c0%1 is blown to kibble by a mortar turret.';

$DeathMessageTurretKill[$DamageType::MissileTurret, 0] = '\c0%1 is killed by a missile turret.';
$DeathMessageTurretKill[$DamageType::MissileTurret, 1] = '\c0%1 is shot down by a missile turret.';
$DeathMessageTurretKill[$DamageType::MissileTurret, 2] = '\c0%1 is blown away by a missile turret.';

$DeathMessageTurretKill[$DamageType::IndoorDepTurret, 0] = '\c0%1 is killed by a clamp turret.';
$DeathMessageTurretKill[$DamageType::IndoorDepTurret, 1] = '\c0%1 gets burned by a clamp turret.';
$DeathMessageTurretKill[$DamageType::IndoorDepTurret, 2] = '\c0A clamp turret eliminates %1.';

$DeathMessageTurretKill[$DamageType::OutdoorDepTurret, 0] = '\c0A spike turret neatly drills %1.';
$DeathMessageTurretKill[$DamageType::OutdoorDepTurret, 1] = '\c0%1 gets taken out by a spike turret.';
$DeathMessageTurretKill[$DamageType::OutdoorDepTurret, 2] = '\c0%1 dies under a spike turret\'s love.';

$DeathMessageTurretKill[$DamageType::SentryTurret, 0] = '\c0%1 didn\'t see that Sentry turret, but it saw %2...';
$DeathMessageTurretKill[$DamageType::SentryTurret, 1] = '\c0%1 needs to watch for Sentry turrets.';
$DeathMessageTurretKill[$DamageType::SentryTurret, 2] = '\c0%1 now understands how Sentry turrets work.';


//used when a player is killed by a teammate controlling a turret
$DeathMessageCTurretTeamKillCount = 1;
$DeathMessageCTurretTeamKill[$DamageType::PlasmaTurret, 0] = '\c0%4 TEAMKILLED %1 with a plasma turret!';

$DeathMessageCTurretTeamKill[$DamageType::AATurret, 0] = '\c0%4 TEAMKILLED %1 with an AA turret!';

$DeathMessageCTurretTeamKill[$DamageType::ELFTurret, 0] = '\c0%4 TEAMKILLED %1 with an ELF turret!';

$DeathMessageCTurretTeamKill[$DamageType::MortarTurret, 0] = '\c0%4 TEAMKILLED %1 with a mortar turret!';

$DeathMessageCTurretTeamKill[$DamageType::MissileTurret, 0] = '\c0%4 TEAMKILLED %1 with a missile turret!';

$DeathMessageCTurretTeamKill[$DamageType::IndoorDepTurret, 0] = '\c0%4 TEAMKILLED %1 with a clamp turret!';

$DeathMessageCTurretTeamKill[$DamageType::OutdoorDepTurret, 0] = '\c0%4 TEAMKILLED %1 with a spike turret!';

$DeathMessageCTurretTeamKill[$DamageType::SentryTurret, 0] = '\c0%4 TEAMKILLED %1 with a sentry turret!';

$DeathMessageCTurretTeamKill[$DamageType::BomberBombs, 0] = '\c0%4 TEAMKILLED %1 in a bombastic explosion of raining death.';

$DeathMessageCTurretTeamKill[$DamageType::BellyTurret, 0] = '\c0%4 TEAMKILLED %1 by annihilating him from a belly turret.';

$DeathMessageCTurretTeamKill[$DamageType::TankChainGun, 0] = '\c0%4 TEAMKILLED %1 with his tank\'s chaingun.';

$DeathMessageCTurretTeamKill[$DamageType::TankMortar, 0] = '\c0%4 TEAMKILLED %1 by lobbing the BIG green death from a tank.';

$DeathMessageCTurretTeamKill[$DamageType::ShrikeBlaster, 0] = '\c0%4 TEAMKILLED %1 by strafing from a Shrike.';

$DeathMessageCTurretTeamKill[$DamageType::MPBMissile, 0] = '\c0%4 TEAMKILLED %1 when the MPB locked onto him.';



//used when a player is killed by an uncontrolled, friendly turret
$DeathMessageCTurretAccdtlKillCount = 1;
$DeathMessageCTurretAccdtlKill[$DamageType::PlasmaTurret, 0] = '\c0%1 got in the way of a plasma turret!';

$DeathMessageCTurretAccdtlKill[$DamageType::AATurret, 0] = '\c0%1 got in the way of an AA turret!';

$DeathMessageCTurretAccdtlKill[$DamageType::ELFTurret, 0] = '\c0%1 got in the way of an ELF turret!';

$DeathMessageCTurretAccdtlKill[$DamageType::MortarTurret, 0] = '\c0%1 got in the way of a mortar turret!';

$DeathMessageCTurretAccdtlKill[$DamageType::MissileTurret, 0] = '\c0%1 got in the way of a missile turret!';

$DeathMessageCTurretAccdtlKill[$DamageType::IndoorDepTurret, 0] = '\c0%1 got in the way of a clamp turret!';

$DeathMessageCTurretAccdtlKill[$DamageType::OutdoorDepTurret, 0] = '\c0%1 got in the way of a spike turret!';

$DeathMessageCTurretAccdtlKill[$DamageType::SentryTurret, 0] = '\c0%1 got in the way of a Sentry turret!';


//these messages for owned or controlled turrets
$DeathMessageCTurretKillCount = 3;
$DeathMessageCTurretKill[$DamageType::PlasmaTurret, 0] = '\c0%4 torches %1 with a plasma turret!';
$DeathMessageCTurretKill[$DamageType::PlasmaTurret, 1] = '\c0%4 fries %1 with a plasma turret!';
$DeathMessageCTurretKill[$DamageType::PlasmaTurret, 2] = '\c0%4 lights up %1 with a plasma turret!';

$DeathMessageCTurretKill[$DamageType::AATurret, 0] = '\c0%4 shoots down %1 with an AA turret.';
$DeathMessageCTurretKill[$DamageType::AATurret, 1] = '\c0%1 gets shot down by %1\'s AA turret.';
$DeathMessageCTurretKill[$DamageType::AATurret, 2] = '\c0%4 takes out %1 with an AA turret.';

$DeathMessageCTurretKill[$DamageType::ElfTurret, 0] = '\c0%1 gets zapped by ELF gunner %4.';
$DeathMessageCTurretKill[$DamageType::ElfTurret, 1] = '\c0%1 gets barbecued by ELF gunner %4.';
$DeathMessageCTurretKill[$DamageType::ElfTurret, 2] = '\c0%1 gets shocked by ELF gunner %4.';

$DeathMessageCTurretKill[$DamageType::MortarTurret, 0] = '\c0%1 is annihilated by %4\'s mortar turret.';
$DeathMessageCTurretKill[$DamageType::MortarTurret, 1] = '\c0%1 is blown away by %4\'s mortar turret.';
$DeathMessageCTurretKill[$DamageType::MortarTurret, 2] = '\c0%1 is pureed by %4\'s mortar turret.';

$DeathMessageCTurretKill[$DamageType::MissileTurret, 0] = '\c0%4 shows %1 a new world of pain with a missile turret.';
$DeathMessageCTurretKill[$DamageType::MissileTurret, 1] = '\c0%4 pops %1 with a missile turret.';
$DeathMessageCTurretKill[$DamageType::MissileTurret, 2] = '\c0%4\'s missile turret lights up %1\'s, uh, ex-life.';

$DeathMessageCTurretKill[$DamageType::IndoorDepTurret, 0] = '\c0%1 is chewed up and spat out by %4\'s clamp turret.';
$DeathMessageCTurretKill[$DamageType::IndoorDepTurret, 1] = '\c0%1 is knocked out by %4\'s clamp turret.';
$DeathMessageCTurretKill[$DamageType::IndoorDepTurret, 2] = '\c0%4\'s clamp turret drills %1 nicely.';

$DeathMessageCTurretKill[$DamageType::OutdoorDepTurret, 0] = '\c0%1 is chewed up by %4\'s spike turret.';
$DeathMessageCTurretKill[$DamageType::OutdoorDepTurret, 1] = '\c0%1 feels the burn from %4\'s spike turret.';
$DeathMessageCTurretKill[$DamageType::OutdoorDepTurret, 2] = '\c0%1 is nailed by %4\'s spike turret.';

$DeathMessageCTurretKill[$DamageType::SentryTurret, 0] = '\c0%4 caught %1 by surprise with a turret.';
$DeathMessageCTurretKill[$DamageType::SentryTurret, 1] = '\c0%4\'s turret took out %1.';
$DeathMessageCTurretKill[$DamageType::SentryTurret, 2] = '\c0%4 blasted %1 with a turret.';

$DeathMessageCTurretKill[$DamageType::BomberBombs, 0] = '\c0%1 catches %4\'s bomb in both teeth.';
$DeathMessageCTurretKill[$DamageType::BomberBombs, 1] = '\c0%4 leaves %1 a smoking bomb crater.';
$DeathMessageCTurretKill[$DamageType::BomberBombs, 2] = '\c0%4 bombs %1 back to the 20th century.';

$DeathMessageCTurretKill[$DamageType::BellyTurret, 0] = '\c0%1 eats a big helping of %4\'s belly turret bolt.';
$DeathMessageCTurretKill[$DamageType::BellyTurret, 1] = '\c0%4 plants a belly turret bolt in %1\'s belly.';
$DeathMessageCTurretKill[$DamageType::BellyTurret, 2] = '\c0%1 fails to evade %4\'s deft bomber strafing.';

$DeathMessageCTurretKill[$DamageType::TankChainGun, 0] = '\c0%1 enjoys the rich, metallic taste of %4\'s tank slug.';
$DeathMessageCTurretKill[$DamageType::TankChainGun, 1] = '\c0%4\'s tank chaingun plays sweet music all over %1.';
$DeathMessageCTurretKill[$DamageType::TankChainGun, 2] = '\c0%1 receives a stellar exit wound from %4\'s tank slug.';

$DeathMessageCTurretKill[$DamageType::TankMortar, 0] = '\c0Whoops! %1 + %4\'s tank mortar = Dead %1.';
$DeathMessageCTurretKill[$DamageType::TankMortar, 1] = '\c0%1 learns the happy explosion dance from %4\'s tank mortar.';
$DeathMessageCTurretKill[$DamageType::TankMortar, 2] = '\c0%4\'s tank mortar has a blast with %1.';

$DeathMessageCTurretKill[$DamageType::ShrikeBlaster, 0] = '\c0%1 dines on a Shrike blaster sandwich, courtesy of %4.';
$DeathMessageCTurretKill[$DamageType::ShrikeBlaster, 1] = '\c0The blaster of %4\'s Shrike turns %1 into finely shredded meat.';
$DeathMessageCTurretKill[$DamageType::ShrikeBlaster, 2] = '\c0%1 gets drilled big-time by the blaster of %4\'s Shrike.';

$DeathMessageCTurretKill[$DamageType::MPBMissile, 0] = '\c0%1 intersects nicely with %4\'s MPB Missile.';
$DeathMessageCTurretKill[$DamageType::MPBMissile, 1] = '\c0%4\'s MPB Missile makes armored chowder out of %1.';
$DeathMessageCTurretKill[$DamageType::MPBMissile, 2] = '\c0%1 has a brief, explosive fling with %4\'s MPB Missile.';

$DeathMessageTurretSelfKillCount = 3;
$DeathMessageTurretSelfKill[0] = '\c0%1 somehow kills %2self with a turret.';
$DeathMessageTurretSelfKill[1] = '\c0%1 apparently didn\'t know the turret was loaded.';
$DeathMessageTurretSelfKill[2] = '\c0%1 helps his team by killing himself with a turret.';

$DeathMessageMeteorCount = 6;
$DeathMessageMeteor[0] = '\c0%1 was killed by a meteor!';
$DeathMessageMeteor[1] = '\c0%1 caught a meteor!';
$DeathMessageMeteor[2] = '\c0%1 gets a facefull of molten meteor.';
$DeathMessageMeteor[3] = '\c0%1 gets smeared by a red hot meteor.';
$DeathMessageMeteor[4] = '\c0%1 is left a smoking crater by a meteor.';
$DeathMessageMeteor[5] = '\c0%1 learns to seek shelter when there\'s hot rock falling from the sky.';

$DeathMessageCursingCount = 2;
$DeathMessageCursing[0] = '\c0%1 was killed for cursing.';
$DeathMessageCursing[1] = '\c0%1\'s mouth gets %2 killed again.';

$DeathMessageIdiocyCount = 2;
$DeathMessageIdiocy[0] = '\c0%1 was killed for being dumb.';
$DeathMessageIdiocy[1] = '\c0%1\'s own stupidity stops %2 cold in %3 tracks.';

$DeathMessageKillerFogCount = 4;
$DeathMessageKillerFog[0] = '\c0%1 got lost in the great beyond.';
$DeathMessageKillerFog[1] = '\c0%1 slipped and fell, never to be seen again.';
$DeathMessageKillerFog[2] = '\c0The fog of death engulfs %1.';
$DeathMessageKillerFog[3] = '\c0%1 got lost in the fog.';


$DeathMessage[$DamageType::SuperChaingun, 0] = '\c0%4 rips %1 up with the super chaingun.';
$DeathMessage[$DamageType::SuperChaingun, 1] = '\c0%4 happily chews %1 into pieces with %6 super chaingun.';
$DeathMessage[$DamageType::SuperChaingun, 2] = '\c0%4 administers a dose of Admin Lead to %1.';
$DeathMessage[$DamageType::SuperChaingun, 3] = '\c0%1 suffers a serious hosing from %4\'s super chaingun.';
$DeathMessage[$DamageType::SuperChaingun, 4] = '\c0%4 bestows the blessings of %6 super chaingun on %1.';

// TODO - create these
$DeathMessageSelfKill[$DamageType::SuperChaingun, 0] = '\c0%1 kills %2self with a super chaingun.';
$DeathMessageSelfKill[$DamageType::SuperChaingun, 1] = '\c0%1 catches the blast of %3 own super chaingun bullet.';
$DeathMessageSelfKill[$DamageType::SuperChaingun, 2] = '\c0%1 kills %2self with a super chaingun.';
$DeathMessageSelfKill[$DamageType::SuperChaingun, 3] = '\c0%1 catches the blast of %3 own super chaingun bullet.';
$DeathMessageSelfKill[$DamageType::SuperChaingun, 4] = '\c0%1 plays Russian roulette with %3 super chaingun.';

$DeathMessage[$DamageType::Zombie, 0] = '\c0%1 is chewed up by a zombie.';
$DeathMessage[$DamageType::Zombie, 1] = '\c0%1 now wishes of never entering the haunted town.';
$DeathMessage[$DamageType::Zombie, 2] = '\c0%1 is now one with the undead.';
$DeathMessage[$DamageType::Zombie, 3] = '\c0%1 now moans instead of talking.';
$DeathMessage[$DamageType::Zombie, 4] = '\c0%1 is sliced and diced by a zombie.';

$DeathMessage[$DamageType::ZAcid, 0] = '\c0%1 gets blasted by zombie acid.';
$DeathMessage[$DamageType::ZAcid, 1] = '\c0%1 is in horrible pain from the infectious acid.';
$DeathMessage[$DamageType::ZAcid, 2] = '\c0%1 will be a zombie soon, thanks to that acid.';
$DeathMessage[$DamageType::ZAcid, 3] = '\c0%1 is blasted by a lord zombie.';
$DeathMessage[$DamageType::ZAcid, 4] = '\c0%1 burns in pain from zombie acid.';

$DeathMessage[$DamageType::Fire, 0] = '\c0%1 gets %2self burnt again.';
$DeathMessage[$DamageType::Fire, 1] = '\c0%1 burns down to cinders.';
$DeathMessage[$DamageType::Fire, 2] = '\c0%1 steps into the fire, and wonders why it hurts.';
$DeathMessage[$DamageType::Fire, 3] = '\c0%1 is to the 3rd degree.';
$DeathMessage[$DamageType::Fire, 4] = '\c0%1 is crisped by flames.';

$DeathMessage[$DamageType::ZombieL, 0] = '\c0%1 is crushed by a Zombie Lord.';
$DeathMessage[$DamageType::ZombieL, 1] = '\c0A Zombie Lord Punts %1, and It\'s GOOD!!!';
$DeathMessage[$DamageType::ZombieL, 2] = '\c0%1 is punched by a zombie lord.';
$DeathMessage[$DamageType::ZombieL, 3] = '\c0%1 is lifted up, then crushed into the ground.';
$DeathMessage[$DamageType::ZombieL, 4] = '\c0That zombie lord made short work of %1.';

$DeathMessage[$DamageType::Pistol, 0] = '\c0%4 nails %1 with a colt pistol.';
$DeathMessage[$DamageType::Pistol, 1] = '\c0%1 is schooled by %4\'s Pistol Skillz.';
$DeathMessage[$DamageType::Pistol, 2] = '\c0%4 shoots at %1 with his puny pistol, AND WINS!!!';
$DeathMessage[$DamageType::Pistol, 3] = '\c0%4 pops a few .77mm rounds into %1.';
$DeathMessage[$DamageType::Pistol, 4] = '\c0%1 is fatally embarassed by %4\'s Pistol.';

$DeathMessage[$DamageType::Melee, 0] = '\c0%4 swings at %1 with %6 Gun Blade.';
$DeathMessage[$DamageType::Melee, 1] = '\c0%1 is beat down by %4.';
$DeathMessage[$DamageType::Melee, 2] = '\c0%4 unleashes fatal blade swings upon %1';
$DeathMessage[$DamageType::Melee, 3] = '\c0%4 makes %1 bleed from blade swings.';
$DeathMessage[$DamageType::Melee, 4] = '\c0%1 is all sliced up by %4.';

$DeathMessage[$DamageType::S3, 0] = '\c0%4 sees %1, and hits %2 with %6 S3.';
$DeathMessage[$DamageType::S3, 1] = '\c0%1 is nailed by %4\'s S3 Rifle.';
$DeathMessage[$DamageType::S3, 2] = '\c0%4 whips out the S3 and shoots down %1';
$DeathMessage[$DamageType::S3, 3] = '\c0%4 rains S3 shots upon %1.';
$DeathMessage[$DamageType::S3, 4] = '\c0%1 is shot down %4\'s S3 Rifle.';

$DeathMessage[$DamageType::W1700, 0] = '\c0%4 uses %6 Pump Action Shotgun on %1.';
$DeathMessage[$DamageType::W1700, 1] = '\c0%1 gets blasted by %4\'s W1700 Shotgun.';
$DeathMessage[$DamageType::W1700, 2] = '\c0%4 buckshots %1 with %6 W1700 Shotgun';
$DeathMessage[$DamageType::W1700, 3] = '\c0%4 unleases a W1700 shotgun blast upon %1.';
$DeathMessage[$DamageType::W1700, 4] = '\c0%1 takes %4\'s W1700 Shotgun blast, and dies.';

$DeathMessage[$DamageType::G41, 0] = '\c0%4 Unloads Semi-Automatic Rifle Fire upon %1.';
$DeathMessage[$DamageType::G41, 1] = '\c0%1 gets nailed by %4\'s G41 Rifle.';
$DeathMessage[$DamageType::G41, 2] = '\c0%4 whips out %6 G41 and shoots down %1';
$DeathMessage[$DamageType::G41, 3] = '\c0%4 shoots %1, then again, and again, with %6 G41.';
$DeathMessage[$DamageType::G41, 4] = '\c0%1 is shot down, Rapidly, by %4\'s G41 Rifle.';

$DeathMessage[$DamageType::R700, 0] = '\c0%4 Nails %1 from the other side of the map.';
$DeathMessage[$DamageType::R700, 1] = '\c0%1\'s arm is on the ground from %4\'s R700.';
$DeathMessage[$DamageType::R700, 2] = '\c0%4 sees %1, smiles, and then blasts a R700 Shot at %2';
$DeathMessage[$DamageType::R700, 3] = '\c0%4 blasts off %1\'s head with %6 R700.';
$DeathMessage[$DamageType::R700, 4] = '\c0%1 is dying on the ground from %4\'s R700 Shot.';

$DeathMessage[$DamageType::MP26, 0] = '\c0%4 sees %1, and unloads MP26 rounds on %2.';
$DeathMessage[$DamageType::MP26, 1] = '\c0%1 is gunned down by %4\'s MP26 SMG.';
$DeathMessage[$DamageType::MP26, 2] = '\c0%4 grabs a MP26, and unloads bullets upon %1';
$DeathMessage[$DamageType::MP26, 3] = '\c0%4 rains MP26 SMG bullets upon %1.';
$DeathMessage[$DamageType::MP26, 4] = '\c0%4 bakes up MP26 rounds of death for %1.';

$DeathMessage[$DamageType::Pg700, 0] = '\c0%4 sees %1 in %6 scope, then kills %1 with %6 Pg700.';
$DeathMessage[$DamageType::Pg700, 1] = '\c0%1 is gunned down by %4\'s Pg700.';
$DeathMessage[$DamageType::Pg700, 2] = '\c0%4 whips out %6 Pg700 and shoots down %1';
$DeathMessage[$DamageType::Pg700, 3] = '\c0%4 makes it rain Pg700 bullets, just for %1.';
$DeathMessage[$DamageType::Pg700, 4] = '\c0%1 is rapidly gunned down from %4\'s Pg700.';

$DeathMessage[$DamageType::FellOff, 0] = '\c0%1 began %3 high flying sky diving career a little too close to the ground.';//
$DeathMessage[$DamageType::FellOff, 1] = '\c0%1 must have thougt staying close to the edge was a great idea.';//
$DeathMessage[$DamageType::FellOff, 2] = '\c0%1 now realizes that gravity is the law, and everyone must obey it.';//
$DeathMessage[$DamageType::FellOff, 3] = '\c0%1 couldn\'t handle the gravity of the situation.';//
$DeathMessage[$DamageType::FellOff, 4] = '\c0%1 takes a nice long trip down the edge.';//

$DeathMessage[$DamageType::M1, 0] = '\c0%4 Pings %1\'s head off with an M1 Sniper.';
$DeathMessage[$DamageType::M1, 1] = '\c0%4 ensured %1\'s death was slow and painful with %6 M1 Sniper.';
$DeathMessage[$DamageType::M1, 2] = '\c0%4 sees %1, smiles, and then blasts %3 head off with an M1';
$DeathMessage[$DamageType::M1, 3] = '\c0%4 enjoys killing %1 with an M1 Sniper.';
$DeathMessage[$DamageType::M1, 4] = '\c0%1 suffers extreme head trauma from %4\'s M1 Sniper.';

$DeathMessage[$DamageType::RP432, 0] = '\c0%4 unleashes the full power of %6 RP432 upon %1.';
$DeathMessage[$DamageType::RP432, 1] = '\c0%4 blows many tiny holes into %1 from %6 RP432.';
$DeathMessage[$DamageType::RP432, 2] = '\c0%4 unloads RP432 Bullets into %1\'s life';
$DeathMessage[$DamageType::RP432, 3] = '\c0%4 blows holes into %1, tiny RP432 holes.';
$DeathMessage[$DamageType::RP432, 4] = '\c0%1 died after taking about 30 RP432 Bullets from %4.';

$DeathMessage[$DamageType::RPG, 0] = '\c0%4 RPG\'d %1 out of existance.';
$DeathMessage[$DamageType::RPG, 1] = '\c0%4 sends an RPG of joy into %1\'s life.';
$DeathMessage[$DamageType::RPG, 2] = '\c0%4 blasted %1 with %6 RPG';
$DeathMessage[$DamageType::RPG, 3] = '\c0%4 blows %1 into epic explosive proportions.';
$DeathMessage[$DamageType::RPG, 4] = '\c0%1 died after taking a RPG to the face from %4.';

$DeathMessage[$DamageType::LaserRifle, 0] = '\c0%4 beamed down %1 with an RSA Laser Rifle.';
$DeathMessage[$DamageType::LaserRifle, 1] = '\c0%4 blasts ultimate laser beams into %1.';
$DeathMessage[$DamageType::LaserRifle, 2] = '\c0%4 utilizes advanced RSA Laser Technology on %1';
$DeathMessage[$DamageType::LaserRifle, 3] = '\c0%4 burns holes into %1 from %6 RSA laser rifle.';
$DeathMessage[$DamageType::LaserRifle, 4] = '\c0%4 gives %1 some laser therapy, too bad it was lethal.';

$DeathMessage[$DamageType::Burn, 0] = '\c0%1 screams fire, nobody comes to aid %2.';
$DeathMessage[$DamageType::Burn, 1] = '\c0%1 is to the �rd Degree.';
$DeathMessage[$DamageType::Burn, 2] = '\c0%1 walks into the flames and never comes back';
$DeathMessage[$DamageType::Burn, 3] = '\c0%1 becomes engulfed in fire, and dies.';
$DeathMessage[$DamageType::Burn, 4] = '\c0%1 experiences spontanious combustion.';

$DeathMessage[$DamageType::Fire, 0] = '\c0%4 burns %1 with a flamethrower.';
$DeathMessage[$DamageType::Fire, 1] = '\c0%4 burns %1, then flips the switch again to ensure.';
$DeathMessage[$DamageType::Fire, 2] = '\c0%1 walks into %4\'s stream of flames';
$DeathMessage[$DamageType::Fire, 3] = '\c0%4 defines the words third degree for %1.';
$DeathMessage[$DamageType::Fire, 4] = '\c0%1 experiences spontanious combustion from %4.';

$DeathMessage[$DamageType::SA2400, 0] = '\c0%4 unloads rapid shotgun shells upon %1.';
$DeathMessage[$DamageType::SA2400, 1] = '\c0%1 is buckshotted down rapidly by %4.';
$DeathMessage[$DamageType::SA2400, 2] = '\c0%1 is nailed by %4\'s rapid shotgun fire.';
$DeathMessage[$DamageType::SA2400, 3] = '\c0%4 unloads a full clip of buckshot upon %1.';
$DeathMessage[$DamageType::SA2400, 4] = '\c0%1 has many tiny buckshot holes from %4 in %2.';

$DeathMessage[$DamageType::deserteagle, 0] = '\c0%4 blows %1 away with a desert eagle pistol.';
$DeathMessage[$DamageType::deserteagle, 1] = '\c0%4 blasts a pistol shot that %1 will never forger.';
$DeathMessage[$DamageType::deserteagle, 2] = '\c0%4 uses the most powerful pistol on %1';
$DeathMessage[$DamageType::deserteagle, 3] = '\c0%4 shows %1 some desert eagle skillz.';
$DeathMessage[$DamageType::deserteagle, 4] = '\c0%4 gives %1 some D.Eagle bullets, they are murderous.';

$DeathMessage[$DamageType::FissionBomb, 0] = '\c0%4 kills %1 with a Fission Bomb..';
$DeathMessage[$DamageType::FissionBomb, 1] = '\c0%4 kills %1 with a Fission Bomb..';
$DeathMessage[$DamageType::FissionBomb, 2] = '\c0%4 kills %1 with a Fission Bomb..';
$DeathMessage[$DamageType::FissionBomb, 3] = '\c0%4 kills %1 with a Fission Bomb..';
$DeathMessage[$DamageType::FissionBomb, 4] = '\c0%4 kills %1 with a Fission Bomb..';

