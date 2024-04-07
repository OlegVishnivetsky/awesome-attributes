using System;

[AttributeUsage(AttributeTargets.Method)]
public class ButtonAttribute : Attribute
{
    public readonly string MethodName;

    public ButtonAttribute(string methodName) 
    {
        MethodName = methodName;
    }
}