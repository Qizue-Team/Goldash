using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GearText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gearText;

    public void UpdateGearText(int gearCount)
    {
        gearText.text = "" + gearCount.ToString();
    }

    private void Start()
    {
        int gearCount = DataManager.Instance.LoadTotalGearCount();
        gearText.text = "" + gearCount.ToString();
    }

}
