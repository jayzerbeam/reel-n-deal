Team: Quantum Quill
Team Members (Name, email, Canvas account)
- Cassandra Durkee, cdurkee6@gatech.edu, 
- Beto Icaza, laip3@gatech.edu
- Jason Long, jlong326@gatech.edu
- - Grady Sullivan, gsullivan36@gatech.edu, gsullivan36

Main Scene: mainGameScene

Controls:
movement controls - WASD
sprint - 
cast rod - 
reel in rod -

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
- 

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

- 

Sources:

- Fish models: https://assetstore.unity.com/packages/3d/characters/animals/fish/fish-polypack-202232
- 