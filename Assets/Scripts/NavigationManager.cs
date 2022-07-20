using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : Singleton<NavigationManager>
{
    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadGameScene()
    {
        LoadSceneByName("Prototype");
    }

    public void ExitFromGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        LoadSceneByName("MainMenu");
    }
}
