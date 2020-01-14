namespace Panda.Services
{
    using Panda.Data.Models;

    public interface IUsersSevice
    {
        string CreateUser(string username, string password, string email);

        User GetUserOrNull(string username, string password);
    }
}