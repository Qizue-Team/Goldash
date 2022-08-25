using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformSpawner : Spawner
{
    #region CONST
    public const float MAX_SCENE_Y = 3.0f;
    public const float MIN_SCENE_Y = -4.0f;
    public const float LEFT_MOST_X_TERRAIN_VALUE = -11.5f;
    public const float RIGHT_MOST_X_TERRAIN_VALUE = 12.5f;
    public const int MIN_LEVELS_NUMBER = 1;
    public const float MIN_SPACE_BETWEEN_PLATFORMS = 0.6f;
    public const float MIN_OFFSET = 0;
    #endregion

    public bool IsSpawningPlatform { get => _spawnPlatform; }

    [SerializeField]
    private TileSet tileSet;

    [Header("Extra References")]
    [SerializeField]
    private TerrainTileSpawner terrainSpawner;

    [Header("Position")]
    [Header("Platform Spawn Settings")]
    
    [SerializeField]
    private int levelNumbers = 2;
    [SerializeField]
    private float spaceBetweenPlatforms = 1.0f;
    [SerializeField]
    private float offset = 0.0f;
    [Header("Spawn Time")]
    [SerializeField]
    private float minPlatformWaitTime = 5.0f;
    [SerializeField]
    private float maxPlatformWaitTime = 10.0f;
    [Header("Platform Length")]
    [SerializeField]
    private int minPlatformLength = 1;
    [SerializeField]
    private int maxPlatformLength = 3;
    [Header("Enemies Settings")]
    [SerializeField]
    private int dynamicEnemiesDistance = 4;

    private bool _spawnPlatform = false;
    private bool _isWaitingForPlatform = false;
    private float _platformTimer = 0.0f;
    private float _platformWaitingTime;
    private int _currentPlatformLength;
    private int _platformIndex = 0;

    private int _currentPickedLevel = 1;

    private bool _isBuildingStairs = false;
    private int _currentStairLevel = 1;
    private float _totalDistance = 0.0f;

    private int _currentDistanceTileCount = 0;

    private bool _stop = false;

    public override void Spawn()
    {
        SpawnOnLevel(_currentPickedLevel, ()=> { PickALevel(); });
    }

    public void SpawnOnLevel(int level, Action OnCreationFinished)
    {
        if (level <= 0)
            return;

        CreateStairs(level, OnCreationFinished);
    }

    public void Stop()
    {
        _stop = true;
    }

    public void ResetSpawner()
    {
        _spawnPlatform = false;
        _isWaitingForPlatform = false;
        _platformTimer = 0.0f;
        _platformWaitingTime =0.0f;
        _currentPlatformLength = 0;
        _platformIndex = 0;
        _totalDistance = 0.0f;

        _currentPickedLevel = 1;

        _isBuildingStairs = false;
        _currentStairLevel = 1;

        _distance = 0.0f;

        _stop = false;
    }

    private void CreateStairs(int level, Action OnCreationFinished)
    {
        _isBuildingStairs = true;
        float levelHeight = MIN_SCENE_Y + (_currentStairLevel - 1) * spaceBetweenPlatforms + offset;
        CreatePlatform(levelHeight, () =>
        {
            _currentStairLevel++;
            if(_currentStairLevel > level)
            {
                // Finish stairs
                _isBuildingStairs=false;
                _currentStairLevel = 1;
                OnCreationFinished?.Invoke();
            }
        });
    }

    private void Start()
    {
        PickALevel();
    }

    private void Update()
    {
        if (_stop)
            return;

        CheckSpawnPlatform();
        _distance += terrainSpawner.TileSpeed * Time.deltaTime;
        _totalDistance += terrainSpawner.TileSpeed * Time.deltaTime;
        if (_distance >= tileWidth)
        {
            if (_spawnPlatform || _isBuildingStairs)
            {
                SpawnOnLevel(_currentPickedLevel, () => {
                    PickALevel();
                });
                _distance = 0.0f;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Calculate space between min and max
        float area = Mathf.Abs(MAX_SCENE_Y - MIN_SCENE_Y);
        float valueToAdd = spaceBetweenPlatforms;

        Gizmos.color = Color.green;

        float addition = 0;
        for(int i = 0; i<levelNumbers; i++)
        {
            Gizmos.DrawLine(new Vector3(RIGHT_MOST_X_TERRAIN_VALUE, MIN_SCENE_Y + addition + offset, 0.0f),
                new Vector3(LEFT_MOST_X_TERRAIN_VALUE, MIN_SCENE_Y + addition + offset, 0.0f));
            addition += valueToAdd;
        }
    }

    private void PickALevel()
    {
        if (levelNumbers <= 0)
        {
            _currentPickedLevel = 0;
            return;
        }
        _currentPickedLevel = UnityEngine.Random.Range(1,levelNumbers+1);
    }

    private void CreatePlatform(float yHeight, Action OnPlatformComplete)
    {
        if (_currentPlatformLength <= 0)
            return;
        
        int totalLength = _currentPlatformLength + 2;
        if(_platformIndex == 0)
        {
            // Spawn Left Edge
            GameObject tileObj = TileSpawn(new Vector3(RIGHT_MOST_X_TERRAIN_VALUE, yHeight, 0.0f), tileSet.GetLeftEdge(),true,false);
            tileObj.tag = "LeftEdge";
        }
        else if(_platformIndex == totalLength - 1)
        {
            // Spawn Right Edge
            GameObject tileObj = TileSpawn(new Vector3(RIGHT_MOST_X_TERRAIN_VALUE, yHeight, 0.0f), tileSet.GetRightEdge());
            tileObj.tag = "RightEdge";

            // Finish
            _currentPlatformLength = 0;
            _spawnPlatform = false;
            _platformIndex = 0;
            OnPlatformComplete?.Invoke();
            return;
            
        }
        else
        {
            // Middle Spawn
            TileSpawn(new Vector3(RIGHT_MOST_X_TERRAIN_VALUE, yHeight, 0.0f), tileSet.GetRandomSprite());
        }
        _platformIndex++;
    }

    private GameObject TileSpawn(Vector3 position, Sprite sprite, bool isColliderActive = true, bool shouldSpawnObject = true)
    {
        _currentDistanceTileCount++;
        var tileObj = TerrainTilePool.Instance.Get();
        TerrainTile tile = tileObj.GetComponent<TerrainTile>();

        tileObj.transform.position = position;

        tileObj.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.3f);
        tileObj.GetComponent<BoxCollider2D>().size = new Vector2(1, 0.4f);

        if (!isColliderActive)
        {
            tileObj.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            tileObj.GetComponent<BoxCollider2D>().enabled = true;
            if (shouldSpawnObject)
            {
                if (_currentDistanceTileCount >= dynamicEnemiesDistance)
                {
                    GameObject spawnedObj = null;
                    if (terrainSpawner.CurrentDistanceBetweenEnemies >= terrainSpawner.MaxDistanceNoEnemies)
                    {
                        spawnedObj = tile.SpawnEnemy(_totalDistance);
                        terrainSpawner.CurrentDistanceBetweenEnemies = 0;
                    }
                    else
                    {
                        spawnedObj = tile.SpawnSpawnableObject(true, _totalDistance);

                        Enemy enemy = null;
                        if (spawnedObj != null && spawnedObj.TryGetComponent(out enemy))
                            terrainSpawner.CurrentDistanceBetweenEnemies = 0;
                    }

                    if (IsDynamicEnemy(spawnedObj))
                    {
                        _currentDistanceTileCount = 0;
                        terrainSpawner.CurrentDistanceBetweenEnemies = 0;
                    }
                }
                else
                {
                    tile.SpawnSpawnableObjectNoEnemy();
                }
            }
        }
       
        tile.SetSpeed(terrainSpawner.TileSpeed);
        tile.SetDestroyTime(terrainSpawner.TileLifeTime);
        tile.SetSprite(sprite);

        tileObj.gameObject.SetActive(true);

        return tileObj.gameObject;
    }

    private void CheckSpawnPlatform()
    {
        // I check for platform spawn only if I finished to spawn another platform
        if (_spawnPlatform)
            return;

        if (minPlatformLength <= 0 || maxPlatformLength <= 0)
            return;

        // If I don't have a waiting time, pick it
        if (!_isWaitingForPlatform)
        {
            // Pick a waiting time
            _platformWaitingTime = UnityEngine.Random.Range(minPlatformWaitTime, maxPlatformWaitTime);
        }

        // Don't wait if I have to build stairs
        if (_isBuildingStairs)
        {
            _platformWaitingTime = 0;
        }

        // Actual waiting
        _platformTimer += Time.deltaTime;
        if(_platformTimer >= _platformWaitingTime)
        {
            _spawnPlatform = true;
            _platformTimer = 0.0f;
            _isWaitingForPlatform = false;
            // Here pick the platform length
            _currentPlatformLength = UnityEngine.Random.Range(minPlatformLength, maxPlatformLength + 1);
        }
    }
    private bool IsDynamicEnemy(GameObject spawnedObj)
    {
        if (spawnedObj == null)
            return false;

        DynamicFloatingEnemy floatingEnemy = null;
        LeftRightEnemy leftRightEnemy = null;

        if (spawnedObj.TryGetComponent(out floatingEnemy) || spawnedObj.TryGetComponent(out leftRightEnemy))
            return true;

        return false;
    }
}
