using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerInventory : MonoBehaviour
{
    // The amount of money the player has
    public int money = 0; // change to private after debugging
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI debugFishList;

    // A dictionary to store fish by type. Each fish type maps to a list of fish details.
    // when a fish is caught there will be a string containing details of the fish in the form "fish name, fish type, fish location, fish color, fish size, fish sex, fish age, fish time caught"
    public Dictionary<string, List<string>> fishInventory = new Dictionary<string, List<string>>(); // cahnge to private after debugging

    // Start is called before the first frame update
    void Start()
    {
        UpdateMoneyText();
        UpdateFishInvText();
    }

    // Update is called once per frame
    void Update() { }

    // Function to add money to the player's inventory
    public void AddMoney(int amount)
    {
        money += amount;
        // Update the money text whenever the player's money changes
        UpdateMoneyText();
    }

    // Function to subtract money from the player's inventory
    public void SubtractMoney(int amount)
    {
        // Ensure the player has enough money before subtracting
        if (money >= amount)
        {
            money -= amount;
            UpdateMoneyText(); // Update the money text whenever the player's money changes
        }
        else
        {
            Debug.Log("Oops you're broke, not enough money to complete transaction.");
        }
    }

    // Function to update the money text
    private void UpdateMoneyText()
    {
        moneyText.text = "Money: " + money.ToString();
    }

    private void UpdateFishInvText()
    {
        // Clear the existing text
        debugFishList.text = "";

        // Append each item from the list
        foreach (KeyValuePair<string, List<string>> kvp in fishInventory)
        {
            // Append the key and value to the Text component
            debugFishList.text += kvp.Key + ": " + kvp.Value.ToString() + "\n";
        }
    }

    // Function to add a fish to the player's inventory
    public void AddFishedFish(string fishDetails)
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
        UpdateFishInvText();
    }

    // Function to add a certain amount of a certain type of fish to the player's inventory
    public void AddFish(string fishType, int amount = 1)
    {
        // If this type of fish isn't in the inventory yet, add it
        if (!fishInventory.ContainsKey(fishType))
        {
            fishInventory[fishType] = new List<string>();
        }

        // Add the specified amount of fish to the list for this fish type
        for (int i = 0; i < amount; i++)
        {
            fishInventory[fishType].Add(fishType);
        }
        UpdateFishInvText();
    }

    // Function to remove a certain amount of a certain type of fish from the player's inventory
    public void RemoveFish(string fishType, int amount = 1)
    {
        // Ensure this type of fish is in the inventory before trying to remove it
        if (fishInventory.ContainsKey(fishType) && fishInventory[fishType].Count >= amount)
        {
            // Remove the specified amount of fish
            for (int i = 0; i < amount; i++)
            {
                fishInventory[fishType].RemoveAt(0);
            }
            UpdateFishInvText();
        }
        else
        {
            Debug.Log("Go fish!, not enough fish of this type in inventory.");
        }
    }

    // Function to check if the player has a certain amount of a certain type of fish
    public bool HasFish(string fishType, int amount)
    {
        // Check if the player has any fish of this type
        if (fishInventory.ContainsKey(fishType))
        {
            // Check if the player has enough fish of this type
            if (fishInventory[fishType].Count >= amount)
            {
                return true;
            }
        }

        return false;
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
        UpdateFishInvText();
    }
}
