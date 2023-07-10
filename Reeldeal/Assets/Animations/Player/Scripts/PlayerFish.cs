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
    int _isFishingHash;

    bool _isCastingAnim;
    bool _isFishingAnim;

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
        _isFishingHash = Animator.StringToHash("isFishing");

        // TODO prune these (if possible)
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
        _isCastingAnim = _animator.GetBool(_isCastingHash);
        _isFishingAnim = _animator.GetBool(_isFishingHash);

        //Cast
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
            // TODO WHY ISN'T THIS WORKING?
            _animator.SetBool(_isFishingHash, false);
        }
    }

    // TODO this should not cancel when the player is casting; only when "fishing"
    // Could allow for early animation ending
    void HandleCancel()
    {
        if (_isCanceled)
        {
            Destroy(GameObject.FindGameObjectWithTag("Bobber"));
        }
    }

    void HandleCast()
    {
        if (_isCastButtonPressed && !FindBobber() && _characterController.isGrounded)
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
        if (FindBobber())
        {
            _bobber = GameObject.FindWithTag("Bobber");
            _bobberRB = _bobber.GetComponent<Rigidbody>();
        }

        // SHOULD PROBABLY USE FORCEMODE.ACCELERATION
        if (_reelValue > 0.0f && _bobber && !_isCastingAnim)
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
        else if (_reelValue <= 0.0f && _bobber && !_isCastingAnim)
        {
            // Not reeling - Bobber must stop in place
            _bobberRB.constraints = RigidbodyConstraints.FreezePosition;
        }
    }
}
