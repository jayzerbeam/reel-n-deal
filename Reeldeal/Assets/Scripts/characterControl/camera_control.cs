using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_control : MonoBehaviour
{


    public Transform player; // The player's transform
    public float followDistance = 3.55f; // The distance from the player
    public float followHeight = 2.28f; // The height at which the camera follows the player
    private float maxDistance = 15f; // The max distance camera can be from the player
    private float minDistance = 1.81f; // The min distance camera can be from the player
    public float obstacleCheckRadius = 0.5f; // Radius of the sphere used to check for obstacles
    public LayerMask obstacleLayers; // The layers which the camera will consider as obstacles

    private float currentDistance;


    // Start is called before the first frame update
    void Start()
    {
        currentDistance = followDistance;
    }

    // Update is called once per frame after normal Update
    void LateUpdate()
    {
        // Find the position the camera would be at when nothing is in the way
        Vector3 idealPosition = player.position + player.up * followHeight - player.forward * followDistance;

        // Check if there is an obstacle in the way
        RaycastHit hit;
        Vector3 playerToCameraDirection = (idealPosition - player.position).normalized;

        // Cast a sphere from the player's position to the desired camera position,checking for any obstacles in the way
        if (Physics.SphereCast(player.position, obstacleCheckRadius, playerToCameraDirection, out hit, followDistance, obstacleLayers)) // If an obstacle is hit
        {
            // find the distance the camera needs to be to not clip it
            currentDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            currentDistance = followDistance;
        }

        // Update camera pos
        transform.position = player.position + player.up * followHeight - player.forward * currentDistance;

        // Make the camera look at the player
        transform.LookAt(player);
    }
}