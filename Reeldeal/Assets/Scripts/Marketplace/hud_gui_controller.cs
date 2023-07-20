using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class hud_gui_controller : MonoBehaviour
{

    public GameObject inventoryHUD;
    public TextMeshProUGUI coin_count_disp;
    public TextMeshProUGUI lake_f_1_disp;
    public TextMeshProUGUI lake_f_2_disp;
    public TextMeshProUGUI lake_f_3_disp;
    public TextMeshProUGUI lake_f_4_disp;
    public TextMeshProUGUI ocean_f_1_disp;
    public TextMeshProUGUI ocean_f_2_disp;
    public TextMeshProUGUI ocean_f_3_disp;
    public TextMeshProUGUI ocean_f_4_disp;
    public TextMeshProUGUI ocean_shark_disp;
    public TextMeshProUGUI river_f_1_disp;
    public TextMeshProUGUI river_f_2_disp;
    public TextMeshProUGUI river_f_3_disp;
    public TextMeshProUGUI river_f_4_disp;

    private bool isInventoryVisible;

    public int coin_Count = 0;
    public int lake_f_1_count = 0;
    public int lake_f_2_count = 0;
    public int lake_f_3_count = 0;
    public int lake_f_4_count = 0;
    public int ocean_f_1_count = 0;
    public int ocean_f_2_count = 0;
    public int ocean_f_3_count = 0;
    public int ocean_f_4_count = 0;
    public int ocean_shark_count = 0;
    public int river_f_1_count = 0;
    public int river_f_2_count = 0;
    public int river_f_3_count = 0;
    public int river_f_4_count = 0;



    private void Start()
    {

        isInventoryVisible = false;
        inventoryHUD.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory1"))
        {
            isInventoryVisible = !isInventoryVisible;
            UpdateHUDInventoryGUI();
        }
    }

    public void AddItemToInv(string item_type, int item_amount)
    {

        switch (item_type)
        {
            case "coin":
                coin_Count += item_amount;
                Debug.Log("Added " + item_amount + " coin(s). Total coins: " + coin_Count);
                break;
            case "lake_f_1":
                lake_f_1_count += item_amount;
                Debug.Log("Added " + item_amount + " lake_f_1(s). Total lake_f_1: " + lake_f_1_count);
                break;
            case "lake_f_2":
                lake_f_2_count += item_amount;
                Debug.Log("Added " + item_amount + " lake_f_2(s). Total lake_f_2: " + lake_f_2_count);
                break;
            case "lake_f_3":
                lake_f_3_count += item_amount;
                Debug.Log("Added " + item_amount + " lake_f_3(s). Total lake_f_3: " + lake_f_3_count);
                break;
            case "lake_f_4":
                lake_f_4_count += item_amount;
                Debug.Log("Added " + item_amount + " lake_f_4(s). Total lake_f_4: " + lake_f_4_count);
                break;
            case "ocean_f_1":
                ocean_f_1_count += item_amount;
                Debug.Log("Added " + item_amount + " ocean_f_1(s). Total ocean_f_1: " + ocean_f_1_count);
                break;
            case "ocean_f_2":
                ocean_f_2_count += item_amount;
                Debug.Log("Added " + item_amount + " ocean_f_2(s). Total ocean_f_2: " + ocean_f_2_count);
                break;
            case "ocean_f_3":
                ocean_f_3_count += item_amount;
                Debug.Log("Added " + item_amount + " ocean_f_3(s). Total ocean_f_3: " + ocean_f_3_count);
                break;
            case "ocean_f_4":
                ocean_f_4_count += item_amount;
                Debug.Log("Added " + item_amount + " ocean_f_4(s). Total ocean_f_4: " + ocean_f_4_count);
                break;
            case "ocean_shark":
                ocean_shark_count += item_amount;
                Debug.Log("Added " + item_amount + " ocean_shark(s). Total ocean_shark: " + ocean_shark_count);
                break;
            case "river_f_1":
                river_f_1_count += item_amount;
                Debug.Log("Added " + item_amount + " river_f_1(s). Total river_f_1: " + river_f_1_count);
                break;
            case "river_f_2":
                river_f_2_count += item_amount;
                Debug.Log("Added " + item_amount + " river_f_2(s). Total river_f_2: " + river_f_2_count);
                break;
            case "river_f_3":
                river_f_3_count += item_amount;
                Debug.Log("Added " + item_amount + " river_f_3(s). Total river_f_3: " + river_f_3_count);
                break;
            case "river_f_4":
                river_f_4_count += item_amount;
                Debug.Log("Added " + item_amount + " river_f_4(s). Total river_f_4: " + river_f_4_count);
                break;
            default:
                Debug.Log("Invalid item type: " + item_type);
                break;
        }

        coin_count_disp.text = coin_Count.ToString();
        lake_f_1_disp.text = lake_f_1_count.ToString();
        lake_f_2_disp.text = lake_f_2_count.ToString();
        lake_f_3_disp.text = lake_f_3_count.ToString();
        lake_f_4_disp.text = lake_f_4_count.ToString();
        ocean_f_1_disp.text = ocean_f_1_count.ToString();
        ocean_f_2_disp.text = ocean_f_2_count.ToString();
        ocean_f_3_disp.text = ocean_f_3_count.ToString();
        ocean_f_4_disp.text = ocean_f_4_count.ToString();
        ocean_shark_disp.text = ocean_shark_count.ToString();
        river_f_1_disp.text = river_f_1_count.ToString();
        river_f_2_disp.text = river_f_2_count.ToString();
        river_f_3_disp.text = river_f_3_count.ToString();
        river_f_4_disp.text = river_f_4_count.ToString();
}

    public void RemoveItemToInv(string item_type, int item_amount)
    {
        //if (isCollectable)
        //{
        //    itemCount--;
        //}
        //else
        //    collectedItems.Remove(itemName);

        switch (item_type)
        {
            case "coin":
                coin_Count -= item_amount;
                coin_Count = Mathf.Max(coin_Count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " coin(s). Total coins: " + coin_Count);
                break;
            case "lake_f_1":
                lake_f_1_count -= item_amount;
                lake_f_1_count = Mathf.Max(lake_f_1_count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " lake_f_1(s). Total lake_f_1: " + lake_f_1_count);
                break;
            case "lake_f_2":
                lake_f_2_count -= item_amount;
                lake_f_2_count = Mathf.Max(lake_f_2_count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " lake_f_2(s). Total lake_f_2: " + lake_f_2_count);
                break;
            case "lake_f_3":
                lake_f_3_count -= item_amount;
                lake_f_3_count = Mathf.Max(lake_f_3_count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " lake_f_3(s). Total lake_f_3: " + lake_f_3_count);
                break;
            case "lake_f_4":
                lake_f_4_count -= item_amount;
                lake_f_4_count = Mathf.Max(lake_f_4_count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " lake_f_4(s). Total lake_f_4: " + lake_f_4_count);
                break;
            case "ocean_f_1":
                ocean_f_1_count -= item_amount;
                ocean_f_1_count = Mathf.Max(ocean_f_1_count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " ocean_f_1(s). Total ocean_f_1: " + ocean_f_1_count);
                break;
            case "ocean_f_2":
                ocean_f_2_count -= item_amount;
                ocean_f_2_count = Mathf.Max(ocean_f_2_count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " ocean_f_2(s). Total ocean_f_2: " + ocean_f_2_count);
                break;
            case "ocean_f_3":
                ocean_f_3_count -= item_amount;
                ocean_f_3_count = Mathf.Max(ocean_f_3_count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " ocean_f_3(s). Total ocean_f_3: " + ocean_f_3_count);
                break;
            case "ocean_f_4":
                ocean_f_4_count -= item_amount;
                ocean_f_4_count = Mathf.Max(ocean_f_4_count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " ocean_f_4(s). Total ocean_f_4: " + ocean_f_4_count);
                break;
            case "ocean_shark":
                ocean_shark_count -= item_amount;
                ocean_shark_count = Mathf.Max(ocean_shark_count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " ocean_shark(s). Total ocean_shark: " + ocean_shark_count);
                break;
            case "river_f_1":
                river_f_1_count -= item_amount;
                river_f_1_count = Mathf.Max(river_f_1_count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " river_f_1(s). Total river_f_1: " + river_f_1_count);
                break;
            case "river_f_2":
                river_f_2_count -= item_amount;
                river_f_2_count = Mathf.Max(river_f_2_count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " river_f_2(s). Total river_f_2: " + river_f_2_count);
                break;
            case "river_f_3":
                river_f_3_count -= item_amount;
                river_f_3_count = Mathf.Max(river_f_3_count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " river_f_3(s). Total river_f_3: " + river_f_3_count);
                break;
            case "river_f_4":
                river_f_4_count -= item_amount;
                river_f_4_count = Mathf.Max(river_f_4_count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " river_f_4(s). Total river_f_4: " + river_f_4_count);
                break;
            default:
                Debug.Log("Invalid item type: " + item_type);
                break;
        }

        coin_count_disp.text = coin_Count.ToString();
        lake_f_1_disp.text = lake_f_1_count.ToString();
        lake_f_2_disp.text = lake_f_2_count.ToString();
        lake_f_3_disp.text = lake_f_3_count.ToString();
        lake_f_4_disp.text = lake_f_4_count.ToString();
        ocean_f_1_disp.text = ocean_f_1_count.ToString();
        ocean_f_2_disp.text = ocean_f_2_count.ToString();
        ocean_f_3_disp.text = ocean_f_3_count.ToString();
        ocean_f_4_disp.text = ocean_f_4_count.ToString();
        ocean_shark_disp.text = ocean_shark_count.ToString();
        river_f_1_disp.text = river_f_1_count.ToString();
        river_f_2_disp.text = river_f_2_count.ToString();
        river_f_3_disp.text = river_f_3_count.ToString();
        river_f_4_disp.text = river_f_4_count.ToString();
    }

    // Function to check if the player has a certain amount of a certain type of fish
    public bool HasFish(string fishType, int amount)
    {
        // Check if the player has any fish of this type
        switch (fishType)
        {
            case "lake_f_1":
                Debug.Log("Player has " + amount + " blue fish(es). Total blue fishes: " + lake_f_1_count);
                if (lake_f_1_count >= amount)
                {
                    return true;
                }
                return false;
            case "lake_f_2":
                Debug.Log("Player has " + amount + " blue fish(es). Total blue fishes: " + lake_f_2_count);
                if (lake_f_2_count >= amount)
                {
                    return true;
                }
                return false;
            case "lake_f_3":
                Debug.Log("Player has " + amount + " blue fish(es). Total blue fishes: " + lake_f_3_count);
                if (lake_f_3_count >= amount)
                {
                    return true;
                }
                return false;
            case "lake_f_4":
                Debug.Log("Player has " + amount + " blue fish(es). Total blue fishes: " + lake_f_4_count);
                if (lake_f_4_count >= amount)
                {
                    return true;
                }
                return false;
            case "ocean_f_1":
                Debug.Log("Player has " + amount + " blue fish(es). Total blue fishes: " + ocean_f_1_count);
                if (ocean_f_1_count >= amount)
                {
                    return true;
                }
                return false;
            case "ocean_f_2":
                Debug.Log("Player has " + amount + " blue fish(es). Total blue fishes: " + ocean_f_2_count);
                if (ocean_f_2_count >= amount)
                {
                    return true;
                }
                return false;
            case "ocean_f_3":
                Debug.Log("Player has " + amount + " blue fish(es). Total blue fishes: " + ocean_f_3_count);
                if (ocean_f_3_count >= amount)
                {
                    return true;
                }
                return false;
            case "ocean_f_4":
                Debug.Log("Player has " + amount + " blue fish(es). Total blue fishes: " + ocean_f_4_count);
                if (ocean_f_4_count >= amount)
                {
                    return true;
                }
                return false;
            case "ocean_shark":
                Debug.Log("Player has " + amount + " blue fish(es). Total blue fishes: " + ocean_shark_count);
                if (ocean_shark_count >= amount)
                {
                    return true;
                }
                return false;
            case "river_f_1":
                Debug.Log("Player has " + amount + " blue fish(es). Total blue fishes: " + river_f_1_count);
                if (river_f_1_count >= amount)
                {
                    return true;
                }
                return false;
            case "river_f_2":
                Debug.Log("Player has " + amount + " blue fish(es). Total blue fishes: " + river_f_2_count);
                if (river_f_2_count >= amount)
                {
                    return true;
                }
                return false;
            case "river_f_3":
                Debug.Log("Player has " + amount + " blue fish(es). Total blue fishes: " + river_f_3_count);
                if (river_f_3_count >= amount)
                {
                    return true;
                }
                return false;
            case "river_f_4":
                Debug.Log("Player has " + amount + " blue fish(es). Total blue fishes: " + river_f_4_count);
                if (river_f_4_count >= amount)
                {
                    return true;
                }
                return false;
            
            
            default:
                Debug.Log("Invalid item type: " + fishType);
                return false; 
        }
    }


    private void UpdateHUDInventoryGUI()
    {
        if (isInventoryVisible)
        {
            inventoryHUD.SetActive(isInventoryVisible);
            Debug.Log("Inventory Opened");

            
        }
        else
        {
            inventoryHUD.SetActive(isInventoryVisible);
            Debug.Log("Inventory Closed");
        }
    }
}