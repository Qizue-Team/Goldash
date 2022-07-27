using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollectedManager : Singleton<TrashCollectedManager>
{
    public Dictionary<string,int> TrashCountDictionary { get => _trashCountDictionary; }
    public Dictionary<string, Trash> TrashDictionary { get => _trashDictionary; }

    private Dictionary<string, int> _trashCountDictionary;
    private Dictionary<string, Trash> _trashDictionary;

    public void AddTrash(Trash trash)
    {
        string name = trash.Name;
        if(_trashCountDictionary.TryGetValue(name, out int count))
        {
            if (_trashCountDictionary.ContainsKey(name))
            {
                _trashCountDictionary[name] = ++count;
            }
        }
        else
        {
            _trashCountDictionary.Add(name, 1);
            _trashDictionary.Add(name, trash);
        }
    }

    public void ResetCollectedTrash()
    {
        _trashCountDictionary.Clear();
    }

    public int GetTotalGearCount()
    {
        int count = 0;
        foreach (string name in _trashDictionary.Keys)
        {
            count = count + (_trashDictionary[name].Gear * _trashCountDictionary[name]);
        }
        return count;
    }

    public int GetTotalTrashCount()
    {
        int count = 0;
        foreach (string name in _trashCountDictionary.Keys)
        {
            count += _trashCountDictionary[name];
        }
        return count;
    }

    protected override void Awake()
    {
        base.Awake();
        _trashCountDictionary = new Dictionary<string, int>();
        _trashDictionary = new Dictionary<string, Trash>();
    }
}
