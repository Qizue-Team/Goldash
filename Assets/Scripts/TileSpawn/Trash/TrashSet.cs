using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="NewTrashSet", menuName = "New Trash Set")]
public class TrashSet : ScriptableObject
{
    [SerializeField]
    private float blankWeight = 2;

    [SerializeField]
    private GameObject[] trashObjs;

    public GameObject GetRandomTrash()
    {
        // If no trash, return blank
        if(trashObjs.Length == 0)
            return null;

        float total = 0.0f;
        for (int i = 0; i < trashObjs.Length; i++)
        {
            total += trashObjs[i].GetComponent<Trash>().Weight;
        }
        total += blankWeight;

        float rand = Random.value;
        float prob = 0.0f;

        int count = trashObjs.Length-1;
        for (int i = 0; i < count; i++)
        {
            prob+=trashObjs[i].GetComponent<Trash>().Weight / total;
            if(prob >= rand)
            {
                return trashObjs[i];
            }
        }

        // Check blank
        prob = blankWeight / total;
        if(prob >= rand)
        {
            return null;
        }

        return trashObjs[count];
    }
}
