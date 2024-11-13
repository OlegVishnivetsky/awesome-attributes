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
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (isPropertyShown)
            {
                return base.GetPropertyHeight(property, label);
            }
            else
            {
                return 0f;
            }
        }
    }
}