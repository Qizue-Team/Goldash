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
    private bool _isJumpTutorialStarted = false;

    public void ResumeTutorial()
    {
        IsStopped = false;
      
        tileSpawner.Resume();
        StartCoroutine(COStopJump());
        NextPhase();
    }

    private void Start()
    {
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

    }

    private void StopTutorial()
    { 
        tileSpawner.Stop();
        IsStopped = true;
    }

    private void NextPhase()
    {
        CurrentPhase++;
    }

    private IEnumerator COExecuteTutorialJump()
    {
        playerJump.SetJumpActive(false);
        _isJumpTutorialStarted = true;
        yield return new WaitForSeconds(2.0f);
        _enemyTarget = tutorialSpawner.SpawnEnemy();
    }

    private IEnumerator COStopJump()
    {
        yield return new WaitForSeconds(0.5f);
        playerJump.SetJumpActive(false);
    }
}
