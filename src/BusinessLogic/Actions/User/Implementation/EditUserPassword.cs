using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class EditUserPassword : IEditUserPassword
    {
        private readonly IUserRepository userRepository;

        public EditUserPassword(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Invoke(int id, string plainPassword)
        {
            var salt = Hasher.GenerateRandomSalt();
            var userToEdit = userRepository.GetById(id);

            userToEdit.PasswordHash = Hasher.CreateHash(plainPassword, salt);
            userToEdit.PasswordSalt = salt;

            userRepository.Edit(userToEdit);
            userRepository.Save();
        }
    }
}