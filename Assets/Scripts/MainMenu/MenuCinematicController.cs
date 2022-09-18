using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

public class MenuCinematicController : Singleton<MenuCinematicController>
{
    [Header("References")]
    [SerializeField]
    private MenuTrasher menuTrasher;
    [SerializeField]
    private CameraFollowPoint menuCameraFollowPoint;
    [SerializeField]
    private List<MenuButtons> menuButtons;

    public void StartPlayTransition()
    {
        menuTrasher.MoveToPoint(-12.5f, () => { menuCameraFollowPoint.MoveToYValue(-11.0f); });
        foreach(MenuButtons button in menuButtons)
            button.Exit();
    }

    public void StartGenericTransistionLeft(string sceneName)
    {
        menuTrasher.MoveToPoint(-12.5f, () => { NavigationManager.Instance.LoadSceneByName(sceneName); });
        foreach (MenuButtons button in menuButtons)
            button.Exit();
    }
    public void StartGenericTransistionRight(string sceneName)
    {
        menuTrasher.MoveToPoint(12.5f, () => { NavigationManager.Instance.LoadSceneByName(sceneName); });
        foreach (MenuButtons button in menuButtons)
            button.Exit();
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
