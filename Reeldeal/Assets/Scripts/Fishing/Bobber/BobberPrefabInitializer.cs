using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberPrefabInitializer : MonoBehaviour
{
    public string[] bobberTags;
    public Material[] bobberMaterials; // top sphere, bottom sphere, top bit colors
    public List<AttractiveBobberInfo> attractiveBobberInfo;

    [System.Serializable]
    public class AttractiveBobberInfo
    {
        public float radius;
        public GameObject fishPrefab;
        public List<float> fishSizeBounds; // only add two values, min and max scaled size
    }

    public void Awake()
    {

    }

    public void Start()
    {
        // set tags
        FishMultiTag multiTag = GetComponent<FishMultiTag>();

        if (multiTag == null)
        {
            multiTag = gameObject.AddComponent<FishMultiTag>();
        }

        foreach (string tag in bobberTags)
        {
            multiTag.AddTag(tag);
        }

        InitializeColors(); // set colors for different bobber types
    }

    private void InitializeColors()
    {
        Renderer[] bobberParts = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < bobberParts.Length; i++)
        {
            bobberParts[i].material = bobberMaterials[i];
        }

    }

}