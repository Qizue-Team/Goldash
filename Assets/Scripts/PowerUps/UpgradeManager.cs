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
        // TO-DO: Upgrade/Update powerup data here 

        // Save persistent -> in the persistent file first
        List<PowerUpData> datas = new List<PowerUpData>();
        foreach (OneTimePowerUp powerUp in oneTimePowerUpList.powerUpList)
        {
            datas.Add(powerUp.PowerUpData);
        }
        DataManager.Instance.SavePowerUpData(datas);
        
        // TO-DO: Force update data with update method in the powerupdata
        return null; // TO-DO: Return new data
    }

    private void Start()
    {
        foreach(OneTimePowerUp powerUp in oneTimePowerUpList.powerUpList)
        {
            GameObject shopEntryObj = Instantiate(shopEntryPrefab, shopContent);
            PowerUpShopEntry shopEntry = shopEntryObj.GetComponent<PowerUpShopEntry>();
            shopEntry.SetEntry(powerUp.PowerUpData);
        }
    }
}
