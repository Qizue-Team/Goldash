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

    public virtual void CompleteAchievement()
    {
        OnAchievementComplete?.Invoke(Data.Description);
    }
}
