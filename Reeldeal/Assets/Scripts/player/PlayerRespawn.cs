using System;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint; 
    private float respawnYThreshold = 47f;

    private CharacterController characterController;
    private Vector3 initialPosition;
    private float timeInWater;
    private float timeThreshold = 3f;
    private bool isDyingTriggered = false;
    public GameObject waterAlert;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        initialPosition = transform.position;

        if (waterAlert != null)
            waterAlert.SetActive(false);
    }

    private void Update()
    {
        if (transform.position.y < respawnYThreshold)
        {


            Debug.Log("Player is underwater!");
            timeInWater += Time.deltaTime;

            if (timeInWater >= timeThreshold && !isDyingTriggered)
            {
                isDyingTriggered = true;
                Debug.Log("Player exceeds underwater time!");
                Respawn();
            }

            if (waterAlert != null)
                waterAlert.SetActive(true);
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