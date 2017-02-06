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
        private readonly IUserRepository userRepository;

        public GetAppUserRole(IUserAppRepository userAppRepository, IUserRepository userRepository)
        {
            this.userAppRepository = userAppRepository;
            this.userRepository = userRepository;
        }

        public UserRole Invoke(int appId, int userId)
        {
            var user = userRepository.GetById(userId);

            if (user.IsSuperAdmin)
            {
                return UserRole.SuperAdmin;
            }

            var userAppRole = userAppRepository.FindBy(m => m.User.Id == userId && m.App.Id == appId).FirstOrDefault();

            if (userAppRole == null)
            {
                return UserRole.None;
            }

            return (UserRole)Enum.Parse(typeof(UserRole), userAppRole.Role.ToString());
        }
    }
}