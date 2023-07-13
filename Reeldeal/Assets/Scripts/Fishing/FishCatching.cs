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
    bool isFishHooked = false;

    // HandleCatch Variables
    int keyPressesRemaining = 5;
    string[] inputKeys = { "w", "a", "s", "d", "q", "e" };
    System.Random random = new System.Random();
    string randomInputKey = "";

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GameObject.FindWithTag("Player");
        _playerInventory = _player.GetComponent<playerInventory>();
        _messaging = _player.GetComponent<FishingMessaging>();

        // Random array selection found here
        // https://stackoverflow.com/questions/14297853/how-to-get-random-values-from-array-in-c-sharp
        randomInputKey = inputKeys[random.Next(0, inputKeys.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (isFishHooked)
        {
            HandleCatch();
        }
    }

    // Used in PlayerFish.cs
    public GameObject GetHookedFishGO()
    {
        return isFishHooked ? hookedFishGO : null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // https://docs.unity3d.com/ScriptReference/Collider.OnCollisionEnter.html
        // https://forum.unity.com/threads/add-an-object-as-a-child-of-another-gameobject-into-a-different-scene.1292907/
        if (!isFishHooked)
        {
            // check for fish collision
            if (collision.gameObject.CompareTag("Fish"))
            {
                hookedFishGO = collision.gameObject;
                hookedFishRB = hookedFishGO.GetComponent<Rigidbody>();
                FishAI fishAIscript = hookedFishGO.GetComponent<FishAI>();

                if (!fishAIscript.WasRecentlyCaught)
                {
                    // Freeze the bobber
                    _rb.constraints = RigidbodyConstraints.FreezeAll;
                    collision.transform.SetParent(transform);

                    if (hookedFishRB != null)
                    {
                        hookedFishRB.constraints = RigidbodyConstraints.FreezeAll;
                    }
                    fishAIscript.enabled = false; // turn off AI when caught
                    isFishHooked = true;
                }
                else
                {
                    hookedFishGO = null;
                    hookedFishRB = null;
                    fishAIscript = null;
                }
            }
        }
    }

    private void ReleaseFish()
    {
        _messaging.DisplayMessage("Oh no! The fish got away...\n\nBetter luck next time!");
        keyPressesRemaining = 5;
        isFishHooked = false;
        FishAI fishAIscript = hookedFishGO.GetComponent<FishAI>();
        fishAIscript.enabled = true;
        fishAIscript.aiState = FishAI.AIState.fleeState;
        fishAIscript.WasRecentlyCaught = true;
        hookedFishRB.constraints = RigidbodyConstraints.None;
        hookedFishGO.transform.SetParent(null);
        hookedFishGO = null;
        hookedFishRB = null;
    }

    float countdownTimer = 15.0f;
    float minSeconds = 3.0f;
    float maxSeconds = 5.0f;

    // Must take into account bobber
    void HandleCatch()
    {
        /*
         * Change timer based on fish difficulty:
         * easy = 10 seconds
         * med = 7 seconds
         * hard = 5 seconds
         * boss = 3 seconds
          */
        countdownTimer -= Time.deltaTime;

        // Set the msg disappear to countdown
        if (countdownTimer >= 0.0f)
        {
            // TODO handle externally
            _messaging.DisplayMessage(
                $"You hooked a fish!\n\nPress {randomInputKey.ToUpper()}!\n\nKeypresses remaining: {keyPressesRemaining}\n\nTime left: {countdownTimer}",
                countdownTimer
            );
        }
        else
        {
            ReleaseFish();
        }

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
                ReleaseFish();
            }
        }
        if (keyPressesRemaining == 0)
        {
            _messaging.DisplayMessage("CONGRATS!\n\nYou caught a fish!");
            keyPressesRemaining = 5;
            isFishHooked = false;
            _playerInventory.AddFishedFish("Fish Type, Other");
            Destroy(GameObject.FindWithTag("Bobber"));
        }
    }
}
