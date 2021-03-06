﻿using System;
using System.Threading.Tasks;
using LegnicaIT.BusinessLogic.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using LegnicaIT.JwtManager.Controllers;
using LegnicaIT.BusinessLogic.Helpers;
using Microsoft.AspNetCore.Http;
using LegnicaIT.BusinessLogic.Models.Common;
using Newtonsoft.Json;
using System.Linq;

namespace LegnicaIT.JwtManager.Authorization
{
    public class AuthorizeFilterAttribute : TypeFilterAttribute
    {
        public AuthorizeFilterAttribute(params UserRole[] userRoles)
            : base(typeof(RequiresPermissionAttributeImpl))
        {
            Arguments = new object[] { new PermissionsAuthorizationRequirement(userRoles) };
        }

        private class RequiresPermissionAttributeImpl : Attribute, IAsyncActionFilter
        {
            private readonly PermissionsAuthorizationRequirement requiredPermissions;

            private const string SessionToken = "token";

            public RequiresPermissionAttributeImpl(PermissionsAuthorizationRequirement requiredPermissions)
            {
                this.requiredPermissions = requiredPermissions;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var controller = (BaseController)context.Controller;
                var settings = controller.Settings;
                bool isValid = false;

                string sessionToken = context.HttpContext.Session.GetString(SessionToken);

                if (sessionToken != null)
                {
                    var apiHelper = new ApiHelper(settings.ApiReference);
                    string rolesString = apiHelper.GetUserRole(sessionToken).ResponseMessage;
                    var rolesResult = JsonConvert.DeserializeObject<ResultModel<object>>(rolesString);

                    if (rolesResult.Status.Code == ResultCode.Ok)
                    {
                        UserRole userRole = (UserRole)Enum.Parse(typeof(UserRole), rolesResult.Value.ToString());

                        // make sure all required roles are assigned to user
                        isValid = requiredPermissions.RequiredPermission.All(rp => userRole.HasRole(rp));
                        controller.ViewData["Role"] = userRole;
                    }
                }

                if (!isValid)
                {
                    if (controller.LoggedUser == null)
                    {
                        controller.Alert.Danger("You need to login to complete this action");
                        context.Result = new RedirectToActionResult("Login", "Auth",
                            new { returnUrl = controller.HttpContext.Request.Path });
                    }
                    else
                    {
                        controller.Alert.Danger("You don't have permission to complete this action");
                        context.Result = new RedirectResult(controller.HttpContext.Request.Path);
                    }
                    return;
                }

                await next();
            }
        }
    }
}