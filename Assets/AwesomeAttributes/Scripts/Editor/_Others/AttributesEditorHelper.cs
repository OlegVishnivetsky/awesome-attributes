using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{
    /// <summary>
    /// Class that contains useful methods for attribute drawers
    /// </summary>
    public static class AttributesEditorHelper
    {
        /// <summary>
        /// Draws a property field based on the property type
        /// </summary>
        /// <param name="position"></param>
        /// <param name="propertyType"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="label"></param>
        /// <returns>The drawn property value</returns>
        /// <exception cref="NotImplementedException"></exception>
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

        /// <summary>
        /// Returns the field info with the given field name, serialized property and target object
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="property"></param>
        /// <param name="targetObject"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the field info with the given field name and target object
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="targetObject"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the property info with the given property name and target object
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="targetObject"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the method info with the given method name and target object
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="targetObject"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Retrieves the target object from a serialized property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static UnityEngine.Object GetTargetObject(SerializedProperty property)
        {
            UnityEngine.Object targetObjectToReturn = property.serializedObject.targetObject;

            return targetObjectToReturn;
        }

        /// <summary>
        /// Invokes a method and returns its value
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="targetObject"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object GetMethodReturnValue(MethodInfo methodInfo, UnityEngine.Object targetObject,
            object[] parameters = null)
        {
            return methodInfo.Invoke(targetObject, parameters);
        }
    }
}