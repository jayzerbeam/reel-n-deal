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
    float _castSpeed = 25f;

    [SerializeField]
    float _castHeight = 2f;

    [SerializeField]
    float _bobberGravity = -35f;

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

    // Todo: allow a variety of different cast forces
    void HandleCast()
    {
        if (GameObject.FindWithTag("Bobber") == null)
        {
            if (_isCastButtonPressed && _characterController.isGrounded)
            {
                GameObject newBobber = Instantiate(
                    bobber,
                    this.transform.position + new Vector3(0.5f, _castHeight, 1),
                    this.transform.rotation
                );

                if (newBobber != null)
                {
                    Rigidbody bobberRB = newBobber.GetComponent<Rigidbody>();

                    Vector3 castDirection = transform.forward * _castSpeed;

                    bobberRB.velocity = new Vector3(
                        castDirection.x,
                        _bobberGravity * Time.deltaTime,
                        castDirection.z
                    );
                }
            }
        }
    }
}
