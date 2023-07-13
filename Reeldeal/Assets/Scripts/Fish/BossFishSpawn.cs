using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFishSpawn : MonoBehaviour
{
    public GameObject bossFish;
    private GameObject fishingRodUpgrade;

    private AudioSource audioPlay;
    private bool hasPlayedAudio = false; 

    private GameObject bossFishAlert;
    private float timer = 3f;
    private bool isRunning = false;

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
        if (fishingRodUpgrade == null && !isRunning)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.enabled = true;

            if (!hasPlayedAudio)
            {
                audioPlay.Play();
                hasPlayedAudio = true;
                bossFishAlert.SetActive(true);
                StartCoroutine(guiAppear());
                
            }

            isRunning = true;



            enabled = false;

            Debug.Log("Playing");
        }

    }

    IEnumerator guiAppear()
    {
        yield return new WaitForSeconds(timer);
        bossFishAlert.SetActive(false);
    }
}