using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPrefabInitializer : MonoBehaviour
{
    public string[] fishTags;
    public List<FishMaterial> fishMaterials;
    public float minSize = 0.75f;
    public float maxSize = 1.5f;


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
}