namespace SandBox
{
    using System;
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ValidationSISAttribute : Attribute
    {
        public ValidationSISAttribute()
        {

        }
        protected ValidationSISAttribute(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }

        public string ErrorMessage{ get; }

        public abstract bool IsValid(object value);
    }
}