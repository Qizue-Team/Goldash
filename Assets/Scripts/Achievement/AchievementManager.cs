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
        List<SerializableAchievementData> dataList = new List<SerializableAchievementData>();
        foreach(var achievement in achievements)
        {
            SerializableAchievementData data = new SerializableAchievementData();
            achievement.UpdateCurrentValue();
            data.CurrentTier = achievement.Data.CurrentTier;
            data.CurrentValue = achievement.Data.CurrentValue;
            dataList.Add(data);
        }
        // Save persistence achievements data
        DataManager.Instance.SaveAchievementsData(dataList);

        SaveManagerValues();
    }

    private void Start()
    {
        LoadDataAchievements();
        LoadDataAchievementsManager();
    }

    private void SaveManagerValues()
    {
        SerializableAchievementManagerData data = new SerializableAchievementManagerData();
        data.EnemiesKilledOneRun = EnemiesKilledOneRun;
        DataManager.Instance.SaveAchievementManagerData(data);
    }

    private void LoadDataAchievements()
    {
        List<SerializableAchievementData> dataList = DataManager.Instance.LoadAchievementsData();
        if (dataList == null)
            return;
        int index = 0;
        foreach (var achievement in achievements)
        {
            achievement.Data.SetCurrentValue(dataList[index].CurrentValue);
            achievement.Data.SetCurrentTier(dataList[index].CurrentTier);
            index++;
        }
    }

    private void LoadDataAchievementsManager()
    {
        SerializableAchievementManagerData managerData = DataManager.Instance.LoadAchievementManagerData();
        if (managerData == null)
            return;
        EnemiesKilledOneRun = managerData.EnemiesKilledOneRun;
    }
}
