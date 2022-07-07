using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TerrainTileSpawner : Spawner
{
    public static event Action<float> OnSpeedUp;
    public static event Action<float,float> OnSpeedDown;

    [SerializeField]
    private float tileSpeed = 1.0f;
    [SerializeField]
    private float tileLifeTime = 10.0f;
    [SerializeField]
    private TileSet tileSet;

    private const float MIN_TILE_SPEED = 2.0f;
    private const float MAX_TILE_SPEED = 16.0f;
    private const float MIN_TILE_LIFE_TIME = 3.0f;
    private const float MAX_TILE_LIFE_TIME = 12.5f;
    
    public float GetTileSpeed()
    {
        return tileSpeed;
    }

    private void Start()
    {
        // Initial Tile Spawn
        float y = -4.5f;
        float x = -11.5f;
        while (x < 12.5f)
        {
            Spawn(new Vector3(x, y, 0.0f));
            x += 1.0f;
        }
        _distance = tileWidth;
    }

    private void Update()
    {
        _distance += tileSpeed * Time.deltaTime;
        if(_distance >= tileWidth)
        {
            Spawn();
            _distance = 0.0f;
        }
    }

    public override void Spawn()
    {
        var tileObj = TerrainTilePool.Instance.Get();
        tileObj.transform.position = transform.position;

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

        TerrainTile tile = tileObj.GetComponent<TerrainTile>();
        tile.SetSpeed(tileSpeed);
        tile.SetDestroyTime(tileLifeTime);
        tile.SetSprite(tileSet.GetRandomSprite());

        tileObj.gameObject.SetActive(true);
    }

    public void SpeedUp(float multiplier)
    {
        if (multiplier <= 1)
            return;

        tileSpeed = tileSpeed * multiplier;
        if(tileSpeed > MAX_TILE_SPEED)
        {
            tileSpeed = MAX_TILE_SPEED;
        }

        tileLifeTime = (TERRAIN_WIDTH / tileSpeed) + 1;
        if(tileLifeTime < MIN_TILE_LIFE_TIME)
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
        if(tileSpeed < MIN_TILE_SPEED)
        {
            tileSpeed = MIN_TILE_SPEED;
        }

        tileLifeTime = (TERRAIN_WIDTH / tileSpeed) + 1;
        if (tileLifeTime > MAX_TILE_LIFE_TIME)
        {
            tileLifeTime = MAX_TILE_LIFE_TIME;
        }

        OnSpeedDown(tileSpeed,tileLifeTime);
    }
}
