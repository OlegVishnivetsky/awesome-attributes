using System;
using UnityEngine;

namespace AwesomeAttributes
{
    /// <summary>
    /// Shows a button under the field to which you placed the attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class ButtonAttribute : PropertyAttribute
    {
        public readonly string MethodName;
        public readonly string Lable;
        public readonly float Height;

        public ButtonAttribute(string methodName, string lable = null, float height = 22)
        {
            if (lable == null)
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
}