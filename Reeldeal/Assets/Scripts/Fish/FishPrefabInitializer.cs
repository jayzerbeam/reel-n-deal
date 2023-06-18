using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPrefabInitializer : MonoBehaviour
{
    public string[] fishTags;

    public void Awake()
    {
        FishMultiTag multiTag = GetComponent<FishMultiTag>();

        if (multiTag == null)
        {
            multiTag = gameObject.AddComponent<FishMultiTag>();
        }

        foreach (string tag in fishTags)
        {
            multiTag.AddTag(tag);
        }
    }
}