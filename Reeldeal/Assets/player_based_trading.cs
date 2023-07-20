using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class player_based_trading : MonoBehaviour
{
    public GameObject tradeMenu; 
    public Button tradeButton;
    public Button closeButton;
    public float tradeDistance = 2.5f; // Can be adjusted in Inspector
    private hud_gui_controller inventoryController; 

    private villager_Trades nearestVillager;
    private villager_Trades tradingVillager;



    //public GameObject player;
    ////private playerInventory playerInventory;
    //private string[] greetings = { "Hello!", "Hi!", "Hey!", "Greetings!", "Good to see you!" };
    ////private string[] fishTypes = { "blue", "pink", "orange" };
    //private string[] fishTypes = { "lake_f_1", "lake_f_2", "lake_f_3", "lake_f_4", "ocean_f_1", "ocean_f_2", "ocean_f_3", "ocean_f_4", "ocean_shark", "river_f_1", "river_f_2", "river_f_3", "river_f_4" };
    //public float radiusToTrade = 2.5f; // Set this to whatever radius you want
    //public TextMeshProUGUI talk_to_playerText;
    //public float timeToErase = 10f;
    //private string msg;



    //public GameObject canvas;
    //public GameObject fish_blue_image;
    //public GameObject fish_pink_image;
    //public GameObject fish_orange_image;
    public TextMeshProUGUI fish_type_text;
    public Slider fish_slider;
    public TextMeshProUGUI fish_amount_text;
    public TextMeshProUGUI fish_price_text;
    //public Button trading_button;

    //private int fish_price;
    //public int max_fish_price;
    //private string fish_type;

    private int max_pos_buy_amount;
    //private int max_buy_amount;
    //private int buy_amount;
    public int trading_fish_price;



    private void Awake()
    {
        inventoryController = FindObjectOfType<hud_gui_controller>();
    }


    // Start is called before the first frame update
    void Start()
    {
        tradeButton.onClick.AddListener(PerformTrade);
        closeButton.onClick.AddListener(CloseMenuTrade);
        tradeMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!tradeMenu.activeSelf)
        {
            CheckNearestVillager();
        }
            
        if (tradeMenu.activeSelf)
        {
            inventoryController = FindObjectOfType<hud_gui_controller>();
            // This will only be executed if tradeMenu is active.
            Debug.Log("Trade menu is active.");

            // Update fish_amount_text
            fish_amount_text.text = fish_slider.value.ToString();

            // Update fish_price_text
            fish_price_text.text = (fish_slider.value * trading_fish_price).ToString();


        }
    }

    private void CheckNearestVillager()
    {
        float shortestDistance = Mathf.Infinity;
        villager_Trades[] villagers = FindObjectsOfType<villager_Trades>();

        //foreach (villager_Trades villager in villagers)
        //{
        //    Debug.Log(villager.gameObject.name);
        //}

        nearestVillager = null;

        foreach (villager_Trades villager in villagers)
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
            if (Input.GetButtonDown("Trade1") && inventoryController.HasFish(nearestVillager.fish_type, 1))
            {
                tradingVillager = nearestVillager;
                tradeMenu.SetActive(true);
                getFishType(tradingVillager.fish_type);
                fish_slider.value = 1;
                trading_fish_price = tradingVillager.fish_price;
                //// Get the minimum between how much the villager is willijng to buy and how much we have in total.
                //max_buy_amount = Mathf.Min(tradingVillager.fish_amount, max_pos_buy_amount);
                Debug.Log(" I want " + tradingVillager.fish_type.ToString() + ", for " + trading_fish_price + " coin(s) each.");
            }
        }
        else
        {
            tradeMenu.SetActive(false);
        }
    }

    private void PerformTrade()
    {
        if (tradingVillager != null)
        {
            int trade_amount = (int)fish_slider.value * trading_fish_price;
            inventoryController.RemoveItemToInv(tradingVillager.fish_type, (int)fish_slider.value);
            inventoryController.AddItemToInv("coin", trade_amount);
            Debug.Log("Trade made, " + trade_amount.ToString() + " coins given, " + fish_slider.value.ToString() + " fish of type: " + tradingVillager.fish_type.ToString() + " taken.");
            tradeMenu.SetActive(false);
            tradingVillager = null;
        }
    }

    private void CloseMenuTrade()
    {
        tradeMenu.SetActive(false); 
    }


    private void getFishType(string fish_type)
    {
        switch (fish_type)
        {
            case "lake_f_1":
                //fish_blue_image.SetActive(true);
                //fish_pink_image.SetActive(false);
                //fish_orange_image.SetActive(false);
                fish_slider.minValue = 0;
                fish_slider.maxValue = inventoryController.lake_f_1_count;
                max_pos_buy_amount = inventoryController.lake_f_1_count;
                Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                break;
            case "lake_f_2":
                fish_slider.minValue = 0;
                fish_slider.maxValue = inventoryController.lake_f_2_count;
                max_pos_buy_amount = inventoryController.lake_f_2_count;
                Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                break;
            case "lake_f_3":
                fish_slider.minValue = 0;
                fish_slider.maxValue = inventoryController.lake_f_3_count;
                max_pos_buy_amount = inventoryController.lake_f_3_count;
                Debug.Log("slider max value set to blue " + fish_slider.maxValue.ToString());
                break;
            case "lake_f_4":
                fish_slider.minValue = 0;
                fish_slider.maxValue = inventoryController.lake_f_4_count;
                max_pos_buy_amount = inventoryController.lake_f_4_count;
                Debug.Log("slider max value set to blue " + fish_slider.maxValue.ToString());
                break;
            case "ocean_f_1":
                fish_slider.minValue = 0;
                fish_slider.maxValue = inventoryController.ocean_f_1_count;
                Debug.Log("slider max value set to blue " + fish_slider.maxValue.ToString());
                max_pos_buy_amount = inventoryController.ocean_f_1_count;
                break;
            case "ocean_f_2":
                fish_slider.minValue = 0;
                fish_slider.maxValue = inventoryController.ocean_f_2_count;
                Debug.Log("slider max value set to blue " + fish_slider.maxValue.ToString());
                max_pos_buy_amount = inventoryController.ocean_f_2_count;
                break;
            case "ocean_f_3":
                fish_slider.minValue = 0;
                fish_slider.maxValue = inventoryController.ocean_f_3_count;
                Debug.Log("slider max value set to blue " + fish_slider.maxValue.ToString());
                max_pos_buy_amount = inventoryController.ocean_f_3_count;
                break;
            case "ocean_f_4":
                fish_slider.minValue = 0;
                fish_slider.maxValue = inventoryController.ocean_f_4_count;
                Debug.Log("slider max value set to blue " + fish_slider.maxValue.ToString());
                max_pos_buy_amount = inventoryController.ocean_f_4_count;
                break;
            case "ocean_shark":
                fish_slider.minValue = 0;
                fish_slider.maxValue = inventoryController.ocean_shark_count;
                Debug.Log("slider max value set to blue " + fish_slider.maxValue.ToString());
                max_pos_buy_amount = inventoryController.ocean_shark_count;
                break;
            case "river_f_1":
                fish_slider.minValue = 0;
                fish_slider.maxValue = inventoryController.river_f_1_count;
                max_pos_buy_amount = inventoryController.river_f_1_count;
                Debug.Log("slider max value set to blue " + fish_slider.maxValue.ToString());
                break;
            case "river_f_2":
                fish_slider.minValue = 0;
                fish_slider.maxValue = inventoryController.river_f_2_count;
                max_pos_buy_amount = inventoryController.river_f_2_count;
                Debug.Log("slider max value set to blue " + fish_slider.maxValue.ToString());
                break;
            case "river_f_3":
                fish_slider.minValue = 0;
                fish_slider.maxValue = inventoryController.river_f_3_count;
                max_pos_buy_amount = inventoryController.river_f_3_count;
                Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                break;
            case "river_f_4":
                fish_slider.minValue = 0;
                fish_slider.maxValue = inventoryController.river_f_4_count;
                max_pos_buy_amount = inventoryController.river_f_4_count;
                Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                break;
            default:
                Debug.Log("Invalid item type: " + fish_type.ToString());
                break;
        }
        fish_type_text.text = fish_type.ToString();
    }
}

