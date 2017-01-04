using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IAddNewUser addNewUser;
        private readonly ICheckUserExist chekcUserExists;
        private readonly IGetLastUser getLastUser;

        public UserController(IAddNewUser addNewUser, ICheckUserExist checkUserExists, IGetLastUser getLastUser)
        {
            this.addNewUser = addNewUser;
            this.chekcUserExists = checkUserExists;
            this.getLastUser = getLastUser;
        }

        //test, delete it later
        [HttpGet("adduser")]
        public JsonResult AddUser(UserModel model)
        {
            addNewUser.Invoke(model);
            return Json("Added user");
        }
    }
}