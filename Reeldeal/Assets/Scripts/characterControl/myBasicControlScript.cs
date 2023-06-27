using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class myBasicControlScript : MonoBehaviour
{
    public GameObject bobberPrefab;
    public LayerMask groundLayer;
    public LayerMask waterLayer;
    private Rigidbody rb;
    private Animator anim;
    private Transform leftFoot;
    private Transform rightFoot;
    private bool isGrounded;
    private bool isInWater;
    private float waterTime;
    private float maxJumpHeight;
    private float startHeight;
    public float jumpStrength = 4f;
    public float timeToSink = 2f;
    public float sinkBy = 0.01f;
    public float floatUpBy = 0.05f;
    public float speed = 5f; 
    public float rotationSpeed = 200f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        startHeight = transform.position.y;
        maxJumpHeight = startHeight + (4 * GetComponent<CapsuleCollider>().height);
        //example of how to get access to certain limbs
        leftFoot = this.transform.Find("mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftLeg/mixamorig:LeftFoot");
        rightFoot = this.transform.Find("mixamorig:Hips/mixamorig:RightUpLeg/mixamorig:RightLeg/mixamorig:RightFoot");

        if (leftFoot == null || rightFoot == null)
            Debug.Log("One of the feet could not be found");

        anim.applyRootMotion = false;
    }

    void Update()
    {
        float inputForward = Input.GetAxis("Vertical");
        float inputTurn = Input.GetAxis("Horizontal");

        anim.SetFloat("velx", inputTurn);
        anim.SetFloat("vely", Mathf.Abs(inputForward) > 0.05f ? 1.0f : 0.5f);
        anim.SetBool("isFalling", !isGrounded);

        rb.MovePosition(rb.position + transform.forward * inputForward * speed * Time.deltaTime);

        rb.rotation = Quaternion.Euler(0f, inputTurn * rotationSpeed * Time.deltaTime, 0f);


        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector3(0, jumpStrength, 0), ForceMode.Impulse);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bobberPrefab, transform.position + transform.forward, Quaternion.identity);
        }

        CheckGrounded();
        CheckInWater();

        if (isInWater)
        {
            if (waterTime < timeToSink)
            {
                waterTime += Time.deltaTime;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - sinkBy, transform.position.z);
            }

            if (Input.GetButtonDown("Jump"))
            {
                waterTime = 0;
                transform.position = new Vector3(transform.position.x, transform.position.y + floatUpBy, transform.position.z);
            }
        }

        // prevent flying
        if (transform.position.y > maxJumpHeight)
        {
            transform.position = new Vector3(transform.position.x, maxJumpHeight, transform.position.z);
        }

        // falling through world
        //if (!isGrounded && !isInWater)
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(transform.position, Vector3.down, out hit))
        //    {
        //        transform.position = hit.point;
        //    }
        //}
    }

    void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void CheckInWater()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1, waterLayer))
        {
            isInWater = true;
        }
        else
        {
            isInWater = false;
        }
    }
}
