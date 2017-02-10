using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.BusinessLogic.Configuration.Seeder;
using LegnicaIT.BusinessLogic.Models.Common;
using LegnicaIT.DataAccess.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class SeedController : Controller
    {
        private readonly IAddNewUser addNewUser;
        private readonly IAddNewApp addNewApp;
        private readonly IAddNewUserApp addNewUserApps;
        private readonly IConfirmUserEmail confirmUserEmail;
        private readonly IHostingEnvironment env;

        public SeedController(IAddNewUser addNewUser,
            IAddNewApp addNewApp,
            IAddNewUserApp addNewUserApps,
            IConfirmUserEmail confirmUserEmail,
            IHostingEnvironment env)
        {
            this.addNewUser = addNewUser;
            this.addNewApp = addNewApp;
            this.addNewUserApps = addNewUserApps;
            this.confirmUserEmail = confirmUserEmail;
            this.env = env;
        }

        [HttpGet("seedall")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (!env.IsDevelopment())
            {
                var errorResult = new ResultModel<string>("Not authorized", ResultCode.Error);
                return Json(errorResult);
            }

            using (var context = new JwtDbContext())
            {
                new JwtDbContextSeeder(context).Seed(addNewUser, confirmUserEmail, addNewApp, addNewUserApps);
            }

            return Json("Database seeded");
        }
    }
}
