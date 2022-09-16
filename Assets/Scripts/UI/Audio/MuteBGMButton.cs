using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class MuteBGMButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI buttonText;

    private const string AUDIO_MUTED_TEXT = "UNMUTE BGM";
    private const string AUDIO_UNMUTED_TEXT = "MUTE BGM";

    private void Start()
    {
        StartCoroutine(COWait(0.1f, UpdateUI));
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

    private void UpdateUI()
    {
        if (AudioController.Instance.IsBGMMuted)
            buttonText.text = AUDIO_MUTED_TEXT;
        else
            buttonText.text = AUDIO_UNMUTED_TEXT;
    }

    private IEnumerator COWait(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}
