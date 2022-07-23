using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITutorialController : Singleton<UITutorialController>
{
    [Header("References")]
    [SerializeField]
    private GameObject jumpTutorialPanel;

    public void ShowJumpTutorialPanel() 
    {
        jumpTutorialPanel.SetActive(true);
    }

    private void Update()
    {
        if(TutorialController.Instance.CurrentPhase == TutorialController.TutorialPhase.JumpTutorial &&
            TutorialController.Instance.IsStopped && Input.GetKeyDown(KeyCode.Mouse0))
        {
            jumpTutorialPanel.SetActive(false);
            TutorialController.Instance.ResumeTutorial();
        }
    }
}
