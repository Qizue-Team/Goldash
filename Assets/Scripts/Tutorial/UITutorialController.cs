using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITutorialController : Singleton<UITutorialController>
{
    [Header("References")]
    [SerializeField]
    private GameObject jumpTutorialPanel;
    [SerializeField]
    private GameObject overheatTutorialPanel;
    [SerializeField]
    private GameObject fallTutorialPanel;

    public void ShowJumpTutorialPanel() 
    {
        jumpTutorialPanel.SetActive(true);
    }

    public void ShowOverheatTutorialPanel()
    {
        overheatTutorialPanel.SetActive(true);
    }

    public void ShowFallTutorialPanel()
    {
        fallTutorialPanel.SetActive(true);
    }

    private void Update()
    {
        if(TutorialController.Instance.CurrentPhase == TutorialController.TutorialPhase.JumpTutorial &&
            TutorialController.Instance.IsStopped && Input.GetKeyDown(KeyCode.Mouse0))
        {
            jumpTutorialPanel.SetActive(false);
            TutorialController.Instance.ResumeTutorial();
        }
        if (TutorialController.Instance.CurrentPhase == TutorialController.TutorialPhase.OverheatTutorial &&
           TutorialController.Instance.IsStopped && Input.GetKeyDown(KeyCode.Mouse0))
        {
            overheatTutorialPanel.SetActive(false);
            TutorialController.Instance.ResumeTutorial();
        }

        if (TutorialController.Instance.CurrentPhase == TutorialController.TutorialPhase.JumpFallTutorial &&
          TutorialController.Instance.IsStopped && Input.GetKeyDown(KeyCode.Mouse0))
        {
            jumpTutorialPanel.SetActive(false);
            TutorialController.Instance.ResumeTutorial();
        }

        if (TutorialController.Instance.CurrentPhase == TutorialController.TutorialPhase.FallTutorial &&
         TutorialController.Instance.IsStopped && Input.GetKeyDown(KeyCode.Mouse0))
        {
            fallTutorialPanel.SetActive(false);
            TutorialController.Instance.ResumeTutorial();
        }
    }
}
