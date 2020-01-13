namespace SIS.Mvc.Framework.Attributes.Validation
{
    public class RequiredSISAttribute : ValidationSISAttribute
    {
        public RequiredSISAttribute()
        {

        }
        public RequiredSISAttribute(string errorMessage) 
            : base(errorMessage)
        {
        }

        public override bool IsValid(object value)
        {
            return value != null;
        }
    }
}