using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    PlayerInput _playerInput;
    CharacterController _characterController;
    Animator _animator;

    Vector2 currMovementInput;
    Vector3 currMovement;
    Vector3 currRunMovement;

    int _isWalkingHash;
    int _isRunningHash;

    bool _isMovementPressed;
    bool _isRunPressed;

    float _runSpeed = 8.0f;
    float _walkSpeed = 2.8f;
    float _rotate;

    [SerializeField]
    float _rotationSpeed = 75f;

    void Awake()
    {
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");

        // Walk input
        _playerInput.CharacterControls.Move.started += OnMovementInput;
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;
        _playerInput.CharacterControls.Move.performed += OnMovementInput;
        // Run input
        _playerInput.CharacterControls.Run.performed += OnRun;
        _playerInput.CharacterControls.Run.canceled += OnRun;
        // Look input
        _playerInput.CharacterControls.Look.performed += OnLook;
    }

    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    void Update()
    {
        HandleRotation();
        HandleMove();
    }

    void OnLook(InputAction.CallbackContext context)
    {
        _rotate = context.ReadValue<Vector2>().x;
    }

    void OnRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currMovementInput = context.ReadValue<Vector2>();
        // Walk input
        currMovement.x = currMovementInput.x * _walkSpeed;
        currMovement.z = currMovementInput.y * _walkSpeed;
        // Run Input
        currRunMovement.x = currMovementInput.x * _runSpeed;
        currRunMovement.z = currMovementInput.y * _runSpeed;
        _isMovementPressed = currMovementInput.x != 0 || currMovementInput.y != 0;
    }

    // Can use CharacterController.velocity to match to animation states
    // See PlayerJump.cs
    // void HandleAnimation()
    // {
    //     bool isWalking = _animator.GetBool(_isWalkingHash);
    //     bool isRunning = _animator.GetBool(_isRunningHash);
    //
    //     if (_isMovementPressed && !isWalking)
    //     {
    //         _animator.SetBool(_isWalkingHash, true);
    //     }
    //     else if (!_isMovementPressed && isWalking)
    //     {
    //         _animator.SetBool(_isWalkingHash, false);
    //     }
    // }

    void HandleMove()
    {
        Vector3 moveDirection = transform.TransformDirection(currMovement);

        if (_isRunPressed)
        {
            _characterController.Move(moveDirection.normalized * _runSpeed * Time.deltaTime);
        }
        else
        {
            _characterController.Move(moveDirection.normalized * _walkSpeed * Time.deltaTime);
        }
    }

    void HandleRotation()
    {
        if (Mouse.current != null && Mouse.current.delta.ReadValue().magnitude > 0f)
        {
            _rotationSpeed = 5f;
        }
        transform.Rotate(Vector3.up * _rotate * _rotationSpeed * Time.deltaTime);
    }
}
