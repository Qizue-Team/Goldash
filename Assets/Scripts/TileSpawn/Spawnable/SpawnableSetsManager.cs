using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewSpawnableSetsManager", menuName = "Sets/New Spawnable Sets Manager")]
public class SpawnableSetsManager : ScriptableObject
{
    [SerializeField]
    private int blankWeight;

    [SerializeField]
    private SpawnableSet[] sets;

    public GameObject GetRandomObject(bool isPlatform = false, bool isRightEdge = false, float totalDistance = 0.0f)
    {
        // If no set, return blank
        if (sets.Length == 0)
            return null;

        float total = 0.0f;
        for (int i = 0; i < sets.Length; i++)
        {
            total += sets[i].Weight;
        }
        total += blankWeight;

        float rand = Random.value;
        float prob = 0.0f;

        int count = sets.Length - 1;
        for (int i = 0; i < count; i++)
        {
            prob += sets[i].Weight / total;
            if (prob >= rand)
            {
                if (isPlatform && sets[i].SetType == SetType.Trash)
                {
                    return sets[i].GetRareObjcet();
                }
                if (sets[i].SetType == SetType.Enemy && !isRightEdge)
                {
                    return sets[i].GetRandomObjectByDistance(totalDistance);
                }
                if (sets[i].SetType == SetType.Enemy && isRightEdge)
                {
                    return sets[i].GetRandomObjectWithoutIndex(2,totalDistance);
                }
                if (!isPlatform && sets[i].SetType == SetType.Trash)
                {
                    return sets[i].GetLessRareObjcet();
                }
                return sets[i].GetRandomObject();
            }
        }

        // Check blank
        prob = blankWeight / total;
        if (prob >= rand)
        {
            return null;
        }

        if (isPlatform && sets[count].SetType == SetType.Trash)
        {
            return sets[count].GetRareObjcet();
        }
        if (!isPlatform && sets[count].SetType == SetType.Trash)
        {
            return sets[count].GetLessRareObjcet();
        }
        if (sets[count].SetType == SetType.Enemy && !isRightEdge)
        {
            return sets[count].GetRandomObjectByDistance(totalDistance);
        }
        if (sets[count].SetType == SetType.Enemy && isRightEdge)
        {
            return sets[count].GetRandomObjectWithoutIndex(2,totalDistance);
        }

        return sets[count].GetRandomObject();
    }

    public GameObject GetRandomObjectNoEnemy(bool isPlatform = false)
    {
        List<SpawnableSet> newSets = new List<SpawnableSet>();
        foreach(SpawnableSet set in sets)
        {
            if(set.SetType != SetType.Enemy)
            {
                newSets.Add(set);
            }
        }

        // If no set, return blank
        if (newSets.Count == 0)
            return null;

        float total = 0.0f;
        for (int i = 0; i < newSets.Count; i++)
        {
            total += newSets[i].Weight;
        }
        total += blankWeight;

        float rand = Random.value;
        float prob = 0.0f;

        int count = newSets.Count - 1;
        for (int i = 0; i < count; i++)
        {
            prob += newSets[i].Weight / total;
            if (prob >= rand)
            {
                if (isPlatform && newSets[i].SetType == SetType.Trash)
                {
                    return newSets[i].GetRareObjcet();
                }
                if (!isPlatform && newSets[i].SetType == SetType.Trash)
                {
                    return newSets[i].GetLessRareObjcet();
                }
                return newSets[i].GetRandomObject();
            }
        }

        // Check blank
        prob = blankWeight / total;
        if (prob >= rand)
        {
            return null;
        }

        if (isPlatform && newSets[count].SetType == SetType.Trash)
        {
            return newSets[count].GetRareObjcet();
        }
        if (!isPlatform && newSets[count].SetType == SetType.Trash)
        {
            return newSets[count].GetLessRareObjcet();
        }

        return newSets[count].GetRandomObject();
    }

    public GameObject GetRandomEnemy(float distance)
    {
        foreach(SpawnableSet set in sets)
        {
            if(set.SetType == SetType.Enemy)
                return set.GetRandomObjectByDistance(distance);
        }
        return null;
    }

    

}

public enum SetType
{
    Trash,
    Enemy,
    PowerUp
}
