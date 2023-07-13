using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFishAudio : MonoBehaviour

{
    AudioSource audioSource;
    bool playMusic;
    bool toggleMusic;

    float maxDistance = 250f;
    float minDistance = 50f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playMusic = true;

        audioSource.maxDistance = maxDistance;
        audioSource.minDistance = minDistance;
        audioSource.loop = true;

    }

    private void Update()
    {
        if (playMusic && toggleMusic)
        {
            audioSource.Play();
            toggleMusic = false;
        }

        if (!playMusic && toggleMusic)
        {
            audioSource.Stop();
            toggleMusic = false;
        }
    }
}
