using System;
using System.Linq;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class ChangeAppUserRole : IChangeAppUserRole
    {
        private readonly IUserRoleRepository userRoleRepository;
        private readonly IRoleRepository roleRepository;

        public ChangeAppUserRole(IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            this.userRoleRepository = userRoleRepository;
            this.roleRepository = roleRepository;
        }

        public void Invoke(int appId, int user, int role)
        {
            var changeRole = roleRepository.GetById(role);
            var userRole = userRoleRepository.GetAll().FirstOrDefault(m => m.User.Id == user && m.App.Id == appId);

            try
            {
                userRole.Role = changeRole;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
                throw;
            }

            userRoleRepository.Edit(userRole);
            userRoleRepository.Save();
        }
    }
}
