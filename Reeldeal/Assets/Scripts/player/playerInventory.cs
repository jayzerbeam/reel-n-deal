using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    // The amount of money the player has
    public int money; // change to private after debugging

    // A dictionary to store fish by type. Each fish type maps to a list of fish details.
    // when a fish is caught there will be a string containing details of the fish in the form "fish name, fish type, fish location, fish color, fish size, fish sex, fish age, fish time caught"
    public Dictionary<string, List<string>> fishInventory = new Dictionary<string, List<string>>(); // cahnge to private after debugging

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Function to add money to the player's inventory
    public void AddMoney(int amount)
    {
        money += amount;
    }

    // Function to subtract money from the player's inventory
    public void SubtractMoney(int amount)
    {
        // Ensure the player has enough money before subtracting
        if (money >= amount)
        {
            money -= amount;
        }
        else
        {
            Debug.Log("Oops you're broke, not enough money to complete transaction.");
        }
    }

    // Function to add a fish to the player's inventory
    public void AddFish(string fishDetails)
    {
        // Split the fish details string into individual csv, string type vals
        string[] details = fishDetails.Split(',');

        // The fish type is the second item in the string
        string fishType = details[1].Trim();

        // If this type of fish isn't in the inventory yet, add it
        if (!fishInventory.ContainsKey(fishType))
        {
            fishInventory[fishType] = new List<string>();
        }

        // Add the fish details to the list for this fish type
        fishInventory[fishType].Add(fishDetails);
    }

    // Function to remove a fish from the player's inventory
    public void RemoveFish(string fishType)
    {
        // Ensure this type of fish is in the inventory before trying to remove it
        if (fishInventory.ContainsKey(fishType) && fishInventory[fishType].Count > 0)
        {
            fishInventory[fishType].RemoveAt(0);
        }
        else
        {
            Debug.Log("Go fish!, no fish of this type in inventory.");
        }
    }

    // Function to remove a random percentage of fish from the inventory when the player dies
    public void PlayerDeath()
    {
        // Calculate the percentage of fish to remove
        float percentageToRemove = Random.Range(0.45f, 0.55f);

        // Iterate over each fish type in the inventory
        foreach (string fishType in new List<string>(fishInventory.Keys))
        {
            // Calculate how many fish to remove of this type
            int fishToRemove = Mathf.RoundToInt(fishInventory[fishType].Count * percentageToRemove);

            // Remove the calculated number of fish
            fishInventory[fishType].RemoveRange(0, fishToRemove);
        }
    }
}

