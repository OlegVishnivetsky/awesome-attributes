using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class AttributesHelper
{
    public static string SplitCamelCase(string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", 
            System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
    }

    public static object DrawPropertyByType(Rect position, SerializedPropertyType propertyType,
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

    public static FieldInfo GetFieldInfo(string fieldName, SerializedProperty property, 
        out UnityEngine.Object targetObject)
    {
        targetObject = property.serializedObject.targetObject;
        FieldInfo fieldInfoToReturn = targetObject.GetType()
            .GetField(fieldName,
            BindingFlags.Instance 
           | BindingFlags.Public 
           | BindingFlags.NonPublic);

        return fieldInfoToReturn;
    }

    public static FieldInfo GetFieldInfo(string fieldName, UnityEngine.Object targetObject)
    {
        FieldInfo fieldInfoToReturn = targetObject.GetType()
            .GetField(fieldName,
              BindingFlags.Instance 
            | BindingFlags.Public 
            | BindingFlags.NonPublic 
            | BindingFlags.Static 
            | BindingFlags.DeclaredOnly);

        return fieldInfoToReturn;
    }

    public static PropertyInfo GetPropertyInfo(string propertyName, UnityEngine.Object targetObject)
    {
        PropertyInfo propertyInfoToReturn = targetObject.GetType()
            .GetProperty(propertyName,
              BindingFlags.Instance
            | BindingFlags.Public
            | BindingFlags.NonPublic
            | BindingFlags.Static
            | BindingFlags.DeclaredOnly);

        return propertyInfoToReturn;
    }

    public static MethodInfo GetMethodInfo(string methodName, UnityEngine.Object targetObject)
    {
        MethodInfo methodInfoToReturn = targetObject.GetType()
            .GetMethod(methodName,
              BindingFlags.Instance
            | BindingFlags.Public
            | BindingFlags.NonPublic
            | BindingFlags.Static
            | BindingFlags.DeclaredOnly);

        return methodInfoToReturn;
    }

    public static UnityEngine.Object GetTargetObject(SerializedProperty property)
    {
        UnityEngine.Object targetObjectToReturn = property.serializedObject.targetObject;

        return targetObjectToReturn;
    }

    public static object GetMethodReturnValue(MethodInfo methodInfo, UnityEngine.Object targetObject, 
        object[] parameters = null)
    {
        return methodInfo.Invoke(targetObject, parameters);
    }
}