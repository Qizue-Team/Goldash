using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
    [Header("Referemces")]
    [SerializeField]
    private TerrainTileSpawner tileSpawner;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject trashPrefab;
    [SerializeField]
    private GameObject powerupPrefab;

    public GameObject SpawnTrash()
    {
        GameObject trashObj = Instantiate(trashPrefab, tileSpawner.LastTileSpawned.transform);
        return trashObj;
    }

    public GameObject SpawnEnemy()
    {
        GameObject enemyObj = Instantiate(enemyPrefab, tileSpawner.LastTileSpawned.transform);
        return enemyObj;
    }

    public GameObject SpawnPowerup()
    {
        GameObject powerObj = Instantiate(powerupPrefab, tileSpawner.LastTileSpawned.transform);
        return powerObj;
    }
}
