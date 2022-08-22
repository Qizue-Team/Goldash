using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinShopEntry : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI costText;
    [SerializeField]
    private Button unlockSetButton;
    [SerializeField]
    private Button tryOnButton;

    private Skin _skin;
    private bool _addOnceUnlock = false;
    private bool _addOnceSet = false;
    private bool _addOnceTryOn = false;

    public void SetEntry(Skin skin)
    {
        _skin = skin;
        iconImage.sprite = skin.gameObject.GetComponent<SpriteRenderer>().sprite;
        nameText.text = skin.SkinName;
        costText.text = "Gear Cost: " + skin.Cost;

        // Listeners here
        if(!_skin.IsUnlocked && !_addOnceUnlock)
        {
            _addOnceUnlock = true;
            unlockSetButton.onClick.RemoveAllListeners();
            unlockSetButton.onClick.AddListener(() => Unlock());
        }
        else if (_skin.IsUnlocked &&!_addOnceSet)
        {
            _addOnceSet = true;
            unlockSetButton.onClick.RemoveAllListeners();
            unlockSetButton.onClick.AddListener(() => SetSkin());
            unlockSetButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "S E T";
        }

        if (!_addOnceTryOn)
        {
            _addOnceTryOn= true;
            tryOnButton.onClick.RemoveAllListeners();
            tryOnButton.onClick.AddListener(()=>TryOn());
        }
    }

    // TO-DO: Listener methods here Unlock and Try On
    private void Unlock()
    {
        if (_skin.IsUnlocked)
            return;
        _skin.Unlock();
        FindObjectOfType<GearText>().UpdateGearText(DataManager.Instance.LoadTotalGearCount());

        // TO-DO: Call event for the Manager -> it will save data persistnetly

        // Now it's unlocked so add set
        if (_skin.IsUnlocked && !_addOnceSet)
        {
            _addOnceSet = true;
            unlockSetButton.onClick.RemoveAllListeners();
            unlockSetButton.onClick.AddListener(() => SetSkin());
            unlockSetButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "S E T";
        }
    }

    private void TryOn()
    {
        // TO-DO: TryOn code
    }

    private void SetSkin()
    {
        // TO-DO: Set code
    }
}
