using System;
using System.Linq;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Configuration;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class RevokeRole : IRevokeRole
    {
        private readonly IUserAppRepository userAppRepository;

        public RevokeRole(IUserAppRepository userAppRepository)
        {
            this.userAppRepository = userAppRepository;
        }

        public void Invoke(int appId, int user)
        {
            var userRole = userAppRepository.GetAll().FirstOrDefault(m => m.User.Id == user && m.App.Id == appId);

            try
            {
                var changeRole = (byte)(userRole.Role - 1);

                // Don't change SuperAdmin role
                if (changeRole <= 0 || changeRole == (int)UserRole.Manager)
                {
                    return;
                }

                userRole.Role = changeRole;

                userAppRepository.Edit(userRole);
                userAppRepository.Save();
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
