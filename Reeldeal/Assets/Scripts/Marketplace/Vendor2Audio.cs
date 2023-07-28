using UnityEngine;

public class Vendor2Audio: MonoBehaviour
{
    public AudioSource vendor2Audio;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vendor2Audio.Play();

        }
    }
}