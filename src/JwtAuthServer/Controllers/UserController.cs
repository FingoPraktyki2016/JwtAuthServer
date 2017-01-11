using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.BussinesLogic.Helpers;
using LegnicaIT.DataAccess.Repositories.Implementations;
using LegnicaIT.JwtAuthServer.Helpers;
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

        public UserController(IAddNewUser addNewUser,
            IChangeAppUserRole changeAppUserRole,
            ICheckUserExist checkUserExist,
            IGetLastUser getLastUser,
            IOptions<LoggerConfig> loggerSettings)
            : base(loggerSettings)

        {
            this.addNewUser = addNewUser;
            this.changeAppUserRole = changeAppUserRole;
            this.checkUserExist = checkUserExist;
            this.getLastUser = getLastUser;
        }

        //test, delete it later
        [HttpGet("adduser")]
        public JsonResult AddUser(UserModel model)
        {
            addNewUser.Invoke(model);

            logger.Information("Action completed");
            return Json("Added user");
        }

        [HttpGet("changerole")]
        public UserRoleRepository ChangeRole()
        {
            changeAppUserRole.Invoke(1, 1, 3);
            return null;
        }
    }
}