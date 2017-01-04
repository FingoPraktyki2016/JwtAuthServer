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
        public UserRepository(IJwtDbContext context)
            : base(context)
        {
        }

        public void Add()
        {
            var byteArraySalt = Hasher.GenerateRandomSalt();
            var hashedPassword = Hasher.CreateHash("123", byteArraySalt);

            User user = new User
            {
                Email = "123@test.pl",
                Name = "345",
                PasswordHash = hashedPassword,
                PasswordSalt = byteArraySalt,
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
            var user = dbSet.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                var byteArraySalt = user.PasswordSalt;
                var hashedPassword = Hasher.CreateHash(password, byteArraySalt);
                if (Equals(hashedPassword, user.PasswordHash))
                {
                    return user;
                }
            }
            return null;
        }
    }
}