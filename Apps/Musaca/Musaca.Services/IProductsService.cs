namespace Musaca.Services
{
    using Musaca.Data.Models;
    using System.Linq;

    public interface IProductsService
    {
        void Create(string name, decimal price);

        IQueryable<Product> GetAllProducts();
    }
}