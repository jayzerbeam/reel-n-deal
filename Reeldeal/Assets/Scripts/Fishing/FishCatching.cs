using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class FishCatching : MonoBehaviour
{
    GameObject _player;
    hud_gui_controller _inventoryController;
    FishingMessaging _messaging;
    GameObject hookedFishGO;
    Rigidbody hookedFishRB;
    Rigidbody _rb;
    FishAI fishAIscript;
    FishMultiTag _fishMultiTag;

    bool _isFishHooked = false;
    bool _didFishEscape = false;
    bool _wasFishCaught = false;
    float countdownTimer = 10.0f;

    int keyPressesRemaining = 5;
    string[] inputKeys = { "w", "a", "s", "d", "q", "e" };
    System.Random random = new System.Random();
    string randomInputKey = "";

    AudioSource _keypressFailure;
    AudioSource _keypressSuccess;
    AudioSource _caughtFishBell;
    AudioSource _escape;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GameObject.FindWithTag("Player");
        _inventoryController = new hud_gui_controller();
        _messaging = _player.GetComponent<FishingMessaging>();
        _inventoryController = FindObjectOfType<hud_gui_controller>();
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
            HandleCatch();
            HandleCountdown();
        }
        if (_didFishEscape)
        {
            ReleaseFish();
        }
        else if (_wasFishCaught)
        {
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
                    _rb.constraints = RigidbodyConstraints.FreezeAll;
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

    private void ReleaseFish()
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
            countdownTimer = 20.0f;
        }
        else if (_fishMultiTag.HasTag("Medium"))
        {
            countdownTimer = 15.0f;
        }
        else if (_fishMultiTag.HasTag("Hard"))
        {
            countdownTimer = 10.0f;
        }
        else if (_fishMultiTag.HasTag("Boss"))
        {
            countdownTimer = 7.0f;
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
            // TODO add fish to inventory
            _didFishEscape = false;
            _wasFishCaught = true;
            _messaging.StopMessage();
            _messaging.DisplayMessage("You caught a fish!");
            Destroy(GameObject.FindWithTag("Bobber"));
        }
    }
}
