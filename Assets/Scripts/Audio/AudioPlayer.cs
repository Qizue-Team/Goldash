using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class AudioPlayer : Singleton<AudioPlayer>
{
    [Header("AudioClips")]
    [SerializeField]
    private AudioClip BGMMenuClip;
    [SerializeField]
    private AudioClip BGMTutorialClip;
    [SerializeField]
    private AudioClip BGMShopsClip;

    private void Start()
    {
       
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += HandleMusicChange;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= HandleMusicChange;
    }

    private void HandleMusicChange(Scene current, Scene next)
    {
        if (SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {
            AudioController.Instance.PlayBGM(BGMMenuClip);
            AudioController.Instance.FadeInBGM(1.0f);
        }
        else if (SceneManager.GetActiveScene().name.Equals("Shops"))
        {
            AudioController.Instance.PlayBGM(BGMShopsClip);
            AudioController.Instance.FadeInBGM(1.0f);
        }
        else if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
        {
            StartCoroutine(COPlayTutorialBGM());
        }
    }

    private IEnumerator COPlayTutorialBGM()
    {
        yield return new WaitForSeconds(0.1f);
        AudioController.Instance.PlayBGM(BGMTutorialClip);
        AudioController.Instance.FadeInBGM(1.0f);
    }
}
