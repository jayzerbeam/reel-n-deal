using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeFishTagger : MonoBehaviour
{
    private GameObject fishPrefab;

    private void OnEnable()
    {
        fishPrefab = gameObject;

        FishMultiTag multiTag = fishPrefab.GetComponent<FishMultiTag>();
        multiTag.AddTag("Lake");
    }
}
