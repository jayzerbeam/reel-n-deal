Team: Quantum Quill
Team Members (Name, email, Canvas account)
- Luis Alberto Icaza, laip3@gatech.edu
- Grady Sullivan, gsullivan36@gatech.edu, gsullivan36
- Jason Long, jlong326@gatech.edu, jlong326
- Cassandra Durkee, cdurkee6@gatech.edu, 

Main Scene: mainGameScene

Controls:
Move - WASD, Gamepad Left Stick
Jump - Gamepad Button South, Spacebar
Sprint - Left Shift, Gamepad Left Shoulder
Cast Rod - Gamepad Right Trigger, Mouse Right Button, F Key
Look - Gamepad Right Stick, Mouse Delta (X-axis scrolling)
Cancel Cast Rod - C 
Change to Bobber View - V
Inventory - E
Trade - Q

Gameplay Information:
This game revolves fishing and trading in catches for trades in the market to geat up and catch the big goldfish boss.

The fish show off the AI requirements for this project. There are four states for each fish: idle, hungry, agressive, and fleeing. In idle,
fish swim between invisble waypoints to give the allusion of loitering around a small area. If a bobber is in the area, the fish will
transition to the "hungry" state, assuming the specified bobber attracts that type of fish. This simply makes the fish set its next waypoint
as the bobber, and its current speed also changes. The aggressive state only occurs for sharks and the boss goldfish. When these fish can
"see" the player, they set the player as their waypoint and charge towards them, killing the player if contact is made. The flee state is the
counterpart to the agressive state, as smaller fish will run away from players if the fish "sees" them. The only other time this state is used
is when a player fails to catch a fish. When this occurs, any fish, regardless of type, transitions to this flee state. This gives the player
a small penalty for failing to catch the fish. The flee and hungry states have associated 2D icons that appear when these conditions occur to
give a visual cue to the player. 

Known Issues:
- fish animations not implemented (this feature has been canceled)
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
- FishCatching.cs -- implemented the framework of catching/releasing fish
- MainMenuScene
- MainMenuCamera.cs -- rotates camera around island for nice menu background
- GameStarter.cs -- starts game
- GameQuitter.cs -- quits game
- shark_start.wav and shark_loop.wav -- trimmed sound files with Audacity
- sharkAudioRandomizer.cs -- randomizes pitch and frequency of shark sounds based on player distance
- SharkAudioManager.cs -- responsible for updating sound source settings and playing sounds to prevent audio overlap
- Casting, reeling, and sound indications for fishing
- Village Aesthetics -- houses, wells, crates, etc prefab creation from free assets and village layouts
- PlayerFish.cs -- added bobber switching mechanic
- Design for Instructions Layout
- InstructionsNav.cs -- Navigation for the instructions menu

Luis Alberto
- Level design + game ideation
	- Worked extensively on implementing TA feedback from alpha as well as coordinating team meetings, helping teammates with code issues, questions, bugs...
- Terrain generation
	- added vegetation, lighting villagers, colliders, boats, cameras, structures...
- Water planes and shading
	- waveDisplay, waveSingle
- Terrain texturing
- Vegetation design and painting
	- collidetrees
- Playtesting
- Original scene creation and groundwork
- Map Texturing
- Main menu integration into build and quit behaviour on escape while in game. Did ~90% of the UI and inventory item system in the game
	- goToMainMenu
	- hud_gui_controller
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
	- hud_gui_controller
- Keep track of currency in the game
	- hud_gui_controller
- Simple economy system implemented
- Icon displaying for user input prompts (instead of a tutorial player learns by playing) also sets up a rythm game like mechnaic (game-feel) for fish catching (ideated, playtested and prototyped idea for implementation)
	- iconPopUpController
- Bug fixing and code auditing
	- spent around ~90hrs fixing bugs and code submitted by the dev team to have everythign work together
- Added a player death mechanich, it removes between 45-55% of fish inventory to give player a cost for death but keep game in the relaxing slice of life genre. Game-feel should be relaxing and playful not hardcore
	- playerInventory
	- hud_gui_controller
- Added a trading villager subsystem. Villagers have villages they congregate on and can trade with the players. Vilagers want different fish types and will greet the player with a set of different greetings.
	- villagerMovement
	- Collectables
