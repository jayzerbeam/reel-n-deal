using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class myPlayerControlScript : MonoBehaviour
{
    public float speed = 10f; // Speed of the player
    public float rotationSpeed = 150f; // Speed of rotation of the player
    public float jumpStrength = 70f; // Jumping force
    public float distanceCanJump = 0.25f; // Distance to ground to enable jumping
    public LayerMask groundLayer; // Layer for the ground
    public LayerMask waterLayer; // Layer for the water
    public float distanceToGround; // Distance to ground
    public float allowedAirTime = 5f; // Maximum time player can be in the air
    public float airTimeCounter; // Time counter for player's air time
    private bool jumpRequest = false; // Flag to handle jump requests
    public GameObject bobber;
    //private GameObject waterCountdownGUI;

    //private bool swimRequest = false;
    private float waterTime;
    public float timeToSink = 2f;
    public float sinkBy = 0.01f;
    public float floatUpBy = 0.05f;

    private Rigidbody rb;
    public bool onGround;
    public bool onWater;

    // Start is called before the first frame update
    void Start()
    {
        bobber = GameObject.FindWithTag("Bobber");
        rb = GetComponent<Rigidbody>();
        //waterCountdownGUI = GameObject.Find("WaterCountdownGUI");

    }

    //// Update is called once per frame
    //void Update()
    //{
    //    MovePlayer();
    //    JumpPlayer();

    //    CheckGround();
    //    CheckWater();

    //    if (!onGround)
    //    {
    //        airTimeCounter += Time.deltaTime;
    //    }
    //}

    void Update()
    {
        MovePlayer();

        // Capture jump input in Update
        // if (!onWater && Input.GetButtonDown("Jump"))
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequest = true;
        }

        CheckGround();
        //CheckWater();

        if (!onGround)
        {
            airTimeCounter += Time.deltaTime;
        }

        //if (onWater && Input.GetButtonDown("Jump"))
        //{
        //    swimRequest = true;
        //}

        // spawn bobber
        //if (onGround && Input.GetButtonDown("Fire1"))
        //{
        //    Instantiate(bobber, transform.position, Quaternion.identity);
        //}

        if (onWater)
        {
            waterTime += Time.deltaTime;
            //if (waterCountdownGUI != null)
            //{
            //    waterCountdownGUI.SetActive(true);
            //    waterCountdownGUI.GetComponentInChildren<TextMesh>().text = waterTime.ToString("F1");
            //}
            if (waterTime >= 3f)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        if (!onWater)
        {
            //if (waterCountdownGUI != null)
            //{
            //    waterCountdownGUI.SetActive(false);
            //}
            waterTime = 0f; 
        }
    }

    // Handle jump in FixedUpdate
    void FixedUpdate()
    {
        JumpPlayer();
        //if (onWater && waterTime < timeToSink)
        //{
        //    waterTime += Time.deltaTime;
        //}
        //else if (onWater && waterTime > timeToSink)
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y - sinkBy, transform.position.z);
        //}
        //SwimPlayer();
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Rotate player
        transform.Rotate(0, moveHorizontal * rotationSpeed * Time.deltaTime, 0);
        // Move player forward or backward
        transform.Translate(0, 0, moveVertical * speed * Time.deltaTime);
    }

    void JumpPlayer()
    {
        //if (onGround && jumpRequest || Input.GetButtonDown("Jump"))
        if (onGround && jumpRequest)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            jumpRequest = false; // Reset jump request after it's handled
        }
    }

    //void SwimPlayer()
    //{
    //    if (swimRequest)
    //    {
    //        waterTime = 0;
    //        transform.position = new Vector3(transform.position.x, transform.position.y + floatUpBy, transform.position.z);
    //    }
    //}

    void CheckGround()
    {
        RaycastHit hit;
        if (
            Physics.Raycast(transform.position, Vector3.down, out hit, distanceCanJump, groundLayer)
        )
        {
            distanceToGround = hit.distance; // Save the distance to the ground
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                onGround = true;
                airTimeCounter = 0; // Reset air time counter when player is on the ground
                return;
            }
        }
        onGround = false;

        // Teleport to nearest ground position if not on ground and air time exceeds allowed air time
        if (
            !onGround
            && airTimeCounter > allowedAirTime
            && Physics.Raycast(
                transform.position,
                Vector3.down,
                out hit,
                Mathf.Infinity,
                groundLayer
            )
        )
        {
            distanceToGround = hit.distance; // Save the distance to the ground
            transform.position = hit.point;
        }

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.05f, waterLayer))
        {
            onWater = true;
            onGround = false;
        }
        else if (Physics.Raycast(transform.position, Vector3.up, out hit, 55f, waterLayer))
        {
            onWater = true;
            onGround = false;
        }
        else
        {
            onWater = false;
        }
    }

    void CheckWater()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, waterLayer))
        {
            onWater = true;
        }
        else if (Physics.Raycast(transform.position, Vector3.up, out hit, 55f, waterLayer))
        {
            onWater = true;
        }
        else
        {
            onWater = false;
        }
        
    }


}
