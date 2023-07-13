using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFishSpawn : MonoBehaviour
{
    public GameObject bossFish;
    private GameObject fishingRodUpgrade;

    private AudioSource audioPlay;

    private GameObject bossFishAlert;
    private float timer = 3f; 

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
            StartCoroutine(guiAppear());
   
           enabled = false; 
        }

    }

    IEnumerator guiAppear()
    {
        yield return new WaitForSeconds(timer);
        bossFishAlert.SetActive(false);
    }
}