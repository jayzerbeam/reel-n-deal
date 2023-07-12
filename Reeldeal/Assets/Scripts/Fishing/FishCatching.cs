using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class FishCatching : MonoBehaviour
{
    PlayerInput _playerInput;
    public bool isFishCaught = false;
    private GameObject hookedFishGO;
    private Rigidbody hookedFishRB;
    private Rigidbody _rb;

    public TextMeshProUGUI talk_to_playerText;
    public float timeToErase = 2f;

    // HandleCatch Variables
    int keyPressesRemaining = 5;
    string[] inputKeys = { "w", "a", "s", "d", "q", "e" };
    System.Random random = new System.Random();
    string randomInputKey = "";

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = new PlayerInput();

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
        if (isFishCaught)
        {
            // HandleCatch();
        }
    }

    public GameObject GetHookedFishGO()
    {
        if (isFishCaught)
        {
            return hookedFishGO;
        }
        else
        {
            return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // https://docs.unity3d.com/ScriptReference/Collider.OnCollisionEnter.html
        // https://forum.unity.com/threads/add-an-object-as-a-child-of-another-gameobject-into-a-different-scene.1292907/
        if (!isFishCaught)
        {
            // check for fish collision
            if (collision.gameObject.CompareTag("Fish"))
            {
                // Freeze the bobber
                _rb.constraints = RigidbodyConstraints.FreezeAll;
                collision.transform.SetParent(transform);

                hookedFishGO = collision.gameObject;
                hookedFishRB = hookedFishGO.GetComponent<Rigidbody>();

                if (hookedFishRB != null)
                {
                    hookedFishRB.velocity = Vector3.zero;
                    hookedFishRB.constraints = RigidbodyConstraints.FreezeAll;
                }

                FishAI fishAIscript = collision.gameObject.GetComponent<FishAI>();
                fishAIscript.enabled = false; // turn off AI when caught
                isFishCaught = true;
            }
        }
    }

    private void ReleaseFish()
    {
        FishAI fishAIscript = hookedFishGO.GetComponent<FishAI>();
        fishAIscript.enabled = true;
        fishAIscript.aiState = FishAI.AIState.fleeState;
        hookedFishGO.transform.SetParent(null);
        hookedFishGO = null;
        isFishCaught = false;
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
        timeToErase = float.PositiveInfinity;
        hookedFishRB.constraints = RigidbodyConstraints.None;

        // TODO must display the countdown timer
        if (randomInputKey != "")
        {
            talk_to_player(
                $"You hooked a fish!\n\nPress {randomInputKey.ToUpper()}!\nKeypresses remaining: {keyPressesRemaining}"
            );
        }

        if (!Input.GetMouseButton(0) && Input.anyKey)
        // Reeling is okay
        // if (_playerInput.CharacterControls.Reel.ReadValue<float>() <= 0.0f && Input.anyKey)
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
                keyPressesRemaining = 5;
                timeToErase = 3.0f;
                Destroy(GameObject.FindGameObjectWithTag("Bobber"));
                talk_to_player("Oh no! The fish got away...");
                ReleaseFish();
            }
        }
        if (keyPressesRemaining == 0)
        {
            keyPressesRemaining = 5;
            isFishCaught = false;
            timeToErase = 3f;
            talk_to_player("CONGRATS! You caught a fish!");
            randomInputKey = "";
            Destroy(GameObject.FindGameObjectWithTag("Bobber"));
            // TODO Actually add to inventory
            Destroy(hookedFishGO);
        }
    }

    // Remove or replace later with the actual instantiated function.
    public void talk_to_player(string talk_to)
    {
        StartCoroutine(talk_to_playerWritethenEraseText(talk_to));
    }

    private IEnumerator talk_to_playerWritethenEraseText(string text)
    {
        talk_to_playerText.text = text;

        yield return new WaitForSeconds(timeToErase);

        talk_to_playerText.text = "";
    }
}
