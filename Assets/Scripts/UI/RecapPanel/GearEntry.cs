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

    public void SetEntry(int totalGear)
    {
        totalGearText.text = totalGear.ToString("000000");
    }
}
