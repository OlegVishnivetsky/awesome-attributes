using System;
using UnityEngine;

/// <summary>
/// Allows you to show properties in the inspector
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class ShowPropertyAttribute : PropertyAttribute
{
    public readonly string PropertyName;

    public ShowPropertyAttribute(string propertyName)
    {
        PropertyName = propertyName;
    }
}