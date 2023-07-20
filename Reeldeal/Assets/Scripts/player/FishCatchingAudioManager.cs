using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCatchingAudioManager : MonoBehaviour
{
    AudioSource _keypress_failure;
    AudioSource _keypress_success;
    AudioSource _rod_reel;
    AudioSource _caught_fish_bell;
    AudioSource _escape;

    public AudioSource KeypressFailure
    {
        get { return _keypress_failure; }
        set { _keypress_failure = value; }
    }
    public AudioSource KeypressSuccess
    {
        get { return _keypress_success; }
        set { _keypress_success = value; }
    }
    public AudioSource RodReel
    {
        get { return _rod_reel; }
        set { _rod_reel = value; }
    }
    public AudioSource CaughtFishBell
    {
        get { return _caught_fish_bell; }
        set { _caught_fish_bell = value; }
    }
    public AudioSource Escape
    {
        get { return _escape; }
        set { _escape = value; }
    }

    void Start()
    {
        KeypressFailure = GetComponents<AudioSource>()[0];
        KeypressSuccess = GetComponents<AudioSource>()[1];
        RodReel = GetComponents<AudioSource>()[2];
        CaughtFishBell = GetComponents<AudioSource>()[3];
        Escape = GetComponents<AudioSource>()[4];
    }
}
