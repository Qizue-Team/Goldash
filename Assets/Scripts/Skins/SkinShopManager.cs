using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinShopManager : Singleton<SkinShopManager>
{
    [Header("References")]
    [SerializeField]
    private SkinSet skinSet;
    [SerializeField]
    private GameObject skinEntryPrefab;
    [SerializeField]
    private Transform shopContent;

    public void Unlock()
    {
        List<SerializableSkinsData> list = new List<SerializableSkinsData>();
        foreach(Skin skin in skinSet.Skins)
        {
            SerializableSkinsData data = new SerializableSkinsData();
            data.ID = skin.ID;
            data.IsUnlocked = skin.IsUnlocked;
            data.IsHidden = skin.IsHidden;
            list.Add(data);
        }
        DataManager.Instance.SaveSkinsData(list);
    }

    private void Start()
    {
        foreach(Skin skin in skinSet.Skins)
        {
            skin.UpdateSkin(); // Updata data
            if (skin.IsHidden)
                continue;
            GameObject skinEntryObj = Instantiate(skinEntryPrefab, shopContent);
            SkinShopEntry shopEntry = skinEntryObj.GetComponent<SkinShopEntry>();
            shopEntry.SetEntry(skin);
        }
    }
}
