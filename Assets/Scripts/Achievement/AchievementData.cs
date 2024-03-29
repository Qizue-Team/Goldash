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
    private float[] maxValues;
    [SerializeField]
    private Reward[] rewards;
    [SerializeField]
    private bool isComplete;

    public float GetCurrentMaxValue() => maxValues[currentTier];
   
    public Reward GetReward() => rewards[currentTier];

    public void SetCurrentValue(float newValue)
    {
        currentValue = newValue;
        if (isComplete)
            currentValue = GetCurrentMaxValue();
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
    public bool IsTierComplete() => currentValue >= GetCurrentMaxValue();
    public bool CompleteAchievement() => isComplete = true;
    public float GetMaxValue(int index)
    {
        if (index >= maxTier)
            return -1;
        return maxValues[index];
    }

    public void ResetAchievement()
    {
        currentTier = 0;
        currentValue = 0;
        isComplete = false;
        foreach(Reward reward in rewards)
        {
            if(reward.SkinReward != null)
            {
                reward.SkinReward.HideSkin();
            }
        }
    }
}
