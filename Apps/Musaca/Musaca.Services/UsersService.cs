namespace Musaca.Services
{
    using Musaca.Data;
    using Musaca.Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class UsersService : IUsersService
    {
        private readonly MusacaDbContext db;

        public UsersService(MusacaDbContext db)
        {
            this.db = db;
        }

        public void CreateOrderIfIsNotActive(string cashierId)
        {
            var order = db.Orders.SingleOrDefault(ordr => ordr.Status == OrderStatus.Active &&
                ordr.CashierId == cashierId);

            if (order == null)
            {
                this.CreateOrder(cashierId);
            }
        }

        public void CreateOrder(string cashierId)
        {
            var cashier = db.Users.SingleOrDefault(u => u.Id == cashierId);
            var order = new Order
            {
                CashierId = cashierId,
                Cashier =cashier
            };

            db.Orders.Add(order);

            db.SaveChanges();
        }

        public string CreateUser(string username, string password, string email)
        {
            var passwordHash = this.HashPassword(password);

            var user = new User()
            {
                Username = username,
                Password = passwordHash,
                Email = email
            };

            db.Users.Add(user);

            db.SaveChanges();

            return user.Id;
        }

        public User GetUserOrNull(string username, string password)
        {
            string passwordHash = this.HashPassword(password);

            return db.Users.FirstOrDefault(user => user.Username == username &&
            user.Password == passwordHash);
        }

        public IQueryable<Order> GetUserOrders(string userId)
        {
            var orders = db.Orders.Where(o => o.CashierId == userId &&
            o.Status == OrderStatus.Completed);

            return orders;
        }

        private string HashPassword(string password)
        {
            using (SHA256 shaHash = SHA256.Create())
            {
                return Encoding.UTF8.GetString(shaHash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}