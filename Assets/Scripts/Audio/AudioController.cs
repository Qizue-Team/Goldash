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

    private float _bgmSourceInitVolume = 0.0f;

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.isPlaying && bgmSource.clip.name.Equals(clip.name))
            return;
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void FadeOutBGM(float duration)
    {
        StartCoroutine(COStartFade(bgmSource, duration, 0.0f));
    }
    public void FadeInBGM(float duration)
    {
        StartCoroutine(COStartFade(bgmSource, duration, _bgmSourceInitVolume));
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

    private void Start()
    {
        _bgmSourceInitVolume = bgmSource.volume;
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
