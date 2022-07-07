using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public TerrainTileSpawner spawner;
    private void Start()
    {
        StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        int i = 0;
        while (true)
        {
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
            }
        }
       
    }
}
