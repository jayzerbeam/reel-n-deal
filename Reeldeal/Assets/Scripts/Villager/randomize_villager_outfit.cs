using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomize_villager_outfit : MonoBehaviour
{
    public Material[] materials; // Array of materials

    // Start is called before the first frame update
    void Start()
    {
        Transform[] childObjects = transform.GetComponentsInChildren<Transform>();
        foreach (Transform child in childObjects)
        {
            if (child.CompareTag("villager_body"))
            {
                // Choose a random material from the list.
                int randomIndex = Random.Range(0, materials.Length);
                Material randomMaterial = materials[randomIndex];

                // Apply the chosen material to child objects.
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = randomMaterial;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


