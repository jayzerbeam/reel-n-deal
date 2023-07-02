using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class villagerMovement : MonoBehaviour
{
    public GameObject village;
    public float rangeFromVillage = 10f;
    public float avoidWater = 5f;
    public LayerMask waterLayer;
    public LayerMask groundLayer;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToNewDestination();
    }

    // Update is called once per frame
    private void Update()
    {
        // If the villager has reached its destination, move to a new destination
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            MoveToNewDestination();
        }
    }

    private void MoveToNewDestination()
    {
        Vector3 destination;

        // Keep trying to find a new destination until we find one that's not over water
        while (true)
        {
            // Choose a random point within range of the village
            destination = village.transform.position + Random.insideUnitSphere * rangeFromVillage;
            destination.y = transform.position.y;

            // Cast a ray downwards from the destination
            if (Physics.Raycast(destination + Vector3.up * 1000f, Vector3.down, out RaycastHit hit, Mathf.Infinity, waterLayer | groundLayer))
            {
                // If the ray hit water before it hit the ground, choose a new destination
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Water"))
                {
                    continue;
                }
            }

            break;
        }

        agent.SetDestination(destination);
    }
}
