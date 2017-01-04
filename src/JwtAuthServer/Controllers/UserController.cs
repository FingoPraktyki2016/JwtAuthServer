using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IAddNewUser addNewUser;
        private readonly IGetLastUser getLastUser;

        public UserController(IGetLastUser getLastUser, IAddNewUser addNewUser, ILogger<AuthController> logger) : base(logger)
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
