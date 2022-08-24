using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using xPoke.CustomLog;

public class DataManager : Singleton<DataManager>
{
    private string _powerUpDataListFileName = "/power-up-data.dat";
    private string _skinsDataListFileName = "/skins-data.dat";
    private string _skinSetDataListFileName = "/skin-set.dat";
    private string _avgGearRunFileName = "/average-gear.csv";

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

    public void SavePowerUpData(List<SerializablePowerUpData> dataList)
    {
        // JSON Creation
        SerializableList<SerializablePowerUpData> listObj = new SerializableList<SerializablePowerUpData>(dataList);
        string json = JsonUtility.ToJson(listObj);
        CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "JSON Created: "+json);

        // Save json string to file
        if(File.Exists(@"" + Application.persistentDataPath + _powerUpDataListFileName))
        {
            File.Delete(@""+ Application.persistentDataPath + _powerUpDataListFileName);
        }
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + _powerUpDataListFileName);
        writer.WriteLine(json);
        writer.Close();
        CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "JSON Saved");
    }

    public List<SerializablePowerUpData> LoadPowerUpData()
    {
        // Read from file the json string
        if (!File.Exists(@""+ Application.persistentDataPath + _powerUpDataListFileName))
            return null;
        StreamReader reader = new StreamReader(Application.persistentDataPath + _powerUpDataListFileName);
        string json = reader.ReadToEnd();
        CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "JSON Read: "+json);
        reader.Close();
        return JsonUtility.FromJson<SerializableList<SerializablePowerUpData>>(json).serializableList;
    }

    public void SaveSkinsData(List<SerializableSkinsData> dataList)
    {
        // JSON Creation
        SerializableList<SerializableSkinsData> listObj = new SerializableList<SerializableSkinsData>(dataList);
        string json = JsonUtility.ToJson(listObj);
        CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "JSON Created: " + json);

        // Save json string to file
        if (File.Exists(@"" + Application.persistentDataPath + _skinsDataListFileName))
        {
            File.Delete(@"" + Application.persistentDataPath + _skinsDataListFileName);
        }
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + _skinsDataListFileName);
        writer.WriteLine(json);
        writer.Close();
        CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "JSON Saved");
    }

    public List<SerializableSkinsData> LoadSkinsData()
    {
        // Read from file the json string
        if (!File.Exists(@"" + Application.persistentDataPath + _skinsDataListFileName))
            return null;
        StreamReader reader = new StreamReader(Application.persistentDataPath + _skinsDataListFileName);
        string json = reader.ReadToEnd();
        CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "JSON Read: " + json);
        reader.Close();
        return JsonUtility.FromJson<SerializableList<SerializableSkinsData>>(json).serializableList;
    }

    public void SaveSetSkinData(SerializableSkinSetData data)
    {
        // JSON Creation
        string json = JsonUtility.ToJson(data);
        CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "JSON Created: " + json);

        // Save json string to file
        if (File.Exists(@"" + Application.persistentDataPath + _skinSetDataListFileName))
        {
            File.Delete(@"" + Application.persistentDataPath + _skinSetDataListFileName);
        }
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + _skinSetDataListFileName);
        writer.WriteLine(json);
        writer.Close();
        CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "JSON Saved");
    }

    public SerializableSkinSetData LoadSkinSetData()
    {
        // Read from file the json string
        if (!File.Exists(@"" + Application.persistentDataPath + _skinSetDataListFileName))
            return null;
        StreamReader reader = new StreamReader(Application.persistentDataPath + _skinSetDataListFileName);
        string json = reader.ReadToEnd();
        CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "JSON Read: " + json);
        reader.Close();
        return JsonUtility.FromJson<SerializableSkinSetData>(json);
    }

    public void SaveGearForAverage(int gear)
    {
        StreamWriter writer = null;
        if (!File.Exists(@"" + Application.persistentDataPath + _avgGearRunFileName))
        {
            writer = new StreamWriter(Application.persistentDataPath + _avgGearRunFileName);
            writer.WriteLine("GEARS;;MEDIA;=MEDIA(A2:A999)");
            writer.Close();
        }
        writer = new StreamWriter(Application.persistentDataPath + _avgGearRunFileName, true);
        writer.WriteLine(gear);
        writer.Close();
        CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "Run's Gears (" + gear + ") saved in " + _avgGearRunFileName + " for AVG count");
    }
}

[System.Serializable]
public class SerializablePowerUpData
{
    public int ID;
    public int CurrentLevel;
    public float CurrentStat;
    public float NextStat;
    public int GearCost;
}

[System.Serializable]
public class SerializableSkinsData
{
    public int ID;
    public bool IsUnlocked;
}

[System.Serializable] 
public class SerializableSkinSetData
{
    public int ID;
}