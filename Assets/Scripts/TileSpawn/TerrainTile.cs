using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private SpawnableSetsManager setsManager;

    private float _speed = 1.0f;
    private float _destroyTime = 10.0f;
    private float _timer = 0.0f;
    private bool _stop = false;

    private GameObject _spawnedObject;

    public GameObject SpawnSpawnableObject(bool isPlatform = false, float totalDistance = 0.0f)
    {
        GameObject spawnableObj = setsManager.GetRandomObject(isPlatform,totalDistance);
        if (spawnableObj!=null)
        {
            _spawnedObject = Instantiate(spawnableObj, transform);
        }
        else
        {
            _spawnedObject = null;
        }
        return _spawnedObject;
    }

    public GameObject SpawnSpawnableObjectNoEnemy(bool isPlatform = false)
    {
        GameObject spawnableObj = setsManager.GetRandomObjectNoEnemy(isPlatform);
        if (spawnableObj != null)
        {
            _spawnedObject = Instantiate(spawnableObj, transform);
        }
        else
        {
            _spawnedObject = null;
        }
        return _spawnedObject;
    }

    public void DestroySpawnedObject()
    {
        if (_spawnedObject != null)
        {
            Destroy(_spawnedObject);
            _spawnedObject = null;
        }
    }

    public void SetSpeed(float speed)
    {
        if(speed < 0)
            speed = 0;
        _speed = speed;
    }

    public void SetDestroyTime(float time)
    {
        if(time < 0)
            time = 0;
        _destroyTime = time;
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    private void Update()
    {
        Move(Vector3.left);
        Timer();
    }

    private void OnEnable()
    {
        TerrainTileSpawner.OnSpeedUp += SetSpeed;
        TerrainTileSpawner.OnSpeedDown += SpeedChangedToSlower;
        TerrainTileSpawner.OnStop += StopTile;
        TerrainTileSpawner.OnReset += ResetTile;
        TerrainTileSpawner.OnResume += ResumeTile;
    }

    private void OnDisable()
    {
        TerrainTileSpawner.OnSpeedUp -= SetSpeed;
        TerrainTileSpawner.OnSpeedDown -= SpeedChangedToSlower;
        TerrainTileSpawner.OnStop -= StopTile;
        TerrainTileSpawner.OnReset -= ResetTile;
        TerrainTileSpawner.OnResume -= ResumeTile;
    }

    private void Move(Vector3 direction)
    {
        if (_stop)
            return;

        if(gameObject.activeSelf)
            transform.position += Time.deltaTime * direction * _speed;
    }

    private void Timer()
    {
        if (_stop)
            return;

        _timer += Time.deltaTime;
        if (_timer >= _destroyTime)
        {
            ResetTile();
        }
    }

    private void SpeedChangedToSlower(float speed, float lifeTime)
    {
        SetSpeed(speed);
        SetDestroyTime(lifeTime);
    }

    private void StopTile()
    {
        _stop = true;
    }

    private void ResumeTile()
    {
        _stop = false;
    }

    private void ResetTile()
    {
        // Destroy Trash if any
        DestroySpawnedObject();

        this.gameObject.tag = "Untagged";

        // Destroy / ReturnToPool
        TerrainTilePool.Instance.ReturnToPool(this);

        // Timer reset
        _timer = 0.0f;

        // Able to move again
        if (_stop)
            _stop = false;
    }
}
