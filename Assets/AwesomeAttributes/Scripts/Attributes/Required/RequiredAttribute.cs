using UnityEditor;
using UnityEngine;

public class RequiredAttribute : PropertyAttribute
{
    public readonly string Message;
    public readonly MessageType MessageType;

    public RequiredAttribute() 
    {
        MessageType = MessageType.Error;
    }

    public RequiredAttribute(string message) 
    {
        Message = message;
        MessageType = MessageType.Error;
    }

    public RequiredAttribute(MessageType messageType)
    {
        MessageType = messageType;
    }

    public RequiredAttribute(string message, MessageType messageType)
    {
        Message = message;
        MessageType = messageType;
    }
}