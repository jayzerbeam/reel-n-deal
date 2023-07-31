using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceBackground : MonoBehaviour
{
    AudioSource _happyBG;

    void Start()
    {
        _happyBG = GetComponents<AudioSource>()[0];
    }

    public void PlayBgClip()
    {
        _happyBG.Play();
    }

    public void StopBgClip()
    {
        _happyBG.Stop();
    }
}
