using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class FishCatching : MonoBehaviour
{
    GameObject _player;
    playerInventory _playerInventory;
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

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GameObject.FindWithTag("Player");
        _playerInventory = _player.GetComponent<playerInventory>();
        _messaging = _player.GetComponent<FishingMessaging>();

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
    }

    void SetCountdownTimer()
    {
        if (_fishMultiTag.HasTag("Easy"))
        {
            countdownTimer = 10.0f;
        }
        else if (_fishMultiTag.HasTag("Medium"))
        {
            countdownTimer = 7.5f;
        }
        else if (_fishMultiTag.HasTag("Hard"))
        {
            countdownTimer = 5.0f;
        }
        else if (_fishMultiTag.HasTag("Boss"))
        {
            countdownTimer = 3.5f;
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
                $"You hooked a fish!\n\nPress {randomInputKey.ToUpper()}!\n\nKeypresses remaining: {keyPressesRemaining}\n\nTime left: {countdownTimer}",
                countdownTimer
            );
        }
        else if (countdownTimer <= 0.0f && !_didFishEscape && !_wasFishCaught)
        {
            _messaging.StopMessage();
            _messaging.DisplayMessage("Oh no! The fish got away...\n\nBetter luck next time!");
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
            }
            else if (!Input.GetKeyDown(randomInputKey))
            {
                _messaging.StopMessage();
                _messaging.DisplayMessage("Oh no! The fish got away...\n\nBetter luck next time!");
                _didFishEscape = true;
                _wasFishCaught = false;
            }
        }

        if (keyPressesRemaining == 0)
        {
            _didFishEscape = false;
            _wasFishCaught = true;
            _messaging.StopMessage();
            _messaging.DisplayMessage("CONGRATS!\n\nYou caught a fish!");
            _playerInventory.AddFishedFish("Fish Type, Other");
            Destroy(GameObject.FindWithTag("Bobber"));
        }
    }
}
