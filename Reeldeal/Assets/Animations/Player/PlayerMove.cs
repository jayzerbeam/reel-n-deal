using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    PlayerInput _playerInput;
    CharacterController _characterController;
    Animator _animator;

    Vector2 _inputValues;

    Vector3 currMovement;
    Vector3 currRunMovement;

    int _isWalkingHash;
    int _isRunningHash;

    bool _isMovementPressed;
    bool _isRunPressed;

    float _runSpeed = 8.0f;
    float _walkSpeed = 2.8f;

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
        HandleAnimation();
        HandleMove();
    }

    void OnRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        _inputValues = context.ReadValue<Vector2>();

        // Walk input
        currMovement.x = _inputValues.x * _walkSpeed;
        currMovement.z = _inputValues.y * _walkSpeed;

        // Run Input
        currRunMovement.x = _inputValues.x * _runSpeed;
        currRunMovement.z = _inputValues.y * _runSpeed;

        _isMovementPressed = _inputValues.x != 0 || _inputValues.y != 0;
    }

    // Can use CharacterController.velocity.x etc to match to animation states
    void HandleAnimation()
    {
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool isRunning = _animator.GetBool(_isRunningHash);

        if (_isMovementPressed && !isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        else if (!_isMovementPressed && isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }
    }

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
}
