using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using xPoke.CustomLog;

public class TerrainTileSpawner : Spawner
{
    public float TileSpeed { get=>tileSpeed;}
    public float TileLifeTime { get => tileLifeTime; }
    public TerrainTile LastTileSpawned { get; private set; }
    public float CurrentDistanceBetweenEnemies { get; set; }
    public float MaxDistanceNoEnemies { get => maxDistanceNoEnemies; }

    public static event Action<float> OnSpeedUp;
    public static event Action<float,float> OnSpeedDown;
    public static event Action OnStop;
    public static event Action OnReset;
    public static event Action OnResume;

    #region CONSTS
    public const float TILE_WIDTH = 0.9f;
    public const float MIN_TILE_SPEED = 2.0f;
    public const float MAX_TILE_SPEED = 16.0f;
    public const float MIN_TILE_LIFE_TIME = 3.0f;
    public const float MAX_TILE_LIFE_TIME = 12.5f;
    public const float LEFT_MOST_X_TERRAIN_VALUE = -11.5f;
    public const float RIGHT_MOST_X_TERRAIN_VALUE = 12.5f;
    public const float TERRAIN_TILES_Y_POS = -4.5f;

    public const float DEFAULT_SPEEDUP_MULTIPLIER = 1.1f;
    public const float DEFAULT_TILE_SPEED = 3.543122f;
    public const float DEFAULT_LIFE_TIME = 7.49145f;
    #endregion

    [SerializeField]
    private float tileSpeed = 1.0f;
    [SerializeField]
    private float tileLifeTime = 10.0f;
    [SerializeField]
    private TileSet tileSet;

    [Header("References")]
    [SerializeField]
    private PlatformSpawner platformSpawner;

    [Header("Holes Spawn Settings")]
    [SerializeField]
    private float minHoleWaitTime = 5.0f;
    [SerializeField]
    private float maxHoleWaitTime = 10.0f;
    [SerializeField]
    private int minHoleLength = 1;
    [SerializeField]
    private int maxHoleLength = 3;

    [Header("Enemies Settings")]
    [SerializeField]
    private int dynamicEnemiesDistance = 4;
    [SerializeField]
    private float maxDistanceNoEnemies = 300.0f;

    [Header("Score Settings")]
    [SerializeField]
    private int scoreAmount = 5;

    [Header("Tutorial Settings")]
    [SerializeField]
    private bool isTutorial;

    [Header("Debug")]
    [SerializeField]
    private bool showDistance;

    private bool _isWaitingForHole = false; // Pick random range wait time if false
    private bool _spawnHole = false; // if I have to spawn holes is true
    private int _holeIndex = 0; // Index for building holes tile <right-edge hole(xlength) left-edge>
    private float _holeWaitingTime = 0.0f;
    private float _holeTimer = 0.0f;
    private int _currentHoleLength = 0;

    private float _totalDistance = 0.0f;

    private int _currentDistanceTileCount = 0;

    private bool _stop = false;

    #region PUBLIC_METHODS_REGION
    public override void Spawn()
    {
        _currentDistanceTileCount++;
        var tileObj = TerrainTilePool.Instance.Get();
        tileObj.transform.position = transform.position;

        tileObj.GetComponent<BoxCollider2D>().enabled = true;

        tileObj.GetComponent<BoxCollider2D>().offset = new Vector2(0.0f, 0.0f);
        tileObj.GetComponent<BoxCollider2D>().size = new Vector2(1.0f, 1.0f);

        TerrainTile tile = tileObj.GetComponent<TerrainTile>();
        tile.SetSpeed(tileSpeed);
        tile.SetDestroyTime(tileLifeTime);
        tile.SetSprite(tileSet.GetRandomSprite());

        tile.SpawnSpawnableObject(false, _totalDistance);

        tileObj.gameObject.SetActive(true);
        LastTileSpawned = tileObj;
    }

    public void Spawn(bool shouldSpawnObject = true)
    {
        _currentDistanceTileCount++;
        var tileObj = TerrainTilePool.Instance.Get();
        tileObj.transform.position = transform.position;

        tileObj.GetComponent<BoxCollider2D>().enabled = true;

        tileObj.GetComponent<BoxCollider2D>().offset = new Vector2(0.0f, 0.0f);
        tileObj.GetComponent<BoxCollider2D>().size = new Vector2(1.0f, 1.0f);

        TerrainTile tile = tileObj.GetComponent<TerrainTile>();
        tile.SetSpeed(tileSpeed);
        tile.SetDestroyTime(tileLifeTime);
        tile.SetSprite(tileSet.GetRandomSprite());

        if (shouldSpawnObject)
        {
            if (_currentDistanceTileCount >= dynamicEnemiesDistance)
            {
                GameObject spawnedObj = null;
                if (CurrentDistanceBetweenEnemies >= maxDistanceNoEnemies && !platformSpawner.IsSpawningPlatform)
                {
                    spawnedObj = tile.SpawnEnemy(_totalDistance);
                    CurrentDistanceBetweenEnemies = 0;
                }
                else
                {
                    spawnedObj = tile.SpawnSpawnableObject(false, _totalDistance);

                    Enemy enemy = null;
                    if (spawnedObj!=null && spawnedObj.TryGetComponent(out enemy))
                        CurrentDistanceBetweenEnemies = 0;
                }
                
                if (IsDynamicEnemy(spawnedObj))
                {
                    _currentDistanceTileCount = 0;
                    CurrentDistanceBetweenEnemies = 0;
                }
            }
            else
            {
                tile.SpawnSpawnableObjectNoEnemy();
            }
        }

        tileObj.gameObject.SetActive(true);
        LastTileSpawned = tileObj;
    }

