using LegnicaIT.BusinessLogic.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserRepository context;

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
            context.AddUser();
            return Json(context.GetLastUser().Name);
        }
    }
}