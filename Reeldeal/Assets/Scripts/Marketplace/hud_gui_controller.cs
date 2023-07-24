using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
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


    public GameObject boots_image;
    public GameObject rod_image;
    public GameObject bobber_image;
    public bool has_boots = false;
    public bool has_rod_upgrade = false;
    public int bobber = 0;
    
    private PlayerMove player_move_script;
    private PlayerJump player_jump_script;
    public float walk_multiplier = 1.75f;
    public float jump_multiplier = 1.5f;


    private void Start()
    {

        isInventoryVisible = false;
        boots_image.SetActive(false);
        rod_image.SetActive(false);
        bobber_image.SetActive(false);
        inventoryHUD.SetActive(false);
        player_move_script = FindObjectOfType<PlayerMove>();
        player_jump_script = FindObjectOfType<PlayerJump>();
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

    // Function to check if the Player has at least a certain amount of a certain type of fish
    public bool HasFish(string fishType, int amount)
    {
        // Check if the Player has at least any fish of this type
        switch (fishType)
        {
            case "coin":
                Debug.Log("Does Player have at least " + amount + " coin(s). Total coins: " + coin_Count);
                if (coin_Count >= amount)
                {
                    return true;
                }
                return false;
            case "lake_f_1":
                Debug.Log("Does Player have at least " + amount + " lake_f_1_count fish(es). Total blue fishes: " + lake_f_1_count);
                if (lake_f_1_count >= amount)
                {
                    return true;
                }
                return false;
            case "lake_f_2":
                Debug.Log("Does Player have at least " + amount + " lake_f_2_count fish(es). Total blue fishes: " + lake_f_2_count);
                if (lake_f_2_count >= amount)
                {
                    return true;
                }
                return false;
            case "lake_f_3":
                Debug.Log("Does Player have at least " + amount + " lake_f_3_count fish(es). Total blue fishes: " + lake_f_3_count);
                if (lake_f_3_count >= amount)
                {
                    return true;
                }
                return false;
            case "lake_f_4":
                Debug.Log("Does Player have at least " + amount + " lake_f_4_count fish(es). Total blue fishes: " + lake_f_4_count);
                if (lake_f_4_count >= amount)
                {
                    return true;
                }
                return false;
            case "ocean_f_1":
                Debug.Log("Does Player have at least " + amount + " ocean_f_1_count fish(es). Total blue fishes: " + ocean_f_1_count);
                if (ocean_f_1_count >= amount)
                {
                    return true;
                }
                return false;
            case "ocean_f_2":
                Debug.Log("Does Player have at least " + amount + " ocean_f_2_count fish(es). Total blue fishes: " + ocean_f_2_count);
                if (ocean_f_2_count >= amount)
                {
                    return true;
                }
                return false;
            case "ocean_f_3":
                Debug.Log("Does Player have at least " + amount + " ocean_f_3_count fish(es). Total blue fishes: " + ocean_f_3_count);
                if (ocean_f_3_count >= amount)
                {
                    return true;
                }
                return false;
            case "ocean_f_4":
                Debug.Log("Does Player have at least " + amount + " ocean_f_4_count fish(es). Total blue fishes: " + ocean_f_4_count);
                if (ocean_f_4_count >= amount)
                {
                    return true;
                }
                return false;
            case "ocean_shark":
                Debug.Log("Does Player have at least " + amount + " ocean_shark_count fish(es). Total blue fishes: " + ocean_shark_count);
                if (ocean_shark_count >= amount)
                {
                    return true;
                }
                return false;
            case "river_f_1":
                Debug.Log("Does Player have at least " + amount + " river_f_1_count fish(es). Total blue fishes: " + river_f_1_count);
                if (river_f_1_count >= amount)
                {
                    return true;
                }
                return false;
            case "river_f_2":
                Debug.Log("Does Player have at least " + amount + " river_f_2_count fish(es). Total blue fishes: " + river_f_2_count);
                if (river_f_2_count >= amount)
                {
                    return true;
                }
                return false;
            case "river_f_3":
                Debug.Log("Does Player have at least " + amount + " river_f_3_count fish(es). Total blue fishes: " + river_f_3_count);
                if (river_f_3_count >= amount)
                {
                    return true;
                }
                return false;
            case "river_f_4":
                Debug.Log("Does Player have at least " + amount + " river_f_4_count fish(es). Total blue fishes: " + river_f_4_count);
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

    public void AddItemToInventory(string item_type, int bobber_type = 0)
    {
        switch (item_type)
        {
            case "boots":
                Debug.Log("Added boots");
                has_boots = true;
                boots_image.SetActive(true);
                player_move_script.increaseWalkSpeed(walk_multiplier);
                player_jump_script.increaseJumpHeight(jump_multiplier);
                break;
            case "rod_upgrade":
                Debug.Log("Added rod upgrade");
                has_boots = true;
                rod_image.SetActive(true);
                break;
            case "bobber":
                Debug.Log("Added bobber");
                bobber = bobber_type;
                bobber_image.SetActive(true);
                break;
            default:
                Debug.Log("Tried to add an invalid item type: " + item_type);
                break;
        }
    }

    public void RespawnLoseCoins()
    {
        if (coin_Count > 0)
        {
            int coinsToRemove = coin_Count / 2;
            RemoveItemToInv("coin", coinsToRemove);
        }
        else
        {
            Debug.Log("Player has no coins");
        }
    }
}