using System.Linq;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class ChangeUserRole : IChangeUserRole
    {
        private readonly IUserRoleRepository userRoleRepository;
        private readonly IRoleRepository roleRepository;

        public ChangeUserRole(IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            this.userRoleRepository = userRoleRepository;
            this.roleRepository = roleRepository;
        }

        public void Invoke(int user, int role, int appId)
        {
            var changeRole = roleRepository.GetById(role);
            var users = userRoleRepository.GetAll();
            var userRole = userRoleRepository.GetAll().FirstOrDefault(m => m.User.Id == user && m.App.Id == appId);
            userRole.Role = changeRole;

            userRoleRepository.Edit(userRole);
            userRoleRepository.Save();
        }
    }
}
