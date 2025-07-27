using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{
    /// <summary>
    /// Property drawer for ShowIf attribute
    /// </summary>
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfAttributeDrawer : ConditionalAttributeDrawerBase
    {
        protected override void DrawProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            if (isPropertyShown)
                EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (isPropertyShown)
                return EditorGUI.GetPropertyHeight(property, label,true);
            else
                return 0f;
        }
    }
}