using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class waveSingle : MonoBehaviour
{
    public static waveSingle instance;

    public float amplitude = 1f;
    public float length = 2f;
    public float speed = 1f;
    public float offset = 0f;
    public float frequency = 1f;

    // Update is called once per frame
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
            Debug.Log("Wave instance already exists, removing");
        }
    }

    private void Update()
    {
        offset += Time.deltaTime * speed;
    }

    public float ReturnWavePos(float calc_x)
    {
        return amplitude * Mathf.Sin(calc_x / length + offset);
    }
}
