using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class ShowPropertyAttribute : PropertyAttribute
{
    public readonly string PropertyName;

    public ShowPropertyAttribute(string propertyName)
    {
        PropertyName = propertyName;
    }
}