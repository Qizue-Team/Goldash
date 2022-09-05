using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesKilledTotal : Achievement
{
    public override void ResetAchievementValue()
    {
        data.SetCurrentValue(data.CurrentValue);
    }

    public override void UpdateCurrentValue()
    {
        if (AchievementManager.Instance.EnemiesKilledTotal < data.CurrentValue)
            return;
        data.SetCurrentValue(AchievementManager.Instance.EnemiesKilledTotal);
        if (data.IsTierComplete())
            CompleteAchievement();
    }
}
