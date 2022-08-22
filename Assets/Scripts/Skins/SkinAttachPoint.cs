using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinAttachPoint : MonoBehaviour
{
    private Skin _currentSkin = null;

    public void TrySkin(Skin skin)
    {
        if(_currentSkin != null)
            Destroy(_currentSkin.gameObject);

        GameObject skinObj = Instantiate(skin.gameObject, this.transform);
        skinObj.transform.localPosition = skin.SpawnPosition;
        _currentSkin = skinObj.GetComponent<Skin>();
    }

    // TO-DO: Method for the actual SetSkin -> with persistence writing
}
