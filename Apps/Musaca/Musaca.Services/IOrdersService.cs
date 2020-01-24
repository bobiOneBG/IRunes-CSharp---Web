namespace Musaca.Services
{
    using Musaca.Data.Models;
    using System.Collections.Generic;

    public interface IOrdersService
    {
        void AddProductToOrder(string cashierId, string productName);

        void CashoutOrder(string cashierId);

        ICollection<Product> GetCurrentOrderProducts(string id);
    }
}