    public void Spawn(Vector3 position, bool shouldSpawnObject = true)
    {
        _currentDistanceTileCount++;
        var tileObj = TerrainTilePool.Instance.Get();
        tileObj.transform.position = position;

        tileObj.GetComponent<BoxCollider2D>().offset = new Vector2(0.0f, 0.0f);
        tileObj.GetComponent<BoxCollider2D>().size = new Vector2(1.0f, 1.0f);
        tileObj.GetComponent<BoxCollider2D>().enabled = true;

        TerrainTile tile = tileObj.GetComponent<TerrainTile>();
        tile.SetSpeed(tileSpeed);
        tile.SetDestroyTime(tileLifeTime);
        tile.SetSprite(tileSet.GetRandomSprite());

        if (shouldSpawnObject)
        {
            if (_currentDistanceTileCount >= dynamicEnemiesDistance)
            {
                GameObject spawnedObj = null;
                if (CurrentDistanceBetweenEnemies >= maxDistanceNoEnemies && !platformSpawner.IsSpawningPlatform)
                {
                    spawnedObj = tile.SpawnEnemy(_totalDistance);
                    CurrentDistanceBetweenEnemies = 0;
                }
                else
                {
                    spawnedObj = tile.SpawnSpawnableObject(false, _totalDistance);

                    Enemy enemy = null;
                    if (spawnedObj != null && spawnedObj.TryGetComponent(out enemy))
                        CurrentDistanceBetweenEnemies = 0;
                }

                if (IsDynamicEnemy(spawnedObj))
                {
                    _currentDistanceTileCount = 0;
                    CurrentDistanceBetweenEnemies = 0;
                }
            }
            else
            {
                tile.SpawnSpawnableObjectNoEnemy();
            }
        }

        tileObj.gameObject.SetActive(true);
        LastTileSpawned = tileObj;
    }

