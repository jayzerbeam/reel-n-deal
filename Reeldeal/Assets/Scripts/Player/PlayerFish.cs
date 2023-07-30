using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerFish : MonoBehaviour
{
    public PlayerInput playerInput;
    FishingMessaging _messaging;
    GameObject _player;
    playerInventory _playerInventory;
    CharacterController _characterController;
    Animator _animator;
    FishCatching _fishCatching;
    AudioSource _rodReel;
    GameObject _bobber;
    Rigidbody _bobberRB;
    GameObject _hookedFishGO;

    public GameObject bobberPrefab;
    public GameObject newBobberPrefab;

    public bool _doBobberSwap;

    bool _isCastingAnim;
    bool _isFishingAnim;
    bool _isCanceled;
    bool _isCastButtonPressed;

    int _isCastingHash;
    int _isFishingHash;

    float _castCountdown = 2.0f;

    [SerializeField]
    float _castSpeed = 20f;

    [SerializeField]
    float _castHeight = 2.5f;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        _player = GameObject.FindWithTag("Player");
        _playerInventory = _player.GetComponent<playerInventory>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _rodReel = _player.GetComponents<AudioSource>()[2];
        _messaging = _player.GetComponent<FishingMessaging>();
        _isCastingHash = Animator.StringToHash("isCasting");
        _isFishingHash = Animator.StringToHash("isFishing");
    }

    void Start()
    {
        playerInput.actions["Cast"].started += OnCast;
        playerInput.actions["Cast"].canceled += OnCast;
        playerInput.actions["Cast"].performed += OnCast;
        playerInput.actions["Cancel"].started += OnCancel;
        playerInput.actions["Cancel"].canceled += OnCancel;
        playerInput.actions["Cancel"].performed += OnCancel;
    }

    void Update()
    {
        HandleAnimation();
        HandleCancel();

        if (_doBobberSwap)
        {
            SwapBobbers(newBobberPrefab);
        }
    }

    void FixedUpdate()
    {
        HandleCast();
    }

    void OnCast(InputAction.CallbackContext context)
    {
        _isCastButtonPressed = context.ReadValueAsButton();
    }

    void OnCancel(InputAction.CallbackContext context)
    {
        _isCanceled = context.ReadValueAsButton();
    }

    void HandleAnimation()
    {
        _isCastingAnim = _animator.GetBool(_isCastingHash);
        _isFishingAnim = _animator.GetBool(_isFishingHash);

        if (_isCastButtonPressed && !_isCastingAnim && !_isFishingAnim)
        {
            _animator.SetBool(_isCastingHash, true);
            _animator.SetBool(_isFishingHash, true);
            _rodReel.Play();
        }
        if (!_isCastButtonPressed && _isCastingAnim)
        {
            _animator.SetBool(_isCastingHash, false);
        }
    }

    void HandleCast()
    {
        bool doesBobberExist = GameObject.FindWithTag("Bobber");

        if (_isCastButtonPressed && !doesBobberExist && _characterController.isGrounded)
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

    void HandleCancel()
    {
        bool doesBobberExist = GameObject.FindWithTag("Bobber");
        if (_isCanceled)
        {
            _messaging.StopMessage();
            _animator.SetBool(_isCastingHash, false);
            _animator.SetBool(_isFishingHash, false);
            if (_fishCatching.GetHookedFishGO())
            {
                _fishCatching.ReleaseFish();
            }
            if (doesBobberExist)
            {
                Destroy(GameObject.FindWithTag("Bobber"));
            }
        }
    }

    public void SwapBobbers(GameObject newBobberPrefab)
    {
        bobberPrefab = newBobberPrefab;
        Debug.Log("Bobber after swap is: " + bobberPrefab.ToString());
    }
}
