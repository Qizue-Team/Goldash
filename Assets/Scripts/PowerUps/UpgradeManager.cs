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

    private void Start()
    {
        foreach(var powerUp in oneTimePowerUpList.powerUpList)
        {
            Instantiate(shopEntryPrefab, shopContent);
        }
    }

}
