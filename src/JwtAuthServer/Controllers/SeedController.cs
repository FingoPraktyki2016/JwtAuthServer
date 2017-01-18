using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
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

        public SeedController(IAddNewUser addNewUser, IAddNewApp addNewApp)
        {
            this.addNewUser = addNewUser;
            this.addNewApp = addNewApp;
        }

        [HttpGet("seedall")]
        [AllowAnonymous]
        //[Authorize(Roles = "Manager")]
        public IActionResult Index()
        {
            using (var context = new JwtDbContext())
            {
                new JwtDbContextSeeder(context).Seed(addNewUser, addNewApp);
            }
            return Json("Database seeded");
        }
    }
}
