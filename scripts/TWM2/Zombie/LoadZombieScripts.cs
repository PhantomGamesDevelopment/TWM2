//Zombie Scripts Loading

//load zombie armors and AI
$search = "scripts/TWM2/Zombie/ZombieTypes/*.cs";
for($file = findFirstFile($search); $file !$= ""; $file = findNextFile($search)) {
   $type = fileBase($file); // get the name of the script
   exec("scripts/TWM2/Zombie/ZombieTypes/" @$type @ ".cs");
}

exec("scripts/TWM2/Zombie/ZombieCore.cs");
exec("scripts/TWM2/Zombie/PlayerZombieFunctions.cs");
exec("scripts/TWM2/Zombie/PlayerZombieAttacks.cs");
