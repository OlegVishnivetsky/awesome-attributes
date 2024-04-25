public class ShowIfAttribute : ShowIfAttributeBase
{
    public ShowIfAttribute(string condition) 
        : base(condition) { }

    public ShowIfAttribute(string conditionsOperator, params string[] conditions) 
        : base(conditionsOperator, conditions) { }

    public ShowIfAttribute(object enumValue, string enumFieldName) 
        : base(enumValue, enumFieldName) { }
}