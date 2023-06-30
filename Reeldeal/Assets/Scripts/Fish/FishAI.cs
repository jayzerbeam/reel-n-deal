using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


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
                // check for state changes first

                // set to "hungry" if bobber is nearby
                GameObject bobber = GameObject.FindWithTag("Bobber");

                if (bobber != null)
                {
                    // get bobber info
                    Vector3.Distance(transform.position, bobber.transform.position);
                    BobberPrefabInitializer bobberInitializer = bobber.GetComponent<BobberPrefabInitializer>();
                    List<BobberPrefabInitializer.AttractiveBobberInfo> bobberInfo = bobberInitializer.attractiveBobberInfo;

                    // get fish info
                    FishPrefabInitializer fishPrefabInitializer = gameObject.GetComponent<FishPrefabInitializer>();
                    GameObject fishPrefab = fishPrefabInitializer.gameObject; // get prefab

                    // match fish prefab with the one in the attractive bobber class, get the respective radius
                    // https://stackoverflow.com/questions/1024559/when-to-use-first-and-when-to-use-firstordefault-with-linq
                    string fishPrefabName = fishPrefab.name.Replace("(Clone)", ""); // remove "Clone" from name
                    BobberPrefabInitializer.AttractiveBobberInfo bobberMechanics = bobberInfo.FirstOrDefault(x => x.fishPrefab.name == fishPrefabName);
                    if (bobberMechanics != null)
                    {
                        float bobberDistance = Vector3.Distance(transform.position, bobber.transform.position);

                        if (bobberDistance < bobberMechanics.radius)
                        {
                            aiState = AIState.hungryState;
                        }
                    }
                }

                UpdateTravel(waypoints[currWaypointIndex]);

                // determine if waypoint reached
                if (Vector3.Distance(rb.position, waypoints[currWaypointIndex].transform.position) < 0.5)
                {
                    currWaypointIndex++;
                    if (currWaypointIndex >= waypoints.Length)
                    {
                        currWaypointIndex = Random.Range(0, waypoints.Length);
                    }
                }

                break;

            case AIState.hungryState:
                bobber = GameObject.FindWithTag("Bobber");

                if (bobber == null)
                {
                    aiState = AIState.idleState;
                }

                UpdateTravel(bobber);

                break;

            case AIState.aggressiveState:
                break;
            default:
                break;

        }
    }

    private void UpdateTravel(GameObject destination)
    {
        // determine distance between points and required rotation / velocity
        Vector3 distance = destination.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(distance, Vector3.up);
        float remainingDistance = Vector3.Distance(transform.position, destination.transform.position);

        // set rotation and speed
        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, rotation, rotationSpeed * Time.deltaTime));
        float adjustedSpeed = Mathf.Lerp(0f, idleSpeed, remainingDistance / 0.5f);
        currentSpeed = Mathf.Lerp(currentSpeed, adjustedSpeed, Time.deltaTime);
        rb.velocity = this.transform.forward * currentSpeed;
    }

}