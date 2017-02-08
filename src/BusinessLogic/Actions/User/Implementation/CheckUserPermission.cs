using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class CheckUserPermission : ICheckUserPermission
    {
        private readonly IGetAppUserRole getAppUserRole;
        private readonly IUserRepository userRepository;

        public CheckUserPermission(IGetAppUserRole getAppUserRole, IUserRepository userRepository)
        {
            this.getAppUserRole = getAppUserRole;
            this.userRepository = userRepository;
        }

        public bool Invoke(int requestorId, int appId, int questionedUserId)
        {
            if (requestorId == questionedUserId)
            {
                return true;
            }

            var user = userRepository.GetById(requestorId);

            if (user == null)
            {
                return false;
            }

            if (user.IsSuperAdmin)
            {
                return true;
            }

            var requestorUserAppRole = getAppUserRole.Invoke(appId, requestorId);

            var questionedUserAppRole = getAppUserRole.Invoke(appId, questionedUserId);

            if (requestorUserAppRole == UserRole.None || questionedUserAppRole == UserRole.None)
            {
                return false;
            }

            return requestorUserAppRole > questionedUserAppRole;
        }
    }
}