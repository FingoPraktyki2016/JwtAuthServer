using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.DataAccess.Repositories.Implementations;
using LegnicaIT.JwtAuthServer.Helpers;
using LegnicaIT.JwtAuthServer.Models.ResultModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IAddNewUser addNewUser;
        private readonly IChangeAppUserRole changeAppUserRole;
        private readonly ICheckUserExist checkUserExist;
        private readonly IGetLastUser getLastUser;
        private readonly IEditUser editUser;
        private readonly IEditUserPassword editUserPassword;
        private readonly IDeleteUser deleteUser;

        public UserController(
            IAddNewUser addNewUser,
            IChangeAppUserRole changeAppUserRole,
            ICheckUserExist checkUserExist,
            IGetLastUser getLastUser,
            IEditUser editUser,
            IEditUserPassword editUserPassword,
            IDeleteUser deleteUser,
            IOptions<LoggerConfig> loggerSettings) : base(loggerSettings)
        {
            this.addNewUser = addNewUser;
            this.changeAppUserRole = changeAppUserRole;
            this.checkUserExist = checkUserExist;
            this.getLastUser = getLastUser;
            this.editUser = editUser;
            this.editUserPassword = editUserPassword;
            this.deleteUser = deleteUser;
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

        [HttpGet("changerole")]
        public UserRoleRepository ChangeRole()
        {
            changeAppUserRole.Invoke(1, 1, 3);
            return null;
        }
    }
}