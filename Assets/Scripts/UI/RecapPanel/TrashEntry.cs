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

    public void SetEntry(string name, int quantity, int score, int gear)
    {
        trashNameText.text = name;
        trashQuantityText.text = "(x" + quantity.ToString("000") + ")";
        trashScoreText.text = score.ToString("00000");
        trashGearText.text = gear.ToString("00000") + "G";
    }
}
