using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    public bool IsBGMMuted { get; private set; }
    public bool IsAudioMuted { get => sfxSources[0].volume == 0.0f; }
    public bool IsBGMPlaying{ get => bgmSource.isPlaying;}

    [Header("Sources")]
    [SerializeField]
    private AudioSource bgmSource;
    [SerializeField]
    private List<AudioSource> sfxSources = new List<AudioSource>();

    private float _bgmSourceInitVolume = 0.0f;
    private float _sfxSourceInitVolume = 0.0f;

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.isPlaying && bgmSource.clip.name.Equals(clip.name))
            return;
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void FadeOutBGM(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(COStartFade(bgmSource, duration, 0.0f));
    }
    public void FadeInBGM(float duration)
    {
        StopAllCoroutines();
        if (IsBGMMuted)
            return;
        StartCoroutine(COStartFade(bgmSource, duration, _bgmSourceInitVolume));
    }

    public void PauseBGM() => bgmSource.Pause();
    public void StopBGM() => bgmSource.Stop();

    public void MuteBGM() { bgmSource.volume = 0.0f; IsBGMMuted = true; }
    public void UnmuteBGM() { bgmSource.volume = _bgmSourceInitVolume; IsBGMMuted = false; }

    public void MuteAudio()
    {
        foreach(AudioSource sfx in sfxSources)
            sfx.volume = 0.0f;
    }
    public void UnmuteAudio()
    {
        foreach (AudioSource sfx in sfxSources)
            sfx.volume = _sfxSourceInitVolume;
    }

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


        if (bgmSource == null || sfxSources[0] == null)
            return;
        _bgmSourceInitVolume = bgmSource.volume;
        _sfxSourceInitVolume = sfxSources[0].volume;
        IsBGMMuted = false;
    }

    private IEnumerator COStartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
