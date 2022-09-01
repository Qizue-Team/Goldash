using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public int GearReward { get => gearReward; }
    public Skin SkinReward { get => skinReward; }

    [Header("Rewards Settings")]
    [SerializeField]
    private int gearReward = 0;
    [SerializeField]
    private Skin skinReward = null;
}
