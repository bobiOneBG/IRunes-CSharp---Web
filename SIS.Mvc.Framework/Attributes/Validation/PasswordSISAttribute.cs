namespace SIS.Mvc.Framework.Attributes.Validation
{
    using System;

    public class PasswordSISAttribute : ValidationSISAttribute
    {
        private const int PasswordMinLength = 3;

        public PasswordSISAttribute(string errorMessage) : base(errorMessage)
        {
        }

        public override bool IsValid(object value)
        {
            string valueAsString = (string)Convert.ChangeType(value, typeof(string));

            return valueAsString.Length >= PasswordMinLength ? true : false;
        }
    }
}