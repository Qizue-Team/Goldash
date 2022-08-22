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

    private Skin _skin;

    public void SetEntry(Skin skin)
    {
        _skin = skin;
        iconImage.sprite = skin.gameObject.GetComponent<SpriteRenderer>().sprite;
        nameText.text = skin.SkinName;
        costText.text = "Gear Cost: " + skin.Cost;

        // Listeners here
    }

    // Listener methods here Unlock and Try On
}
