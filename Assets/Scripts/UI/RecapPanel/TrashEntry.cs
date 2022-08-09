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

    private bool _stop = false;
    private int _quantitySet = 0;
    private int _scoreSet = 0;
    private int _gearSet = 0;

    public IEnumerator COSetEntry(string name, int quantity, int score, int gear)
    {
        trashNameText.text = name;
        _quantitySet = quantity;
        _scoreSet = score;
        _gearSet = gear;
        if (_stop)
        {
            Skip();
            yield return null;
        }
        yield return COUpdateTextAnimation(quantity, score, gear, 0.02f);   
    }

    private IEnumerator COUpdateTextAnimation(int quantity, int trashScore, int gearValue, float updateDelay)
    { 
        int currentValueTrash = 0;
        int currentValueGear = 0;
        int currentQuantity = 0;

        while(currentQuantity <= quantity && !_stop)
        {
            trashQuantityText.text = "(x" + currentQuantity.ToString("000") + ")";
            yield return new WaitForSeconds(updateDelay);
            currentQuantity++;
        }

        while (currentValueTrash <= trashScore && !_stop)
        {
            trashScoreText.text = currentValueTrash.ToString("00000");
            yield return new WaitForSeconds(updateDelay);
            currentValueTrash++;
        }

        while (currentValueGear <= gearValue && !_stop)
        {
            trashGearText.text = currentValueGear.ToString("00000");
            yield return new WaitForSeconds(updateDelay);
            currentValueGear++;
        }
    }

    private void OnEnable()
    {
        RecapPanel.OnSkipClicked += Skip;
    }

    private void OnDisable()
    {
        RecapPanel.OnSkipClicked -= Skip;
    }
    private void Skip()
    {
        _stop = true;
        trashQuantityText.text = "(x" + _quantitySet.ToString("000") + ")";
        trashScoreText.text = _scoreSet.ToString("00000");
        trashGearText.text = _gearSet.ToString("00000");
    }
}
