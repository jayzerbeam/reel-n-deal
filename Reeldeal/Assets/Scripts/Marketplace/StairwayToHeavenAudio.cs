using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairwayToHeavenAudio : MonoBehaviour
{

    public GameObject stairway;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource audioPlay = stairway.GetComponent<AudioSource>();
            audioPlay.Play();

        }
    }
}
