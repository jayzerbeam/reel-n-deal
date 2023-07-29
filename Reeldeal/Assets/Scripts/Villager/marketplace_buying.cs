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
    public Button rerollButton;
    public float tradeDistance = 2.5f; // Can be adjusted in Inspector
    private hud_gui_controller inventoryController;

    private villager_marketplace_Items nearestVillager;
    private villager_marketplace_Items tradingVillager;

    //public TextMeshProUGUI item_type_text;
    public GameObject boots_image;
    public GameObject rod_image;
    public GameObject stand_0_bobber_image;
    public GameObject begin_1_bobber_image;
    public GameObject fresh_2_bobber_image;
    public GameObject river_3_bobber_image;
    public GameObject nauti_4_bobber_image;

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
        rerollButton.onClick.AddListener(rerollTrade);
        boots_image.SetActive(false);
        rod_image.SetActive(false);
        stand_0_bobber_image.SetActive(false);
        begin_1_bobber_image.SetActive(false);
        fresh_2_bobber_image.SetActive(false);
        river_3_bobber_image.SetActive(false);
        nauti_4_bobber_image.SetActive(false);
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

            if (nearestVillager != null)
            {
                if (tradingVillager.selling_item == "bobber")
                {
                    rerollButton.gameObject.SetActive(true);
                    switch (tradingVillager.bobber_type)
                    {
                        case 0:
                            stand_0_bobber_image.SetActive(true);
                            begin_1_bobber_image.SetActive(false);
                            fresh_2_bobber_image.SetActive(false);
                            river_3_bobber_image.SetActive(false);
                            nauti_4_bobber_image.SetActive(false);
                            break;
                        case 1:
                            stand_0_bobber_image.SetActive(false);
                            begin_1_bobber_image.SetActive(true);
                            fresh_2_bobber_image.SetActive(false);
                            river_3_bobber_image.SetActive(false);
                            nauti_4_bobber_image.SetActive(false);
                            break;
                        case 2:
                            stand_0_bobber_image.SetActive(false);
                            begin_1_bobber_image.SetActive(false);
                            fresh_2_bobber_image.SetActive(true);
                            river_3_bobber_image.SetActive(false);
                            nauti_4_bobber_image.SetActive(false);
                            break;
                        case 3:
                            stand_0_bobber_image.SetActive(false);
                            begin_1_bobber_image.SetActive(false);
                            fresh_2_bobber_image.SetActive(false);
                            river_3_bobber_image.SetActive(true);
                            nauti_4_bobber_image.SetActive(false);
                            break;
                        case 4:
                            stand_0_bobber_image.SetActive(false);
                            begin_1_bobber_image.SetActive(false);
                            fresh_2_bobber_image.SetActive(false);
                            river_3_bobber_image.SetActive(false);
                            nauti_4_bobber_image.SetActive(true);
                            break;
                        default:
                            stand_0_bobber_image.SetActive(true);
                            begin_1_bobber_image.SetActive(false);
                            fresh_2_bobber_image.SetActive(false);
                            river_3_bobber_image.SetActive(false);
                            nauti_4_bobber_image.SetActive(false);
                            Debug.Log("Tried to add an invalid bobber: " + tradingVillager.bobber_type);
                            break;
                    }
                }
                else
                {
                    rerollButton.gameObject.SetActive(false);
                    stand_0_bobber_image.SetActive(false);
                    begin_1_bobber_image.SetActive(false);
                    fresh_2_bobber_image.SetActive(false);
                    river_3_bobber_image.SetActive(false);
                    nauti_4_bobber_image.SetActive(false);
                    if (tradingVillager.selling_item == "rod_upgrade")
                    {
                        rod_image.SetActive(true);
                    }
                    else //boots
                    {
                        boots_image.SetActive(true);
                    }
                }
            }

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
                if (tradingVillager.selling_item == "bobber")
                {
                    market_item_price = tradingVillager.sale_price;
                    //// Get the minimum between how much the villager is willijng to buy and how much we have in total.
                    //max_buy_amount = Mathf.Min(tradingVillager.fish_amount, max_pos_buy_amount);
                    Debug.Log("Selling " + tradingVillager.bobber_type.ToString() + " " + tradingVillager.selling_item.ToString() + ", for " + market_item_price + " coins.");
                } 
                else
                {
                    market_item_price = tradingVillager.sale_price;
                    //// Get the minimum between how much the villager is willijng to buy and how much we have in total.
                    //max_buy_amount = Mathf.Min(tradingVillager.fish_amount, max_pos_buy_amount);
                    Debug.Log("Selling " + tradingVillager.selling_item.ToString() + ", for " + market_item_price + " coins.");
                }
                
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
            if (inventoryController.checkItemInInv("coin", tradingVillager.sale_price))
            {
                inventoryController.RemoveItemToInv("coin", tradingVillager.sale_price);
                if (tradingVillager.selling_item != "bobber")
                {
                    inventoryController.AddItemToInventory(tradingVillager.selling_item);
                    Debug.Log("Marketplace bought: " + tradingVillager.selling_item);
                }
                else
                {
                    inventoryController.AddItemToInventory(tradingVillager.selling_item, tradingVillager.bobber_type);
                    Debug.Log("Marketplace bought: " + tradingVillager.bobber_type.ToString() + " " + tradingVillager.selling_item.ToString());
                    tradingVillager.updateBobberType();
                }
                
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

    private void rerollTrade()
    {
        if (inventoryController.checkItemInInv("coin", 5))
        {
            inventoryController.RemoveItemToInv("coin", 5);
            tradingVillager.updateBobberType();
            //change bobber type
            Debug.Log("Rerolled item in marketplace");
        }
        else
        {
            Debug.Log("Not enough coins to reroll");
        }
    }

}

