using UnityEngine;

public class RequiredAttribute : PropertyAttribute
{
    public readonly string Message;

    public RequiredAttribute() { }

    public RequiredAttribute(string message) 
    {
        Message = message;
    }
}