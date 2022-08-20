using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TerrainTileSpawner : Spawner
{
    public float TileSpeed { get=>tileSpeed;}
    public float TileLifeTime { get => tileLifeTime; }
    public TerrainTile LastTileSpawned { get; private set; }

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

    [Header("Holes Spawn Settings")]
    [SerializeField]
    private float minHoleWaitTime = 5.0f;
    [SerializeField]
    private float maxHoleWaitTime = 10.0f;
    [SerializeField]
    private int minHoleLength = 1;
    [SerializeField]
    private int maxHoleLength = 3;

    [Header("Score Settings")]
    [SerializeField]
    private int scoreAmount = 5;

    [Header("Tutorial Settings")]
    [SerializeField]
    private bool isTutorial;

    private bool _isWaitingForHole = false; // Pick random range wait time if false
    private bool _spawnHole = false; // if I have to spawn holes is true
    private int _holeIndex = 0; // Index for building holes tile <right-edge hole(xlength) left-edge>
    private float _holeWaitingTime = 0.0f;
    private float _holeTimer = 0.0f;
    private int _currentHoleLength = 0;

    private bool _stop = false;

    #region PUBLIC_METHODS_REGION
    public override void Spawn()
    {
        var tileObj = TerrainTilePool.Instance.Get();
        tileObj.transform.position = transform.position;

        tileObj.GetComponent<BoxCollider2D>().enabled = true;

        tileObj.GetComponent<BoxCollider2D>().offset = new Vector2(0.0f, 0.0f);
        tileObj.GetComponent<BoxCollider2D>().size = new Vector2(1.0f, 1.0f);

        TerrainTile tile = tileObj.GetComponent<TerrainTile>();
        tile.SetSpeed(tileSpeed);
        tile.SetDestroyTime(tileLifeTime);
        tile.SetSprite(tileSet.GetRandomSprite());

        tile.SpawnSpawnableObject();

        tileObj.gameObject.SetActive(true);
        LastTileSpawned = tileObj;
    }

    public void Spawn(bool shouldSpawnObject = true)
    {
        var tileObj = TerrainTilePool.Instance.Get();
        tileObj.transform.position = transform.position;

        tileObj.GetComponent<BoxCollider2D>().enabled = true;

        tileObj.GetComponent<BoxCollider2D>().offset = new Vector2(0.0f, 0.0f);
        tileObj.GetComponent<BoxCollider2D>().size = new Vector2(1.0f, 1.0f);

        TerrainTile tile = tileObj.GetComponent<TerrainTile>();
        tile.SetSpeed(tileSpeed);
        tile.SetDestroyTime(tileLifeTime);
        tile.SetSprite(tileSet.GetRandomSprite());

        if(shouldSpawnObject)
            tile.SpawnSpawnableObject();

        tileObj.gameObject.SetActive(true);
        LastTileSpawned = tileObj;
    }

    public void Spawn(Vector3 position, bool shoudlSpawnObject = true)
    {
        var tileObj = TerrainTilePool.Instance.Get();
        tileObj.transform.position = position;

        tileObj.GetComponent<BoxCollider2D>().offset = new Vector2(0.0f, 0.0f);
        tileObj.GetComponent<BoxCollider2D>().size = new Vector2(1.0f, 1.0f);
        tileObj.GetComponent<BoxCollider2D>().enabled = true;

        TerrainTile tile = tileObj.GetComponent<TerrainTile>();
        tile.SetSpeed(tileSpeed);
        tile.SetDestroyTime(tileLifeTime);
        tile.SetSprite(tileSet.GetRandomSprite());

        if(shoudlSpawnObject)
            tile.SpawnSpawnableObject();

        tileObj.gameObject.SetActive(true);
        LastTileSpawned = tileObj;
    }

    public GameObject Spawn(Sprite sprite, bool isColliderActive = true, bool shouldSpawnObject = true)
    {
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
                tile.SpawnSpawnableObject();
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
            Spawn(false);
        }
        else if(_holeIndex == 1)
        {
            // Spawn Right Edge
            GameObject tileObj = Spawn(tileSet.GetRightEdge(),true,false);
            tileObj.tag = "RightEdge";
        }
        else if(_holeIndex == totalLength - 1)
        {
            // Spawn Left Edge
            GameObject tileObj = Spawn(tileSet.GetLeftEdge(),true,false);
            tileObj.tag = "LeftEdge";
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
    }

    private void Update()
    {
        if (_stop)
            return;

        if(!isTutorial)
            CheckSpawnHoles();

        _distance += tileSpeed * Time.deltaTime;
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
                    Spawn(transform.position,false);
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
            return;

        DynamicFloatingEnemy floatingEnemy = null;
        LeftRightEnemy leftRightEnemy = null;

        if (spawnedObj.TryGetComponent(out floatingEnemy) || spawnedObj.TryGetComponent(out leftRightEnemy))
            return true;

        return false;
    }
}
