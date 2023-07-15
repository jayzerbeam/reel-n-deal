using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class hud_gui_controller : MonoBehaviour
{

    public GameObject inventoryHUD;
    public TextMeshProUGUI coin_count_disp;
    public TextMeshProUGUI blue_fish_disp;
    public TextMeshProUGUI pink_fish_disp;
    public TextMeshProUGUI orange_fish_disp;
    private bool isInventoryVisible;
    public int coin_Count = 0;
    public int blue_fish_Count = 0;
    public int pink_fish_Count = 0;
    public int orange_fish_Count = 0;


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
            case "blue":
                blue_fish_Count += item_amount;
                Debug.Log("Added " + item_amount + " blue item(s). Total blue items: " + blue_fish_Count);
                break;
            case "pink":
                pink_fish_Count += item_amount;
                Debug.Log("Added " + item_amount + " pink item(s). Total pink items: " + pink_fish_Count);
                break;
            case "orange":
                orange_fish_Count += item_amount;
                Debug.Log("Added " + item_amount + " pink item(s). Total pink items: " + orange_fish_Count);
                break;
            default:
                Debug.Log("Invalid item type: " + item_type);
                break;
        }

        coin_count_disp.text = coin_Count.ToString();
        blue_fish_disp.text = blue_fish_Count.ToString();
        pink_fish_disp.text = pink_fish_Count.ToString();
        orange_fish_disp.text = orange_fish_Count.ToString();
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
            case "blue":
                blue_fish_Count -= item_amount;
                blue_fish_Count = Mathf.Max(blue_fish_Count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " blue item(s). Total blue items: " + blue_fish_Count);
                break;
            case "pink":
                pink_fish_Count -= item_amount;
                pink_fish_Count = Mathf.Max(pink_fish_Count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " pink item(s). Total pink items: " + pink_fish_Count);
                break;
            case "orange":
                orange_fish_Count -= item_amount;
                orange_fish_Count = Mathf.Max(orange_fish_Count, 0); // Ensure the count doesn't go below 0
                Debug.Log("Removed " + item_amount + " pink item(s). Total pink items: " + orange_fish_Count);
                break;
            default:
                Debug.Log("Invalid item type: " + item_type);
                break;
        }

        coin_count_disp.text = coin_Count.ToString();
        blue_fish_disp.text = blue_fish_Count.ToString();
        pink_fish_disp.text = pink_fish_Count.ToString();
        orange_fish_disp.text = orange_fish_Count.ToString();


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