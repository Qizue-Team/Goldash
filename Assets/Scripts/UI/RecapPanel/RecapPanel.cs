using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecapPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GearEntry gearEntry;
    [SerializeField]
    private ScoreEntry scoreEntry;
    [SerializeField]
    private Transform trashContentList;
    [SerializeField]
    private GameObject trashEntryPrefab;

    public void AddTrashEntry(string name, int quantity, int score, int gear)
    {
        GameObject entryObj = Instantiate(trashEntryPrefab, trashContentList);
        TrashEntry entry = entryObj.GetComponent<TrashEntry>();
        entry.SetEntry(name, quantity, score, gear);
    }

    public void SetGearEntry(int gear)
    {
        gearEntry.SetEntry(gear);
    }

    public void SetScoreEntry(int total, int trash, int enemy, int distance)
    {
        scoreEntry.SetEntry(total, trash, enemy, distance);
    }
}
