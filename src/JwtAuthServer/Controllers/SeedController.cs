using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Configuration.Seeder;
using LegnicaIT.DataAccess.Context;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class SeedController : Controller
    {
        private readonly IAddNewUser addNewUser;

        public SeedController(IAddNewUser addNewUser)
        {
            this.addNewUser = addNewUser;
        }

        [HttpGet("seedall")]
        public IActionResult Index()
        {
            using (var context = new JwtDbContext())
            {
                new JwtDbContextSeeder().Seed(context, addNewUser);
            }
            return Json("Database seeded");
        }
    }
}