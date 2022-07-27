using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public void SaveTrashCount(int count)
    {
        if(count<0)
            return;
        PlayerPrefs.SetInt("TrashCount", count);
        PlayerPrefs.Save();
    }

    public int LoadTrashCount()
    {
        return PlayerPrefs.GetInt("TrashCount", 0);
    }

    public void SaveBestScore(int score)
    {
        if(score <0)
            return;

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if(score > bestScore)
            PlayerPrefs.SetInt("BestScore",score);
        PlayerPrefs.Save();
    }

    public void SaveTotalGearCount(int count)
    {
        if(count < 0)
            return;
        PlayerPrefs.SetInt("GearCount", count);
        PlayerPrefs.Save();
    }

    public int LoadTotalGearCount()
    {
        return PlayerPrefs.GetInt("GearCount");
    }

    public int LoadBestScore()
    {
        return PlayerPrefs.GetInt("BestScore", 0);
    }

    public void WriteTutorialFlag()
    {
        PlayerPrefs.SetInt("TutorialPlayed", 1);
        PlayerPrefs.Save();
    }

    public int ReadTutorialFlag()
    {
        return PlayerPrefs.GetInt("TutorialPlayed");
    }
    
    public void WriteShowTutorialFlag(int value)
    {
        if (value != 0 && value != 1)
            return;
        PlayerPrefs.SetInt("ShowTutorial", value);
        PlayerPrefs.Save();
    }

    public int ReadShowTutorialFlag()
    {
        return PlayerPrefs.GetInt("ShowTutorial");
    }
}
