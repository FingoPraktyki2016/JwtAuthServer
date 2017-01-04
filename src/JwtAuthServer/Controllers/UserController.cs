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
        private readonly ICheckUserExist checkUserExist;
        private readonly IGetLastUser getLastUser;
        private readonly IChangeUserRole changeUserRole;

        public UserController(IGetLastUser getLastUser, IAddNewUser addNewUser, IChangeUserRole changeUserRole, ICheckUserExist checkUserExist)
        {
            this.addNewUser = addNewUser;
            this.checkUserExist = checkUserExist;
            this.getLastUser = getLastUser;
            this.changeUserRole = changeUserRole;
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
            changeUserRole.Invoke(1, 3, 1);
            return null;
        }
    }
}
