using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class AddNewUser : IAddNewUser
    {
        private readonly IUserRepository userRepository;

        public AddNewUser(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Invoke(UserModel user)
        {
            var timeNow = DateTime.UtcNow;
            var hasher = new Hasher();
            var salt = hasher.GenerateRandomSalt();
            var newUser = new DataAccess.Models.User()
            {
                Email = user.Email,
                PasswordSalt = salt,
                PasswordHash = hasher.CreateHash(user.Password, salt),
                Name = user.Name,
                CreatedOn = timeNow,
            };
            userRepository.Add(newUser);
            userRepository.Save();
        }
    }
}