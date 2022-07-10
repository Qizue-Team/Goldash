using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private TrashSet trashSet;

    private float _speed = 1.0f;
    private float _destroyTime = 10.0f;
    private float _timer = 0.0f;

    private GameObject _trash;

    public void SpawnTrash()
    {
        GameObject trashObj = trashSet.GetRandomTrash();
        if (trashObj!=null)
        {
            _trash = Instantiate(trashObj, transform);
        }
        else
        {
            _trash = null;
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
    }

    private void OnDisable()
    {
        TerrainTileSpawner.OnSpeedUp -= SetSpeed;
        TerrainTileSpawner.OnSpeedDown -= SpeedChangedToSlower;
    }

    private void Move(Vector3 direction)
    {
        if(gameObject.activeSelf)
            transform.position += Time.deltaTime * direction * _speed;
    }

    private void Timer()
    {
        _timer += Time.deltaTime;
        if (_timer >= _destroyTime)
        {
            // Destroy Trash if any
            DestroyTrash();

            // Destroy / ReturnToPool
            TerrainTilePool.Instance.ReturnToPool(this);

            // Timer reset
            _timer =0.0f;
        }
    }

    private void SpeedChangedToSlower(float speed, float lifeTime)
    {
        SetSpeed(speed);
        SetDestroyTime(lifeTime);
    }

    private void DestroyTrash()
    {
        if(_trash != null)
        {
            Destroy(_trash);
            _trash = null;
        }
    }
}
