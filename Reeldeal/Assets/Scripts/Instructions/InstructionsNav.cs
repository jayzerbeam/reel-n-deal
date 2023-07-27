using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsNav : MonoBehaviour
{
    public GameObject[] panels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPanel(int index)
    {
        // https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false); // disable all panels first
        }

        panels[index].SetActive(true);

    }
}
