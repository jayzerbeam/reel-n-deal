using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerJump : MonoBehaviour
{
    PlayerInput _playerInput;
    CharacterController _characterController;
    Animator _animator;
    Vector3 _characterVelocity;
    int _isJumpingHash;
    bool _isJumpButtonPressed;
    bool _isJumping;
    bool _isFishing;

    // Can edit in Unity
    [SerializeField]
    float _jumpHeight = 2f;

    // Can edit in Unity
    [SerializeField]
    float _gravity = -9.81f;
    float _groundedGravity = 0.05f;

    void Awake()
    {
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _isJumpingHash = Animator.StringToHash("isJumping");
        _playerInput.CharacterControls.Jump.performed += OnJump;
    }

    void Update()
    {
        _isJumping = _animator.GetBool(_isJumpingHash);
        _isFishing = GameObject.FindWithTag("Bobber");

        HandleAnimation();
    }

    void FixedUpdate()
    {
        HandleGravity();
        HandleJump();
    }

    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        _isJumpButtonPressed = context.ReadValueAsButton();
    }

    void HandleAnimation()
    {
        if (!_isFishing)
        {
            if (_isJumpButtonPressed && !_isJumping)
            {
                _animator.SetBool(_isJumpingHash, true);
            }
            else if (!_isJumpButtonPressed && _isJumping)
            {
                _animator.SetBool(_isJumpingHash, false);
            }
        }
    }

    void HandleGravity()
    {
        if (_characterController.isGrounded)
        {
            _characterVelocity.y = _groundedGravity;
        }
        else
        {
            _characterVelocity.y += _gravity * Time.deltaTime;
        }
    }

    void HandleJump()
    {
        bool isFishing = GameObject.FindWithTag("Bobber");
        if (_isJumpButtonPressed && _characterController.isGrounded && !isFishing)
        {
            _characterVelocity.y += Mathf.Sqrt(_jumpHeight * -2.0f * _gravity);
            _isJumpButtonPressed = false;
        }

        _characterController.Move(_characterVelocity * Time.deltaTime);
    }
}
