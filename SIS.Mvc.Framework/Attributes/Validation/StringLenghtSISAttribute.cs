namespace SIS.Mvc.Framework.Attributes.Validation
{
    using System;

    public class StringLenghtSISAttribute : ValidationSISAttribute
    {
        private readonly int minLength;
        private readonly int maxLength;

        public StringLenghtSISAttribute(int minLength, int maxLength, string errorMessage)
            : base(errorMessage)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        public override bool IsValid(object value)
        {
            string valueAsString = Convert.ChangeType(value, typeof(string)) as string;

            if (valueAsString.Length >= minLength && valueAsString.Length <= maxLength)
            {
                return true;
            }

            return false;
        }
    }
}