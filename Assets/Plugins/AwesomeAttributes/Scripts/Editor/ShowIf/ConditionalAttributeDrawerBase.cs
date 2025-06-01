using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{
    public abstract class ConditionalAttributeDrawerBase : PropertyDrawer
    {
        protected bool isPropertyShown;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ConditionalAttributeBase showIfAttribute = attribute as ConditionalAttributeBase;

            switch (showIfAttribute.ShowIfAttributeType)
            {
                case ShowIfAttributeType.OneCondition:
                    DrawShowIfOneConditionType(position, property, label, showIfAttribute);
                    break;
                case ShowIfAttributeType.MultipleConditions:
                    DrawShowIfMultipleConditionsType(position, property, label, showIfAttribute);
                    break;
                case ShowIfAttributeType.EnumCondition:
                    DrawShowIfEnumConditionType(position, property, label, showIfAttribute);
                    break;
            }

            DrawProperty(position, property, label);
        }

        protected abstract void DrawProperty(Rect position, SerializedProperty property, GUIContent label);

        #region [ShowIf] One Condition Type

        private void DrawShowIfOneConditionType(Rect position, SerializedProperty property, GUIContent label,
            ConditionalAttributeBase showIfAttribute)
        {
            SerializedProperty conditionProperty = GetRelativeProperty(property, showIfAttribute.Conditions[0]);
            if (conditionProperty != null && conditionProperty.propertyType == SerializedPropertyType.Boolean)
            {
                isPropertyShown = conditionProperty.boolValue;
            }
            else
            {
                object targetObject = GetNestedTargetObject(property);
                
                if (targetObject == null)
                {
                    Debug.LogWarning($"Could not resolve target object for condition {showIfAttribute.Conditions[0]} in {property.propertyPath}");
                    isPropertyShown = false;
                    return;
                }

                FieldInfo conditionFieldInfo = AttributesEditorHelper.GetFieldInfo(showIfAttribute.Conditions[0], targetObject as UnityEngine.Object);
                MethodInfo conditionMethodInfo = AttributesEditorHelper.GetMethodInfo(showIfAttribute.Conditions[0], targetObject as UnityEngine.Object);

                if (IsFieldBoolType(conditionFieldInfo))
                    isPropertyShown = (bool)conditionFieldInfo.GetValue(targetObject);
                
                else if (IsMethodReturnBoolType(conditionMethodInfo))
                    isPropertyShown = (bool)conditionMethodInfo.Invoke(targetObject, null);
                else
                {
                    Debug.LogWarning($"Condition {showIfAttribute.Conditions[0]} is not a valid boolean field or method for {property.propertyPath}");
                    isPropertyShown = false;
                }
            }
        }

        #endregion [ShowIf] One Condition Type

        #region [ShowIf] Multiple Condition Type

        private void DrawShowIfMultipleConditionsType(Rect position, SerializedProperty property, GUIContent label,
            ConditionalAttributeBase showIfAttribute)
        {
            List<bool> conditionsList = new List<bool>();

            foreach (string condition in showIfAttribute.Conditions)
            {
                SerializedProperty conditionProperty = GetRelativeProperty(property, condition);
                
                if (conditionProperty != null && conditionProperty.propertyType == SerializedPropertyType.Boolean)
                    conditionsList.Add(conditionProperty.boolValue);
                else
                {
                    object targetObject = GetNestedTargetObject(property);
                    
                    if (targetObject == null)
                    {
                        Debug.LogWarning($"Could not resolve target object for condition {condition} in {property.propertyPath}");
                        continue;
                    }

                    FieldInfo conditionFieldInfo = AttributesEditorHelper.GetFieldInfo(condition, targetObject as UnityEngine.Object);
                    MethodInfo conditionMethodInfo = AttributesEditorHelper.GetMethodInfo(condition, targetObject as UnityEngine.Object);
                    PropertyInfo conditionPropertyInfo = AttributesEditorHelper.GetPropertyInfo(condition, targetObject as UnityEngine.Object);

                    if (IsFieldBoolType(conditionFieldInfo))
                        conditionsList.Add((bool)conditionFieldInfo.GetValue(targetObject));
                    else if (IsMethodReturnBoolType(conditionMethodInfo))
                        conditionsList.Add((bool)conditionMethodInfo.Invoke(targetObject, null));
                    else if (IsPropertyBoolType(conditionPropertyInfo))
                        conditionsList.Add((bool)conditionPropertyInfo.GetValue(targetObject));
                    else
                        Debug.LogWarning($"Condition {condition} is not a valid boolean field, method, or property for {property.propertyPath}");
                }
            }

            if (conditionsList.Count > 0)
            {
                isPropertyShown = showIfAttribute.ConditionsOperator == "&&"
                    ? conditionsList.All(conditionValue => conditionValue)
                    : conditionsList.Any(conditionValue => conditionValue);
            }
            else
            {
                isPropertyShown = false;
            }
        }

        #endregion [ShowIf] Multiple Condition Type

        #region [ShowIf] Enum Condition Type

        private void DrawShowIfEnumConditionType(Rect position, SerializedProperty property, GUIContent label,
            ConditionalAttributeBase showIfAttribute)
        {
            SerializedProperty enumProperty = GetRelativeProperty(property, showIfAttribute.EnumFieldName);
            
            if (enumProperty != null && enumProperty.propertyType == SerializedPropertyType.Enum)
            {
                int enumValueIndex = enumProperty.enumValueIndex;
                int targetEnumValueIndex = (int)showIfAttribute.EnumValue;
                isPropertyShown = enumValueIndex == targetEnumValueIndex;
            }
            else
            {
                object targetObject = GetNestedTargetObject(property);
                
                if (targetObject == null)
                {
                    Debug.LogWarning($"Could not resolve target object for enum field {showIfAttribute.EnumFieldName} in {property.propertyPath}");
                    isPropertyShown = false;
                    return;
                }

                Enum enumValue = GetEnumValueByName(targetObject, showIfAttribute.EnumFieldName);

                if (enumValue != null)
                {
                    if (enumValue.GetType().GetCustomAttribute<FlagsAttribute>() == null)
                        isPropertyShown = showIfAttribute.EnumValue.Equals(enumValue);
                    else
                        isPropertyShown = enumValue.HasFlag((Enum)showIfAttribute.EnumValue);
                }
                else
                {
                    Debug.LogWarning($"Enum field {showIfAttribute.EnumFieldName} not found for {property.propertyPath}");
                    isPropertyShown = false;
                }
            }
        }

        private Enum GetEnumValueByName(object targetObject, string enumFieldName)
        {
            FieldInfo enumFieldInfo = AttributesEditorHelper.GetFieldInfo(enumFieldName, targetObject as UnityEngine.Object);
            
            if (enumFieldInfo != null && enumFieldInfo.FieldType.IsSubclassOf(typeof(Enum)))
                return (Enum)enumFieldInfo.GetValue(targetObject);
            
            MethodInfo enumMethodInfo = AttributesEditorHelper.GetMethodInfo(enumFieldName, targetObject as UnityEngine.Object);
            
            if (enumMethodInfo != null && enumMethodInfo.ReturnType.IsSubclassOf(typeof(Enum)))
                return (Enum)enumMethodInfo.Invoke(targetObject, null);

            PropertyInfo enumPropertyInfo = AttributesEditorHelper.GetPropertyInfo(enumFieldName, targetObject as UnityEngine.Object);
            
            if (enumPropertyInfo != null && enumPropertyInfo.PropertyType.IsSubclassOf(typeof(Enum)))
                return (Enum)enumPropertyInfo.GetValue(targetObject);
            
            return null;
        }

        #endregion [ShowIf] Enum Condition Type

        private bool IsFieldBoolType(FieldInfo fieldInfo) => fieldInfo != null && fieldInfo.FieldType == typeof(bool);

        private bool IsMethodReturnBoolType(MethodInfo methodInfo) => methodInfo != null && methodInfo.ReturnType == typeof(bool);

        private bool IsPropertyBoolType(PropertyInfo propertyInfo) => propertyInfo != null && propertyInfo.PropertyType == typeof(bool);

        private SerializedProperty GetRelativeProperty(SerializedProperty property, string conditionName)
        {
            string propertyPath = property.propertyPath;
            int lastDot = propertyPath.LastIndexOf('.');
            
            if (lastDot >= 0)
            {
                string parentPath = propertyPath.Substring(0, lastDot);
                return property.serializedObject.FindProperty($"{parentPath}.{conditionName}");
            }
            return property.serializedObject.FindProperty(conditionName);
        }

        private object GetNestedTargetObject(SerializedProperty property)
        {
            object target = property.serializedObject.targetObject;
            string[] pathParts = property.propertyPath.Split('.');

            for (int i = 0; i < pathParts.Length - 1; i++)
            {
                if (pathParts[i] == "Array" && i + 1 < pathParts.Length && pathParts[i + 1].StartsWith("data["))
                {
                    string arrayPath = string.Join(".", pathParts.Take(i));
                    int indexStart = pathParts[i + 1].IndexOf('[') + 1;
                    int indexEnd = pathParts[i + 1].IndexOf(']');
                    
                    if (int.TryParse(pathParts[i + 1].Substring(indexStart, indexEnd - indexStart), out int index))
                    {
                        target = GetArrayElementAtIndex(arrayPath, index, target, property);
                        if (target == null)
                        {
                            Debug.LogWarning($"Failed to resolve array element at index {index} for path {arrayPath}");
                            return null;
                        }
                    }
                    
                    i++;
                    continue;
                }

                FieldInfo fieldInfo = target.GetType().GetField(pathParts[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    target = fieldInfo.GetValue(target);
                    
                    if (target == null)
                    {
                        Debug.LogWarning($"Field {pathParts[i]} is null in path {property.propertyPath}");
                        return null;
                    }
                }
                else
                {
                    Debug.LogWarning($"Field {pathParts[i]} not found in type {target.GetType().Name} for path {property.propertyPath}");
                    return null;
                }
            }

            return target;
        }

        private object GetArrayElementAtIndex(string arrayPath, int index, object target, SerializedProperty property)
        {
            FieldInfo fieldInfo = target.GetType().GetField(arrayPath, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            
            if (fieldInfo != null)
                target = fieldInfo.GetValue(target);
            else
            {
                Debug.LogWarning($"Array field {arrayPath} not found in type {target.GetType().Name}");
                return null;
            }

            if (target is Array array && index < array.Length)
                return array.GetValue(index);
            
            if (target is System.Collections.IList list && index < list.Count)
                return list[index];
            
            Debug.LogWarning($"Array or list at {arrayPath} is invalid or index {index} is out of bounds");
            return null;
        }
    }
}