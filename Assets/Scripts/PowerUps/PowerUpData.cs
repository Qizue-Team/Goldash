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
    public int CurrentGearCost { get => currentGearCost; set => currentGearCost = value; }
    public int CostGrowthFactor { get => costGrowthFactor; set => costGrowthFactor = value; }
    public int TimeStep { get => timeStep; set => timeStep = value; }
    public int GearBaseCost { get => gearBaseCost; set => gearBaseCost = value; }

    [Header("Data: General")]
    [SerializeField]
    private int id;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private string description;
    [Header("Data: Stats")]
    [SerializeField]
    private bool isAdditional;
    [SerializeField]
    private float currentStat;
    [SerializeField]
    private int statAmount;
    [SerializeField]
    private float nextStat;
    [SerializeField]
    private string statLabel;
    [Header("Data: Cost")]
    [SerializeField]
    private int gearBaseCost;
    [SerializeField]
    private int costGrowthFactor;
    [SerializeField]
    private int timeStep;
    [SerializeField]
    private int currentGearCost;

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
                CurrentLevel = data.CurrentLevel;
                CurrentStat = data.CurrentStat;
                NextStat = data.NextStat;
                CurrentGearCost = data.GearCost;
            }
        }
    }
}
