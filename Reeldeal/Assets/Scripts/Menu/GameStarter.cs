using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Starting Game");
        SceneManager.LoadScene("mainGameScene");
        Time.timeScale = 1f;
    }
}
