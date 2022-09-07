using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Source")]
    [SerializeField]
    private AudioSource source;

    [Header("Clips")]
    [SerializeField]
    private AudioClip[] clips;

    public void PlayClipByIndex(int index)
    {
        if(index >= clips.Length)
            return;
        source.clip = clips[index];
        source.Play();
    }

    public void PlayClibByName(string name)
    {
        foreach(AudioClip clip in clips)
        {
            if(clip.name.Equals(name))
            {
                source.clip = clip;
                source.Play();
                return;
            }
        }
    }
}
