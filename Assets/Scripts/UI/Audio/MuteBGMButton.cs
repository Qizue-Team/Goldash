using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class MuteBGMButton : MonoBehaviour
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
        StartCoroutine(COWait(0.1f, UpdateUI));
    }

    public void MuteUnmuteBGM()
    {
        if (AudioController.Instance.IsBGMMuted)
        {
            AudioController.Instance.UnmuteBGM();
            imageIcon.sprite = unmutedIcon;
        }
        else
        {
            AudioController.Instance.MuteBGM();
            imageIcon.sprite = mutedIcon;
        }
    }

    private void UpdateUI()
    {
        if (AudioController.Instance.IsBGMMuted)
            imageIcon.sprite = mutedIcon;
        else
            imageIcon.sprite = unmutedIcon;
    }

    private IEnumerator COWait(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}
