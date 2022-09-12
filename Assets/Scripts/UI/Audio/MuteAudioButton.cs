using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MuteAudioButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI buttonText;

    private const string AUDIO_MUTED_TEXT = "UNMUTE AUDIO";
    private const string AUDIO_UNMUTED_TEXT = "MUTE AUDIO";

    private void Start()
    {
        if(AudioController.Instance.IsAudioMuted)
            buttonText.text = AUDIO_MUTED_TEXT;
       else
            buttonText.text = AUDIO_UNMUTED_TEXT;
    }

    public void MuteUnmuteAudio()
    {
        if (AudioController.Instance.IsAudioMuted)
        {
            AudioController.Instance.UnmuteAudio();
            buttonText.text = AUDIO_UNMUTED_TEXT;
        }
        else
        {
            AudioController.Instance.MuteAudio();
            buttonText.text = AUDIO_MUTED_TEXT;
        }
    }
}
