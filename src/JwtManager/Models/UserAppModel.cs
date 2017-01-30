using LegnicaIT.BusinessLogic.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LegnicaIT.JwtManager.Models
{
    public class UserAppModel
    {
        private readonly HttpContext httpContext;

        public UserAppModel(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }

        public UserModel GetUserDetails()
        {
            // Gonna come back to this later
            try
            {
                var userModel = JsonConvert.DeserializeObject<UserModel>(httpContext.Session.GetString("UserDetails"));

                return userModel;
            }
            catch
            {
                return null;
            }
        }
    }
}
