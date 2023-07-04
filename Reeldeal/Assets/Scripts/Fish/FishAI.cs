using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class FishAI : MonoBehaviour
{
    // speeds
    public float idleSpeed = 0.5f;
    public float hungrySpeed = 1f;
    public float aggressiveSpeed = 2f;
    public float rotationSpeed = 10f;
    public float fleeSpeed = 4f;

    // fish "sight"
    public int rayCount = 5;
    public float rayLength = 40f;
    public float sightHeight = 2f; // how high to make fish "see"
    public Color rayColor = Color.red;

    // fish properties
    private FishMultiTag fishMultiTag;
    private Animator anim;
    private Rigidbody rb;
    private float currentSpeed;

    // waypoints / positioning
    private GameObject[] waypoints;
    private int currWaypointIndex;
    private float yValue;

    // bobber info
    private BobberPrefabInitializer.AttractiveBobberInfo bobberMechanics;
    private float bobberDistance;

    // state info
    public enum AIState
    {
        idleState,
        hungryState,
        aggressiveState,
        fleeState
    };

    private bool fleeing = false;
    public float fleeTimer = 0f;
    Quaternion targetRotation = Quaternion.identity;

    public AIState aiState;

    private void Start()
    {
        aiState = AIState.idleState;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        fishMultiTag = GetComponent<FishMultiTag>();

        yValue = transform.position.y;
        currentSpeed = idleSpeed; // default speed
        
        // get waypoints from initialization, first initial destination
        FishPrefabInitializer fishPrefabInitializer = GetComponent<FishPrefabInitializer>();
        waypoints = fishPrefabInitializer.waypoints;
        currWaypointIndex = 0;
    }

    private void Update()
    {
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
            bobberMechanics = bobberInfo.FirstOrDefault(x => x.fishPrefab.name == fishPrefabName);
        }

        switch (aiState)
        {
            case AIState.idleState: // idle - move between waypoints
                if (bobber != null)
                {
                    if (bobberMechanics != null)
                    {
                        bobberDistance = Vector3.Distance(transform.position, bobber.transform.position);

                        if (bobberDistance < bobberMechanics.radius)
                        {
                            aiState = AIState.hungryState;
                        }
                    }
                }

                UpdateTravel(waypoints[currWaypointIndex].transform.position, idleSpeed);

                // determine if waypoint reached
                if (Vector3.Distance(rb.position, waypoints[currWaypointIndex].transform.position) < 0.5)
                {
                    currWaypointIndex++;
                    if (currWaypointIndex >= waypoints.Length)
                    {
                        currWaypointIndex = Random.Range(0, waypoints.Length);
                    }
                }

                // set rays facing fish's direction
                float spacing = sightHeight;
                for (int i = 0; i < rayCount; i++)
                {
                    Vector3 position = transform.position + transform.up * (i * spacing);
                    Ray ray = new Ray(position, transform.forward);
                    Debug.DrawRay(ray.origin, ray.direction * rayLength, rayColor);
                    RaycastHit[] hits = Physics.RaycastAll(ray, rayLength);
                    foreach (RaycastHit hit in hits)
                    {
                        if (hit.collider.CompareTag("Player") && fishMultiTag.HasTag("Fleeable"))
                        {
                            Debug.Log("Enter Flee");
                            aiState = AIState.fleeState;
                        }
                        else if (hit.collider.CompareTag("Player") && fishMultiTag.HasTag("Agressive"))
                        {
                            Debug.Log("Enter Agressive");
                            aiState = AIState.aggressiveState;
                        }
                    }
                }

                break;

            case AIState.hungryState:
                bobber = GameObject.FindWithTag("Bobber");

                if (bobber == null) // if bobber gets deleted
                {
                    aiState = AIState.idleState;
                }

                if (bobber != null)
                {
                    bobberDistance = Vector3.Distance(transform.position, bobber.transform.position);
                    if (bobberDistance > bobberMechanics.radius) // if bobber is too far away
                    {
                        aiState = AIState.idleState;
                    }
                }

                if (bobber != null)
                {
                    UpdateTravel(bobber.transform.position, idleSpeed);
                }

                break;

            case AIState.aggressiveState:
                
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    UpdateTravel(player.transform.position, aggressiveSpeed);
                }
                else
                {
                    aiState = AIState.idleState;
                }

                break;

            case AIState.fleeState:
                if (!fleeing) // initialize fleeing if on first enter
                {
                    fleeing = true;
                    fleeTimer = 0f;
                    targetRotation = Quaternion.LookRotation(-transform.forward, Vector3.up); // backward direction
                    rb.velocity = Vector3.zero;
                }

                fleeTimer += Time.deltaTime;
                if (fleeTimer >= 5f) // stay in flee stay 5 seconds to swim away
                {
                    fleeing = false;
                    aiState = AIState.idleState;
                }
                else
                {
                    Quaternion newRotation = Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * 20 * Time.deltaTime); // rotate faster than normal rotation speed
                    rb.MoveRotation(newRotation); // update rotation
                    rb.velocity = transform.forward * fleeSpeed;

                    // clamp y value
                    Vector3 newPosition = transform.position;
                    newPosition = new Vector3(newPosition.x, yValue, newPosition.z);
                    transform.position = newPosition;
                }
                break;


            default:
                break;

        }
    }

    private void UpdateTravel(Vector3 destination, float fishSpeed)
    {
        // determine distance between points and required rotation / velocity
        Vector3 distance = destination - transform.position;
        Quaternion rotation = Quaternion.LookRotation(distance, Vector3.up);
        float remainingDistance = Vector3.Distance(transform.position, destination);

        // set rotation and speed
        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, Quaternion.Euler(0f, rotation.eulerAngles.y, 0f), rotationSpeed * Time.deltaTime)); // only update y rotation (spinning)
        float adjustedSpeed = Mathf.Lerp(0f, fishSpeed, remainingDistance / 0.5f);
        currentSpeed = Mathf.Lerp(currentSpeed, adjustedSpeed, Time.deltaTime);
        rb.velocity = this.transform.forward * currentSpeed;

        // keep at correct y-level
        Vector3 newPosition = transform.position;
        newPosition = new Vector3(newPosition.x, yValue, newPosition.z);
        transform.position = newPosition;
    }

}