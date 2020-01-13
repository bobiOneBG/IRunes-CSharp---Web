namespace SIS.Mvc.Framework.Attributes.Validation
{
    using System;
    using System.Text.RegularExpressions;

    public class EmailSISAttribute : ValidationSISAttribute
    {
        public EmailSISAttribute()
        {

        }
        public EmailSISAttribute(string errorMessage) : base(errorMessage)
        {
        }

        public override bool IsValid(object value)
        {
            string valueAsString = (string)Convert.ChangeType(value, typeof(string));

            return Regex.IsMatch(valueAsString, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.
                   [a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+
                   [a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }
    }
}