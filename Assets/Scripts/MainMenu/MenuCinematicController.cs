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
    private MenuButtons menuButtons;
    [SerializeField]
    private CameraFollowPoint menuCameraFollowPoint;

    public void StartPlayTransition()
    {
        menuTrasher.MoveToPoint(-12.5f);
        menuButtons.Exit();
        StartCoroutine(COWaitForAction(1.2f, () =>
        {
            menuCameraFollowPoint.MoveToYValue(-11.0f);
        }));
    }

    public void StartGenericTransistion(string sceneName)
    {
        menuTrasher.MoveToPoint(-12.5f);
        menuButtons.Exit();
        StartCoroutine(COWaitForAction(1.2f, () =>
        {
            NavigationManager.Instance.LoadSceneByName(sceneName);
        }));
    }

    private void OnEnable()
    {
        CameraFollowPoint.onMoveFinished += OnCameraMoveFinished;
    }

    private void OnDisable()
    {
        CameraFollowPoint.onMoveFinished -= OnCameraMoveFinished;
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
