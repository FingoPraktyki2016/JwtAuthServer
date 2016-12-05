using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class VersionController : Controller
    {
        [HttpGet]
        public JsonResult Version()
        {
            string version = Assembly.GetEntryAssembly().GetName().Version.ToString();
            return Json(version);
        }
    }
}