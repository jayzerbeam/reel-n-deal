Team: Quantum Quill
Team Members (Name, email, Canvas account)
- Cassandra Durkee, cdurkee6@gatech.edu, 
- Beto Icaza, laip3@gatech.edu
- Jason Long, jlong326@gatech.edu, jlong326
- Grady Sullivan, gsullivan36@gatech.edu, gsullivan36

Main Scene: mainGameScene

Controls:
movement controls - WASD, Gamepad Left Stick
Jump - Gamepad Button South, Spacebar
sprint - Left Shift, Gamepad Left Shoulder
cast rod - Gamepad Right Trigger, Mouse Right Button, F Key
reel in rod - Gamepad Left Trigger, Mouse Left Button
Look - Gamepad Right Stick, Mouse Delta (X-axis scrolling)

Gameplay Information:
This game revolves fishing and trading in catches for trades in the market to geat up and catch the big goldfish boss.

The fish show off the AI requirements for this project. There are four states for each fish: idle, hungry, agressive, and fleeing. In idle,
fish swim between invisble waypoints to give the allusion of loitering around a small area. If a bobber is in the area, the fish will
transition to the "hungry" state, assuming the specified bobber attracts that type of fish. This simply makes the fish set its next waypoint
as the bobber, and its current speed also changes. The aggressive state only occurs for sharks and the boss goldfish. When these fish can
"see" the player, they set the player as their waypoint and charge towards them, killing the player if contact is made. The flee state is the
counterpart to the agressive state, as smaller fish will run away from players if the fish "sees" them. The only other time this state is used
is when a player fails to catch a fish. When this occurs, any fish, regardless of type, transitions to this flee state. This gives the player
a small penalty for failing to catch the fish.

Known Issues:
- occasional odd behavior when two fish collide
- fish animations to be implemented
- Some items in marketplace do not disappear when purchased 

Teammate Contributions

Grady
- Fish Prefabs / variants
- FishAI.cs -- controls fish behavior
- FishSpawning.cs -- determines how fish spawn
- FishDespawning.cs -- despawns fish under certain distance criteria
- FishMultiTag.cs -- framework to give fish multiple tags
- FishPrefabInitializer.cs -- intializes prefab variants
- Water placement and setup to work with spawning algorithm
- Bobber Prefabs / variants
- BobberMultiTag.cs -- framework to give bobbers multiple tags
- BobberPrefabInitializer.cs -- initializes bobber variants
- FishCatching -- implemented the framework of catching/releasing fish
- MainMenuScene
- MainMenuCamera.cs -- rotates camera around island for nice menu background
- GameStarter.cs -- starts game
- GameQuitter.cs -- quits game
- shark_start.wav and shark_loop.wav -- trimmed sound files with Audacity
- sharkAudioRandomizer.cs -- randomizes pitch and frequency of shark sounds based on player distance
- SharkAudioManager.cs -- responsible for updating sound source settings and playing sounds to prevent audio overlap

Luis Alberto
- Level design + game ideation
- Terrain generation
- Water planes and shading
	- waveDisplay, waveSingle
- Terrain texturing
- Vegetation design and painting
	- collidetrees
- Playtesting
- Original scene creation and groundwork
- Map Texturing
- Main menu integration into build and quit behaviour on escape while in game
	- goToMainMenu
- Getting player movement done for Jumping and physics
	- BasicControlScript, myBasicControlScript, myBasicControlScript_2
- Added Walking, Running, Turning for player, initial player model, initial camera and lighting for development work
	- myPlayerControlScript
- Getting player camera to focus on player
	- CameraBehavior
- Created an inventory system
	- playerInventory, player_controller2
- Keep track of the fishes caught
	- playerInventory
- Keep track of currency in the game
	- playerInventory
- Simple economy system implemented
- Icon displaying for user input prompts (instead of a tutorial player learns by playing) also sets up a rythm game like mechnaic (game-feel) for fish catching (ideated, playtested and prototyped idea for implementation)
	- iconPopUpController
