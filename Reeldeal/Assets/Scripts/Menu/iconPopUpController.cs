using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class iconPopUpController : MonoBehaviour
{

    public GameObject player;
    public GameObject targetObject;
    public Image iconImage;
    public float rangeFromObject = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, targetObject.transform.position);

        if (distance <= rangeFromObject)
        {
            iconImage.enabled = true;

            // Position the icon above the target object
            Vector3 iconPosition = targetObject.transform.position + Vector3.up * 2f;
            transform.position = iconPosition;
        }
        else
        {
            iconImage.enabled = false;
        }
    }
}
