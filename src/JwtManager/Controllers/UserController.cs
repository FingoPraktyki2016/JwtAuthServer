using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.JwtManager.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LegnicaIT.JwtManager.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/Edit
        [AuthorizeFilter(UserRole.Manager)]
        public IActionResult Details(int id)
        {
            var model = new UserModel();
            return View(model);
        }

        //
        // GET: /User/Me
        public IActionResult Me()
        {
            var model = JsonConvert.DeserializeObject<UserModel>(HttpContext.Session.GetString("UserDetails"));
            return View(model);
        }

        //
        // GET: /User/Edit
        public IActionResult Edit()
        {
            return View();
        }

        //
        // POST: /User/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserModel model)
        {
            return View();
        }

        //
        // GET: /User/Edit
        public IActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /User/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(UserModel model)
        {
            return View();
        }
    }
}