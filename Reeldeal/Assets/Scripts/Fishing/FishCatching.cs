using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishCatching : MonoBehaviour
{
    public bool fishCaught = false;
    public bool release = false;
    public bool bobberLocked = false;

    public float catchCoolDown = 5f;
    public float catchCoolDownTimer = 0f;
    public bool startCoolDown;

    private Vector3 bobberLockedPosition;
    private Quaternion bobberLockedRotation;
    private Vector3 fishLockedPosition;
    private Quaternion fishLockedRotation;

    private GameObject caughtFish;
    private Rigidbody _rb;

    int pressCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bobberLocked)
        {
            transform.position = bobberLockedPosition; // prevents other fish from meshing with position
            transform.rotation = bobberLockedRotation;
        }

        if (caughtFish)
        {
            caughtFish.transform.position = fishLockedPosition;
            caughtFish.transform.rotation = fishLockedRotation;
        }

        if (fishCaught)
        {
            CatchFish();
        }

        if (release) // enter if player doesn't press correct inputs
        {
            ReleaseFish();
            bobberLocked = false;
        }

        catchCoolDownTimer -= Time.deltaTime;

        if (startCoolDown)
        {
            transform.position = bobberLockedPosition; // prevents other fish from meshing with position
            transform.rotation = bobberLockedRotation;
            if (catchCoolDownTimer < 0f && fishCaught)
            {
                fishCaught = false;
                startCoolDown = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // https://docs.unity3d.com/ScriptReference/Collider.OnCollisionEnter.html
        // https://forum.unity.com/threads/add-an-object-as-a-child-of-another-gameobject-into-a-different-scene.1292907/
        if (!fishCaught)
        {
            // check for fish collision
            if (collision.gameObject.CompareTag("Fish"))
            {
                collision.transform.SetParent(transform);
                Rigidbody fishRigidbody = collision.gameObject.GetComponent<Rigidbody>();

                if (fishRigidbody != null)
                {
                    fishRigidbody.velocity = Vector3.zero;
                }

                // fishRigidbody.constraints = RigidbodyConstraints.FreezePosition;
                fishLockedPosition = collision.gameObject.transform.position;
                fishLockedRotation = collision.gameObject.transform.rotation;

                FishAI fishAIscript = collision.gameObject.GetComponent<FishAI>();
                fishAIscript.aiState = FishAI.AIState.fleeState;
                fishAIscript.enabled = false; // turn off AI when caught
                caughtFish = collision.gameObject;
                fishCaught = true;
            }
        }
    }

    private void ReleaseFish()
    {
        FishAI fishAIscript = caughtFish.GetComponent<FishAI>();
        fishAIscript.enabled = true;

        caughtFish.transform.SetParent(null);
        caughtFish = null;
        release = false;

        startCoolDown = true;
        catchCoolDownTimer = catchCoolDown;
    }

    // Add fishcaught logic
    public void CatchFish()
    {
        Rigidbody fishRB = _rb.GetComponentInChildren<Rigidbody>();

        // Better placed elsewhere?
        if (fishRB != null)
        {
            caughtFish = fishRB.gameObject;
            fishRB.constraints = RigidbodyConstraints.None;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            pressCount += 1;
        }

        // Exit condition
        if (pressCount == 5)
        {
            // talk_to_player("You caught the fish!");

            pressCount = 0;

            Destroy(GameObject.FindGameObjectWithTag("Bobber"));

            if (caughtFish)
            {
                Destroy(caughtFish);
                // TODO
                // _inventory.AddFishedFish("Alpha Fish Test Fish");
            }
        }
    }

    // Remove or replace later with the actual instantiated function.
    //
    // public TextMeshProUGUI talk_to_playerText;
    // public float timeToErase = 5f;
    //
    // public void talk_to_player(string talk_to)
    // {
    //     StartCoroutine(talk_to_playerWritethenEraseText(talk_to));
    // }
    //
    // private IEnumerator talk_to_playerWritethenEraseText(string text)
    // {
    //     talk_to_playerText.text = text;
    //
    //     yield return new WaitForSeconds(timeToErase);
    //
    //     talk_to_playerText.text = "";
    // }
}
