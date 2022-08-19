using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [Header("References")]
    [SerializeField]
    private GameObject shopEntryPrefab;
    [SerializeField]
    private OneTimePowerUpList oneTimePowerUpList;
    [SerializeField]
    private Transform shopContent;

    public PowerUpData Upgrade(int ID)
    {
        Debug.Log("Upgrade for " + ID);
        // Upgrade/Update powerup data
        // Get the current powerup by ID
        PowerUpData currentData = oneTimePowerUpList.GetPowerUpByID(ID).PowerUpData;

        // Actual Upgrade
        currentData.CurrentLevel++;
        if (currentData.IsAdditional)
        {
            currentData.CurrentStat += currentData.StatAmount;
            currentData.NextStat = currentData.CurrentStat + currentData.StatAmount;
        }
        else
        {
            currentData.CurrentStat -= currentData.StatAmount;
            currentData.NextStat = currentData.CurrentStat - currentData.StatAmount;
        }
        currentData.CurrentGearCost = (int)CalculateNextCost(currentData.GearBaseCost, currentData.CostGrowthFactor, currentData.TimeStep, currentData.CurrentLevel);

        // Save persistently data first
        List<SerializablePowerUpData> datas = new List<SerializablePowerUpData>();
        int index = 0;
        foreach (OneTimePowerUp powerUp in oneTimePowerUpList.powerUpList)
        {
            if(powerUp.PowerUpData.ID == ID)
            {
                datas.Add(new SerializablePowerUpData());
                datas[index].ID = ID;
                datas[index].CurrentLevel = currentData.CurrentLevel;
                datas[index].CurrentStat = currentData.CurrentStat;
                datas[index].NextStat = currentData.NextStat;
                datas[index].GearCost = currentData.CurrentGearCost;
            }
            else
            {
                datas.Add(new SerializablePowerUpData());
                datas[index].ID = powerUp.PowerUpData.ID;
                datas[index].CurrentLevel = powerUp.PowerUpData.CurrentLevel;
                datas[index].CurrentStat = powerUp.PowerUpData.CurrentStat;
                datas[index].NextStat = powerUp.PowerUpData.NextStat;
                datas[index].GearCost = powerUp.PowerUpData.CurrentGearCost;
            }
                
            index++;
        }
        DataManager.Instance.SavePowerUpData(datas);

        // Force update data with update method in the powerupdata
        currentData.UpdateData(); // Only current or all? -> maybe better all
        // Return new data
        return currentData;
    }

    private void Start()
    {
        foreach(OneTimePowerUp powerUp in oneTimePowerUpList.powerUpList)
        {
            powerUp.PowerUpData.UpdateData();
            GameObject shopEntryObj = Instantiate(shopEntryPrefab, shopContent);
            PowerUpShopEntry shopEntry = shopEntryObj.GetComponent<PowerUpShopEntry>();
            shopEntry.SetEntry(powerUp.PowerUpData);
        }
    }

    private static float CalculateNextCost(float baseCost, float growthFactor, float timeRequired, int currentLevel)
    {
        if (growthFactor < 0)
            Debug.LogError("[UpgradeManager CalculateNextCost]: Growth Factor cannot be < 0");

        float cost = baseCost * Mathf.Pow(growthFactor, (currentLevel / timeRequired));
        return cost;
    }
}
