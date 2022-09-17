using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MuteAudioButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Image imageIcon;
    [Header("Icons")]
    [SerializeField]
    private Sprite unmutedIcon;
    [SerializeField]
    private Sprite mutedIcon;

    private void Start()
    {
        if (AudioController.Instance.IsAudioMuted)
            imageIcon.sprite = mutedIcon;
        else
            imageIcon.sprite = unmutedIcon;
    }

    public void MuteUnmuteAudio()
    {
        if (AudioController.Instance.IsAudioMuted)
        {
            AudioController.Instance.UnmuteAudio();
            imageIcon.sprite = unmutedIcon;
        }
        else
        {
            AudioController.Instance.MuteAudio();
            imageIcon.sprite = mutedIcon;
        }
    }
}
