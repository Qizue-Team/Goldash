using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : Singleton<TutorialController>
{
    [Header("References")]
    [SerializeField]
    private PlayerJump playerJump;
    [SerializeField]
    private TutorialSpawner tutorialSpawner;
    [SerializeField]
    private TerrainTileSpawner tileSpawner;

    private GameObject enemyTarget;

    private void Start()
    {
        StartCoroutine(COExecuteTutorialJump());
    }

    private void Update()
    {
        if(enemyTarget != null && Mathf.Abs(playerJump.gameObject.transform.position.x - enemyTarget.transform.position.x) <= 2.0f)
        {
            StopTutorial();
        }
    }

    private void StopTutorial()
    {
        Time.timeScale = 0.0f;
    }

    private void ResumeTutorial()
    {
        Time.timeScale = 1.0f;
    }

    private IEnumerator COExecuteTutorialJump()
    {
        playerJump.SetJumpActive(false);
        yield return new WaitForSeconds(2.0f);
        enemyTarget = tutorialSpawner.SpawnEnemy();
    }
}
