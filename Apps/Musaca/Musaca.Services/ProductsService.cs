namespace Musaca.Services
{
    using Musaca.Data;
    using Musaca.Data.Models;

    public class ProductsService : IProductsService
    {
        private readonly MusacaDbContext db;

        public ProductsService(MusacaDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, decimal price) 
        {
            var product = new Product
            {
                Name = name,
                Price = price
            };

            db.Products.Add(product);

            db.SaveChanges();
        }
    }
}
