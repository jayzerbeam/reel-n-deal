using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuCamera : MonoBehaviour
{
    public Transform mapCenter; // empty gameObejct at map center
    public float rotationSpeed = 1f;
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - mapCenter.position;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.Euler(0f, rotationSpeed * Time.deltaTime, 0f); // rotate on y-axis
        offset = rotation * offset;
        Vector3 newPosition = mapCenter.position + (rotation * offset); // camera position will be based on mapCenter + offset + rotation
        transform.position = newPosition;
        transform.LookAt(mapCenter);
    }
}
