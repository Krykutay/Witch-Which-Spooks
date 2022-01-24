# Witch-Which-Spooks
A spooky witch game on Unity

To play, click the link below!

Gameplay video on [YouTube: Skeleton Farming Simulator](https://youtu.be/5Be4gMgr_7E) <br/>

Click the link to play! https://simmer.io/@Krykutay/witch-which-spooks  <br/>

<br/>
<img src="https://user-images.githubusercontent.com/44427408/150218476-7c13002d-1342-4f59-8bb1-f75452e0c8b3.png" width="720" height="405"> <br/>

2D flappy bird-ish game where the main character, the witch, throws fireballs and kills monsters which are on hunt for the witch. The powerups along the way make the game super fun! Enjoy the Halloween theme o/

# Game Overview
<br/>
<img src="https://user-images.githubusercontent.com/44427408/141692654-8390d441-48e1-456c-abf3-83f14b3df1df.png" width="720" height="405"> <br/>

## Game Genre
Arcade/Action Game

## Game Summary
It's basically a game game where the main character, the witch, tries not to get hit by enemy fireballs & ghostly hand obstacles, and in response she can throw her own fireballs to her enemies. 
The game has two scenes, first one being the main menu with its play button, settings (Sound, Graphics, Controls), how to play tab and Exit. The second one is where the actual game goes on. In this scene, the game relatively gets harder the more time passes (everything moves faster and faster). On top of that, there are randomly spawned coins and powerups which add the game more juice. 

## Thing that the game include

+ A main character that can Jump/Fly and throws quite alright fireballs.
+ Ghostly obstacles from another world and enemies on the path that can fight back the witch.
+ Powerup system that adds variety to the game.
+ Responsive UI system working on all resolutions.
+ A rich detialed Settings menu -> Sounds (master, effects, ambiance,), Graphics (resolution, quality, vsync, fullscreen), Controls (changable key binds).
+ Super nice sound effects and ambiance music!
+ Some alright easter eggs.
+ An endless, infinite and moving background.

# Technical Details

- The game is run via managers, most of which are simple singletons.<br/><br/>
- GameController -> This one is responsible for the current and increasing difficulty of the game as well as spawning the monsters/obstacles relative to the difficulty. Additionally, also manages the play/pause state of the game.<br/><br/>
- GameSceneManager -> This manager adjusts the scenes accordingly, player can move between the scenes including the main menu scene.<br/><br/>
- SoundManager -> This manager simply adjusts all the sounds in the game. Instead of adding audio for a lot of objects, a simple manager handles it all.<br/><br/>
- ObjectPoolingManager -> Instead of destroying and re instantiating gameobjects, Objects pooling manager simply has a Generic Script and basically disables and re-enables objects (object pooling) without destroying them in order to avoid memory allocations which causes performance issues.<br/><br/>
- ScoreManager -> This manager is responsible for the stones and gems that are collected as well as keeping the highscore.<br/><br/>
- InputManager -> Since Unity's new input system is used, this manager handles the C# based event system for the inputs.<br/><br/>

- Playerpref Save system for all settings, scores, player inventory and current outfit/sword.

- Many performance and memory adjustments such as keeping the sprites in power of 2, using sprite atlas, efficient animations and coding.

## UI Elements
### Settings
Audio Settings offer the player a chance to alter Master, Effects, Music and Voice Volumes.<br/>
<img src="https://user-images.githubusercontent.com/44427408/150685194-e9c31e10-1101-4a5e-969d-50c70fca5eb9.jpg" width="390" height="250"> <br/> <br/>
Graphics Settings offer the player a chance to change Quality, Resolution, FullScreen(or not), V-Sync(or not). <br/>
<img src="https://user-images.githubusercontent.com/44427408/150685219-25eb842c-d655-4a41-b552-a0a2bc8c60aa.jpg" width="390" height="250"> <br/> <br/>
Controls Settings offer the player a chance to change any key binding. <br/>
<img src="https://user-images.githubusercontent.com/44427408/150685265-bacd5063-d7dd-4ee9-bdb6-77c46d453dd4.jpg" width="390" height="250"> <br/> <br/>

### Powerup Visual Effects & Duration Countdown
Each powerup pops up it's own visual effect. Health has its own particle-system animations, Offensive powerup vibrates an orange aura and the character glooms orange while Defensive powerup vibrates a blue auro and the character glooms blue. <br/>
These icons on top left indicates that the corresponding powerup is active and also tell its remaning duration. <br/>
<img src="https://user-images.githubusercontent.com/44427408/150685532-85cbc5b0-a237-4465-93d7-4e0aa00090bb.jpg" width="720" height="405"> <br/>

### Gameover
When the player dies, or tries to leave the scene, the gameover panel pops-up and turns the collected stones into gems as well as showing the kill/collect counts and highscore. <br/>
<img src="https://user-images.githubusercontent.com/44427408/141692662-12340863-04e8-4596-91c4-304af5258d89.png" width="720" height="405"> <br/> 

## Quite sophisticated easter eggs
Loading Screen <br/>
<img src="https://user-images.githubusercontent.com/44427408/150685643-d2fb9171-c012-4b0c-9367-e83bb828445a.jpg" width="720" height="405"> <br/> <br/>

![unity logo](https://user-images.githubusercontent.com/44427408/141692619-6d3b3148-0e20-42f7-901d-5a878551c87d.png)
