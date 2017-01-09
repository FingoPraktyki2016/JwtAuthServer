using LegnicaIT.JwtManager.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LegnicaIT.JwtManager.Controllers
{
    public class BaseController : Controller
    {
        public ManagerSettings Settings { get; }

        public BaseController(IOptions<ManagerSettings> settings)
        {
            Settings = settings.Value;
        }
    }
}
