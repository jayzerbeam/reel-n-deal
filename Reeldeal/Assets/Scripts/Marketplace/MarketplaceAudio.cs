using UnityEngine;

public class MarketplaceAudio : MonoBehaviour
{
   // public AudioClip marketplaceAudio;
    AudioSource audioSource;
    bool playMusic;
    bool toggleMusic;

    float maxDistance = 50f;
    float minDistance = 3f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playMusic = true;

        audioSource.maxDistance = maxDistance;
        audioSource.minDistance = minDistance;
        audioSource.loop = true;

      //  audioSource.clip = marketplaceAudio;
    }

    private void Update()
    {
        if (playMusic && toggleMusic)
        {
            audioSource.Play();
            toggleMusic = false;
        }

        if (!playMusic && toggleMusic)
        {
            audioSource.Stop();
            toggleMusic = false;
        }
    }
}
