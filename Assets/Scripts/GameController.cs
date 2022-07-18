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

        UIController.Instance.SetOpenGameMenuPanel(true);
    }

    public void ResetGame()
    {
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "Game Reset");

        UIController.Instance.SetOpenGameMenuPanel(false);

        StartCoroutine(COWaitForAction(1.0f, () =>
        {
            terrainTileSpawner.ResetSpawner();
            platformSpawner.ResetSpawner();

            ResetScore();
            ResetTrashCount();

            terrainTileSpawner.InitializeTerrainTiles();

            SpawnPlayer();
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

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity);
    }

    private IEnumerator COWaitForAction(float delay, Action ActionToPerform)
    {
        yield return new WaitForSeconds(delay);
        ActionToPerform?.Invoke();
    }
}
