using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDespawning : MonoBehaviour
{
    public float despawnRange = 128f;

    // Start is called before the first frame update
    void Start()
    {

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
            FishMultiTag fishMultiTag = fish.GetComponent<FishMultiTag>();
            if (Vector3.Distance(fish.transform.position, transform.position) > despawnRange && !fishMultiTag.HasTag("Persistent"))
            {
                Destroy(fish);
            }
        }
    }
}
