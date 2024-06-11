using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(OnlyChildGameObjectsAttribute))]
public class OnlyChildGameObjectsAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.ObjectReference)
        {
            EditorGUI.PropertyField(position, property, label);
            return;
        }

        EditorGUI.BeginProperty(position, label, property);

        FieldInfo fieldInfo = AttributesHelper.GetFieldInfo(property.propertyPath,
                property.serializedObject.targetObject);

        Rect objectFieldRect = new Rect(position.x, position.y, position.width - 50,
            position.height);
        Rect pickerButtonRect = new Rect(position.x + position.width - 50, position.y, 
            50, position.height);

        EditorGUI.ObjectField(objectFieldRect, property,
            fieldInfo.FieldType, label);

        if (GUI.Button(pickerButtonRect, "Pick"))
        {   
            Component targetComponent = property.serializedObject.targetObject as Component;

            if (targetComponent != null)
            {
                ChildObjectPickerWindow.ShowWindow(targetComponent.transform, 
                    fieldInfo, (pickedObject) =>
                {
                    fieldInfo.SetValue(property.serializedObject.targetObject, 
                        pickedObject.GetComponent(fieldInfo.FieldType.Name));

                    property.serializedObject.ApplyModifiedProperties();
                });
            }
        }

        EditorGUI.EndProperty();
    }
}