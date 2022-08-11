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

    public void Upgrade(int ID)
    {
        Debug.Log("Upgrade for " + ID);
        // Upgrade/Update powerup data here
    }

    private void Start()
    {
        foreach(OneTimePowerUp powerUp in oneTimePowerUpList.powerUpList)
        {
            GameObject shopEntryObj = Instantiate(shopEntryPrefab, shopContent);
            PowerUpShopEntry shopEntry = shopEntryObj.GetComponent<PowerUpShopEntry>();
            shopEntry.SetEntry(powerUp.PowerUpData.ID, powerUp.PowerUpData.Icon, powerUp.PowerUpData.CurrentLevel, powerUp.PowerUpData.Description, powerUp.PowerUpData.IsAdditional, powerUp.PowerUpData.NextStat, powerUp.PowerUpData.StatLabel, powerUp.PowerUpData.GearCost);
        }
    }
}
