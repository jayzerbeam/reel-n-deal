using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager3Approach : MonoBehaviour
{
    GameObject villager3;

    private void Start()
    {
        villager3 = GameObject.Find("Cassye's_Villagers/cassye_villager_3");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource audioPlay = villager3.GetComponent<AudioSource>();
            audioPlay.Play();

        }
    }
}