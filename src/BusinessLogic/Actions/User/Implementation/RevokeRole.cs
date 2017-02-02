using System.Linq;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
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

        public bool Invoke(int appId, int user, UserRole newRole)
        {
            var userApp = userAppRepository.FindBy(m => m.User.Id == user && m.App.Id == appId).FirstOrDefault();

            if (userApp == null)
            {
                return false;
            }

            var userRole = (UserRole) userApp.Role;

            if (userRole.Equals(newRole) || newRole > userRole)
            {
                return false;
            }

            userApp.Role = (DataAccess.Enums.UserRole) newRole;
            userAppRepository.Edit(userApp);
            userAppRepository.Save();

            return true;
        }
    }
}
