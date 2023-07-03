using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFish : MonoBehaviour
{
    PlayerInput _playerInput;
    CharacterController _characterController;
    public GameObject bobber;
    public LayerMask groundLayer; // Layer for the ground
    public LayerMask waterLayer; // Layer for the water
    public float distanceToGround; // Distance to ground
    public float allowedAirTime = 5f; // Maximum time player can be in the air
    public float airTimeCounter; // Time counter for player's air time
    private bool jumpRequest = false; // Flag to handle jump requests
    public bool onGround;
    public bool onWater;
    public float distanceCanJump = 0.25f; // Distance to ground to enable jumping
    public bool _isFishPressed;

    void Start()
    {
        bobber = GameObject.FindWithTag("Bobber");
    }

    void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.CharacterControls.Fish.started += OnFish;
        _playerInput.CharacterControls.Fish.canceled += OnFish;
        _playerInput.CharacterControls.Fish.performed += OnFish;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        HandleFish();
    }

    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    void HandleFish()
    {
        if (_isFishPressed)
        {
            Instantiate(bobber, transform.position, Quaternion.identity);
        }
    }

    void OnFish(InputAction.CallbackContext context)
    {
        _isFishPressed = context.ReadValueAsButton();
    }

    void CheckGround()
    {
        RaycastHit hit;
        if (
            Physics.Raycast(transform.position, Vector3.down, out hit, distanceCanJump, groundLayer)
        )
        {
            distanceToGround = hit.distance; // Save the distance to the ground
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                onGround = true;
                airTimeCounter = 0; // Reset air time counter when player is on the ground
                return;
            }
        }
        onGround = false;

        // Teleport to nearest ground position if not on ground and air time exceeds allowed air time
        if (
            !onGround
            && airTimeCounter > allowedAirTime
            && Physics.Raycast(
                transform.position,
                Vector3.down,
                out hit,
                Mathf.Infinity,
                groundLayer
            )
        )
        {
            distanceToGround = hit.distance; // Save the distance to the ground
            transform.position = hit.point;
        }

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.05f, waterLayer))
        {
            onWater = true;
            onGround = false;
        }
        else if (Physics.Raycast(transform.position, Vector3.up, out hit, 55f, waterLayer))
        {
            onWater = true;
            onGround = false;
        }
        else
        {
            onWater = false;
        }
    }
}
