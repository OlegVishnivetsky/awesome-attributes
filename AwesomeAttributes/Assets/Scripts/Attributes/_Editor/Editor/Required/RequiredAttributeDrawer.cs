using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RequiredAttribute))]
public class RequiredAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        HandlePropertyFieldDrawing(position, property, label);
    }

    private void HandlePropertyFieldDrawing(Rect position, SerializedProperty property,
        GUIContent label)
    {
        RequiredAttribute requiredAttribute = attribute as RequiredAttribute;

        if (!property.objectReferenceValue)
        {
            DrawFieldWithHelpBox(position, property, label, requiredAttribute);
        }
        else
        {
            DrawFieldWithOutHelpBox(position, property, label);
        }
    }

    private void DrawFieldWithHelpBox(Rect position, SerializedProperty property, 
        GUIContent label, RequiredAttribute requiredAttribute)
    {
        EditorGUILayout.BeginVertical();
        EditorGUI.PropertyField(position, property, label);

        if (requiredAttribute.Message == null)
        {
            EditorGUILayout.HelpBox($"Field \"{property.name}\" is required", MessageType.Error);
        }
        else
        {
            EditorGUILayout.HelpBox(requiredAttribute.Message, MessageType.Error);
        }

        EditorGUILayout.EndVertical();
    }

    private void DrawFieldWithOutHelpBox(Rect position, SerializedProperty property,
        GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label);
    }
}