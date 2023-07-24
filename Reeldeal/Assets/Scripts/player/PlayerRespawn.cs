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

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        initialPosition = transform.position;

        waterCountdown = waterAlert.transform.Find("Countdown").gameObject;
        countdownText = waterCountdown.GetComponent<TextMeshProUGUI>();

        if (waterAlert != null)
            waterAlert.SetActive(false);
    }

    private void Update()
    {
        if (transform.position.y < respawnYThreshold)
        {


            Debug.Log("Player is underwater!");
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
                Debug.Log("Player exceeds underwater time!");
                Respawn();
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
    }

}