using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDeath : MonoBehaviour
{
    private float waterTime;
    private bool isInWater = false;
    private Rigidbody[] ragdollRigidBody;
    private CharacterController charController;
    private Animator animator;
    private bool isRagdoll = false;

    private void Awake()
    {
        ragdollRigidBody = GetComponentsInChildren<Rigidbody>();
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        charController.enabled = true; 
        DisableRagdoll();
    }

    private void Update()
    {
        if (isInWater)
        {
            waterTime += Time.deltaTime;
            if (waterTime >= 3f && !isRagdoll)
            {
                EnableRagdoll();
                Invoke("LoadMainMenu", 3f);
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

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void DisableRagdoll()
    {
        foreach (var rb in ragdollRigidBody)
        {
            rb.isKinematic = true;
        }
    }

    private void EnableRagdoll()
    {
        isRagdoll = true;


        charController.enabled = false;

        foreach (var rb in ragdollRigidBody)
        {
            rb.isKinematic = false;
        }

        animator.enabled = false;
    }
}