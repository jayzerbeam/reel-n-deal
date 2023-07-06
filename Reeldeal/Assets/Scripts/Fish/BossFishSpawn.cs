using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFishSpawn : MonoBehaviour
{
    public GameObject bossFish;
    private GameObject fishingRodUpgrade;

    private void Start()
    {
        bossFish = GameObject.Find("BossFish");
        fishingRodUpgrade = GameObject.FindGameObjectWithTag("FishingRodUpgrade");
        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = false;

        if (bossFish == null)
        {
            Debug.LogError("Boss Fish not found");
        }
    }

    private void Update()
    {
        if (fishingRodUpgrade == null)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.enabled = true; 
            enabled = false; 
        }
    }
}