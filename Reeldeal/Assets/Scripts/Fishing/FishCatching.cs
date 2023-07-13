using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class FishCatching : MonoBehaviour
{
    GameObject _player;
    playerInventory _playerInventory;
    GameObject hookedFishGO;
    Rigidbody hookedFishRB;
    Rigidbody _rb;
    bool isFishHooked = false;

    public TextMeshProUGUI talk_to_playerText;

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

        GameObject textToPlayer = GameObject.Find("textToPlayer");
        // Random array selection found here
        // https://stackoverflow.com/questions/14297853/how-to-get-random-values-from-array-in-c-sharp
        randomInputKey = inputKeys[random.Next(0, inputKeys.Length)];

        if (textToPlayer != null)
        {
            talk_to_playerText = textToPlayer.GetComponent<TextMeshProUGUI>();
        }
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
        FishAI fishAIscript = hookedFishGO.GetComponent<FishAI>();
        fishAIscript.enabled = true;
        fishAIscript.aiState = FishAI.AIState.fleeState;
        fishAIscript.WasRecentlyCaught = true;
        hookedFishRB.constraints = RigidbodyConstraints.None;

        hookedFishGO.transform.SetParent(null);
        hookedFishGO = null;
        hookedFishRB = null;
    }

    // TODO countdown in real time and display to user
    void ShowCountdown()
    {
        // 3-5 second timer
        float countdownSeconds = 0.0f;
        float minSeconds = 3.0f;
        float maxSeconds = 5.0f;
        talk_to_player(countdownSeconds.ToString());
    }

    // Must take into account bobber
    void HandleCatch()
    {
        // TODO must display the countdown timer
        // Set the msg disappear to countdown
        talk_to_player(
            $"You hooked a fish!\n\nPress {randomInputKey.ToUpper()}!\nKeypresses remaining: {keyPressesRemaining}"
        );

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
                talk_to_player("Oh no! The fish got away...", 5f);
                keyPressesRemaining = 5;
                isFishHooked = false;
                ReleaseFish();
            }
        }
        if (keyPressesRemaining == 0)
        {
            // Why isn't this disappearing?
            talk_to_player("CONGRATS! You caught a fish!", 5f);
            keyPressesRemaining = 5;
            isFishHooked = false;
            // Destroy(GameObject.FindWithTag("Bobber"));
            _playerInventory.AddFishedFish("Fish Type, Other");
        }
    }

    // Remove or replace later with the actual instantiated function.
    public void talk_to_player(string talk_to, float timeToErase = 8f)
    {
        StartCoroutine(talk_to_playerWritethenEraseText(talk_to, timeToErase));
    }

    private IEnumerator talk_to_playerWritethenEraseText(string text, float timeToErase)
    {
        talk_to_playerText.text = text;

        yield return new WaitForSeconds(timeToErase);

        talk_to_playerText.text = "";
    }
}
