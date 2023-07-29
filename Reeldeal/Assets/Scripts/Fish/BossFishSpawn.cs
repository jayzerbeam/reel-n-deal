using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossFishSpawn : MonoBehaviour
{
    public GameObject bossFish;
    private bool player_has_rod_upgrade;

    private AudioSource audioPlay;
    private bool hasPlayedAudio = false; 

    //public GameObject bossFishAlert;
    private float timer = 3f;
    private bool isRunning = false;

    public bool testing = false; 

    private hud_gui_controller inventoryController;


    private void Start()
    {
        inventoryController = FindObjectOfType<hud_gui_controller>();
        player_has_rod_upgrade = inventoryController.has_rod_upgrade;
        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = false;


        if (bossFish == null)
        {
            Debug.LogError("Boss Fish not found");
        }

        audioPlay = GetComponent<AudioSource>();
        audioPlay.playOnAwake = false;

        //bossFishAlert.SetActive(false);
    }

    private void Update()
    {
        SpawnFish();

    }

    IEnumerator guiAppear()
    {
        yield return new WaitForSeconds(timer);
        //bossFishAlert.SetActive(false);
    }

    public void SpawnFish()
    {
        if (player_has_rod_upgrade == true && !isRunning)
        //if (testing == true && !isRunning)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.enabled = true;

            if (!hasPlayedAudio)
            {
                audioPlay.Play();
                hasPlayedAudio = true;
                //bossFishAlert.SetActive(true);
                StartCoroutine(guiAppear());

            }

            BossFishAudio bossFishAudio = GetComponent<BossFishAudio>();
            if (bossFishAudio != null)
            {
                bossFishAudio.playMusic = true; 
            }

            isRunning = true;



            enabled = false;

            //Debug.Log("Playing");
        }
    }
}