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

    private void Start()
    {
        foreach(Skin skin in skinSet.Skins)
        {
            skin.UpdateSkin(); // Updata data
            GameObject skinEntryObj = Instantiate(skinEntryPrefab, shopContent);
            SkinShopEntry shopEntry = skinEntryObj.GetComponent<SkinShopEntry>();
            shopEntry.SetEntry(skin);
        }
    }
}
