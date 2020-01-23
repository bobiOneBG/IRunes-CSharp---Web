namespace Musaca.Services
{
    using Musaca.Data.Models;

    public interface IUsersService
    {
        User GetUserOrNull(string username, string password);

        string CreateUser(string username, string password, string email);

        void CreateOrder(string id);

        void CreateOrderIfIsNotActive(string id);
    }
}