using System;
using System.Linq;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Helpers;
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

        public void Invoke(int appId, int user, UserRole removeRole)
        {
            var userApp = userAppRepository.FindBy(m => m.User.Id == user && m.App.Id == appId).FirstOrDefault();

            if (userApp == null)
            {
                return;
            }

            var userRole = (UserRole)userApp.Role;

            if (userRole.HasRole(removeRole))
            {
                return;
            }

            userApp.Role = (DataAccess.Enums.UserRole)removeRole;
            userAppRepository.Edit(userApp);
            userAppRepository.Save();
        }
    }
}
