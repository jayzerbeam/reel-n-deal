using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    private List<string> collectedItems = new List<string>();
    private int itemCount = 0;

    private bool isInventoryOpen = false;

    private GameObject itemsPanel;
    private TextMeshProUGUI itemListText;
    private TextMeshProUGUI collectablesText;


    private void Start()
    {
        Transform findChild = GameObject.Find("UICanvas").transform;
        itemsPanel = findChild.Find("Items").gameObject;
        itemListText = itemsPanel.transform.Find("ItemsList").GetComponent<TextMeshProUGUI>();
        collectablesText = itemsPanel.transform.Find("Collectables").GetComponent<TextMeshProUGUI>();

        itemsPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            isInventoryOpen = !isInventoryOpen;
            UpdateInventoryGUI();
        }
    }

    public void AddItem(string itemName, bool isCollectable)
    {
        if (isCollectable)
        {
            itemCount++;
        }
        else
            collectedItems.Add(itemName);
    }

    public void RemoveItem(string itemName, bool isCollectable)
    {
        if (isCollectable)
        {
            itemCount--;
        }
        else
            collectedItems.Remove(itemName);
    }


    private void UpdateInventoryGUI()
    {
        if (isInventoryOpen)
        {
            itemsPanel.SetActive(true);
            Debug.Log("Inventory Opened");

            TextMeshProUGUI itemListText = itemsPanel.transform.Find("ItemsList").GetComponent<TextMeshProUGUI>();
            itemListText.text = "";
            foreach (string item in collectedItems)
            {

                itemListText.text += "- " + item + "\n";
                Debug.Log("Collected Item: " + item);
            }
            collectablesText.text = "Collectables: " + itemCount.ToString();
        }
        else
        {
            itemsPanel.SetActive(false);
            Debug.Log("Inventory Closed");
        }
    }
}