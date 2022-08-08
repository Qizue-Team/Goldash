using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xPoke.CustomLog;

public class GameOverMenuPanel : GameMenuPanel
{
    // Add recap menu reference here
    [Header("GameOver References")]
    [SerializeField]
    private RecapPanel recapPanel;

    public void ResetPanel()
    {
        StopAllCoroutines();
        recapPanel.ResetEntries();
    }

    public override void Open()
    {
        base.Open();

        // Load data on recap panel
        StartCoroutine(COSetRecapPanel());
    }
   
    private IEnumerator COSetRecapPanel()
    {
        yield return new WaitForSeconds(1.0f);
        foreach (string name in TrashCollectedManager.Instance.TrashCountDictionary.Keys)
        {
            yield return recapPanel.COAddTrashEntry(name, TrashCollectedManager.Instance.TrashCountDictionary[name],
                (TrashCollectedManager.Instance.TrashCountDictionary[name] * TrashCollectedManager.Instance.TrashDictionary[name].ScorePoints),
                (TrashCollectedManager.Instance.TrashCountDictionary[name] * TrashCollectedManager.Instance.TrashDictionary[name].Gear));
        }

        yield return recapPanel.COSetScoreEntry(GameController.Instance.Score,
            TrashCollectedManager.Instance.GetTotalScoreFromTrash(),
            EnemyKilledManager.Instance.TotalPoints,
            (GameController.Instance.Score - TrashCollectedManager.Instance.GetTotalScoreFromTrash() - EnemyKilledManager.Instance.TotalPoints));

        recapPanel.SetGearEntry(TrashCollectedManager.Instance.GetTotalGearCount());
    }
}
