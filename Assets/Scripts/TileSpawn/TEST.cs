using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public TerrainTileSpawner spawner;
    float distance = 0;
    private void Start()
    {
      // StartCoroutine(Loop());
    }

    private void Update()
    {
        distance += spawner.GetTileSpeed() * Time.deltaTime;
        Debug.Log("Distance = " + distance);
        if (distance >=0.9f)
        {
            spawner.Spawn();
            distance = 0;
        }
    }

    private IEnumerator Loop()
    {
        int i = 0;
        while (true)
        {/*
            while (i != 4)
            {
                yield return new WaitForSeconds(1.0f);
                spawner.SpeedUp(2f);
                i++;
            }
            while (i !=0)
            {
                yield return new WaitForSeconds(1.0f);
                spawner.SpeedDown(2f);
                i--;
            }*/
        }
       
    }
}
