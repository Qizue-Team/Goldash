using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="NewTileSet", menuName = "Tileset/Create New TileSet")]
public class TileSet : ScriptableObject
{
    [SerializeField]
    private Sprite[] sprites;

    public Sprite GetRandomSprite()
    {
        if(sprites.Length == 0)
        {
            Debug.LogError("[TileSet GetRandomSprite] Can not retrieve random sprite - sprites in the set are 0");
            return null;
        }

        int randomIndex = Random.Range(0, sprites.Length);
        return sprites[randomIndex];
    }
}
