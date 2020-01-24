namespace Musaca.Services
{
    using Musaca.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public interface IUsersService
    {
        User GetUserOrNull(string username, string password);

        string CreateUser(string username, string password, string email);

        void CreateOrder(string id);

        void CreateOrderIfIsNotActive(string id);

        IQueryable<Order> GetUserOrders(string userId);
    }
}