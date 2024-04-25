using UnityEngine;


public class ShowIfAttributeBase : PropertyAttribute
{
    public readonly string[] Conditions;
    public readonly string ConditionsOperator;

    public readonly object EnumValue;
    public readonly string EnumFieldName;

    public readonly ShowIfAttributeType ShowIfAttributeType;

    public ShowIfAttributeBase(string condition)
    {
        Conditions = new string[] { condition };

        ShowIfAttributeType = ShowIfAttributeType.OneCondition;
    }

    public ShowIfAttributeBase(string conditionsOperator, params string[] conditions)
    {
        ConditionsOperator = conditionsOperator;
        Conditions = conditions;

        ShowIfAttributeType = ShowIfAttributeType.MultipleConditions;
    }

    public ShowIfAttributeBase(object enumValue, string enumFieldName)
    {
        EnumValue = enumValue;
        EnumFieldName = enumFieldName;

        ShowIfAttributeType = ShowIfAttributeType.EnumCondition;
    }
}