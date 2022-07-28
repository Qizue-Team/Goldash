using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="NewSpawnableSet", menuName = "Sets/New Spawnable Set")]
public class SpawnableSet : ScriptableObject
{
    public int Weight { get => weight; }
    public SetType SetType { get => setType; }

    [SerializeField]
    private int weight;
    [SerializeField]
    private Spawnable[] objects;
    [SerializeField]
    private SetType setType;

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

    public GameObject GetRareObjcet()
    {
        List<Spawnable> rareObjs = new List<Spawnable>();

        // Find the average score point
        float avg = 0;
        float sum = 0;
        foreach(Spawnable obj in objects)
        {
            sum += obj.ScorePoints;
        }
        avg = sum / objects.Length;

        // Find the elements > avg
        foreach(Spawnable obj in objects)
        {
            if(obj.ScorePoints > avg)
            {
                rareObjs.Add(obj);
            }
        }

        // Get the obj based on weight
        if (rareObjs.Count == 0)
            return null;

        float total = 0.0f;
        for (int i = 0; i < rareObjs.Count; i++)
        {
            total += rareObjs[i].Weight;
        }

        float rand = Random.value;
        float prob = 0.0f;

        int count = rareObjs.Count - 1;
        for (int i = 0; i < count; i++)
        {
            prob += rareObjs[i].Weight / total;
            if (prob >= rand)
            {
                return rareObjs[i].gameObject;
            }
        }
        return rareObjs[count].gameObject;
    }
}
