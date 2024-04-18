using UnityEngine;

public class MinMaxSliderAttribute : PropertyAttribute
{
    public readonly float MinValue;
    public readonly float MaxValue;

    public MinMaxSliderAttribute(float minValue, float maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }
}