using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox
{
    public class PassswordSISAttribute : ValidationSISAttribute
    {
        public PassswordSISAttribute()
        {

        }
        public PassswordSISAttribute(string errorMessage)
            : base(errorMessage)
        {
        }

        public override bool IsValid(object value)
        {
            throw new NotImplementedException();
        }
    }
}
