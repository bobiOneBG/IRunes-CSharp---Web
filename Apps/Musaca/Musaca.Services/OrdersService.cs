namespace Musaca.Services
{
    using Musaca.Data;
    using Musaca.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OrdersService : IOrdersService
    {
        private readonly MusacaDbContext db;

        public OrdersService(MusacaDbContext db)
        {
            this.db = db;
        }

        public void AddProductToOrder(string cashierId, string productName)
        {
            var order = GetOrderFromDbByCashierId(cashierId);

            var product = db.Products.SingleOrDefault(x => x.Name == productName);

            if (order != null && product != null)
            {
                order.Products.Add(product);
                Console.WriteLine(order.Products.Count);
                db.SaveChanges();
            }
        }

        public void CashoutOrder(string cashierId)
        {
            var order = GetOrderFromDbByCashierId(cashierId);

            order.Status = OrderStatus.Completed;
            order.IssuedOn = DateTime.UtcNow;

            this.db.Orders.Update(order);
            db.SaveChanges();
        }

        public ICollection<Product> GetCurrentOrderProducts(string cashierId)
        {
            var order = GetOrderFromDbByCashierId(cashierId);

            var products= order.Products;

            return products;
        }

        private Order GetOrderFromDbByCashierId(string cashierId)
        {
            return db.Orders.FirstOrDefault(ordr => ordr.Status == OrderStatus.Active &&
                        ordr.CashierId == cashierId);
        }
    }
}