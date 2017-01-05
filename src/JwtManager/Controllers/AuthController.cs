using JwtManager.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtManager.Controllers
{
    
    public class AuthController : Controller
    {
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
