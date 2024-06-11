using UnityEngine;
using UnityEditor;
using NUnit.Framework.Constraints;

[CustomPropertyDrawer(typeof(RequiredAttribute))]
public class RequiredAttributeDrawer : PropertyDrawer
{
    private Rect propertyRect;
    private float totalHeight;

    private const float HelpBoxHeight = 37f;
    private const float SpaceBetweenHelpBox = 2f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        propertyRect = new Rect(position.x, position.y, 
            position.width, EditorGUI.GetPropertyHeight(property));

        HandlePropertyFieldDrawing(property, label);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    { 
        return totalHeight; 
    }

    private void HandlePropertyFieldDrawing(SerializedProperty property,
        GUIContent label)
    {
        RequiredAttribute requiredAttribute = attribute as RequiredAttribute;

        if (!property.objectReferenceValue)
        {
            DrawFieldWithHelpBox(property, label, requiredAttribute);
        }
        else
        {
            DrawFieldWithOutHelpBox(property, label);
        }
    }

    private void DrawFieldWithHelpBox(SerializedProperty property, 
        GUIContent label, RequiredAttribute requiredAttribute)
    {
        Rect helpBoxRect = new Rect(propertyRect.xMin, propertyRect.yMax + SpaceBetweenHelpBox, 
            propertyRect.width, HelpBoxHeight);
        EditorGUI.PropertyField(propertyRect, property, label);

        totalHeight = propertyRect.height + HelpBoxHeight;

        if (requiredAttribute.Message == null)
        {
            EditorGUI.HelpBox(helpBoxRect, $"Field \"{property.name}\" is required", 
                requiredAttribute.MessageType);
        }
        else
        {
            EditorGUI.HelpBox(helpBoxRect, requiredAttribute.Message,
                requiredAttribute.MessageType);
        }
    }

    private void DrawFieldWithOutHelpBox(SerializedProperty property,
        GUIContent label)
    {
        totalHeight = 18f;
        EditorGUI.PropertyField(propertyRect, property, label);
    }
}