- Villager payouts are determined by fish type plus some market differences in rates. Currently all villagers can trade all fishes but later different villagers can trade fish from different locations and may give vetter prices based on familiarity.
	- villagerTrading
	- villagerMovement
- Added currency and inventory GUI
	- playerInventory
	- marketplace_buying
	- player_based_trading
	- villagerTrading
	- villager_Trades
- Added a struct to store details of fishes caught
	- hud_gui_controller
	- playerInventory
- Also worked extensively on debugging optimization of the game
	- random_fish_found
	- multiple other scripts

Jason Long
- Player movement 
  - PlayerMove.cs
  - PlayerJump.cs
- Camera control
  - PlayerLook.cs
- Player Input controller
  - PlayerInput.cs
- Player animation / blendtree
  - PlayerAnimationController
- Player cast mechanic
  - PlayerFish.cs
- Fish catching mechanic (w/Grady)
  - FishCatching.cs
- Player death / respawn mechanic (w/Cassye)
  - PlayerRespawn.cs
- Player sound effects (assets listed below)
- Bobber behavior and audio (w/Grady, his contributions are in other Bobber scripts)
  - BobberBehavior.cs
- Background ocean sounds (listed under assets below)
- Background piano music (listed under assets below)
  - AmbianceBackground.cs

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
   - Assets/Sounds/Fish/seashantybossfish.mp3 
 - Add functionality for player to increase speed 2x after purchasing boots 
   - added in Assets/Animations/Player/Scripts/PlayerMove.cs in HandleMove() 
 - Add script to have player have die and respawn animations 
  - Assets/Scripts/Player/playerDeath.cs 
 - Add Marketplace ambient noise that gets louder as player gets closer to marketplace
  - Assets/Scripts/Marketplace/MarketplaceAudio.cs 
  - Assets/Scripts/Marketplace/marketplaceAudio.mp3 
 - Add villager greetings 
  - Assets/Scripts/Marketplace/Vendor1Audio.mp3    
  - Assets/Scripts/Marketplace/Vendor2Audio.mp3    
  - Assets/Scripts/Marketplace/Vendor3Audio.mp3   
 - Add instructions for game 
  - Assets/Scripts/Instructions/InstructionsOnPlay.cs
  - Assets/Scripts/Instructions/InstructionsPanel.cs
 - Add respawning script where the player dies/respawns after 5 seconds underwater or being too close to a shark, with a punitive
   effect of losing half their coins if they die. 
  - PlayerRespawn.cs
  - Main UI Canvas:
    - RespawnCoinLoss 
  - Let the player know they won when they catch the boss fish
   - PlayerWonAlert.cs
   - Main UI Canvas:
    - WonGame UI
    - ConfettiCelebration
  - Add stairway leading to the rod that player has to jump to access, to satisfy movement requirements.  Player gets a clue about 
    the staircase in the main marketplace by a vendor. 
    - StairwayToHeavenAudio.cs
    - Main UI Canvas:
     - Clue Stairs 
    - Towns and Marketplaces:
      - Villager 1, fish piles
    - Audio Manager 
      - Stairway Audio 
    - StairwayToHeaven
      - Stairs, Stairs1, Stairs2, Plank, Plank1, Step, Step1, Step2, StairwayAudio, FishingRodUpgrade, FoldingTable2
    
   

Sources:

Game Menu Music: Chunky Monkey - https://assetstore.unity.com/packages/audio/music/free-music-tracks-for-games-156413	Licesnse: https://unity.com/legal/as-terms
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
Ambiant Marketplace Sounds: https://pixabay.com/sound-effects/search/fish-market/
Stairway Music: https://pixabay.com/music/beautiful-plays-reflected-light-147979/
Staircase assets: https://assetstore.unity.com/packages/3d/props/exterior/modular-wooden-bridge-tiles-29501
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
Player & bobber sounds: 
  - https://freesound.org/people/pan14/sounds/263133/
  - https://freesound.org/people/MLaudio/sounds/511484/
  - https://freesound.org/people/TitanKaempfer/sounds/689902/
  - https://freesound.org/people/tran5ient/sounds/190119/
  - https://freesound.org/people/alegemaate/sounds/364700/
  - https://freesound.org/people/Reitanna/sounds/344004/
Background ocean sounds: 
  - https://freesound.org/people/davidgtr1/sounds/581489/
Background happy piano sounds: 
  - https://freesound.org/people/Migfus20/sounds/560446/
