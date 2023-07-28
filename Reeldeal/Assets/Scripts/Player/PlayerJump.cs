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
    bool _isGrounded;

    float _jumpHeight = 2f;

    public float _gravity = -9.81f;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _isJumpingHash = Animator.StringToHash("isJumping");
    }

    void Start()
    {
        _characterVelocity = new Vector3(0, 0, 0);
        playerInput.actions["Jump"].started += OnJump;
        playerInput.actions["Jump"].canceled += OnJump;
    }

    void Update()
    {
        _isJumping = _animator.GetBool(_isJumpingHash);
        // TODO refactor for better performance
        _isFishing = GameObject.FindWithTag("Bobber");

        HandleAnimation();
    }

    void FixedUpdate()
    {
        HandleJump();
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

    void HandleJump()
    {
        if (_isGrounded && _characterVelocity.y < 0.0f)
        {
            _characterVelocity.y = 0.0f;
        }

        _characterVelocity.y -= _gravity * -1.0f * Time.fixedDeltaTime;

        if (_isJumpButtonPressed && _isGrounded && !_isFishing)
        {
            _characterVelocity.y += Mathf.Sqrt(_jumpHeight * -2.0f * _gravity);
        }
        _characterController.Move(_characterVelocity * Time.fixedDeltaTime);
        _isGrounded = _characterController.isGrounded;
    }

    public void increaseJumpHeight(float multiplier)
    {
        _jumpHeight *= multiplier;
    }
}
