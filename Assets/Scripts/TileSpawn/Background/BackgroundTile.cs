using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    private float _normalSpeed = 1.0f;
    private float _slowSpeed = 0.5f;

    private float _speed = 0.0f;
    private bool _stop = false;

    private void Start()
    {
        _speed = _normalSpeed;
    }

    private void Update()
    {
        Move(Vector3.left);
        DestroyBackground();
    
    }

    private void OnEnable()
    {
        TerrainTileSpawner.OnSpeedUp += NormalSpeed;
        TerrainTileSpawner.OnSpeedDown += SlowSped;
        TerrainTileSpawner.OnStop += StopTile;
        TerrainTileSpawner.OnReset += ResetTile;
        TerrainTileSpawner.OnResume += ResumeTile;
    }

    private void OnDisable()
    {
        TerrainTileSpawner.OnSpeedUp -= NormalSpeed;
        TerrainTileSpawner.OnSpeedDown -= SlowSped;
        TerrainTileSpawner.OnStop -= StopTile;
        TerrainTileSpawner.OnReset -= ResetTile;
        TerrainTileSpawner.OnResume -= ResumeTile;
    }

    private void Move(Vector3 direction)
    {
        if (_stop)
            return;

        if (gameObject.activeSelf)
            transform.position += Time.deltaTime * direction * _speed;
    }

    private void DestroyBackground()
    {
        if(this.transform.position.x < -21.0f)
            Destroy(gameObject);
    }

    private void ResetTile()
    {
        Destroy(gameObject);
    }

    private void StopTile()
    {
        _stop = true;
    }

    private void ResumeTile()
    {
        _stop = false;
    }

    private void NormalSpeed(float s)
    {
        _speed = _normalSpeed;
    }

    private void SlowSped(float s, float t)
    {
        _speed = _slowSpeed;
    }
}
