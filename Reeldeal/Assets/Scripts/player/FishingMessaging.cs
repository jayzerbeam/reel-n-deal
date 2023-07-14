using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishingMessaging : MonoBehaviour
{
    public TextMeshProUGUI FishingMessageBox;
    public Image FishingMessageBoxBackground;

    public void DisplayMessage(string message, float timeToErase = 4f)
    {
        StartCoroutine(DisplayThenErase(message, timeToErase));
    }

    public void StopMessage()
    {
        StopAllCoroutines();
        FishingMessageBoxBackground.enabled = false;
        FishingMessageBox.text = "";
    }

    private IEnumerator DisplayThenErase(string message, float timeToErase)
    {
        FishingMessageBoxBackground.enabled = true;
        FishingMessageBox.text = message;

        yield return new WaitForSeconds(timeToErase);

        FishingMessageBoxBackground.enabled = false;
        FishingMessageBox.text = "";
    }
}
