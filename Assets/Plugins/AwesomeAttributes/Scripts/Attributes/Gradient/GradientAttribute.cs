using System;
using UnityEngine;

namespace AwesomeAttributes
{
    /// <summary>
    /// Allows editing Gradient fields directly in the Inspector
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class GradientAttribute : PropertyAttribute
    {
    }
}