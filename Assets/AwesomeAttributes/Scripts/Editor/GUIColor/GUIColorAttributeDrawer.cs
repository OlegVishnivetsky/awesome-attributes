using UnityEditor;
using UnityEngine;

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