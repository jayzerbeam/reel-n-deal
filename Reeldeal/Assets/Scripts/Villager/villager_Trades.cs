using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villager_Trades : MonoBehaviour
{
    public string villager_type;
    private string[] villagerTypes = { "lake", "river", "ocean" };
    //private string[] fishTypes = { "lake_f_1", "lake_f_2", "lake_f_3", "lake_f_4", "ocean_f_1", "ocean_f_2", "ocean_f_3", "ocean_f_4", "ocean_shark", "river_f_1", "river_f_2", "river_f_3", "river_f_4" };
    private string[] lakeFishTypes = { "lake_f_1", "lake_f_2", "lake_f_3", "lake_f_4" };
    private string[] oceanFishTypes = { "ocean_f_1", "ocean_f_2", "ocean_f_3", "ocean_f_4", "ocean_shark" };
    private string[] riverFishTypes = { "river_f_1", "river_f_2", "river_f_3", "river_f_4" };
    public string fish_type;

    public int max_fish_amount;
    public int max_fish_price;
    private int min_fish_price = 1;
    public int fish_amount;
    public int fish_price;

    public int numOfTrades;


    // Start is called before the first frame update
    void Start()
    {
        SetVillagerType();
        SetFishType();
        SetTradeAmount();
        //ShuffleTrade();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetVillagerType()
    {
        int index = Random.Range(0, villagerTypes.Length);
        villager_type = villagerTypes[index];
    }

    private void SetFishType()
    {
        switch (villager_type)
        {
            case "lake":
                fish_type = lakeFishTypes[Random.Range(0, lakeFishTypes.Length)];
                break;
            case "river":
                fish_type = riverFishTypes[Random.Range(0, riverFishTypes.Length)];
                break;
            case "ocean":
                fish_type = oceanFishTypes[Random.Range(0, oceanFishTypes.Length)];
                break;
            default:
                Debug.LogError("Unknown villager type: " + villager_type);
                break;
        }
    }

    public void SetTradeAmount()
    {
        // Random.Range is inclusive for min and exclusive for max, so we have 1 to max_amount


        fish_amount = Random.Range(min_fish_price, max_fish_amount + 1);

        fish_price = Random.Range(min_fish_price, max_fish_price + 1);
    }

    public void ShuffleTrade()
    {
        SetFishType();
        SetTradeAmount();
    }

    public void IncrementNumOfTrades()
    {
        numOfTrades++;
        if (Random.value < 0.75f) // 75% chance that villager can give better trades next time
        {
            max_fish_price += 1;
            Debug.Log("Max fish price increased: " + max_fish_price);
            if (Random.value < 0.9f) // 90% chance that villager will give better trades next time
            {
                min_fish_price += 1;
            }
        }
        ShuffleTrade();
        
    }
}

