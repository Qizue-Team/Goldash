using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{ 
    public int ID { get => id; }
    public bool IsUnlocked { get => isUnlocked; }
    public bool IsSet { get; private set; }
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
    private bool isUnlocked;
    [SerializeField]
    private Vector3 spawnPosition;

    public void UpdateSkin()
    { 
        // Check if is already unlocked from file
        List<SerializableSkinsData> datas = DataManager.Instance.LoadSkinsData();
        if (datas == null)
            return;
        foreach(SerializableSkinsData data in datas)
        {
            if(data.ID == id)
            {
                isUnlocked = data.IsUnlocked;
            }
        }

        SerializableSkinSetData setData = DataManager.Instance.LoadSkinSetData();
        if(setData != null && setData.ID == ID)
        {
            IsSet = true;
        }
        else
        {
            IsSet = false;
        }
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

        isUnlocked = true;
    }

    private void Start()
    {
        isUnlocked = false;
        IsSet = false;
    }
}
