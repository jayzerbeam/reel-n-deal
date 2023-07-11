using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberBehavior : MonoBehaviour
{
    Rigidbody _rb;
    string[] _tags = { "Ground", "River", "Lake", "Ocean", "Spring" };

    [SerializeField]
    float _gravity = -9.81f;
    float _groundedGravity = 0.05f;

    private bool _isInWater = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Modified from here:
    // https://stackoverflow.com/questions/63036126/floating-objects-in-unity#63043878
    void HandleFloat()
    {
        Vector3 floatGravity = -Physics.gravity * _rb.mass * (-_rb.velocity.y * 10f);
        _rb.AddForceAtPosition(floatGravity, transform.position);
    }

    void FixedUpdate()
    {
        if (_isInWater)
        {
            HandleFloat();
        }
        else
        {
            HandleGravity();
        }
    }

    void HandleGravity()
    {
        Vector3 gravityForce = _gravity * Vector3.up;
        _rb.AddForce(gravityForce, ForceMode.Acceleration);
    }

    private void FreezeBobber()
    {
        _rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnCollisionEnter(Collision collision)
    {
        LoopTags(collision.gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        LoopTags(collider.gameObject);
    }

    private void LoopTags(GameObject gameObject)
    {
        foreach (string tag in _tags)
        {
            if (gameObject.CompareTag(tag) && tag == "Ground")
            {
                FreezeBobber();
                _isInWater = false;
            }
            else if (gameObject.CompareTag(tag))
            {
                FreezeBobber();
                _isInWater = true;
            }
        }
    }
}
