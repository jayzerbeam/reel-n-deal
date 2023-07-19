using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class VillagerTrading_script : MonoBehaviour
{
    public GameObject player;
    //private playerInventory playerInventory;
    private string[] greetings = { "Hello!", "Hi!", "Hey!", "Greetings!", "Good to see you!" };
    //private string[] fishTypes = { "blue", "pink", "orange" };
    private string[] fishTypes = { "lake_f_1", "lake_f_2", "lake_f_3", "lake_f_4", "ocean_f_1", "ocean_f_2", "ocean_f_3", "ocean_f_4", "ocean_shark", "river_f_1", "river_f_2", "river_f_3", "river_f_4" };
    public float radiusToTrade = 2.5f; // Set this to whatever radius you want
    public TextMeshProUGUI talk_to_playerText;
    public float timeToErase = 10f;
    private string msg;



    public GameObject canvas;
    public GameObject fish_blue_image;
    public GameObject fish_pink_image;
    public GameObject fish_orange_image;
    public TextMeshProUGUI fish_type_text;
    //public Slider fish_slider;
    public TextMeshProUGUI fish_amount_text;
    public TextMeshProUGUI fish_price_text;
    public Button trading_button;

    private int fish_price;
    public int max_fish_price;
    private string fish_type;

    private int max_pos_buy_amount;
    public int max_buy_amount; 
    private int buy_amount;

    private hud_gui_controller inventoryController;

    private void Awake()
    {
        inventoryController = FindObjectOfType<hud_gui_controller>();
        fish_type = fishTypes[Random.Range(0, fishTypes.Length)];
    }

    void Start()
    {
        //playerInventory = player.GetComponent<playerInventory>();

        fish_price = 1;
        fish_type = fishTypes[Random.Range(0, fishTypes.Length)];
        max_buy_amount = Mathf.Max(max_buy_amount, 1);
        // Assign the Button's listener
        //trading_button.onClick.AddListener(TradeFish);
    }

    // Update is called once per frame
    private void Update()
    {

        // Check if "Fire2" was pressed
        if (Input.GetButtonDown("Trade1"))
        {
            // Check if the player is within trading radius
            if (Vector3.Distance(transform.position, player.transform.position) <= radiusToTrade)
            {
                // Generate a random greeting
                string greeting = greetings[Random.Range(0, greetings.Length)];

                //// Randomly select fish type
                //fish_type = fishTypes[Random.Range(0, fishTypes.Length)];

                // Randomly assign fish price from 1 to 5
                fish_price = Random.Range(1, max_fish_price + 1);
                if (fish_price > max_fish_price/2)
                {
                    fish_price = Random.Range(1, max_fish_price + 1);
                }

                // Print the greeting and request
                //Debug.Log(greeting + " I want " + numFish + " of " + fishType + ".");
                msg = greeting.ToString() + " I want " + fish_type.ToString() + " fish.";
                Debug.Log(msg);
                talk_to_player(msg);

                inventoryController = FindObjectOfType<hud_gui_controller>();

                // Check if the player has fish of the desired type
                if (inventoryController.HasFish(fish_type, 1))
                {
                    // Make canvas active
                    canvas.SetActive(true);
                    fish_blue_image.SetActive(false);
                    fish_pink_image.SetActive(false);
                    fish_orange_image.SetActive(false);

                    //lake_f_1_count = 0;
                    //lake_f_2_count = 0;
                    //lake_f_3_count = 0;
                    //lake_f_4_count = 0;
                    //ocean_f_1_count = 0;
                    //ocean_f_2_count = 0;
                    //ocean_f_3_count = 0;
                    //ocean_f_4_count = 0;
                    //ocean_shark_count = 0;
                    //river_f_1_count = 0;
                    //river_f_2_count = 0;
                    //river_f_3_count = 0;
                    //river_f_4_count = 0;

                    switch (fish_type)
                    {
                        case "lake_f_1":
                            //fish_blue_image.SetActive(true);
                            //fish_pink_image.SetActive(false);
                            //fish_orange_image.SetActive(false);
                            //fish_slider.minValue = 0;
                            //fish_slider.maxValue = inventoryController.lake_f_1_count;
                            max_pos_buy_amount =  inventoryController.lake_f_1_count;
                            //Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                            break;
                        case "lake_f_2":
                            //fish_slider.minValue = 0;
                            //fish_slider.maxValue = inventoryController.lake_f_2_count;
                            max_pos_buy_amount =  inventoryController.lake_f_2_count;
                            //Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                            break;
                        case "lake_f_3":
                            //fish_slider.minValue = 0;
                            //fish_slider.maxValue = inventoryController.lake_f_3_count;
                            max_pos_buy_amount =  inventoryController.lake_f_3_count;
                            //Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                            break;
                        case "lake_f_4":
                            //fish_slider.minValue = 0;
                            //fish_slider.maxValue = inventoryController.lake_f_4_count;
                            max_pos_buy_amount =  inventoryController.lake_f_4_count;
                            //Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                            break;
                        case "ocean_f_1":
                            //fish_slider.minValue = 0;
                            //fish_slider.maxValue = inventoryController.ocean_f_1_count;
                            //Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                            max_pos_buy_amount = inventoryController.ocean_f_1_count;
                            break;
                        case "ocean_f_2":
                            //fish_slider.minValue = 0;
                            //fish_slider.maxValue = inventoryController.ocean_f_2_count;
                            //Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString
                            max_pos_buy_amount =  inventoryController.ocean_f_2_count;
                            break;
                        case "ocean_f_3":
                            //fish_slider.minValue = 0;
                            //fish_slider.maxValue = inventoryController.ocean_f_3_count;
                            //Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                            max_pos_buy_amount = inventoryController.ocean_f_3_count;
                            break;
                        case "ocean_f_4":
                            //fish_slider.minValue = 0;
                            //fish_slider.maxValue = inventoryController.ocean_f_4_count;
                            //Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                            max_pos_buy_amount =  inventoryController.ocean_f_4_count;
                            break;
                        case "ocean_shark":
                            //fish_slider.minValue = 0;
                            //fish_slider.maxValue = inventoryController.ocean_shark_count;
                            //Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                            max_pos_buy_amount =  inventoryController.ocean_shark_count;
                            break;
                        case "river_f_1":
                            //fish_slider.minValue = 0;
                            //fish_slider.maxValue = inventoryController.river_f_1_count;
                            max_pos_buy_amount =  inventoryController.river_f_1_count;
                            //Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                            break;
                        case "river_f_2":
                            //fish_slider.minValue = 0;
                            //fish_slider.maxValue = inventoryController.river_f_2_count;
                            max_pos_buy_amount =  inventoryController.river_f_2_count;
                            //Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                            break;
                        case "river_f_3":
                            //fish_slider.minValue = 0;
                            //fish_slider.maxValue = inventoryController.river_f_3_count;
                            max_pos_buy_amount =  inventoryController.river_f_3_count;
                            //Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                            break;
                        case "river_f_4":
                            //fish_slider.minValue = 0;
                            //fish_slider.maxValue = inventoryController.river_f_4_count;
                            max_pos_buy_amount =  inventoryController.river_f_4_count;
                            //Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                            break;
                        //case "blue":
                        //    fish_blue_image.SetActive(true);
                        //    fish_pink_image.SetActive(false);
                        //    fish_orange_image.SetActive(false);
                        //    fish_slider.minValue = 0;
                        //    fish_slider.maxValue = inventoryController.lake_f_1_count;
                        //    Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                        //    break;
                        //case "pink":
                        //    fish_blue_image.SetActive(false);
                        //    fish_pink_image.SetActive(true);
                        //    fish_orange_image.SetActive(false);
                        //    fish_slider.minValue = 0;
                        //    fish_slider.maxValue = inventoryController.lake_f_2_count;
                        //    Debug.Log("Slider max value set to pink " + fish_slider.maxValue.ToString());
                        //    break;
                        //case "orange":
                        //    fish_blue_image.SetActive(false);
                        //    fish_pink_image.SetActive(false);
                        //    fish_orange_image.SetActive(true);
                        //    fish_slider.minValue = 0;
                        //    fish_slider.maxValue = inventoryController.lake_f_3_count;
                        //    Debug.Log("Slider max value set to orange " + fish_slider.maxValue.ToString());
                        //    break;
                        default:
                            Debug.Log("Invalid item type: " + fish_type.ToString());
                            //fish_slider.minValue = 0;
                            //fish_slider.maxValue = 0;
                            //Debug.Log("Slider max value set to 0");
                            break;
                    }

                    //// Update fish_amount_text
                    //fish_amount_text.text = fish_slider.value.ToString();

                    //// Update fish_price_text
                    //fish_price_text.text = (fish_slider.value * fish_price).ToString();


                    //buy_amount = Random.Range(1, max_pos_buy_amount);
                    //buy_amount = Mathf.Min(max_buy_amount, buy_amount);
                    //buy_amount = Random.Range(1, buy_amount + 1);
                    buy_amount = Mathf.Min(max_buy_amount, max_pos_buy_amount);
                    buy_amount = Random.Range(1, buy_amount + 1);
                    fish_type_text.text = fish_type.ToString(); 
                    fish_amount_text.text = buy_amount.ToString();
                    fish_price_text.text = (buy_amount * fish_price).ToString();


                    //// Remove the fish from the player's inventory
                    //playerInventory.RemoveFish(fishType, numFish);

                    //int randomPayoutMultiplier = Random.Range(1, 5);

                    //// Give the player 5 money
                    //playerInventory.AddMoney(randomPayoutMultiplier * numFish);
                }
                else
                {
                    msg = "You don't have enough " + fish_type.ToString() + " fish.";
                    Debug.Log(msg);
                    talk_to_player(msg);
                    if (Random.value < 0.9f)
                    {
                        inventoryController.AddItemToInv("coin", 1); // debugging code randomly give player 1 coin
                        Debug.Log("But go ahead and take this coin to help you on your travels.");
                        talk_to_player(msg + " But go ahead and take this coin to help you on your travels.");
                    }
                }

            }

        }
    }

    public void OnSliderChanged()
    {
        //// Update fish_amount_text
        //fish_amount_text.text = fish_slider.value.ToString();

        //// Update fish_price_text
        //fish_price_text.text = (fish_slider.value * fish_price).ToString();
    }

    public void TradeFish()
    {
        // Trade fishes for coins
        // int trade_amount = (int)fish_slider.value * fish_price;
        int trade_amount = buy_amount * fish_price;
        inventoryController.RemoveItemToInv(fish_type, buy_amount);
        // inventoryController.RemoveItemToInv(fish_type, (int)fish_slider.value);
        inventoryController.AddItemToInv("coin", trade_amount);
        // debugging code randomly give player 1 coin
        // Debug.Log("Trade made " + trade_amount.ToString() + " coins given " + fish_slider.value.ToString() + " fish of type " + fish_type.ToString() + " taken.");
        Debug.Log("Trade made " + trade_amount.ToString() + " coins given, " + buy_amount.ToString() + " fish of type: " + fish_type.ToString() + " taken.");

        // Hide the trading GUI
        canvas.SetActive(false);
    }


    public void talk_to_player(string talk_to)
    {
        StartCoroutine(talk_to_playerWritethenEraseText(talk_to));
    }

    private IEnumerator talk_to_playerWritethenEraseText(string text)
    {
        talk_to_playerText.text = text;

        yield return new WaitForSeconds(timeToErase);

        talk_to_playerText.text = "";
    }
}



