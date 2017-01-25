using System;
using System.Linq;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class GrantRole : IGrantRole
    {
        private readonly IUserAppRepository userAppRepository;

        public GrantRole(IUserAppRepository userAppRepository)
        {
            this.userAppRepository = userAppRepository;
        }

        public void Invoke(int appId, int user, UserRole newRole)
        {
            var userApp = userAppRepository.FindBy(m => m.User.Id == user && m.App.Id == appId).FirstOrDefault();

            if (userApp == null)
            {
                return;
            }

            var userRole = (UserRole)userApp.Role;

            if (userRole.HasRole(newRole))
            {
                return;
            }

            userApp.Role = (DataAccess.Enums.UserRole)newRole;
            userAppRepository.Edit(userApp);
            userAppRepository.Save();
        }
    }
}
