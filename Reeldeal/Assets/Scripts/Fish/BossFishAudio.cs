using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFishAudio : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponents<AudioSource>()[1];
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }
}
