using LegnicaIT.BusinessLogic;
using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Helpers.Interfaces;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.BusinessLogic.Models.Common;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.JwtManager.Configuration;
using LegnicaIT.JwtManager.Models;
using LegnicaIT.JwtManager.Models.Auth;
using LegnicaIT.JwtManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading.Tasks;
using LegnicaIT.BusinessLogic.Configuration;

namespace LegnicaIT.JwtManager.Controllers
{
    [Route("auth")]
    public class AuthController : BaseController
    {
        private readonly IGetUserDetails getUserDetails;
        private readonly IAddNewUser addNewUser;
        private readonly IEmailService emailService;
        private readonly IConfirmUserEmail confirmUserEmail;

        public AuthController(IOptions<ManagerSettings> managerSettings,
            IGetUserDetails getUserDetails,
            IGetUserApps getUserApps,
            IAddNewUser addNewUser,
            IEmailService emailService,
            IConfirmUserEmail confirmUserEmail,
            ICheckUserExist checkUserExist,
            IOptions<LoggerConfig> loggerSettings,
            ISessionService<LoggedUserModel> loggedUserSessionService)
            : base(managerSettings, loggerSettings, getUserApps, loggedUserSessionService)
        {
            this.getUserDetails = getUserDetails;
            this.addNewUser = addNewUser;
            this.emailService = emailService;
            this.confirmUserEmail = confirmUserEmail;
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            if (LoggedUser != null)
            {
                return RedirectToAction("Index", "Home");
            }

            Breadcrumb.Add("Login", "Login", "Auth");

            ViewBag.ReturnUrl = returnUrl;
            var LoginModel = new LoginModel();
            return View(LoginModel);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost("login")]
        public IActionResult Login(LoginModel model, string returnUrl)
        {
            Breadcrumb.Add("Login", "Login", "Auth");

            if (!ModelState.IsValid)
            {
                logger.Information("Model is not valid");

                Alert.Warning();

                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }

            var handler = new ApiHelper(Settings.ApiReference);
            var resultString = handler.AcquireToken(model.Email, model.Password, "1");
            var result = JsonConvert.DeserializeObject<ResultModel<object>>(resultString.ResponseMessage);

            if (result.Status.Code == ResultCode.Error)
            {
                ModelState.AddModelError("Email", "Invalid email or password");

                logger.Information("Token is not valid");

                Alert.Warning();
                ViewBag.ReturnUrl = returnUrl;

                return View(model);
            }

            HttpContext.Session.SetString("token", result.Value.ToString());

            var userDetails = getUserDetails.Invoke(model.Email);
            HttpContext.Session.SetString("UserDetails", JsonConvert.SerializeObject(userDetails));

            ViewData["Message"] = model.Email;

            Alert.Success("Logged in");

            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(returnUrl);
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet("confirmemail")]
        public IActionResult ConfirmEmail(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                Alert.Danger("Invalid token");
                return View("Error");
            }

            var parser = new JwtParser();
            var verifiyResult = parser.VerifyEmailToken(token);
            if (!verifiyResult.IsValid)
            {
                Alert.Danger("InvalidToken");
                return View("Error");
            }

            if (!confirmUserEmail.Invoke(verifiyResult.UserId))
            {
                Alert.Danger("User not found or already confirmed");
                return View("Error");
            }

            Alert.Success("Email confirmed");
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var userModel = new UserModel() { Email = model.Email, Password = model.Password, Name = model.Name };
            var userAddAction = addNewUser.Invoke(userModel);

            if (userAddAction == 0)
            {
                Alert.Danger("User Already exists");
                return View();
            }
            var parser = new JwtParser();
            var confirmationToken = parser.AcquireEmailConfirmationToken(model.Email, userAddAction).Token;
            var callbackUrl = Url.Action("ConfirmEmail", "Auth", new { token = confirmationToken }, Request.Scheme);

            var emailConfirmView = RenderViewToString("ConfirmEmail", "", callbackUrl);
            await emailService.SendEmailAsync(model.Email, "Confirm your account", emailConfirmView);

            Alert.Success("Confirmation email has been sent to your account");
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet("resendconfirmationemail")]
        public IActionResult ResendConfirmationEmail()
        {
            return View();
        }

        [HttpPost("resendconfirmationemail")]
        public async Task<IActionResult> ResendConfirmationEmail(ResendEmailConfirmationViewModel model)
        {
            var user = getUserDetails.Invoke(model.Email);
            if (user == null)
            {
                Alert.Danger("Something went wrong");
                return View();
            }
            if (user.EmailConfirmedOn != null)
            {
                Alert.Danger("Email already confirmed");
                return View();
            }

            var parser = new JwtParser();
            var confirmationToken = parser.AcquireEmailConfirmationToken(model.Email, user.Id).Token;
            var callbackUrl = Url.Action("ConfirmEmail", "Auth", new { token = confirmationToken }, Request.Scheme);

            await emailService.SendEmailAsync(model.Email, "Confirm your account", callbackUrl);

            Alert.Success("Email sent");
            return View();
        }

        [AuthorizeFilter(UserRole.User)]
        [ValidateAntiForgeryToken]
        [HttpPost("switchapp/{appId}")]
        public IActionResult SwitchApp(int appId)
        {
            if (appId == LoggedUser.AppId || appId == 0)
            {
                return RedirectToAction("Me", "User", new { id = appId });
            }

            var token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
            {
                return View("Error");
            }

            var handler = new ApiHelper(Settings.ApiReference);
            var resultString = handler.SwitchApp(token, appId.ToString());
            var result = JsonConvert.DeserializeObject<ResultModel<object>>(resultString.ResponseMessage);

            if (result.Status.Code == ResultCode.Error)
            {
                logger.Information("Token is not valid");
                return View("Error");
            }

            HttpContext.Session.SetString("token", result.Value.ToString());
            return RedirectToAction("Details", "Application", new { id = appId });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                HttpContext.Session.Remove("token");
            }
            else
            {
                logger.Information("Something went wrong during logout");
            }

            HttpContext.Session.Clear();

            Alert.Success("Logged out");

            return RedirectToAction("Login");
        }
    }
}
