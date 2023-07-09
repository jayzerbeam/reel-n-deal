using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Vector3 CamOffset = new Vector3(0f, 3f, -5f);
    private Transform _target;
    private GameObject _player;
    private GameObject _bobber;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        GetTarget();
    }

    void GetTarget()
    {
        // Disabling for now - do not want this to affect the camera on main.
        _bobber = GameObject.FindWithTag("Bobber");

        if (_bobber)
        {
            _target = _bobber.transform;
            CamOffset = new Vector3(0f, 40f, 35f);
        }
        else
        {
            _target = _player.transform;
            CamOffset = new Vector3(0f, 3, -5f);
        }
    }

    void LateUpdate()
    {
        this.transform.position = _target.TransformPoint(CamOffset);
        this.transform.LookAt(_target);
    }
}
