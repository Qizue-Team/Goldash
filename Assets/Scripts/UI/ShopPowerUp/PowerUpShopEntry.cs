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

    public void SetEntry(int ID, Sprite icon, int level, string description, float nextStat, string label, int cost)
    {
        imageIcon.sprite = icon;
        
        if(level != 0)
            upgradeLevelSlider.value = 1 / level;
        
        descriptionText.text = description;

        statText.text = "Next Stat: " + nextStat + " " + label;

        costText.text = "Gear Cost: "+cost;

        upgradeButton.onClick.AddListener(()=>CallUpgrade(ID));
    }

    private void CallUpgrade(int ID)
    {
        // TO-DO: Remove cost if enough gears ...

        UpgradeManager.Instance.Upgrade(ID);
    }
}
