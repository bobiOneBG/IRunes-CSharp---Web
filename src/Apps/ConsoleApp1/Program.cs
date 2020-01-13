namespace SandBox
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class Program
    {
        public static void Main()
        {
            var user = new User
            {
            };

            var objectProperties = user.GetType().GetProperties();

            foreach (var objectProperty in objectProperties)
            {
                var validationAttributes = objectProperty
                    .GetCustomAttributes()
                    .Where(caType => caType is ValidationSISAttribute)
                    .Cast<ValidationSISAttribute>();

                foreach (var validationAttribute in validationAttributes)
                {
                    if (validationAttribute.IsValid(objectProperty.GetValue(user))) 
                    {
                        Console.WriteLine(true);
                    }

                    else
                    {
                        Console.WriteLine(false);
                    }
                }
            }
        }
    }
}
