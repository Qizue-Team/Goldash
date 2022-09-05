using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using xPoke.CustomLog;

[RequireComponent(typeof(PlayerJump))]
public class PlayerCollision : MonoBehaviour
{
    public int EnemyCollisionCount { get; private set; }

    [Header("Tutorial")]
    [SerializeField]
    private bool isTutorial;
    [Header("Debug")]
    [SerializeField]
    private bool isDebugActive;

    private PlayerJump _playerJump;
    private Vector2 _hitPoint;
    private Vector2 _hitDirection;
    private bool _canBeHitByEnemies = true;
    private int _trashCountMultiplier = 1;

    public void CanBeHitByEnemies(bool active) 
    {
        _canBeHitByEnemies = active;
    }

    public void SetTrashCountMultiplier(int multiplier)
    {
        if(multiplier <=0)
            multiplier = 1;
        _trashCountMultiplier = multiplier;
    }

    private void Awake()
    {
        _playerJump = GetComponent<PlayerJump>();
        EnemyCollisionCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TerrainTile tile = collision.GetComponentInParent<TerrainTile>();

        Trash trash = null;
        if (collision.TryGetComponent(out trash))
        {
            if (!isTutorial)
            {
                // Add Collected Trash
                for(int i = 0; i < _trashCountMultiplier; i++)
                    TrashCollectedManager.Instance.AddTrash(trash);

                // Update Score
                GameController.Instance.IncreaseScore(trash.ScorePoints*_trashCountMultiplier);
                UIController.Instance.UpdateScore(GameController.Instance.Score);

                // Update Trash Count
                GameController.Instance.IncreaseTrashCount(1*_trashCountMultiplier);
                UIController.Instance.UpdateTrashCount(GameController.Instance.TrashCount);

                tile.DestroySpawnedObject();
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }

        Enemy enemy = null;
        if(collision.TryGetComponent(out enemy))
        {
            _hitPoint = collision.ClosestPoint(transform.position);
            _hitDirection = _hitPoint - (Vector2)transform.position;

            // If hit direction is pointing downwards
            if(_hitDirection.y < 0)
            {
                _playerJump.BounceJump();
                if (!isTutorial)
                {
                    // Add Collected Trash
                    EnemyKilledManager.Instance.AddEnemyKilled(enemy);

                    // Achievement value
                    AchievementManager.Instance.EnemiesKilledOneRun++;
                    AchievementManager.Instance.EnemiesKilledTotal++;
                    AchievementManager.Instance.UpdateAchievements();

                    // Update Score
                    GameController.Instance.IncreaseScore(enemy.ScorePoints);
                    UIController.Instance.UpdateScore(GameController.Instance.Score);

                }
                // Enemy Kill
                enemy.Die();
                if(isDebugActive)
                    Debug.Break(); 
            }
            else if(_canBeHitByEnemies)
            {
                // Player Dead - GameOver
                GameController.Instance.GameOver();
                CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "GameOver for Enemy collision");
                
                // Animation and Destroy Player
                GetComponent<Animator>().SetTrigger("Explode");
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                StartCoroutine(COWaitForAction(0.5f, ()=> {
                    Destroy(gameObject);
                }));
            }
            else // If invuln power up is active
            {
                enemy.Die();
                EnemyCollisionCount++;
            }
        }

        OneTimePowerUp powerUp = null;
        if(collision.TryGetComponent(out powerUp))
        {
            RunPowerUpManager.Instance.AddPowerUp(powerUp.GetType());
            Destroy(powerUp.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (!isDebugActive)
            return;

        Gizmos.color = Color.green;
        if (!_hitPoint.Equals(Vector2.zero))
        {
            Gizmos.DrawWireSphere(_hitPoint, 0.2f);

            if (!_hitDirection.Equals(Vector2.zero))
            {
                Gizmos.DrawLine(transform.position, _hitPoint);
            }
        }
    }

    private IEnumerator COWaitForAction(float delay, Action Callback)
    {
        yield return new WaitForSeconds(delay);
        Callback?.Invoke();
    }
}
