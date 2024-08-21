using System;
using UnityEngine;

namespace AwesomeAttributes
{
    /// <summary>
    /// Changes the field name in the inspector, useful for long names
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class LabelAttribute : PropertyAttribute
    {
        public readonly string Lable;

        public LabelAttribute(string lable)
        {
            Lable = lable;
        }
    }
}