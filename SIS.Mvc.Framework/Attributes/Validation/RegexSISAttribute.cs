namespace SIS.Mvc.Framework.Attributes.Validation
{
    using System;
    using System.Text.RegularExpressions;

    public class RegexSISAttribute : ValidationSISAttribute
    {
        private readonly string pattern;

        public RegexSISAttribute(string pattern, string errorMessage) : base(errorMessage)
        {
            this.pattern = pattern;
        }

        public override bool IsValid(object value)
        {
            string valueAsString = (string)Convert.ChangeType(value, typeof(string));
            return Regex.IsMatch(valueAsString, pattern);
        }
    }
}