using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDeath : MonoBehaviour

{
    private float waterTime;
    private bool isInWater = false;

    private void Update()
    {

        {
            if (isInWater)
            {
                waterTime += Time.deltaTime;
                if (waterTime >= 3f)
                {
                    SceneManager.LoadScene("MainMenu");
                }


            }
        }
    }
    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Lake") || c.CompareTag("Waterfall") || c.CompareTag("River") || c.CompareTag("Spring") || c.CompareTag("Ocean"))
        {
            isInWater = true;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Lake") || c.CompareTag("Waterfall") || c.CompareTag("River") || c.CompareTag("Spring") || c.CompareTag("Ocean"))
        {
            isInWater = false;
            waterTime = 0f;
        }

    }


}