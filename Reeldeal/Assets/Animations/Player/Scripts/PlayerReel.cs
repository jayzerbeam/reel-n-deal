using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerReel : MonoBehaviour
{
    PlayerInput _playerInput;
    CharacterController _characterController;
    Animator _animator;
    int _isReelingHash;
    bool _isReelButtonPressed;

    // Grady's stuff
    public bool fishCaught = false;
    public bool release = false;
    public bool bobberLocked = false;
    public float catchCoolDown = 5f;
    public float catchCoolDownTimer = 0f;
    public bool startCoolDown;

    // Grady's stuff
    private Vector3 bobberLockedPosition;
    private Quaternion bobberLockedRotation;
    private Vector3 fishLockedPosition;
    private Quaternion fishLockedRotation;

    public GameObject caughtFish;

    void Awake()
    {
        _playerInput = new PlayerInput();
        _animator = GetComponent<Animator>();
        _isReelingHash = Animator.StringToHash("isReeling");
        _characterController = GetComponent<CharacterController>();

        _playerInput.CharacterControls.Reel.started += OnReel;
        _playerInput.CharacterControls.Reel.canceled += OnReel;
        _playerInput.CharacterControls.Reel.performed += OnReel;
    }

    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (bobberLocked)
        {
            transform.position = bobberLockedPosition; // prevents other fish from meshing with position
            transform.rotation = bobberLockedRotation;

            caughtFish.transform.position = fishLockedPosition;
            caughtFish.transform.rotation = fishLockedRotation;
        }

        if (fishCaught)
        {
            // create method to have player press keys here
            HandleReel();
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

    void OnReel(InputAction.CallbackContext context)
    {
        _isReelButtonPressed = context.ReadValueAsButton();
    }

    void HandleReel()
    {
        Debug.Log("You snagged a fish! Reel it in!");
    }

    void HandleAnimation() { }

    private void OnCollisionEnter(Collision collision)
    {
        // https://docs.unity3d.com/ScriptReference/Collider.OnCollisionEnter.html
        // https://forum.unity.com/threads/add-an-object-as-a-child-of-another-gameobject-into-a-different-scene.1292907/
        if (!fishCaught)
        {
            // check for fish collision
            if (collision.gameObject.CompareTag("Fish"))
            {
                bobberLocked = true;
                bobberLockedPosition = transform.position;
                bobberLockedRotation = transform.rotation;

                collision.transform.SetParent(transform);
                Rigidbody fishRigidbody = collision.gameObject.GetComponent<Rigidbody>();

                if (fishRigidbody != null)
                {
                    fishRigidbody.velocity = Vector3.zero;
                }

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
