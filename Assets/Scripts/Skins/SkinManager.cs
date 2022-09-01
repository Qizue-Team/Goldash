using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : Singleton<SkinManager>
{
    [Header("References")]
    [SerializeField]
    private SkinSet skinSet;

    public void AddSkin(Skin skin)
    {
        skinSet.Skins.Add(skin);
        UpdateSkins();
    }

    public void UpdateSkins()
    {
        List<SerializableSkinsData> list = new List<SerializableSkinsData>();
        foreach (Skin skin in skinSet.Skins)
        {
            SerializableSkinsData data = new SerializableSkinsData();
            data.ID = skin.ID;
            data.IsUnlocked = skin.IsUnlocked;
            list.Add(data);
        }
        DataManager.Instance.SaveSkinsData(list);
    }
}
