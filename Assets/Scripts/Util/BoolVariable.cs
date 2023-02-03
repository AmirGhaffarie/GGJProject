using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Variables/Boolean")]
public class BoolVariable : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] public bool startingValue;
    [NonSerialized] private bool value;

    public Action<bool> onChange;

    public bool Value
    {
        set
        {
            Set(value);
        }
        get
        {
            return value;
        }
    }

    public void Reset()
    {
        value = startingValue;
    }

    public void OnAfterDeserialize()
    {
        value = startingValue;
    }

    public void OnBeforeSerialize()
    {
    }

    public void Set(bool value)
    {
        this.value = value;
        onChange?.Invoke(Value);
    }

    public static implicit operator bool(BoolVariable bv) => bv.Value;
}

