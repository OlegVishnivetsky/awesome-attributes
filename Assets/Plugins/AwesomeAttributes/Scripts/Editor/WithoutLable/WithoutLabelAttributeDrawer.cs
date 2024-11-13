using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{
    /// <summary>
    /// Property drawer for WithoutLabel attribute
    /// </summary>
    [CustomPropertyDrawer(typeof(WithoutLabelAttribute))]
    public class WithoutLabelAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawPropertyWithoutLabel(position, property);
        }

        /// <summary>
        /// Draws property with empty label
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        private static void DrawPropertyWithoutLabel(Rect position, SerializedProperty property)
        {
            GUIContent newLabel = new GUIContent("");

            EditorGUI.PropertyField(position, property, newLabel);
        }
    }
}