namespace Musaca.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}