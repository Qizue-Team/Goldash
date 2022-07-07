using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [Header("Object to spawn")]
    // Prefab to spawn
    [SerializeField]
    protected GameObject prefab; 

    [Header("Spawn Settings")]
    // Spawn every <spawnRate> secs
    [SerializeField]
    protected float spawnRate = 1.0f;

    protected float _spawnTimer = 0.0f;

    public abstract void Spawn();
}
