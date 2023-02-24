// Purpose: Instructions for the Level Manager, used for the menus
// Source 1: https://www.youtube.com/watch?v=1rjzmYSIOhw
// Source 2: https://www.youtube.com/watch?v=YdxYdHidCkE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialBackButton : MonoBehaviour
{
// Each function allows me to use the scene manager to switch scenes when a button is pressed
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
