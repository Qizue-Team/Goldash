using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementEntry : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    [SerializeField]
    private TextMeshProUGUI tierText;
    [SerializeField]
    private TextMeshProUGUI completitionText;
    [SerializeField]
    private TextMeshProUGUI rewardText;
    [SerializeField]
    private Slider slider;

    public void SetEntry(Achievement achievement)
    {
        if (achievement == null)
            return;

        descriptionText.text = achievement.Data.Description;
        tierText.text = "Tier " + (achievement.Data.CurrentTier+1);
        completitionText.text = achievement.Data.CurrentValue.ToString() + "/" + achievement.Data.GetCurrentMaxValue().ToString();

        slider.value = (achievement.Data.CurrentValue / achievement.Data.GetCurrentMaxValue());

        if (achievement.Data.IsMaxTier() && achievement.Data.CurrentValue == achievement.Data.GetCurrentMaxValue())
        {
            rewardText.text = "COMPLETED";
            return;
        }
        
        if (achievement.Data.GetReward().SkinReward != null && achievement.Data.GetReward().GearReward <= 0)
            rewardText.text = "" + achievement.Data.GetReward().SkinReward.SkinName+" Skin";
        if (achievement.Data.GetReward().SkinReward == null)
            rewardText.text = "" + achievement.Data.GetReward().GearReward.ToString() + " Gears";
        if(achievement.Data.GetReward().SkinReward != null && achievement.Data.GetReward().GearReward > 0)
            rewardText.text = ""+ achievement.Data.GetReward().SkinReward.SkinName + " Skin and " + achievement.Data.GetReward().GearReward.ToString() + " Gears";
    }
}
