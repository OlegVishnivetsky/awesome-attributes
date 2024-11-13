using System;
using UnityEngine;

namespace AwesomeAttributes
{
    /// <summary>
    /// Allows selecting assets from the Resources folder and stores the path for Resources.Load.
    /// Also restricts selection to assets within the Resources folder 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ResourcesPathAttribute : PropertyAttribute
    {
    }
}