using System;
using System.Linq;

using LegnicaIT.DataAccess.Context;
using LegnicaIT.DataAccess.Helpers;
using LegnicaIT.DataAccess.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;

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
            return this.dbSet.FirstOrDefault(x => x.Email == email && x.PasswordHash == password);
            //var byteArraySalt = Convert.FromBase64String(user.PasswordSalt);
            //var hashedPassword = Hasher.CreateHash(password, byteArraySalt);
            //if (Hasher.VerifyHashedPassword(hashedPassword, user.PasswordHash))
            //{
            //    return user;
            //}

            //return null;
        }
    }
}
