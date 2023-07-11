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
    GameObject _player;
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

    float _reelForce = 0.0f;

    [SerializeField]
    float _castSpeed = 20f;

    [SerializeField]
    float _castHeight = 2.5f;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player");
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
        HandleReel();
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
        _reelForce = context.ReadValue<float>();
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
            _animator.SetBool(_isFishingHash, false);
        }
    }

    float DistanceToPlayer()
    {
        return (_player.transform.position - _bobber.transform.position).magnitude;
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
            _bobber = Instantiate(
                bobber,
                this.transform.position + new Vector3(0f, _castHeight, 0f),
                this.transform.rotation
            );

            if (_bobber != null)
            {
                _bobberRB = _bobber.GetComponent<Rigidbody>();
                Vector3 castDirection = transform.forward;
                Vector3 castVelocity = castDirection * _castSpeed;
                _bobberRB.AddForce(castVelocity, ForceMode.Impulse);
            }
        }
    }

    void HandleReel()
    {
        Rigidbody hookedFishRB = null;

        if (FindBobber())
        {
            _bobber = GameObject.FindWithTag("Bobber");
            _bobberRB = _bobber.GetComponent<Rigidbody>();
            _bobberRB.sleepThreshold = 1f;
            hookedFishRB = _bobber.GetComponentInChildren<Rigidbody>();
        }

        if (_reelForce > 0.0f && _bobber && !_isCastingAnim)
        {
            float reelSpeed = 20.0f;
            float minReelSpeed = 1.0f;
            float slowdownDistance = 4.0f;
            float retrieveDistance = 1.0f;

            _bobberRB.constraints = RigidbodyConstraints.None;
            // Largely prevents the bobber from rolling away
            _bobberRB.constraints = RigidbodyConstraints.FreezeRotation;

            Vector3 playerPosition = this.transform.position;
            Vector3 reelDirection = playerPosition - _bobber.transform.position;
            reelDirection.Normalize();

            if (DistanceToPlayer() > slowdownDistance)
            {
                _bobberRB.AddForce(reelDirection * reelSpeed, ForceMode.Force);
            }
            else if (
                DistanceToPlayer() <= slowdownDistance && DistanceToPlayer() > retrieveDistance
            )
            {
                _bobberRB.AddForce(reelDirection * minReelSpeed, ForceMode.Impulse);
            }
            else if (DistanceToPlayer() <= retrieveDistance)
            {
                // TODO account for attached fish
                Destroy(GameObject.FindGameObjectWithTag("Bobber"));
            }
        }
    }
}
