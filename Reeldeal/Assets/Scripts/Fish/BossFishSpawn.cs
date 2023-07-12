using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFishSpawn : MonoBehaviour
{
    public GameObject bossFish;
    private GameObject fishingRodUpgrade;

    private AudioSource audioPlay;

    private GameObject bossFishAlert;
    private float displayTime;
    private float timer; 

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

        audioPlay = GetComponent<AudioSource>();
        audioPlay.playOnAwake = false;

        bossFishAlert = GameObject.Find("UICanvas/BossFishAlert");
        bossFishAlert.SetActive(false);
    }

    private void Update()
    {
        if (fishingRodUpgrade == null)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.enabled = true;
            audioPlay.Play();
            bossFishAlert.SetActive(true);
            timer = displayTime;
            enabled = false; 
        }

        if (timer > 0f)
        {
            displayTime -= Time.deltaTime;
            if (displayTime <= 0f)
            {
                bossFishAlert.SetActive(false);
            }
        }
    }
}