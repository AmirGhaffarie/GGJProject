using System;
using UnityEngine;

[Serializable]
public struct ClipWithChance
{
    public ClipSource clipSource;
    [Range(0, 1)] public float chance;
}
