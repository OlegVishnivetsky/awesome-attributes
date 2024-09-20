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
            LabelAttribute lableAttribute = attribute as LabelAttribute;
            GUIContent newLable = new GUIContent(lableAttribute.Lable);

            EditorGUI.PropertyField(position, property, newLable);
        }
    }
}