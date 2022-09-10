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
        if (bgmSource.isPlaying && bgmSource.clip.name.Equals(clip.name))
            return;
        Debug.Log("PLAYING");
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

    protected override void Awake()
    {
        AudioController[] controllers = FindObjectsOfType<AudioController>();
        if(controllers.Length > 1)
        {
            for(int i=1; i<controllers.Length; i++)
            {
                Destroy(controllers[i].gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
