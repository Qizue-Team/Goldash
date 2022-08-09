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

    private float _updateDelay = 0.01f;

    private int _totalSet = 0;
    private int _trashSet = 0;
    private int _enemySet = 0;
    private int _distanceSet = 0;
    private bool _stop = false;

    public void ResetEntry()
    {
        StopAllCoroutines();
        _totalSet = 0;
        _trashSet = 0;
        _enemySet = 0;
        _distanceSet = 0;
        _stop = false;
        totalScoreText.text = _totalSet.ToString("000000");
        trashScoreText.text = _trashSet.ToString("000000");
        enemyScoreText.text = _enemySet.ToString("000000");
        distanceScoreText.text = _distanceSet.ToString("000000");
    }

    public IEnumerator COSetEntry(int total, int trash, int enemy, int distance)
    {
        _totalSet = total;
        _trashSet = trash;
        _enemySet = enemy;
        _distanceSet = distance;
        if (_stop)
        {
            Skip();
            yield return null;
        }
        else
            yield return COUpdateTextAnimation(total, trash, enemy, distance);
    }

    private IEnumerator COUpdateTextAnimation(int totalValue, int trashValue, int enemyValue, int distanceValue)
    {
        int currentValueTotal = 0;
        int currentValueTrash = 0;
        int currentValueEnemy = 0;
        int currentValueDistance = 0;

        while (currentValueTotal <= totalValue && !_stop)
        {
            totalScoreText.text = currentValueTotal.ToString("000000");
            yield return new WaitForSeconds(_updateDelay);
            currentValueTotal++;
        }

        while (currentValueTrash <= trashValue && !_stop)
        {
            trashScoreText.text = currentValueTrash.ToString("000000");
            yield return new WaitForSeconds(_updateDelay);
            currentValueTrash++;
        }

        while (currentValueEnemy <= enemyValue && !_stop)
        {
            enemyScoreText.text = currentValueEnemy.ToString("000000");
            yield return new WaitForSeconds(_updateDelay);
            currentValueEnemy++;
        }

        while (currentValueDistance <= distanceValue && !_stop)
        {
            distanceScoreText.text = currentValueDistance.ToString("000000");
            yield return new WaitForSeconds(_updateDelay);
            currentValueDistance++;
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
        totalScoreText.text = _totalSet.ToString("000000");
        trashScoreText.text = _trashSet.ToString("000000");
        enemyScoreText.text = _enemySet.ToString("000000");
        distanceScoreText.text = _distanceSet.ToString("000000");
    }
}
