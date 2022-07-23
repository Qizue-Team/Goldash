using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        if(CurrentPhase== TutorialPhase.HeatDecreaseTutorial)
        {
            playerJump.gameObject.GetComponent<Rigidbody2D>().constraints = _constraints;
            
            //StartCoroutine
        }
    }

    private void Start()
    {
        _constraints = playerJump.gameObject.GetComponent<Rigidbody2D>().constraints;
        StartCoroutine(COExecuteTutorialJump());
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
        if(CurrentPhase == TutorialPhase.FallTutorial && _enemyTarget != null && Mathf.Abs(playerJump.gameObject.transform.position.y - _enemyTarget.transform.position.y) >= 1.7f)
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

    private IEnumerator COStopJump()
    {
        yield return new WaitForSeconds(0.5f);
        playerJump.SetJumpActive(false);
        playerJump.SetFallJumpActive(false);
    }

    private IEnumerator CODestroyEnemyTarget()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(_enemyTarget);
    }
}
