using System;
using System.Linq;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Configuration;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class GrantRole : IRevokeRole
    {
        private readonly IUserAppRepository userAppRepository;

        public GrantRole(IUserAppRepository userAppRepository)
        {
            this.userAppRepository = userAppRepository;
        }

        public void Invoke(int appId, int user)
        {
            var userRole = userAppRepository.GetAll().FirstOrDefault(m => m.User.Id == user && m.App.Id == appId);

            try
            {
                var changeRole = (byte)(userRole.Role + 1);

                // Don't grant to SuperAdmin
                if (changeRole >= (int)UserRole.SuperAdmin)
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
