using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDeath : MonoBehaviour
{
    private float waterTime;
    private bool isInWater = false;
    private Animator animator;

    public bool isDying;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInWater)
        {
            waterTime += Time.deltaTime;
            if (waterTime >= 3f)
            {
                animator.SetTrigger("isDying");
                Invoke("LoadMainMenu", 8f);
            }
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if (
            c.CompareTag("Lake")
            || c.CompareTag("Waterfall")
            || c.CompareTag("River")
            || c.CompareTag("Spring")
            || c.CompareTag("Ocean")
        )
        {
            isInWater = true;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (
            c.CompareTag("Lake")
            || c.CompareTag("Waterfall")
            || c.CompareTag("River")
            || c.CompareTag("Spring")
            || c.CompareTag("Ocean")
        )
        {
            isInWater = false;
            waterTime = 0f;
        }
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void isDead()
    {
        if (isDying)
        {
            animator.SetTrigger("isDying");
        }
    }
}
