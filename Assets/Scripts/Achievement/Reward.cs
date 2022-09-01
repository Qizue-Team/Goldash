using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardTier", menuName = "Reward")]
public class Reward : ScriptableObject
{
    public int GearReward { get => gearReward; }
    public Skin SkinReward { get => skinReward; }

    [Header("Rewards Settings")]
    [SerializeField]
    private int gearReward = 0;
    [SerializeField]
    private Skin skinReward = null;
}
