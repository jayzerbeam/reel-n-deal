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
    private string[] fishTypes = { "blue", "pink", "orange" };
    public float radiusToTrade = 2.5f; // Set this to whatever radius you want
    public TextMeshProUGUI talk_to_playerText;
    public float timeToErase = 10f;
    private string msg;



    public GameObject canvas;
    public GameObject fish_blue_image;
    public GameObject fish_pink_image;
    public GameObject fish_orange_image;
    public Slider fish_slider;
    public TextMeshProUGUI fish_amount_text;
    public TextMeshProUGUI fish_price_text;
    public Button trading_button;

    private int fish_price;
    private string fish_type;

    private hud_gui_controller inventoryController;

    private void Awake()
    {
        inventoryController = FindObjectOfType<hud_gui_controller>();
    }

    void Start()
    {
        //playerInventory = player.GetComponent<playerInventory>();

        fish_price = 0;
        // Assign the Button's listener
        trading_button.onClick.AddListener(TradeFish);
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

                // Randomly select fish type
                fish_type = fishTypes[Random.Range(0, fishTypes.Length)];

                // Randomly assign fish price from 1 to 5
                fish_price = Random.Range(1, 6);

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

                    switch (fish_type)
                    {
                        case "blue":
                            fish_blue_image.SetActive(true);
                            fish_pink_image.SetActive(false);
                            fish_orange_image.SetActive(false);
                            fish_slider.minValue = 0;
                            fish_slider.maxValue = inventoryController.blue_fish_Count;
                            Debug.Log("Slider max value set to blue " + fish_slider.maxValue.ToString());
                            break;
                        case "pink":
                            fish_blue_image.SetActive(false);
                            fish_pink_image.SetActive(true);
                            fish_orange_image.SetActive(false);
                            fish_slider.minValue = 0;
                            fish_slider.maxValue = inventoryController.pink_fish_Count;
                            Debug.Log("Slider max value set to pink " + fish_slider.maxValue.ToString());
                            break;
                        case "orange":
                            fish_blue_image.SetActive(false);
                            fish_pink_image.SetActive(false);
                            fish_orange_image.SetActive(true);
                            fish_slider.minValue = 0;
                            fish_slider.maxValue = inventoryController.orange_fish_Count;
                            Debug.Log("Slider max value set to orange " + fish_slider.maxValue.ToString());
                            break;
                        default:
                            Debug.Log("Invalid item type: " + fish_type.ToString());
                            fish_slider.minValue = 0;
                            fish_slider.maxValue = 0;
                            Debug.Log("Slider max value set to 0");
                            break;
                    }

                    // Update fish_amount_text
                    fish_amount_text.text = fish_slider.value.ToString();

                    // Update fish_price_text
                    fish_price_text.text = (fish_slider.value * fish_price).ToString();



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
        // Update fish_amount_text
        fish_amount_text.text = fish_slider.value.ToString();

        // Update fish_price_text
        fish_price_text.text = (fish_slider.value * fish_price).ToString();
    }

    public void TradeFish()
    {
        // Trade fishes for coins
        int trade_amount = (int)fish_slider.value * fish_price;
        inventoryController.RemoveItemToInv(fish_type, (int)fish_slider.value);
        inventoryController.AddItemToInv("coin", trade_amount); 
        // debugging code randomly give player 1 coin
        Debug.Log("Trade made " + trade_amount.ToString() + " coins given " + fish_slider.value.ToString() + "fish of type " + fish_type.ToString() + " taken.");

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


