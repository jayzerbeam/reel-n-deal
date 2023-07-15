using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_fish_found : MonoBehaviour
{

    public int minAmount = 1;
    public int maxAmount = 5;

    private string[] fishTypes = { "blue", "pink", "orange" };

    private string fish_type = "blue";

    private hud_gui_controller inventoryController;

    private void Awake()
    {
        inventoryController = FindObjectOfType<hud_gui_controller>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int minAmount = Random.Range(1, 9);
            int maxAmount = Random.Range(10, 26);
            fish_type = fishTypes[Random.Range(0, fishTypes.Length)];
            int randomAmount = Random.Range(minAmount, maxAmount + 1);
            inventoryController.AddItemToInv(fish_type, randomAmount);

            Destroy(gameObject);
        }
    }

}