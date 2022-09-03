using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Achievement : MonoBehaviour
{
    public static event Action<Achievement> OnAchievementComplete;

    public AchievementData Data { get => data; }

    [Header("Data Settings")]
    [SerializeField]
    protected AchievementData data;

    public abstract void UpdateCurrentValue();
    public abstract void ResetAchievementValue();

    public virtual void CompleteAchievement()
    {
        if (data.IsComplete)
            return;

        if (data.IsMaxTier())
            data.CompleteAchievement();

        ObtainReward();
        OnAchievementComplete?.Invoke(this);

        if (!data.IsMaxTier())
        {
            data.NextTier();
            ResetAchievementValue();
        }
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
