using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerCast : MonoBehaviour
{
    PlayerInput _playerInput;
    CharacterController _characterController;
    Animator _animator;
    GameObject _bobber;

    int _isCastingHash;
    bool _isCastButtonPressed;

    bool _doesBobberExist = false;

    void Awake()
    {
        _bobber = GameObject.FindGameObjectWithTag("Bobber");
        _playerInput = new PlayerInput();
        _animator = GetComponent<Animator>();
        _isCastingHash = Animator.StringToHash("isCasting");
        _characterController = GetComponent<CharacterController>();

        _playerInput.CharacterControls.Cast.started += OnCast;
        _playerInput.CharacterControls.Cast.canceled += OnCast;
        _playerInput.CharacterControls.Cast.performed += OnCast;
    }

    void Update()
    {
        HandleAnimation();
    }

    void FixedUpdate()
    {
        HandleCast();
    }

    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    void OnCast(InputAction.CallbackContext context)
    {
        _isCastButtonPressed = context.ReadValueAsButton();
    }

    void HandleAnimation()
    {
        bool isCasting = _animator.GetBool(_isCastingHash);

        if (_isCastButtonPressed && !isCasting)
        {
            _animator.SetBool(_isCastingHash, true);
        }
        else if (!_isCastButtonPressed && isCasting)
        {
            _animator.SetBool(_isCastingHash, false);
        }
    }

    void HandleCast()
    {
        if (_isCastButtonPressed && !_doesBobberExist && _characterController.isGrounded)
        {
            Instantiate(_bobber, transform.position, Quaternion.identity);
            _doesBobberExist = true;
        }
    }
}
