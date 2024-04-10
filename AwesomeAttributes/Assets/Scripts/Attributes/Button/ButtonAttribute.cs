using System;
using System.Reflection;
using UnityEngine;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
public class ButtonAttribute : PropertyAttribute
{
    public readonly string MethodName;
    public readonly string Lable;
    public readonly float Height;

    public ButtonAttribute(string methodName, string lable = null, float height = 18) 
    {
        if (lable  == null)
        {
            Lable = AttributesHelper.SplitCamelCase(methodName);
        }
        else
        {
            Lable = lable;
        }

        Height = height;
        MethodName = methodName;
    }
}