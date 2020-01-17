namespace Musaca.Data.Models
{
    using SIS.Mvc.Framework.Attributes.Validation;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }//- a GUID String, Primary Key

        [RequiredSIS]
        [MaxLength(10)]
        public string Name { get; set; }//- a string with min length 3 and max length 10 (required)

        public decimal Price { get; set; } //– a decimal with min value – 0.01.
    }
}