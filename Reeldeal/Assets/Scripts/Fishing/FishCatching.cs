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

    GameObject _canvasObject;
    Canvas _fishCaughtMsg; // Reference to the canvas GameObject

    private Vector3 bobberLockedPosition;
    private Quaternion bobberLockedRotation;
    private Vector3 fishLockedPosition;
    private Quaternion fishLockedRotation;

    private GameObject caughtFish;
    private Rigidbody _rb;

    PlayerReel _playerReel = new PlayerReel();

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _canvasObject = GameObject.Find("FishCaughtMsg");

        if (_canvasObject != null)
        {
            _fishCaughtMsg = _canvasObject.GetComponent<Canvas>();
            _fishCaughtMsg.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bobberLocked)
        {
            // transform.position = bobberLockedPosition; // prevents other fish from meshing with position
            // transform.rotation = bobberLockedRotation;
        }
        if (caughtFish)
        {
            // caughtFish.transform.position = fishLockedPosition;
            // caughtFish.transform.rotation = fishLockedRotation;
        }

        if (fishCaught)
        {
            // create method to have player press keys here
            _playerReel.HandleCatchFish();
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

    private void OnTriggerEnter(Collider collider)
    {
        if (
            collider.gameObject.CompareTag("River")
            || collider.gameObject.CompareTag("Lake")
            || collider.gameObject.CompareTag("Ocean")
            || collider.gameObject.CompareTag("Spring")
        )
        {
            bobberLocked = true;
            bobberLockedPosition = transform.position;
            bobberLockedRotation = transform.rotation;

            _rb.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // https://docs.unity3d.com/ScriptReference/Collider.OnCollisionEnter.html
        // https://forum.unity.com/threads/add-an-object-as-a-child-of-another-gameobject-into-a-different-scene.1292907/
        if (!fishCaught)
        {
            // Ground or water collision to stop bobber
            if (collision.gameObject.CompareTag("Ground"))
            {
                bobberLocked = true;
                bobberLockedPosition = transform.position;
                bobberLockedRotation = transform.rotation;

                _rb.constraints = RigidbodyConstraints.FreezePosition;
            }
            // check for fish collision
            if (collision.gameObject.CompareTag("Fish"))
            {
                collision.transform.SetParent(transform);
                Rigidbody fishRigidbody = collision.gameObject.GetComponent<Rigidbody>();

                _fishCaughtMsg.enabled = true;

                if (fishRigidbody != null)
                {
                    fishRigidbody.velocity = Vector3.zero;
                }

                fishRigidbody.constraints = RigidbodyConstraints.FreezePosition;
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
}
