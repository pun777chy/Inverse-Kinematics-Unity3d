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

Plugins Used in the Project

RifleAnimsetPro : The animations for player and enemies are taken from this plugin.

Realistic Effects Pack : Effects of Shooting, Blast and muzzle flash are taken from this plugin.

You can adjust the threshold of aiming by adjusting the poistion of cube call "Load" in the scene.

I wrote the code as simple as possible.

The sample is also android Mobile Ready. Android APK is also added in master copy.

I am looking forward if some one has queries.

![48388167_764491510586862_7247939130850541568_n](https://github.com/pun777chy/Inverse-Kinematics-Unity3d/assets/6859320/168e13ce-86a6-4d22-b541-aac65d3e18ea)
![48387939_360497021182964_421065919251349504_n](https://github.com/pun777chy/Inverse-Kinematics-Unity3d/assets/6859320/88314ad6-f7e5-44da-bbd9-6516d748732b)
![48366141_2363418663688127_2827516398480130048_n](https://github.com/pun777chy/Inverse-Kinematics-Unity3d/assets/6859320/60ff7b42-b175-4ffd-b63f-e1add80532cc)
![48397881_1159390594227144_7968135696768565248_n](https://github.com/pun777chy/Inverse-Kinematics-Unity3d/assets/6859320/6e14abf2-005e-467a-b009-84a87787680f)

