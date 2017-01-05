using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace JwtManager.Authorization
{
    public class PermissionsAuthorizationRequirement : IAuthorizationRequirement
    {
        // TODO: UserRole Enum should be replaced by UserRole enum from BussinesLogic Model
        public PermissionsAuthorizationRequirement(IEnumerable<UserRole> requiredPermissions)
        {
            RequiredPermission = requiredPermissions;
        }

        public IEnumerable<UserRole> RequiredPermission { get; }
    }
}