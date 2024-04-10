using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LableAttribute))]
public class LableAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        LableAttribute lableAttribute = attribute as LableAttribute;
        GUIContent newLable = new GUIContent(lableAttribute.Lable);
        
        EditorGUI.PropertyField(position, property, newLable);
    }
}