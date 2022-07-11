using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="NewSpawnableSet", menuName = "Sets/New Spawnable Set")]
public class SpawnableSet : ScriptableObject
{
    public int Weight { get => weight; }

    [SerializeField]
    private int weight;
    [SerializeField]
    private Spawnable[] objects;

    public GameObject GetRandomObject()
    {
        // If no object, return blank
        if(objects.Length == 0)
            return null;

        float total = 0.0f;
        for (int i = 0; i < objects.Length; i++)
        {
            total += objects[i].Weight;
        }

        float rand = Random.value;
        float prob = 0.0f;

        int count = objects.Length-1;
        for (int i = 0; i < count; i++)
        {
            prob+=objects[i].Weight / total;
            if(prob >= rand)
            {
                return objects[i].gameObject;
            }
        }
        return objects[count].gameObject;
    }
}
