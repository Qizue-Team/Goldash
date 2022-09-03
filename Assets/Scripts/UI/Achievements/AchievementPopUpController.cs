using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPopUpController : Singleton<AchievementPopUpController>
{
    [Header("References")]
    [SerializeField]
    private AchievementPopUp popUp;

    private Queue<AchievementUIInfo> _achievementsQueue;

    protected override void Awake()
    {
        base.Awake();
        _achievementsQueue = new Queue<AchievementUIInfo>();
    }

    private void OnEnable() => Achievement.OnAchievementComplete += AddAchievement;

    private void OnDisable() => Achievement.OnAchievementComplete -= AddAchievement;

    private void AddAchievement(Achievement achievement)
    {
        AchievementUIInfo info = new AchievementUIInfo();
        info.SetInfos(achievement);
        _achievementsQueue.Enqueue(info);
        popUp.Play(info, PlayNextAchievement);
    }

    private void PlayNextAchievement()
    {
        _achievementsQueue.Dequeue();
        if (_achievementsQueue.Count == 0)
            return;
        popUp.Play(_achievementsQueue.Peek(), PlayNextAchievement);
    }
}
