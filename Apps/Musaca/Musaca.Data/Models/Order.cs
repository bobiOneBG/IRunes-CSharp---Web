namespace Musaca.Data.Models
{
    using SIS.Mvc.Framework.Attributes.Validation;
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public Order()
        {
            Id = Guid.NewGuid().ToString();
            this.Status = OrderStatus.Active;
            this.Products = new List<Product>();
        }
        public string Id { get; set; }   

        public OrderStatus Status { get; set; }    

        public DateTime IssuedOn { get; set; }   

        public ICollection<Product> Products { get; set; }  

        [RequiredSIS]
        public string CashierId { get; set; }   

        public User Cashier { get; set; }    
    }
}