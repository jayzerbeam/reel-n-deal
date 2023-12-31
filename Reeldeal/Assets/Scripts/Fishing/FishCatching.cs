using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class FishCatching : MonoBehaviour
{
    Animator _animator;
    GameObject _player;
    hud_gui_controller _inventoryController;
    PlayerWonAlert _playerWonAlert;
    FishingMessaging _messaging;
    GameObject hookedFishGO;
    Rigidbody hookedFishRB;
    Rigidbody _bobberRB;
    FishAI fishAIscript;
    FishMultiTag _fishMultiTag;

    bool _isFishHooked = false;
    bool _didFishEscape = false;
    bool _wasFishCaught = false;
    float countdownTimer = 10.0f;

    int _isFishingHash;

    int keyPressesRemaining = 5;
    string[] inputKeys = { "w", "a", "s", "d", "q", "r", "x", "z" };
    System.Random random = new System.Random();
    string randomInputKey = "";

    AudioSource _keypressFailure;
    AudioSource _keypressSuccess;
    AudioSource _caughtFishBell;
    AudioSource _escape;

    private BobberPrefabInitializer.AttractiveBobberInfo bobberMechanics;

    void Awake()
    {
        _isFishingHash = Animator.StringToHash("isFishing");
    }

    void Start()
    {
        _bobberRB = GetComponent<Rigidbody>();
        _player = GameObject.FindWithTag("Player");
        _animator = _player.GetComponent<Animator>();
        _messaging = _player.GetComponent<FishingMessaging>();
        _inventoryController = FindObjectOfType<hud_gui_controller>();
        _playerWonAlert = FindObjectOfType<PlayerWonAlert>();
        _keypressFailure = _player.GetComponents<AudioSource>()[0];
        _keypressSuccess = _player.GetComponents<AudioSource>()[1];
        _caughtFishBell = _player.GetComponents<AudioSource>()[3];
        _escape = _player.GetComponents<AudioSource>()[4];

        // https://stackoverflow.com/questions/14297853/how-to-get-random-values-from-array-in-c-sharp
        randomInputKey = inputKeys[random.Next(0, inputKeys.Length)];
    }

    void Update()
    {
        if (_isFishHooked)
        {
            // set bobber distance to zero to stop odd fish collisions
            BobberPrefabInitializer bobberInitializer = GetComponent<BobberPrefabInitializer>();
            for (int i = 0; i < bobberInitializer.attractiveBobberInfo.Count; i++)
            {
                bobberInitializer.attractiveBobberInfo[i].radius = 0f;
            }

            HandleCatch();
            HandleCountdown();
        }
        if (_didFishEscape)
        {
            _animator.SetBool(_isFishingHash, false);
            ReleaseFish();
        }
        else if (_wasFishCaught)
        {
            _animator.SetBool(_isFishingHash, false);
            _caughtFishBell.Play();
        }
    }

    public GameObject GetHookedFishGO()
    {
        return _isFishHooked ? hookedFishGO : null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // https://docs.unity3d.com/ScriptReference/Collider.OnCollisionEnter.html
        // https://forum.unity.com/threads/add-an-object-as-a-child-of-another-gameobject-into-a-different-scene.1292907/
        if (!_isFishHooked)
        {
            if (collision.gameObject.CompareTag("Fish"))
            {
                hookedFishGO = collision.gameObject;
                hookedFishRB = hookedFishGO.GetComponent<Rigidbody>();
                _fishMultiTag = hookedFishGO.GetComponent<FishMultiTag>();
                fishAIscript = hookedFishGO.GetComponent<FishAI>();

                // 5 Second cooldown
                if (!fishAIscript.WasRecentlyCaught)
                {
                    _bobberRB.constraints = RigidbodyConstraints.FreezeAll;
                    collision.transform.SetParent(transform);
                    SetCountdownTimer();

                    if (hookedFishRB != null)
                    {
                        hookedFishRB.constraints = RigidbodyConstraints.FreezeAll;
                    }
                    fishAIscript.enabled = false; // turn off AI when caught
                    _isFishHooked = true;
                    _didFishEscape = false;
                    _wasFishCaught = false;
                }
            }
        }
    }

    public void ReleaseFish()
    {
        SetCountdownTimer(); // Will reset to 10f
        keyPressesRemaining = 5;
        fishAIscript.enabled = true;
        fishAIscript.aiState = FishAI.AIState.fleeState;
        fishAIscript.WasRecentlyCaught = true;
        hookedFishRB.constraints = RigidbodyConstraints.None;
        hookedFishGO.transform.SetParent(null);
        Destroy(GameObject.FindWithTag("Bobber"));
        _escape.Play();
    }

    void SetCountdownTimer()
    {
        if (_fishMultiTag.HasTag("Easy"))
        {
            countdownTimer = 10.0f;
        }
        else if (_fishMultiTag.HasTag("Medium"))
        {
            countdownTimer = 8.0f;
        }
        else if (_fishMultiTag.HasTag("Hard"))
        {
            countdownTimer = 6.0f;
        }
        else if (_fishMultiTag.HasTag("Boss"))
        {
            countdownTimer = 5.0f;
        }
        else
        {
            countdownTimer = 10f;
        }
    }

    void HandleCountdown()
    {
        countdownTimer -= Time.deltaTime;

        if (countdownTimer > 0.0f && !_didFishEscape && !_wasFishCaught)
        {
            _messaging.DisplayMessage(
                $"You hooked a fish!\n\nPress {randomInputKey}!\n\nTime left: {countdownTimer}",
                countdownTimer
            );
        }
        else if (countdownTimer <= 0.0f && !_didFishEscape && !_wasFishCaught)
        {
            _messaging.StopMessage();
            _messaging.DisplayMessage("The fish got away...\n\nBetter luck next time!");
            _didFishEscape = true;
            _wasFishCaught = false;
        }
    }

    void HandleCatch()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(randomInputKey))
            {
                string prevInputKey = randomInputKey;
                randomInputKey = inputKeys[random.Next(0, inputKeys.Length)];

                // Ensure the next input is not the same as the previous input
                while (prevInputKey == randomInputKey)
                {
                    randomInputKey = inputKeys[random.Next(0, inputKeys.Length)];
                }
                keyPressesRemaining -= 1;
                _keypressSuccess.Play();
            }
            else if (!Input.GetKeyDown(randomInputKey))
            {
                _keypressFailure.Play();
                _messaging.StopMessage();
                _messaging.DisplayMessage("The fish got away...\n\nBetter luck next time!");
                _didFishEscape = true;
                _wasFishCaught = false;
            }
        }

        if (keyPressesRemaining == 0)
        {
            _didFishEscape = false;
            _wasFishCaught = true;
            if (_fishMultiTag.HasTag("Boss"))
            {
                _messaging.StopMessage();
                _playerWonAlert.PlayerWins();
            }
            else
            {
                _messaging.StopMessage();
                _messaging.DisplayMessage("You caught a fish!");
                _inventoryController.AddItemToInv(GetFishTypeByTag(), 1);
            }
            Destroy(GameObject.FindWithTag("Bobber"));
        }
    }

    string GetFishTypeByTag()
    {
        string fishTypeTag = "";
        string[] fishTypes =
        {
            "lake_f_1",
            "lake_f_2",
            "lake_f_3",
            "lake_f_4",
            "ocean_f_1",
            "ocean_f_2",
            "ocean_f_3",
            "ocean_f_4",
            "ocean_shark",
            "river_f_1",
            "river_f_2",
            "river_f_3",
            "river_f_4"
        };

        foreach (string fishType in fishTypes)
        {
            if (_fishMultiTag.HasTag(fishType))
            {
                fishTypeTag = fishType;
            }
        }

        return fishTypeTag;
    }
}
