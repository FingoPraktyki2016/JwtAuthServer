using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.JwtManager.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LegnicaIT.JwtManager.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(IOptions<ManagerSettings> settings)
            : base(settings)
        {

        }

        [HttpGet("/auth")]
        [AuthorizeFilter(UserRole.One, UserRole.Two)]
        public string Index()
        {
            return "user have Permission";
        }

        [HttpGet("/auth/login")]
        public string Login()
        {
            return "LoginPage";
        }
    }
}
