using LegnicaIT.BusinessLogic.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository context;

        public UserController(IUserRepository _context)
        {
            context = _context;
        }

        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        //test, delete it later
        [HttpGet("adduser")]
        public ActionResult AddUser()
        {
            context.Add();
            return Json(context.GetLast().Name);
        }
    }
}
