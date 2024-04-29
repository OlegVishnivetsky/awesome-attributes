public class ReadonlyIfAttribute : ConditionalAttributeBase
{
    public ReadonlyIfAttribute(string condition)
        : base(condition) { }

    public ReadonlyIfAttribute(string conditionsOperator, params string[] conditions)
        : base(conditionsOperator, conditions) { }

    public ReadonlyIfAttribute(object enumValue, string enumFieldName)
        : base(enumValue, enumFieldName) { }
}