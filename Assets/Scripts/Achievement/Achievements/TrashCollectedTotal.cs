using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollectedTotal : Achievement
{
    public override void ResetAchievementValue()
    {
        data.SetCurrentValue(data.CurrentValue);
    }

    public override void UpdateCurrentValue()
    {
        if (AchievementManager.Instance.TrashCollectedTotal < data.CurrentValue)
            return;
        data.SetCurrentValue(AchievementManager.Instance.TrashCollectedTotal);
        if (data.IsTierComplete())
            CompleteAchievement();
    }
}
