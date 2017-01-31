using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Models;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace LegnicaIT.JwtManager.Models
{
    public class UserAppModel
    {
        public UserModel UserModel = new UserModel();

        public int AppId;
        public UserRole Role;

        public UserAppModel(string userDetailsString)
        {
            AssignUserDetailsValues(userDetailsString);
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
                Role = (UserRole)Enum.Parse(typeof(UserRole), GetClaim(jwt, "role"));
            }
        }

        private string GetClaim(JwtSecurityToken token, string claimType)
        {
            return token.Claims.Where(c => c.Type == claimType).Select(c => c.Value).SingleOrDefault();
        }
    }
}