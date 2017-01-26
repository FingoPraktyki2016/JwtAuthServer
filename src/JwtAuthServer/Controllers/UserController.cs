﻿using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Models.Common;
using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.JwtAuthServer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IAddNewUser addNewUser;
        private readonly ICheckUserExist checkUserExist;
        private readonly IGrantRole grantRole;
        private readonly IEditUser editUser;
        private readonly IEditUserPassword editUserPassword;
        private readonly IDeleteUser deleteUser;
        private readonly IRevokeRole revokeRole;
        private readonly IGetAppUserRole getAppUserRole;
        private readonly IGetUserId getUserId;

        public UserController(
            IAddNewUser addNewUser,
            ICheckUserExist checkUserExist,
            IGrantRole grantRole,
            IEditUser editUser,
            IEditUserPassword editUserPassword,
            IDeleteUser deleteUser,
            IRevokeRole revokeRole,
            IGetAppUserRole getAppUserRole,
            IGetUserId getUserId,
            IOptions<LoggerConfig> loggerSettings) : base(loggerSettings)
        {
            this.addNewUser = addNewUser;
            this.checkUserExist = checkUserExist;
            this.grantRole = grantRole;
            this.editUser = editUser;
            this.editUserPassword = editUserPassword;
            this.deleteUser = deleteUser;
            this.revokeRole = revokeRole;
            this.getAppUserRole = getAppUserRole;
            this.getUserId = getUserId;
        }

        //test, delete it later
        [HttpGet("adduser")]
        public JsonResult AddUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var errorResult = ModelState.GetErrorModel();
                return Json(errorResult);
            }

            addNewUser.Invoke(model);
            var result = new ResultModel<UserModel>(model);
            return Json(result);
        }

        [HttpPost("getroles")]
        [Authorize]
        public JsonResult GetRoles()
        {
            var userId = getUserId.Invoke(LoggedUser.Email);
            var userRole = getAppUserRole.Invoke(LoggedUser.AppId, userId);

            var result = new ResultModel<UserRole>(userRole);
            return Json(result);
        }

        //[HttpGet("grantrole")]
        //[Authorize(Roles = "SuperAdmin")]
        //public UserRoleRepository GrantRole()
        //{
        //    //grantRole.Invoke(1, 1);
        //    return null;
        //}

        //[HttpGet("revokerole")]
        ////[Authorize(Roles = "SuperAdmin")]
        //public UserRoleRepository RevokeRole()
        //{
        //    //revokeRole.Invoke(1, 1);
        //    return null;
        //}
    }
}
