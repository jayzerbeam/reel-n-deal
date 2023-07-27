using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerJump : MonoBehaviour
{
    public PlayerInput playerInput;
    InputAction jumpAction;
    CharacterController _characterController;

    Animator _animator;
    Vector3 _characterVelocity;

    int _isJumpingHash;
    bool _isJumpButtonPressed;
    bool _isJumping;
    bool _isFishing;

    float _jumpHeight = 2f;

    public float _gravity = -9.81f;
    public float _groundedGravity = 0.05f;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _isJumpingHash = Animator.StringToHash("isJumping");
    }

    void Start()
    {
        playerInput.actions["Jump"].started += OnJump;
        playerInput.actions["Jump"].canceled += OnJump;
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
        _characterController.Move(_characterVelocity * Time.fixedDeltaTime);
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
            _characterVelocity.y += _gravity * Time.fixedDeltaTime;
        }
    }

    void HandleJump()
    {
        if (_isJumpButtonPressed && _characterController.isGrounded && !_isFishing)
        {
            _characterVelocity.y += Mathf.Sqrt(_jumpHeight * -2.0f * _gravity);
        }
    }

    public void increaseJumpHeight(float multiplier)
    {
        _jumpHeight *= multiplier;
    }
}
