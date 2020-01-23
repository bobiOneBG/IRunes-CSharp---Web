namespace Musaca.Services
{
    using Musaca.Data;
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
            var order = db.Orders.SingleOrDefault(x => x.CashierId == cashierId);
            var product = db.Products.SingleOrDefault(x => x.Name == productName);

            if (order != null && product != null)
            {
                order.Products.Add(product);

                db.SaveChanges();
            }
        }
    }
}