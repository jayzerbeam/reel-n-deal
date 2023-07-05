using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (fishCaught)
        // {
        //     create method to have player press keys here
        // }

        // if (release) // enter if player doesn't press correct inputs
        // {
        //     ReleaseFish();
        //     bobberLocked = false;
        // }

        // catchCoolDownTimer -= Time.deltaTime;
        //
        // if (startCoolDown)
        // {
        //     transform.position = bobberLockedPosition; // prevents other fish from meshing with position
        //     transform.rotation = bobberLockedRotation;
        //     if (catchCoolDownTimer < 0f && fishCaught)
        //     {
        //         fishCaught = false;
        //         startCoolDown = false;
        //     }
        // }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // https://docs.unity3d.com/ScriptReference/Collider.OnCollisionEnter.html
        // https://forum.unity.com/threads/add-an-object-as-a-child-of-another-gameobject-into-a-different-scene.1292907/
        if (!fishCaught)
        {
            // Ground or water collision to stop bobber
            if (
                collision.gameObject.CompareTag("Ground")
                || collision.gameObject.CompareTag("Water")
            )
            {
                _rb.constraints = RigidbodyConstraints.FreezePosition;
                bobberLocked = true;
            }
            // check for fish collision
            if (collision.gameObject.CompareTag("Fish"))
            {
                collision.transform.SetParent(transform);
                Rigidbody fishRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                if (fishRigidbody != null)
                {
                    fishRigidbody.velocity = Vector3.zero;
                }

                fishRigidbody.constraints = RigidbodyConstraints.FreezePosition;

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
}
