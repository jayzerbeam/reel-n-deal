using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_fish_found : MonoBehaviour
{

    public int minAmount = 1;
    public int maxAmount = 5;

    private string[] fishTypes = { "lake_f_1", "lake_f_2", "lake_f_3", "lake_f_4", "ocean_f_1", "ocean_f_2", "ocean_f_3", "ocean_f_4", "ocean_shark", "river_f_1", "river_f_2", "river_f_3", "river_f_4" };

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
            int fishes = Random.Range(3, 13);
            for (int i = 0; i < fishes; i++)
            {
                fish_type = fishTypes[Random.Range(0, fishTypes.Length)];
                int randomAmount = Random.Range(minAmount, maxAmount + 1);
                inventoryController.AddItemToInv(fish_type, randomAmount);
            }
                

            Destroy(gameObject);
        }
    }

}