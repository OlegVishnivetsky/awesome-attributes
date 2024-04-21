using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadonlyIfAttribute))]
public class ReadonlyIfAttributeDrawer : ShowIfAttributeDrawerBase
{
    protected override void DrawProperty(Rect position, SerializedProperty property, GUIContent label)
    {
        if (isPropertyShown)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }
    }
}