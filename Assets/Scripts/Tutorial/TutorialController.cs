using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : Singleton<TutorialController>
{
    public TutorialPhase CurrentPhase { get; private set; }
    public bool IsStopped { get; private set; }

    public enum TutorialPhase
    {
        JumpTutorial,
        OverheatTutorial,
        JumpFallTutorial,
        FallTutorial,
        HeatDecreaseTutorial,
        TrashTutorial,
        EndTutorial
    }

    [Header("References")]
    [SerializeField]
    private PlayerJump playerJump;
    [SerializeField]
    private TutorialSpawner tutorialSpawner;
    [SerializeField]
    private TerrainTileSpawner tileSpawner;
    [SerializeField]
    private PlayerTutorialMovements playerMovements;
    [SerializeField]
    private LoadingScreen loadingScreen;

    private GameObject _enemyTarget;
    private RigidbodyConstraints2D _constraints;
    public void ResumeTutorial()
    {
        IsStopped = false;
      
        tileSpawner.Resume();
        StartCoroutine(COStopJump());
        NextPhase();

        if(_enemyTarget != null && CurrentPhase != TutorialPhase.FallTutorial)
            StartCoroutine(CODestroyEnemyTarget());

        if (CurrentPhase == TutorialPhase.OverheatTutorial)
            StartCoroutine(COExecuteOverheatTutorial());

        if(CurrentPhase == TutorialPhase.JumpFallTutorial)
            StartCoroutine(COExecuteTutorialJump());
        
        if(CurrentPhase == TutorialPhase.HeatDecreaseTutorial)
        {
            playerJump.gameObject.GetComponent<Rigidbody2D>().constraints = _constraints;
            StartCoroutine(COExecuteHeatDecreaseTutorial());
        }

        if (CurrentPhase == TutorialPhase.TrashTutorial)
            StartCoroutine(COExecuteTrashTutorial());

        if (CurrentPhase == TutorialPhase.EndTutorial)
            StartCoroutine(COExecuteFinalTransition());
    }

    private void Start()
    {
        _constraints = playerJump.gameObject.GetComponent<Rigidbody2D>().constraints;

        playerJump.SetJumpActive(false);
        playerJump.SetFallJumpActive(false);
        StartCoroutine(COWaitForAction(2.0f, () => {

            if (DataManager.Instance.ReadTutorialFlag() == 0 || DataManager.Instance.ReadShowTutorialFlag() == 1)
            {
                DataManager.Instance.WriteShowTutorialFlag(0);
                loadingScreen.Close();
                StartCoroutine(COExecuteTutorialJump());
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }));
    }

    private void Update()
    {
        if (CurrentPhase == TutorialPhase.JumpTutorial && _enemyTarget != null && Mathf.Abs(playerJump.gameObject.transform.position.x - _enemyTarget.transform.position.x) <= 2.0f)
        {
            StopTutorial();
            playerJump.SetJumpActive(true);
            UITutorialController.Instance.ShowJumpTutorialPanel();
        }
        if (CurrentPhase == TutorialPhase.JumpFallTutorial && _enemyTarget != null && Mathf.Abs(playerJump.gameObject.transform.position.x - _enemyTarget.transform.position.x) <= 2.0f)
        {
            StopTutorial();
            playerJump.SetJumpActive(true);
            UITutorialController.Instance.ShowJumpTutorialPanel();
        }
        if(CurrentPhase == TutorialPhase.FallTutorial && _enemyTarget != null && Mathf.Abs(playerJump.gameObject.transform.position.y - _enemyTarget.transform.position.y) >= 1.5f)
        {
            StopTutorial();
            playerJump.SetFallJumpActive(true);
            UITutorialController.Instance.ShowFallTutorialPanel();
        }
    }

    private void StopTutorial()
    { 
        tileSpawner.Stop();
        if(CurrentPhase == TutorialPhase.FallTutorial)
            playerJump.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        IsStopped = true;
    }

    private void NextPhase()
    {
        CurrentPhase++;
    }

    private IEnumerator COExecuteTutorialJump()
    {
        playerJump.SetJumpActive(false);
        playerJump.SetFallJumpActive(false);
        yield return new WaitForSeconds(2.0f);
        _enemyTarget = tutorialSpawner.SpawnEnemy();
    }

    private IEnumerator COExecuteOverheatTutorial()
    {
        yield return new WaitForSeconds(2.0f);
        StopTutorial();
        UITutorialController.Instance.ShowOverheatTutorialPanel();
    }

    private IEnumerator COExecuteHeatDecreaseTutorial()
    {
        yield return new WaitForSeconds(2.0f);
        StopTutorial();
        UITutorialController.Instance.ShowHeatDecreaseTutorialPanel();
    }

    private IEnumerator COExecuteTrashTutorial()
    {
        yield return new WaitForSeconds(2.0f);
        tutorialSpawner.SpawnTrash();
        yield return new WaitForSeconds(3.0f);
        StopTutorial();
        UITutorialController.Instance.ShowTrashTutorialPanel();
    }

    private IEnumerator COExecuteFinalTransition()
    {
        yield return new WaitForSeconds(4.5f);
        StopTutorial();
        DataManager.Instance.WriteTutorialFlag();
        AudioController.Instance.FadeOutBGM(5.0f);
        playerMovements.MoveToPoint(12.0f);
    }

    private IEnumerator COStopJump()
    {
        yield return new WaitForSeconds(0.5f);
        playerJump.SetJumpActive(false);
        //playerJump.SetFallJumpActive(false);
    }

    private IEnumerator CODestroyEnemyTarget()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(_enemyTarget);
    }

    private IEnumerator COWaitForAction(float delay, Action Callback)
    {
        yield return new WaitForSeconds(delay);
        Callback?.Invoke();
    }
}
