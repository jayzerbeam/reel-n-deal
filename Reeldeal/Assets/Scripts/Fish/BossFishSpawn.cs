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

        if (bossFish == null)
        {
            Debug.LogError("Boss Fish not found");
        }
    }

    private void Update()
    {
        if (fishingRodUpgrade == null && bossFish != null)
        {
            bossFish.SetActive(true);
            enabled = false;
        }
    }
}