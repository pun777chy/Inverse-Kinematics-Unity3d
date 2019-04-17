# Inverse-Kinematics-Unity3d
A simple Sample of Standing shooting commando game project using inverse Kinematic of Unit3d 5.5.2

The sample contains just Aiming by using Inverse Kinematics, it does not have moving controls.

I took Background images from Internet and rest of UI is Unity3d default button, sorry for my bad aesthetic sense.

It also Mobile touch controls for Mobile build

It has 3 scenes. 1) Main Menu 2) Infinite Enemies 3) Heli Attack.

Here are some mjor scripts used in the project

PlayerControl.CS : To control the player AiMing, Player animations, player State (which is Enum) and input controls.
healthSystem.CS :  To handle Health of both enemy and player. Its object is composed in both enemies and player.
EnemyGenerator.CS : This script is used in only Enemy Rushing Scene to generate infinite enemies as long as player is alive.
EnemeyAI.CS : It handles the Enemy animations, Aiming and shooting towards player.
LevelManager.CS : This script is handling the ingame UI panels of Game Complete and Game Over accordingly.

Plugins Used in the Projects
RifleAnimsetPro : The animations for player and enemies are taken from this plugin
Realistic Effects Pack : Effects of Shooting, Blast and muzzle flash are taken from this plugin.

You can adjust the threshold of aiming by adjusting the poistion of cube call "Load" in the scene.

I wrote the code as simple as possible.

The sample is also android Mobile Ready. Android APK is also added in master copy.

I am looking forward if some one has queries.
