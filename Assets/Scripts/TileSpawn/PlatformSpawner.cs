using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : Spawner
{
    public const float MAX_SCENE_Y = 3.0f;
    public const float MIN_SCENE_Y = -4.0f;
    public const float LEFT_MOST_X_TERRAIN_VALUE = -11.5f;
    public const float RIGHT_MOST_X_TERRAIN_VALUE = 12.5f;

    [SerializeField]
    private TileSet tileSet;

    [Header("Extra References")]
    [SerializeField]
    private TerrainTileSpawner terrainSpawner;

    [Header("Platform Spawn Settings")]
    [SerializeField]
    private int levelNumbers = 2;
    [SerializeField]
    private float spaceBetweenPlatforms = 1.0f;
    [SerializeField]
    private float offset = 0.0f;
    [SerializeField]
    private float minPlatformWaitTime = 5.0f;
    [SerializeField]
    private float maxPlatformWaitTime = 10.0f;
    [SerializeField]
    private int minPlatformLength = 1;
    [SerializeField]
    private int maxPlatformLength = 3;

    private bool _spawnPlatform = false;
    private bool _isWaitingForPlatform = false;
    private float _platformTimer = 0.0f;
    private float _platformWaitingTime;
    private int _currentPlatformLength;
    private int _platformIndex = 0;

    public override void Spawn()
    {
        SpawnOnLevel(1);
    }

    public void SpawnOnLevel(int level)
    {
        if (level <= 0)
            return;
        if(level == 1)
        {
            float levelHeight = MIN_SCENE_Y + 0 + offset;
            CreatePlatform(levelHeight,true);
        }
    }

    private void Update()
    {
        CheckSpawnPlatform();
        _distance += terrainSpawner.TileSpeed * Time.deltaTime;
        if (_distance >= tileWidth)
        {
            if (_spawnPlatform)
            {
                // Choose random level X if not already picked
                // Spawn platform on a X level, if level X > 1 -> stairs else single platform

                // For now Spawn on level 1 (Aka Spawn)
                Spawn();
                _distance = 0.0f;
            }
            // Else nothing
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

    private void CreatePlatform(float yHeight, bool shouldDeclareFinish = true)
    {
        if (_currentPlatformLength <= 0)
            return;
        int totalLength = _currentPlatformLength + 2;
        if(_platformIndex == 0)
        {
            // Spawn Left Edge
            TileSpawn(new Vector3(RIGHT_MOST_X_TERRAIN_VALUE, yHeight, 0.0f), tileSet.GetLeftEdge());
        }
        else if(_platformIndex == totalLength - 1)
        {
            // Spawn Right Edge
            TileSpawn(new Vector3(RIGHT_MOST_X_TERRAIN_VALUE, yHeight, 0.0f), tileSet.GetRightEdge());
            if (shouldDeclareFinish)
            {
                // Finish
                _currentPlatformLength = 0;
                _spawnPlatform = false;
                _platformIndex = 0;
                return;
            }
        }
        else
        {
            // Middle Spawn
            TileSpawn(new Vector3(RIGHT_MOST_X_TERRAIN_VALUE, yHeight, 0.0f), tileSet.GetRandomSprite());
        }
        _platformIndex++;
    }

    private void TileSpawn(Vector3 position, Sprite sprite, bool isColliderActive = true)
    {
        var tileObj = TerrainTilePool.Instance.Get();
        tileObj.transform.position = position;

        if (!isColliderActive)
        {
            tileObj.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            tileObj.GetComponent<BoxCollider2D>().enabled = true;
        }

        TerrainTile tile = tileObj.GetComponent<TerrainTile>();
        tile.SetSpeed(terrainSpawner.TileSpeed);
        tile.SetDestroyTime(terrainSpawner.TileLifeTime);
        tile.SetSprite(sprite);

        tileObj.gameObject.SetActive(true);
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
}
