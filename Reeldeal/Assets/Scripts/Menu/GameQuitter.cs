using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
