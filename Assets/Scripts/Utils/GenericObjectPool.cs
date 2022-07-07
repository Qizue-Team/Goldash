using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{
    public T LastObject { get; private set; }

    [SerializeField] private T prefab;
    public static GenericObjectPool<T> Instance { get; private set; } // Singleton instance
    private Queue<T> objects = new Queue<T>();

    private void Awake()
    {
        // Singleton Instance
        Instance = this;
    }

    public T Get()
    {
        if (objects.Count == 0)
        {
            AddObject(1);
        }
        LastObject = objects.Dequeue();
        return LastObject;
    }

    public void ReturnToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        objects.Enqueue(objectToReturn);
    }

    private void AddObject(int count)
    {
        var newObject = GameObject.Instantiate(prefab);
        newObject.gameObject.SetActive(false);
        objects.Enqueue(newObject);
    }
}
