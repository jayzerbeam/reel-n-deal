using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorClueStairs : MonoBehaviour
{
    public GameObject stairsClue; 

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            stairsClue.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            stairsClue.SetActive(false);
        }
    }
}
