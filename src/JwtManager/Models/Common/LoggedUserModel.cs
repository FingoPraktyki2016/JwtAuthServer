using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Models;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace LegnicaIT.JwtManager.Models
{
    public class LoggedUserModel
    {
        public UserModel UserModel = new UserModel();

        public int AppId;
        public UserRole Role;

        // Default constructor is used by JsonConverter when deserializing object to this type.
        public LoggedUserModel()
        {
            
        }

        public LoggedUserModel(string userDetailsString, string tokenString)
        {
            AssignUserDetailsValues(userDetailsString);
            AssignTokenValues(tokenString);
        }

        private void AssignUserDetailsValues(string userDetailsString)
        {
            var userModel = JsonConvert.DeserializeObject<UserModel>(userDetailsString);

            if (userModel != null)
            {
                this.UserModel.Email = userModel.Email;
                this.UserModel.Name = userModel.Name;
                this.UserModel.Id = userModel.Id;
            }
        }

        private void AssignTokenValues(string tokenString)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadToken(tokenString) as JwtSecurityToken;

            if (jwt != null)
            {
                AppId = Convert.ToInt32(GetClaim(jwt, "appId"));
            }
        }

        private string GetClaim(JwtSecurityToken token, string claimType)
        {
            return token.Claims.Where(c => c.Type == claimType).Select(c => c.Value).FirstOrDefault();
        }
    }
}