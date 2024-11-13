using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{
    /// <summary>
    /// Property drawer for gui color attribute
    /// </summary>
    [CustomPropertyDrawer(typeof(GUIColorAttribute))]
    public class GUIColorAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUIColorAttribute guiColorAttribute = attribute as GUIColorAttribute;

            GUI.color = guiColorAttribute.Color;
            EditorGUI.PropertyField(position, property, label);
        }
    }
}