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

    int _isWalkingHash;
    int _isRunningHash;
    int _velocityZHash;
    int _velocityXHash;

    bool _isMovementPressed;
    bool _isRotationPressed;
    bool _isRunPressed;
    bool _isMovementFrozen;

    bool _isWalkingAnim;
    bool _isRunningAnim;
    bool _isFishingAnim;

    // TODO create base movement & rotation & multiply instead of multiple vars
    [SerializeField]
    float _walkSpeed = 2.0f;

    [SerializeField]
    float _stillRotationSpeed = 60f;

    [SerializeField]
    float _walkRotationSpeed = 160f;

    [SerializeField]
    float _runSpeed = 6.0f;

    [SerializeField]
    float _runRotationSpeed = 240f;

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
        _isWalkingAnim = _animator.GetBool(_isWalkingHash);
        _isRunningAnim = _animator.GetBool(_isRunningHash);
        _isFishingAnim = GameObject.FindWithTag("Bobber");

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

    void HandleMove()
    {
        Vector3 baseRotation = Vector3.up * _inputValues.x * Time.fixedDeltaTime;
        Vector3 movement = transform.forward * _inputValues.y * _walkSpeed * Time.deltaTime;

        if (_isMovementFrozen)
        {
            return;
        }
        if (_isMovementPressed)
        {
            if (_isRunPressed)
            {
                Quaternion angleRot = Quaternion.Euler(baseRotation * _runRotationSpeed);
                _characterController.Move(movement * _runSpeed);
                transform.rotation *= angleRot;
            }
            else
            {
                Quaternion angleRot = Quaternion.Euler(baseRotation * _walkRotationSpeed);
                _characterController.Move(movement * _walkSpeed);
                transform.rotation *= angleRot;
            }
        }
        else if (!_isMovementPressed && _isRotationPressed)
        {
            Quaternion angleRot = Quaternion.Euler(baseRotation * _stillRotationSpeed);
            transform.rotation *= angleRot;
        }
    }

    void FreezeIfFishing()
    {
        if (_isFishingAnim)
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
