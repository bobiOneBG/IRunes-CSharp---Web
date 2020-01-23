namespace SIS.Mvc.Framework.Attributes.Validation
{
    using System;

    public class RangeSISAttribute : ValidationSISAttribute
    {
        private readonly object minValue;
        private readonly object maxValue;
        private readonly Type objectType;


        public RangeSISAttribute(int minValue, int maxValue, string errorMessage)
            : base(errorMessage)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.objectType = typeof(int);
        }
        public RangeSISAttribute(double minValue, double maxValue, string errorMessage)
            : base(errorMessage)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.objectType = typeof(double);
        }
        public RangeSISAttribute(Type type, string minValue, string maxValue, string errorMessage)
            : base(errorMessage)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.objectType = type;
        }

        public override bool IsValid(object value)
        {
            if (objectType == typeof(int))
            {
                return (int)value >= (int)minValue && (int)value <= (int)maxValue;
            }

            if (objectType == typeof(double))
            {
                return (double)value >= (double)minValue && (double)value <= (double)maxValue;
            }

            if (objectType == typeof(decimal))
            {
                _ = decimal.TryParse(value.ToString(), out decimal valueM);

                var minValueM = decimal.Parse(minValue.ToString());
                var maxValueM = decimal.Parse(maxValue.ToString());

                return valueM >= minValueM && valueM <= maxValueM;
            }

            return false;
        }
    }
}