using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpShopEntry : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Image imageIcon;
    [SerializeField]
    private Slider upgradeLevelSlider;
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    [SerializeField]
    private TextMeshProUGUI statText;
    [SerializeField]
    private TextMeshProUGUI costText;
    [SerializeField]
    private Button upgradeButton;

    private int _cost;

    public void SetEntry(PowerUpData data)
    {
        imageIcon.sprite = data.Icon;
        
        if((int)data.CurrentLevel != 0)
            upgradeLevelSlider.value = 1 / (int)data.CurrentLevel;
        
        descriptionText.text = data.Description;

        statText.text = "Next Stat: " + data.NextStat + " " + data.StatLabel;

        costText.text = "Gear Cost: "+data.GearCost;
        _cost = data.GearCost;

        upgradeButton.onClick.AddListener(()=>CallUpgrade(data.ID));
    }

    private void CallUpgrade(int ID)
    {
        int currentGears = DataManager.Instance.LoadTotalGearCount();

        if (currentGears < _cost)
            return;

        currentGears -= _cost;
        DataManager.Instance.SaveTotalGearCount(currentGears);
        FindObjectOfType<GearText>().UpdateGearText(currentGears);

        PowerUpData newData = UpgradeManager.Instance.Upgrade(ID);

        if (newData == null)
            return;

        SetEntry(newData);
    }
}
