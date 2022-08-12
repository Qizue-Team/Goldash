using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PowerUp Data", menuName = "Data/PowerUp Data")]
public class PowerUpData : ScriptableObject
{
    public int ID { get => id; set => id = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public int CurrentLevel { get => currentLevel; set => currentLevel = value;  }
    public string Description { get => description; set => description = value; }
    public int StatAmount { get => statAmount; set => statAmount = value; }
    public bool IsAdditional { get => isAdditional; set => isAdditional = value; }
    public float CurrentStat { get => currentStat; set => currentStat = value;  }
    public float NextStat { get => nextStat; set => nextStat = value;  }
    public string StatLabel { get => statLabel; set => statLabel = value; }
    public int GearCost { get => gearCost; set => gearCost = value; }
    public int GearAddAmountCost { get => gearAddAmountCost; set => gearAddAmountCost = value; }

    [SerializeField]
    private int id;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private string description;
    [SerializeField]
    private int statAmount;
    [SerializeField]
    private bool isAdditional;
    [SerializeField]
    private float currentStat;
    [SerializeField]
    private float nextStat;
    [SerializeField]
    private string statLabel;
    [SerializeField]
    private int gearCost;
    [SerializeField]
    private int gearAddAmountCost;

    // Run this in the Managers -> OnStart for all data -> load updated data
    public void UpdateData()
    {
        // Read from file new data based on ID, and update them
        List<SerializablePowerUpData> datas = DataManager.Instance.LoadPowerUpData();
        if (datas == null)
            return;
        foreach(SerializablePowerUpData data in datas)
        {
            if(data.ID == ID)
            {
                Debug.Log("Updating ID " + ID + " READ CURRENT LEVEL: " + data.CurrentLevel);
                CurrentLevel = data.CurrentLevel;
                CurrentStat = data.CurrentStat;
                NextStat = data.NextStat;
                GearCost = data.GearCost;
            }
        }
    }
}
