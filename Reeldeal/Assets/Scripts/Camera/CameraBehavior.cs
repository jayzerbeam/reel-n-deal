using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    Vector3 camPosition = new Vector3(0f, 3f, -5f);
    Transform _target;
    GameObject _player;
    GameObject _bobber;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _target = _player.transform;
    }

    void Update()
    {
        _bobber = GameObject.FindWithTag("Bobber");
        SetCamPosition();
        HandleChangeTarget();
    }

    void HandleChangeTarget()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            if (_bobber != null && _target == _player.transform)
            {
                _target = _bobber.transform;
            }
            else if (_bobber == null || (_bobber != null && _target == _bobber.transform))
            {
                _target = _player.transform;
            }
        }
    }

    void SetCamPosition()
    {
        // Camera is on bobber
        if (_bobber != null && _target == _bobber.transform)
        {
            camPosition = new Vector3(0f, 20f, -10f);
        }
        // Camera is on fishing player
        else if (_bobber != null && _target == _player.transform)
        {
            camPosition = new Vector3(0f, 6f, -5f);
        }
        // Camera is on non-fishing player
        else
        {
            camPosition = new Vector3(0f, 3f, -5f);
        }
        this.transform.position = _target.TransformPoint(camPosition);
        this.transform.LookAt(_target);
    }
}
