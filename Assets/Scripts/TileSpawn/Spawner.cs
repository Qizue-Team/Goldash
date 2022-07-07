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
    [SerializeField]
    protected float tileWidth = 0.9f;

    protected float _distance = 0.0f;

    public abstract void Spawn();
}
