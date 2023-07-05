using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerReel : MonoBehaviour
{
    PlayerInput _playerInput;
    CharacterController _characterController;
    Animator _animator;
    Inventory _inventory;

    int _isCastingHash;
    bool _isCasting;

    int _isReelingHash;
    bool _isReeling;

    float _reelValue = 0.0f;
    GameObject _bobber;
    GameObject _caughtFish;

    [SerializeField]
    float _reelSpeed = 10f;

    void Awake()
    {
        _playerInput = new PlayerInput();
        _animator = GetComponent<Animator>();
        _isReelingHash = Animator.StringToHash("isReeling");
        _isCastingHash = Animator.StringToHash("isCasting");
        _characterController = GetComponent<CharacterController>();
        _inventory = new Inventory();

        _playerInput.CharacterControls.Reel.started += OnReel;
        _playerInput.CharacterControls.Reel.canceled += OnReel;
        _playerInput.CharacterControls.Reel.performed += OnReel;
    }

    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    void Update()
    {
        _isReeling = _animator.GetBool(_isReelingHash);
        _isCasting = _animator.GetBool(_isCastingHash);
        HandleAnimation();
        HandleReel();
    }

    void OnReel(InputAction.CallbackContext context)
    {
        _reelValue = context.ReadValue<float>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bobber"))
        {
            Destroy(_bobber);
            Destroy(_caughtFish);
            _inventory.AddItem("Fish", true);
        }
    }

    void HandleAnimation()
    {
        if (!_isReeling && _reelValue > 0)
        {
            _animator.SetBool(_isReelingHash, true);
        }
        else if (_isReeling && _reelValue <= 0)
        {
            _animator.SetBool(_isReelingHash, false);
        }
    }

    void HandleReel()
    {
        Debug.Log(_isCasting);

        _bobber = GameObject.FindWithTag("Bobber");
        Rigidbody bobberRB = _bobber.GetComponent<Rigidbody>();

        if (bobberRB != null)
        {
            if (_reelValue > 0.0f && _bobber && !_isCasting)
            {
                {
                    // Make sure the frozen bobber can move
                    bobberRB.constraints = RigidbodyConstraints.None;
                    Vector3 reelDirection = -transform.forward * _reelSpeed;

                    bobberRB.velocity = new Vector3(
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
            }
            else if (_reelValue <= 0.0f && _bobber && !_isCasting)
            {
                // Not reeling - Bobber must stop in place
                bobberRB.constraints = RigidbodyConstraints.FreezePosition;
            }
        }
    }
}
