using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTrashSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform leftBorder;
    [SerializeField]
    private Transform rightBorder;
    [SerializeField]
    private SpawnableSet trashSet;

    [Header("Spawn Settings")]
    [SerializeField]
    private int maxTrashCount = 20;
    [SerializeField]
    private float trashSpawnRate = 2.0f;

    private float _trashTimer = 0.0f;
    private int _trashCount = 0;

    private void Update()
    {
        _trashTimer += Time.deltaTime;
        if(_trashTimer > trashSpawnRate)
        {
            Spawn();
            _trashTimer = 0.0f;
        }
    }

    private void Spawn()
    {
        if (_trashCount > maxTrashCount)
            return;

        _trashCount++;

        float randomX =  Random.Range(leftBorder.position.x, rightBorder.position.x);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0.0f);

        GameObject spawnedObj = Instantiate(trashSet.GetRandomObject(), spawnPosition, Quaternion.identity);

        spawnedObj.GetComponent<BoxCollider2D>().isTrigger = false;
        spawnedObj.AddComponent<Rigidbody2D>();
    }
}