- Added a player death mechanich, it removes between 45-55% of fish inventory to give player a cost for death but keep game in the relaxing slice of life genre. Game-feel should be relaxing and playful not hardcore
	- playerInventory
- Added a trading villager subsystem. Villagers have villages they congregate on and can trade with the players. Vilagers want different fish types and will greet the player with a set of different greetings.
	- villagerMovement
- Villager payouts are determined by fish type plus some market differences in rates. Currently all villagers can trade all fishes but later different villagers can trade fish from different locations and may give vetter prices based on familiarity.
	- villagerTrading
- Added currency and inventory GUI
	- playerInventory
- Added a struct to store details of fishes caught
	- playerInventory

Jason Long
- Player movement 
  - PlayerMove.cs
  - PlayerJump.cs
- Camera control
  - PlayerLook.cs
- Player Input controller
  - PlayerInput.cs (Input System package)
- Player animation / blendtree
  - PlayerAnimationController
- Player cast and reel mechanic (w/Grady's help)
  - PlayerCast.cs
  - PlayerReel.cs
- Fish rigging (in Blender) and animations (Unity)
  - Fish1Rigged / Fish1Swim
  - Fish2Rigged / Fish2Swim
  - Fish3Rigged / Fish3Swim
  - Fish4Rigged / Fish4Swim
  - SharkRigged / SharkSwim

Cassandra Durkee
 - Added marketplace items tables, TNT, boots, boats, fishing rods, villagers 
   - Assets/Prefabs/Marketplace/FishingRodUpgrade
   - Assets/Prefabs/Marketplace/FishingRodStandard
   - Assets/Prefabs/Marketplace/TNT
   - Assets/Prefabs/Marketplace/Boots
   - Assets/Prefabs/Marketplace/FishingBoat
   - Assets/Prefabs/Marketplace/WoodenBoat
   - Assets/Prefabs/Marketplace/FoldingTable1
   - Assets/Prefabs/Marketplace/FoldingTable2
   - Assets/Prefabs/Marketplace/FoldingTable3
   - Assets/Prefabs/Marketplace/FishPile
   - Assets/Prefabs/Marketplace/Villager_cassye_1
   - Assets/Prefabs/Marketplace/Villager_cassye_2
   - Assets/Prefabs/Marketplace/Villager_cassye_3
 - Added GUI to show which items have been purchased at marketplace by pressing "I" with scripts
   - Assets/Prefabs/Marketplace/UICanvas
   - Assets/Prefabs/Marketplace/InventoryGUI
   - Assets/Prefabs/Marketplace/Inventory
   - Assets/Scripts/Marketplace/Inventory.cs 
  -Added GUIs for each marketplace item, to show item description and option to purchase by pressing a button
   - Assets/Scripts/Marketplace/MarketplaceItems.cs
   - Assets/Scripts/Marketplace/Collectables.cs
 - Added/Modified script that has player die if they are in the water for three continuous seconds
   - Assets/Scripts/player/PlayerDeath.cs
 - Added script that renders the boss fish invisible until the marketplace item FishingRodUpgrade is purchased 
   - Assets/Scripts/Fish/BossFishSpawn.cs 
 - Add music to MainMenu scene 
   - (May have been updated to new music, and code in script removed) - was originally part of Assets/Scripts/Menu/goToMainMenu.cs
 - Add sound and text alert when boss fish spawns
   - Assets/Scripts/Fish/BossfishSpawn.cs
   - Assets/Sounds/Fish/bossfishboom.mp3
 - Add music that gets louder as player approaches boss fish
   - Assets/Fish/Scripts/BossFishAudio.cs
   - Assets/Sounds/Fish/seashantybossfish.mp3  ********** move mp3? 
 - Add functionality for player to increase speed 2x after purchasing boots 
   - added in Assets/Animations/Player/Scripts/PlayerMove.cs in HandleMove() 
 - Add script to have player have die and respawn animations 
  - Assets/Scripts/Player/playerDeath.cs 
 - Add Marketplace ambient noise that gets louder as player gets closer to marketplace
  - Assets/Scripts/Marketplace/MarketplaceAudio.cs 
  - Assets/Scripts/Marketplace/marketplaceAudio.mp3 ********** move mp3?
 - Add villager greetings 
  - Assets/Scripts/Marketplace/villager1greeting.mp3    ********** move mp3?
  - Assets/Scripts/Marketplace/villager2greeting.mp3    ********** move mp3?
  - Assets/Scripts/Marketplace/villager3greeting.mp3    ********** move mp3?
  - Assets/Scripts/Marketplace/Villager1Approach.cs
  - Assets/Scripts/Marketplace/Villager2Approach.cs
  - Assets/Scripts/Marketplace/Villager3Approach.cs
 - Add instructions for game 
  - Assets/Scripts/Instructions/InstructionsOnPlay.cs
  - Assets/Scripts/Instructions/InstructionsPanel.cs
 - 
   

Sources:

Game Menu Music: Chunky Monkey - https://assetstore.unity.com/packages/audio/music/free-music-tracks-for-games-156413
Shading: https://www.youtube.com/watch?v=qH4XQaZhihw, https://www.youtube.com/watch?v=78WCzTVmc28, https://www.youtube.com/watch?v=cq2CoHVDxSQ
Textures: https://assetstore.unity.com/packages/2d/textures-materials/sky/fantasy-skybox-free-18353
Scripting: https://stackoverflow.com/, https://www.youtube.com/watch?v=UCwwn2q4Vys, https://docs.unity3d.com/
Marketplace Items: https://assetstore.unity.com/packages/3d/vehicles/sea/boats-polypack-189866
Marketplace Tables: https://assetstore.unity.com/packages/3d/props/furniture/folding-table-and-chair-pbr-111726
Marketplace Boots: https://assetstore.unity.com/packages/3d/props/clothing/female-ankle-boots-photoscanned-159578 
Boss Fish Audio: https://www.free-stock-music.com/alexander-nakarada-the-worlds-most-epic-sea-shanty.html
Boss Fish Spawn Music: https://pixabay.com/sound-effects/search/boom/
Marketplace Vendor Greetings: https://pixabay.com/sound-effects/search/hello/
Trading Sound Effects: https://mixkit.co/free-sound-effects/
Ambiant Marketplace Items: https://pixabay.com/sound-effects/search/fish-market/
Fish models: https://assetstore.unity.com/packages/3d/characters/animals/fish/fish-polypack-202232
Player models: https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/stylized-npc-peasant-nolant-252440
Player animations: https://mixamo.com/ (Links to individual animations are not available)
Shark audio: https://freesound.org/people/kradziej/sounds/511281/		CC0 1.0 License: https://creativecommons.org/publicdomain/zero/1.0/
Reeling audio (rod_reel.wav): https://freesound.org/people/mike%20campbell/sounds/34968/	Sampling Plus 1.0 License: https://creativecommons.org/licenses/sampling+/1.0/
Casting audio (rod_cast.wav): https://freesound.org/people/TyroneW/sounds/326381/		Attribution-NonCommercial 3.0 Licesnse: https://creativecommons.org/licenses/by-nc/3.0/
Fishing capture sound audio (catch_success.wav): https://freesound.org/people/LaurenPonder/sounds/639432/	CC0 1.0 License: https://creativecommons.org/publicdomain/zero/1.0/
Fishing escape sound audio (escape.wav): https://freesound.org/people/caitlynbananas/sounds/563592/		CC0 1.0 License: https://creativecommons.org/publicdomain/zero/1.0/	
Fishing Lodges: https://assetstore.unity.com/packages/3d/environments/low-poly-fishing-lodges-4555
Village Well: https://assetstore.unity.com/packages/3d/environments/old-water-well-138903
Crates:https://assetstore.unity.com/packages/3d/props/furniture/cartoon-wooden-box-242926
Food Sprite: https://assetstore.unity.com/packages/2d/environments/free-pixel-food-113523 License: https://unity.com/legal/as-terms
Scared Emoji Sprite: https://assetstore.unity.com/packages/2d/gui/icons/free-emojis-pixel-art-231243 License: https://unity.com/legal/as-terms
