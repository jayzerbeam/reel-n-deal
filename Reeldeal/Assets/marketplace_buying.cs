using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class marketplace_buying : MonoBehaviour
{
    public GameObject marketplaceMenu;
    public Button buyButton;
    public Button closeButton;
    public float tradeDistance = 2.5f; // Can be adjusted in Inspector
    private hud_gui_controller inventoryController;

    private villager_marketplace_Items nearestVillager;
    private villager_marketplace_Items tradingVillager;

    public TextMeshProUGUI item_type_text;
    public TextMeshProUGUI item_price_text;
    public int market_item_price;


    private void Awake()
    {
        inventoryController = FindObjectOfType<hud_gui_controller>();
    }


    // Start is called before the first frame update
    void Start()
    {
        buyButton.onClick.AddListener(PerformTrade);
        closeButton.onClick.AddListener(CloseMenuTrade);
        marketplaceMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!marketplaceMenu.activeSelf)
        {
            CheckNearestVillager();
        }

        if (marketplaceMenu.activeSelf)
        {
            inventoryController = FindObjectOfType<hud_gui_controller>();
            // This will only be executed if tradeMenu is active.
            Debug.Log("Marketplace menu is active.");

            // Update fish_price_text
            item_price_text.text = market_item_price.ToString();


        }
    }

    private void CheckNearestVillager()
    {
        float shortestDistance = Mathf.Infinity;
        villager_marketplace_Items[] villagers = FindObjectsOfType<villager_marketplace_Items>();

        //foreach (villager_Trades villager in villagers)
        //{
        //    Debug.Log(villager.gameObject.name);
        //}

        nearestVillager = null;

        foreach (villager_marketplace_Items villager in villagers)
        {
            float distanceToVillager = Vector3.Distance(transform.position, villager.transform.position);
            if (distanceToVillager < shortestDistance)
            {
                shortestDistance = distanceToVillager;
                nearestVillager = villager;
            }
        }

        if (nearestVillager != null && shortestDistance <= tradeDistance)
        {
            if (Input.GetButtonDown("Trade1") && nearestVillager.selling_item.ToString() != "empty")
            {
                Debug.Log(nearestVillager.selling_item.ToString());
                tradingVillager = nearestVillager;
                marketplaceMenu.SetActive(true);
                item_type_text.text = tradingVillager.selling_item.ToString();
                market_item_price = tradingVillager.sale_price;
                //// Get the minimum between how much the villager is willijng to buy and how much we have in total.
                //max_buy_amount = Mathf.Min(tradingVillager.fish_amount, max_pos_buy_amount);
                Debug.Log("Selling " + tradingVillager.selling_item.ToString() + ", for " + market_item_price + " coins.");
            }
        }
        else
        {
            marketplaceMenu.SetActive(false);
        }
    }

    private void PerformTrade()
    {
        if (tradingVillager != null)
        {
            if (inventoryController.HasFish("coin", tradingVillager.sale_price))
            {
                inventoryController.RemoveItemToInv("coin", tradingVillager.sale_price);
                inventoryController.AddItemToInventory(tradingVillager.selling_item);
                Debug.Log("Marketplace bought: " + tradingVillager.selling_item);
            }
            else
            {
                Debug.Log("Not enough coins to buy marketplace item");
            }
            marketplaceMenu.SetActive(false);
            tradingVillager = null;
        }
    }

    private void CloseMenuTrade()
    {
        marketplaceMenu.SetActive(false);
    }

}

