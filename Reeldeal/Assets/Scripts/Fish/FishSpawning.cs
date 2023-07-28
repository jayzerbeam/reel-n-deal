using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawning : MonoBehaviour
{
    [System.Serializable]
    public class FishPrefabInfo
    {
        public GameObject prefabVariant;
        public float weight;
    }

    struct PotentialFishPrefabs
    {
        public GameObject prefab;
        public float weight;
    }

    // game object intialization
    public List<FishPrefabInfo> fishPrefabs;
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
        foreach (FishPrefabInfo fishPrefabInfo in fishPrefabs)
        {
            GameObject fishPrefab = fishPrefabInfo.prefabVariant;
            fishPrefab.GetComponent<FishPrefabInitializer>().Awake();
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
        GameObject[] fishObjects = GameObject.FindGameObjectsWithTag("Fish"); // get all fish in range

        // no need for checking fish distances - despawn makes it so all fish are within the spawning sphere 
        return fishObjects.Length;
    }

    private void spawnFish()
    {
        //spawn fish somewhere randomly in the specified range

        Collider[] waterColliders = Physics.OverlapSphere(playerTransform.position, maxSpawnDistance, waterLayer);
        if (waterColliders.Length == 0) // exits if no water around
        {
            //Debug.Log("ERROR: No Water Found");
            return;
        }

        List<float> colliderAreas = new List<float>(); // get areas, save to list
        float totalWeight = 0f;
        float weight = 0f;
        foreach (Collider collider in waterColliders) 
        {
            Bounds bounds = collider.bounds;
            weight = bounds.size.x * bounds.size.z;
            if (collider.gameObject.CompareTag("Ocean"))
            {
                weight *= 0.25f; // done so oceans don't get all the spawns
            }
            colliderAreas.Add(weight);
            totalWeight += weight; 
        }

        // determine which collider to try to spawn on
        float threshold = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;
        Collider randomWaterCollider = null;
        for (int i = 0; i < waterColliders.Length; i++)
        {
            cumulativeWeight += colliderAreas[i];

            if (threshold <= cumulativeWeight)
            {
                randomWaterCollider = waterColliders[i];
                break;
            }
        }

        string waterTag = randomWaterCollider.gameObject.tag; // tag of chosen body of water
        Vector3 spawnPosition = GetRandomPointOnCollider(randomWaterCollider); // random spot on water that meets spawning criteria
        
        if (spawnPosition == Vector3.zero)
        {
            //Debug.Log("Spawn Location Failure");
            return; // if no spot found
        }

        // pick prefab based on tags
        if (waterTag == "Untagged")
            return;
        GameObject fishPrefab = GetRandomFishPrefab(waterTag);

        // create fish (docs.unity3d.com/ScriptReference/Object.Instantiate.html)
        GameObject fish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);

        // move fish into water
        Renderer FishRenderer = fish.GetComponent<Renderer>();
        fish.transform.position = new Vector3(fish.transform.position.x, randomWaterCollider.bounds.max.y - FishRenderer.bounds.size.y, fish.transform.position.z); // half body in water
        //spawnPosition = new Vector3(fish.transform.position.x, randomWaterCollider.bounds.max.y - FishRenderer.bounds.size.y / 2, fish.transform.position.z);

        // all for debugging
        FishMultiTag fishMultiTag = fish.GetComponent<FishMultiTag>();
        //Debug.Log("------------");
        //Debug.Log("Fish Spawned at " + spawnPosition);
        //Debug.Log("Distance to player: " + Vector3.Distance(spawnPosition, playerTransform.position));
        //Debug.Log("Fish Tags: " + string.Join(", ", fishMultiTag.tags));
        //Debug.Log("Water Type: " + waterTag);
    }

    private Vector3 GetRandomPointOnCollider(Collider collider)
    {
        Bounds bounds = collider.bounds;

        float distance;
        int attempt = 0;
        Vector3 randomPoint;
        while (attempt < spawnAttempts)
        {
            // generate random point within the collider bounds
            randomPoint = new Vector3(Random.Range(bounds.min.x, bounds.max.x), bounds.max.y, Random.Range(bounds.min.z, bounds.max.z));
            distance = Vector3.Distance(randomPoint, playerTransform.position);
            // conditions needed to spawn
            if (InWater(randomPoint) && IsPointAboveGround(randomPoint) && distance >= minSpawnDistance && distance <= maxSpawnDistance)
            {
                return randomPoint;
            }
            attempt++;
        }
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
        float highestPoint = float.MinValue;

        // highest point on map
        Terrain[] terrains = Terrain.activeTerrains;
        foreach (Terrain terrain in terrains)
        {
            float terrainHeight = terrain.SampleHeight(point);
            if (terrainHeight > highestPoint)
            {
                highestPoint = terrainHeight;
            }
        }

        // check if greater than potential spawn position
        if (point.y < highestPoint)
        {
            //Debug.Log("Spawn Point Below Ground");
            return false;
        }

        return true;
    }



    private GameObject GetRandomFishPrefab(string waterTag)
    {
        List<PotentialFishPrefabs> potentialFishPrefabs = new List<PotentialFishPrefabs>();
        float totalWeight = 0f;

        // determine potential prefabs
        foreach (FishPrefabInfo variant in fishPrefabs)
        {
            FishMultiTag multiTag = variant.prefabVariant.GetComponent<FishMultiTag>();
            if (multiTag != null && multiTag.HasTag(waterTag))
            {
                potentialFishPrefabs.Add(new PotentialFishPrefabs { prefab = variant.prefabVariant, weight = variant.weight });
                //Debug.Log("Fish Tags: " + string.Join(", ", multiTag.tags) + " WEIGHT: " + variant.weight);
                totalWeight += variant.weight;
            }
        }

        // choose from potential prefab variants
        if (potentialFishPrefabs.Count > 0)
        {
            float threshold = Random.Range(0f, totalWeight);
            float cumulativeWeight = 0f;

            foreach (PotentialFishPrefabs fishPrefabWeight in potentialFishPrefabs)
            {
                cumulativeWeight += fishPrefabWeight.weight;
                
                if (threshold <= cumulativeWeight)
                {
                    return fishPrefabWeight.prefab;
                }
            }
        }
        //Debug.Log("Prefab error");
        return potentialFishPrefabs[0].prefab;
    }

}