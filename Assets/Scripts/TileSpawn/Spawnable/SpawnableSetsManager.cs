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

    public GameObject GetRandomObject()
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
                return sets[i].GetRandomObject();
            }
        }

        // Check blank
        prob = blankWeight / total;
        if (prob >= rand)
        {
            return null;
        }

        return sets[count].GetRandomObject();
    }
}
