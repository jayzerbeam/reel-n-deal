using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsPanel : MonoBehaviour
{

    public GameObject instructionsPanel;
    private bool activePanel = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleInstructions();
        }
    }

    void ToggleInstructions()
    {
        activePanel = !activePanel;

        if (activePanel)
        {
            instructionsPanel.SetActive(true);
        }
        else
        {
            instructionsPanel.SetActive(false);
        }
    }
}
