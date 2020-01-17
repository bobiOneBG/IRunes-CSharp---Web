namespace Panda.Services
{
    using Panda.Data;
    using Panda.Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class UsersSevice : IUsersService
    {
        private readonly PandaDbContext db;

        public UsersSevice(PandaDbContext db)
        {
            this.db = db;
        }

        public string CreateUser(string username, string password, string email)
        {
            var user = new User
            {
                Username = username,
                Password =this.HashPassword(password),
                Email = email
            };

            db.Users.Add(user);

            db.SaveChanges();

            return user.Id;
        }

        public IEnumerable<string> GetUsersnames()
        {
            var userNames = db.Users.Select(user=>user.Username).ToList();

            return userNames;
        }

        public User GetUserOrNull(string username, string password)
        {
            var passwordHash = this.HashPassword(password);

            return this.db.Users.FirstOrDefault(user => user.Username == username &&
            user.Password == passwordHash);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sHA256 = SHA256.Create())
            {
                return Encoding.UTF8.GetString(sHA256.ComputeHash(Encoding.UTF8.GetBytes(password)));
            };
        }

        public IEnumerable<string> GetUsernames()
        {
            var userNames = db.Users.Select(u => u.Username).ToList();

            return userNames;
        }
    }
}