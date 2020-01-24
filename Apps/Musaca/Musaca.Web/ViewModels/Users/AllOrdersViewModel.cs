namespace Musaca.Web.ViewModels.Products
{
    using System;

    public class AllOrdersViewModel
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public decimal Price { get; set; }

        public string Username { get; set; }
    }
}