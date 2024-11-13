using UnityEngine;

namespace AwesomeAttributes
{
    public class ConditionalAttributeBase : PropertyAttribute
    {
        public readonly string[] Conditions;
        public readonly string ConditionsOperator;

        public readonly object EnumValue;
        public readonly string EnumFieldName;

        public readonly ShowIfAttributeType ShowIfAttributeType;

        public ConditionalAttributeBase(string condition)
        {
            Conditions = new string[] { condition };

            ShowIfAttributeType = ShowIfAttributeType.OneCondition;
        }

        public ConditionalAttributeBase(string conditionsOperator, params string[] conditions)
        {
            ConditionsOperator = conditionsOperator;
            Conditions = conditions;

            ShowIfAttributeType = ShowIfAttributeType.MultipleConditions;
        }

        public ConditionalAttributeBase(object enumValue, string enumFieldName)
        {
            EnumValue = enumValue;
            EnumFieldName = enumFieldName;

            ShowIfAttributeType = ShowIfAttributeType.EnumCondition;
        }
    }
}