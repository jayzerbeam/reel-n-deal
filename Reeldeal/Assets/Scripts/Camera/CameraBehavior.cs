using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Vector3 CamOffset = new Vector3(0f, 3f, -5f);
    private Transform _target;
    public GameObject player_obj;

    // Start is called before the first frame update
    void Start()
    {
        _target = player_obj.transform; 
        //_target = GameObject.Find("Player_InputSystem").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = _target.TransformPoint(CamOffset);
        this.transform.LookAt(_target);
    }
}