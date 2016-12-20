using LegnicaIT.JwtAuthServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    public class BaseController : Controller
    {
        protected AppUserModel LoggedUser { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            // we will have User authenticated by app.UseJwtBearerAuthentication(...)
            var user  = ((ControllerBase)context.Controller).User;

            if (user != null &&
                user.Identity.IsAuthenticated)
            {
                LoggedUser = new AppUserModel();
                // convert security claims to our custom user data
                LoggedUser.FillFromClaims(user.Claims);
            }
        }
    }
}
