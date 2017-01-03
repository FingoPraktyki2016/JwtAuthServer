using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IAddNewUser addNewUser;
        private readonly IGetLastUser getLastUser;
        public UserController(IGetLastUser getLastUser, IAddNewUser addNewUser)
        {
            this.addNewUser = addNewUser;
            this.getLastUser = getLastUser;
        }

        //test, delete it later
        [HttpGet("adduser")]
        public ActionResult AddUser()
        {
            addNewUser.Invoke();
            return Json(getLastUser.Invoke().Name);
        }
    }
}