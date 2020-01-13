﻿namespace SIS.Mvc.Framework.Attributes.Validation
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ValidationSISAttribute : Attribute
    {
        protected ValidationSISAttribute(string errorMessage="Error Message")
        {
            this.ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }

        public abstract bool IsValid(object value);
    }
}