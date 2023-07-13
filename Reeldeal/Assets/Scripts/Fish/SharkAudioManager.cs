using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkAudioManager : MonoBehaviour
{
    public AudioSource startSound;
    public AudioSource loopSound;
    private static SharkAudioManager audioManager;

    void Start()
    {
        startSound = GetComponents<AudioSource>()[0];
        loopSound = GetComponents<AudioSource>()[1];
    }

    private void Awake()
    {
        audioManager = this; // set to particular instance
    }

    public static void PlayStartSound(Vector3 position, float pitch)
    {
        if (!audioManager.startSound.isPlaying && !audioManager.loopSound.isPlaying)
        {
            audioManager.startSound.transform.position = position;
            audioManager.startSound.pitch = pitch;
            audioManager.startSound.Play();
        }
    }

    public static void PlayLoopSound(Vector3 position, float pitch)
    {
        if (!audioManager.startSound.isPlaying && !audioManager.loopSound.isPlaying)
        {
            audioManager.loopSound.transform.position = position;
            audioManager.loopSound.pitch = pitch;
            audioManager.loopSound.Play();
        }
    }
}
