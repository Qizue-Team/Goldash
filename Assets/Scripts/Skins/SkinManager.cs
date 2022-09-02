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
        foreach(Skin currentSkin in skinSet.Skins)
        {
            if(skin.ID == currentSkin.ID)
            {
                skin.ShowSkin();
                break;
            }
        }
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
            data.IsHidden = skin.IsHidden;
            list.Add(data);
        }
        DataManager.Instance.SaveSkinsData(list);
    }
}
