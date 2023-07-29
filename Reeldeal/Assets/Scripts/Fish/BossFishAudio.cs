using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFishAudio : MonoBehaviour

{
    public AudioSource audioSource;
    public bool playMusic = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (playMusic)
        {
            audioSource.Play();
        }

        if (!playMusic)
        {
            audioSource.Stop();
        }
    }
}
