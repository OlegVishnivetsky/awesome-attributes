using UnityEngine;

namespace AwesomeAttributes
{
    /// <summary>
    /// Attribute that creates a warning box if the field is null  
    /// </summary>
    public class RequiredAttribute : PropertyAttribute
    {
        public readonly string Message;
        public readonly RequiredMessageType MessageType;

        public RequiredAttribute()
        {
            MessageType = RequiredMessageType.Error;
        }

        public RequiredAttribute(string message)
        {
            Message = message;
            MessageType = RequiredMessageType.Error;
        }

        public RequiredAttribute(RequiredMessageType messageType)
        {
            MessageType = messageType;
        }

        public RequiredAttribute(string message, RequiredMessageType messageType)
        {
            Message = message;
            MessageType = messageType;
        }
    }
}