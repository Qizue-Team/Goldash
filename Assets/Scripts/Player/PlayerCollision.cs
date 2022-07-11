using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
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
        
    }
}
