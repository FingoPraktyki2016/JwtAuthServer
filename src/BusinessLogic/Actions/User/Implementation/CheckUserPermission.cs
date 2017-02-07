using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class CheckUserPermission : ICheckUserPermission
    {
        private readonly IGetAppUserRole getAppUserRole;

        public CheckUserPermission(IGetAppUserRole getAppUserRole)
        {
            this.getAppUserRole = getAppUserRole;
        }

        public bool Invoke(int requestorId, int appId, int questionedUserId)
        {
            if (requestorId == questionedUserId)
            {
                return true;
            }

            var requestorUserAppRole = getAppUserRole.Invoke(appId, requestorId);

            if (requestorUserAppRole == UserRole.SuperAdmin)
            {
                return true;
            }

            var questionedUserAppRole = getAppUserRole.Invoke(appId, questionedUserId);

            if (requestorUserAppRole == UserRole.None || questionedUserAppRole == UserRole.None)
            {
                return false;
            }

            if (requestorUserAppRole > questionedUserAppRole)
            {
                return true;
            }

            return false;
        }
    }
}