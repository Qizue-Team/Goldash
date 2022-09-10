using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioSearcher : MonoBehaviour
{
    public void PlayClip(AudioClip clip)
    {
        AudioController.Instance.PlaySFX(clip);
    }
}
