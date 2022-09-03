using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollectedOneRun : Achievement
{
    [SerializeField]
    private string trashName;

    public override void ResetAchievementValue()
    {
        data.SetCurrentValue(data.CurrentValue);
    }

    public override void UpdateCurrentValue()
    {
        if (!AchievementManager.Instance.TrashCountDictionary.TryGetValue(trashName, out var count))
            return;
        if (AchievementManager.Instance.TrashCountDictionary[trashName] < data.CurrentValue)
            return;
        data.SetCurrentValue(AchievementManager.Instance.TrashCountDictionary[trashName]);
        if (data.IsTierComplete())
            CompleteAchievement();
    }
}
