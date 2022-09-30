using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
    private void OnEnable()
    {
        Logo.OnLogoAnimationCompleted += LoadTutorialOrMenu;
    }

    private void OnDisable()
    {
        Logo.OnLogoAnimationCompleted -= LoadTutorialOrMenu;
    }

    private void LoadTutorialOrMenu()
    {
        if (DataManager.Instance.ReadTutorialFlag() == 0)
            NavigationManager.Instance.LoadSceneByName("Tutorial");
        else
            NavigationManager.Instance.LoadSceneByName("MainMenu");
    }
}
