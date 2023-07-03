using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.Build.Content;

public class villagerTrading : MonoBehaviour
{
    public GameObject player;
    private playerInventory playerInventory;
    private string[] greetings = { "Hello!", "Hi!", "Hey!", "Greetings!", "Good to see you!" };
    private string[] fishTypes = { "Fish Type 1", "Fish Type 2", "Fish Type 3" };
    public float radiusToTrade = 2.5f; // Set this to whatever radius you want

    void Start()
    {
        playerInventory = player.GetComponent<playerInventory>();
    }

    // Update is called once per frame


    private void Update()
    {
        // Check if "Fire2" was pressed
        if (Input.GetButtonDown("Fire2"))
        {
            // Check if the player is within trading radius
            if (Vector3.Distance(transform.position, player.transform.position) <= radiusToTrade)
            {
                // Generate a random greeting
                string greeting = greetings[Random.Range(0, greetings.Length)];

                // Generate a random fish type
                string fishType = fishTypes[Random.Range(0, fishTypes.Length)];

                // Generate a random number of fish
                int numFish = Random.Range(1, 6);

                // Print the greeting and request
                Debug.Log(greeting + " I want " + numFish + " of " + fishType + ".");

                // Check if the player has enough fish of the desired type
                if (playerInventory.HasFish(fishType, numFish))
                {
                    // Remove the fish from the player's inventory
                    playerInventory.RemoveFish(fishType, numFish);

                    int randomPayoutMultiplier = Random.Range(1, 5);

                    // Give the player 5 money
                    playerInventory.AddMoney(randomPayoutMultiplier * numFish);
                }
                else
                {
                    Debug.Log("You don't have enough " + fishType + ".");
                    if (Random.value < 0.9f)
                    {
                        playerInventory.AddMoney(1); // debugging code randomly give player gift
                        Debug.Log("But go ahead and take this gift to help you.");
                    }
                    else
                    {
                        for (int i = 0; i < numFish; i++)
                        {
                            playerInventory.RemoveFish(fishType);
                        }
                    }
                }

                //// Call the AddMoney or RemoveFish function in the PlayerInventory script
                //if (Random.value < 0.5f)
                //{
                //    playerInventory.AddMoney(numFish*10);
                //}
                //else
                //{
                //    for (int i = 0; i < numFish; i++)
                //    {
                //        playerInventory.RemoveFish(fishType);
                //    }
                //}
            }
        }
    }

    //private void Update()
    //{
    //    // Check if "Fire2" was pressed
    //    if (Input.GetButtonDown("Fire2"))
    //    {

    //        // Check if the mouse is over this GameObject
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        // Draw a debug ray in the Scene view
    //        Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 1f);


    //        //if (Physics.Raycast(ray, out hit) && hit.transform == transform)
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            // Log the name of the hit object
    //            Debug.Log("Hit: " + hit.transform.name);

    //            if (hit.transform == transform)
    //            {
    //                Debug.Log("yop");
    //                // Generate a random greeting
    //                string greeting = greetings[Random.Range(0, greetings.Length)];

    //                // Generate a random fish type
    //                string fishType = fishTypes[Random.Range(0, fishTypes.Length)];

    //                // Generate a random number of fish from 1 to 5
    //                int numFish = Random.Range(1, 6);

    //                // Print the greeting and request
    //                Debug.Log(greeting + " I want " + numFish + " of " + fishType + ".");

    //                // Call the AddMoney or RemoveFish function in the PlayerInventory script
    //                if (Random.value < 0.5f)
    //                {
    //                    playerInventory.AddMoney(numFish * 10);
    //                }
    //                else
    //                {
    //                    for (int i = 0; i < numFish; i++)
    //                    {
    //                        playerInventory.RemoveFish(fishType);
    //                    }
    //                }
    //            }

    //        }
    //    }
    //}
}