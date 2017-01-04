using LegnicaIT.JwtManager.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace LegnicaIT.JwtManager.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        [HttpGet("test")]
        public ActionResult Test()
        {
            var api = new ApiHelper("http://localhost:52418/");
          
            string result = api.AcquireToken("aaa@gmail.com","1234","2");
             
            return Content(result);
        }

    }
}
