using System;
using UnityEngine;

public class ShowIfAttribute : PropertyAttribute
{
    public readonly string[] Conditions;
    public readonly string ConditionsOperator;

    public readonly Type EnumType;
    public readonly Enum EnumValue;

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

    public ShowIfAttribute(Type enumType, Enum enumValue)
    {
        EnumType = enumType;
        EnumValue = enumValue;

        ShowIfAttributeType = ShowIfAttributeType.EnumCondition;
    }
}