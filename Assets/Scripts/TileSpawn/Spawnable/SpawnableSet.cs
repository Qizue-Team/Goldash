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

    public GameObject GetRandomObjectByDistance(float totalDistance)
    {
        // If no object, return blank
        if (objects.Length == 0)
            return null;

        List<Spawnable> newObjects = new List<Spawnable>();
        foreach(var obj in objects)
        {
            if(totalDistance >= obj.AppearDistance)
                newObjects.Add(obj);
        }

        float total = 0.0f;
        for (int i = 0; i < newObjects.Count; i++)
        {
            total += newObjects[i].Weight;
        }

        float rand = Random.value;
        float prob = 0.0f;

        int count = newObjects.Count - 1;
        for (int i = 0; i < count; i++)
        {
            prob += newObjects[i].Weight / total;
            if (prob >= rand)
            {
                return newObjects[i].gameObject;
            }
        }
        return newObjects[count].gameObject;
    }

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
            if(obj.ScorePoints >= avg)
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

    public GameObject GetLessRareObjcet()
    {
        List<Spawnable> rareObjs = new List<Spawnable>();

        // Find the average score point
        float avg = 0;
        float sum = 0;
        foreach (Spawnable obj in objects)
        {
            sum += obj.ScorePoints;
        }
        avg = sum / objects.Length;
      
        // Find the elements > avg
        foreach (Spawnable obj in objects)
        {
            if (obj.ScorePoints < avg)
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

    public GameObject GetRandomObjectWithoutIndex(int index, float totalDistance)
    {
        // If no object, return blank
        if (objects.Length == 0)
            return null;

        List<Spawnable> newObjects = new List<Spawnable>();
        for(int i=0; i< objects.Length; i++)
        {
            if (index == i)
                continue;
            if (totalDistance >= objects[i].AppearDistance)
                newObjects.Add(objects[i]);
        }

        float total = 0.0f;
        for (int i = 0; i < newObjects.Count; i++)
        {
            total += newObjects[i].Weight;
        }

        float rand = Random.value;
        float prob = 0.0f;

        int count = newObjects.Count - 1;
        for (int i = 0; i < count; i++)
        {
            prob += newObjects[i].Weight / total;
            if (prob >= rand)
            {
                return newObjects[i].gameObject;
            }
        }
        return newObjects[count].gameObject;
    }
}
