namespace AwesomeAttributes
{
    /// <summary>
    /// Shows the field in the inspector if the condition is true, otherwise hides it
    /// </summary>
    public class ShowIfAttribute : ConditionalAttributeBase
    {
        public ShowIfAttribute(string condition)
            : base(condition) { }

        public ShowIfAttribute(string conditionsOperator, params string[] conditions)
            : base(conditionsOperator, conditions) { }

        public ShowIfAttribute(object enumValue, string enumFieldName)
            : base(enumValue, enumFieldName) { }
    }
}