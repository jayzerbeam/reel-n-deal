using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor1Audio : MonoBehaviour
{
    GameObject vendor1;

    private void Start()
    {    
            vendor1 = GameObject.Find("Town and Marketplace/Vendor1");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource audioPlay = vendor1.GetComponent<AudioSource>();
            audioPlay.Play();

        }
    }
}