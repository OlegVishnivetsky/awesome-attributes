using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class ShowIfAttributeDrawer : PropertyDrawer
{
    private bool isPropertyShown;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ShowIfAttribute showIfAttribute = attribute as ShowIfAttribute;

        switch (showIfAttribute.ShowIfAttributeType)
        {
            case ShowIfAttributeType.OneCondition:
                DrawShowIfOneConditionType(position, property, label, showIfAttribute);
                break;
            case ShowIfAttributeType.MultipleConditions:
                DrawShowIfMultipleConditionsType(property, label, showIfAttribute);
                break;
            case ShowIfAttributeType.EnumCondition:
                DrawShowIfEnumConditionType(position, property, label, showIfAttribute);
                break;

            default:
                break;
        }

        if (isPropertyShown)
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (isPropertyShown)
        {
            return base.GetPropertyHeight(property, label);
        }
        else
        {
            return 0f;
        }
    }

    #region [ShowIf] One Condition Type

    private void DrawShowIfOneConditionType(Rect position, SerializedProperty property, 
        GUIContent label, ShowIfAttribute showIfAttribute)
    {
        Object targetObject = AttributesHelper.GetTargetObject(property);

        FieldInfo conditionFieldInfo =
            AttributesHelper.GetFieldInfo(showIfAttribute.Conditions[0], targetObject);
        MethodInfo conditionMethodInfo =
            AttributesHelper.GetMethodInfo(showIfAttribute.Conditions[0], targetObject);

        DrawShowIfPropertyWithFieldCondition(property, label, showIfAttribute, targetObject,
            conditionFieldInfo, conditionMethodInfo);
        DrawShowIfPropertyWithMethodCondition(position, property, label, showIfAttribute, targetObject, 
            conditionFieldInfo, conditionMethodInfo);
    }

    private void DrawShowIfPropertyWithMethodCondition(Rect position, SerializedProperty property, GUIContent label, 
        ShowIfAttribute showIfAttribute, Object targetObject, 
        FieldInfo conditionFieldInfo, MethodInfo conditionMethodInfo)
    {
        if (IsMethodReturnBoolType(conditionMethodInfo))
        {
            isPropertyShown = (bool)AttributesHelper.GetMethodReturnValue(conditionMethodInfo, targetObject); 
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
        ShowIfAttribute showIfAttribute, Object targetObject, FieldInfo conditionFieldInfo, 
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
    private void DrawShowIfMultipleConditionsType(SerializedProperty property,
        GUIContent label, ShowIfAttribute showIfAttribute)
    {
        List<bool> conditionsList = GetAllConitions(AttributesHelper.GetTargetObject(property),
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
            FieldInfo conditionFieldInfo = AttributesHelper.GetFieldInfo(conditions[i], targetObject);

            if (IsFieldBoolType(conditionFieldInfo))
            {
                conditionsToReturn.Add((bool)conditionFieldInfo.GetValue(targetObject));
            }

            MethodInfo conditionMethodInfo = AttributesHelper.GetMethodInfo(conditions[i], targetObject);

            if (IsMethodReturnBoolType(conditionMethodInfo))
            {
                conditionsToReturn.Add((bool)AttributesHelper
                    .GetMethodReturnValue(conditionMethodInfo, targetObject));
            }

            PropertyInfo conditionPropertyInfo = AttributesHelper
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

    public void DrawShowIfEnumConditionType(Rect position, SerializedProperty property,
        GUIContent label, ShowIfAttribute showIfAttribute)
    {
        Object targetObject = AttributesHelper.GetTargetObject(property);
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
        FieldInfo enumFieldInfo = AttributesHelper.GetFieldInfo(enumFieldName, targetObject);

        if (enumFieldInfo != null && enumFieldInfo.FieldType.IsSubclassOf(typeof(System.Enum)))
        {
            return (System.Enum)enumFieldInfo.GetValue(targetObject);
        }

        MethodInfo enumMethodInfo = AttributesHelper.GetMethodInfo(enumFieldName, targetObject);

        if (enumMethodInfo != null && enumMethodInfo.ReturnType.IsSubclassOf(typeof(System.Enum)))
        {
            return (System.Enum)enumMethodInfo.Invoke(targetObject, null);
        }

        PropertyInfo enumPropertyInfo = AttributesHelper.GetPropertyInfo(enumFieldName, targetObject);

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