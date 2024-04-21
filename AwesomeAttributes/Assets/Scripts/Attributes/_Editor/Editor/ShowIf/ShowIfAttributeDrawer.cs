using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class ShowIfAttributeDrawer : ShowIfAttributeDrawerBase
{
    protected override void DrawProperty(Rect position, SerializedProperty property, GUIContent label)
    {
        if (isPropertyShown)
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}