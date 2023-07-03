using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerJump : MonoBehaviour
{
    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private Animator _animator;
    private Vector3 _characterVelocity;
    private int _isJumpingHash;
    private bool _isJumpButtonPressed;

    // Can edit in Unity
    [SerializeField]
    private float _jumpHeight = 3.25f;

    // Can edit in Unity
    [SerializeField]
    private float _gravity = -9.81f;
    private float _groundedGravity = 0.05f;

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
        HandleAnimation();
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
        bool isJumping = _animator.GetBool(_isJumpingHash);

        if (_isJumpButtonPressed && !isJumping)
        {
            _animator.SetBool(_isJumpingHash, true);
        }
        else if (!_isJumpButtonPressed && isJumping)
        {
            _animator.SetBool(_isJumpingHash, false);
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
        if (_isJumpButtonPressed && _characterController.isGrounded)
        {
            _characterVelocity.y += Mathf.Sqrt(_jumpHeight * -2.0f * _gravity);
            _isJumpButtonPressed = false;
        }

        _characterController.Move(_characterVelocity * Time.deltaTime);
    }
}
