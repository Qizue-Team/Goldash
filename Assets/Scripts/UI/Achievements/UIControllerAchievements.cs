using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerAchievements : Singleton<UIControllerAchievements>
{
    [Header("References")]
    [SerializeField]
    private Transform listContent;
    [SerializeField]
    private GameObject achievementEntry;

    private void Start()
    {
        foreach(Achievement achievement in AchievementManager.Instance.Achievements)
        {
            GameObject obj = Instantiate(achievementEntry, listContent);
            AchievementEntry entry = obj.GetComponent<AchievementEntry>();
            entry.SetEntry(achievement);
        }
    }

}
