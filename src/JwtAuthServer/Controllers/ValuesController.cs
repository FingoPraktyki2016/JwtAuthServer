using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Diagnostics;

namespace JwtAuthServer.Controllers
{
    [Route("Version/[controller]")]
    public class VersionController : Controller
    {
        // GET api/values
        [HttpGet]
        public JsonResult Get()
        {
            string version = Assembly.GetEntryAssembly().GetName().Version.ToString();          
            return Json(version);
        }
    }
}
