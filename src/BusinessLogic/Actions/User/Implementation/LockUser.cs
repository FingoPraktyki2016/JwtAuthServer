using LegnicaIT.DataAccess.Repositories.Interfaces;
using System;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class LockUser : ILockUser
    {
        private readonly IUserRepository userRepository;

        public LockUser(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Invoke(int id)
        {
            var dbUser = userRepository.GetById(id);

            if (dbUser == null || dbUser.LockedOn != null)
            {
                return;
            }

            dbUser.LockedOn = DateTime.UtcNow;
            userRepository.Edit(dbUser);
            userRepository.Save();
        }
    }
}
