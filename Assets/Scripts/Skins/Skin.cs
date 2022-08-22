using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{ 
    public int ID { get => id; }
    public bool IsUnlocked { get; private set; }
    public int Cost { get => cost; }
    public string SkinName { get=> skinName; }
    public Vector3 SpawnPosition { get=>spawnPosition; }

    [Header("Skin Settings")]
    [SerializeField]
    private int id;
    [SerializeField]
    private string skinName;
    [SerializeField]
    private int cost;
    [SerializeField]
    private Vector3 spawnPosition;

    public void UpdateSkin()
    {
        // TO-DO:
        // Check if is already unlocked from file
        // if it is, unlock = true
    }

    public void Unlock()
    {
        if (IsUnlocked)
            return;

        int total = DataManager.Instance.LoadTotalGearCount();
        if (total < cost)
            return;
        total -= cost;
        DataManager.Instance.SaveTotalGearCount(total);

        IsUnlocked = true;
    }
}
