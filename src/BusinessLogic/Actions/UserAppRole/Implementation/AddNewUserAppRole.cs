using LegnicaIT.BusinessLogic.Actions.UserAppRole.Interfaces;
using LegnicaIT.BusinessLogic.Models.UserAppRole;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.UserAppRole.Implementation
{
    public class AddNewUserAppRole : IAddNewUserAppRole
    {
        private readonly IUserRoleRepository userRoleRepository;

        public AddNewUserAppRole(IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }

        public void Invoke(int userId, int appId, int roleId)
        {
            var newApp = new DataAccess.Models.UserAppRole()
            {
            };

            userRoleRepository.Add(newApp);
            userRoleRepository.Save();
        }
    }
}