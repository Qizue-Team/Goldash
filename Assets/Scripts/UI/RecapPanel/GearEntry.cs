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

    public void ResetEntry()
    {
        StopAllCoroutines();
        int currentValue = 0;
        totalGearText.text = currentValue.ToString("000000");
    }

    public void SetEntry(int totalGear)
    {
        StartCoroutine(COUpdateTextAnimation(totalGear, 0.05f));
    }

    private IEnumerator COUpdateTextAnimation(int value, float updateDelay)
    {
        int currentValue = 0;

        while (currentValue <= value)
        {
            totalGearText.text = currentValue.ToString("000000");
            yield return new WaitForSeconds(updateDelay);
            currentValue++;
        }
    }
}
