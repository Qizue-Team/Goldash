using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MuteBGMButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI buttonText;

    private const string AUDIO_MUTED_TEXT = "UNMUTE BGM";
    private const string AUDIO_UNMUTED_TEXT = "MUTE BGM";

    private void Start()
    {
        if (AudioController.Instance.IsBGMMuted)
            buttonText.text = AUDIO_MUTED_TEXT;
        else
            buttonText.text = AUDIO_UNMUTED_TEXT;
    }

    public void MuteUnmuteBGM()
    {
        if (AudioController.Instance.IsBGMMuted)
        {
            AudioController.Instance.UnmuteBGM();
            buttonText.text = AUDIO_UNMUTED_TEXT;
        }
        else
        {
            AudioController.Instance.MuteBGM();
            buttonText.text = AUDIO_MUTED_TEXT;
        }
    }
}
