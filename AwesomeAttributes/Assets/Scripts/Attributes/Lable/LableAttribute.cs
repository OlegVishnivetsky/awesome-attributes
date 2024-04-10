using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class LableAttribute : PropertyAttribute
{
    public readonly string Lable;

    public LableAttribute(string lable)
    {
        Lable = lable;
    }
}