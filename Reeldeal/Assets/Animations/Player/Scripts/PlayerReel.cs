using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerReel : MonoBehaviour
{
    PlayerInput _playerInput;
    CharacterController _characterController;
    Animator _animator;
    playerInventory _inventory;

    bool _isCanceled;

    int _isCastingHash;
    bool _isCasting;

    int _isReelingHash;
    bool _isReeling;

    int pressCount = 0;

    float _reelValue = 0.0f;
    GameObject _bobber;
    Rigidbody _bobberRB;
    GameObject _caughtFish;

    [SerializeField]
    float _reelSpeed = 10f;

    public TextMeshProUGUI fishCaughtMsg;


    public TextMeshProUGUI talk_to_playerText;
    public float timeToErase = 5f;
    private string msg;

    void Awake()
    {
        _playerInput = new PlayerInput();
        _animator = GetComponent<Animator>();
        _isReelingHash = Animator.StringToHash("isReeling");
        _isCastingHash = Animator.StringToHash("isCasting");
        _characterController = GetComponent<CharacterController>();
        _inventory = new playerInventory();

        _playerInput.CharacterControls.Reel.started += OnReel;
        _playerInput.CharacterControls.Reel.canceled += OnReel;
        _playerInput.CharacterControls.Reel.performed += OnReel;

        _playerInput.CharacterControls.Cancel.started += OnCancel;
        _playerInput.CharacterControls.Cancel.canceled += OnCancel;
        _playerInput.CharacterControls.Cancel.performed += OnCancel;
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
        HandleCancel();
        HandleReel();
        HandleCatchFish();
    }

    void OnReel(InputAction.CallbackContext context)
    {
        _reelValue = context.ReadValue<float>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bobber"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Bobber"));
            if (_caughtFish)
            {
                fishCaughtMsg.enabled = false;
                Destroy(_caughtFish);
                _inventory.AddFishedFish("Alpha Fish Test Fish");
            }
        }
    }

    // TODO Preventing recast.
    void OnCancel(InputAction.CallbackContext context)
    {
        _isCanceled = context.ReadValueAsButton();
    }

    void HandleCancel()
    {
        if (_isCanceled)
        {
            Destroy(GameObject.FindGameObjectWithTag("Bobber"));
        }
    }

    void HandleAnimation()
    {
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

    public void HandleCatchFish()
    {
        _bobber = GameObject.FindWithTag("Bobber");

        if (_bobber)
        {
            _bobberRB = _bobber.GetComponent<Rigidbody>();
        }

        Rigidbody fishRB = _bobber.GetComponentInChildren<Rigidbody>();

        if (fishRB != null)
        {
            _caughtFish = fishRB.gameObject;
            fishRB.constraints = RigidbodyConstraints.None;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            pressCount += 1;
            Debug.Log(pressCount);
        }
        // Exit condition
        if (pressCount == 5)
        {
            fishCaughtMsg.enabled = false;
            Debug.Log("You caught the fish!");
            talk_to_player("You caught the fish!");
            pressCount = 0;

            Destroy(GameObject.FindGameObjectWithTag("Bobber"));
            if (_caughtFish)
            {
                Destroy(_caughtFish);
                _inventory.AddFishedFish("Alpha Fish Test Fish");
            }
        }
    }

    public void talk_to_player(string talk_to)
    {
        StartCoroutine(talk_to_playerWritethenEraseText(talk_to));
    }

    private IEnumerator talk_to_playerWritethenEraseText(string text)
    {
        talk_to_playerText.text = text;

        yield return new WaitForSeconds(timeToErase);

        talk_to_playerText.text = "";
    }

    void HandleReel()
    {
        if (GameObject.FindWithTag("Bobber"))
        {
            _bobber = GameObject.FindWithTag("Bobber");
            _bobberRB = _bobber.GetComponent<Rigidbody>();
        }

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
