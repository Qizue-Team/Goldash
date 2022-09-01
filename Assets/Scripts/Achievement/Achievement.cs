using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Achievement : MonoBehaviour
{
    public static event Action<string> OnAchievementComplete;

    public AchievementData Data { get => data; }

    [Header("Data Settings")]
    [SerializeField]
    protected AchievementData data;

    public abstract void UpdateCurrentValue();
    public abstract void ResetAchievementValue();

    public virtual void CompleteAchievement()
    {
        if (data.IsMaxTier()) 
            return; // In HERE if you want to remove/destroy the achievement when completed

        Debug.Log("Obtain Reward HERE");
        OnAchievementComplete?.Invoke(Data.Description);
        data.NextTier();
        ResetAchievementValue();
    }

    public virtual void ObtainReward()
    {
        Reward reward = data.GetReward();
        if(reward.GearReward > 0)
        {
            int gearCount = DataManager.Instance.LoadTotalGearCount();
            gearCount += reward.GearReward;
            DataManager.Instance.SaveTotalGearCount(gearCount);
        }
        if(reward.SkinReward != null)
        {
            reward.SkinReward.Unlock(); // Cost must be 0
            SkinManager.Instance.AddSkin(reward.SkinReward);
        }
    }
}
