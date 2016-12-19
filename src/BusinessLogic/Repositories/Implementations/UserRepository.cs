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
            User user = new User();
            user.Email = "123";
            user.Name = "345";
            user.Password = "123";

            user.LockedOn = DateTime.Now;

            user.ModifiedOn = DateTime.Now;
            user.CreatedOn = DateTime.Now;
            user.DeletedOn = DateTime.Now;
            user.EmailConfirmedOn = DateTime.Now;

            this.Add(user);
            this.Save();
        }

        public User GetLastUser()
        {
            return this.GetAll().Last();
        }
    }
}