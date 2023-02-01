using System.Collections.Generic;
using UnityEngine;

public class SetRandomColor : MonoBehaviour
{
    [SerializeField] Renderer rendererComponent;
    [SerializeField] ColorPicker colors;

    public Color Current { get; private set; }

    private static readonly Dictionary<Color, MaterialPropertyBlock> colorBlocksDictionary = new();

    private void OnEnable()
    {
        SetColor();
    }

    public void SetColor()
    {
        Current = colors.GetColor();
        rendererComponent.SetPropertyBlock(GetPropertyBlock(Current));
    }

    private MaterialPropertyBlock GetPropertyBlock(Color color)
    {
        if(colorBlocksDictionary.ContainsKey(color))
        {
            return colorBlocksDictionary[color];
        }
        else
        {
            var block = new MaterialPropertyBlock();
            block.SetColor("_BaseColor", color);
            block.SetColor("_Color", color);
            colorBlocksDictionary.Add(color, block);
            return block;
        }
    }

}
