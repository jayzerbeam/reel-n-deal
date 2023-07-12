using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketplaceItems : MonoBehaviour
{
    public GameObject RodUpgradeGUI;
    public GameObject RodStandardGUI;
    public GameObject BootsGUI;
    public GameObject TNTGUI;
    public GameObject FishBoatGUI;
    public GameObject WoodBoatGUI;
    private GameObject triggeredObject;
    public Inventory inventory;

    private AudioSource audioPlay; 

    public void Start()
    {
        Transform findChild = GameObject.Find("UICanvas").transform;
        RodUpgradeGUI = findChild.Find("RodUpgradeGUI").gameObject;
        RodStandardGUI = findChild.Find("RodStandardGUI").gameObject;
        BootsGUI = findChild.Find("BootsGUI").gameObject;
        TNTGUI = findChild.Find("TNTGUI").gameObject;
        FishBoatGUI = findChild.Find("FishBoatGUI").gameObject;
        WoodBoatGUI = findChild.Find("WoodBoatGUI").gameObject;

        inventory = GameObject.Find("InventoryGUI").GetComponent<Inventory>();

        //audioPlay = GetComponent<AudioSource>();
        audioPlay.playOnAwake = false; 
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            if (gameObject.CompareTag("FishingRodUpgrade") && RodUpgradeGUI != null)
            {
                RodUpgradeGUI.SetActive(true);
                triggeredObject = gameObject;
            }

            else if (gameObject.CompareTag("FishingRodStandard") && RodStandardGUI != null)
            {
                RodStandardGUI.SetActive(true);
                triggeredObject = gameObject;
            }

            else if (gameObject.CompareTag("Boots") && BootsGUI != null)
            {
                BootsGUI.SetActive(true);
                triggeredObject = gameObject;
            }

            else if (gameObject.CompareTag("TNT") && TNTGUI != null)
            {
                TNTGUI.SetActive(true);
                triggeredObject = gameObject;
            }

            else if (gameObject.CompareTag("FishingBoat") && FishBoatGUI != null)
            {
                FishBoatGUI.SetActive(true);
                triggeredObject = gameObject;
            }

            else if (gameObject.CompareTag("WoodenBoat") && WoodBoatGUI != null)
            {
                WoodBoatGUI.SetActive(true);
                triggeredObject = gameObject;
            }
        }

    }

    private void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            if (gameObject.CompareTag("FishingRodUpgrade") && RodUpgradeGUI != null)
                RodUpgradeGUI.SetActive(false);

            else if (gameObject.CompareTag("FishingRodStandard") && RodStandardGUI != null)
                RodStandardGUI.SetActive(false);

            else if (gameObject.CompareTag("Boots") && BootsGUI != null)
                BootsGUI.SetActive(false);

            else if (gameObject.CompareTag("TNT") && TNTGUI != null)
                TNTGUI.SetActive(false);

            else if (gameObject.CompareTag("FishingBoat") && FishBoatGUI != null)
                FishBoatGUI.SetActive(false);

            else if (gameObject.CompareTag("WoodenBoat") && WoodBoatGUI != null)
                WoodBoatGUI.SetActive(false);
        }
        triggeredObject = null;

    }


    public void OnButtonClick()
    {
        audioPlay = GetComponent<AudioSource>();

        if (gameObject.CompareTag("FishingRodUpgrade") && RodUpgradeGUI != null && triggeredObject != null)
        {
            RodUpgradeGUI.SetActive(false);
            audioPlay.Play();
            //audioPlay = GetComponent<AudioSource>();
            inventory.AddItem("FishingRodUpgrade", false);
            Destroy(triggeredObject);
            triggeredObject = null;
        }

        else if (gameObject.CompareTag("FishingRodStandard") && RodStandardGUI != null && triggeredObject != null)
        {
            RodStandardGUI.SetActive(false);
            inventory.AddItem("FishingRodStandard", false);
            //audioPlay.Play();
            Destroy(triggeredObject);
            triggeredObject = null;
        }


        else if (gameObject.CompareTag("Boots") && BootsGUI != null && triggeredObject != null)
        {
            BootsGUI.SetActive(false);
            inventory.AddItem("Boots", false);
            //audioPlay.Play();
            Destroy(triggeredObject);
            triggeredObject = null;
        }

        else if (gameObject.CompareTag("TNT") && TNTGUI != null && triggeredObject != null)
        {
            TNTGUI.SetActive(false);
            inventory.AddItem("TNT", false);
            //audioPlay.Play();
            Destroy(triggeredObject);
            triggeredObject = null;
        }

        else if (gameObject.CompareTag("FishingBoat") && FishBoatGUI != null && triggeredObject != null)
        {
            FishBoatGUI.SetActive(false);
            inventory.AddItem("FishingBoat", false);
            //audioPlay.Play();
            Destroy(triggeredObject);
            triggeredObject = null;
        }

        else if (gameObject.CompareTag("WoodenBoat") && WoodBoatGUI != null && triggeredObject != null)
        {
            WoodBoatGUI.SetActive(false);
            inventory.AddItem("WoodenBoat", false);
            //audioPlay.Play();
            Destroy(triggeredObject);
            triggeredObject = null;
        }



    }

}

