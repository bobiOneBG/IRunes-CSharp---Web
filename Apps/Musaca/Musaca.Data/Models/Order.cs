namespace Musaca.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Products = new List<Product>();
        }

        public string Id { get; set; }

        public string Status { get; set; }

        public DateTime IssuedOn { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        [Required]
        public string CashierId { get; set; }//– a GUID foreign key(required)

        public User Cashier { get; set; } //– a User object
    }
}