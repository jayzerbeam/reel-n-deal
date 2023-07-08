using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class WaterVolumeUpdater : MonoBehaviour
{
    public Volume volume;
    public VolumeProfile originalVolumeProfile;
    public VolumeProfile waterVolumeProfile;

    public float transitionDuration = 1f;
    private bool transitioning = false;
    public float newWeight;
    public string transition = null;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter(Collider collider)
    {
        
        if (collider.CompareTag("Player"))
        {
            if (!transitioning)
            {
                transition = "in";
                volume.profile = waterVolumeProfile;
                updateVolume();

            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
       
        if (collider.CompareTag("Player"))
        {
            if (!transitioning)
            {
                transition = "out";
                updateVolume();
            }
        }
    }

    void updateVolume()
    {
        transitioning = true;
        timer = 0f;
    }

    void Update()
    {
        if (transitioning)
        {
            timer += Time.deltaTime;
            float t = timer / transitionDuration;
            if (transition == "in")
            {
                newWeight = Mathf.Lerp(0, 1, t);
            }
            else if (transition == "out")
            {
                newWeight = Mathf.Lerp(1, 0, t);
            }
            
            volume.weight = newWeight;

            if (timer >= transitionDuration)
            {
                transitioning = false;
                if (transition == "out")
                {
                    volume.profile = originalVolumeProfile;
                }
                transition = null;
                
            }

        }
    }
}
