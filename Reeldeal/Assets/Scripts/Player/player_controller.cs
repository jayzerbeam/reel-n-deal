using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    Rigidbody rb;
    public float movementSpeed = 6f;
    public float jumpForce = 5f;

    public LayerMask ground;

    //private bool grounded = false;
    private float groundDisCheck;
    //private float fudgeFactRayCast = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

        //groundDisCheck = (GetComponent<CapsuleCollider>().height / 2) + fudgeFactRayCast;

        //if (Input.GetButtonDown("Jump") && IsGrounded())
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, transform.up, groundDisCheck))
        {
            return true;
        } else
        {
            return false;
        }
    }
}