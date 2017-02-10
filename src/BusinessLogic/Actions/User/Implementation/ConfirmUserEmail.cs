using LegnicaIT.DataAccess.Repositories.Interfaces;
using System;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class ConfirmUserEmail : IConfirmUserEmail
    {
        private readonly IUserRepository userRepository;

        public ConfirmUserEmail(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool Invoke(int userId)
        {
            var userToEdit = userRepository.GetById(userId);

            if (userToEdit == null || userToEdit.EmailConfirmedOn != null)
            {
                return false;
            }

            userToEdit.EmailConfirmedOn = DateTime.UtcNow;

            userRepository.Edit(userToEdit);
            userRepository.Save();

            return true;
        }
    }
}