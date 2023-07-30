using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairwayToHeavenAudio : MonoBehaviour
{
    public GameObject stairway;
    public AudioSource audioPlay;

    private void Start()
    {
        audioPlay = stairway.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            audioPlay.Play();
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            audioPlay.Stop();
        }
    }
}
