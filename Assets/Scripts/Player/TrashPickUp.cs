using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TerrainTile tile = collision.GetComponentInParent<TerrainTile>();
        Trash trash = collision.GetComponent<Trash>();

        // Update Score
        GameController.Instance.IncreaseScore(trash.ScorePoints);
        UIController.Instance.UpdateScore(GameController.Instance.Score);

        // Update Trash Count
        GameController.Instance.IncreaseTrashCount(1);
        UIController.Instance.UpdateTrashCount(GameController.Instance.TrashCount);

        tile.DestroyTrash();
    }
}
