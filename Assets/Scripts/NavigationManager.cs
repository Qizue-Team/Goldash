using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : Singleton<NavigationManager>
{
    public static string PrevScene { get; private set; }

    public void LoadSceneByName(string name)
    {
        PrevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(name);
    }

    public void LoadGameScene()
    {
        PrevScene = SceneManager.GetActiveScene().name;
        LoadSceneByName("Prototype");
    }

    public void ExitFromGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        PrevScene = SceneManager.GetActiveScene().name;
        Time.timeScale = 1.0f; // if loaded from pause menu
        LoadSceneByName("MainMenu");
    }

    public void LoadTutorial()
    {
        PrevScene = SceneManager.GetActiveScene().name;
        DataManager.Instance.WriteShowTutorialFlag(1);
        LoadSceneByName("Tutorial");
    }
}
