using System;
using UnityEngine;

public class ShowIfAttribute : PropertyAttribute
{
    public readonly string[] Conditions;
    public readonly string ConditionsOperator;

    public readonly object EnumValue;
    public readonly string EnumFieldName;

    public readonly ShowIfAttributeType ShowIfAttributeType;

    public ShowIfAttribute(string condition)
    {
        Conditions = new string[] { condition };

        ShowIfAttributeType = ShowIfAttributeType.OneCondition;
    }

    public ShowIfAttribute(string conditionsOperator, params string[] conditions)
    {
        ConditionsOperator = conditionsOperator;
        Conditions = conditions;
        
        ShowIfAttributeType = ShowIfAttributeType.MultipleConditions;
    }

    public ShowIfAttribute(object enumValue, string enumFieldName)
    {
        EnumValue = enumValue;
        EnumFieldName = enumFieldName;

        ShowIfAttributeType = ShowIfAttributeType.EnumCondition;
    }
}