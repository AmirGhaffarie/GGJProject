using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/RandomColorPicker")]
public class ColorPicker : ScriptableObject
{
    [SerializeField] Color[] Colors;
    public Color GetColor()
    {
        if (Colors.Length == 0)
            return Color.black;
        return Colors[Random.Range(0, Colors.Length)];
    }
}
