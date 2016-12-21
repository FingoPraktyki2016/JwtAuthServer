using LegnicaIT.BusinessLogic.Repositories.Interfaces;
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

        public void Add()
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

        public User GetLast()
        {
            return GetAll().Last();
        }

        public User Get(string email, string password)
        {
            var user = dbSet.FirstOrDefault(x => x.Email == email && x.Password == password);
            return user;
        }

        public bool IsInDatabase(string email, string password)
        {
            var user = Get(email, password);

            if (user != null)
            {
                return true;
            }

            return false;
        }
    }
}
