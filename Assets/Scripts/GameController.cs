using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public const int MAX_SCORE = 9999999;
    public const int MIN_SCORE = 0;
    public const int MAX_TRASH_COUNT = 99999;
    public const int MIN_TRASH_COUNT = 0;

    public int Score { get; private set; }
    public int TrashCount { get; private set; }

    public void IncreaseScore(int amount)
    {
        if(amount<MIN_SCORE || amount > MAX_SCORE)
            return;
        Score += amount;
        if (Score > MAX_SCORE)
            Score = MAX_SCORE;
    }

    public void IncreaseTrashCount(int amount)
    {
        if (amount < MIN_TRASH_COUNT || amount > MAX_TRASH_COUNT )
            return;
        TrashCount += amount;
        if(TrashCount > MAX_TRASH_COUNT)
            TrashCount = MAX_TRASH_COUNT;
    }
}
