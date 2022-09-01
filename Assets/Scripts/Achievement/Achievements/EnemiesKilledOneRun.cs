using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesKilledOneRun : Achievement
{
    public override void UpdateCurrentValue()
    {
        data.SetCurrentValue(AchievementManager.Instance.EnemiesKilledOneRun);
    }

    public override void CompleteAchievement()
    {
        base.CompleteAchievement();
        // Dunno if I have to do anything else atm, I think not
    }
}
