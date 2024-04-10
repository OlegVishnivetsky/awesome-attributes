using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowPropertyAttribute))]
public class ShowPropertyAttributeDrawer : PropertyDrawer
{
    private PropertyInfo propertyFieldInfo = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        UnityEngine.Object target = property.serializedObject.targetObject;

        if (propertyFieldInfo == null)
            propertyFieldInfo = target.GetType()
                .GetProperty(((ShowPropertyAttribute)attribute).PropertyName,
                                                 BindingFlags.Instance 
                                                 | BindingFlags.Public 
                                                 | BindingFlags.NonPublic);

        if (propertyFieldInfo != null)
        {
            object value = propertyFieldInfo.GetValue(target, null);

            EditorGUI.BeginChangeCheck();

            value = DrawProperty(position, property.propertyType, propertyFieldInfo.PropertyType, value, label);

            if (EditorGUI.EndChangeCheck() && propertyFieldInfo != null)
            {
                Undo.RecordObject(target, "Inspector");
                propertyFieldInfo.SetValue(target, value, null);
            }

        }
        else
        {
            EditorGUI.LabelField(position, "Error: could not retrieve property.");
        }
    }

    private object DrawProperty(Rect position, SerializedPropertyType propertyType, 
        Type type, object value, GUIContent label)
    {
        switch (propertyType)
        {
            case SerializedPropertyType.Integer:
                return EditorGUI.IntField(position, label, (int)value);
            case SerializedPropertyType.Boolean:
                return EditorGUI.Toggle(position, label, (bool)value);
            case SerializedPropertyType.Float:
                return EditorGUI.FloatField(position, label, (float)value);
            case SerializedPropertyType.String:
                return EditorGUI.TextField(position, label, (string)value);
            case SerializedPropertyType.Color:
                return EditorGUI.ColorField(position, label, (Color)value);
            case SerializedPropertyType.ObjectReference:
                return EditorGUI.ObjectField(position, label, 
                    (UnityEngine.Object)value, type, true);
            case SerializedPropertyType.ExposedReference:
                return EditorGUI.ObjectField(position, label, 
                    (UnityEngine.Object)value, type, true);
            case SerializedPropertyType.LayerMask:
                return EditorGUI.LayerField(position, label, (int)value);
            case SerializedPropertyType.Enum:
                return EditorGUI.EnumPopup(position, label, (Enum)value);
            case SerializedPropertyType.Vector2:
                return EditorGUI.Vector2Field(position, label, (Vector2)value);
            case SerializedPropertyType.Vector3:
                return EditorGUI.Vector3Field(position, label, (Vector3)value);
            case SerializedPropertyType.Vector4:
                return EditorGUI.Vector4Field(position, label, (Vector4)value);
            case SerializedPropertyType.Rect:
                return EditorGUI.RectField(position, label, (Rect)value);
            case SerializedPropertyType.AnimationCurve:
                return EditorGUI.CurveField(position, label, (AnimationCurve)value);
            case SerializedPropertyType.Bounds:
                return EditorGUI.BoundsField(position, label, (Bounds)value);
            default:
                throw new NotImplementedException("Unimplemented propertyType " + propertyType + ".");
        }
    }
}