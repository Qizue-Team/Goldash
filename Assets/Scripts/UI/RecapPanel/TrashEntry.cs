using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TrashEntry : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI trashNameText;
    [SerializeField]
    private TextMeshProUGUI trashQuantityText;
    [SerializeField]
    private TextMeshProUGUI trashScoreText;
    [SerializeField]
    private TextMeshProUGUI trashGearText;

    public IEnumerator COSetEntry(string name, int quantity, int score, int gear)
    {
        trashNameText.text = name;
        yield return COUpdateTextAnimation(quantity, score, gear, 0.02f);   
    }

    private IEnumerator COUpdateTextAnimation(int quantity, int trashScore, int gearValue, float updateDelay)
    { 
        int currentValueTrash = 0;
        int currentValueGear = 0;
        int currentQuantity = 0;

        while(currentQuantity <= quantity)
        {
            trashQuantityText.text = "(x" + currentQuantity.ToString("000") + ")";
            yield return new WaitForSeconds(updateDelay);
            currentQuantity++;
        }

        while (currentValueTrash <= trashScore)
        {
            trashScoreText.text = currentValueTrash.ToString("00000");
            yield return new WaitForSeconds(updateDelay);
            currentValueTrash++;
        }

        while (currentValueGear <= gearValue)
        {
            trashGearText.text = currentValueGear.ToString("00000");
            yield return new WaitForSeconds(updateDelay);
            currentValueGear++;
        }

    }
}
