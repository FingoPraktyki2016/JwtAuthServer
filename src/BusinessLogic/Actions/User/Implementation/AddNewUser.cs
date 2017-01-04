using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.BussinesLogic.Helpers;
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
            var salt = Hasher.GenerateRandomSalt();
            var newUser = new DataAccess.Models.User()
            {
                Email = user.Email,
                PasswordSalt = salt,
                PasswordHash = Hasher.CreateHash(user.Password, salt),
                CreatedOn = DateTime.UtcNow
            };
            userRepository.Add(newUser);
        }
    }
}