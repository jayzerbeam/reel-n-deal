using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWonAlert : MonoBehaviour
{
    public GameObject playerWonGUI;
    public GameObject confettiParticleSystem;
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void PlayerWins()
    {
        Debug.Log("PlayerWins() method called.");

        playerWonGUI.SetActive(true);
        audioSource.clip = audioClip;
        audioSource.Play();

        Vector3 playerPosition = transform.position;

        GameObject confettiInstance = Instantiate(
            confettiParticleSystem,
            playerPosition,
            Quaternion.identity
        );

        Invoke("GoToMainMenu", 5f);
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
