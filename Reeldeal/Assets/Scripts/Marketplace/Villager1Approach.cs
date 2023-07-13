using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager1Approach : MonoBehaviour
{
    GameObject villager1;

    private void Start()
    {    
            villager1 = GameObject.Find("Cassye's_Villagers/cassye_villager_1");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource audioPlay = villager1.GetComponent<AudioSource>();
            audioPlay.Play();

        }
    }
}