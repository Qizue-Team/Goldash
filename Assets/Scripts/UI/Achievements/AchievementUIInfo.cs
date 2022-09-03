using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementUIInfo : MonoBehaviour
{
    public string Description { get; private set; }
    public int CurrentTier { get; private set; }

    public void SetInfos(Achievement achievement)
    {
        Description = achievement.Data.Description;
        CurrentTier = achievement.Data.CurrentTier;
    }
}
