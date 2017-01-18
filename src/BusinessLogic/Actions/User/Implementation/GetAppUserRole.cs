using System;
using System.Linq;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class GetAppUserRole : IGetAppUserRole
    {
        private readonly IUserAppRepository userAppRepository;

        public GetAppUserRole(IUserAppRepository userAppRepository)
        {
            this.userAppRepository = userAppRepository;
        }

        public UserRole Invoke(int appId, int user)
        {
            try
            {
                var userAppRole = userAppRepository.GetAll().FirstOrDefault(m => m.User.Id == user && m.App.Id == appId);

                return (UserRole)Enum.Parse(typeof(UserRole), userAppRole.Role.ToString());
            }
            catch (NullReferenceException e)
            {
                return UserRole.None;
            }
        }
    }
}