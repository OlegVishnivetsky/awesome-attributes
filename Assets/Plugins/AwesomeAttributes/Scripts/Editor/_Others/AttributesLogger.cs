using UnityEngine;

namespace AwesomeAttributes.Editor
{
    public static class AttributesLogger
    {
        public static void LogFieldTypeWarning(string attributeName, string fieldName = null)
        {
            if (fieldName == null)
            {
                Debug.LogWarning($"[{attributeName}]: You specified a field with " +
                    $"the wrong type for this attribute");
            }
            else
            {
                Debug.LogWarning($"[{attributeName}]: You specified a field ({fieldName}) with " +
                    $"the wrong type for this attribute");
            }
        }

        public static void LogMethodReturnTypeWarning(string attributeName, string methodName = null)
        {
            if (methodName == null)
            {
                Debug.LogWarning($"[{attributeName}]: You specified a method with " +
                    $"the wrong return type for this attribute");
            }
            else
            {
                Debug.LogWarning($"[{attributeName}]: You specified a method ({methodName}) with " +
                    $"the wrong return type for this attribute");
            }
        }
    }
}