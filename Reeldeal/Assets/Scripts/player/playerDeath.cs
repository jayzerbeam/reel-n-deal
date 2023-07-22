using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDeath : MonoBehaviour
{
    public PlayerDrownVolume playerDrownVolume;
    public Animator dyingAnimation; 

    public bool isDying;

    private void Start()
    {

    }

    private void Drown()
    {
       // transform.position = respawnPosition;

        playerDrownVolume.isDrowning = true; 
    }



    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Die()
    {
        if (isDying)
        {
            dyingAnimation.SetBool("isDrowning", true);
        }
    }
}
