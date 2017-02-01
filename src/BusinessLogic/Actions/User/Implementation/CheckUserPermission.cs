using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Models;
using Microsoft.Extensions.Logging.Abstractions;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class CheckUserPermission : ICheckUserPermission
    {
        private readonly IGetAppUserRole getAppUserRole;

        public CheckUserPermission(IGetAppUserRole getAppUserRole)
        {
            this.getAppUserRole = getAppUserRole;
        }

        public bool Invoke(int requestorId, int appId, ActionType actionType, int questionedUserId)
        {
            if (actionType == ActionType.Edit)
            {
                var requestorUserAppRole = getAppUserRole.Invoke(appId, requestorId);
                var questionedUserAppRole = getAppUserRole.Invoke(appId, questionedUserId);

                if (requestorUserAppRole == UserRole.SuperAdmin)
                {
                    return true;
                }

                if (requestorUserAppRole > questionedUserAppRole)
                {
                    return true;
                }
            }

            if (actionType == ActionType.Display)
            {
                var requestorUserAppRole = getAppUserRole.Invoke(appId, requestorId);
                var questionedUserAppRole = getAppUserRole.Invoke(appId, questionedUserId);

                if (requestorUserAppRole == UserRole.None || questionedUserAppRole == UserRole.None)
                {
                    return false;
                }

                if (requestorUserAppRole == UserRole.SuperAdmin)
                {
                    return true;
                }

                if (requestorUserAppRole > questionedUserAppRole)
                {
                    return true;
                }
            }

            return false;
        }
    }
}