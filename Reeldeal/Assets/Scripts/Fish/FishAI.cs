using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    public float idleSpeed = 0.025f;
    public float hungrySpeed = 0.5f;
    public float aggressiveSpeed = 1f;
    public float rotationSpeed = 10f;

    private Animator anim;
    private float currentSpeed;
    private Rigidbody rb;

    private GameObject[] waypoints;
    private int currWaypointIndex;

    public enum AIState
    {
        idleState,
        hungryState,
        aggressiveState
    };

    public AIState aiState;

    private void Start()
    {
        aiState = AIState.idleState;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        currentSpeed = idleSpeed;
        // get waypoints from initialization, first initial destination
        FishPrefabInitializer fishPrefabInitializer = GetComponent<FishPrefabInitializer>();
        waypoints = fishPrefabInitializer.waypoints;
        currWaypointIndex = 0;

    }

    private void Update()
    {
        switch (aiState)
        {
            case AIState.idleState: // idle - move between waypoints
                GameObject currWaypoint = waypoints[currWaypointIndex];
                
                // determine distance between points and required rotation / velocity
                Vector3 distance = currWaypoint.transform.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(distance, Vector3.up);
                float remainingDistance = Vector3.Distance(transform.position, currWaypoint.transform.position);

                // set rotation and speed
                rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, rotation, rotationSpeed * Time.deltaTime));
                float adjustedSpeed = Mathf.Lerp(0f, idleSpeed, remainingDistance / 0.5f);
                currentSpeed = Mathf.Lerp(currentSpeed, adjustedSpeed, Time.deltaTime);
                rb.velocity = this.transform.forward * currentSpeed;

                // determine if waypoint reached
                if (Vector3.Distance(rb.position, currWaypoint.transform.position) < 0.5)
                {
                    currWaypointIndex++;
                    if (currWaypointIndex >= waypoints.Length)
                    {
                        currWaypointIndex = Random.Range(0, waypoints.Length);
                    }
                }
                break;
            case AIState.hungryState:
                break;
            case AIState.aggressiveState:
                break;
            default:
                break;

        }
    }

    
}

