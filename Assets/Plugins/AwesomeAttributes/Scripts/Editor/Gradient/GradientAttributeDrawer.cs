using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{
    [CustomPropertyDrawer(typeof(GradientAttribute))]
    public class GradientAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType == SerializedPropertyType.Gradient)
            {
                property.gradientValue = EditorGUI.GradientField(position, label, property.gradientValue);
            }

            EditorGUI.EndProperty();
        }
    }
}