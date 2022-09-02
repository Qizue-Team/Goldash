using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPopUpController : Singleton<AchievementPopUpController>
{
    [Header("References")]
    [SerializeField]
    private AchievementPopUp popUp;

    private Queue<Achievement> _achievementsQueue;

    protected override void Awake()
    {
        base.Awake();
        _achievementsQueue = new Queue<Achievement>();
    }

    private void OnEnable() => Achievement.OnAchievementComplete += AddAchievement;

    private void OnDisable() => Achievement.OnAchievementComplete -= AddAchievement;

    private void AddAchievement(Achievement achievement)
    {
        _achievementsQueue.Enqueue(achievement);
        popUp.Play(achievement, PlayNextAchievement);
    }

    private void PlayNextAchievement()
    {
        _achievementsQueue.Dequeue();
        if (_achievementsQueue.Count == 0)
            return;
        popUp.Play(_achievementsQueue.Peek(), PlayNextAchievement);
    }
}
