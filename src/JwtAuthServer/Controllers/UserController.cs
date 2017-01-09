using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.DataAccess.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IAddNewUser addNewUser;
        private readonly IChangeAppUserRole changeAppUserRole;
        private readonly ICheckUserExist checkUserExist;
        private readonly IGetLastUser getLastUser;

        public UserController(IAddNewUser addNewUser, IChangeAppUserRole changeAppUserRole, ICheckUserExist checkUserExist, IGetLastUser getLastUser)
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
