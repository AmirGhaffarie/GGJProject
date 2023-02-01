using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Variables/Float")]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] public float startingValue;
    [NonSerialized] private float value;

    public Action<float> onChange;

    public float Value
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

    public void Set(float value)
    {
        this.value = value;
        onChange?.Invoke(Value);
    }
    public void Add(float value)
    {
        this.value += value;
        onChange?.Invoke(Value);
    }
    public void Multiply(float value)
    {
        this.value *= value;
        onChange?.Invoke(Value);
    }
}

