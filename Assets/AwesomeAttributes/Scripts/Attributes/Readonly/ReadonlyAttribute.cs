using System;
using UnityEngine;

namespace AwesomeAttributes
{
    /// <summary>
    /// Attribute class for readonly fields, they are visible in the inspector but cannot be edited
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadonlyAttribute : PropertyAttribute
    {
        public readonly ReadonlyType ReadonlyType;

        public ReadonlyAttribute(ReadonlyType readonlyType = ReadonlyType.Always)
        {
            ReadonlyType = readonlyType;
        }
    }
}