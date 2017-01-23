using System.Linq;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Models.User;
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
            if (!userRepository.FindBy(x => x.Email == user.Email).Any())
            {
                var hasher = new Hasher();

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
}