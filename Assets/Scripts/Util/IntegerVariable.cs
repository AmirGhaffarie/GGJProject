using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Variables/Integer")]
public class IntegerVariable : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] public int startingValue;
    [NonSerialized] private int value;

    public Action<int> onChange;

    public int Value
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

    public void Set(int value)
    {
        this.value = value;
        onChange?.Invoke(Value);
    }
    public void Add(int value)
    {
        this.value += value;
        onChange?.Invoke(Value);
    }
    public void Multiply(int value)
    {
        this.value *= value;
        onChange?.Invoke(Value);
    }
    public void SetMin(int value)
    {
        this.value = Mathf.Max(value, this.value);
        onChange?.Invoke(Value);
    }
    public void SetMax(int value)
    {
        this.value = Mathf.Min(value, this.value);
        onChange?.Invoke(Value);
    }
}
