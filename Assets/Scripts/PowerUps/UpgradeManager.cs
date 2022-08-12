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
        currentData.GearCost += currentData.GearAddAmountCost;

        // Save persistently data first
        List<PowerUpData> datas = new List<PowerUpData>();
        foreach (OneTimePowerUp powerUp in oneTimePowerUpList.powerUpList)
        {
            if(powerUp.PowerUpData.ID == ID)
                datas.Add(currentData);
            else
                datas.Add(powerUp.PowerUpData);
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
}
