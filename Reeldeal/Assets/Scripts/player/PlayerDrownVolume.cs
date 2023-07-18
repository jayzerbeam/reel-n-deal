using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class PlayerDrownVolume : MonoBehaviour
{
    public Volume volume;
    public VolumeProfile originalVolumeProfile;
    public VolumeProfile waterVolumeProfile;

    public float transitionDuration = 5f;
    private float timer = 0f;

    private float newWeight;

    public bool drowned = false;
    private bool started = false; // updated when drowning is first set (prevents volume looping)
    private bool updating = false; // transitioning between volumes


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (drowned && !started)
        {
            volume.profile = waterVolumeProfile;
            volume.weight = 0f;
            updating = true;
            started = true;
            timer = 0f;
        }
        else if (!drowned) // revert to normal view
        {
            volume.profile = originalVolumeProfile;
            started = false;
        }

        if (updating)
            UpdateVolume();
    }

    void UpdateVolume()
    {
        // volume weight scales over time
        timer += Time.deltaTime;
        float t = timer / transitionDuration;
        newWeight = Mathf.Lerp(0, 1, t);
        volume.weight = newWeight;

        // ending condition
        if (t >= transitionDuration)
        {
            updating = false;
            timer = 0f;
            volume.weight = 1f;
        }
    }
}
