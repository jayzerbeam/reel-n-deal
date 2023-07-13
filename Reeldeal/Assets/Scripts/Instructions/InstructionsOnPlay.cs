using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsOnPlay : MonoBehaviour
{
    public GameObject openingInstructions;
    public float showDisplay = 5f; 
    void Start()
    {
        openingInstructions.SetActive(true);
    }
    void Update()
    {
        if (showDisplay > 0f)
        {
            showDisplay -= Time.deltaTime;
            if (showDisplay <= 0f)
            {
                openingInstructions.SetActive(false);
            }
        }
    }

    void ShowOpeningInstructions()
    {
        openingInstructions.SetActive(true);
}
}
