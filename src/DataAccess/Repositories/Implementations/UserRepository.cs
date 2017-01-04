using LegnicaIT.DataAccess.Context;
using LegnicaIT.DataAccess.Helpers;
using LegnicaIT.DataAccess.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System;
using System.Linq;

namespace LegnicaIT.DataAccess.Repositories.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IJwtDbContext _context) : base(_context)
        {
        }

        public User Get(string email, string password)
        {
            var user = dbSet.FirstOrDefault(x => x.Email == email);
            if(user != null)
            {
                var byteArraySalt = Convert.FromBase64String(user.PasswordSalt);
                var hashedPassword = Hasher.CreateHash(password, byteArraySalt);
                if (Hasher.VerifyHashedPassword(hashedPassword, user.PasswordHash))
                {
                    return user;
                }
            }           
            return null;
        }

        public bool IsSet(string email, string password)
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
