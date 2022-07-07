using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTileSpawner : Spawner
{
    [SerializeField]
    private float tileSpeed = 1.0f;
    [SerializeField]
    private float tileLifeTime = 10.0f;
    [SerializeField]
    private TileSet tileSet;

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
        _spawnTimer = spawnRate;
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;
        if(_spawnTimer >= spawnRate)
        {
            Spawn();
            _spawnTimer = 0.0f;
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
}
