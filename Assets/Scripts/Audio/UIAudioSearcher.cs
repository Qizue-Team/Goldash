using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioSearcher : MonoBehaviour
{
    public void PlayClip(AudioClip clip) => AudioController.Instance.PlaySFX(clip);
    public void FadeInBGM(float duration) => AudioController.Instance.FadeInBGM(duration);
    public void FadeOutBGM(float duration) => AudioController.Instance.FadeOutBGM(duration);
}
