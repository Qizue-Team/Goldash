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
        Debug.Log("Data.GiveReward");
        if (data.IsMaxTier())
        {
            Debug.Log("Destroy/Remove this achievement");
            return;
        }
        OnAchievementComplete?.Invoke(Data.Description);
        data.NextTier();
        ResetAchievementValue();
    }
}
