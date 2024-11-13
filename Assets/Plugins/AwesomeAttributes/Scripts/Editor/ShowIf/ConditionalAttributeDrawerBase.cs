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
                    DrawShowIfMultipleConditionsType(property, showIfAttribute);
                    break;
                case ShowIfAttributeType.EnumCondition:
                    DrawShowIfEnumConditionType(property, showIfAttribute);
                    break;

                default:
                    break;
            }

            DrawProperty(position, property, label);
        }

        protected abstract void DrawProperty(Rect position, SerializedProperty property, GUIContent label);

        private void HandleShowIfAttributeType(ShowIfAttributeType showIfAttributeType, ConditionalAttributeBase
            showIfAttribute, Rect position, SerializedProperty property, GUIContent label)
        {
            switch (showIfAttributeType)
            {
                case ShowIfAttributeType.OneCondition:
                    DrawShowIfOneConditionType(position, property, label, showIfAttribute);
                    break;
                case ShowIfAttributeType.MultipleConditions:
                    DrawShowIfMultipleConditionsType(property, showIfAttribute);
                    break;
                case ShowIfAttributeType.EnumCondition:
                    DrawShowIfEnumConditionType(property, showIfAttribute);
                    break;

                default:
                    break;
            }
        }

        #region [ShowIf] One Condition Type

        private void DrawShowIfOneConditionType(Rect position, SerializedProperty property,
            GUIContent label, ConditionalAttributeBase showIfAttribute)
        {
            Object targetObject = AttributesEditorHelper.GetTargetObject(property);

            FieldInfo conditionFieldInfo =
                AttributesEditorHelper.GetFieldInfo(showIfAttribute.Conditions[0], targetObject);
            MethodInfo conditionMethodInfo =
                AttributesEditorHelper.GetMethodInfo(showIfAttribute.Conditions[0], targetObject);

            DrawShowIfPropertyWithFieldCondition(property, label, showIfAttribute, targetObject,
                conditionFieldInfo, conditionMethodInfo);
            DrawShowIfPropertyWithMethodCondition(position, property, label, showIfAttribute, targetObject,
                conditionFieldInfo, conditionMethodInfo);
        }

        private void DrawShowIfPropertyWithMethodCondition(Rect position, SerializedProperty property, GUIContent label,
            ConditionalAttributeBase showIfAttribute, Object targetObject,
            FieldInfo conditionFieldInfo, MethodInfo conditionMethodInfo)
        {
            if (IsMethodReturnBoolType(conditionMethodInfo))
            {
                isPropertyShown = (bool)AttributesEditorHelper.GetMethodReturnValue(conditionMethodInfo, targetObject);
            }
            else if (conditionFieldInfo == null
                && conditionMethodInfo != null
                && conditionMethodInfo.ReturnType != typeof(bool))
            {
                AttributesLogger.LogMethodReturnTypeWarning(showIfAttribute.ToString(),
                    showIfAttribute.Conditions[0]);
            }
        }

        private void DrawShowIfPropertyWithFieldCondition(SerializedProperty property, GUIContent label,
            ConditionalAttributeBase showIfAttribute, Object targetObject, FieldInfo conditionFieldInfo,
            MethodInfo conditionMethodInfo)
        {
            if (IsFieldBoolType(conditionFieldInfo))
            {
                isPropertyShown = (bool)conditionFieldInfo.GetValue(targetObject);
            }
            else if (conditionFieldInfo != null
                && conditionMethodInfo == null
                && conditionFieldInfo.FieldType != typeof(bool))
            {
                AttributesLogger.LogFieldTypeWarning(showIfAttribute.ToString(),
                    showIfAttribute.Conditions[0]);
            }
        }
        #endregion [ShowIf] One Condition Type

        #region [ShowIf] Multiple Condition Type
        private void DrawShowIfMultipleConditionsType(SerializedProperty property, ConditionalAttributeBase showIfAttribute)
        {
            List<bool> conditionsList = GetAllConitions(AttributesEditorHelper.GetTargetObject(property),
                showIfAttribute.Conditions);

            if (conditionsList.Count > 0)
            {
                switch (showIfAttribute.ConditionsOperator)
                {
                    case "&&":
                        if (!conditionsList.Any(conditionValue => conditionValue == true) ||
                            !conditionsList.Any(conditionValue => conditionValue == false))
                        {
                            isPropertyShown = true;
                        }
                        else
                        {
                            isPropertyShown = false;
                        }
                        break;
                    case "||":
                        if (conditionsList.Any(conditionValue => conditionValue == true) &&
                            conditionsList.Any(conditionValue => conditionValue == false))
                        {
                            isPropertyShown = true;
                        }
                        else
                        {
                            isPropertyShown = false;
                        }
                        break;
                }
            }
        }

        private List<bool> GetAllConitions(Object targetObject, string[] conditions)
        {
            List<bool> conditionsToReturn = new List<bool>();

            for (int i = 0; i < conditions.Length; i++)
            {
                FieldInfo conditionFieldInfo = AttributesEditorHelper.GetFieldInfo(conditions[i], targetObject);

                if (IsFieldBoolType(conditionFieldInfo))
                {
                    conditionsToReturn.Add((bool)conditionFieldInfo.GetValue(targetObject));
                }

                MethodInfo conditionMethodInfo = AttributesEditorHelper.GetMethodInfo(conditions[i], targetObject);

                if (IsMethodReturnBoolType(conditionMethodInfo))
                {
                    conditionsToReturn.Add((bool)AttributesEditorHelper
                        .GetMethodReturnValue(conditionMethodInfo, targetObject));
                }

                PropertyInfo conditionPropertyInfo = AttributesEditorHelper
                    .GetPropertyInfo(conditions[i], targetObject);

                if (IsPropertyBoolType(conditionPropertyInfo))
                {
                    conditionsToReturn.Add((bool)conditionPropertyInfo.GetValue(targetObject));
                }
            }

            return conditionsToReturn;
        }
        #endregion [ShowIf] One Condition Type

        #region [ShowIf] Enum Condition Type

        public void DrawShowIfEnumConditionType(SerializedProperty property,
            ConditionalAttributeBase showIfAttribute)
        {
            Object targetObject = AttributesEditorHelper.GetTargetObject(property);
            System.Enum enumValue = GetEnumValueByName(targetObject, showIfAttribute.EnumFieldName);

            if (enumValue.GetType().GetCustomAttribute<System.FlagsAttribute>() == null)
            {
                isPropertyShown = showIfAttribute.EnumValue.Equals(enumValue);
            }
            else
            {
                isPropertyShown = enumValue.HasFlag((System.Enum)showIfAttribute.EnumValue);
            }
        }

        private System.Enum GetEnumValueByName(Object targetObject, string enumFieldName)
        {
            FieldInfo enumFieldInfo = AttributesEditorHelper.GetFieldInfo(enumFieldName, targetObject);

            if (enumFieldInfo != null && enumFieldInfo.FieldType.IsSubclassOf(typeof(System.Enum)))
            {
                return (System.Enum)enumFieldInfo.GetValue(targetObject);
            }

            MethodInfo enumMethodInfo = AttributesEditorHelper.GetMethodInfo(enumFieldName, targetObject);

            if (enumMethodInfo != null && enumMethodInfo.ReturnType.IsSubclassOf(typeof(System.Enum)))
            {
                return (System.Enum)enumMethodInfo.Invoke(targetObject, null);
            }

            PropertyInfo enumPropertyInfo = AttributesEditorHelper.GetPropertyInfo(enumFieldName, targetObject);

            if (enumPropertyInfo != null && enumPropertyInfo.PropertyType.IsSubclassOf(typeof(System.Enum)))
            {
                return (System.Enum)enumPropertyInfo.GetValue(targetObject);
            }

            return null;
        }

        #endregion [ShowIf] Enum Condition Type

        private bool IsFieldBoolType(FieldInfo fieldInfo)
        {
            return fieldInfo != null && fieldInfo.FieldType == typeof(bool);
        }

        private bool IsMethodReturnBoolType(MethodInfo methodInfo)
        {
            return methodInfo != null && methodInfo.ReturnType == typeof(bool);
        }

        private bool IsPropertyBoolType(PropertyInfo propertyInfo)
        {
            return propertyInfo != null && propertyInfo.PropertyType == typeof(bool);
        }
    }
}