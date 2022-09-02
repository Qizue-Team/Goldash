using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : Singleton<AchievementManager>
{
    public List<Achievement> Achievements { get => achievements; }
    
    // Values needed for achievements
    public int EnemiesKilledOneRun { get; set; }

    [Header("Achievement List")]
    [SerializeField]
    private List<Achievement> achievements = new List<Achievement>();

    public void UpdateAchievements()
    {
        foreach(var achievement in achievements)
            achievement.UpdateCurrentValue();

        // Save data in persistence / DataManager
    }

    private void OnStart()
    {
        // Load data from persistence / DataManager
    }
}
