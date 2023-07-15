using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin_found : MonoBehaviour
{

    public int minAmount = 1;
    public int maxAmount = 5;

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
            int randomAmount = Random.Range(minAmount, maxAmount + 1);
            inventoryController.AddItemToInv("coin", randomAmount);

            Destroy(gameObject);
        }
    }

}
