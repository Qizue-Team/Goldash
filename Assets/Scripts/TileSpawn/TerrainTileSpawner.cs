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
        // Use terrain tile pool here
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
        // Use terrain tile pool here
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
        if(tileSpeed > 16)
        {
            tileSpeed = 16;
        }
        tileLifeTime = (23.0f / tileSpeed) + 1;
        if(tileLifeTime < 3)
        {
            tileLifeTime = 3;
        }

        OnSpeedUp(tileSpeed);
    }

    public void SpeedDown(float multiplier)
    {
        if (multiplier <= 1)
            return;

     
        tileSpeed = tileSpeed / multiplier;
        if(tileSpeed < 2)
        {
            tileSpeed = 2;
        }
        tileLifeTime = (23.0f / tileSpeed) + 1;
        if (tileLifeTime > 12.5f)
        {
            tileLifeTime = 12.5f;
        }


        OnSpeedDown(tileSpeed,tileLifeTime);
    }
}