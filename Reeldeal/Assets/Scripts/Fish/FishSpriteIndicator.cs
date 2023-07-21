using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpriteIndicator : MonoBehaviour
{
    // https://docs.unity3d.com/Manual/class-SpriteRenderer.html

    public Sprite[] sprites;
    private Transform cameraTransform;
    private FishAI fishAI;
    public int n = 0;

    // Start is called before the first frame update
    void Start()
    {
        fishAI = GetComponentInParent<FishAI>();

        // get camera info
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraTransform = camera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //get AI state
        FishAI.AIState aiState = fishAI.aiState;

        switch (aiState)
        {
            case FishAI.AIState.hungryState:
                GetComponent<SpriteRenderer>().enabled = true;
                n = 0;
                break;
            case FishAI.AIState.fleeState:
                GetComponent<SpriteRenderer>().enabled = true;
                n = 1;
                break;
            default:
                GetComponent<SpriteRenderer>().enabled = false;
                break;
        }

        // set sprite, rotation, and scale
        GetComponent<SpriteRenderer>().sprite = sprites[n];
        transform.LookAt(cameraTransform.position);
        float distance = Vector3.Distance(cameraTransform.position, transform.position);
        float scale;
        if (n == 0)
            scale = distance / 10f * 0.4f;
        else
            scale = distance / 10f;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
