using System.Collections.Generic;
using LegnicaIT.BusinessLogic.Enums;
using Microsoft.AspNetCore.Authorization;

namespace LegnicaIT.JwtManager.Authorization
{
    public class PermissionsAuthorizationRequirement : IAuthorizationRequirement
    {
        public PermissionsAuthorizationRequirement(IEnumerable<UserRole> requiredPermissions)
        {
            RequiredPermission = requiredPermissions;
        }

        public IEnumerable<UserRole> RequiredPermission { get; }
    }
}
