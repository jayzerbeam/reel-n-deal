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

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        initialPosition = transform.position;
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
        }

        else
        {
            timeInWater = 0f;
            isDyingTriggered = false;
        }
    
    }

    public void Respawn()
    {
        characterController.enabled = false;
        transform.position = respawnPoint.position;
        characterController.enabled = true;
    }
}