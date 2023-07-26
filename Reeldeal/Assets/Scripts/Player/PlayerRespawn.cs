using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    private float respawnYThreshold = 47f;
    private Animator _animator;
    private Rigidbody _rb;

    private CharacterController characterController;
    private Vector3 initialPosition;
    private float dyingAnimCountdown;
    private float timeInWater;
    private float timeThreshold = 5f;
    private float sharkAttackLimit = 3f;
    private float timeSharkAttack;
    private bool isSharkAttack = false;
    private int _isDeadHash;
    private bool _isDeadAnim;
    private bool _isDead = false;

    public GameObject waterAlert;
    public GameObject sharkAlert; 
    private GameObject waterCountdown;
    private TextMeshProUGUI countdownText;

    private hud_gui_controller coinInventory;
    private PlayerDrownVolume drownVolumeScript;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        initialPosition = transform.position;
        coinInventory = FindObjectOfType<hud_gui_controller>();
        _animator = GetComponent<Animator>();
        _isDeadHash = Animator.StringToHash("isDead");

        waterCountdown = waterAlert.transform.Find("Countdown").gameObject;
        countdownText = waterCountdown.GetComponent<TextMeshProUGUI>();

        if (waterAlert != null)
            waterAlert.SetActive(false);

        if (sharkAlert != null)
            sharkAlert.SetActive(false);

        drownVolumeScript = GetComponent<PlayerDrownVolume>();
    }

    private void Update()
    {
        _isDeadAnim = _animator.GetBool(_isDeadHash);


        if (SharkAttack())
        {

            timeSharkAttack += Time.deltaTime;

            if (sharkAlert != null)
                sharkAlert.SetActive(true);

            if (timeSharkAttack >= sharkAttackLimit && !isSharkAttack)
            {
                isSharkAttack = true;
                KillThePlayer();
            }
        }

        else
        {
            timeSharkAttack = 0f;
            isSharkAttack = false;
            dyingAnimCountdown = 0f;
            _animator.SetBool(_isDeadHash, false);

            if (sharkAlert != null)
                sharkAlert.SetActive(false);
        }



        if (transform.position.y < respawnYThreshold)
        {
            timeInWater += Time.deltaTime;

            if (waterAlert != null)
                waterAlert.SetActive(true);

            if (waterCountdown != null)
            {
                if (timeInWater <= timeThreshold)
                {
                    countdownText.text =
                        "Time Before Respawn: " + (timeThreshold - timeInWater).ToString("F0");
                }
                else
                {
                    countdownText.text = "You died...";
                }
            }

            if (timeInWater >= timeThreshold)
            {
                KillThePlayer();
            }

            if (drownVolumeScript != null)
            {
                drownVolumeScript.drowned = true;
            }
        }
        // Not underwater
        else
        {
            timeInWater = 0f;
            dyingAnimCountdown = 0f;
            _animator.SetBool(_isDeadHash, false);

            if (waterAlert != null)
                waterAlert.SetActive(false);

            if (drownVolumeScript != null)
            {
                drownVolumeScript.drowned = false;
            }
        }
    }

    private void KillThePlayer()
    {
        _animator.SetBool(_isDeadHash, true);
        dyingAnimCountdown += Time.deltaTime;
        characterController.enabled = false;

        if (dyingAnimCountdown >= 2.4f)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        transform.position = respawnPoint.position;
        characterController.enabled = true;

        if (coinInventory != null)
        {
            coinInventory.RespawnLoseCoins();
        }
        Debug.Log("Coins after respawn: " + coinInventory.coin_Count);

        if (drownVolumeScript != null)
        {
            drownVolumeScript.drowned = false;
        }
    }

    private bool SharkAttack()
    {
        SharkIdentifier[] sharks = FindObjectsOfType<SharkIdentifier>();
        foreach (SharkIdentifier shark in sharks)
        {
            FishAI fishAI = shark.GetComponent<FishAI>();
            if (fishAI != null && fishAI.aiState == FishAI.AIState.aggressiveState)
            {
                Debug.Log("Shark attack");
                return true;
            }
        }
        return false;
    }
}

