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
    private bool _addOnce = false;
    private int _level = 0;

    public void SetEntry(PowerUpData data)
    {
        imageIcon.sprite = data.Icon;

        _level = (int)data.CurrentLevel;

        if ((int)data.CurrentLevel != 0)
            upgradeLevelSlider.value = (0.25f) * data.CurrentLevel;
        
        if(data.CurrentLevel >= 4) // Max level
        {
            upgradeLevelSlider.value = 1;
            descriptionText.text = data.Description;
            statText.text = "Next Stat: MAX LEVEL";
            costText.text = "Gear Cost: MAX LEVEL";
            upgradeButton.onClick.RemoveAllListeners();
            return;
        }

        descriptionText.text = data.Description;

        statText.text = "Next Stat: " + data.NextStat + " " + data.StatLabel;

        costText.text = "Gear Cost: "+data.CurrentGearCost;
        _cost = data.CurrentGearCost;

        if (!_addOnce)
        {
            upgradeButton.onClick.AddListener(() => CallUpgrade(data.ID));
            _addOnce = true;
        }
           
    }

    private void CallUpgrade(int ID)
    {
        if (_level >= 4)
            return;

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
