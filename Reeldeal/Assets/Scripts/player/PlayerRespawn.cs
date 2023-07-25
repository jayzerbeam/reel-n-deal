 using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint; 
    private float respawnYThreshold = 47f;

    private CharacterController characterController;
    private Vector3 initialPosition;
    private float timeInWater;
    private float timeThreshold = 5f;
    private bool isDyingTriggered = false;
    public GameObject waterAlert;
    private GameObject waterCountdown;
    private TextMeshProUGUI countdownText;

    private hud_gui_controller coinInventory;

    private PlayerDrownVolume drownVolumeScript;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        initialPosition = transform.position;
        coinInventory = FindObjectOfType<hud_gui_controller>();

        waterCountdown = waterAlert.transform.Find("Countdown").gameObject;
        countdownText = waterCountdown.GetComponent<TextMeshProUGUI>();

        if (waterAlert != null)
            waterAlert.SetActive(false);


        drownVolumeScript = GetComponent<PlayerDrownVolume>();
    }

    private void Update()
    {
        if (transform.position.y < respawnYThreshold)
        {

            timeInWater += Time.deltaTime;


            if (waterAlert != null)
                waterAlert.SetActive(true);

            if (waterCountdown != null)
            {
                countdownText.text = "Time Before Respawn: " + (timeThreshold - timeInWater).ToString("F0");
            }


            if (timeInWater >= timeThreshold && !isDyingTriggered)
            {
                isDyingTriggered = true;
                Respawn();
            }

            if (drownVolumeScript != null)
            {
                drownVolumeScript.drowned = true;
            }


        }

        else
        {
            timeInWater = 0f;
            isDyingTriggered = false;

            if (waterAlert != null)
                waterAlert.SetActive(false);
        }
    
    }

    public void Respawn()
    {
        characterController.enabled = false;
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

}