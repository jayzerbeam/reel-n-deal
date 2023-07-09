using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerFish : MonoBehaviour
{
    // private string msg;

    PlayerInput _playerInput;
    CharacterController _characterController;
    Animator _animator;
    GameObject _bobber;
    Rigidbody _bobberRB;
    GameObject _caughtFish;

    public GameObject bobber;

    bool _isCanceled;
    bool _isCastButtonPressed;

    int _isCastingHash;
    int _isReelingHash;

    bool _isCasting;
    bool _isReeling;

    float _reelValue = 0.0f;

    [SerializeField]
    float _reelSpeed = 10f;

    [SerializeField]
    float _castSpeed = 80f;

    [SerializeField]
    float _castHeight = 2f;

    [SerializeField]
    float _bobberGravity = -10f;

    void Awake()
    {
        _playerInput = new PlayerInput();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        _isCastingHash = Animator.StringToHash("isCasting");
        _isReelingHash = Animator.StringToHash("isReeling");

        _playerInput.CharacterControls.Cast.started += OnCast;
        _playerInput.CharacterControls.Cast.canceled += OnCast;
        _playerInput.CharacterControls.Cast.performed += OnCast;

        _playerInput.CharacterControls.Reel.started += OnReel;
        _playerInput.CharacterControls.Reel.canceled += OnReel;
        _playerInput.CharacterControls.Reel.performed += OnReel;

        _playerInput.CharacterControls.Cancel.started += OnCancel;
        _playerInput.CharacterControls.Cancel.canceled += OnCancel;
        _playerInput.CharacterControls.Cancel.performed += OnCancel;
    }

    void Update()
    {
        _isReeling = _animator.GetBool(_isReelingHash);
        _isCasting = _animator.GetBool(_isCastingHash);

        HandleAnimation();
        HandleCancel();
        FindBobber();
    }

    void FixedUpdate()
    {
        HandleCast();
        // HandleReel();
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
            Destroy(GameObject.FindGameObjectWithTag("Bobber"));
            if (_caughtFish)
            {
                Destroy(_caughtFish);
            }
        }
    }

    void OnReel(InputAction.CallbackContext context)
    {
        _reelValue = context.ReadValue<float>();
    }

    bool FindBobber()
    {
        return GameObject.FindWithTag("Bobber");
    }

    void HandleAnimation()
    {
        //Cast
        if (_isCastButtonPressed && !_isCasting && !FindBobber())
        {
            _animator.SetBool(_isCastingHash, true);
        }
        else if (!_isCastButtonPressed && _isCasting)
        {
            _animator.SetBool(_isCastingHash, false);
        }
        // Reel
        if (!_isReeling && _reelValue > 0)
        {
            _animator.SetBool(_isReelingHash, true);
        }
        else if (_isReeling && _reelValue <= 0 || _isCanceled)
        {
            _animator.SetBool(_isReelingHash, false);
            _animator.SetBool(_isCastingHash, false);
        }
    }

    void HandleCancel()
    {
        if (_isCanceled)
        {
            Destroy(GameObject.FindGameObjectWithTag("Bobber"));
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

    void HandleReel()
    {
        if (GameObject.FindWithTag("Bobber"))
        {
            _bobber = GameObject.FindWithTag("Bobber");
            _bobberRB = _bobber.GetComponent<Rigidbody>();
        }

        // SHOULD PROBABLY USE FORCEMODE.ACCELERATION
        if (_reelValue > 0.0f && _bobber && !_isCasting)
        {
            // Make sure the frozen bobber can move
            _bobberRB.constraints = RigidbodyConstraints.None;
            Vector3 reelDirection = -transform.forward * _reelSpeed;

            _bobberRB.velocity = new Vector3(
                reelDirection.x,
                -5f * Time.deltaTime,
                reelDirection.z
            );

            Rigidbody fishRB = _bobber.GetComponentInChildren<Rigidbody>();

            if (fishRB != null)
            {
                _caughtFish = fishRB.gameObject;
                fishRB.constraints = RigidbodyConstraints.None;
                fishRB.velocity = new Vector3(
                    reelDirection.x,
                    -5f * Time.deltaTime,
                    reelDirection.z
                );
            }
        }
        else if (_reelValue <= 0.0f && _bobber && !_isCasting)
        {
            // Not reeling - Bobber must stop in place
            _bobberRB.constraints = RigidbodyConstraints.FreezePosition;
        }
    }
}
