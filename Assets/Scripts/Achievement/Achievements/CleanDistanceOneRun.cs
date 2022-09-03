using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanDistanceOneRun : Achievement
{
    public override void ResetAchievementValue()
    {
        data.SetCurrentValue(data.CurrentValue);
    }

    public override void UpdateCurrentValue()
    {
        if (AchievementManager.Instance.TotalDistanceOneRun < data.CurrentValue)
            return;
        data.SetCurrentValue(AchievementManager.Instance.TotalDistanceOneRun);

        int completed = 0;
        for(int i=data.CurrentTier; i<data.MaxTier; ++i)
        {
            if(AchievementManager.Instance.TotalDistanceOneRun >= data.GetMaxValue(i))
                completed++;
        }

        for(int i=0; i<completed; ++i)
            CompleteAchievement();
    }
}
