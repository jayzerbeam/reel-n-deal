using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor3Audio : MonoBehaviour
{
    GameObject vendor3;

    private void Start()
    {
        vendor3 = GameObject.Find("Town and Marketplace/Vendor3");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource audioPlay = vendor3.GetComponent<AudioSource>();
            audioPlay.Play();

        }
    }
}