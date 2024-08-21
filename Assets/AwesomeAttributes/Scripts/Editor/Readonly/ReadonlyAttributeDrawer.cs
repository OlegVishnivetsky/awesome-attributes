using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes
{
    /// <summary>
    /// Property drawer for Readonly attribute
    /// </summary>
    [CustomPropertyDrawer(typeof(ReadonlyAttribute))]
    public class ReadonlyAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawReadonlyField(position, property, label);
        }

        /// <summary>
        /// Draw readonly field
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        private static void DrawReadonlyField(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }
    }
}