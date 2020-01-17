namespace Panda.Services
{
    using Panda.Data.Models;
    using System.Collections.Generic;

    public interface IUsersService
    {
        string CreateUser(string username, string password, string email);

        User GetUserOrNull(string username, string password);

        IEnumerable<string> GetUsernames();
    }
}