﻿using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.JwtManager.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LegnicaIT.JwtManager.Controllers
{
    [AuthorizeFilter(UserRole.User)]
    public class UserController : BaseController
    {
        public UserController(IOptions<ManagerSettings> managerSettings, IOptions<LoggerConfig> loggerSettings) : base(managerSettings, loggerSettings)
        {
        }

        /*
         * GET: /User/Details
         */
        [AuthorizeFilter(UserRole.Manager)]
        public IActionResult Details(int id)
        {
            var model = new UserModel();
            return View(model);
        }

        /*
         * GET: /User/Me
         */
        public IActionResult Me()
        {
            var model = JsonConvert.DeserializeObject<UserModel>(HttpContext.Session.GetString("UserDetails"));

            return View(model);
        }

        /*
         * GET: /User/Edit
         */
        public IActionResult Edit()
        {
            return View();
        }

        /*
         * POST: /User/Edit
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserModel model)
        {
            return View();
        }

        /*
         * GET: /User/ChangePassword
         */
        public IActionResult ChangePassword()
        {
            return View();
        }

        /*
         * POST: /User/ChangePassword
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(UserModel model)
        {
            return View();
        }

        [HttpGet ("details")]
        [AuthorizeFilter (UserRole.SuperAdmin)]
        public IActionResult Details()
        {
            return View();
        }

    }
}