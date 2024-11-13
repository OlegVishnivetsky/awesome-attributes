using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{
    /// <summary>
    /// Property drawer for ReadonlyIf attribute
    /// </summary>
    [CustomPropertyDrawer(typeof(ReadonlyIfAttribute))]
    public class ReadonlyIfAttributeDrawer : ConditionalAttributeDrawerBase
    {
        protected override void DrawProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            if (isPropertyShown)
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property, label);
                GUI.enabled = true;
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }
    }
}