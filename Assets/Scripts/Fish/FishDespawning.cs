using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDespawning : MonoBehaviour
{
    public float despawnRange = 128f;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        despawnFish();
    }

    private void despawnFish()
    {
        GameObject[] fishObjects = GameObject.FindGameObjectsWithTag("Fish");
        for (int i = 0; i < fishObjects.Length; i++)
        {
            GameObject fish = fishObjects[i];
            if (Vector3.Distance(fish.transform.position, playerTransform.position) > despawnRange)
            {
                Destroy(fish);
                Debug.Log("Fish Despawned");
            }
        }
    }
}
