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

    public IEnumerator COAddTrashEntry(string name, int quantity, int score, int gear)
    {
        GameObject entryObj = Instantiate(trashEntryPrefab, trashContentList);
        TrashEntry entry = entryObj.GetComponent<TrashEntry>();
        yield return entry.COSetEntry(name, quantity, score, gear);
    }

    public void SetGearEntry(int gear)
    {
        gearEntry.SetEntry(gear);
    }

    public IEnumerator COSetScoreEntry(int total, int trash, int enemy, int distance)
    {
        yield return scoreEntry.COSetEntry(total, trash, enemy, distance);
    }
}
