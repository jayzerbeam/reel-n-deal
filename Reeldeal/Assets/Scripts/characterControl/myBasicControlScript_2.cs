using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class myBasicControlScript_2 : MonoBehaviour
{
    public GameObject bobber;
    public float walkSpeed = 2f;
    public float runSpeed = 6f;
    public float turnSmoothTime = 0.2f;
    public float speedSmoothTime = 0.1f;
    public float jumpHeight = 2f;
    public float gravity = -12f;

    //[HideInInspector]
    public bool isGrounded;
    //[HideInInspector]
    public bool isInWater;
    public float waterTimer = 2f;
    public float groundY;
    public float waterY;

    private float turnSmoothVelocity;
    private float speedSmoothVelocity;
    private float currentSpeed;
    private float velocityY;

    //private Animator animator;
    private Transform cameraT;
    private Rigidbody rigidBody;

    private void Start()
    {
        //animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // movement input
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        // rotation
        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        // running
        bool running = Input.GetButton("Fire3");
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        // apply gravity before moving
        velocityY += Time.deltaTime * gravity;

        // jump
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
                velocityY = jumpVelocity;
            }
            else if (isInWater && waterTimer > 0)
            {
                waterTimer -= Time.deltaTime;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            }
        }

        // adjust vertical velocity for water
        if (isInWater && transform.position.y < waterY)
        {
            velocityY = 0;
            transform.position = new Vector3(transform.position.x, waterY, transform.position.z);
            waterTimer = 2f;
        }

        // move the character
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;
        transform.Translate(velocity * Time.deltaTime, Space.World);

        // spawn bobber
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bobber, transform.position, Quaternion.identity);
        }

        // animator
        float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
        //animator.SetFloat("vely", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // check if character is grounded
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, LayerMask.GetMask("Ground"));

        // check if character is in water
        isInWater = Physics.CheckSphere(transform.position, 0.1f, LayerMask.GetMask("Water"));

        // store y coordinates
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f, LayerMask.GetMask("Ground")))
        {
            groundY = hit.point.y;
        }
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f, LayerMask.GetMask("Water")))
        {
            waterY = hit.point.y;
        }
    }
}
