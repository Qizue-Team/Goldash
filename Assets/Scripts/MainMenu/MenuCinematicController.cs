using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuCinematicController : Singleton<MenuCinematicController>
{
    [Header("References")]
    [SerializeField]
    private MenuTrasher menuTrasher;
    [SerializeField]
    private MenuButtons menuButtons;
    [SerializeField]
    private MenuCameraFollowPoint menuCamera;

    public void StartPlayTransition()
    {
        menuTrasher.MoveToPoint(-11.0f);
        menuButtons.Exit();
        StartCoroutine(COWaitForAction(1.0f, () =>
        {
            menuCamera.MoveToYValue(-11.0f);
        }));
    }
    private IEnumerator COWaitForAction(float delay, Action Callback)
    {
        yield return new WaitForSeconds(delay);
        Callback?.Invoke();
    }
}
