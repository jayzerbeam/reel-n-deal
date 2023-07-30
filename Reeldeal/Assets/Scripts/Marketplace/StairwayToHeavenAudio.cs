using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairwayToHeavenAudio : MonoBehaviour
{
    public GameObject stairway;
    public AudioSource audioPlay;
    BossFishSpawn _bossFishSpawn;
    AmbianceBackground _ambianceBG;

    private void Start()
    {
        audioPlay = stairway.GetComponent<AudioSource>();
        _ambianceBG = FindObjectOfType<AmbianceBackground>();
        _bossFishSpawn = FindObjectOfType<BossFishSpawn>();
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            audioPlay.Play();
            _ambianceBG.StopBgClip();
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            audioPlay.Stop();
            if (_bossFishSpawn.hasBossFishSpawned)
            {
                _ambianceBG.StopBgClip();
            }
            else
            {
                _ambianceBG.PlayBgClip();
            }
        }
    }
}
