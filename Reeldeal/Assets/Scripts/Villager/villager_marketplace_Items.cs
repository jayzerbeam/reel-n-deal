using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villager_marketplace_Items : MonoBehaviour
{

    private string[] marketplace_items = { "empty", "boots", "bobber", "rod_upgrade" };
    public string selling_item;
    //private string villager_type = "marketplace_seller";
    public int sale_price;

    public int bobber_type = 0;

    private void Start()
    {
        // Check if selling_item is a marketplace_items
        bool is_marketplace_item = false;
        foreach (string item in marketplace_items)
        {
            if (selling_item == item)
            {
                is_marketplace_item = true;
                break;
            }
            if (selling_item == "bobber")
            {
                updateBobberType();
            }
        }

        if (is_marketplace_item)
        {
            // Debug.Log("Marketplace Villager has a sellable item set as selling_item.");
        }
        else
        {
            Debug.Log("Marketplace Villager has an invalid selling item set in selling_item.");
        }
    }

    public void updateBobberType()
    {
        bobber_type = Random.Range(0, 5);
        Debug.Log("Updated bobber_type to: " + bobber_type);
    }
}
