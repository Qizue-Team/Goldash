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
    [SerializeField]
    private GameObject heatDecreaseTutorialPanel;
    [SerializeField]
    private GameObject trashTutorialPanel;
    [SerializeField]
    private GameObject powerupTutorialPanel;
    [SerializeField]
    private GameObject powerupCollectedTutorialPanel;

    public void ShowPowerupCollectedTutorialPanel()
    {
        powerupCollectedTutorialPanel.SetActive(true);
    }

    public void ShowPowerupTutorialPanel()
    {
        powerupTutorialPanel.SetActive(true);
    }

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

    public void ShowHeatDecreaseTutorialPanel()
    {
        heatDecreaseTutorialPanel.SetActive(true);
    }

    public void ShowTrashTutorialPanel()
    {
        trashTutorialPanel.SetActive(true);
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

        if (TutorialController.Instance.CurrentPhase == TutorialController.TutorialPhase.HeatDecreaseTutorial &&
          TutorialController.Instance.IsStopped && Input.GetKeyDown(KeyCode.Mouse0))
        {
            heatDecreaseTutorialPanel.SetActive(false);
            TutorialController.Instance.ResumeTutorial();
        }

        if (TutorialController.Instance.CurrentPhase == TutorialController.TutorialPhase.TrashTutorial &&
         TutorialController.Instance.IsStopped && Input.GetKeyDown(KeyCode.Mouse0))
        {
            trashTutorialPanel.SetActive(false);
            TutorialController.Instance.ResumeTutorial();
        }

        if (TutorialController.Instance.CurrentPhase == TutorialController.TutorialPhase.PowerupTutorial &&
         TutorialController.Instance.IsStopped && Input.GetKeyDown(KeyCode.Mouse0))
        {
            powerupTutorialPanel.SetActive(false);
            TutorialController.Instance.ResumeTutorial();
        }

        if (TutorialController.Instance.CurrentPhase == TutorialController.TutorialPhase.PowerupCollected &&
        TutorialController.Instance.IsStopped && Input.GetKeyDown(KeyCode.Mouse0))
        {
            powerupCollectedTutorialPanel.SetActive(false);
            TutorialController.Instance.ResumeTutorial();
        }
    }
}
