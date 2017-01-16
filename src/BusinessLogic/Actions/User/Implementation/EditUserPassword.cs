using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Helpers.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class EditUserPassword : IEditUserPassword
    {
        private readonly IUserRepository userRepository;
        private readonly IHasher hasher;

        public EditUserPassword(
            IUserRepository userRepository,
            IHasher hasher = null)
        {
            this.userRepository = userRepository;
            this.hasher = hasher ?? new Hasher();
        }

        public void Invoke(int id, string plainPassword)
        {
            var salt = hasher.GenerateRandomSalt();

            var userToEdit = userRepository.GetById(id);
            if (userToEdit != null)
            {
                userToEdit.PasswordHash = hasher.CreateHash(plainPassword, salt);
                userToEdit.PasswordSalt = salt;

                userRepository.Edit(userToEdit);
                userRepository.Save();

                userRepository.Edit(userToEdit);
                userRepository.Save();
            }
        }
    }
}