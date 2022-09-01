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

}
