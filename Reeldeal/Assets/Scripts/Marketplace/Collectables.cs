using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private PlayerCollector pc;
    public bool isIncrementable = true;
    public bool isCollected = false;
    public bool isCollectable = true;

    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.Find("InventoryGUI").GetComponent<Inventory>();
    }



    public void OnTriggerEnter(Collider other)
    {


        if (isCollectable)
        {
            inventory.AddItem("CollectableCube", true);
            isCollected = true;
            Destroy(gameObject);
        }

    }
}