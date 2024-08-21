using UnityEngine;

namespace AwesomeAttributes
{
    /// <summary>
    /// Creates special slider the user can use to specify a range between a min and a max
    /// </summary>
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
}