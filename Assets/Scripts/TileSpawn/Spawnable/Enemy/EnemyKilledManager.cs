using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKilledManager : Singleton<EnemyKilledManager>
{
    public int TotalEnemiesKilled { get; private set; }
    public int TotalPoints { get; private set; }

    public void AddEnemyKilled(Enemy enemy)
    {
        TotalPoints += enemy.ScorePoints;
    }

    public void ResetManager()
    {
        TotalPoints = 0;
    }
}
