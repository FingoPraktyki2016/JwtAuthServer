using LegnicaIT.DataAccess.Context;
using LegnicaIT.DataAccess.Models;
using System;
using System.Linq;

namespace LegnicaIT.BusinessLogic.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IJwtDbContext _context) : base(_context)
        {
        }

        public void AddUser()
        {
            // for test, delete anytime
            User user = new User
            {
                Email = "123@test.pl",
                Name = "345",
                Password = "123",
                LockedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                CreatedOn = DateTime.Now,
                DeletedOn = DateTime.Now,
                EmailConfirmedOn = DateTime.Now
            };

            Add(user);
            Save();
        }

        public User GetLastUser()
        {
            return GetAll().Last();
        }

        public User GetUser(string email, string password)
        {
            var user = dbSet.FirstOrDefault(x => x.Email == email && x.Password == password);
            return user;
        }

        public bool IsUserInDatabase(string email, string password)
        {
            var user = dbSet.FirstOrDefault(x => x.Email == email && x.Password == password);

            if (user != null)
            {
                return true;
            }

            return false;
        }
    }
}
