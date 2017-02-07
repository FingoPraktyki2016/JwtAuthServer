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
        private readonly IUserRepository userRepository;

        public GrantRole(IUserAppRepository userAppRepository, IUserRepository userRepository)
        {
            this.userAppRepository = userAppRepository;
            this.userRepository = userRepository;
        }

        public bool Invoke(int appId, int user, UserRole newRole)
        {
            if (newRole == UserRole.SuperAdmin)
            {
                var userFromDB = userRepository.GetById(user);

                if (userFromDB.IsSuperAdmin)
                {
                    return false;
                }

                userFromDB.IsSuperAdmin = true;
                userRepository.Edit(userFromDB);
                userRepository.Save();

                return true;
            }

            var userApp = userAppRepository.FindBy(m => m.User.Id == user && m.App.Id == appId).FirstOrDefault();

            if (userApp == null)
            {
                return false;
            }

            var userRole = (UserRole)userApp.Role;

            if (userRole.HasRole(newRole))
            {
                return false;
            }

            userApp.Role = (DataAccess.Enums.UserRole)newRole;
            userAppRepository.Edit(userApp);
            userAppRepository.Save();

            return true;
        }
    }
}
