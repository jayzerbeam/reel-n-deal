using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDeath : MonoBehaviour
{
    public Animator dyingAnimation;
    public LayerMask waterLayer;

    private Camera mainCamera;
    public Vector3 respawnPosition; 
    private Transform playerTransform; 
    private float timeInWater;
    private float timeThreshold = 3f;
    private bool isDyingTriggered = false;

    public bool isDying;

    private void Start()
    {
        mainCamera = Camera.main;
        playerTransform = transform; 
    }

    private void Update()
    {
        //Vector3 raycastOrigin = playerTransform.position + Vector3.up * 0.2f;
        //Vector3 raycastDirection = Vector3.down; 
        //if (Physics.Raycast(raycastOrigin, raycastDirection, out RaycastHit hit, Mathf.Infinity, waterLayer))

        if (Physics.Raycast(mainCamera.transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, waterLayer))
        {
            Debug.Log("Player is underwater!");
            timeInWater += Time.deltaTime;

            if (timeInWater >= timeThreshold && !isDyingTriggered)
            {
                isDyingTriggered = true;
                Debug.Log("Player exceeds underwater time!");
                Die();
            }
        }

        else
        {
            timeInWater = 0f;
            isDyingTriggered = false;
        }
    
    }

    private void Drown()
    {
       // transform.position = respawnPosition;

        //playerDrownVolume.isDrowning = true; 
    }

    private void Die()
    {

        //dyingAnimation.SetBool("isDying", true);
        StartCoroutine(Respawn());  
        //SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        playerTransform.transform.position = respawnPosition;
        //dyingAnimation.SetBool("isDying", false);

    }
       
}
