# Witch-Which-Spooks
A spooky witch game on Unity

To play, click the link below!

Gameplay video on [YouTube: Witch Which Spooks](https://youtu.be/5Be4gMgr_7E) <br/>

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
Audio Settings offer the player a chance to alter Master, Effects and Music Volumes.<br/>
<img src="https://user-images.githubusercontent.com/44427408/150787361-1c3d24c5-e46f-4038-a42f-bce889e726da.jpg" width="390" height="250"> <br/> <br/>
Graphics Settings offer the player a chance to change Quality, Resolution, FullScreen(or not), V-Sync(or not). <br/>
<img src="https://user-images.githubusercontent.com/44427408/150788404-29c01028-fe09-4186-a1f6-88c8fbf94d3e.jpg" width="390" height="250"> <br/> <br/>
Controls Settings offer the player a chance to change any key binding. <br/>
<img src="https://user-images.githubusercontent.com/44427408/150787373-2dad1a9b-2ab9-4509-beb4-219ace976a86.jpg" width="390" height="250"> <br/> <br/>

### Powerup Visual Effects & Duration Countdown
Each powerup pops up it's own visual effect. Blue one sets sparkles around the witch fuels her mana to full immediately, and increases her mana regeneration for it's duration, green one moves witch into pellucid state and makes her translucent, in return she becomes immune to collisions. Red one simply vaporizes the entire hostile living. <br/>
<img src="https://user-images.githubusercontent.com/44427408/150788033-b95be2fe-4cc6-4262-b8eb-dbe4e945e237.jpg" width="720" height="405"> <br/>
<img src="https://user-images.githubusercontent.com/44427408/150788036-608ac095-e35b-4f17-9010-c1f6c05cbc12.jpg" width="720" height="405"> <br/>

These icons on bottom left indicates that the corresponding powerup is active and also tell its remaning duration. <br/>
<img src="https://user-images.githubusercontent.com/44427408/150788041-4ebc7195-bca4-41fc-b969-49d732299345.jpg" width="720" height="405"> <br/>

### Gameover
When the player dies, or tries to leave the scene, the gameover panel pops-up and turns the collected stones into gems as well as showing the kill/collect counts and highscore. <br/>
<img src="https://user-images.githubusercontent.com/44427408/141692662-12340863-04e8-4596-91c4-304af5258d89.png" width="720" height="405"> <br/> 

## Quite sophisticated easter eggs
Loading Screen <br/>
<img src="https://user-images.githubusercontent.com/44427408/150685643-d2fb9171-c012-4b0c-9367-e83bb828445a.jpg" width="720" height="405"> <br/> <br/>

![unity logo](https://user-images.githubusercontent.com/44427408/141692619-6d3b3148-0e20-42f7-901d-5a878551c87d.png)
