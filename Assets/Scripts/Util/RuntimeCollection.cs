using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeCollection<T> : ScriptableObject
{
    private readonly List<T> collection = new();
    public Action onCollectionChange;
    public int Count => collection.Count;

    public T GetItem(int index)
    {
        if (index > Count)
            return default;
        return collection[index];
    }

    public void Add(T obj)
    {
        collection.Add(obj);
        onCollectionChange?.Invoke();
    }

    public bool Remove(T obj)
    {
        var removed = collection.Remove(obj);
        if (removed)
        {
            onCollectionChange?.Invoke();
            return true;
        }
        return false;
    }
}
