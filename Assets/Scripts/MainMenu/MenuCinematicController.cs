using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class MenuCinematicController : Singleton<MenuCinematicController>
{
    [Header("References")]
    [SerializeField]
    private MenuTrasher menuTrasher;
    [SerializeField]
    private CameraFollowPoint menuCameraFollowPoint;
    [SerializeField]
    private List<MenuButtons> menuButtons;
    [Header("Audio")]
    [SerializeField]
    private AudioClip BGMMenuClip;

    public void StartPlayTransition()
    {
        menuTrasher.MoveToPoint(-12.5f, () => { menuCameraFollowPoint.MoveToYValue(-11.0f); });
        foreach(MenuButtons button in menuButtons)
            button.Exit();
        /*
        StartCoroutine(COWaitForAction(1.2f, () =>
        {
            menuCameraFollowPoint.MoveToYValue(-11.0f);
        }));*/
    }

    public void StartGenericTransistionLeft(string sceneName)
    {
        menuTrasher.MoveToPoint(-12.5f, () => { NavigationManager.Instance.LoadSceneByName(sceneName); });
        foreach (MenuButtons button in menuButtons)
            button.Exit();
        /*
        StartCoroutine(COWaitForAction(1.2f, () =>
        {
            NavigationManager.Instance.LoadSceneByName(sceneName);
        }));*/
    }
    public void StartGenericTransistionRight(string sceneName)
    {
        menuTrasher.MoveToPoint(12.5f, () => { NavigationManager.Instance.LoadSceneByName(sceneName); });
        foreach (MenuButtons button in menuButtons)
            button.Exit();
        /*
        StartCoroutine(COWaitForAction(1.2f, () =>
        {
            NavigationManager.Instance.LoadSceneByName(sceneName);
        }));*/
    }

    private void OnEnable()
    {
        CameraFollowPoint.onMoveFinished += OnCameraMoveFinished;
    }

    private void OnDisable()
    {
        CameraFollowPoint.onMoveFinished -= OnCameraMoveFinished;
    }

    private void Start()
    {
        foreach (MenuButtons button in menuButtons)
            button.Enter();

        if(!AudioController.Instance.IsBGMPlaying)
            AudioController.Instance.PlayBGM(BGMMenuClip);

    }

    private IEnumerator COWaitForAction(float delay, Action Callback)
    {
        yield return new WaitForSeconds(delay);
        Callback?.Invoke();
    }

    private void OnCameraMoveFinished()
    {
        NavigationManager.Instance.LoadGameScene();
    }
}
