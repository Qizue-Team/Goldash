using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreEntry : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI totalScoreText;
    [SerializeField]
    private TextMeshProUGUI trashScoreText;
    [SerializeField]
    private TextMeshProUGUI enemyScoreText;
    [SerializeField]
    private TextMeshProUGUI distanceScoreText;

    public void SetEntry(int total, int trash, int enemy, int distance)
    {
        totalScoreText.text = total.ToString("000000");
        trashScoreText.text = trash.ToString("000000");
        enemyScoreText.text = enemy.ToString("000000");
        distanceScoreText.text = distance.ToString("000000");
    }
}
