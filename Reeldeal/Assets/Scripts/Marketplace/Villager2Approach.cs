using UnityEngine;

public class Villager2Approach : MonoBehaviour
{
    GameObject villager2;

    private void Start()
    {
        villager2 = GameObject.Find("Cassye's_Villagers/cassye_villager_2");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource audioPlay = villager2.GetComponent<AudioSource>();
            audioPlay.Play();

        }
    }
}