using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossFishSpawn : MonoBehaviour
{
    public GameObject bossFish;

    private AudioSource audioPlay;
    private bool hasPlayedAudio = false;
    BossFishAudio bossFishAudio;

    public GameObject bossFishAlert;
    private float timer = 4f;
    private bool isRunning = false;

    private hud_gui_controller inventoryController;

    private void Start()
    {
        inventoryController = FindObjectOfType<hud_gui_controller>();
        Renderer renderer = GetComponent<Renderer>();
        bossFishAudio = GetComponent<BossFishAudio>();
        renderer.enabled = false;

        if (bossFish == null)
        {
            Debug.LogError("Boss Fish not found");
        }

        audioPlay = GetComponent<AudioSource>();
        audioPlay.playOnAwake = false;

        bossFishAlert.SetActive(false);
    }

    private void Update()
    {
        SpawnFish();
    }

    IEnumerator guiAppear()
    {
        yield return new WaitForSeconds(timer);
        bossFishAlert.SetActive(false);
    }

    public void SpawnFish()
    {
        if (inventoryController.has_rod_upgrade == true && !isRunning)
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

            if (bossFishAudio != null)
            {
                bossFishAudio.PlayMusic();
            }

            isRunning = true;
            enabled = false;
        }
    }
}
