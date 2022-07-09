using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TerrainTileSpawner : Spawner
{
    public float TileSpeed { get=>tileSpeed;}
    public float TileLifeTime { get => tileLifeTime; }

    public static event Action<float> OnSpeedUp;
    public static event Action<float,float> OnSpeedDown;

    #region CONSTS
    public const float TILE_WIDTH = 0.9f;
    public const float MIN_TILE_SPEED = 2.0f;
    public const float MAX_TILE_SPEED = 16.0f;
    public const float MIN_TILE_LIFE_TIME = 3.0f;
    public const float MAX_TILE_LIFE_TIME = 12.5f;
    public const float LEFT_MOST_X_TERRAIN_VALUE = -11.5f;
    public const float RIGHT_MOST_X_TERRAIN_VALUE = 12.5f;
    public const float TERRAIN_TILES_Y_POS = -4.5f;
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

    private bool _isWaitingForHole = false; // Pick random range wait time if false
    private bool _spawnHole = false; // if I have to spawn holes is true
    private int _holeIndex = 0; // Index for building holes tile <right-edge hole(xlength) left-edge>
    private float _holeWaitingTime = 0.0f;
    private float _holeTimer = 0.0f;
    private int _currentHoleLength = 0;

    #region PUBLIC_METHODS_REGION
    public override void Spawn()
    {
        var tileObj = TerrainTilePool.Instance.Get();
        tileObj.transform.position = transform.position;

        tileObj.GetComponent<BoxCollider2D>().enabled = true;

        TerrainTile tile = tileObj.GetComponent<TerrainTile>();
        tile.SetSpeed(tileSpeed);
        tile.SetDestroyTime(tileLifeTime);
        tile.SetSprite(tileSet.GetRandomSprite());

        tileObj.gameObject.SetActive(true);
    }

    public void Spawn(Vector3 position)
    {
        var tileObj = TerrainTilePool.Instance.Get();
        tileObj.transform.position = position;

        tileObj.GetComponent<BoxCollider2D>().enabled = true;

        TerrainTile tile = tileObj.GetComponent<TerrainTile>();
        tile.SetSpeed(tileSpeed);
        tile.SetDestroyTime(tileLifeTime);
        tile.SetSprite(tileSet.GetRandomSprite());

        tileObj.gameObject.SetActive(true);
    }

    public void Spawn(Sprite sprite, bool isColliderActive = true)
    {
        var tileObj = TerrainTilePool.Instance.Get();
        tileObj.transform.position = transform.position;

        if (!isColliderActive)
        {
            tileObj.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            tileObj.GetComponent<BoxCollider2D>().enabled = true;
        }

        TerrainTile tile = tileObj.GetComponent<TerrainTile>();
        tile.SetSpeed(tileSpeed);
        tile.SetDestroyTime(tileLifeTime);
        tile.SetSprite(sprite);

        tileObj.gameObject.SetActive(true);
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

    public void SpawnHole()
    {
        if (_currentHoleLength <= 0)
            return;
        int totalLength = _currentHoleLength + 2;
        if(_holeIndex == 0)
        {
            // Spawn Right Edge
            Spawn(tileSet.GetRightEdge());
        }
        else if(_holeIndex == totalLength - 1)
        {
            // Spawn Left Edge
            Spawn(tileSet.GetLeftEdge());
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
    #endregion

    private void Start()
    {
        InitializeTerrainTiles();
    }

    private void Update()
    {
        CheckSpawnHoles();

        _distance += tileSpeed * Time.deltaTime;
        if(_distance >= tileWidth)
        {
            if (!_spawnHole)
                Spawn();
            else
                SpawnHole();
            _distance = 0.0f;
        }
    }

    private void InitializeTerrainTiles()
    {
        float y = TERRAIN_TILES_Y_POS;
        float x = LEFT_MOST_X_TERRAIN_VALUE;
        while (x < RIGHT_MOST_X_TERRAIN_VALUE)
        {
            Spawn(new Vector3(x, y, 0.0f));
            x += 1.0f;
        }
        _distance = tileWidth;
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
}
