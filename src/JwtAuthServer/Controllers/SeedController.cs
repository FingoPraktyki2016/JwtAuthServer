using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Actions.Role.Interfaces;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Configuration.Seeder;
using LegnicaIT.DataAccess.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class SeedController : Controller
    {
        private readonly IAddNewUser addNewUser;
        private readonly IAddNewApp addNewApp;
        private readonly IAddNewRole addNewRole;

        public SeedController(IAddNewUser addNewUser, IAddNewApp addNewApp, IAddNewRole addNewRole)
        {
            this.addNewUser = addNewUser;
            this.addNewApp = addNewApp;
            this.addNewRole = addNewRole;
        }

        [HttpGet("seedall")]
        [AllowAnonymous]
        //   [Authorize(Roles = "ApplicationManager")]
        public IActionResult Index()
        {
            using (var context = new JwtDbContext())
            {
                new JwtDbContextSeeder(context).Seed(addNewUser, addNewApp, addNewRole);
            }
            return Json("Database seeded");
        }
    }
}
