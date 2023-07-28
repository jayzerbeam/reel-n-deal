using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor1Audio : MonoBehaviour
{
    public AudioSource vendor1Audio;

    private void Start()
    {    

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vendor1Audio.Play();

        }
    }
}