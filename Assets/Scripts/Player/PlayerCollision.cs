using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xPoke.CustomLog;

[RequireComponent(typeof(PlayerJump))]
public class PlayerCollision : MonoBehaviour
{

    private PlayerJump _playerJump;

    private void Awake()
    {
        _playerJump = GetComponent<PlayerJump>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TerrainTile tile = collision.GetComponentInParent<TerrainTile>();

        Trash trash = null;
        if (collision.TryGetComponent(out trash))
        {
            // Update Score
            GameController.Instance.IncreaseScore(trash.ScorePoints);
            UIController.Instance.UpdateScore(GameController.Instance.Score);

            // Update Trash Count
            GameController.Instance.IncreaseTrashCount(1);
            UIController.Instance.UpdateTrashCount(GameController.Instance.TrashCount);

            tile.DestroySpawnedObject();
        }

        Enemy enemy = null;
        if(collision.TryGetComponent(out enemy))
        {
            Vector2 hitPoint = collision.ClosestPoint(transform.position);
            Vector2 hitDirection = hitPoint - (Vector2)transform.position;

            // If hit direction is pointing downwards
            if(hitDirection.y < 0)
            {
                _playerJump.BounceJump();

                // Update Score
                GameController.Instance.IncreaseScore(enemy.ScorePoints);
                UIController.Instance.UpdateScore(GameController.Instance.Score);

                // Enemy Kill
                enemy.Die();
                //tile.DestroySpawnedObject();
            }
            else
            {
                // Player Dead - GameOver
                GameController.Instance.GameOver();
                CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "GameOver for Enemy collision");
                // TEMP: Destroy Player
                Destroy(gameObject);
            }
        }
        
    }
}
