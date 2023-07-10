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
    public GameObject bobber;

    [SerializeField]
    float _castSpeed = 80f;

    [SerializeField]
    float _castHeight = 2f;

    [SerializeField]
    float _bobberGravity = -10f;

    int _isCastingHash;
    bool _isCasting;
    bool _isCastButtonPressed;

    void Awake()
    {
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
        FindBobber();
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

    bool FindBobber()
    {
        return GameObject.FindWithTag("Bobber");
    }

    void HandleAnimation()
    {
        _isCasting = _animator.GetBool(_isCastingHash);

        if (_isCastButtonPressed && !_isCasting && !FindBobber())
        {
            _animator.SetBool(_isCastingHash, true);
        }
        else if (!_isCastButtonPressed && _isCasting)
        {
            _animator.SetBool(_isCastingHash, false);
        }
    }

    void HandleCast()
    {
        if (_isCastButtonPressed && !FindBobber())
        {
            GameObject newBobber = Instantiate(
                bobber,
                this.transform.position + new Vector3(0f, _castHeight, 1),
                this.transform.rotation
            );

            if (newBobber != null)
            {
                Rigidbody bobberRB = newBobber.GetComponent<Rigidbody>();

                Vector3 castVelocity = transform.forward * _castSpeed;

                bobberRB.velocity = new Vector3(castVelocity.x, _bobberGravity, castVelocity.z);
            }
        }
    }
}
