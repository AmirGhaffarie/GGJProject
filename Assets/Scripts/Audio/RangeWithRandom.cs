using System;

[Serializable]
public struct RangeWithRandom
{
    public bool random;
    public float min;
    public float max;
    public float Minimum;
    public float Maximum;
    public RangeWithRandom(float min, float max, float defaultValue)
    {
        Minimum = min;
        Maximum = max;
        this.min = this.max = defaultValue;
        random = false;
    }
    public float Value
    {
        get
        {
            if (random)
                return UnityEngine.Random.Range(min, max);
            else
                return min;
        }
    }
}