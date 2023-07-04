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

    int _isCastingHash;
    bool _isCasting;

    int _isReelingHash;
    float _reelValue = 0.0f;
    GameObject _bobber;
    GameObject _caughtFish;

    [SerializeField]
    float _reelSpeed = 2f;

    void Awake()
    {
        _playerInput = new PlayerInput();
        _animator = GetComponent<Animator>();
        _isReelingHash = Animator.StringToHash("isReeling");
        _characterController = GetComponent<CharacterController>();

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
        }
    }

    // Idle reel animation should be attached
    // void HandleAnimation() { }

    void HandleReel()
    {
        // Player must only be able to reel if bobber exists and if _isCasting is false
        _bobber = GameObject.FindWithTag("Bobber");

        if (_reelValue > 0.7f && _bobber) // && !_isCasting
        {
            {
                Rigidbody bobberRB = _bobber.GetComponent<Rigidbody>();

                if (bobberRB != null)
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
        }
    }
}
