using System.Collections.Generic;
using UnityEngine;

public static class UnityExtensions
{
    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    public static T GetRandomItem<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}