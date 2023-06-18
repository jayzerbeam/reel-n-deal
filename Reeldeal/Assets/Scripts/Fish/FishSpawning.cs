using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawning : MonoBehaviour
{
    // game object intialization
    public GameObject[] fishPrefabs;
    public Material[] fishMaterials; 
    public LayerMask waterLayer;

    // spawn parameters
    public float maxSpawnDistance = 128f;
    public float minSpawnDistance = 24f;
    public float spawnAttempts = 10;
    public float spawnInterval = 2f;
    public int maxFish = 10;

    private float spawnTimer = 0f;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = transform;
        spawnTimer = 5f;

        //add tags to fish prefabs
        foreach (GameObject fishPrefab in fishPrefabs)
        {
            FishPrefabInitializer prefabInitializer = fishPrefab.GetComponent<FishPrefabInitializer>(); // get the initializer script component of the prefab
            prefabInitializer.Awake();
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        // check if spawning should occur
        if (spawnTimer <= 0f && fishNearby() < maxFish)
        {
            spawnFish();
            spawnTimer = spawnInterval;
        }
    }

    private int fishNearby()
    {
        // counts up total number of fish near player
        int totalFish = 0;
        Collider[] collidersInRange = Physics.OverlapSphere(playerTransform.position, maxSpawnDistance); // get collider of any object in range

        // add to counter if it is a fish
        foreach (Collider collider in collidersInRange)
        {
            if (collider.gameObject.CompareTag("Fish"))
            {
                totalFish++;
            }
        }

        return totalFish;
    }

    private void spawnFish()
    {
        //spawn fish somewhere randomly in the specified range

        Collider[] waterColliders = Physics.OverlapSphere(playerTransform.position, maxSpawnDistance, waterLayer);

        Collider randomWaterCollider = waterColliders[Random.Range(0, waterColliders.Length)];
        Vector3 spawnPosition = GetRandomPointOnCollider(randomWaterCollider);

        if (spawnPosition == Vector3.zero)
        {
            return;
        }

        // pick prefab based on tags
        GameObject fishPrefab;
        List<GameObject> potentialFishPrefabs = new List<GameObject>();
        foreach (GameObject prefab in fishPrefabs)
        {
            FishMultiTag prefabMultiTag = prefab.GetComponent<FishMultiTag>(); // Use the correct variable name here
            if (prefabMultiTag.HasTag("Lake"))
            {
                potentialFishPrefabs.Add(prefab);
            }
        }
        if (potentialFishPrefabs.Count > 0)
        {
            fishPrefab = potentialFishPrefabs[Random.Range(0, potentialFishPrefabs.Count)]; // randomly pick from available prefabs    
        }
        else
        {
            Debug.Log("No prefab found");
            return; // no spawning if not found
        }

        Material fishMaterial = fishMaterials[Random.Range(0, fishMaterials.Length)]; // randomly pick material

        // create fish (docs.unity3d.com/ScriptReference/Object.Instantiate.html)
        GameObject fish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);

        Renderer fishRenderer = fish.GetComponent<Renderer>(); // allows for material modification
        if (fishRenderer != null)
        {
            fishRenderer.material = fishMaterial;
        }


        // all for debugging
        FishMultiTag fishMultiTag = fish.GetComponent<FishMultiTag>();
        Debug.Log("Fish Spawned at " + spawnPosition);
        Debug.Log("Distance to player: " + Vector3.Distance(spawnPosition, playerTransform.position));
        Debug.Log("Fish Tags: " + string.Join(", ", fishMultiTag.tags));
    }

    private Vector3 GetRandomPointOnCollider(Collider collider)
    {
        
        Bounds bounds = collider.bounds; // get bounds of water plane (https://forum.unity.com/threads/what-are-bounds.480975/)

        // Calculate the ranges within the collider bounds
        float maxRange = Mathf.Min(maxSpawnDistance, Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) / 2f);
        float minRange = Mathf.Min(minSpawnDistance, maxRange);

        // Generate a valid random point within the spawning range
        Vector3 randomPoint;
        float distance;
        int attempt = 0;
        while (attempt < spawnAttempts)
        {
            randomPoint = new Vector3(Random.Range(bounds.min.x, bounds.max.x), bounds.max.y, Random.Range(bounds.min.z, bounds.max.z));
            distance = Vector3.Distance(randomPoint, playerTransform.position);
            if (InWater(randomPoint) && IsPointAboveGround(randomPoint) && distance >= minSpawnDistance && distance <= maxSpawnDistance)
            {
                return randomPoint;
            }

            attempt++;
        }

        // return if no valid point found after a few attempts to prevent game freezes
        Debug.Log("Failed to find a valid spawn point");
        return Vector3.zero;
    }


    private bool InWater(Vector3 point)
    {
        // checks if point is in water
        Collider[] waterColliders = Physics.OverlapSphere(point, 0.1f, waterLayer);
        return waterColliders.Length > 0;
    }

    private bool IsPointAboveGround(Vector3 point)
    {
        // Raycast straight down determines if it is immediately overlapping with land
        RaycastHit hit;
        if (Physics.Raycast(point, Vector3.down, out hit))
        {
            if (hit.distance > 1f)
            {
                return true;
            }
        }
        return false;
    }

}