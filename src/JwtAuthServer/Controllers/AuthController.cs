using JwtAuthServer.Models;
using LegnicaIT.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
       
        [HttpPost("verify")]
        public JsonResult Verify(VerifyTokenModel model)
        {
            var parser = new JwtParser();
            parser.Verify(model.Token);
            return Json($"verify-result: ...");
        }
    }
}
