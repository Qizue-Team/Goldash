using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [Header("Sources")]
    [SerializeField]
    private AudioSource bgmSource;
    [SerializeField]
    private List<AudioSource> sfxSources = new List<AudioSource>();

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void PauseBGM() => bgmSource.Pause();
    public void StopBGM() => bgmSource.Stop();
    

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
            return;
        foreach(AudioSource source in sfxSources)
        {
            if(!source.isPlaying)
            {
                source.clip = clip;
                source.Play();
                return;
            }
        }
    }
}
