using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class EditUser : IEditUser
    {
        private readonly IUserRepository userRepository;

        public EditUser(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool Invoke(UserModel user)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                return false;
            }

            var userToEdit = userRepository.GetById(user.Id);

            if (userToEdit == null)
            {
                return false;
            }

            userToEdit.Name = user.Name;

            userRepository.Edit(userToEdit);
            userRepository.Save();

            return true;
        }
    }
}