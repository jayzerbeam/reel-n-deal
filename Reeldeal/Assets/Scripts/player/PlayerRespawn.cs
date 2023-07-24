using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint; // Assign the respawn point GameObject in the Inspector
    public float respawnYThreshold = 40f; // Respawn if player falls below -10 Y value

    private CharacterController characterController;
    private Vector3 initialPosition;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Check if the player falls below the respawn Y threshold
        if (transform.position.y < respawnYThreshold)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        // Move the player's position to the respawn point using CharacterController
        characterController.enabled = false;
        transform.position = respawnPoint.position;
        characterController.enabled = true;

        // Reset any other player states or variables as needed
    }
}