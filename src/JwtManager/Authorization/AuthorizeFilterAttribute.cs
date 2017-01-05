using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JwtManager.Authorization
{
    public class AuthorizeFilterAttribute : TypeFilterAttribute
    {
        // TODO: UserRole Enum should be replaced by UserRole enum from BussinesLogic Model
        public AuthorizeFilterAttribute(params UserRole[] userRoles)
          : base(typeof(RequiresPermissionAttributeImpl))
        {
            Arguments = new[] { new PermissionsAuthorizationRequirement(userRoles) };
        }

        private class RequiresPermissionAttributeImpl : Attribute, IAsyncResourceFilter
        {
            private readonly PermissionsAuthorizationRequirement requiredPermissions;


            public RequiresPermissionAttributeImpl(PermissionsAuthorizationRequirement requiredPermissions)
            {
                this.requiredPermissions = requiredPermissions;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                //TODO: Call service for user roles and validate any user role is in requiredPermissions
                if (false) //TODO: If user doesnt have permission redirect to login page
                {
                    context.Result = new RedirectToActionResult("Login", "Auth", null);
                    return;
                }

                await next();
            }
        }
    }
}