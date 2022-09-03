using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementData", menuName = "Data/Achievement Data")]
public class AchievementData : ScriptableObject
{
    public string Description { get => description; }
    public int MaxTier { get => maxTier; }
    public int CurrentTier { get => currentTier; }
    public float CurrentValue { get => currentValue; }
    public bool IsComplete { get=> isComplete; }

    [SerializeField]
    private string description;
    [SerializeField]
    private int maxTier;
    [SerializeField]
    private int currentTier;
    [SerializeField]
    private float currentValue;
    [SerializeField]
    private int[] maxValues;
    [SerializeField]
    private Reward[] rewards;
    [SerializeField]
    private bool isComplete;

    public int GetMaxValue() => maxValues[currentTier];
    public Reward GetReward() => rewards[currentTier];

    public void SetCurrentValue(float newValue)
    {
        if (newValue > GetMaxValue())
            return;
        currentValue = newValue;
    }

    public void NextTier()
    {
        if((currentTier+1)>maxTier)
            return;
        currentTier++;
    }

    public void SetCurrentTier(int tier)
    {
        if (tier > maxTier)
            return;
        currentTier = tier;
    }

    public bool IsMaxTier() => maxTier-1 == currentTier;
    public bool IsTierComplete() => currentValue >= GetMaxValue();
    public bool CompleteAchievement() => isComplete = true;
}
