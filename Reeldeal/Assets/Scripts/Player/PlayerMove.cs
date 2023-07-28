using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerMove : MonoBehaviour
{
    public PlayerInput playerInput;

    CharacterController _characterController;
    Animator _animator;
    Vector2 _inputValues;

    int _isWalkingHash;
    int _isRunningHash;
    int _isFishingHash;
    int _velocityZHash;
    int _velocityXHash;

    bool _isMovementPressed;
    bool _isRotationPressed;
    bool _isRunPressed;
    bool _isMovementFrozen;

    bool _isWalkingAnim;
    bool _isRunningAnim;
    bool _isFishingAnim;

    float _walkSpeed = 2.0f;
    float _stillRotationSpeed = 60f;
    float _walkRotationSpeed = 160f;
    float _runSpeed = 6.0f;
    float _runRotationSpeed = 240f;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isFishingHash = Animator.StringToHash("isFishing");
        _velocityXHash = Animator.StringToHash("Velocity X");
        _velocityZHash = Animator.StringToHash("Velocity Z");
    }

    void Start()
    {
        playerInput.actions["Move"].started += OnMovementInput;
        playerInput.actions["Move"].canceled += OnMovementInput;
        playerInput.actions["Move"].performed += OnMovementInput;
        playerInput.actions["Run"].started += OnRun;
        playerInput.actions["Run"].performed += OnRun;
        playerInput.actions["Run"].canceled += OnRun;
    }

    void Update()
    {
        _isWalkingAnim = _animator.GetBool(_isWalkingHash);
        _isRunningAnim = _animator.GetBool(_isRunningHash);
        _isFishingAnim = _animator.GetBool(_isFishingHash);

        HandleAnimation();
        FreezeIfFishing();
    }

    void FixedUpdate()
    {
        HandleMove();
    }

    void OnRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        _inputValues = context.ReadValue<Vector2>();
        _isMovementPressed = _inputValues.y != 0;
        _isRotationPressed = _inputValues.x != 0;
    }

    void HandleAnimation()
    {
        // Run!
        if (!_isFishingAnim)
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
            else if (
                (_isMovementPressed && !_isRunPressed)
                || (_isRotationPressed && !_isMovementPressed && !_isRunPressed)
                || (_isRotationPressed && !_isMovementPressed && _isRunPressed)
            )
            {
                _animator.SetBool(_isWalkingHash, true);
                _animator.SetBool(_isRunningHash, false);
                _animator.SetFloat(_velocityZHash, _inputValues.y);
                _animator.SetFloat(_velocityXHash, _inputValues.x);
            }
            // Idle.
            else if (!_isMovementPressed && _isWalkingAnim || !_isMovementPressed && _isRunningAnim)
            {
                float resetMultiplier = 0.0f;
                _animator.SetBool(_isWalkingHash, false);
                _animator.SetBool(_isRunningHash, false);
                _animator.SetFloat(_velocityZHash, resetMultiplier);
                _animator.SetFloat(_velocityXHash, resetMultiplier);
            }
        }
    }

    Quaternion GetRotationAngle(float rotationSpeed)
    {
        // Learning C# by Developing Games With Unity
        // Ch. 7, Subsection: "Rigidbody Components in Motion"
        // Author: Harrison Ferrone
        return Quaternion.Euler(Vector3.up * _inputValues.x * Time.fixedDeltaTime * rotationSpeed);
    }

    // TODO: refactor to remove repeat .Move code - only update multipliers
    void HandleMove()
    {
        Vector3 movement = transform.forward * _inputValues.y * _walkSpeed * Time.fixedDeltaTime;
        // TODO: refactor for performance
        bool playerDoesNotHaveBoots = GameObject.Find("Boots");
        float bootSpeedMultiplier = 2.0f;

        if (_isMovementFrozen)
        {
            return;
        }
        if (_isMovementPressed)
        {
            if (_isRunPressed)
            {
                if (playerDoesNotHaveBoots)
                {
                    _characterController.Move(movement * _runSpeed);
                }
                else
                {
                    _characterController.Move(movement * _runSpeed * bootSpeedMultiplier);
                }
                transform.rotation *= GetRotationAngle(_runRotationSpeed);
            }
            else
            {
                if (playerDoesNotHaveBoots)
                {
                    _characterController.Move(movement * _walkSpeed);
                }
                else
                {
                    _characterController.Move(movement * _walkSpeed * bootSpeedMultiplier);
                }
                transform.rotation *= GetRotationAngle(_walkRotationSpeed);
            }
        }
        else if (!_isMovementPressed && _isRotationPressed)
        {
            transform.rotation *= GetRotationAngle(_stillRotationSpeed);
        }
    }

    public void increaseWalkSpeed(float multiplier)
    {
        _walkSpeed *= multiplier;
    }

    void FreezeIfFishing()
    {
        if (_isFishingAnim)
        {
            _isMovementFrozen = true;
            _characterController.enabled = false;
        }
        else
        {
            _isMovementFrozen = false;
            _characterController.enabled = true;
        }
    }
}
