using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    PlayerInput _playerInput;
    Rigidbody _rb;
    CharacterController _characterController;
    Animator _animator;

    Vector2 _inputValues;

    Vector3 currMovement;
    Vector3 currRunMovement;

    int _isWalkingHash;
    int _isRunningHash;
    int _velocityZHash;
    int _velocityXHash;

    bool _isMovementPressed;
    bool _isRunPressed;
    bool _isMovementFrozen;
    bool _isWalking;
    bool _isRunning;
    bool _isFishing;

    float _runSpeed = 8.0f;
    float _walkSpeed = 2.8f;

    void Awake()
    {
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _velocityXHash = Animator.StringToHash("Velocity X");
        _velocityZHash = Animator.StringToHash("Velocity Z");

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
        _isWalking = _animator.GetBool(_isWalkingHash);
        _isRunning = _animator.GetBool(_isRunningHash);
        _isFishing = GameObject.FindWithTag("Bobber");

        HandleAnimation();
        FreezeIfFishing();
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

    void HandleAnimation()
    {
        // Run!
        if (!_isFishing)
        {
            if (_isMovementPressed && _isRunPressed)
            {
                float runMultiplier = 2.0f;
                _animator.SetBool(_isRunningHash, true);
                _animator.SetBool(_isWalkingHash, false);
                _animator.SetFloat(_velocityZHash, _inputValues.y * runMultiplier);
                _animator.SetFloat(_velocityXHash, _inputValues.x * runMultiplier);
            }
            // Walk.
            else if (_isMovementPressed && !_isRunPressed)
            {
                _animator.SetBool(_isWalkingHash, true);
                _animator.SetBool(_isRunningHash, false);
                _animator.SetFloat(_velocityZHash, _inputValues.y);
                _animator.SetFloat(_velocityXHash, _inputValues.x);
            }
            // Idle.
            else if (!_isMovementPressed && _isWalking || !_isMovementPressed && _isRunning)
            {
                float resetMultiplier = 0.0f;
                _animator.SetBool(_isWalkingHash, false);
                _animator.SetBool(_isRunningHash, false);
                _animator.SetFloat(_velocityZHash, resetMultiplier);
                _animator.SetFloat(_velocityXHash, resetMultiplier);
            }
        }
    }

    void HandleMove()
    {
        Vector3 moveDirection = transform.TransformDirection(currMovement);

        if (_isMovementFrozen)
        {
            return;
        }
        else if (_isRunPressed)
        {
            if (GameObject.Find("Boots") != null)
            {
                _characterController.Move(moveDirection.normalized * _runSpeed * Time.deltaTime);
            }
            else
            {
                _characterController.Move(moveDirection.normalized * (_runSpeed * 2) * Time.deltaTime);
            }


        }
        else
        {
            if (GameObject.Find("Boots") != null)
            {
                _characterController.Move(moveDirection.normalized * _walkSpeed * Time.deltaTime);
            }
            else
            {
                _characterController.Move(moveDirection.normalized * (_walkSpeed * 2) * Time.deltaTime);
            }
        }
    }

    void FreezeIfFishing()
    {
        if (_isFishing)
        {
            _isMovementFrozen = true;
            _rb.isKinematic = true;
        }
        else
        {
            _isMovementFrozen = false;
            _rb.isKinematic = false;
        }
    }
}
