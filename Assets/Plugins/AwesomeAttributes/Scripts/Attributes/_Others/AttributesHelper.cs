namespace AwesomeAttributes
{
    public static class AttributesHelper
    {
        /// <summary>
        /// Splits a camel case string into separate words
        /// </summary>
        /// <param name="input"></param>
        /// <returns>The splited string</returns>
        public static string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1",
                System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }
    }
}