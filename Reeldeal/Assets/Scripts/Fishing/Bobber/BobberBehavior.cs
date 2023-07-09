using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberBehavior : MonoBehaviour
{
    Rigidbody _rb;

    [SerializeField]
    float _gravity = -9.81f;
    float _groundedGravity = 0.05f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleGravity();
    }

    void HandleGravity()
    {
        Vector3 gravityForce = _gravity * Vector3.up;
        _rb.AddForce(gravityForce, ForceMode.Acceleration);
    }
}
