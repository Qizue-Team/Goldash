using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PowerUp Data", menuName = "Data/PowerUp Data")]
[System.Serializable]
public class PowerUpData : ScriptableObject
{
    public int ID { get => id; }
    public Sprite Icon { get => icon; }
    public int CurrentLevel { get => currentLevel; }
    public string Description { get => description; }
    public int StatAmount { get => statAmount; }
    public bool IsAdditional { get => isAdditional; }
    public float CurrentStat { get => currentStat; }
    public float NextStat { get => nextStat; }
    public string StatLabel { get => statLabel; }
    public int GearCost { get => gearCost; }
    public int GearAddAmountCost { get => gearAddAmountCost; }

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

    // TO-DO: Run this in the Manager -> OnStart for all datas should be fine, trust me
    public void UpdateData()
    {
        // TO-DO: Read from file new data based on ID, and update them
    }
}
