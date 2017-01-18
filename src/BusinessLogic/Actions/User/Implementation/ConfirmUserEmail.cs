using LegnicaIT.DataAccess.Repositories.Interfaces;
using System;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class ConfirmUserEmail
    {
        private readonly IUserRepository userRepository;

        public ConfirmUserEmail(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Invoke(int userId)
        {
            var userToEdit = userRepository.GetById(userId);
            if (userToEdit != null && userToEdit.EmailConfirmedOn == null)
            {
                userToEdit.EmailConfirmedOn = DateTime.UtcNow;

                userRepository.Edit(userToEdit);
                userRepository.Save();
            }
        }
    }
}