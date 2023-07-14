using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Following links gave useful background on Unity audio techniques\
// learn.unity.com/tutorial/sound-effects-scripting#
// https://gamedevbeginner.com/how-to-play-audio-in-unity-with-examples/
public class sharkAudioRandomizer : MonoBehaviour
{
    public float frequency;
    public float pitch;
    private GameObject player;
    public bool update = false;

    // Start is called before the first frame update
    void Start()
    {
        frequency = 10;
        SharkAudioManager.PlayStartSound(transform.position, 1f);
        
        // get player gameObject
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform != null) // player must exist to run
        {
            float playerDistance = Vector3.Distance(transform.position, player.transform.position);
            float r = Random.Range(0, playerDistance); // plays sound if r matches required threshold
            if (r < 1)
            {
                // update pitch, but not frequently enough to cause audio to be skipped
                if (playerDistance < 10)
                    SharkAudioManager.PlayLoopSound(transform.position, 1.3f);
                else if (playerDistance < 20)
                    SharkAudioManager.PlayLoopSound(transform.position, 1.2f);
                else if (playerDistance < 40)
                    SharkAudioManager.PlayLoopSound(transform.position, 1.1f);
                else
                    SharkAudioManager.PlayLoopSound(transform.position, 1f);
            }     
        }
    }
}
