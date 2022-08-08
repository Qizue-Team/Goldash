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

    public void ResetEntry()
    {
        StopAllCoroutines();
        int total = 0;
        int trash = 0;
        int enemy = 0;
        int distance = 0;
        totalScoreText.text = total.ToString("000000");
        trashScoreText.text = trash.ToString("000000");
        enemyScoreText.text = enemy.ToString("000000");
        distanceScoreText.text = distance.ToString("000000");
    }

    public IEnumerator COSetEntry(int total, int trash, int enemy, int distance)
    {
        yield return COUpdateTextAnimation(total, trash, enemy, distance, 0.02f);
    }

    private IEnumerator COUpdateTextAnimation(int totalValue, int trashValue, int enemyValue, int distanceValue, float updateDelay)
    {
        int currentValueTotal = 0;
        int currentValueTrash = 0;
        int currentValueEnemy = 0;
        int currentValueDistance = 0;

        while (currentValueTotal <= totalValue)
        {
            totalScoreText.text = currentValueTotal.ToString("000000");
            yield return new WaitForSeconds(updateDelay);
            currentValueTotal++;
        }

        while (currentValueTrash <= trashValue)
        {
            trashScoreText.text = currentValueTrash.ToString("000000");
            yield return new WaitForSeconds(updateDelay);
            currentValueTrash++;
        }

        while (currentValueEnemy <= enemyValue)
        {
            enemyScoreText.text = currentValueEnemy.ToString("000000");
            yield return new WaitForSeconds(updateDelay);
            currentValueEnemy++;
        }

        while (currentValueDistance <= distanceValue)
        {
            distanceScoreText.text = currentValueDistance.ToString("000000");
            yield return new WaitForSeconds(updateDelay);
            currentValueDistance++;
        }
    }
}
