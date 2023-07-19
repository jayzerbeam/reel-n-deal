using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerFish : MonoBehaviour
{
    PlayerInput _playerInput;
    GameObject _player;
    playerInventory _playerInventory;
    CharacterController _characterController;
    Animator _animator;
    FishCatching _fishCatching;

    GameObject _bobber;
    Rigidbody _bobberRB;

    GameObject _hookedFishGO;

    public GameObject bobberPrefab;

    bool _isCanceled;
    bool _isCastButtonPressed;

    int _isCastingHash;
    int _isFishingHash;

    bool _isCastingAnim;
    bool _isFishingAnim;

    [SerializeField]
    float _castCountdown = 2.0f;

    [SerializeField]
    float _castSpeed = 20f;

    [SerializeField]
    float _castHeight = 2.5f;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _playerInput = new PlayerInput();
        _playerInventory = _player.GetComponent<playerInventory>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        _isCastingHash = Animator.StringToHash("isCasting");
        _isFishingHash = Animator.StringToHash("isFishing");

        _playerInput.CharacterControls.Cast.started += OnCast;
        _playerInput.CharacterControls.Cast.canceled += OnCast;
        _playerInput.CharacterControls.Cast.performed += OnCast;

        // _playerInput.CharacterControls.Reel.started += OnReel;
        // _playerInput.CharacterControls.Reel.canceled += OnReel;
        // _playerInput.CharacterControls.Reel.performed += OnReel;

        _playerInput.CharacterControls.Cancel.started += OnCancel;
        _playerInput.CharacterControls.Cancel.canceled += OnCancel;
        _playerInput.CharacterControls.Cancel.performed += OnCancel;
    }

    void Update()
    {
        HandleAnimation();
        HandleCancel();
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

    void OnCancel(InputAction.CallbackContext context)
    {
        _isCanceled = context.ReadValueAsButton();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bobber"))
        {
            Destroy(GameObject.FindWithTag("Bobber"));
        }
    }

    bool FindBobber()
    {
        return GameObject.FindWithTag("Bobber");
    }

    void HandleAnimation()
    {
        _isCastingAnim = _animator.GetBool(_isCastingHash);
        _isFishingAnim = _animator.GetBool(_isFishingHash);

        if (_isCastButtonPressed && !_isCastingAnim && !FindBobber())
        {
            _animator.SetBool(_isCastingHash, true);
            _animator.SetBool(_isFishingHash, true);
        }
        if (!_isCastButtonPressed && _isCastingAnim)
        {
            _animator.SetBool(_isCastingHash, false);
        }
        if (_isFishingAnim && !FindBobber())
        {
            _animator.SetBool(_isFishingHash, false);
        }
    }

    float DistanceToPlayer()
    {
        return (_player.transform.position - _bobber.transform.position).magnitude;
    }

    void HandleCancel()
    {
        if (_isCanceled)
        {
            Destroy(GameObject.FindWithTag("Bobber"));
            _animator.SetBool(_isCastingHash, false);
            _animator.SetBool(_isFishingHash, false);
        }
    }

    // TODO add variable cast force on hold.
    void HandleCast()
    {
        if (_isCastButtonPressed && !FindBobber() && _characterController.isGrounded)
        {
            _castCountdown = 2.0f;
            _bobber = Instantiate(
                bobberPrefab,
                this.transform.position + new Vector3(0f, _castHeight, 0f),
                this.transform.rotation
            );

            _fishCatching = _bobber.GetComponent<FishCatching>();

            if (_bobber != null)
            {
                _bobberRB = _bobber.GetComponent<Rigidbody>();
                Vector3 castDirection = transform.forward;
                Vector3 castVelocity = castDirection * _castSpeed;
                _bobberRB.AddForce(castVelocity, ForceMode.Impulse);
            }
        }
    }
}
