using System.Linq;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Actions.UserApp.Implementation;
using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class RevokeRole : IRevokeRole
    {
        private readonly IUserAppRepository userAppRepository;
        private readonly IUserRepository userRepository;
        private readonly IAddNewUserApp addNewUserApp;

        public RevokeRole(IUserAppRepository userAppRepository,
            IUserRepository userRepository,
            IAddNewUserApp addNewUserApp)
        {
            this.userAppRepository = userAppRepository;
            this.userRepository = userRepository;
            this.addNewUserApp = addNewUserApp;
        }

        public bool Invoke(int appId, int user, UserRole newRole)
        {
            var userFromDB = userRepository.GetById(user);

            if (userFromDB == null || userFromDB.IsSuperAdmin && newRole == UserRole.SuperAdmin)
            {
                return false;
            }

            var userApp = userAppRepository.FindBy(m => m.User.Id == user && m.App.Id == appId).FirstOrDefault();

            if (userApp == null && !userFromDB.IsSuperAdmin)
            {
                return false;
            }

            if (userApp == null && userFromDB.IsSuperAdmin)
            {
                // Set IsSuperAdmin flag to false
                userFromDB.IsSuperAdmin = false;
                userRepository.Edit(userFromDB);
                userRepository.Save();

                // Add new user to this app with newRole
                var newUserApp = new UserAppModel
                {
                    AppId = appId,
                    UserId = user,
                    Role = newRole
                };

                addNewUserApp.Invoke(newUserApp);

                return true;
            }

            var userRole = (UserRole) userApp.Role;

            if (userRole.Equals(newRole) || newRole > userRole)
            {
                if (!userFromDB.IsSuperAdmin)
                {
                    return false;
                }

                // Set IsSuperAdmin flag to false
                userFromDB.IsSuperAdmin = false;
                userRepository.Edit(userFromDB);
                userRepository.Save();

                return true;
            }

            userApp.Role = (DataAccess.Enums.UserRole) newRole;
            userAppRepository.Edit(userApp);
            userAppRepository.Save();

            return true;
        }
    }
}
