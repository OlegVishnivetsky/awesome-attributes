using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{
    /// <summary>
    /// Custom property drawer for lable attribute
    /// </summary>
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            LabelAttribute labelAttribute = attribute as LabelAttribute;
            GUIContent newLabel = new GUIContent(labelAttribute.Lable);
            EditorGUI.PropertyField(position, property, newLabel);
        }
    }
}