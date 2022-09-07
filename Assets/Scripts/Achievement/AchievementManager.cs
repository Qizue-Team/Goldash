using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementManager : Singleton<AchievementManager>
{
    public List<Achievement> Achievements { get => achievements; }
    
    // Values needed for achievements
    public int EnemiesKilledOneRun { get; set; }
    public Dictionary<string, int> TrashCountDictionary { get; set; }
    public int EnemiesKilledTotal { get; set; }

    public float TotalDistanceOneRun { get; set; }

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
            data.IsComplete = achievement.Data.IsComplete;
            dataList.Add(data);
        }
        // Save persistence achievements data
        DataManager.Instance.SaveAchievementsData(dataList);

        SaveManagerValues();
    }

    private void Start()
    {
        TrashCountDictionary = new Dictionary<string, int>();
        LoadDataAchievements();
        LoadDataAchievementsManager();
        if (SceneManager.GetActiveScene().name.Equals("Achievements"))
        {
            UIControllerAchievements.Instance.ShowAchievements();
        }
    }

    private void SaveManagerValues()
    {
        SerializableAchievementManagerData data = new SerializableAchievementManagerData();
        data.EnemiesKilledOneRun = EnemiesKilledOneRun;
        data.TotalDistanceOneRun = TotalDistanceOneRun;
        data.EnemiesKilledTotal = EnemiesKilledTotal;
        data.TrashNames = new List<string>();
        data.TrashCounts = new List<int>();
        foreach(string name in TrashCountDictionary.Keys)
        {
            data.TrashNames.Add(name);
            data.TrashCounts.Add(TrashCountDictionary[name]);
        }
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
            if (dataList[index].IsComplete)
                achievement.Data.CompleteAchievement();
            index++;
        }
    }

    private void LoadDataAchievementsManager()
    {
        SerializableAchievementManagerData managerData = DataManager.Instance.LoadAchievementManagerData();
        if (managerData == null)
            return;

        TrashCountDictionary.Clear();
        int index = 0;
        foreach(string name in managerData.TrashNames)
        {
            TrashCountDictionary.Add(name, managerData.TrashCounts[index]);
            index++;
        }

        EnemiesKilledOneRun = managerData.EnemiesKilledOneRun;
        TotalDistanceOneRun = managerData.TotalDistanceOneRun;
        EnemiesKilledTotal = managerData.EnemiesKilledTotal;
    }
}
