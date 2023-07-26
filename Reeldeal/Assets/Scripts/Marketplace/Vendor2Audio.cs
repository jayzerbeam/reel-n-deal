using UnityEngine;

public class Vendor2Audio: MonoBehaviour
{
    GameObject vendor2;

    private void Start()
    {
        vendor2 = GameObject.Find("Town and Marketplace/Vendor2");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource audioPlay = vendor2.GetComponent<AudioSource>();
            audioPlay.Play();

        }
    }
}