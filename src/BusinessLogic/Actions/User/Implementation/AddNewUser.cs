using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System.Linq;

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
            var hasher = new Hasher();

            if (userRepository.FindBy(x => x.Email == user.Email) == null)
            {
                return;
            }

            var salt = hasher.GenerateRandomSalt();
            var newUser = new DataAccess.Models.User()
            {
                Email = user.Email,
                PasswordSalt = salt,
                PasswordHash = hasher.CreateHash(user.Password, salt),
                Name = user.Name,
            };

            userRepository.Add(newUser);
            userRepository.Save();
        }
    }
}