using System.Linq;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;

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
            if (!user.IsValid() ||
                userRepository.FindBy(x => x.Email == user.Email).Any())
            {
                return;
            }

            var hasher = new Hasher();

            var salt = hasher.GenerateRandomSalt();
            var passwordHash = hasher.CreateHash(user.Password, salt);
            var newUser = new DataAccess.Models.User()
            {
                Email = user.Email,
                PasswordSalt = salt,
                PasswordHash = passwordHash,
                Name = user.Name,
            };

            userRepository.Add(newUser);
            userRepository.Save();
        }
    }
}
