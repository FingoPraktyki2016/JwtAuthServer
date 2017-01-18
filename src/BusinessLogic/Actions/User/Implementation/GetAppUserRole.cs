using System;
using System.Linq;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class GetAppUserRole : IGetAppUserRole
    {
        private readonly IUserRoleRepository userRoleRepository;

        public GetAppUserRole(IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }

        public string Invoke(int appId, int user)
        {
            try
            {
                var userAppRole = userRoleRepository.GetAll()
                    .FirstOrDefault(m => m.User.Id == user && m.App.Id == appId);

                return userAppRole.Role.Name;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}
