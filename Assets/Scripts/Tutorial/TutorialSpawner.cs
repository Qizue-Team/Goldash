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
}