    public GameObject Spawn(Sprite sprite, bool isColliderActive = true, bool shouldSpawnObject = true)
    {
        _currentDistanceTileCount++;
        var tileObj = TerrainTilePool.Instance.Get();
        TerrainTile tile = tileObj.GetComponent<TerrainTile>();

        tileObj.transform.position = transform.position;

        tileObj.GetComponent<BoxCollider2D>().offset = new Vector2(0.0f, 0.0f);
        tileObj.GetComponent<BoxCollider2D>().size = new Vector2(1.0f, 1.0f);
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
                    if (CurrentDistanceBetweenEnemies >= maxDistanceNoEnemies && !platformSpawner.IsSpawningPlatform)
                    {
                        spawnedObj = tile.SpawnEnemy(_totalDistance);
                        CurrentDistanceBetweenEnemies = 0;
                    }
                    else
                    {
                        spawnedObj = tile.SpawnSpawnableObject(false, _totalDistance);

                        Enemy enemy = null;
                        if (spawnedObj != null && spawnedObj.TryGetComponent(out enemy))
                            CurrentDistanceBetweenEnemies = 0;
                    }

                    if (IsDynamicEnemy(spawnedObj))
                    {
                        _currentDistanceTileCount = 0;
                        CurrentDistanceBetweenEnemies = 0;
                    }
                }
                else
                {
                    tile.SpawnSpawnableObjectNoEnemy();
                }
            }
        }

        tile.SetSpeed(tileSpeed);
        tile.SetDestroyTime(tileLifeTime);
        tile.SetSprite(sprite);

        tileObj.gameObject.SetActive(true);
        LastTileSpawned = tileObj;

        return tileObj.gameObject;
    }

    public void SpeedUp(float multiplier)
    {
        if (multiplier <= 1)
            return;

        tileSpeed = tileSpeed * multiplier;
        if (tileSpeed > MAX_TILE_SPEED)
        {
            tileSpeed = MAX_TILE_SPEED;
        }

        tileLifeTime = (TERRAIN_WIDTH / tileSpeed) + 1;
        if (tileLifeTime < MIN_TILE_LIFE_TIME)
        {
            tileLifeTime = MIN_TILE_LIFE_TIME;
        }

        OnSpeedUp(tileSpeed);
    }

    public void SpeedDown(float multiplier)
    {
        if (multiplier <= 1)
            return;

        tileSpeed = tileSpeed / multiplier;
        if (tileSpeed < MIN_TILE_SPEED)
        {
            tileSpeed = MIN_TILE_SPEED;
        }

        tileLifeTime = (TERRAIN_WIDTH / tileSpeed) + 1;
        if (tileLifeTime > MAX_TILE_LIFE_TIME)
        {
            tileLifeTime = MAX_TILE_LIFE_TIME;
        }

        OnSpeedDown(tileSpeed, tileLifeTime);
    }

    public void Stop()
    {
        _stop = true;
        tileSpeed = 0;
        tileLifeTime = 0;
        OnStop?.Invoke();
    }

    public void Resume()
    {
        OnResume?.Invoke();

        // Reset Param tiles
        tileSpeed = DEFAULT_TILE_SPEED;
        tileLifeTime = DEFAULT_LIFE_TIME;

        _stop = false;
    }

    public void ResetSpawner()
    {
        OnReset?.Invoke();

        // Reset Params
        _isWaitingForHole = false;
        _spawnHole = false;
        _holeIndex = 0; 
        _holeWaitingTime = 0.0f;
        _holeTimer = 0.0f;
        _currentHoleLength = 0;
        _distance = 0.0f;
        _totalDistance = 0.0f;
        CurrentDistanceBetweenEnemies = 0.0f;

        // Reset Param tiles
        tileSpeed = DEFAULT_TILE_SPEED;
        tileLifeTime = DEFAULT_LIFE_TIME;

        _stop = false;
    }

    public void SpawnHole()
    {
        if (_currentHoleLength <= 0)
            return;
        int totalLength = _currentHoleLength + 3;
        if(_holeIndex == 0)
        {
            _currentDistanceTileCount++;
            Spawn(false);
        }
        else if(_holeIndex == 1)
        {
            // Spawn Right Edge
            GameObject tileObj = Spawn(tileSet.GetRightEdge(),true,false);
            tileObj.tag = "RightEdge";
            _currentDistanceTileCount++;

        }
        else if(_holeIndex == totalLength - 1)
        {
            // Spawn Left Edge
            GameObject tileObj = Spawn(tileSet.GetLeftEdge(),true,false);
            tileObj.tag = "LeftEdge";
            _currentDistanceTileCount++;
            // Finished
            _currentHoleLength = 0;
            _spawnHole = false;
            _holeIndex = 0;
            return;
        }
        else
        {
            // Spawn Hole / No sprite witouth collider
            Spawn(null, false);
        }
        _holeIndex++;
    }

    public void ApplyOverheatSlowDown()
    {
        SpeedDown(99);
        for (int i = 0; i < 3; ++i)
            SpeedUp(DEFAULT_SPEEDUP_MULTIPLIER);
    }

    public void ApplyNormalSpeed()
    {
        SpeedDown(99);
        for(int i =0;i<6;++i)
            SpeedUp(DEFAULT_SPEEDUP_MULTIPLIER);
    }

    public void InitializeTerrainTiles()
    {
        float y = TERRAIN_TILES_Y_POS;
        float x = LEFT_MOST_X_TERRAIN_VALUE;
        while (x < RIGHT_MOST_X_TERRAIN_VALUE+2.2f)
        {
            Spawn(new Vector3(x, y, 0.0f), false);
            x += 1.0f;
        }
        _distance = tileWidth;
    }
    #endregion

    private void Start()
    {
        InitializeTerrainTiles();
        CurrentDistanceBetweenEnemies = 0;
    }

    private void Update()
    {
        if (_stop)
            return;

        if(!isTutorial)
            CheckSpawnHoles();

        Debug.Log(CurrentDistanceBetweenEnemies);
        _distance += tileSpeed * Time.deltaTime;
        _totalDistance += tileSpeed * Time.deltaTime;
        CurrentDistanceBetweenEnemies += tileSpeed * Time.deltaTime;

        if(showDistance)
            CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "Total Distance: "+_totalDistance);

        if(_distance >= tileWidth)
        {
            if (!isTutorial)
            {
                GameController.Instance.IncreaseScore(scoreAmount);
                UIController.Instance.UpdateScore(GameController.Instance.Score);
            }
            if (!_spawnHole)
            {
                if(!isTutorial)
                    Spawn();
                else
                {
                    Spawn(transform.position, false);
                }
                    
            }
            else
                SpawnHole();
            _distance = 0.0f;
        }
    }

    
    private void CheckSpawnHoles()
    {
        // I check for holes spawn only if I finished to spawn another hole
        if (_spawnHole)
            return;

        if (minHoleLength <= 0 || maxHoleLength <= 0)
            return;

        // If I don't have a waiting time, pick it
        if (!_isWaitingForHole)
        {
            // Pick a waiting time
            _holeWaitingTime = UnityEngine.Random.Range(minHoleWaitTime, maxHoleWaitTime);
        }

        // Actual waiting
        _holeTimer += Time.deltaTime;
        if(_holeTimer >= _holeWaitingTime)
        {
            _spawnHole = true;
            _holeTimer = 0.0f;
            _isWaitingForHole = false;
            // Here pick the hole length
            _currentHoleLength = UnityEngine.Random.Range(minHoleLength, maxHoleLength+1);
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
