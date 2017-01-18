using System;
using System.Threading.Tasks;
using LegnicaIT.BusinessLogic.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using LegnicaIT.JwtManager.Controllers;
using LegnicaIT.BusinessLogic.Helpers;

namespace LegnicaIT.JwtManager.Authorization
{
    public class AuthorizeFilterAttribute : TypeFilterAttribute
    {
        // TODO: UserRole Enum should be replaced by UserRole enum from BusinessLogic Model
        public AuthorizeFilterAttribute(params UserRole[] userRoles)
          : base(typeof(RequiresPermissionAttributeImpl))
        {
            Arguments = new[] { new PermissionsAuthorizationRequirement(userRoles) };
        }

        private class RequiresPermissionAttributeImpl : Attribute, IAsyncActionFilter
        {
            private readonly PermissionsAuthorizationRequirement requiredPermissions;

            public RequiresPermissionAttributeImpl(PermissionsAuthorizationRequirement requiredPermissions)
            {
                this.requiredPermissions = requiredPermissions;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var controller = (BaseController)context.Controller;
                var settings = controller.Settings;

                var apiHelper = new ApiHelper(settings.ApiReference);
                //TODO: api call here

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
