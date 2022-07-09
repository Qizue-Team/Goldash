using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : Spawner
{
    public const float MAX_SCENE_Y = 3.0f;
    public const float MIN_SCENE_Y = -4.0f;
    public const float LEFT_MOST_X_TERRAIN_VALUE = -11.5f;
    public const float RIGHT_MOST_X_TERRAIN_VALUE = 12.5f;

    [SerializeField]
    private TileSet tileSet;

    [Header("Platform Spawn Settings")]
    [SerializeField]
    private int levelNumbers = 2;
    [SerializeField]
    private float spaceBetweenPlatforms = 1.0f;
    [SerializeField]
    private float offset = 0.0f;

    public override void Spawn()
    {
        // SpawnOnLevel(1)
    }

    public void SpawnOnLevel(int level)
    {

    }

    private void OnDrawGizmosSelected()
    {
        // Calculate space between min and max
        float area = Mathf.Abs(MAX_SCENE_Y - MIN_SCENE_Y);
        float valueToAdd = spaceBetweenPlatforms;

        Gizmos.color = Color.green;

        float addition = 0;
        for(int i = 0; i<levelNumbers; i++)
        {
            Gizmos.DrawLine(new Vector3(RIGHT_MOST_X_TERRAIN_VALUE, MIN_SCENE_Y + addition + offset, 0.0f),
                new Vector3(LEFT_MOST_X_TERRAIN_VALUE, MIN_SCENE_Y + addition + offset, 0.0f));
            addition += valueToAdd;
        }
    }
}
