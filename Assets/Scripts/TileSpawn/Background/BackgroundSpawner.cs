using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] backgroundObjs;
    [SerializeField]
    private Vector3 spawnPosition;

    private BackgroundTile _currentBackground;

    public void Spawn(Vector3 pos)
    {
        GameObject obj = Instantiate(backgroundObjs[Random.Range(0,backgroundObjs.Length)], pos, Quaternion.identity);
        _currentBackground = obj.GetComponent<BackgroundTile>();
    }

    private void Start()
    {
        Spawn(Vector3.zero);
    }

    private void Update()
    {
        if(_currentBackground!= null && _currentBackground.transform.position.x < 0)
        {
            _currentBackground = null;
            Spawn(spawnPosition);
        }
    }
}
