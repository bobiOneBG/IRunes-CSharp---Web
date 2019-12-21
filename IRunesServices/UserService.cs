namespace IRunesServices
{
    using IRunes.Data;
    using IRunes.Models;
    using System;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly RunesDbContext context;

        public UserService()
        {
            this.context = new RunesDbContext();
        }

        public User CreateUser(User user)
        {
            user = this.context.Users.Add(user).Entity;
            this.context.SaveChanges();

            return user;
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return context.Users
                .SingleOrDefault(user => (user.Username == username||user.Email==username)
                                      && user.Password == password);
        }
    }
}