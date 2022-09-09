using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xPoke.CustomLog;

public class SkinAttachPoint : MonoBehaviour
{
    [SerializeField]
    private SkinSet skinSet;

    private Skin _currentSkin = null;

    public void TrySkin(Skin skin)
    {
        if (_currentSkin != null)
            Destroy(_currentSkin.gameObject);

        GameObject skinObj = Instantiate(skin.gameObject, this.transform);
        skinObj.transform.localPosition = skin.SpawnPosition;
        _currentSkin = skinObj.GetComponent<Skin>();
    }

    // Method for the actual SetSkin -> with persistence writing
    public void SetSkin(Skin skin)
    {
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "Skin set ID: " + skin.ID);
        TrySkin(skin);
        SerializableSkinSetData data = new SerializableSkinSetData();
        data.ID = skin.ID;
        DataManager.Instance.SaveSetSkinData(data);
        skin.UpdateSkin();
    }

    public void UnsetSkin(Skin skin)
    {
        if(_currentSkin != null)
            Destroy(_currentSkin.gameObject);
        SerializableSkinSetData data = new SerializableSkinSetData();
        data.ID = -1;
        DataManager.Instance.SaveSetSkinData(data);
        _currentSkin = null;
        skin.UpdateSkin();
    }

    public void FlipSkin()
    {
        if(_currentSkin.ShouldFlip)
            transform.GetChild(0).transform.localPosition = new Vector3(-0.2f, transform.GetChild(0).transform.localPosition.y, transform.GetChild(0).transform.localPosition.z);
    }

    private void Start()
    {
        SerializableSkinSetData data = DataManager.Instance.LoadSkinSetData();
        if (data == null)
            return;
        foreach(Skin skin in skinSet.Skins)
        {
            if(skin.ID == data.ID)
            {
                SetSkin(skin);
                return;
            }
        }
    }

}
