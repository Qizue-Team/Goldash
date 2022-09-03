using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesKilledOneRun : Achievement
{
    public override void ResetAchievementValue()
    {
        data.SetCurrentValue(0);
    }

    public override void UpdateCurrentValue()
    {
        if (AchievementManager.Instance.EnemiesKilledOneRun < data.CurrentValue)
            return;
        data.SetCurrentValue(AchievementManager.Instance.EnemiesKilledOneRun);
        if (data.IsTierComplete())
            CompleteAchievement();
    }

}
