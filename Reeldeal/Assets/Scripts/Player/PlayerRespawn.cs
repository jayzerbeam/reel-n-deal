using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    private float respawnYThreshold = 47f;
    private Animator _animator;
    AudioSource _heavyGrunt;
    bool audioPlayed;

    private CharacterController characterController;
    private Vector3 initialPosition;
    private float dyingAnimCountdown;
    private float revivingAnimCountdown;

    private float timeInWater;
    private float timeThreshold = 5f;

    private float sharkAttackLimit = 5f;
    private float timeSharkAttack;

    private int _isDeadHash;

    private bool playerRespawned = false;

    public GameObject waterAlert;
    public GameObject sharkAlert;
    private GameObject waterCountdown;
    private TextMeshProUGUI countdownText;
    private TextMeshProUGUI sharkText;

    private hud_gui_controller coinInventory;
    private PlayerDrownVolume drownVolumeScript;

    public GameObject respawnGUI;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        initialPosition = transform.position;
        coinInventory = FindObjectOfType<hud_gui_controller>();
        _animator = GetComponent<Animator>();
        _isDeadHash = Animator.StringToHash("isDead");
        _heavyGrunt = GetComponents<AudioSource>()[7];

        waterCountdown = waterAlert.transform.Find("Countdown").gameObject;
        countdownText = waterCountdown.GetComponent<TextMeshProUGUI>();
        sharkText = sharkAlert.transform.Find("SharkText").GetComponent<TextMeshProUGUI>();

        drownVolumeScript = GetComponent<PlayerDrownVolume>();
    }

    private bool IsPlayerUnderwater()
    {
        return transform.position.y < respawnYThreshold;
    }

    private void Update()
    {
        if (SharkAttack() && !IsPlayerUnderwater())
        {
            timeSharkAttack += Time.deltaTime;

            if (sharkAlert != null)
            {
                sharkAlert.SetActive(true);
                sharkText.text = "You're attracting sharks!\nRun away!";
            }

            if (timeSharkAttack >= sharkAttackLimit)
            {
                sharkText.text = "You passed out...";
                _animator.SetBool(_isDeadHash, true);
                Die();
            }
        }
        else if (!SharkAttack())
        {
            sharkAlert.SetActive(false);
            timeSharkAttack = 0f;
        }

        if (IsPlayerUnderwater())
        {
            timeInWater += Time.deltaTime;

            if (waterAlert != null)
            {
                sharkAlert.SetActive(false);
                waterAlert.SetActive(true);
            }

            if (waterCountdown != null)
            {
                if (timeInWater <= timeThreshold)
                {
                    countdownText.text =
                        "Time Before Respawn: " + (timeThreshold - timeInWater).ToString("F0");
                }
            }

            if (timeInWater >= timeThreshold - 1.0f)
            {
                countdownText.text = "You passed out...";
                _animator.SetBool(_isDeadHash, true);
                Die();
            }
        }
        else if (!IsPlayerUnderwater())
        {
            waterAlert.SetActive(false);
            timeInWater = 0f;
        }

        if (playerRespawned)
        {
            const float timeToCompleteAnim = 2.2f;
            revivingAnimCountdown += Time.deltaTime;
            timeInWater = 0f;
            dyingAnimCountdown = 0f;
            timeSharkAttack = 0f;
            _animator.SetBool(_isDeadHash, false);

            if (waterAlert != null)
                waterAlert.SetActive(false);

            if (sharkAlert != null)
                sharkAlert.SetActive(false);

            if (drownVolumeScript != null)
            {
                drownVolumeScript.drowned = false;
            }

            if (revivingAnimCountdown >= timeToCompleteAnim)
            {
                revivingAnimCountdown = 0.0f;
                characterController.enabled = true;
                playerRespawned = false;
            }

            if (respawnGUI != null)
            {
                respawnGUI.SetActive(true);
                StartCoroutine(HideGUI());
            }
        }
    }

    private void Die()
    {
        const float timeToCompleteAnim = 2.3f;

        if (GameObject.FindWithTag("Bobber"))
        {
            Destroy(GameObject.FindWithTag("Bobber"));
        }

        dyingAnimCountdown += Time.deltaTime;
        characterController.enabled = false;
        playerRespawned = false;

        if (!_heavyGrunt.isPlaying && !audioPlayed)
        {
            _heavyGrunt.Play();
            audioPlayed = true;
        }

        if (drownVolumeScript != null)
        {
            drownVolumeScript.drowned = true;
        }
        if (dyingAnimCountdown >= timeToCompleteAnim)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = respawnPoint.position;
        playerRespawned = true;
        audioPlayed = false;

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

    private IEnumerator HideGUI()
    {
        yield return new WaitForSeconds(5f);
        respawnGUI.SetActive(false);
    }
}
