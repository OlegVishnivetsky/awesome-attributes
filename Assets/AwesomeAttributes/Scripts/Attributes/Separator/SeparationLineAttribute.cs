using System;
using UnityEngine;

/// <summary>
/// Draws a separator line with height and spacing
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class SeparationLineAttribute : PropertyAttribute
{
    [Header("")]
    public readonly float Spacing;
    public readonly float Height;
    public readonly Color LineColor;

    /// <summary>
    /// Constructor with height and optional spacing parameters
    /// </summary>
    /// <param name="height"></param>
    /// <param name="spacing"></param>
    public SeparationLineAttribute(float height, float spacing = 1)
    {
        Spacing = spacing;
        Height = height;
    }
}