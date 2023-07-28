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
    public float landRayLength = 10f; // how close to land fish can go
    public float sightHeight = 2f; // how high to make fish "see"
    public Color rayColor = Color.red;

    // fish properties
    private FishMultiTag fishMultiTag;
    private Animator anim;
    private Rigidbody rb;
    private float currentSpeed;
    private float lastNearLand; // time since ray last collided with land
    public bool landCollision = false;
    private bool _wasRecentlyCaught;
    public bool WasRecentlyCaught
    {
        get { return _wasRecentlyCaught; }
        set { _wasRecentlyCaught = value; }
    }

    // waypoints / positioning
    private GameObject[] waypoints;
    public int currWaypointIndex;
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
        if (fishMultiTag.HasTag("river_f_1") || fishMultiTag.HasTag("river_f_2") || fishMultiTag.HasTag("river_f_3") || fishMultiTag.HasTag("river_f_4"))
        {
            rotationSpeed = 40f;
            rayLength = 20f;
        }

        yValue = transform.position.y;
        currentSpeed = idleSpeed; // default speed
        
        // get waypoints from initialization, first initial destination
        FishPrefabInitializer fishPrefabInitializer = GetComponent<FishPrefabInitializer>();
        waypoints = fishPrefabInitializer.waypoints;
        currWaypointIndex = 0;
        lastNearLand = Time.time;
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

                // flee / aggro rays
                float spacing = sightHeight;
                for (int i = 0; i < rayCount; i++)
                {
                    Vector3 position = transform.position + transform.up * (i * spacing);
                    Ray ray = new Ray(position, transform.forward);
                    //Debug.DrawRay(ray.origin, ray.direction * rayLength, rayColor);
                    RaycastHit[] hits = Physics.RaycastAll(ray, rayLength);
                    foreach (RaycastHit hit in hits)
                    {
                        if (hit.collider.CompareTag("Player") && fishMultiTag.HasTag("Fleeable"))
                        {
                            aiState = AIState.fleeState;
                        }
                        else if (hit.collider.CompareTag("Player") && fishMultiTag.HasTag("Agressive"))
                        {
                            aiState = AIState.aggressiveState;
                        }
                    }
                }

                // land collision ray
                Ray landRay = new Ray(transform.position, transform.forward);
                //Debug.DrawRay(landRay.origin, landRay.direction * landRayLength, Color.green);
                RaycastHit[] landHits = Physics.RaycastAll(landRay, landRayLength);
                foreach (RaycastHit hit in landHits)
                {
                    if (hit.collider.CompareTag("Ground") && Time.time - lastNearLand > 5f) // change to new waypoint if it spawned partially in land
                    {
                        currWaypointIndex = Random.Range(0, waypoints.Length);
                        lastNearLand = Time.time;
                    }
                }

                // also be agressive is player is simply too close (sharks only)
                GameObject playerObject = GameObject.FindWithTag("Player");
                if (playerObject != null)
                {
                    float playerDistance = Vector3.Distance(transform.position, playerObject.transform.position);
                    if (playerDistance < 16 && fishMultiTag.HasTag("Agressive"))
                    {
                        StartCoroutine(PreAggroTurn(playerObject)); // smooth turn towards player before Ai state
                    }
                }

                break;

            case AIState.hungryState:
                bobber = GameObject.FindWithTag("Bobber");
                rotationSpeed = 80; // makes it easier for fish to turn towards bobber when nearby

                if (bobber == null) // if bobber gets deleted
                {
                    aiState = AIState.idleState;
                }

                if (bobber != null)
                {
                    bobberDistance = Vector3.Distance(transform.position, bobber.transform.position);
                    if (bobberDistance > bobberMechanics.radius) // if bobber is too far away
                    {
                        fishMultiTag = GetComponent<FishMultiTag>();
                        if (fishMultiTag.HasTag("river_f_1") || fishMultiTag.HasTag("river_f_2") ||fishMultiTag.HasTag("river_f_3") || fishMultiTag.HasTag("river_f_4"))
                            rotationSpeed /= 2f;
                        else
                            rotationSpeed /= 8; // revert to orginal rotation to be smoother in idle
                        aiState = AIState.idleState;
                    }
                }

                if (bobber != null)
                {
                    UpdateTravel(bobber.transform.position, hungrySpeed);
                }

                break;

            case AIState.aggressiveState:

                // land collision check
                Collider[] groundColliders = Physics.OverlapSphere(transform.position, transform.localScale.magnitude * 0.9f);
                foreach (Collider collider in groundColliders)
                {
                    if (collider.CompareTag("Ground") && Time.time - lastNearLand > 5f)
                    {
                        landCollision = true;
                        lastNearLand = Time.time;
                        break;
                    }
                }
                if (landCollision)
                {
                    rb.velocity = Vector3.zero;
                }

                GameObject player = GameObject.FindWithTag("Player");
                float playerDist = Vector3.Distance(transform.position, player.transform.position);
                if (player != null && !landCollision)
                {
                    UpdateTravel(player.transform.position, aggressiveSpeed);
                }
                else if (landCollision && playerDist > 16f) // return to idle state if shark has collided and player if far enough away
                {
                    aiState = AIState.idleState;
                }
                else if (landCollision) // prevents shark from glitchy collisions into shore
                {
                    rb.velocity = Vector3.zero;
                }
                else if (player == null)
                {
                    landCollision = false;
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
                    if (_wasRecentlyCaught)
                    {
                        _wasRecentlyCaught = false;
                    }
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
        if (aiState != AIState.hungryState)
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, Quaternion.Euler(0f, rotation.eulerAngles.y, 0f), rotationSpeed * Time.deltaTime)); // only update y rotation (spinning)
        else
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, 0f), rotationSpeed * Time.deltaTime));
        float adjustedSpeed = Mathf.Lerp(0f, fishSpeed, remainingDistance / 0.5f);
        currentSpeed = Mathf.Lerp(currentSpeed, adjustedSpeed, Time.deltaTime);
        rb.velocity = this.transform.forward * currentSpeed;
        
        if (!InWater()) // ensures fish don't fly out of water during hungry state + fish collision
        {
            Vector3 updatedPosition = transform.position;
            float newY = GetWaterHeight();
            updatedPosition.y = newY;
            yValue = newY;
            transform.position = updatedPosition;
        }

        // keep at correct y-level
        Vector3 newPosition = transform.position;
        if (aiState != AIState.hungryState)
            newPosition = new Vector3(newPosition.x, yValue, newPosition.z);
        transform.position = newPosition;
    }

    private IEnumerator PreAggroTurn(GameObject player)
    {
        float turnProgress = 0f; // percentage complete
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);

        while (turnProgress < 1f)
        {
            turnProgress += rotationSpeed / 10f * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, turnProgress); // https://docs.unity3d.com/ScriptReference/Quaternion.Slerp.html
            yield return null; // https://www.javatpoint.com/unity-coroutines#:~:text=Here%2C%20the%20yield%20is%20a,continue%20on%20the%20next%20frame.&text=yield%20return%20null%20%2D%20This%20will,called%2C%20on%20the%20next%20frame.
        }
        aiState = AIState.aggressiveState; // transition state after turn completed
    }

    private bool InWater()
    {
        int waterLayer = LayerMask.NameToLayer("Water");

        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.001f);
        foreach(Collider collider in colliders)
        {
            if (collider.gameObject.layer == waterLayer)
                return true; // fish in water
        }
        return false; // fish not in water
    }

    private float GetWaterHeight()
    {
        int waterLayer = LayerMask.NameToLayer("Water");
        float maxWaterHeight = 0f; // default value
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.layer == waterLayer)
            {
                float waterHeight = collider.gameObject.transform.position.y;
                if (waterHeight > maxWaterHeight) // choose highest point if multiple exist
                    maxWaterHeight = waterHeight;
            }
        }

        return maxWaterHeight;
    }
}
