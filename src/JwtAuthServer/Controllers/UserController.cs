using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.DataAccess.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IAddNewUser addNewUser;
        private readonly IGetLastUser getLastUser;
        private readonly IChangeUserRole changeUserRole;

        public UserController(IGetLastUser getLastUser, IAddNewUser addNewUser, IChangeUserRole changeUserRole, ILogger<AuthController> logger) : base(logger)
        {
            this.addNewUser = addNewUser;
            this.getLastUser = getLastUser;
            this.changeUserRole = changeUserRole;
        }

        //test, delete it later
        [HttpGet("adduser")]
        public ActionResult AddUser()
        {
            addNewUser.Invoke();
            return Json(getLastUser.Invoke().Name);
        }

        [HttpGet("changerole")]
        public UserRoleRepository ChangeRole()
        {
            changeUserRole.Invoke(1, 3, 1);
            return null;
        }
    }
}
