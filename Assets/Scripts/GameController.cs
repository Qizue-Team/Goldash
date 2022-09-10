using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xPoke.CustomLog;
using System;

public class GameController : Singleton<GameController>
{
    public const int MAX_SCORE = 9999999;
    public const int MIN_SCORE = 0;
    public const int MAX_TRASH_COUNT = 99999;
    public const int MIN_TRASH_COUNT = 0;

    public int Score { get; private set; }
    public int TrashCount { get; private set; }

    [Header("Player")]
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Vector3 playerSpawnPosition;

    [Header("Spawners")]
    [SerializeField]
    private TerrainTileSpawner terrainTileSpawner;
    [SerializeField]
    private PlatformSpawner platformSpawner;

    [Header("Audio")]
    [SerializeField]
    private AudioClip BGMRunClip;

    private GameObject _player;

    public void IncreaseScore(int amount)
    {
        if(amount<MIN_SCORE || amount > MAX_SCORE)
            return;
        Score += amount;
        if (Score > MAX_SCORE)
            Score = MAX_SCORE;
    }

    public void IncreaseTrashCount(int amount)
    {
        if (amount < MIN_TRASH_COUNT || amount > MAX_TRASH_COUNT )
            return;
        TrashCount += amount;
        if(TrashCount > MAX_TRASH_COUNT)
            TrashCount = MAX_TRASH_COUNT;
    }

    public void GameOver()
    {
        terrainTileSpawner.Stop();
        platformSpawner.Stop();

        // Audio
        AudioController.Instance.StopBGM();

        // Achievements values
        AchievementManager.Instance.TotalDistanceOneRun = Mathf.FloorToInt(terrainTileSpawner.TotalDistance);
        AchievementManager.Instance.UpdateAchievements();
        
        // Add trash collected to the general amount of account's trash
        int trash = DataManager.Instance.LoadTrashCount();
        trash += TrashCount;
        DataManager.Instance.SaveTrashCount(trash);

        // Save best score if score is the best
        DataManager.Instance.SaveBestScore(Score);

        // Save Total Gear Count
        int gear = DataManager.Instance.LoadTotalGearCount();
        gear += TrashCollectedManager.Instance.GetTotalGearCount();
        DataManager.Instance.SaveTotalGearCount(gear);

        UIController.Instance.SetOpenGameMenuPanel(true);

        // Logs
        CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "Trash Collected Saved - Current Trash Count: " + DataManager.Instance.LoadTrashCount());
        CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "Score Saved - Current Best Score: " + DataManager.Instance.LoadBestScore());
        CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "Total Gear Count Saved - Current Gear Count: " + DataManager.Instance.LoadTotalGearCount());
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "TRASH COLLECTED LIST: ");
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "START");
        foreach (string name in TrashCollectedManager.Instance.TrashCountDictionary.Keys)
        {
            CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, name +
                " (x" + TrashCollectedManager.Instance.TrashCountDictionary[name] + ") " +
                "Score: " + (TrashCollectedManager.Instance.TrashCountDictionary[name] * TrashCollectedManager.Instance.TrashDictionary[name].ScorePoints) +
                " Gear: " + (TrashCollectedManager.Instance.TrashCountDictionary[name] * TrashCollectedManager.Instance.TrashDictionary[name].Gear));
        }
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "END");
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "Total Gear Count: "+TrashCollectedManager.Instance.GetTotalGearCount());
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "Total Points Made From Trash: " + TrashCollectedManager.Instance.GetTotalScoreFromTrash());
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "Total Points Made From Enemy kill: " + EnemyKilledManager.Instance.TotalPoints);
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "Points Made From Distance Travelled: "+ (Score - TrashCollectedManager.Instance.GetTotalScoreFromTrash() - EnemyKilledManager.Instance.TotalPoints));

        DataManager.Instance.SaveGearForAverage(TrashCollectedManager.Instance.GetTotalGearCount());
    }

    public void ResetGame(float waitTimeToReset = 1.0f)
    {
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "Game Reset");

        UIController.Instance.SetOpenGameMenuPanel(false);
        UIController.Instance.SetOpenPauseMenuPanel(false);
        UIController.Instance.ResetRecapPanel();

        RunPowerUpManager.Instance.ResetManager();

        // Achievements Values One Run
        AchievementManager.Instance.EnemiesKilledOneRun = 0;
        AchievementManager.Instance.TotalDistanceOneRun = 0;

        if (_player != null)
            Destroy(_player);

        StartCoroutine(COWaitForAction(waitTimeToReset, () =>
        {
            terrainTileSpawner.ResetSpawner();
            platformSpawner.ResetSpawner();

            ResetScore();
            ResetTrashCount();
            TrashCollectedManager.Instance.ResetManager();
            EnemyKilledManager.Instance.ResetManager();

            terrainTileSpawner.InitializeTerrainTiles();

            _player = SpawnPlayer();

            // Audio
            AudioController.Instance.PlayBGM(BGMRunClip);
        }));
    }

    public void PauseGame()
    {
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "Game Paused");

        Time.timeScale = 0;

        UIController.Instance.SetOpenPauseMenuPanel(true);
    }
    public void UnpauseGame()
    {
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "Game Unpaused");

        UIController.Instance.SetOpenPauseMenuPanel(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetScore()
    {
        Score = MIN_SCORE;
        UIController.Instance.UpdateScore(Score);
    }

    public void ResetTrashCount()
    {
        TrashCount = MIN_TRASH_COUNT;
        UIController.Instance.UpdateTrashCount(TrashCount);
    }

    private void Start()
    {
        Physics2D.baumgarteScale = 0.02f;
        _player = FindObjectOfType<Player>().gameObject;
        // Achievement values
        AchievementManager.Instance.EnemiesKilledOneRun = 0;
        AchievementManager.Instance.TrashCountDictionary.Clear();
        AchievementManager.Instance.UpdateAchievements();
        // Audio
        AudioController.Instance.PlayBGM(BGMRunClip);
        AudioController.Instance.FadeInBGM(2.0f);
    }

    private GameObject SpawnPlayer()
    {
        GameObject player = Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity);
        return player;
    }

    private IEnumerator COWaitForAction(float delay, Action ActionToPerform)
    {
        yield return new WaitForSeconds(delay);
        ActionToPerform?.Invoke();
    }
}
