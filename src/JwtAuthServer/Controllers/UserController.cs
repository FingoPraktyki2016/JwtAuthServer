using LegnicaIT.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        //test, delete it later
        //[HttpGet("adduser")]
        //public ActionResult AddUser()
        //{
        //    userRepository.Add();
        //    return Json(userRepository.GetLast().Name);
        //}
    }
}