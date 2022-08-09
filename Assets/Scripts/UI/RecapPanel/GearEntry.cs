using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GearEntry : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI totalGearText;

    private bool _stop = false;
    private int _gearSet = 0;

    public void ResetEntry()
    {
        StopAllCoroutines();
        _stop = false;
        _gearSet = 0;
        totalGearText.text = _gearSet.ToString("000000");
    }

    public void SetEntry(int totalGear)
    {
        _gearSet = totalGear;
        if (_stop)
        {
            Skip();
        }
        else
        {
            StartCoroutine(COUpdateTextAnimation(totalGear, 0.05f));
        }
    }

    private IEnumerator COUpdateTextAnimation(int value, float updateDelay)
    {
        int currentValue = 0;

        while (currentValue <= value && !_stop)
        {
            totalGearText.text = currentValue.ToString("000000");
            yield return new WaitForSeconds(updateDelay);
            currentValue++;
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
        totalGearText.text = _gearSet.ToString("000000");
    }
}
