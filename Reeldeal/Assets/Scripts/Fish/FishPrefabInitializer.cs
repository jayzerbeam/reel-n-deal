using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPrefabInitializer : MonoBehaviour
{
    public string[] fishTags;
    public List<FishMaterial> fishMaterials;
    public float minSize = 0.75f;
    public float maxSize = 1.5f;
    public float totalWaypoints = 4;
    public float maxWaypointSpacing = 25f;
    public GameObject waypointPrefab;
    private GameObject waypointParent;
    public GameObject[] waypoints;
    public LayerMask waterLayer;

    // individual materials
    [System.Serializable]
    public class FishMaterial
    {
        public Material fishMaterial;
        public float weight;
    }

    // all materials
    [System.Serializable]
    public class FishMaterials
    {
        public FishMaterial[] weights;
        public Material defaultMaterial;
    }

    public void Awake()
    {

    }

    public void OnEnable()
    {
        // set tags
        FishMultiTag multiTag = GetComponent<FishMultiTag>();

        if (multiTag == null)
        {
            multiTag = gameObject.AddComponent<FishMultiTag>();
        }

        foreach (string tag in fishTags)
        {
            multiTag.AddTag(tag);
        }

        SelectSize();

        SelectTexture();

        GenerateWaypoints();
    }

    private void SelectSize()
    {
        // randomize size
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(size, size, size);
    }

    private void SelectTexture()
    {
        //randomized weighted texture

        // total weight
        float totalWeight = 0f;
        foreach (FishMaterial fishMaterial in fishMaterials)
        {
            totalWeight += fishMaterial.weight;
        }

        Renderer fishRenderer = GetComponent<Renderer>();

        // choose material based on random value
        float threshold = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;
        foreach (FishMaterial fishMaterial in fishMaterials)
        {
            cumulativeWeight += fishMaterial.weight;
            if (threshold <= cumulativeWeight)
            {
                fishRenderer.material = fishMaterial.fishMaterial;
                return;
            }
        }

        // should only reach here is something unexpected happens
        fishRenderer.material = null;
    }

    private void GenerateWaypoints()
    {
        waypointParent = new GameObject("WaypointParent"); // create empty parent for waypoints
        waypointParent.transform.position = transform.position;
        waypoints = new GameObject[(int)totalWaypoints]; // array of waypoints

        Vector3 fishBottomY = transform.position - new Vector3(0f, transform.localScale.y, 0f);


        // conditions for valid waypoint
        bool validPosition = false;
        int maxWaypointAttempts = 5;
        int currWaypointAttempt = 0;

        for (int i = 0; i < totalWaypoints; i++)
        {
            // initialize default variables for each new waypoint
            GameObject waypoint = null;
            validPosition = false;
            currWaypointAttempt = 0;
            
            while (!validPosition && currWaypointAttempt < maxWaypointAttempts) // conditions for valid waypoint location
            {
                Vector3 offset = new Vector3(Random.Range(-maxWaypointSpacing, maxWaypointSpacing), 0f, Random.Range(-maxWaypointSpacing, maxWaypointSpacing));
                
                if (IsPointAboveGround(waypointParent.transform.position + offset) && InWater(waypointParent.transform.position + offset))
                {
                    waypoint = Instantiate(waypointPrefab, waypointParent.transform.position + offset, Quaternion.identity);
                    validPosition = true;
                }
                currWaypointAttempt++;
            } 
            
            if (!validPosition) // set to parent position if no suitable location found
            {
                waypoint = Instantiate(waypointPrefab, waypointParent.transform.position, Quaternion.identity);
            }

            waypoint.transform.SetParent(waypointParent.transform);
            waypoints[i] = waypoint; // add to array
        }
    }

    private bool InWater(Vector3 point)
    {
        // checks if point is in water
        Collider[] waterColliders = Physics.OverlapSphere(point, 1f, waterLayer);
        Debug.Log(waterColliders.Length > 0);
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
            Debug.Log("Ground: False");
            return false;
        }
        Debug.Log("Ground: True");
        return true;
    }


    private void OnDisable()
    {
        // destroy waypoints when fish despawn or is caught to prevent lag
        Destroy(waypointParent);
    }
}