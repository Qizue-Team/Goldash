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
        menuTrasher.MoveToPoint(-12.5f, () => { menuCameraFollowPoint.MoveToYValue(-11.5f, ()=> { NavigationManager.Instance.LoadGameScene(); }); });
        foreach(MenuButtons button in menuButtons)
            button.Exit();
    }

    public void StartGenericTransistionLeft(string sceneName)
    {
        menuTrasher.MoveToPoint(-12.5f, () => { NavigationManager.Instance.LoadSceneByName(sceneName); });
        foreach (MenuButtons button in menuButtons)
            button.Exit();
    }

    public void StartCameraTransitionRight(string sceneName)
    {
        menuCameraFollowPoint.MoveToXValue(23.8f, () => { NavigationManager.Instance.LoadSceneByName(sceneName); }, 3.3f);
        menuTrasher.MoveToPoint(-12.5f);
        foreach (MenuButtons button in menuButtons)
            button.Exit();
    }

    public void StartGenericTransistionRight(string sceneName)
    {
        menuTrasher.MoveToPoint(12.5f, () => { NavigationManager.Instance.LoadSceneByName(sceneName); });
        foreach (MenuButtons button in menuButtons)
            button.Exit();
    }

    private void Start()
    {
        foreach (MenuButtons button in menuButtons)
            button.Enter();
    }
}
