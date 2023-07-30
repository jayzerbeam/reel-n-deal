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

    public GameObject boots_image;
    public GameObject rod_image;
    public GameObject upgraded_rod_image;
    public GameObject stand_0_bobber_image;
    public GameObject begin_1_bobber_image;
    public GameObject fresh_2_bobber_image;
    public GameObject river_3_bobber_image;
    public GameObject nauti_4_bobber_image;

    public bool has_boots = false;
    public bool has_rod_upgrade = false;
    public int bobber = 0;

    public GameObject bossfish_prefab; //really???
    public TextMeshProUGUI bossfish_alert;

    private void Start()
    {
        isInventoryVisible = false;
        boots_image.SetActive(false);
        rod_image.SetActive(true);
        upgraded_rod_image.SetActive(false);
        stand_0_bobber_image.SetActive(true);
        begin_1_bobber_image.SetActive(false);
        fresh_2_bobber_image.SetActive(false);
        river_3_bobber_image.SetActive(false);
        nauti_4_bobber_image.SetActive(false);
        inventoryHUD.SetActive(false);

        bossfish_alert.gameObject.SetActive(false);
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
                Debug.Log(
                    "Added " + item_amount + " lake_f_1(s). Total lake_f_1: " + lake_f_1_count
                );
                break;
            case "lake_f_2":
                lake_f_2_count += item_amount;
                Debug.Log(
                    "Added " + item_amount + " lake_f_2(s). Total lake_f_2: " + lake_f_2_count
                );
                break;
            case "lake_f_3":
                lake_f_3_count += item_amount;
                Debug.Log(
                    "Added " + item_amount + " lake_f_3(s). Total lake_f_3: " + lake_f_3_count
                );
                break;
            case "lake_f_4":
                lake_f_4_count += item_amount;
                Debug.Log(
                    "Added " + item_amount + " lake_f_4(s). Total lake_f_4: " + lake_f_4_count
                );
                break;
            case "ocean_f_1":
                ocean_f_1_count += item_amount;
                Debug.Log(
                    "Added " + item_amount + " ocean_f_1(s). Total ocean_f_1: " + ocean_f_1_count
                );
                break;
            case "ocean_f_2":
                ocean_f_2_count += item_amount;
                Debug.Log(
                    "Added " + item_amount + " ocean_f_2(s). Total ocean_f_2: " + ocean_f_2_count
                );
                break;
            case "ocean_f_3":
                ocean_f_3_count += item_amount;
                Debug.Log(
                    "Added " + item_amount + " ocean_f_3(s). Total ocean_f_3: " + ocean_f_3_count
                );
                break;
            case "ocean_f_4":
                ocean_f_4_count += item_amount;
                Debug.Log(
                    "Added " + item_amount + " ocean_f_4(s). Total ocean_f_4: " + ocean_f_4_count
                );
                break;
            case "ocean_shark":
                ocean_shark_count += item_amount;
                Debug.Log(
                    "Added "
                        + item_amount
                        + " ocean_shark(s). Total ocean_shark: "
                        + ocean_shark_count
                );
                break;
            case "river_f_1":
                river_f_1_count += item_amount;
                Debug.Log(
                    "Added " + item_amount + " river_f_1(s). Total river_f_1: " + river_f_1_count
                );
                break;
            case "river_f_2":
                river_f_2_count += item_amount;
                Debug.Log(
                    "Added " + item_amount + " river_f_2(s). Total river_f_2: " + river_f_2_count
                );
                break;
            case "river_f_3":
                river_f_3_count += item_amount;
                Debug.Log(
                    "Added " + item_amount + " river_f_3(s). Total river_f_3: " + river_f_3_count
                );
                break;
            case "river_f_4":
                river_f_4_count += item_amount;
                Debug.Log(
                    "Added " + item_amount + " river_f_4(s). Total river_f_4: " + river_f_4_count
                );
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
                Debug.Log(
                    "Removed " + item_amount + " lake_f_1(s). Total lake_f_1: " + lake_f_1_count
                );
                break;
            case "lake_f_2":
                lake_f_2_count -= item_amount;
                lake_f_2_count = Mathf.Max(lake_f_2_count, 0); // Ensure the count doesn't go below 0
                Debug.Log(
                    "Removed " + item_amount + " lake_f_2(s). Total lake_f_2: " + lake_f_2_count
                );
                break;
            case "lake_f_3":
                lake_f_3_count -= item_amount;
                lake_f_3_count = Mathf.Max(lake_f_3_count, 0); // Ensure the count doesn't go below 0
                Debug.Log(
                    "Removed " + item_amount + " lake_f_3(s). Total lake_f_3: " + lake_f_3_count
                );
                break;
            case "lake_f_4":
                lake_f_4_count -= item_amount;
                lake_f_4_count = Mathf.Max(lake_f_4_count, 0); // Ensure the count doesn't go below 0
                Debug.Log(
                    "Removed " + item_amount + " lake_f_4(s). Total lake_f_4: " + lake_f_4_count
                );
                break;
            case "ocean_f_1":
                ocean_f_1_count -= item_amount;
                ocean_f_1_count = Mathf.Max(ocean_f_1_count, 0); // Ensure the count doesn't go below 0
                Debug.Log(
                    "Removed " + item_amount + " ocean_f_1(s). Total ocean_f_1: " + ocean_f_1_count
                );
                break;
            case "ocean_f_2":
                ocean_f_2_count -= item_amount;
                ocean_f_2_count = Mathf.Max(ocean_f_2_count, 0); // Ensure the count doesn't go below 0
                Debug.Log(
                    "Removed " + item_amount + " ocean_f_2(s). Total ocean_f_2: " + ocean_f_2_count
                );
                break;
            case "ocean_f_3":
                ocean_f_3_count -= item_amount;
                ocean_f_3_count = Mathf.Max(ocean_f_3_count, 0); // Ensure the count doesn't go below 0
                Debug.Log(
                    "Removed " + item_amount + " ocean_f_3(s). Total ocean_f_3: " + ocean_f_3_count
                );
                break;
            case "ocean_f_4":
                ocean_f_4_count -= item_amount;
                ocean_f_4_count = Mathf.Max(ocean_f_4_count, 0); // Ensure the count doesn't go below 0
                Debug.Log(
                    "Removed " + item_amount + " ocean_f_4(s). Total ocean_f_4: " + ocean_f_4_count
                );
                break;
            case "ocean_shark":
                ocean_shark_count -= item_amount;
                ocean_shark_count = Mathf.Max(ocean_shark_count, 0); // Ensure the count doesn't go below 0
                Debug.Log(
                    "Removed "
                        + item_amount
                        + " ocean_shark(s). Total ocean_shark: "
                        + ocean_shark_count
                );
                break;
            case "river_f_1":
                river_f_1_count -= item_amount;
                river_f_1_count = Mathf.Max(river_f_1_count, 0); // Ensure the count doesn't go below 0
                Debug.Log(
                    "Removed " + item_amount + " river_f_1(s). Total river_f_1: " + river_f_1_count
                );
                break;
            case "river_f_2":
                river_f_2_count -= item_amount;
                river_f_2_count = Mathf.Max(river_f_2_count, 0); // Ensure the count doesn't go below 0
                Debug.Log(
                    "Removed " + item_amount + " river_f_2(s). Total river_f_2: " + river_f_2_count
                );
                break;
            case "river_f_3":
                river_f_3_count -= item_amount;
                river_f_3_count = Mathf.Max(river_f_3_count, 0); // Ensure the count doesn't go below 0
                Debug.Log(
                    "Removed " + item_amount + " river_f_3(s). Total river_f_3: " + river_f_3_count
                );
                break;
            case "river_f_4":
                river_f_4_count -= item_amount;
                river_f_4_count = Mathf.Max(river_f_4_count, 0); // Ensure the count doesn't go below 0
                Debug.Log(
                    "Removed " + item_amount + " river_f_4(s). Total river_f_4: " + river_f_4_count
                );
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
    public bool checkItemInInv(string fishType, int amount)
    {
        // Check if the Player has at least any fish of this type
        switch (fishType)
        {
            case "coin":
                Debug.Log(
                    "Does Player have at least " + amount + " coin(s). Total coins: " + coin_Count
                );
                if (coin_Count >= amount)
                {
                    return true;
                }
                return false;
            case "lake_f_1":
                Debug.Log(
                    "Does Player have at least "
                        + amount
                        + " lake_f_1_count fish(es). Total blue fishes: "
                        + lake_f_1_count
                );
                if (lake_f_1_count >= amount)
                {
                    return true;
                }
                return false;
            case "lake_f_2":
                Debug.Log(
                    "Does Player have at least "
                        + amount
                        + " lake_f_2_count fish(es). Total blue fishes: "
                        + lake_f_2_count
                );
                if (lake_f_2_count >= amount)
                {
                    return true;
                }
                return false;
            case "lake_f_3":
                Debug.Log(
                    "Does Player have at least "
                        + amount
                        + " lake_f_3_count fish(es). Total blue fishes: "
                        + lake_f_3_count
                );
                if (lake_f_3_count >= amount)
                {
                    return true;
                }
                return false;
            case "lake_f_4":
                Debug.Log(
                    "Does Player have at least "
                        + amount
                        + " lake_f_4_count fish(es). Total blue fishes: "
                        + lake_f_4_count
                );
                if (lake_f_4_count >= amount)
                {
                    return true;
                }
                return false;
            case "ocean_f_1":
                Debug.Log(
                    "Does Player have at least "
                        + amount
                        + " ocean_f_1_count fish(es). Total blue fishes: "
                        + ocean_f_1_count
                );
                if (ocean_f_1_count >= amount)
                {
                    return true;
                }
                return false;
            case "ocean_f_2":
                Debug.Log(
                    "Does Player have at least "
                        + amount
                        + " ocean_f_2_count fish(es). Total blue fishes: "
                        + ocean_f_2_count
                );
                if (ocean_f_2_count >= amount)
                {
                    return true;
                }
                return false;
            case "ocean_f_3":
                Debug.Log(
                    "Does Player have at least "
                        + amount
                        + " ocean_f_3_count fish(es). Total blue fishes: "
                        + ocean_f_3_count
                );
                if (ocean_f_3_count >= amount)
                {
                    return true;
                }
                return false;
            case "ocean_f_4":
                Debug.Log(
                    "Does Player have at least "
                        + amount
                        + " ocean_f_4_count fish(es). Total blue fishes: "
                        + ocean_f_4_count
                );
                if (ocean_f_4_count >= amount)
                {
                    return true;
                }
                return false;
            case "ocean_shark":
                Debug.Log(
                    "Does Player have at least "
                        + amount
                        + " ocean_shark_count fish(es). Total blue fishes: "
                        + ocean_shark_count
                );
                if (ocean_shark_count >= amount)
                {
                    return true;
                }
                return false;
            case "river_f_1":
                Debug.Log(
                    "Does Player have at least "
                        + amount
                        + " river_f_1_count fish(es). Total blue fishes: "
                        + river_f_1_count
                );
                if (river_f_1_count >= amount)
                {
                    return true;
                }
                return false;
            case "river_f_2":
                Debug.Log(
                    "Does Player have at least "
                        + amount
                        + " river_f_2_count fish(es). Total blue fishes: "
                        + river_f_2_count
                );
                if (river_f_2_count >= amount)
                {
                    return true;
                }
                return false;
            case "river_f_3":
                Debug.Log(
                    "Does Player have at least "
                        + amount
                        + " river_f_3_count fish(es). Total blue fishes: "
                        + river_f_3_count
                );
                if (river_f_3_count >= amount)
                {
                    return true;
                }
                return false;
            case "river_f_4":
                Debug.Log(
                    "Does Player have at least "
                        + amount
                        + " river_f_4_count fish(es). Total blue fishes: "
                        + river_f_4_count
                );
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
                break;
            case "rod_upgrade":
                Debug.Log("Added rod upgrade");
                has_rod_upgrade = true;
                rod_image.SetActive(false);
                upgraded_rod_image.SetActive(true);
                // spawnBossFish();
                break;
            case "bobber":
                Debug.Log("Added bobber");
                bobber = bobber_type;
                switch (bobber)
                {
                    case 0:
                        stand_0_bobber_image.SetActive(true);
                        begin_1_bobber_image.SetActive(false);
                        fresh_2_bobber_image.SetActive(false);
                        river_3_bobber_image.SetActive(false);
                        nauti_4_bobber_image.SetActive(false);
                        Debug.Log("Standard Bobber added bobber index: " + bobber);
                        break;
                    case 1:
                        stand_0_bobber_image.SetActive(false);
                        begin_1_bobber_image.SetActive(true);
                        fresh_2_bobber_image.SetActive(false);
                        river_3_bobber_image.SetActive(false);
                        nauti_4_bobber_image.SetActive(false);
                        Debug.Log("Beginner Bobber added bobber index: " + bobber);
                        break;
                    case 2:
                        stand_0_bobber_image.SetActive(false);
                        begin_1_bobber_image.SetActive(false);
                        fresh_2_bobber_image.SetActive(true);
                        river_3_bobber_image.SetActive(false);
                        nauti_4_bobber_image.SetActive(false);
                        Debug.Log("Freshwater Bobber added bobber index: " + bobber);
                        break;
                    case 3:
                        stand_0_bobber_image.SetActive(false);
                        begin_1_bobber_image.SetActive(false);
                        fresh_2_bobber_image.SetActive(false);
                        river_3_bobber_image.SetActive(true);
                        nauti_4_bobber_image.SetActive(false);
                        Debug.Log("River Bobber added bobber index: " + bobber);
                        break;
                    case 4:
                        stand_0_bobber_image.SetActive(false);
                        begin_1_bobber_image.SetActive(false);
                        fresh_2_bobber_image.SetActive(false);
                        river_3_bobber_image.SetActive(false);
                        nauti_4_bobber_image.SetActive(true);
                        Debug.Log("Nautical Bobber added bobber index: " + bobber);
                        break;
                    default:
                        stand_0_bobber_image.SetActive(true);
                        begin_1_bobber_image.SetActive(false);
                        fresh_2_bobber_image.SetActive(false);
                        river_3_bobber_image.SetActive(false);
                        nauti_4_bobber_image.SetActive(false);
                        Debug.Log("Tried to add an invalid bobber: " + bobber);
                        break;
                }
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

        if (has_boots)
        {
            RemoveItemToInv("boots", 1);
        }
    }

    // public void spawnBossFish()
    // {
    //     // Check if the fishersPrefab is assigned.
    //     if (bossfish_prefab != null)
    //     {
    //         Vector3 bossfish_spawn_loc = new Vector3(-360.6f, 48f, -36.4f);
    //         Quaternion bossfish_spawn_rot = Quaternion.identity;
    //         // Spawn the "fishers" GameObject at the position and rotation of this spawner's transform.
    //         StartCoroutine(ActivateTextCoroutine(20f));
    //         Instantiate(bossfish_prefab, bossfish_spawn_loc, bossfish_spawn_rot);
    //         Debug.Log("The bossfish_prefab has been instantiated.");
    //     }
    //     else
    //     {
    //         Debug.LogError("The bossfish_prefab is not assigned. Please assign the prefab in the Inspector.");
    //     }
    // }



    public void ActivateTextForDuration(float duration)
    {
        StartCoroutine(ActivateTextCoroutine(duration));
    }

    private System.Collections.IEnumerator ActivateTextCoroutine(float duration)
    {
        // Activate the Text object.
        bossfish_alert.gameObject.SetActive(true);

        // Wait for the specified duration.
        yield return new WaitForSeconds(duration);

        // Deactivate the Text object after the duration has passed.
        bossfish_alert.gameObject.SetActive(false);
    }
}
