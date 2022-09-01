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

    public void SetEntry(string description, int tier, float currentValue, float maxValue, Reward reward)
    {
        descriptionText.text = description;
        tierText.text = "Tier " + tier;
        completitionText.text = currentValue.ToString()+"/"+maxValue.ToString();
        if (reward.SkinReward != null && reward.GearReward <= 0)
            rewardText.text = "Reward: " + reward.SkinReward.SkinName+" Skin";
        if (reward.SkinReward == null)
            rewardText.text = "Reward: " + reward.GearReward.ToString() + " Gears";
        if(reward.SkinReward != null && reward.GearReward > 0)
            rewardText.text = "Reward: "+reward.SkinReward.ToString() + " Skin and " + reward.GearReward.ToString() + " Gears";
    }
}
