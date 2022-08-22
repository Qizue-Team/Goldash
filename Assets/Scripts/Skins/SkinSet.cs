using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sets/Skin Set", fileName = "Skin Set")]
public class SkinSet : ScriptableObject
{
    public List<Skin> Skins;

    public Skin GetSkinByID(int id)
    {
        foreach(Skin skin in Skins)
        {
            if(skin.ID == id)
                return skin;
        }
        return null;
    }
}
